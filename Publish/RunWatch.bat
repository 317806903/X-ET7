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
dotnet.exe App.dll --AppType=Watcher --StartConfig=StartConfig/Release_Zpb3 --Console=1 --NeedDB=0
