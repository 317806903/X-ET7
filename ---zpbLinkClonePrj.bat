@echo off

if exist "%SystemRoot%\SysWOW64" path %path%;%windir%\SysNative;%SystemRoot%\SysWOW64;%~dp0
bcdedit >nul
if '%errorlevel%' NEQ '0' (goto UACPrompt) else (goto UACAdmin)
:UACPrompt
%1 start "" mshta vbscript:createobject("shell.application").shellexecute("""%~0""","::",,"runas",1)(window.close)&exit
exit /B
:UACAdmin
cd /d "%~dp0"
echo 当前运行路径是：%CD%（已获取管理员权限）

set ProjectOrgName=Unity
set ProjectCloneName=Unity_Clone

rd /s /Q %ProjectCloneName%

if not exist %ProjectOrgName% (
	echo ProjectOrgName=%ProjectOrgName% not exist!
	goto end
)

if exist %ProjectCloneName% (
	echo ProjectCloneName=%ProjectCloneName% already exist!
	goto end
) else (
    md %ProjectCloneName%
)

mklink /J %ProjectCloneName%\Assets %ProjectOrgName%\Assets
mklink /J %ProjectCloneName%\Packages %ProjectOrgName%\Packages
mklink /J %ProjectCloneName%\ProjectSettings %ProjectOrgName%\ProjectSettings

setlocal enabledelayedexpansion
set curDir=Library
if exist %ProjectOrgName%\%curDir% (
	md %ProjectCloneName%\%curDir%
	for /f %%s in ('dir /b "%ProjectOrgName%\%curDir%"') do (
		set isMatch=false
		::for %%i in ("PackageCache", "ScriptAssemblies", "Artifacts", "ShaderCache") do (
		for %%i in ("PackageCache", "ScriptAssemblies", "Artifacts", "ShaderCache") do (
			if %%i=="%%s" (
				set isMatch=true
			)
		)
		if !isMatch!==true (
			for %%a in (%ProjectOrgName%\%curDir%\%%s) do (
				set "b=%%~aa"
				set F=!b:~0,1!
				
				if !F!==d (
					mklink /J %ProjectCloneName%\%curDir%\%%s %ProjectOrgName%\%curDir%\%%s
				) else (
					mklink %ProjectCloneName%\%curDir%\%%s %ProjectOrgName%\%curDir%\%%s
				)
			)
		) else (
			for %%a in (%ProjectOrgName%\%curDir%\%%s) do (
				set "b=%%~aa"
				set F=!b:~0,1!
				
				if !F!==d (
					xcopy %ProjectOrgName%\%curDir%\%%s %ProjectCloneName%\%curDir%\%%s /s/e/i/y
				) else (
					copy %ProjectOrgName%\%curDir%\%%s %ProjectCloneName%\%curDir%\%%s /y
				)
			)
		)
	)
)



echo ====================success!
:end
pause