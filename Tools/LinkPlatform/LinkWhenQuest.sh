#!/bin/bash

# 获取脚本所在的目录
scriptDirectory=$(pwd)

# 往上退一层
parentDirectory=$(dirname "$scriptDirectory")
# 再往上退一层
grandparentDirectory=$(dirname "$parentDirectory")

projectPath=$grandparentDirectory
echo "projectPath: $projectPath"

# 定义一个包含目标目录和链接名称的数组
declare -A links
links["Unity/Assets/Config"]="Unity_Quest/Assets/Config"
links["Unity/Assets/Bundles/Config/AbilityConfig"]="Unity_Quest/Assets/Bundles/Config/AbilityConfig"
links["Unity/Assets/Scripts/Core/Common"]="Unity_Quest/Assets/Scripts/Core/Common"
links["Unity/Assets/Scripts/Editor/Common"]="Unity_Quest/Assets/Scripts/Editor/Common"
links["Unity/Assets/Scripts/Loader/Common"]="Unity_Quest/Assets/Scripts/Loader/Common"
links["Unity/Assets/Scripts/ThirdParty/Common"]="Unity_Quest/Assets/Scripts/ThirdParty/Common"
links["Unity/Assets/Scripts/Codes/Editor/Common"]="Unity_Quest/Assets/Scripts/Codes/Editor/Common"
links["Unity/Assets/Scripts/Codes/Hotfix/Client/Common"]="Unity_Quest/Assets/Scripts/Codes/Hotfix/Client/Common"
links["Unity/Assets/Scripts/Codes/Hotfix/Server/Common"]="Unity_Quest/Assets/Scripts/Codes/Hotfix/Server/Common"
links["Unity/Assets/Scripts/Codes/Hotfix/Share/Common"]="Unity_Quest/Assets/Scripts/Codes/Hotfix/Share/Common"
links["Unity/Assets/Scripts/Codes/HotfixView/Client/Common"]="Unity_Quest/Assets/Scripts/Codes/HotfixView/Client/Common"
links["Unity/Assets/Scripts/Codes/Model/Client/Common"]="Unity_Quest/Assets/Scripts/Codes/Model/Client/Common"
links["Unity/Assets/Scripts/Codes/Model/Server/Common"]="Unity_Quest/Assets/Scripts/Codes/Model/Server/Common"
links["Unity/Assets/Scripts/Codes/Model/Share/Common"]="Unity_Quest/Assets/Scripts/Codes/Model/Share/Common"
links["Unity/Assets/Scripts/Codes/Model/Generate"]="Unity_Quest/Assets/Scripts/Codes/Model/Generate"
links["Unity/Assets/Scripts/Codes/ModelView/Client/Common"]="Unity_Quest/Assets/Scripts/Codes/ModelView/Client/Common"

# 遍历数组并创建软链接
for targetDir in "${!links[@]}"; do
    linkName=${links[$targetDir]}
    
    linkDirPath="$projectPath/$linkName"
    targetDirPath="$projectPath/$targetDir"

    echo "linkDirPath: $linkDirPath"
    echo "targetDirPath: $targetDirPath"
    if [ -L "$linkDirPath" ] || [ -d "$linkDirPath" ]; then
        echo "==链接已存在: $linkDirPath"
    else
        ln -sfn "$targetDirPath" "$linkDirPath"
        echo "==[软链接创建成功]: $linkDirPath -> $targetDirPath"
    fi
done

