set baseDir=%~dp0\src

REM https://docs.asp.net/en/latest/tutorials/dotnet-watch.html
REM Adding dotnet watch to a project
REM Add Microsoft.DotNet.Watcher.Tools to the tools section of the WebApp/project.json file as in the example below:
REM "tools": {
REM   "Microsoft.DotNet.Watcher.Tools": "1.0.0-preview2-final"

set ASPNETCORE_ENVIRONMENT=Development
set Project_BaseURI=localhost
set Project_IS4_URL=http://%Project_BaseURI%:5000
set Project_API_URL=http://%Project_BaseURI%:5001
set Project_MVC_URL=http://%Project_BaseURI%:5002

start cmd.exe /K "cd %baseDir%\ServerSide\IdentityServerWithAspNetIdentity\ && dotnet watch run" 
start cmd.exe /K "cd %baseDir%\ServerSide\Api\ && dotnet watch run" 
start cmd.exe /K "cd %baseDir%\ServerSide\IdentityServerClients\MvcClient\ && dotnet watch run" 
