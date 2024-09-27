using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class GameObjectFlickerComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<GameObjectFlickerComponent>
        {
            protected override void Awake(GameObjectFlickerComponent self)
            {
                self.SetMaterialRecord();
                self.isFlickering = false;
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<GameObjectFlickerComponent>
        {
            protected override void Destroy(GameObjectFlickerComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<GameObjectFlickerComponent>
        {
            protected override void Update(GameObjectFlickerComponent self)
            {
                if (self.GetGo() == null)
                {
                    return;
                }

                self.Update();
            }
        }

        public static GameObject GetGo(this GameObjectFlickerComponent self)
        {
            GameObjectShowComponent gameObjectShowComponent = self.GetParent<GameObjectShowComponent>();
            return gameObjectShowComponent?.GetGo();
        }

        public static void SetMaterialRecord(this GameObjectFlickerComponent self)
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

        public static void ResetTime(this GameObjectFlickerComponent self,
        long flickerEndTime)
        {
            self.flickerEndTime = flickerEndTime;
        }

        public static void Update(this GameObjectFlickerComponent self)
        {
            self.UpdateFlicker();
        }

        public static void UpdateFlicker(this GameObjectFlickerComponent self)
        {
            if (self.flickerEndTime < TimeHelper.ServerNow())
            {
                if (self.isFlickering)
                {
                    self.isFlickering = false;
                    self.ChgColor(false);
                }
                return;
            }
            self.isFlickering = true;
            self.ChgColor(true);
        }

        public static void ChgColor(this GameObjectFlickerComponent self, bool isFlicker)
        {
            if (isFlicker)
            {
                Color colorNew = new Color(0.6f, 0.6f, 0.6f);
                self._ChgColor(true, colorNew);
            }
            else
            {
                Color colorNew = Color.black;
                self._ChgColor(false, colorNew);
            }
        }

        public static void _ChgColor(this GameObjectFlickerComponent self, bool enableEmission, Color emissionColor)
        {
            foreach (var item in self.MaterialRecordDic)
            {
                Renderer renderer = item.Key;
                if (renderer == null)
                {
                    continue;
                }
                Material[] mats = renderer.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    Material material = mats[i];
                    if (material == null)
                    {
                        continue;
                    }
                    if (enableEmission)
                    {
                        material.EnableKeyword("_EMISSION");
                        material.SetColor("_EmissionColor", emissionColor);
                    }
                    else
                    {
                        material.DisableKeyword("_EMISSION");
                    }
                }
            }
        }
    }
}