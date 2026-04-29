using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditor.ProjectWindowCallback;

namespace Satania.QuickWrapper
{
    [CustomEditor(typeof(QuickWrapperBatch))]
    public class QuickWrapperBatchEditor : Editor
    {
        private QuickWrapperBatch batch => target as QuickWrapperBatch;
        private SerializedProperty manifests;

        private void OnEnable()
        {
            manifests = serializedObject.FindProperty("manifests");
        }

        public void Export()
        {
            string successes = "";
            string errors = "";

            //nullと被っている物を排除
            var _manifests = batch.manifests
                .Where(x => x != null)
                .Distinct();

            foreach (var manifest in _manifests)
            {
                try
                {
                    if (manifest != null)
                        manifest.Export();
                }
                catch (System.Exception ex)
                {
                    //例外が発生した場合は名前をメモしてログ
                    Debug.LogError(ex.ToString());
                    if (!string.IsNullOrEmpty(errors))
                        errors += '\n';

                    errors += manifest.name;

                    continue;
                }


                if (!string.IsNullOrEmpty(successes))
                    successes += '\n';

                //成功したオブジェクト名をメモ
                successes += manifest.name;
            }

            if (!string.IsNullOrEmpty(errors))
            {
                EditorUtility.DisplayDialog(typeof(QuickWrapperBatch).Name,
                    "成功しました。" + '\n' + successes
                    + '\n'
                    + "以下のマニフェストのエクスポートに失敗しました。" + '\n' + errors,
                    "OK");
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(manifests);

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space(5);

            if (GUILayout.Button("Export All"))
            {
                Export();
            }
        }
    }

    internal static class CreateWrapperBatch
    {
        [MenuItem("Assets/Create/Quick Wrapper/Batcher", priority = 0, secondaryPriority = 10)]
        private static void CreateNewWrapperBatch()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                Editor.CreateInstance<DoCreateQuickWrapperBatch>(),
                "New Batch.asset",
                EditorGUIUtility.IconContent("d_ScriptableObject Icon").image as Texture2D,
                null);
        }

        private class DoCreateQuickWrapperBatch : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                QuickWrapperBatch batch = Editor.CreateInstance<QuickWrapperBatch>();
                AssetDatabase.CreateAsset(batch, pathName);
                Selection.activeObject = batch;
            }
        }
    }
}