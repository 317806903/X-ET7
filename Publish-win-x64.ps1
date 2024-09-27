dotnet publish -r win-x64 --no-self-contained --no-dependencies -c Release
if($?){
	echo success
} else {
	pause
	exit
}

$path = ".\Publish\win-x64"
Remove-Item $path\Bin\ -Recurse -ErrorAction Ignore
Copy-Item .\Bin\win-x64\publish -Destination $path\Bin -Recurse -Force
Remove-Item $path\Config -Recurse -ErrorAction Ignore
Copy-Item .\Config -Destination $path\Config  -Recurse -Force
Remove-Item $path\Config\Json -Recurse -ErrorAction Ignore
Remove-Item $path\Config\Excel\cs -Recurse -ErrorAction Ignore
Remove-Item $path\Config\Excel\c -Recurse -ErrorAction Ignore


#---------------------------------
$path = ".\Publish\win-x64-Quick"

if((Test-Path $path)){
	Remove-Item $path\Bin\ -Recurse -ErrorAction Ignore
    mkdir -p $path\Bin
} else {
    mkdir -p $path\Bin
}

Copy-Item .\Bin\win-x64\publish\App.dll -Destination $path\Bin\App.dll -Recurse -Force
Copy-Item .\Bin\win-x64\publish\Model.dll -Destination $path\Bin\Model.dll -Recurse -Force
Copy-Item .\Bin\win-x64\publish\Hotfix.dll -Destination $path\Bin\Hotfix.dll -Recurse -Force
Copy-Item .\Bin\win-x64\publish\Loader.dll -Destination $path\Bin\Loader.dll -Recurse -Force
Copy-Item .\Bin\win-x64\publish\Core.dll -Destination $path\Bin\Core.dll -Recurse -Force
Copy-Item .\Bin\win-x64\publish\ThirdParty.dll -Destination $path\Bin\ThirdParty.dll -Recurse -Force

Remove-Item $path\Config -Recurse -ErrorAction Ignore
Copy-Item .\Config\Excel\s\AbilityConfig -Destination $path\Config\Excel\s\AbilityConfig  -Recurse -Force

echo "`n`n=========================Build Server win-x64 Success========================="
timeout /t 1
