using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // 需要引用UnityEditor命名空间

public partial class DealEffectPrefab
{
    [MenuItem("Tools/DealEffectPrefab/DealAudioEditor")]
    private static void DealAudioEditor()
    {
        List<string> resourcePaths = new List<string>();
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!string.IsNullOrEmpty(path) && !path.EndsWith(".meta"))
            {
                if (path.EndsWith("/AudioSource.prefab"))
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
            AudioSource[] Audios = gameObject.GetComponentsInChildren<AudioSource>(false);

            foreach (AudioSource audioSource in Audios)
            {
                // Object.DestroyImmediate(audioSource);
                // isNeedSave = true;
                // Debug.LogError($"--[{path}][{audioSource.gameObject.name}]-- DestroyImmediate AudioSource");
                audioSource.enabled = false;
                isNeedSave = true;
                Debug.LogError($"--[{path}][{audioSource.gameObject.name}]-- audioSource.enabled = false");
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