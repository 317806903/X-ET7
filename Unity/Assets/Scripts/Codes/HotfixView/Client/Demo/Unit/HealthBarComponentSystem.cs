using System;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class HealthBarComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HealthBarComponent>
        {
            protected override void Awake(HealthBarComponent self)
            {
                string resName = "";
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
                if (gamePlayTowerDefenseComponent != null)
                {
                    PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                    if (putHomeComponent != null && putHomeComponent.unitId == self.GetUnit().Id)
                    {
                        resName = "ResEffect_MainTowerBar";
                    }
                    else if (self.GetComponent<TowerComponent>() != null)
                    {
                        resName = "ResEffect_MainTowerBar";
                    }
                }

                if(string.IsNullOrEmpty(resName))
                {
                    resName = "ResEffect_HealthBar_1";
                }

                GameObjectComponent gameObjectComponent = self.GetUnit().GetComponent<GameObjectComponent>();
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
                GameObject HealthBarGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,10);
                HealthBarGo.transform.SetParent(gameObjectComponent.gameObject.transform);
                float height = self.GetUnit().model.BodyHeight + 1f;
                HealthBarGo.transform.localPosition = new float3(0, height, 0);
                HealthBarGo.transform.localScale = Vector3.one;

                self.go = HealthBarGo;
                self.healthBar = self.go.transform.Find("Bar/GreenAnchor");
                self.backgroundBar = self.go.transform.Find("Bar/RedAnchor");
                self.UpdateHealth();
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<HealthBarComponent>
        {
            protected override void Destroy(HealthBarComponent self)
            {
                //UnityEngine.Object.Destroy(self.go);
                GameObjectPoolHelper.ReturnTransformToPool(self.go.transform);
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<HealthBarComponent>
        {
            protected override void Update(HealthBarComponent self)
            {
                self.Update();
            }
        }

        public static Unit GetUnit(this HealthBarComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void UpdateHealth(this HealthBarComponent self)
        {
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            int curHp = math.max(numericComponent.GetAsInt(NumericType.Hp), 0);
            int maxHp = numericComponent.GetAsInt(NumericType.MaxHp);
            float normalizedHealth = (float)curHp / maxHp;
            Vector3 scale = Vector3.one;

            if (self.healthBar != null)
            {
                scale.x = normalizedHealth;
                self.healthBar.transform.localScale = scale;
            }

            if (self.backgroundBar != null)
            {
                scale.x = 1 - normalizedHealth;
                self.backgroundBar.transform.localScale = scale;
            }
            //Log.Debug($"normalizedHealth={normalizedHealth}");

            if (normalizedHealth > 0f && normalizedHealth < 1.0f)
            {
                self.go.SetActive(true);
            }
            else
            {
                self.go.SetActive(false);
            }
        }

        public static void Update(this HealthBarComponent self)
        {
            if (self.go == null)
            {
                return;
            }
            Transform transform = self.go.transform;
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