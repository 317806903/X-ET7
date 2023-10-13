using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    public static class TowerStarBarComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<TowerStarBarComponent>
        {
            protected override void Awake(TowerStarBarComponent self)
            {
                GameObjectComponent gameObjectComponent = self.GetUnit().GetComponent<GameObjectComponent>();
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_TowerStarBar_1");
                GameObject towerStarBarGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,5);
                towerStarBarGo.transform.SetParent(gameObjectComponent.gameObject.transform);
                float height = self.GetUnit().model.BodyHeight + 1f;
                towerStarBarGo.transform.localPosition = new float3(0, height, 0);
                towerStarBarGo.transform.localScale = Vector3.one;

                self.transRoot = towerStarBarGo.transform;
                self.transStar1 = self.transRoot.Find("Bar/Star1/Star");
                self.transStar2 = self.transRoot.Find("Bar/Star2/Star");
                self.transStar3 = self.transRoot.Find("Bar/Star3/Star");
                self.transStarGrey1 = self.transRoot.Find("Bar/Star1/StarGrey");
                self.transStarGrey2 = self.transRoot.Find("Bar/Star2/StarGrey");
                self.transStarGrey3 = self.transRoot.Find("Bar/Star3/StarGrey");

                self.transRoot.gameObject.SetActive(false);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<TowerStarBarComponent>
        {
            protected override void Destroy(TowerStarBarComponent self)
            {
                if (self.transRoot != null)
                {
                    GameObjectPoolHelper.ReturnTransformToPool(self.transRoot);
                    self.transRoot = null;
                }
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<TowerStarBarComponent>
        {
            protected override void Update(TowerStarBarComponent self)
            {
                self.Update();
            }
        }

        public static Unit GetUnit(this TowerStarBarComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void Init(this TowerStarBarComponent self, TowerComponent towerComponent)
        {
            self.ShowStar(towerComponent.model.Level[0]);
        }

        public static void ShowStar(this TowerStarBarComponent self, int starCount)
        {
            self.transRoot.gameObject.SetActive(true);
            self.transStar1.gameObject.SetActive(starCount >= 1);
            self.transStar2.gameObject.SetActive(starCount >= 2);
            self.transStar3.gameObject.SetActive(starCount >= 3);
            self.transStarGrey1.gameObject.SetActive(!(starCount >= 1));
            self.transStarGrey2.gameObject.SetActive(!(starCount >= 2));
            self.transStarGrey3.gameObject.SetActive(!(starCount >= 3));

            self.Update();
        }

        public static void Update(this TowerStarBarComponent self)
        {
            if (self.transRoot == null)
            {
                return;
            }
            Transform transform = self.transRoot.transform;
            Camera mainCamera = CameraHelper.GetMainCamera(self.DomainScene());
            if (mainCamera == null)
            {
                return;
            }
            Vector3 direction = mainCamera.transform.forward;
            transform.forward = -direction;
        }
    }
}