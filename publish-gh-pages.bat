@echo off
REM KeryxPars Message Viewer - Local Publish Script (Windows)
REM This script publishes the Blazor WASM app for GitHub Pages deployment

echo.
echo ?? Publishing KeryxPars Message Viewer for GitHub Pages...
echo.

REM Navigate to the solution directory
cd /d "%~dp0"

REM Clean previous publish
echo ?? Cleaning previous publish...
if exist publish rmdir /s /q publish

REM Restore dependencies
echo ?? Restoring dependencies...
dotnet restore KeryxPars.MessageViewer\KeryxPars.MessageViewer.sln

REM Build the solution
echo ?? Building solution...
dotnet build KeryxPars.MessageViewer\KeryxPars.MessageViewer.sln --configuration Release --no-restore

REM Publish the client project
echo ?? Publishing Blazor WASM client...
dotnet publish KeryxPars.MessageViewer\KeryxPars.MessageViewer.Client\KeryxPars.MessageViewer.Client.csproj -c Release -o publish

REM Update base tag for GitHub Pages using PowerShell
echo ?? Updating base tag for GitHub Pages...
powershell -Command "(Get-Content publish\wwwroot\index.html) -replace '<base href=\"/\" />', '<base href=\"/KeryxPars/\" />' | Set-Content publish\wwwroot\index.html"

REM Copy index.html to 404.html
echo ?? Creating 404.html for SPA routing...
copy publish\wwwroot\index.html publish\wwwroot\404.html

REM Add .nojekyll
echo ?? Adding .nojekyll file...
type nul > publish\wwwroot\.nojekyll

echo.
echo ? Publish complete!
echo.
echo ?? Published files are in: .\publish\wwwroot
echo.
echo ?? To deploy manually:
echo    1. Copy contents of .\publish\wwwroot to your web server
echo    2. Or commit and push to trigger GitHub Actions
echo.
echo ?? Your app is ready for deployment!
echo.
pause
