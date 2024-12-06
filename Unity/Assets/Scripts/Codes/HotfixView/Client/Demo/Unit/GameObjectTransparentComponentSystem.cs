using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class GameObjectTransparentComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<GameObjectTransparentComponent>
        {
            protected override void Awake(GameObjectTransparentComponent self)
            {
                self.SetMaterialRecord();
                self.ChgTransparent(true);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<GameObjectTransparentComponent>
        {
            protected override void Destroy(GameObjectTransparentComponent self)
            {
                self.ChgTransparent(false);
            }
        }

        public static GameObject GetGo(this GameObjectTransparentComponent self)
        {
            GameObjectShowComponent gameObjectShowComponent = self.GetParent<GameObjectShowComponent>();
            return gameObjectShowComponent?.GetGo();
        }

        public static void SetMaterialRecord(this GameObjectTransparentComponent self)
        {
            GameObject go = self.GetGo();
            if (go == null)
            {
                return;
            }

            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if (child.gameObject.GetComponent<PoolObject>() != null)
                {
                    continue;
                }
                Renderer[] renderers = child.gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    self.MaterialRecordDic[renderer] = renderer.materials;
                }
            }

        }

        public static void ChgTransparent(this GameObjectTransparentComponent self, bool isTransparent)
        {
            if (isTransparent)
            {
                self.SetTransparent(true, 0.6f);
            }
            else
            {
                self.SetTransparent(false, 1f);
            }
        }

        public static void SetTransparent(this GameObjectTransparentComponent self, bool transparent, float alpha)
        {
            if (transparent)
            {
                foreach (var item in self.MaterialRecordDic)
                {
                    Renderer renderer = item.Key;
                    if (renderer == null)
                    {
                        continue;
                    }
                    Material[] mOriMat = item.Value;
                    Material[] mats = renderer.materials;
                    Material newMat;
                    for (int i = 0; i < mats.Length; i++)
                    {
                        if (mOriMat[i] == null)
                        {
                            mats[i] = null;
                            continue;
                        }

                        newMat = UnityEngine.Object.Instantiate(mOriMat[i]);
                        newMat.SetFloat("_Surface", 1.0f);
                        newMat.SetFloat("_Blend", 0f);
                        newMat.SetFloat("_BlendModePreserveSpecular", 0f);
                        newMat.SetFloat("_ZWrite", 0.0f);
                        newMat.SetFloat("_SrcBlend", 5.0f);
                        newMat.SetFloat("_DstBlend", 10.0f);
                        newMat.SetFloat("_SrcBlendAlpha", 5.0f);
                        newMat.SetFloat("_DstBlendAlpha", 10.0f);
                        if (newMat.HasProperty("_BaseColor"))
                        {
                            newMat.SetColor("_BaseColor", new Color(1f, 1f, 1f, alpha));
                        }
                        else
                        {
                            newMat.SetColor("_Color", new Color(1f, 1f, 1f, alpha));
                        }

                        newMat.SetFloat("_QueueOffset", 1000);
                        mats[i] = newMat;
                    }

                    renderer.materials = mats;
                }
            }
            else
            {
                foreach (var item in self.MaterialRecordDic)
                {
                    Renderer renderer = item.Key;
                    if (renderer == null)
                    {
                        continue;
                    }
                    Material[] mOriMat = item.Value;
                    Material[] mats = renderer.materials;
                    for (int i = 0; i < mats.Length; i++)
                    {
                        if (mats[i] != null)
                        {
                            UnityEngine.Object.Destroy(mats[i]);
                        }
                    }

                    renderer.materials = mOriMat;
                }
            }
        }
    }
}