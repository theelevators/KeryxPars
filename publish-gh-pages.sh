#!/bin/bash

# KeryxPars Message Viewer - Local Publish Script
# This script publishes the Blazor WASM app for GitHub Pages deployment

echo "?? Publishing KeryxPars Message Viewer for GitHub Pages..."
echo ""

# Navigate to the solution directory
cd "$(dirname "$0")"

# Clean previous publish
echo "?? Cleaning previous publish..."
rm -rf publish

# Restore dependencies
echo "?? Restoring dependencies..."
dotnet restore KeryxPars.MessageViewer/KeryxPars.MessageViewer.sln

# Build the solution
echo "?? Building solution..."
dotnet build KeryxPars.MessageViewer/KeryxPars.MessageViewer.sln --configuration Release --no-restore

# Publish the client project
echo "?? Publishing Blazor WASM client..."
dotnet publish KeryxPars.MessageViewer/KeryxPars.MessageViewer.Client/KeryxPars.MessageViewer.Client.csproj -c Release -o publish

# Update base tag for GitHub Pages
echo "?? Updating base tag for GitHub Pages..."
if [[ "$OSTYPE" == "darwin"* ]]; then
    # macOS
    sed -i '' 's/<base href="\/" \/>/<base href="\/KeryxPars\/" \/>/g' publish/wwwroot/index.html
else
    # Linux
    sed -i 's/<base href="\/" \/>/<base href="\/KeryxPars\/" \/>/g' publish/wwwroot/index.html
fi

# Copy index.html to 404.html
echo "?? Creating 404.html for SPA routing..."
cp publish/wwwroot/index.html publish/wwwroot/404.html

# Add .nojekyll
echo "?? Adding .nojekyll file..."
touch publish/wwwroot/.nojekyll

echo ""
echo "? Publish complete!"
echo ""
echo "?? Published files are in: ./publish/wwwroot"
echo ""
echo "?? To deploy manually:"
echo "   1. Copy contents of ./publish/wwwroot to your web server"
echo "   2. Or commit and push to trigger GitHub Actions"
echo ""
echo "?? Your app is ready for deployment!"
