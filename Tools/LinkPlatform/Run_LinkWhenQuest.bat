@echo off
REM 获取批处理文件所在的目录
set "curPath=%~dp0"

REM 输出批处理文件所在的目录
echo Batch script directory is: %curPath%

set absolutePath="%curPath%/LinkWhenQuest.ps1"

echo absolutePath is: %absolutePath%

powershell -NoProfile -ExecutionPolicy Bypass -File %absolutePath%
pause