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
dotnet.exe App.dll --Process=2 --StartConfig=StartConfig/Release_Zpb2 --Console=1 --NeedDB=1