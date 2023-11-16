using System;
using ET.AbilityConfig;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class HealthBarHomeComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HealthBarHomeComponent>
        {
            protected override void Awake(HealthBarHomeComponent self)
            {
                string resName = "ResEffect_MainTowerBar";

                GameObjectComponent gameObjectComponent = self.GetUnit().GetComponent<GameObjectComponent>();
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
                GameObject HealthBarGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,10);
                HealthBarGo.transform.SetParent(gameObjectComponent.gameObject.transform);
                float height = self.GetUnit().model.BodyHeight + 1f;
                HealthBarGo.transform.localPosition = new float3(0, height, 0);
                HealthBarGo.transform.localScale = Vector3.one;

                self.go = HealthBarGo;
                self.healthBar = self.go.transform.Find("Bar/Root/GreenAnchor");
                self.backgroundBar = self.go.transform.Find("Bar/Root/RedAnchor");
                self.HpValueShowTrans = self.go.transform.Find("GameObject/HpValueShow");

                self.mat = self.healthBar.GetComponent<SpriteRenderer>().material;

                self.UpdateHealth(true);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<HealthBarHomeComponent>
        {
            protected override void Destroy(HealthBarHomeComponent self)
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
        public class UpdateSystem: UpdateSystem<HealthBarHomeComponent>
        {
            protected override void Update(HealthBarHomeComponent self)
            {
                self.Update();
            }
        }

        public static Unit GetUnit(this HealthBarHomeComponent self)
        {
            return self.Parent.GetParent<Unit>();
        }

        public static void UpdateHealth(this HealthBarHomeComponent self, bool isInit)
        {
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            int curHp = math.max(numericComponent.GetAsInt(NumericType.Hp), 0);
            int maxHp = numericComponent.GetAsInt(NumericType.MaxHp);
            float normalizedHealth = (float)curHp / maxHp;
            self.mat.SetFloat("_Angle", normalizedHealth);

            //Log.Debug($"normalizedHealth={normalizedHealth}");

            if (self.HpValueShowTrans != null)
            {
                TextMeshPro textMeshPro = self.HpValueShowTrans.GetComponent<TextMeshPro>();
                textMeshPro.text = $"{curHp}/{maxHp}";
            }

            if (normalizedHealth > 0f && normalizedHealth < 1.0f)
            {
                self.go.SetActive(true);
            }
            else
            {
                self.go.SetActive(false);
            }
        }

        public static void Update(this HealthBarHomeComponent self)
        {
            if (self.go == null)
            {
                return;
            }

            self.UpdateForward();
        }

        public static void UpdateForward(this HealthBarHomeComponent self)
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