// dllmain.cpp : DLL 애플리케이션의 진입점을 정의합니다.
#include "pch.h"
#include "_wrap_sqlite.h"
#include "wrap_sqlite3.h"
#include "json/json.h"
#include "utf8.h"

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

__declspec(dllexport) int __stdcall OpenDB(LPCWSTR path, int idx)
{
    std::wstring path0 = path;
    std::string s;

    utf8::utf16to8(path0.begin(), path0.end(), std::back_inserter(s));

    return db_open(s.c_str(), idx);
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
    std::wstring path0 = path;
    std::string s;

    utf8::utf16to8(path0.begin(), path0.end(), std::back_inserter(s));

    return db_save(s.c_str(), idx);
}

__declspec(dllexport) WCHAR * __stdcall QuerySQL(int idx, LPCWSTR sql)
{
    std::string s;
    std::wstring ret;
    std::wstring sql0 = sql;

    utf8::utf16to8(sql0.begin(), sql0.end(), std::back_inserter(s));

    if (!db_query_sql(idx, s.c_str())) {
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
            s = write.write(jData);

            utf8::utf8to16(s.begin(), s.end(), std::back_inserter(ret));
        }
    }

    if (ret.empty()) {
        ret = L"[]";
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
    std::string s;
    std::wstring sql0 = sql;

    utf8::utf16to8(sql0.begin(), sql0.end(), std::back_inserter(s));

    return db_execute_sql(idx, s.c_str());
}
