using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // 需要引用UnityEditor命名空间

public partial class DealEffectPrefab
{
    [MenuItem("Tools/DealEffectPrefab/RemoveCollider")]
    private static void RemoveCollider()
    {
        List<string> resourcePaths = new List<string>();
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!string.IsNullOrEmpty(path) && !path.EndsWith(".meta"))
            {
                if (path.EndsWith("/HomeShow.prefab")
                    || path.EndsWith("/MonsterCallShow.prefab")
                    || path.EndsWith("/TowerShow.prefab")
                    || path.EndsWith("/PlayerUnitShow.prefab"))
                {
                    continue;
                }
                resourcePaths.Add(path);
            }
        }
        // 打印或处理找到的资源路径
        foreach (string path in resourcePaths)
        {
            bool isNeedSave = false;
            var gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            Collider[] colliders = gameObject.GetComponentsInChildren<Collider>(false);

            foreach (Collider collider in colliders)
            {
                // Object.DestroyImmediate(collider);
                // isNeedSave = true;
                // Debug.LogError($"--[{path}][{collider.gameObject.name}]-- DestroyImmediate Collider");
                collider.enabled = false;
                isNeedSave = true;
                Debug.LogError($"--[{path}][{collider.gameObject.name}]-- collider.enabled = false");
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