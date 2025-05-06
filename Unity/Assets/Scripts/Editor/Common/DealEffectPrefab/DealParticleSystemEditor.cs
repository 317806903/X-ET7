using System.Collections.Generic;
using ET;
using UnityEngine;
using UnityEditor; // 需要引用UnityEditor命名空间

public partial class DealEffectPrefab
{
    [MenuItem("Tools/DealEffectPrefab/DealParticleSystemEditor")]
    private static void DealParticleSystemEditor()
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
            ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem ps in particleSystems)
            {
                if (ps.main.maxParticles > 100)
                {
                    ParticleSystem.MainModule main = ps.main;
                    main.maxParticles = 100;
                    isNeedSave = true;
                    Debug.LogError($"--[{path}][{ps.gameObject.name}]-- maxParticles > 100");
                }

                // 关闭碰撞检测
                ParticleSystem.CollisionModule collisionModule = ps.collision; // 获取碰撞模块
                if (collisionModule.enabled)
                {
                    collisionModule.enabled = false;
                    isNeedSave = true;
                    Debug.LogError($"--[{path}][{ps.gameObject.name}]-- collisionModule.enabled");
                }

                ParticleSystem.TriggerModule trigger = ps.trigger;
                if (trigger.enabled)
                {
                    trigger.enabled = false;
                    isNeedSave = true;
                    Debug.LogError($"--[{path}][{ps.gameObject.name}]-- trigger.enabled");
                }

                ParticleSystem.SubEmittersModule subEmitters = ps.subEmitters;
                if (subEmitters.enabled)
                {
                    subEmitters.enabled = false;
                    isNeedSave = true;
                    Debug.LogError($"--[{path}][{ps.gameObject.name}]-- subEmitters.enabled");
                }

                ParticleSystem.LightsModule lights = ps.lights;
                if (lights.enabled)
                {
                    lights.enabled = false;
                    isNeedSave = true;
                    Debug.LogError($"--[{path}][{ps.gameObject.name}]-- lights.enabled");
                }

                if (ps.main.scalingMode == ParticleSystemScalingMode.Local)
                {
                    ParticleSystem.MainModule main = ps.main;
                    main.scalingMode = ParticleSystemScalingMode.Hierarchy;
                    isNeedSave = true;
                    Debug.LogError($"--[{path}][{ps.gameObject.name}]-- scalingMode == ParticleSystemScalingMode.Local");
                }

                if (ps.main.gravityModifierMultiplier != 0)
                {
                    if (ps.gameObject.GetComponent<ParticleSystemScaler>() == null)
                    {
                        ps.gameObject.AddComponent<ParticleSystemScaler>();
                        isNeedSave = true;
                        Debug.LogError($"--[{path}][{ps.gameObject.name}]-- gravityModifierMultiplier != 0");
                    }
                }

                if (isNeedSave)
                {
                    EditorUtility.SetDirty(ps);
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