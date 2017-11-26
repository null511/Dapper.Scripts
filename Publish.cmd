if exist "Dapper.Scripts\bin\Package\" rmdir /Q /S "Dapper.Scripts\bin\Package"
nuget pack "Dapper.Scripts\Dapper.Scripts.csproj" -OutputDirectory "Dapper.Scripts\bin\Package" -Build -Prop "Configuration=Release;Platform=AnyCPU"
nuget push "Dapper.Scripts\bin\Package\*.nupkg" -Source "https://www.nuget.org/api/v2/package" -NonInteractive
pause
