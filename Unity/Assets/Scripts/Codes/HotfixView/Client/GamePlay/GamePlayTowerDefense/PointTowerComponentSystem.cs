using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    public static class PointTowerComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<PointTowerComponent>
        {
            protected override void Awake(PointTowerComponent self)
            {
                GameObjectShowComponent gameObjectShowComponent = self.GetUnit().GetComponent<GameObjectShowComponent>();
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_PointTower_1");
                GameObject PointTowerPrefab = ResComponent.Instance.LoadAsset<GameObject>(resEffectCfg.ResName);
                PointTowerPrefab = GameObject.Instantiate(PointTowerPrefab);
                PointTowerPrefab.transform.SetParent(gameObjectShowComponent.gameObject.transform);
                PointTowerPrefab.transform.localPosition = new float3(0, 0, 0);
                PointTowerPrefab.transform.localScale = Vector3.one;

                self.transRoot = PointTowerPrefab.transform;

                self.transRoot.gameObject.SetActive(true);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<PointTowerComponent>
        {
            protected override void Destroy(PointTowerComponent self)
            {
                if (self.transRoot != null)
                {
                    GameObject.Destroy(self.transRoot.gameObject);
                    self.transRoot = null;
                }
            }
        }

        public static Unit GetUnit(this PointTowerComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void Init(this PointTowerComponent self)
        {
        }

    }
}