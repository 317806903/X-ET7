set WORKSPACE=..\..

set IsOnlyExcel=%1
echo "xxxxxx "%IsOnlyExcel%"
if "%IsOnlyExcel%"=="" (
	set IsOnlyExcel=False
)
set GEN_CLIENT=Luban.ClientServer\Luban.ClientServer.exe
set CONF_ROOT=%WORKSPACE%\Unity\Assets\Config\Excel\AbilityConfig
set OUTPUT_CODE_DIR=%WORKSPACE%\Unity\Assets\Scripts\Codes\Model\Generate
set OUTPUT_DATA_DIR=%WORKSPACE%\Config\Excel
set OUTPUT_JSON_DIR=%WORKSPACE%\Config\Json

echo ======================= ClientServer ==========================

if "%IsOnlyExcel%"=="False" (
	%GEN_CLIENT% --template_search_path CustomTemplate -j cfg --^
	 -d %CONF_ROOT%\__root__.xml ^
	 --input_data_dir %CONF_ROOT% ^
	 --output_code_dir %OUTPUT_CODE_DIR%\ClientServer\Config\AbilityConfig ^
	 --output_data_dir %OUTPUT_DATA_DIR%\cs\AbilityConfig ^
	 --gen_types code_cs_bin,data_bin ^
	 -s all

	if %ERRORLEVEL% NEQ 0 (
		pause
		exit
	)
) else (
	%GEN_CLIENT% --template_search_path CustomTemplate -j cfg --^
	 -d %CONF_ROOT%\__root__.xml ^
	 --input_data_dir %CONF_ROOT% ^
	 --output_code_dir %OUTPUT_CODE_DIR%\ClientServer\Config\AbilityConfig ^
	 --output_data_dir %OUTPUT_DATA_DIR%\cs\AbilityConfig ^
	 --gen_types data_bin ^
	 -s all

	if %ERRORLEVEL% NEQ 0 (
		pause
		exit
	)
)

echo ======================= ClientServer Json ==========================
%GEN_CLIENT% --template_search_path CustomTemplate -j cfg --^
 -d %CONF_ROOT%\__root__.xml ^
 --input_data_dir %CONF_ROOT% ^
 --output_data_dir %OUTPUT_JSON_DIR%\cs\AbilityConfig ^
 --gen_types data_json ^
 -s all

if %ERRORLEVEL% NEQ 0 (
	pause
	exit
)

echo "============success============"
timeout /t 3
