using System.Collections.Generic;
using ET;
using UnityEngine;
using UnityEditor; // 需要引用UnityEditor命名空间

public partial class DealEffectPrefab
{
    [MenuItem("Tools/DealEffectPrefab/DealLightEditor")]
    private static void DealLightEditor()
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
            Light[] lights = gameObject.GetComponentsInChildren<Light>();

            foreach (Light light in lights)
            {
                if (light.type == LightType.Point)
                {
                    if (light.gameObject.GetComponent<LightScaler>() == null)
                    {
                        light.gameObject.AddComponent<LightScaler>();
                        isNeedSave = true;
                        Debug.LogError($"--[{path}][{light.gameObject.name}]-- light.type == LightType.Point");
                    }
                }

                if (isNeedSave)
                {
                    EditorUtility.SetDirty(light);
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