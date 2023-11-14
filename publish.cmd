del .\bin\Dapper.Scripts.*.nupkg
dotnet pack -c Release -o .\bin
cd bin
dotnet nuget push Dapper.Scripts.*.nupkg -s https://api.nuget.org/v3/index.json -k %NUGET_APIKEY%
