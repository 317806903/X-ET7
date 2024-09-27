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

        public static async ETTask Init(this HealthBarNormalComponent self)
        {
            string resName = "ResEffect_HealthBar_1";

            GameObjectShowComponent gameObjectShowComponent = self.GetUnit().GetComponent<GameObjectShowComponent>();
            if (gameObjectShowComponent == null || gameObjectShowComponent.gameObject == null)
            {
                return;
            }
            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
            GameObject HealthBarGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,30);
            HealthBarGo.transform.SetParent(gameObjectShowComponent.gameObject.transform);
            float scaleX = gameObjectShowComponent.gameObject.transform.localScale.x;
            HealthBarGo.transform.localScale = Vector3.one / scaleX;
            float height = ET.Ability.UnitHelper.GetBodyHeight(self.GetUnit()) + 0.75f;
            HealthBarGo.transform.position = gameObjectShowComponent.gameObject.transform.position + new Vector3(0, height, 0);

            self.go = HealthBarGo;
            self.healthBar = self.go.transform.Find("Bar/GreenAnchor");
            self.delayHealthBar = self.go.transform.Find("Bar/DelayAnchor");
            self.backgroundBar = self.go.transform.Find("Bar/RedAnchor");

            self.UpdateHealth(true);
        }

        public static Unit GetUnit(this HealthBarNormalComponent self)
        {
            return self.Parent.GetParent<Unit>();
        }

        public static void UpdateHealth(this HealthBarNormalComponent self, bool isInit)
        {
            if (self.go == null)
            {
                return;
            }

            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float curHp = math.max(numericComponent.GetAsFloat(NumericType.Hp), 0);
            float maxHp = numericComponent.GetAsFloat(NumericType.MaxHp);
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
                if (isInit || self.isActivity == false)
                {
                    self.go.SetActive(true);
                    self.isActivity = true;
                }
            }
            else if(normalizedHealth == 0)
            {
                if (isInit || self.isActivity)
                {
                    self.go.SetActive(false);
                    self.isActivity = false;
                }
            }
            else if(normalizedHealth == 1)
            {
                if (isInit || self.isActivity)
                {
                    self.go.SetActive(false);
                    self.isActivity = false;
                }
            }
        }

        public static Camera GetMainCamera(this HealthBarNormalComponent self)
        {
            if (self.mainCamera == null)
            {
                Camera mainCamera = CameraHelper.GetMainCamera(self.DomainScene());
                self.mainCamera = mainCamera;
            }
            return self.mainCamera;
        }

        public static void Update(this HealthBarNormalComponent self)
        {
            if (self.go == null)
            {
                return;
            }

            if (self.isActivity == false)
            {
                return;
            }

            if (++self.curFrame >= self.waitFrame)
            {
                self.curFrame = 0;

                self.UpdateForward();
            }

            self.UpdateDelayHp();
        }

        public static void UpdateForward(this HealthBarNormalComponent self)
        {
            if (self.go == null)
            {
                return;
            }
            Transform transform = self.go.transform;
            Camera mainCamera = self.GetMainCamera();
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