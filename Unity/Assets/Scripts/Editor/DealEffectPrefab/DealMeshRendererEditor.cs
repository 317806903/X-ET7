using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // 需要引用UnityEditor命名空间

public class DealMeshRendererEditor
{
    [MenuItem("Tools/DealEffectPrefab/DisableMeshRendererMotionVectors")]
    private static void DisableMeshRendererMotionVectors()
    {
        List<string> resourcePaths = new List<string>();
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!string.IsNullOrEmpty(path) && !path.EndsWith(".meta"))
            {
                resourcePaths.Add(path);
            }
        }

        foreach (string path in resourcePaths)
        {
            bool isNeedSave = false;
            var gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                if (meshRenderer.motionVectorGenerationMode != MotionVectorGenerationMode.ForceNoMotion)
                {
                    meshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
                    isNeedSave = true;
                    Debug.LogError($"--[{path}][{meshRenderer.gameObject.name}]-- motionVectorGenerationMode");
                }

                if (isNeedSave)
                {
                    EditorUtility.SetDirty(meshRenderer);
                }
            }
            if (isNeedSave)
            {
                EditorUtility.SetDirty(gameObject);
                // 保存Prefab的更改
                AssetDatabase.SaveAssets();
            }
        }
        // 可选：刷新AssetDatabase，以便更改立即可见
        AssetDatabase.Refresh();

    }
}