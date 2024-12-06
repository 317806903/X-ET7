using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // 需要引用UnityEditor命名空间

public class DealSkinnedMeshRendererEditor
{
    [MenuItem("Tools/DealEffectPrefab/DealSkinnedMeshRendererMotionVectors")]
    private static void DealSkinnedMeshRendererMotionVectors()
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
            SkinnedMeshRenderer[] skinnedMeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
            {
                if (skinnedMeshRenderer.skinnedMotionVectors != false)
                {
                    skinnedMeshRenderer.skinnedMotionVectors = false;
                    isNeedSave = true;
                    Debug.LogError($"--[{path}][{skinnedMeshRenderer.gameObject.name}]-- skinnedMotionVectors");
                }

                if (skinnedMeshRenderer.motionVectorGenerationMode != MotionVectorGenerationMode.ForceNoMotion)
                {
                    skinnedMeshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
                    isNeedSave = true;
                    Debug.LogError($"--[{path}][{skinnedMeshRenderer.gameObject.name}]-- motionVectorGenerationMode");
                }

                if (skinnedMeshRenderer.updateWhenOffscreen != false)
                {
                    skinnedMeshRenderer.updateWhenOffscreen = false;
                    isNeedSave = true;
                    Debug.LogError($"--[{path}][{skinnedMeshRenderer.gameObject.name}]-- updateWhenOffscreen");
                }

                if (isNeedSave)
                {
                    EditorUtility.SetDirty(skinnedMeshRenderer);
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