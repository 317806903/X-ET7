set WORKSPACE=..\..

set GEN_CLIENT=Luban.ClientServer\Luban.ClientServer.exe
set CONF_ROOT=%WORKSPACE%\Unity\Assets\Config\Excel\AbilityConfig
set OUTPUT_CODE_DIR=%WORKSPACE%\Unity\Assets\Scripts\Codes\Model\Generate
set OUTPUT_DATA_DIR=%WORKSPACE%\Config\Excel
set OUTPUT_JSON_DIR=%WORKSPACE%\Config\Json

echo ======================= ClientServer ==========================
%GEN_CLIENT% --template_search_path CustomTemplate -j cfg --^
 -d %CONF_ROOT%\__root__.xml ^
 --input_data_dir %CONF_ROOT% ^
 --output_code_dir %OUTPUT_CODE_DIR%\ClientServer\Config\AbilityConfig ^
 --output_data_dir %OUTPUT_DATA_DIR%\cs\AbilityConfig ^
 --gen_types code_cs_bin,data_bin ^
 -s all ^
 --validate_root_dir %WORKSPACE%\Unity ^
 --l10n:input_text_files %CONF_ROOT%\TextKeyValue\LocalizeConfig_Excel.xlsx ^
 --l10n:text_field_name text_en ^
 --l10n:output_not_translated_text_file %CONF_ROOT%\TextKeyValue\__NotLocalized_Excel.txt

if %ERRORLEVEL% NEQ 0 (
	pause
	exit
)

echo "============success============"
timeout /t 1
