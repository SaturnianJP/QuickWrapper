using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System.IO;

namespace Satania.QuickWrapper
{
    //[CreateAssetMenu(menuName = "QuickWrapper/Manifest", fileName = "New Manifest", order = 1000)]
    public class QuickWrapperManifest : ScriptableObject
    {
        public bool includeDependecies;
        public bool includeLibrary;
        public List<Object> assetsToExport;

        /// <summary>
        /// エクスポート先の .unitypackage パス。
        /// 絶対パスのほか、プロジェクトルート (Assets の親フォルダ) 基準の相対パスも指定できる。
        /// </summary>
        public string packageFilepath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "New Package.unitypackage");

        /// <summary>エクスポート先のフォルダが存在しない場合に作成する。</summary>
        public bool createDirectory = true;

        /// <summary>プロジェクトルート (Assets の親フォルダ) の絶対パス。</summary>
        public static string ProjectRoot => Path.GetFullPath(Path.Combine(Application.dataPath, ".."));

        public void SetFilepath(string path) => packageFilepath = path;

        /// <summary>packageFilepath がプロジェクトルート基準の相対パスかどうか。</summary>
        public bool IsRelativePath => IsRelative(packageFilepath);

        /// <summary>packageFilepath を絶対パスに解決する。未設定なら null。</summary>
        public string GetResolvedFilepath() => ResolveFilepath(packageFilepath);

        /// <summary>パスが相対パス (ドライブやルートから始まらない) かどうか。</summary>
        public static bool IsRelative(string path)
            => !string.IsNullOrWhiteSpace(path) && !Path.IsPathRooted(path.Trim());

        /// <summary>
        /// 絶対パス・相対パスのどちらでも受け取り、絶対パスに解決する。
        /// 相対パスはプロジェクトルート基準で解決する。空なら null。
        /// </summary>
        public static string ResolveFilepath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            path = path.Trim();
            if (!Path.IsPathRooted(path))
                path = Path.Combine(ProjectRoot, path);

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// 絶対パスがプロジェクト内を指していればプロジェクトルート基準の相対パス (区切りは '/') に変換する。
        /// プロジェクト外ならそのまま返す。
        /// </summary>
        public static string ToProjectRelativePath(string absolutePath)
        {
            if (string.IsNullOrWhiteSpace(absolutePath))
                return absolutePath;

            string full = Path.GetFullPath(absolutePath);
            string root = ProjectRoot.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;

            // Windows / macOS のファイルシステムは大文字小文字を区別しない
            var comparison = Application.platform == RuntimePlatform.LinuxEditor
                ? System.StringComparison.Ordinal
                : System.StringComparison.OrdinalIgnoreCase;

            if (!full.StartsWith(root, comparison))
                return absolutePath;

            return full.Substring(root.Length).Replace('\\', '/');
        }

        /// <summary>エクスポートし、完了後にファイルブラウザで表示する。</summary>
        public void Export() => Export(true);

        /// <param name="reveal">true なら非同期にエクスポートし、完了後にファイルブラウザで表示する (ExportPackageOptions.Interactive)。</param>
        public void Export(bool reveal)
        {
            if (assetsToExport == null || assetsToExport.Count < 1)
                return;

            string exportPath = GetResolvedFilepath();
            if (string.IsNullOrEmpty(exportPath))
                throw new System.Exception($"{name}内のファイルパスが不正です。");

            string directory = Path.GetDirectoryName(exportPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                if (!createDirectory)
                    throw new DirectoryNotFoundException($"{name}: エクスポート先のフォルダが存在しません。 {directory}");

                Directory.CreateDirectory(directory);
            }

            string[] assetPaths = assetsToExport
                .Where(x => x != null)
                .Where(x => AssetDatabase.IsMainAsset(x))
                .Select(x => AssetDatabase.GetAssetPath(x))
                .Distinct()
                .ToArray();

            ExportPackageOptions options = ExportPackageOptions.Recurse;

            if (reveal)
                options |= ExportPackageOptions.Interactive;

            if (includeDependecies)
                options |= ExportPackageOptions.IncludeDependencies;

            if (includeLibrary)
                options |= ExportPackageOptions.IncludeLibraryAssets;

#if DEBUG_SATANIA
            Debug.Log(options);
#endif

            AssetDatabase.ExportPackage(assetPaths, exportPath, options);
        }
    }
}
