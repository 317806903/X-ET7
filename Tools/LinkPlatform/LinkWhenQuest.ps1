# Windows PowerShell 脚本 - 处理多个目录的软链接

# 获取脚本所在的目录
$scriptDirectory = $PSScriptRoot

# 往上退一层
$parentDirectory = Split-Path -Path $scriptDirectory -Parent
# 再往上退一层
$grandparentDirectory = Split-Path -Path $parentDirectory -Parent


$projectPath = $grandparentDirectory
"projectPath: $projectPath"  | Write-Output | Out-Host

# 定义一个包含目标目录和链接名称的哈希表
$links = @{
    "Unity\Assets\Config" = "Unity_Quest\Assets\Config"
    "Unity\Assets\Bundles\Config\AbilityConfig" = "Unity_Quest\Assets\Bundles\Config\AbilityConfig"
    "Unity\Assets\Scripts\Core\Common" = "Unity_Quest\Assets\Scripts\Core\Common"
    "Unity\Assets\Scripts\Editor\Common" = "Unity_Quest\Assets\Scripts\Editor\Common"
    "Unity\Assets\Scripts\Loader\Common" = "Unity_Quest\Assets\Scripts\Loader\Common"
    "Unity\Assets\Scripts\ThirdParty\Common" = "Unity_Quest\Assets\Scripts\ThirdParty\Common"
    "Unity\Assets\Scripts\Codes\Editor\Common" = "Unity_Quest\Assets\Scripts\Codes\Editor\Common"
    "Unity\Assets\Scripts\Codes\Hotfix\Client\Common" = "Unity_Quest\Assets\Scripts\Codes\Hotfix\Client\Common"
    "Unity\Assets\Scripts\Codes\Hotfix\Server\Common" = "Unity_Quest\Assets\Scripts\Codes\Hotfix\Server\Common"
    "Unity\Assets\Scripts\Codes\Hotfix\Share\Common" = "Unity_Quest\Assets\Scripts\Codes\Hotfix\Share\Common"
    "Unity\Assets\Scripts\Codes\HotfixView\Client\Common" = "Unity_Quest\Assets\Scripts\Codes\HotfixView\Client\Common"
    "Unity\Assets\Scripts\Codes\Model\Client\Common" = "Unity_Quest\Assets\Scripts\Codes\Model\Client\Common"
    "Unity\Assets\Scripts\Codes\Model\Server\Common" = "Unity_Quest\Assets\Scripts\Codes\Model\Server\Common"
    "Unity\Assets\Scripts\Codes\Model\Share\Common" = "Unity_Quest\Assets\Scripts\Codes\Model\Share\Common"
    "Unity\Assets\Scripts\Codes\Model\Generate" = "Unity_Quest\Assets\Scripts\Codes\Model\Generate"
    "Unity\Assets\Scripts\Codes\ModelView\Client\Common" = "Unity_Quest\Assets\Scripts\Codes\ModelView\Client\Common"
}

# 遍历哈希表并创建软链接
foreach ($targetDir in $links.Keys) {
    $linkName = $links[$targetDir]
	
	$linkDirPath = $projectPath + "\" + $linkName
	$targetDirPath = $projectPath + "\" + $targetDir

    if (Test-Path $linkDirPath) {
        "==Link is Exist: $linkName"  | Write-Output | Out-Host
    } else {
        New-Item -ItemType SymbolicLink -Path $linkDirPath -Target $targetDirPath -ErrorAction Stop -Force
		"==Create Link Success: $linkDirPath -> $targetDirPath"  | Write-Output | Out-Host
    }
}
