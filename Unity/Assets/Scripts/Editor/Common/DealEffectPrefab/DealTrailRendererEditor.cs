using System.Collections.Generic;
using ET;
using UnityEngine;
using UnityEditor; // 需要引用UnityEditor命名空间

public partial class DealEffectPrefab
{
    [MenuItem("Tools/DealEffectPrefab/DealTrailRendererEditor")]
    private static void DealTrailRendererEditor()
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
        // 打印或处理找到的资源路径
        foreach (string path in resourcePaths)
        {
            bool isNeedSave = false;
            var gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            TrailRenderer[] TrailRenderers = gameObject.GetComponentsInChildren<TrailRenderer>();

            foreach (TrailRenderer trailRenderer in TrailRenderers)
            {
                if (trailRenderer.widthMultiplier != 0)
                {
                    if (trailRenderer.gameObject.GetComponent<TrailRendererScaler>() == null)
                    {
                        trailRenderer.gameObject.AddComponent<TrailRendererScaler>();
                        isNeedSave = true;
                        Debug.LogError($"--[{path}][{trailRenderer.gameObject.name}]-- widthMultiplier != 0");
                    }
                }

                if (isNeedSave)
                {
                    EditorUtility.SetDirty(trailRenderer);
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