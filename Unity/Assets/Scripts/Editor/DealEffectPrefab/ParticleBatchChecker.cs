/*
 * ==============================================================================
 * Filename: ParticleBatchChecker
 * Created:  2024 / 11 / 7
 * Author: HuaHua
 * Purpose: 检查粒子合批的工具
 * ==============================================================================
 **/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ParticleBatchChecker
{
    private class ParticleBatchInfo
    {
        public Material Material;
        public Texture MainTexture;
        public ParticleSystemRenderMode RenderMode;
    }

    private static readonly Dictionary<GameObject, int> _batchingGroups = new();
    private static Dictionary<Material, int> _materialGroups = new();

    /// <summary>
    /// 是否能合批
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static bool CanBatchTogether(ParticleBatchInfo a, ParticleBatchInfo b)
    {
        return a.Material == b.Material && // 材质一致
            a.MainTexture == b.MainTexture && // Texture Sheet 贴图一致
            a.RenderMode == b.RenderMode; // Render Mode 一致
    }

    /// <summary>
    /// 右键点击
    /// </summary>
    /// <param name="command"></param>
    [MenuItem("GameObject/---Check Particle Batching")]
    private static void CheckParticleBatching()
    {
        foreach (Object item in Selection.objects)
        {
            GameObject selectedObject = (GameObject)item;
            if (selectedObject == null)
            {
                Debug.LogWarning("请选择一个有效的对象进行批处理检查。");
                return;
            }

            _batchingGroups.Clear();
            _materialGroups.Clear();
            int batchGroupCounter = 1;
            int materialGroupCounter = 1;

            ParticleSystem[] particleSystems = selectedObject.GetComponentsInChildren<ParticleSystem>(true);
            List<ParticleBatchInfo> batchInfos = new List<ParticleBatchInfo>();

            foreach (ParticleSystem ps in particleSystems)
            {
                Renderer renderer = ps.GetComponent<Renderer>();
                if (renderer != null && renderer.sharedMaterial != null)
                {
                    Material mat = renderer.sharedMaterial;

                    Texture mainTexture = null;
                    var textureSheet = ps.textureSheetAnimation;
                    if (textureSheet.enabled && textureSheet.mode == ParticleSystemAnimationMode.Sprites)
                    {
                        if (textureSheet.GetSprite(0) != null)
                        {
                            mainTexture = textureSheet.GetSprite(0).texture;
                        }
                    }

                    ParticleSystemRenderer psRenderer = ps.GetComponent<ParticleSystemRenderer>();
                    ParticleBatchInfo info = new ParticleBatchInfo
                    {
                        Material = mat, MainTexture = mainTexture, RenderMode = psRenderer.renderMode // 获取 Render Mode
                    };

                    batchInfos.Add(info);

                    // 分配材质组编号
                    if (!_materialGroups.ContainsKey(mat))
                    {
                        _materialGroups[mat] = materialGroupCounter++;
                    }
                }
            }

            for (int i = 0; i < particleSystems.Length; i++)
            {
                bool isGrouped = false;
                for (int j = 0; j < i; j++)
                {
                    if (CanBatchTogether(batchInfos[i], batchInfos[j]))
                    {
                        _batchingGroups[particleSystems[i].gameObject] = _batchingGroups[particleSystems[j].gameObject];
                        isGrouped = true;
                        break;
                    }
                }

                if (!isGrouped)
                {
                    _batchingGroups[particleSystems[i].gameObject] = batchGroupCounter++;
                }
            }

            Debug.Log("Particle batch check complete. Check Hierarchy for labels.");
            EditorApplication.RepaintHierarchyWindow();
        }
    }

    [InitializeOnLoadMethod]
    private static void OnHierarchyGUI()
    {
        EditorApplication.hierarchyWindowItemOnGUI += (instanceID, selectionRect) =>
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj != null && _batchingGroups.ContainsKey(obj))
            {
                // 获取批处理组编号
                if (_batchingGroups.ContainsKey(obj))
                {
                    int batchGroupID = _batchingGroups[obj];
                    // 使用蓝色显示批处理组编号
                    GUIStyle batchGroupStyle = new GUIStyle(EditorStyles.label) { normal = { textColor = Color.white } };
                    Rect batchRect = new Rect(selectionRect) { x = selectionRect.xMax - 50, width = 25 };
                    EditorGUI.LabelField(batchRect, $"[{batchGroupID}]", batchGroupStyle);
                }

                // 获取材质组编号
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null && renderer.sharedMaterial != null && _materialGroups.ContainsKey(renderer.sharedMaterial))
                {
                    int materialGroupID = _materialGroups[renderer.sharedMaterial];
                    // 使用绿色显示材质组编号
                    GUIStyle materialGroupStyle = new GUIStyle(EditorStyles.label) { normal = { textColor = Color.green } };
                    Rect materialRect = new Rect(selectionRect) { x = selectionRect.xMax - 25, width = 25 };
                    EditorGUI.LabelField(materialRect, $"[{materialGroupID}]", materialGroupStyle);
                }
            }
        };
    }
}