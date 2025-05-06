using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;

public partial class DealEffectPrefab
{
    [MenuItem("Tools/DealEffectPrefab/Remove Missing Scripts from Prefabs")]
    private static void RemoveMissingScripts()
    {
        string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets" });
        foreach (string guid in prefabGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            // if (path.Contains("BulletAoe1") == false)
            // {
            //     int i = 9;
            //     continue;
            // }
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab != null)
            {
                bool isNeedSave = false;
                Component[] components = prefab.GetComponentsInChildren<Component>();
                foreach (Component component in components)
                {
                    if (component == null) // 检查组件是否为null
                    {
                        isNeedSave = true;
                        Debug.LogError($"Removed missing script component from prefab: {path}");
                        break;
                    }
                }

                if (isNeedSave)
                {
                    SearchChild(prefab);
                    AssetDatabase.SaveAssets();
                }
            }
        }
        AssetDatabase.Refresh();
    }

    private static void SearchChild(GameObject gameObject)
    {
        GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
        if (gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                SearchChild(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}