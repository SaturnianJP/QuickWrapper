# QuickWrapper VPM Listing

VRChat Package Manager (VPM) listing repository for **QuickWrapper** (`net.satania.quickwrapper`), hosted via GitHub Pages.

Built from [satania-vpm-template](https://github.com/SaturnianJP/satania-vpm-template) by **Satania** and **Claude**.

## Setup

1. **Set the `PACKAGE_NAME` repository variable**
   - Go to *Settings → Secrets and variables → Actions → Variables*
   - Add a new variable named `PACKAGE_NAME` with value `net.satania.quickwrapper`.
2. **Enable GitHub Pages**
   - Go to *Settings → Pages*
   - Set **Source** to `GitHub Actions`.
3. **Run the workflows**
   - Go to *Actions → Build Release → Run workflow*. This creates the first GitHub Release.
   - The *Build Repo Listing* workflow runs automatically after a release and publishes the listing to GitHub Pages.

## Adding the listing to VCC

After GitHub Pages publishes, your listing URL is:

```
https://<your-username>.github.io/QuickWrapper/index.json
```

Open it in your browser and click **Add to VCC**, or paste the URL into VCC manually.

## Releasing a new version

1. Bump the `version` field in `Packages/net.satania.quickwrapper/package.json`.
2. Commit and push.
3. Run the *Build Release* workflow from the Actions tab.

## License

[MIT](LICENSE)
