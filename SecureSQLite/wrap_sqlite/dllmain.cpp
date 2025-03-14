// dllmain.cpp : DLL 애플리케이션의 진입점을 정의합니다.
#include "pch.h"
#include "_wrap_sqlite.h"

#include <mutex>

#include "json/json.h"

std::mutex mtxsql;

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

__declspec(dllexport) int __stdcall OpenDB(const char* path, int idx)
{
    return db_open(path, idx);
}

__declspec(dllexport) int __stdcall CloseDB(int idx)
{
    return db_close(idx);
}

__declspec(dllexport) int __stdcall SaveDB(const char* path, int idx)
{
    return db_save(path, idx);
}

__declspec(dllexport) const char* __stdcall QuerySQL(int idx, const char* sql)
{
    std::lock_guard<std::mutex> guard(mtxsql);
    Json::FastWriter write;
    static std::string s;
    Json::Value jData;

    if (!db_query_sql(idx, sql)) {
        Json::Value jVal;
        int r = 0, c, rows = DB_ROW_COUNT(0), columns = DB_COL_COUNT(0);

        while (++r <= rows) {
            c = -1;
            while (++c < columns) {
                jVal[DB_COL_NAME(0, c)] = DB_GET_VALUE(0, r, c);
            }
            jData.append(jVal);
        }
    }

    s = write.write(jData);

    return s.data();
}

__declspec(dllexport) int __stdcall ExecuteSQL(int idx, const char* sql)
{
    return db_execute_sql(idx, sql);
}
