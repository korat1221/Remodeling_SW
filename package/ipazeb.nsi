;NSIS Modern User Interface
;Multilingual Example Script
;Written by Joost Verburg

;--------------------------------
;Include Modern UI

  !include "MUI2.nsh"

;--------------------------------
;General

  ;Name and file
  Name $(Title)
  OutFile "main.exe"

  ;Default installation folder
  InstallDir "$LOCALAPPDATA\ZEROFIX"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\ZEROFIX" ""

  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin

;--------------------------------
;Variables

  Var StartMenuFolder
  Var WebView2
;--------------------------------
;Interface Settings

  !define MUI_ICON "orange-install.ico"
  !define MUI_UNICON "orange-uninstall.ico"
  !define MUI_HEADERIMAGE
  !define MUI_HEADERIMAGE_BITMAP "nsis.bmp" ; optional
  !define MUI_ABORTWARNING
    
;--------------------------------
;Language Selection Dialog Settings

  ;Remember the installer language
  !define MUI_LANGDLL_REGISTRY_ROOT "HKCU" 
  !define MUI_LANGDLL_REGISTRY_KEY "Software\ZEROFIX" 
  !define MUI_LANGDLL_REGISTRY_VALUENAME "Installer Language"

;--------------------------------
;Pages

  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "$(myLicenseData)"
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder
  !insertmacro MUI_PAGE_INSTFILES
  !define MUI_FINISHPAGE_RUN "$INSTDIR\net6.0-windows\main.exe"
  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English" ;first language is the default language
  !insertmacro MUI_LANGUAGE "Korean"

  LangString Uninstall ${LANG_ENGLISH} "ZEROFIX Uninstall"
  LangString Uninstall ${LANG_KOREAN} "ZEROFIX 프로그램 제거"
  LangString Title ${LANG_ENGLISH} "ZEROFIX"
  LangString Title ${LANG_KOREAN} "ZEROFIX"
  LangString License ${LANG_ENGLISH} "license_en.txt"
  LangString License ${LANG_KOREAN} "license_ko.txt"
  LangString WebViewIns ${LANG_ENGLISH} "..."
  LangString WebViewIns ${LANG_KOREAN} "WebView2 컴포넌트를 찾을수 없습니다. 새로 설치하시겠습니까 ?"
  LangString NodeJSIns ${LANG_ENGLISH} "..."
  LangString NodeJSIns ${LANG_KOREAN} "Node.js 를 찾을수 없습니다. 새로 설치하시겠습니까 ?"
  LangString DotNetIns ${LANG_ENGLISH} "..."
  LangString DotNetIns ${LANG_KOREAN} "정상적인 프로그램 실행을 위해 닷넷 컴포넌트를 설치하시겠습니까 ?"

  LicenseLangString myLicenseData ${LANG_ENGLISH} "license_en.txt"
  LicenseLangString myLicenseData ${LANG_KOREAN} "license_ko.txt"
;LicenseData $(myLicenseData)

;--------------------------------
;Reserve Files
  
  ;If you are using solid compression, files that are required before
  ;the actual installation should be stored first in the data block,
  ;because this will make your installer start faster.
  
  !insertmacro MUI_RESERVEFILE_LANGDLL

;--------------------------------
;Installer Sections

Section "Dummy Section" SecDummy

  SetOverwrite try

  SetOutPath "$INSTDIR"

  ;ADD YOUR OWN FILES HERE...
  File /r ..\main\bin\Release\*.*
  File /r ..\asset\*.*

  CreateShortCut "$DESKTOP\$(Title).lnk" "$INSTDIR\net6.0-windows\main.exe"

  ;Store installation folder
  WriteRegStr HKCU "Software\ZEROFIX" "" $INSTDIR

  ;Run on startup
  ;WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Run" "ZEROFIX" "$INSTDIR\net6.0-windows\main.exe"

  ; write uninstall strings
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZEROFIX" "DisplayName" "$(Title)"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZEROFIX" "UninstallString" '"$INSTDIR\Uninstall.exe"'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZEROFIX" "DisplayIcon" "$INSTDIR\Uninstall.exe,0"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZEROFIX" "DisplayVersion" "1.0.0"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZEROFIX" "URLInfoAbout" "http://www.ipazeb.org/"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZEROFIX" "Publisher" "www.ipazeb.org"
  WriteRegDWORD  HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZEROFIX" "EstimatedSize" 174912

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

  ReadRegStr $WebView2 HKLM "SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}" "pv"

  ${If} $WebView2 == '' 
    MessageBox MB_YESNO $(WebViewIns) IDYES true IDNO false
    true:
      ExecWait "MicrosoftEdgeWebView2RuntimeInstallerX64.exe"
      Goto next
    false:
    next:
  ${EndIf}

  ClearErrors
  ExecWait '"node" -v' $0
  ${If} ${Errors}
    MessageBox MB_YESNO $(NodeJSIns) IDYES true2 IDNO false2
    true2:
      ExecWait 'msiexec /i "node-v18.16.0-x64.msi"'
      Goto next2
    false2:
    next2:
  ${EndIF}

  ClearErrors
	MessageBox MB_YESNO $(DotNetIns) IDYES true3 IDNO false3
	true3:
	  ExecWait 'windowsdesktop-runtime-6.0.16-win-x64.exe'
	  Goto next3
	false3:
	next3:

  ExecWait '"$INSTDIR\installer.exe" init'

  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    
    ;Create shortcuts
    CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\$(Uninstall).lnk" "$INSTDIR\Uninstall.exe"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\$(Title).lnk" "$INSTDIR\net6.0-windows\main.exe"

  !insertmacro MUI_STARTMENU_WRITE_END

SectionEnd

;--------------------------------
;Installer Functions

Function .onInit

	# the plugins dir is automatically deleted when the installer exits
	InitPluginsDir
	File /oname=$PLUGINSDIR\splash.bmp "orange-nsis.bmp"

	splash::show 1000 $PLUGINSDIR\splash

	Pop $0 ; $0 has '1' if the user closed the splash screen early,
			; '0' if everything closed normally, and '-1' if some error occurred.

;  !insertmacro MUI_LANGDLL_DISPLAY

  StrCpy $INSTDIR "$PROGRAMFILES\ZEROFIX"

FunctionEnd
;--------------------------------
;Descriptions

  ;USE A LANGUAGE STRING IF YOU WANT YOUR DESCRIPTIONS TO BE LANGAUGE SPECIFIC

  ;Assign descriptions to sections
;  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
;   !insertmacro MUI_DESCRIPTION_TEXT ${SecDummy} "A test section."
;  !insertmacro MUI_FUNCTION_DESCRIPTION_END

 
;--------------------------------
;Uninstaller Section

Section "Uninstall"

  ExecWait '"$INSTDIR\installer.exe" exit'

  Delete "$DESKTOP\$(Title).lnk"

  RMDir /r "$INSTDIR"

  !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
    
  RMDir /r "$SMPROGRAMS\$StartMenuFolder"
  RMDir /r "$LOCALAPPDATA\ZEROFIX"

  DeleteRegKey HKCU "Software\ZEROFIX"
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZEROFIX"
  DeleteRegValue HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Run" "ZEROFIX"

SectionEnd

;--------------------------------
;Uninstaller Functions

Function un.onInit

  !insertmacro MUI_UNGETLANGUAGE
  
FunctionEnd