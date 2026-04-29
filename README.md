# QuickWrapper VPM Listing

VRChat Package Manager (VPM) listing repository for **QuickWrapper** (`net.satania.quickwrapper`), hosted via GitHub Pages.

[![Open Listing Page](https://img.shields.io/badge/-Open%20Listing%20Page-2ea44f?style=for-the-badge&logo=github)](https://saturnianjp.github.io/QuickWrapper/)
[![Add to VCC](https://img.shields.io/badge/-Add%20to%20VCC-blue?style=for-the-badge&logo=vrchat)](https://saturnianjp.github.io/QuickWrapper/index.json)

Built from [satania-vpm-template](https://github.com/SaturnianJP/satania-vpm-template) (Private Repository) by **Satania** and **Claude**.

## QuickWrapper とは

任意のアセットをまとめて `.unitypackage` として書き出すための、Unity Editor 拡張です。エクスポートの設定を ScriptableObject として保存しておけるので、リリース作業や配布物のビルドを毎回ポチポチ手動で組む必要がなくなります。

### 機能

- **Manifest** (`Assets/Create/Quick Wrapper/Manifest`)
  - エクスポートしたいアセットをリストで保持する ScriptableObject。
  - 依存関係 (`Include Dependencies`) / Library アセット (`Include Library Assets`) の同梱有無をトグルで切り替え可能。
  - 出力先パスを指定して、Inspector の **Export Package** ボタン一発で `.unitypackage` を書き出し。
  - パスの「参照」ボタンからファイルダイアログで保存先を指定可能。
- **Batch** (`Assets/Create/Quick Wrapper/Batcher`)
  - 複数の Manifest をまとめて持てる ScriptableObject。
  - **Export All** ボタンで一括エクスポート。失敗したマニフェストはダイアログで報告されるため、原因切り分けが容易。

### 使い方

1. プロジェクトウィンドウで右クリック → *Create → Quick Wrapper → Manifest* を作成。
2. Inspector で書き出したいアセットをドラッグ＆ドロップで追加。
3. 出力先パスを指定して **Export Package** をクリック。
4. 複数の Manifest を一括処理したい場合は *Quick Wrapper → Batcher* を作成し、Manifest を登録して **Export All**。

## Adding the listing to VCC

リスティング URL:

```
https://saturnianjp.github.io/QuickWrapper/index.json
```

ランディングページ（[https://saturnianjp.github.io/QuickWrapper/](https://saturnianjp.github.io/QuickWrapper/)）の **Add to VCC** ボタンから追加するか、上記 URL を VCC に直接貼り付けてください。

## License

[MIT](LICENSE)
