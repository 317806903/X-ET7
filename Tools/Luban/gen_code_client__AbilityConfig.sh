#!/bin/zsh
WORKSPACE=../..

GEN_CLIENT=Luban.ClientServer/Luban.ClientServer.dll
CONF_ROOT=${WORKSPACE}/Unity/Assets/Config/Excel/AbilityConfig
OUTPUT_CODE_DIR=${WORKSPACE}/Unity/Assets/Scripts/Codes/Model/Generate
OUTPUT_DATA_DIR=${WORKSPACE}/Config/Excel
OUTPUT_JSON_DIR=${WORKSPACE}/Config/Json
  
echo ======================= Client ==========================
/usr/local/share/dotnet/dotnet ${GEN_CLIENT} --template_search_path CustomTemplate -j cfg --\
 -d ${CONF_ROOT}/__root__.xml \
 --input_data_dir ${CONF_ROOT} \
 --output_code_dir ${OUTPUT_CODE_DIR}/Client/Config/AbilityConfig \
 --output_data_dir ${OUTPUT_DATA_DIR}/c/AbilityConfig \
 --output:exclude_tags s \
 --gen_types code_cs_bin,data_bin \
 -s client
  
if [ $? -eq 1 ]; then
    exit 1
fi

echo ======================= Client Json ==========================
/usr/local/share/dotnet/dotnet ${GEN_CLIENT} --template_search_path CustomTemplate -j cfg --\
 -d ${CONF_ROOT}/__root__.xml \
 --input_data_dir ${CONF_ROOT} \
 --output_data_dir ${OUTPUT_JSON_DIR}/c/AbilityConfig \
 --output:exclude_tags s \
 --gen_types data_json \
 -s client

if [ $? -eq 1 ]; then
    exit 1
fi
