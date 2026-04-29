using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
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

        private void OnEnable()
        {
            includeDependecies = serializedObject.FindProperty("includeDependecies");
            includeLibrary = serializedObject.FindProperty("includeLibrary");
            assetsToExport = serializedObject.FindProperty("assetsToExport");
            packageFilepath = serializedObject.FindProperty("packageFilepath");
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
                packageFilepath.stringValue = EditorGUILayout.TextField(manifest.packageFilepath);

                if (GUILayout.Button("参照", GUILayout.Width(40)))
                {
                    EditorApplication.delayCall += () =>
                    {
                        string path = EditorUtility.SaveFilePanel("Select path for export", manifest.packageFilepath, manifest.name, "unitypackage");
                        if (!string.IsNullOrEmpty(path))
                        {
                            Undo.RecordObject(manifest, "Set Filepath");
                            manifest.SetFilepath(path);
                        }
                    };
                }
            }

            if (GUILayout.Button("Export Package", GUILayout.Height(40)))
            {
                manifest.Export();
            }


            serializedObject.ApplyModifiedProperties();
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