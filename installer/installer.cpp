// installer.cpp : 애플리케이션에 대한 진입점을 정의합니다.
//

#include "framework.h"
#include "installer.h"
#include <string>
#include <Shlobj.h>
#include <atlconv.h>
#include "utf8.h"

#define VIRTUAL_PATH_NAME			L"ZEROFIX"

static void copy_files(const std::wstring src, const std::wstring pattern, const std::wstring obj)
{
	HANDLE				hFile;
	WIN32_FIND_DATAW	nFileSizeLow;
	std::wstring			sFile;

	CreateDirectory(obj.data(), NULL);

	if ((hFile = FindFirstFileW((src + pattern).data(), &nFileSizeLow)) != INVALID_HANDLE_VALUE) {
		do {
			sFile = nFileSizeLow.cFileName;
			if (!sFile.empty() && sFile != L"." && sFile != L"..") {
				if (nFileSizeLow.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) {
					copy_files(src + sFile + L"\\", pattern, obj + sFile + L"\\");
				}
				else {
					CopyFile((src + sFile).data(), (obj + sFile).data(), FALSE);
				}
			}
		} while (FindNextFileW(hFile, &nFileSizeLow));

		FindClose(hFile);
	}
}

static std::wstring get_virtual_store_path(HWND hWnd)
{
	TCHAR buffer[MAX_PATH];

	SHGetSpecialFolderPath(hWnd, buffer, CSIDL_LOCAL_APPDATA, 0);

	std::wstring path = buffer;
	path += L"\\";
	path += VIRTUAL_PATH_NAME;
	path += L"\\";

	CreateDirectory(path.data(), NULL);

	return path;
}

static BOOL RemoveDir(std::wstring src)
{
	HANDLE				hFile;
	WIN32_FIND_DATAW	nFileSizeLow;
	std::wstring			sFile;

	if ((hFile = FindFirstFileW((src + L"*.*").data(), &nFileSizeLow)) != INVALID_HANDLE_VALUE) {
		do {
			sFile = nFileSizeLow.cFileName;
			if (!sFile.empty() && sFile != L"." && sFile != L"..") {
				if (nFileSizeLow.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) {
					RemoveDir(src + sFile + L"\\");
				}
				else {
					DeleteFile((src + sFile).data());
				}
			}
		} while (FindNextFileW(hFile, &nFileSizeLow));

		FindClose(hFile);
	}

	return RemoveDirectory(src.data());
}

static BOOL mem2file_utf8(LPCSTR pData, int dwLength, LPCWSTR sPath)
{
	BOOL bRet = TRUE;

	try {
		HANDLE hFile = CreateFileW(sPath, GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

		if (hFile != INVALID_HANDLE_VALUE) {
			char header[] = { 0xEF, 0xBB, 0xBF };
			DWORD  dwBytesWritten;

			SetFilePointer(hFile, 0, NULL, FILE_END);
			WriteFile(hFile, header, 3, &dwBytesWritten, NULL);
			WriteFile(hFile, pData, dwLength, &dwBytesWritten, NULL);
			CloseHandle(hFile);
		}
	}
	catch (LONG&) {
		bRet = FALSE;
	}

	return bRet;
}

static DWORD get_file_length(LPCWSTR sPath)
{
	HANDLE	hFile;
	WIN32_FIND_DATAW nFileSizeLow;

	if ((hFile = FindFirstFileW(sPath, &nFileSizeLow)) != INVALID_HANDLE_VALUE) {
		FindClose(hFile);
		return nFileSizeLow.nFileSizeLow;
	}
	return (DWORD)-1;
}

static std::string file2mem_utf8(LPCWSTR sPath, BOOL convert = FALSE)
{
	DWORD dwLen = get_file_length(sPath);

	if (dwLen > 0) {
		try {
			HANDLE hFile = CreateFileW(sPath, GENERIC_READ, 0, NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

			if (hFile != INVALID_HANDLE_VALUE) {
				DWORD dwBytesRead;
				LPBYTE pBuf = new BYTE[dwLen + 2];
				ReadFile(hFile, pBuf, dwLen, &dwBytesRead, NULL);
				CloseHandle(hFile);

				pBuf[dwBytesRead] = 0;

				std::string sRet;

				if (pBuf[0] == 0xEF && pBuf[1] == 0xBB && pBuf[2] == 0xBF) sRet = (LPCSTR)pBuf + 3;
				else if (convert) {

					USES_CONVERSION;

					std::wstring s = A2W((LPSTR)pBuf);

					utf8::utf16to8(s.begin(), s.end(), back_inserter(sRet));
				}
				else sRet = (LPCSTR)pBuf;

				delete pBuf;

				return sRet;
			}
		}
		catch (LONG&) {
		}
	}
	return "";
}

static std::wstring find_html_inc(std::string & sBuf, int & nStart, int & nEnd)
{
	nStart = sBuf.find("<!--{{{", nStart);
	if (nStart != std::string::npos) {
		nEnd = sBuf.find("}}}-->", nStart);

		if (nStart != std::string::npos && nEnd >= 0) {
			USES_CONVERSION;
			std::wstring sRet = A2W(sBuf.substr(nStart + 7, nEnd - (nStart + 7)).data());
			nEnd += 6;
			return sRet;
		}
	}
	return L"";
}

static const std::string & replace_html(std::string & sBuf, const int & nStart, const int & nEnd, const std::string & sHTML)
{
	sBuf = sBuf.substr(0, nStart) + sHTML + sBuf.substr(nEnd);
	return sBuf;
}

static BOOL replace_html_inc(const std::wstring & sPath, std::string & sBuf)
{
	int start = 0, end = 0;
	std::wstring path;
	std::string sRet;
	BOOL bRet = FALSE;

	while (!(path = find_html_inc(sBuf, start, end)).empty()) {
		if (!(sRet = file2mem_utf8((sPath + path).data())).empty()) {
			sBuf = replace_html(sBuf, start, end, sRet);
			bRet = TRUE;
		}
		start = end;
	}
	return bRet;
}

static void replace_html_includes(const std::wstring src, const std::wstring obj)
{
	HANDLE				hFile;
	WIN32_FIND_DATAW	nFileSizeLow;
	std::wstring		sFile;
	std::string			sBuf;

	if ((hFile = FindFirstFileW((src + L"*.html").data(), &nFileSizeLow)) != INVALID_HANDLE_VALUE) {
		do {
			sFile = nFileSizeLow.cFileName;
			if (!sFile.empty() && sFile != L"." && sFile != L".." && !(sBuf = file2mem_utf8((src + sFile).data())).empty() && replace_html_inc(src, sBuf)) {
				mem2file_utf8(sBuf.data(), sBuf.size(), (obj + sFile).data());
			}
		} while (FindNextFileW(hFile, &nFileSizeLow));

		FindClose(hFile);
	}
}

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

	WCHAR sExePath[MAX_PATH];

    // TODO: 여기에 코드를 입력합니다.
	GetModuleFileNameW(NULL, sExePath, MAX_PATH);

	std::wstring str = sExePath;
	std::wstring rpath = str.substr(0, str.rfind(_T('\\')) + 1);

	if (_tcsstr(lpCmdLine, L"init")) {
		std::wstring path = get_virtual_store_path(NULL);

		copy_files(str.substr(0, str.rfind(_T('\\')) + 1), L"*.*", path.data());

		return FALSE;
	}
	else if (_tcsstr(lpCmdLine, L"exit")) {
		return FALSE;
	}
	else if (_tcsstr(lpCmdLine, L"pack")) {
		std::wstring wpath = get_virtual_store_path(NULL);

		if (wpath.empty()) wpath = rpath;

		RemoveDir(rpath + L"..\\..\\..\\hres\\");

		copy_files((rpath + L"..\\..\\..\\asset\\").data(), L"*.*", (rpath + L"..\\..\\hres\\").data());

		replace_html_includes((rpath + L"..\\..\\..\\asset\\").data(), (rpath + L"..\\..\\hres\\").data());

		copy_files((rpath + L"..\\..\\..\\hres\\").data(), L"*.*", wpath.data());
	}

    return 0;
}

