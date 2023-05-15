set WORKSPACE=..\..

set GEN_CLIENT=Luban.ClientServer\Luban.ClientServer.exe
set CONF_ROOT=%WORKSPACE%\Unity\Assets\Config\Excel\StartConfig\Localhost
set OUTPUT_CODE_DIR=%WORKSPACE%\Unity\Assets\Scripts\Codes\Model\Generate
set OUTPUT_DATA_DIR=%WORKSPACE%\Config\Excel
set OUTPUT_JSON_DIR=%WORKSPACE%\Config\Json

echo ======================= Server ==========================
%GEN_CLIENT% --template_search_path CustomTemplate -j cfg --^
 -d %CONF_ROOT%\__root__.xml ^
 --input_data_dir %CONF_ROOT% ^
 --output_code_dir %OUTPUT_CODE_DIR%\Server\Config\StartConfig ^
 --output_data_dir %OUTPUT_DATA_DIR%\s\StartConfig ^
 --output:exclude_tags c ^
 --gen_types code_cs_bin,data_bin ^
 -s server

if %ERRORLEVEL% NEQ 0 (
	pause
	exit
)

echo ======================= Server Json ==========================
%GEN_CLIENT% --template_search_path CustomTemplate -j cfg --^
 -d %CONF_ROOT%\__root__.xml ^
 --input_data_dir %CONF_ROOT% ^
 --output_code_dir %OUTPUT_CODE_DIR%\Server\Config\StartConfig ^
 --output_data_dir %OUTPUT_JSON_DIR%\s\StartConfig ^
 --output:exclude_tags c ^
 --gen_types data_json ^
 -s server

if %ERRORLEVEL% NEQ 0 (
	pause
	exit
)

echo "============success============"
pause
