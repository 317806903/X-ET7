using System;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class HealthBarComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HealthBarComponent, GameObject>
        {
            protected override void Awake(HealthBarComponent self, GameObject go)
            {
                self.go = go;
                self.healthBar = go.transform.Find("Bar/GreenAnchor");
                self.backgroundBar = go.transform.Find("Bar/RedAnchor");
                self.UpdateHealth();
            }
        }
        
        [ObjectSystem]
        public class DestroySystem: DestroySystem<HealthBarComponent>
        {
            protected override void Destroy(HealthBarComponent self)
            {
                UnityEngine.Object.Destroy(self.go);
            }
        }
        
        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<HealthBarComponent>
        {
            protected override void Update(HealthBarComponent self)
            {
                Transform transform = self.go.transform;
                Vector3 direction = CameraHelper.GetMainCamera(self.DomainScene()).transform.forward;
                transform.forward = -direction;
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

            if (normalizedHealth > 0f && normalizedHealth < 1.0f)
            {
                self.go.SetActive(true);
            }
            else
            {
                self.go.SetActive(false);
            }
        }

    }
}