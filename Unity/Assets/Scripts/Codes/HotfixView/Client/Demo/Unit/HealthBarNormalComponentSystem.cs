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
                HealthBarNormalManager.Instance?.RemoveRefList(self.Id);
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
            self.UpdateHealth(true);

            HealthBarNormalManager.Instance.AddRefList(self);
        }

        public static Unit GetUnit(this HealthBarNormalComponent self)
        {
            return self.Parent.GetParent<Unit>();
        }

        public static void UpdateHealth(this HealthBarNormalComponent self, bool isInit)
        {
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float curHp = math.max(numericComponent.GetAsFloat(NumericType.Hp), 0);
            float maxHp = numericComponent.GetAsFloat(NumericType.MaxHp);
            float normalizedHealth = (float)curHp / maxHp;
            self.targetNormalizedHealth = normalizedHealth;

            //Log.Debug($"normalizedHealth={normalizedHealth}");

            if (normalizedHealth > 0f && normalizedHealth < 1.0f)
            {
                if (isInit || self.isShow == false)
                {
                    self.isShow = true;
                }
            }
            else if(normalizedHealth == 0)
            {
                if (isInit || self.isShow)
                {
                    self.isShow = false;
                }
            }
            else if(normalizedHealth == 1)
            {
                if (isInit || self.isShow)
                {
                    self.isShow = false;
                }
            }
        }

        public static void Update(this HealthBarNormalComponent self)
        {
            self.UpdateDelayHp();
        }

        public static bool ChkIsShow(this HealthBarNormalComponent self)
        {
            if (self.isShow == false)
            {
                return false;
            }
            GameObjectComponent gameObjectComponent = self.GetUnit().GetComponent<GameObjectComponent>();
            if (gameObjectComponent == null || gameObjectComponent.isHiding)
            {
                return false;
            }
            GameObjectShowComponent gameObjectShowComponent = self.GetUnit().GetComponent<GameObjectShowComponent>();
            if (gameObjectShowComponent == null || gameObjectShowComponent.gameObject == null)
            {
                return false;
            }
            return true;
        }

        public static Vector3 GetPos(this HealthBarNormalComponent self)
        {
            GameObjectShowComponent gameObjectShowComponent = self.GetUnit().GetComponent<GameObjectShowComponent>();
            if (gameObjectShowComponent == null || gameObjectShowComponent.gameObject == null)
            {
                return Vector3.zero;
            }

            float height = ET.Ability.UnitHelper.GetBodyHeight(self.GetUnit()) + 0.75f;
            Vector3 position = gameObjectShowComponent.gameObject.transform.position + new Vector3(0, height, 0);
            return position;
        }

        public static float GetCurHpPer(this HealthBarNormalComponent self)
        {
            return self.targetNormalizedHealth;
        }

        public static float GetCurDelayHpPer(this HealthBarNormalComponent self)
        {
            return self.curDelayHpPer;
        }

        public static void UpdateDelayHp(this HealthBarNormalComponent self)
        {
            if (self.curDelayHpPer.Equals(self.targetNormalizedHealth))
            {
                return;
            }
            else if (math.abs(math.abs(self.curDelayHpPer) - math.abs(self.targetNormalizedHealth)) < 0.005f)
            {
                self.curDelayHpPer = self.targetNormalizedHealth;
                return;
            }
            self.curDelayHpPer = math.lerp(self.curDelayHpPer, self.targetNormalizedHealth, Time.deltaTime*2);
        }

    }
}