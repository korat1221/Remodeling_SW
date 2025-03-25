// dllmain.cpp : DLL 애플리케이션의 진입점을 정의합니다.
#include "pch.h"
#include "_wrap_sqlite.h"
#include "wrap_sqlite3.h"
#include "json/json.h"
#include "utf8.h"
#include <io.h>
#include <fstream>
#include <iostream>
#include <string>

#define PASSWORD    L"1234"

std::wstring gProPath;
bool gIsEncrypt = true;

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

const std::wstring get_temp_path(std::wstring out = std::wstring(L""))
{
    WCHAR path[MAX_PATH] = { 0, };
    WCHAR file[MAX_PATH] = { 0, };

    GetTempPath(MAX_PATH, path);
    GetTempFileName(path, TEXT("__"), 0, file);

	out = file;
    return out;
}

void run_app(BOOL bWait, const std::wstring fmt, ...) {
    int size = ((int)fmt.size()) * 2;    
    std::wstring buffer;    
    va_list ap;    
    
    while (1) { 
        buffer.resize(size);        
        va_start(ap, fmt);        
        int n = _vsnwprintf_s((WCHAR*)buffer.data(), buffer.size(), _TRUNCATE, fmt.c_str(), ap);        
        va_end(ap);        
        if (n > -1 && n < size) { 
            buffer.resize(n);            
            break;
        }        
        if (n > -1)   
            size = n + 1;  
        else   
            size *= 2;
    } 

    STARTUPINFO StartupInfo = { 0 };
    PROCESS_INFORMATION ProcessInfo;

    StartupInfo.cb = sizeof(STARTUPINFO);
    StartupInfo.dwFlags = STARTF_USESHOWWINDOW;

    if (CreateProcess(NULL, (LPWSTR)buffer.c_str(), NULL, NULL, FALSE, 0, NULL, NULL, &StartupInfo, &ProcessInfo)) {
        if (bWait) WaitForSingleObject(ProcessInfo.hProcess, INFINITE);
        CloseHandle(ProcessInfo.hThread);
        CloseHandle(ProcessInfo.hProcess);
    }
}

bool read_header(FILE* db)
{
    uint8_t sql_buf[100] = { 0 };

    /* load the header */
    if (fread(sql_buf, 100, 1, db) != 1) {
        return false;
    }

    /* verify that we have a proper header */
    if (strcmp((char*)sql_buf, "SQLite format 3") != 0) {
        return false;
    }

    return true;
}

bool is_sqlite_file(std::string path) 
{
    FILE* f_input;

    errno_t err = fopen_s(&f_input, path.data(), "rb");
    if (err != 0) {
        /* Unable to open file for reading */
        return false;
    }

    bool ret = read_header(f_input);

    fclose(f_input);

    return ret;
}

std::string asUTF8(std::wstring text) {
    std::string s;
    utf8::utf16to8(text.begin(), text.end(), std::back_inserter(s));
    return s;
}

std::wstring asUTF16(std::string text) {
	std::wstring s;
	utf8::utf8to16(text.begin(), text.end(), std::back_inserter(s));
	return s;
}

void encrypt_db(std::wstring from, std::wstring to) {
    DeleteFile(to.data());
    run_app(TRUE, L"\"%sopenssl\\openssl.exe\" enc -aes-256-cbc -salt -in \"%s\" -out \"%s\" -k %s", gProPath.data(), from.data(), to.data(), PASSWORD);
    DeleteFile(from.data());
}

__declspec(dllexport) void __stdcall SetInfo(LPCWSTR path, bool isEncrypt)
{
	gIsEncrypt = isEncrypt;
    gProPath = path;
}

__declspec(dllexport) int __stdcall OpenDB(LPCWSTR path, int idx)
{
    if (gIsEncrypt) {
        std::wstring p0 = gProPath + path;
        std::wstring p = get_temp_path();

        if (is_sqlite_file(asUTF8(p0))) {
            encrypt_db(p0, p);
            DeleteFile(p0.data());
            MoveFile(p.data(), p0.data());
        }

        run_app(TRUE, L"\"%sopenssl\\openssl.exe\" enc -d -aes-256-cbc -in \"%s\" -out \"%s\" -k %s", gProPath.data(), p0.data(), p.data(), PASSWORD);

        int ret = db_open(asUTF8(p).c_str(), idx);

        DeleteFile(p.data());

        return ret;
    }
    else {
		return db_open(asUTF8(gProPath + path).c_str(), idx);
    }
}

__declspec(dllexport) int __stdcall OpenMemoryDB(int idx)
{
    return db_open_mem(idx);
}

__declspec(dllexport) int __stdcall CloseDB(int idx)
{
    return db_close(idx);
}

__declspec(dllexport) int __stdcall SaveDB(LPCWSTR path, int idx)
{
    if (gIsEncrypt) {
        std::wstring p = get_temp_path();
        int ret = db_save(asUTF8(p).c_str(), idx);

        encrypt_db(p, gProPath + path);

        DeleteFile(p.data());

        return ret;
    }
    else {
        return db_save(asUTF8(gProPath + path).c_str(), idx);
    }
}

__declspec(dllexport) WCHAR * __stdcall QuerySQL(int idx, LPCWSTR sql)
{
    std::wstring ret = L"[]";

    if (!db_query_sql(idx, asUTF8(sql).c_str())) {
        int r = 0, c, rows = DB_ROW_COUNT(idx), columns = DB_COL_COUNT(idx);

		if (r < rows) {
            char* ch;
            Json::Value jData;
            Json::FastWriter write;

            while (++r <= rows) {
                Json::Value jVal;
                c = -1;
                while (++c < columns) {
                    ch = DB_GET_VALUE(idx, r, c);
                    jVal.append(ch == 0 ? "" : ch);
                }
                jData.append(jVal);
            }
            ret = asUTF16(write.write(jData));
        }
    }

    int len = (ret.size() + 1) * sizeof(WCHAR);
    WCHAR* res = (WCHAR*)LocalAlloc(LPTR, len);

    if (res != NULL) {
        wcscpy_s(res, len / sizeof(WCHAR), ret.data());
        return res;
    }

    return NULL;
}

__declspec(dllexport) int __stdcall ExecuteSQL(int idx, LPCWSTR sql)
{
    return db_execute_sql(idx, asUTF8(sql).c_str());
}
