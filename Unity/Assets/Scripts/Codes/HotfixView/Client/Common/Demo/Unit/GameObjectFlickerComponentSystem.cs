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
                self.StopColorFlicker();
                self.MaterialRecordList.Clear();
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
            return gameObjectShowComponent?.GetUnitResGo();
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
                    foreach (Material material in renderer.materials)
                    {
                        if (material == null)
                        {
                            continue;
                        }

                        bool hasPropertyEmission = material.HasProperty("_EmissionColor");
                        if (hasPropertyEmission)
                        {
                            self.MaterialRecordList.Add(material);
                        }
                    }
                }
            }
        }

        public static void ResetTime(this GameObjectFlickerComponent self,
        long flickerEndTime, float flickerFrequency, Color startColor, Color endColor)
        {
            if (self.flickerEndTime >= flickerEndTime)
            {
                return;
            }
            if (self.flickerEndTime < flickerEndTime)
            {
                self.flickerStartTime = TimeHelper.ServerNow();
            }
            self.flickerEndTime = flickerEndTime;
            self.flickerFrequency = flickerFrequency;
            self.startColor = startColor;
            self.endColor = endColor;
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
                    self.StopColorFlicker();
                }
                return;
            }

            if (self.isFlickering == false)
            {
                self.isFlickering = true;
                self.flickerStartTime = TimeHelper.ServerNow();
            }
            self.StartColorFlicker();
        }

        public static Color GetCurColor(this GameObjectFlickerComponent self)
        {
            float flickerDuration = 1 / self.flickerFrequency;
            float curTime = (TimeHelper.ServerNow() - self.flickerStartTime) * 0.001f;

            float progress = (curTime % flickerDuration) / (float)flickerDuration;
            return Color.Lerp(self.startColor, self.endColor, progress);
        }

        public static void StartColorFlicker(this GameObjectFlickerComponent self)
        {
            self._ChgColor(true, self.GetCurColor());
        }

        public static void StopColorFlicker(this GameObjectFlickerComponent self)
        {
            Color colorNew = Color.black;
            self._ChgColor(false, colorNew);
        }

        public static void _ChgColor(this GameObjectFlickerComponent self, bool enableEmission, Color emissionColor)
        {
            foreach (Material material in self.MaterialRecordList)
            {
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
                    material.SetColor("_EmissionColor", emissionColor);
                }
            }
        }
    }
}