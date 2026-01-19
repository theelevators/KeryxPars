# ?? GitHub Pages Deployment Guide

## Overview
This guide explains how to deploy the KeryxPars Message Viewer to GitHub Pages.

---

## ?? Prerequisites

1. **GitHub Repository** - Your code is already in a GitHub repository
2. **GitHub Pages enabled** - You need to enable GitHub Pages in your repository settings

---

## ?? Setup Instructions

### 1. Enable GitHub Pages

1. Go to your GitHub repository: `https://github.com/theelevators/KeryxPars`
2. Click **Settings** ? **Pages** (in the left sidebar)
3. Under **Source**, select:
   - **Source**: `GitHub Actions`
4. Click **Save**

### 2. Configure the Repository

The repository is now configured with:
- ? `.nojekyll` file (prevents Jekyll processing)
- ? `404.html` (handles client-side routing for SPAs)
- ? GitHub Actions workflow (`.github/workflows/deploy-gh-pages.yml`)
- ? Base path handling in `index.html`

---

## ?? Deployment Process

### Automatic Deployment

Every time you push to the `master` or `main` branch, the GitHub Actions workflow will:

1. ? Build the Blazor WASM application
2. ? Publish to the `publish/wwwroot` directory
3. ? Update the base path to `/KeryxPars/`
4. ? Copy `index.html` to `404.html` for deep linking support
5. ? Deploy to GitHub Pages

### Manual Deployment

You can also trigger deployment manually:

1. Go to **Actions** tab in your repository
2. Select **Deploy to GitHub Pages** workflow
3. Click **Run workflow** ? **Run workflow**

---

## ?? Access Your Deployed App

Once deployed, your app will be available at:

**https://theelevators.github.io/KeryxPars/**

---

## ??? Local Testing with GitHub Pages Base Path

To test locally with the GitHub Pages base path:

1. Update `index.html` base tag to match production:
   ```html
   <base href="/KeryxPars/" />
   ```

2. Run the app:
   ```bash
   cd KeryxPars.MessageViewer/KeryxPars.MessageViewer.Client
   dotnet watch
   ```

3. Access via: `http://localhost:5000/KeryxPars/`

**Remember to revert to `<base href="/" />` for local development!**

---

## ?? Published Files Structure

After deployment, the following structure is published to GitHub Pages:

```
wwwroot/
??? .nojekyll                          # Prevents Jekyll processing
??? 404.html                           # Handles deep links
??? index.html                         # Main entry point
??? css/
?   ??? app.css                       # Your styles
??? lib/                              # Libraries (Bootstrap, etc.)
??? _framework/                       # Blazor framework files
?   ??? blazor.webassembly.js
?   ??? dotnet.*.wasm
?   ??? KeryxPars.*.dll
??? sample-data/                      # Sample data files
```

---

## ?? Configuration Details

### Base Path Configuration

The GitHub Actions workflow automatically updates the base path:

```bash
sed -i 's/<base href="\/" \/>/<base href="\/KeryxPars\/" \/>/g' publish/wwwroot/index.html
```

This ensures all resources load correctly from the GitHub Pages subdirectory.

### SPA Routing

The `404.html` redirect script handles client-side routing:
- When a user navigates to `/KeryxPars/viewer`, GitHub Pages serves `404.html`
- The script extracts the path and redirects to `index.html?/viewer`
- The Blazor router handles the rest

---

## ?? Troubleshooting

### Issue: 404 errors on navigation

**Solution**: Make sure `.nojekyll` file exists in the published output.

### Issue: CSS/JS files not loading

**Solution**: Verify the base path in `index.html` is set to `/KeryxPars/`.

### Issue: Deep links don't work

**Solution**: Ensure `404.html` has the redirect script and matches `index.html`.

### Issue: Build fails in GitHub Actions

**Solution**: 
1. Check the Actions logs for specific errors
2. Ensure all dependencies are restored
3. Verify the .NET version matches (8.0.x)

---

## ?? Custom Domain (Optional)

If you want to use a custom domain:

1. In repository **Settings** ? **Pages**
2. Enter your custom domain in the **Custom domain** field
3. Add a `CNAME` record in your DNS settings pointing to:
   ```
   theelevators.github.io
   ```

---

## ?? Updating the Deployed App

Simply push changes to the `master` branch:

```bash
git add .
git commit -m "Update message viewer"
git push origin master
```

The workflow will automatically deploy the changes within ~2-5 minutes.

---

## ?? Monitoring Deployments

1. Go to **Actions** tab in your repository
2. Click on the latest **Deploy to GitHub Pages** workflow run
3. View logs for each step (Build, Publish, Deploy)
4. Check for any errors or warnings

---

## ?? Success!

Your KeryxPars Message Viewer is now live on GitHub Pages! ??

**Live URL**: https://theelevators.github.io/KeryxPars/

Share it with the world and enjoy your market-leading HL7 viewer! ??
