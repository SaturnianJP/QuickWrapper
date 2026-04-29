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
        public string packageFilepath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "New Package.unitypackage");

        public void SetFilepath(string path) => packageFilepath = path;

        public void Export()
        {
            if (assetsToExport.Count < 1)
                return;

            if (string.IsNullOrEmpty(packageFilepath))
                throw new System.Exception($"{name}内のファイルパスが不正です。");

            string[] assetPaths = assetsToExport
                .Where(x => x != null)
                .Where(x => AssetDatabase.IsMainAsset(x))
                .Select(x => AssetDatabase.GetAssetPath(x))
                .Distinct()
                .ToArray();

            ExportPackageOptions options = ExportPackageOptions.Interactive | ExportPackageOptions.Recurse;

            if (includeDependecies)
                options |= ExportPackageOptions.IncludeDependencies;

            if (includeLibrary)
                options |= ExportPackageOptions.IncludeLibraryAssets;

#if DEBUG_SATANIA
            Debug.Log(options);
#endif

            AssetDatabase.ExportPackage(assetPaths, packageFilepath, options);
        }
    }
}