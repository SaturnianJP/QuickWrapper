# Satania VPM Template

A GitHub-Pages-ready template for hosting a VRChat Package Manager (VPM) listing, with a custom cyberpunk-styled landing page.

Forked from [vrchat-community/template-package](https://github.com/vrchat-community/template-package) by **Satania** and **Claude**.

[![Use This Template](https://img.shields.io/badge/-Use%20This%20Template-2ea44f?style=for-the-badge&logo=github)](https://github.com/SaturnianJP/satania-vpm-template/generate)

## What you get

- A working VPM listing repository wired to GitHub Actions
- An automated **Build Release** workflow that zips your package and creates a GitHub Release
- An automated **Build Repo Listing** workflow that publishes the listing site to GitHub Pages
- A custom landing page (`Website/index.html`) with a cyber-themed UI

## Setup after forking / using this template

1. **Create the repository** via the *Use This Template* button above.
2. **Set the `PACKAGE_NAME` repository variable**
   - Go to *Settings → Secrets and variables → Actions → Variables*
   - Add a new variable named `PACKAGE_NAME` whose value matches your package folder name (e.g. `net.yourname.yourpackage`).
   - Without this variable, the *Build Release* workflow is skipped.
3. **Enable GitHub Pages**
   - Go to *Settings → Pages*
   - Set **Source** to `GitHub Actions`.
4. **Replace the example package**
   - Rename `Packages/net.satania.package_test/` to your own package id (e.g. `Packages/net.yourname.yourpackage/`).
   - Edit `Packages/<your-package>/package.json` (`name`, `displayName`, `version`, `description`, `author`, etc.).
   - Update `Packages/.gitignore`: replace the `!net.satania.package_test` line with your new package folder name.
5. **Customize the landing page** (`Website/index.html`)
   - The hero banner now reads from `{{ listingInfo.Name }}` and `{{ listingInfo.Description }}`, so it auto-fills from your listing metadata. No hardcoded edits required.
   - Replace `Website/icon.png` and `Website/favicon.ico` with your own artwork.
6. **Run the workflows**
   - Go to *Actions → Build Release → Run workflow*. This creates the first GitHub Release for your package.
   - The *Build Repo Listing* workflow runs automatically after a release and publishes the listing to GitHub Pages.

## Adding the listing to VCC

After GitHub Pages publishes, your listing URL is `https://<your-username>.github.io/<your-repo>/index.json`. Open it in your browser and click **Add to VCC**, or paste the URL into VCC manually.

## Releasing a new version

1. Bump the `version` field in `Packages/<your-package>/package.json`.
2. Commit and push.
3. Run the *Build Release* workflow from the Actions tab. The new version will be tagged, released, and the listing site updated.

## Documentation

For deeper details about the VPM listing format, listing source files, and what the build action does, see:

- [VRChat Creator Companion docs](https://vcc.docs.vrchat.com)
- [Original VRChat package template](https://github.com/vrchat-community/template-package)

## License

[MIT](LICENSE)
