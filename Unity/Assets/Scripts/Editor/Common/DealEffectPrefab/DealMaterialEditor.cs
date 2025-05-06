using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public partial class DealEffectPrefab
{
    [MenuItem("Tools/DealEffectPrefab/DealMaterialEditor")]
    static void CleanMaterialUnusedTextures()
    {
        List<string> resourcePaths = new List<string>();
        string[] guids = AssetDatabase.FindAssets("t:Material", new string[] { "Assets" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!string.IsNullOrEmpty(path) && !path.EndsWith(".meta"))
            {
                // if (path.Contains("Flare_GlowdotPickbook") == false)
                // {
                //     continue;
                // }
                resourcePaths.Add(path);
            }
        }

        foreach (string path in resourcePaths)
        {
            bool isNeedSave = false;
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);
            Shader shader = material.shader;
            if (shader.name.Contains("Error"))
                continue;
            HashSet<string> temp = GetTextureProperyOfShader(shader);
            Dictionary<string, UnityEngine.Object> result = GetTextureProperyOfMaterial(material).Where(s => !temp.Contains(s.Key)).ToDictionary(s=>s.Key,s=>s.Value);
            foreach (var item in result)
            {
                material.SetTexture(item.Key, null);
                isNeedSave = true;
                Debug.LogError($"--[{path}]material.SetTexture({item.Key}, null);[{item.Value.name}])");
            }

			if (isNeedSave)
			{
                EditorUtility.SetDirty(material);
                // 保存Prefab的更改
                AssetDatabase.SaveAssets();
			}
        }
        // 可选：刷新AssetDatabase，以便更改立即可见
        AssetDatabase.Refresh();

    }

    ///***********************************核心**********************************
    /// <summary>
    /// 获取一个材质的贴图属性 贴图不为空的属性
    /// </summary>
    /// <param name="m"></param>
    private static Dictionary<string, UnityEngine.Object> GetTextureProperyOfMaterial(Material m)
    {
        Dictionary<string, UnityEngine.Object> dic = new Dictionary<string, UnityEngine.Object>();
        SerializedObject so = new SerializedObject(m);
        var iter = so.GetIterator();
        while (iter.NextVisible(true))
        {
            if (iter.propertyPath.Contains("m_TexEnvs"))
            {
                for (int i = 0; i < iter.FindPropertyRelative("Array").FindPropertyRelative("size").intValue; i++)
                {
                    UnityEngine.Object o = iter.FindPropertyRelative("Array").FindPropertyRelative("data[" + i + "]").FindPropertyRelative("second").FindPropertyRelative("m_Texture").objectReferenceValue;
                    if (o != null)
                    {
                        string key = iter.FindPropertyRelative("Array").FindPropertyRelative("data[" + i + "]").FindPropertyRelative("first")
                            .stringValue;
                        dic.Add(key, o);
                    }
                }
                break;
            }
        }
        return dic;
    }
    /// <summary>
    /// 获取一个shader的贴图属性
    /// </summary>
    /// <param name="s"></param>
    /// <returns>结果</returns>
    private static HashSet<string> GetTextureProperyOfShader(Shader s)
    {
        HashSet<string> t = new();
        for (int i = 0; i < ShaderUtil.GetPropertyCount(s); i++)
        {
            if (ShaderUtil.GetPropertyType(s,i) == ShaderUtil.ShaderPropertyType.TexEnv)
            {
                t.Add(ShaderUtil.GetPropertyName(s, i));
            }
        }
        return t;
    }
}