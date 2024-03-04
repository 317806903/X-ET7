using System;
using ET.AbilityConfig;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class HealthBarNormalComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HealthBarNormalComponent>
        {
            protected override void Awake(HealthBarNormalComponent self)
            {
                string resName = "ResEffect_HealthBar_1";

                GameObjectComponent gameObjectComponent = self.GetUnit().GetComponent<GameObjectComponent>();
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
                GameObject HealthBarGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,10);
                HealthBarGo.transform.SetParent(gameObjectComponent.gameObject.transform);
                float height = self.GetUnit().model.BodyHeight + 1f;
                HealthBarGo.transform.localPosition = new float3(0, height, 0);
                float scaleX = gameObjectComponent.gameObject.transform.localScale.x;
                HealthBarGo.transform.localScale = Vector3.one / scaleX;

                self.go = HealthBarGo;
                self.healthBar = self.go.transform.Find("Bar/GreenAnchor");
                self.delayHealthBar = self.go.transform.Find("Bar/DelayAnchor");
                self.backgroundBar = self.go.transform.Find("Bar/RedAnchor");

                self.UpdateHealth(true);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<HealthBarNormalComponent>
        {
            protected override void Destroy(HealthBarNormalComponent self)
            {
                if (self.go != null)
                {
                    //UnityEngine.Object.Destroy(self.go);
                    GameObjectPoolHelper.ReturnTransformToPool(self.go.transform);
                    self.go = null;
                }
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<HealthBarNormalComponent>
        {
            protected override void Update(HealthBarNormalComponent self)
            {
                self.Update();
            }
        }

        public static Unit GetUnit(this HealthBarNormalComponent self)
        {
            return self.Parent.GetParent<Unit>();
        }

        public static void UpdateHealth(this HealthBarNormalComponent self, bool isInit)
        {
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            int curHp = math.max(numericComponent.GetAsInt(NumericType.Hp), 0);
            int maxHp = numericComponent.GetAsInt(NumericType.MaxHp);
            float normalizedHealth = (float)curHp / maxHp;
            Vector3 scale = Vector3.one;
            self.targetNormalizedHealth = normalizedHealth;
            if (self.healthBar != null)
            {
                scale.x = normalizedHealth;
                self.healthBar.localScale = scale;
            }

            if (isInit)
            {
                if (self.backgroundBar != null)
                {
                    scale.x = 1 - normalizedHealth;
                    self.backgroundBar.localScale = scale;
                }

                if (self.delayHealthBar != null)
                {
                    self.delayHealthBar.localScale = Vector3.zero;
                }
            }
            else
            {
                if (self.delayHealthBar == null)
                {
                    if (self.backgroundBar != null)
                    {
                        scale.x = 1 - normalizedHealth;
                        self.backgroundBar.localScale = scale;
                    }
                }
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

        public static void Update(this HealthBarNormalComponent self)
        {
            if (self.go == null)
            {
                return;
            }

            self.UpdateForward();
            self.UpdateDelayHp();
        }

        public static void UpdateForward(this HealthBarNormalComponent self)
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

        public static void UpdateDelayHp(this HealthBarNormalComponent self)
        {
            if (self.go == null)
            {
                return;
            }
            if (self.delayHealthBar != null)
            {
                if (self.curNormalizedHealth == 1 - self.targetNormalizedHealth)
                {
                    return;
                }
                else if (math.abs(math.abs(self.curNormalizedHealth) - math.abs(1 - self.targetNormalizedHealth)) < 0.005f)
                {
                    self.curNormalizedHealth = 1-self.targetNormalizedHealth;
                    self.backgroundBar.localScale = new Vector3(1-self.targetNormalizedHealth, 1, 1);
                    self.delayHealthBar.localScale = new Vector3(0, 1, 1);
                    return;
                }
                Vector3 scale = Vector3.one;
                self.curNormalizedHealth = math.lerp(self.curNormalizedHealth, 1-self.targetNormalizedHealth, Time.deltaTime*2);
                scale.x = self.curNormalizedHealth;

                self.backgroundBar.localScale = scale;

                self.delayHealthBar.localScale = new Vector3(1-self.targetNormalizedHealth - self.curNormalizedHealth, 1, 1);
                self.delayHealthBar.localPosition = new Vector3(self.healthBar.localPosition.x - self.healthBar.localScale.x, 0, 0);
            }
        }

    }
}