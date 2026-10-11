using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;

namespace Satania.QuickWrapper
{
    [CustomEditor(typeof(QuickWrapperManifest))]
    public class QuickWrapperManifestEditor : Editor
    {
        private QuickWrapperManifest manifest => target as QuickWrapperManifest;
        private SerializedProperty assetsToExport;
        private SerializedProperty includeDependecies, includeLibrary;
        private SerializedProperty packageFilepath;
        private SerializedProperty createDirectory;

        private static GUIStyle resolvedPathStyle;

        private static readonly GUIContent browseContent = new GUIContent("参照", "保存先を選択します。プロジェクト内を選んだ場合はプロジェクト相対パスで保存されます。");
        private static readonly GUIContent createDirectoryContent = new GUIContent("フォルダが無ければ作成", "エクスポート先のフォルダが存在しない場合、エクスポート時に自動で作成します。");

        private void OnEnable()
        {
            includeDependecies = serializedObject.FindProperty("includeDependecies");
            includeLibrary = serializedObject.FindProperty("includeLibrary");
            assetsToExport = serializedObject.FindProperty("assetsToExport");
            packageFilepath = serializedObject.FindProperty("packageFilepath");
            createDirectory = serializedObject.FindProperty("createDirectory");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            EditorGUILayout.PropertyField(assetsToExport);
            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("エクスポート設定 (基本的にオフ推奨)", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(includeDependecies);
            EditorGUILayout.PropertyField(includeLibrary);

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("エクスポート先", EditorStyles.boldLabel);

            using (new GUILayout.HorizontalScope())
            {
                EditorGUI.BeginChangeCheck();
                string path = EditorGUILayout.TextField(packageFilepath.stringValue);
                if (EditorGUI.EndChangeCheck())
                    packageFilepath.stringValue = path;

                if (GUILayout.Button(browseContent, GUILayout.Width(40)))
                {
                    EditorApplication.delayCall += BrowseFilepath;
                }
            }

            DrawResolvedPath();

            EditorGUILayout.PropertyField(createDirectory, createDirectoryContent);

            EditorGUILayout.Space(5);

            if (GUILayout.Button("Export Package", GUILayout.Height(40)))
            {
                serializedObject.ApplyModifiedProperties();
                manifest.Export();
            }

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>入力されたパスがどこに解決されるかと、フォルダの有無を表示する。</summary>
        private void DrawResolvedPath()
        {
            string raw = packageFilepath.stringValue;
            string resolved;

            try
            {
                resolved = QuickWrapperManifest.ResolveFilepath(raw);
            }
            catch (System.Exception ex)
            {
                EditorGUILayout.HelpBox($"パスが不正です: {ex.Message}", MessageType.Error);
                return;
            }

            if (string.IsNullOrEmpty(resolved))
            {
                EditorGUILayout.HelpBox("エクスポート先が未設定です。絶対パス、またはプロジェクトルート (Assets の親フォルダ) 基準の相対パスを指定してください。", MessageType.Warning);
                return;
            }

            if (resolvedPathStyle == null)
                resolvedPathStyle = new GUIStyle(EditorStyles.miniLabel) { wordWrap = true };

            string kind = QuickWrapperManifest.IsRelative(raw) ? "プロジェクト相対" : "絶対パス";
            EditorGUILayout.LabelField($"{kind} → {resolved}", resolvedPathStyle);

            string directory = Path.GetDirectoryName(resolved);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                if (createDirectory.boolValue)
                    EditorGUILayout.HelpBox("フォルダが存在しないため、エクスポート時に作成されます。", MessageType.Info);
                else
                    EditorGUILayout.HelpBox("エクスポート先のフォルダが存在しません。「フォルダが無ければ作成」をオンにするか、先にフォルダを作成してください。", MessageType.Warning);
            }
        }

        private void BrowseFilepath()
        {
            string directory = QuickWrapperManifest.ProjectRoot;
            string defaultName = manifest.name;

            try
            {
                string current = manifest.GetResolvedFilepath();
                if (!string.IsNullOrEmpty(current))
                {
                    string currentDirectory = Path.GetDirectoryName(current);
                    if (!string.IsNullOrEmpty(currentDirectory) && Directory.Exists(currentDirectory))
                        directory = currentDirectory;

                    string currentName = Path.GetFileNameWithoutExtension(current);
                    if (!string.IsNullOrEmpty(currentName))
                        defaultName = currentName;
                }
            }
            catch (System.Exception)
            {
                // 現在のパスが不正でも参照ダイアログは開けるようにする
            }

            string path = EditorUtility.SaveFilePanel("Select path for export", directory, defaultName, "unitypackage");
            if (string.IsNullOrEmpty(path))
                return;

            Undo.RecordObject(manifest, "Set Filepath");
            manifest.SetFilepath(QuickWrapperManifest.ToProjectRelativePath(path));
            EditorUtility.SetDirty(manifest);
        }
    }

    internal static class CreateWrapperManifest
    {
        [MenuItem("Assets/Create/Quick Wrapper/Manifest", priority = 0, secondaryPriority = 0)]
        private static void CreateNewWrapperBatch()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                Editor.CreateInstance<DoCreateQuickWrapperManifest>(),
                "New Manifest.asset",
                EditorGUIUtility.IconContent("d_ScriptableObject Icon").image as Texture2D,
                null);
        }

        private class DoCreateQuickWrapperManifest : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                QuickWrapperManifest manifest = Editor.CreateInstance<QuickWrapperManifest>();
                AssetDatabase.CreateAsset(manifest, pathName);
                Selection.activeObject = manifest;
            }
        }
    }
}
