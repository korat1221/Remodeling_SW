#ifndef _SQLITE3SEO_H_
#define _SQLITE3SEO_H_

#include <string>

extern "C"
{

	__declspec(dllexport) void __stdcall SetInfo(LPCWSTR path, bool isEncrypt);
	__declspec(dllexport) int __stdcall OpenDB(LPCWSTR path, int idx);
	__declspec(dllexport) int __stdcall OpenMemoryDB(int idx);
	__declspec(dllexport) int __stdcall CloseDB(int idx);
	__declspec(dllexport) int __stdcall SaveDB(LPCWSTR path, int idx);
	__declspec(dllexport) WCHAR* __stdcall QuerySQL(int idx, LPCWSTR sql);
	__declspec(dllexport) int __stdcall ExecuteSQL(int idx, LPCWSTR sql);

};

#endif  /* ifndef _SQLITE3SEO_H_ */
