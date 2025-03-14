#ifndef _SQLITE3SEO_H_
#define _SQLITE3SEO_H_

__declspec(dllexport) int __stdcall OpenDB(const char* path, int idx);
__declspec(dllexport) int __stdcall CloseDB(int idx);
__declspec(dllexport) int __stdcall SaveDB(const char* path, int idx);
__declspec(dllexport) const char* __stdcall QuerySQL(int idx, const char* sql);
__declspec(dllexport) int __stdcall ExecuteSQL(int idx, const char* sql);

#endif  /* ifndef _SQLITE3SEO_H_ */
