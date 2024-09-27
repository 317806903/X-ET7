using System;
using ET.AbilityConfig;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class HealthBarTowerComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HealthBarTowerComponent>
        {
            protected override void Awake(HealthBarTowerComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<HealthBarTowerComponent>
        {
            protected override void Destroy(HealthBarTowerComponent self)
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
        public class UpdateSystem: UpdateSystem<HealthBarTowerComponent>
        {
            protected override void Update(HealthBarTowerComponent self)
            {
                self.Update();
            }
        }

        public static async ETTask Init(this HealthBarTowerComponent self)
        {
            string resName = "ResEffect_MainTowerBar";

            GameObjectShowComponent gameObjectShowComponent = self.GetUnit().GetComponent<GameObjectShowComponent>();
            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
            GameObject HealthBarGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
            HealthBarGo.transform.SetParent(gameObjectShowComponent.gameObject.transform);

            float scaleX = gameObjectShowComponent.gameObject.transform.localScale.x;
            HealthBarGo.transform.localScale = Vector3.one / scaleX;

            float height = ET.Ability.UnitHelper.GetBodyHeight(self.GetUnit()) + 2f;
            HealthBarGo.transform.position = gameObjectShowComponent.gameObject.transform.position + new Vector3(0, height, 0);

            self.go = HealthBarGo;
            self.healthBar = self.go.transform.Find("Bar/Root/GreenAnchor");
            self.backgroundBar = self.go.transform.Find("Bar/Root/RedAnchor");
            self.HpValueShowTrans = self.go.transform.Find("GameObject/HpValueShow");

            self.mat = self.healthBar.GetComponent<SpriteRenderer>().material;

            self.UpdateHealth(true);
        }

        public static Unit GetUnit(this HealthBarTowerComponent self)
        {
            return self.Parent.GetParent<Unit>();
        }

        public static void UpdateHealth(this HealthBarTowerComponent self, bool isInit)
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

        public static Camera GetMainCamera(this HealthBarTowerComponent self)
        {
            if (self.mainCamera == null)
            {
                Camera mainCamera = CameraHelper.GetMainCamera(self.DomainScene());
                self.mainCamera = mainCamera;
            }
            return self.mainCamera;
        }

        public static void Update(this HealthBarTowerComponent self)
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

        }

        public static void UpdateForward(this HealthBarTowerComponent self)
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
    }
}