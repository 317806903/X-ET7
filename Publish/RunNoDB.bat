if exist win-x64 (
	cd win-x64/Bin
) else (
	if exist Bin (
		cd Bin
	) else (
		exit
	)
)
xcopy runtimes\win\native . /s/e/y
title ===Release_Zpb===
dotnet.exe App.dll --Process=1 --StartConfig=StartConfig/Release_Zpb --Console=1 --NeedDB=0
