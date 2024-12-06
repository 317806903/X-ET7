using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(DamageComponent))]
    [FriendOf(typeof(DamageObj))]
    public static class DamageComponentSystem
    {
        [ObjectSystem]
        public class DamageComponentAwakeSystem: AwakeSystem<DamageComponent>
        {
            protected override void Awake(DamageComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DamageComponentDestroySystem: DestroySystem<DamageComponent>
        {
            protected override void Destroy(DamageComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DamageComponentFixedUpdateSystem: FixedUpdateSystem<DamageComponent>
        {
            protected override void FixedUpdate(DamageComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static DamageObj Add(this DamageComponent self, Unit attackerUnit, Unit targetUnit, Damage damage, bool isCrit, ref ActionContext actionContext)
        {
            DamageObj damageObj = self.AddChild<DamageObj>();
            damageObj.Init(attackerUnit.Id, targetUnit.Id, damage, isCrit, ref actionContext);
            return damageObj;
        }

        public static void ChgDamageObj(this DamageComponent self, ActionCfg_DamageChg _ActionCfg_DamageChg, ref ActionContext actionContext)
        {
            long damageObjId = actionContext.damageObjId;
            if (damageObjId == 0)
            {
                return;
            }
            DamageObj damageObj = self.GetChild<DamageObj>(damageObjId);
            if (damageObj == null)
            {
                return;
            }

            bool isPercent = _ActionCfg_DamageChg.IsPercent;
            float chgValue = _ActionCfg_DamageChg.ChgValue;
            ValueOperation valueOperation = _ActionCfg_DamageChg.ChgValueOperation;
            if (valueOperation == ValueOperation.Add)
            {
                if (isPercent)
                {
                    damageObj.damage = damageObj.damage * (1 + chgValue * 0.01f);
                }
                else
                {
                    damageObj.damage = damageObj.damage + chgValue;
                }
            }
            else if (valueOperation == ValueOperation.Reduce)
            {
                if (isPercent)
                {
                    damageObj.damage = damageObj.damage * (1 - chgValue * 0.01f);
                }
                else
                {
                    damageObj.damage = damageObj.damage + (-chgValue);
                }
            }
            else if (valueOperation == ValueOperation.Set)
            {
                if (isPercent)
                {
                    damageObj.damage = damageObj.damage * (chgValue * 0.01f);
                }
                else
                {
                    damageObj.damage = Damage.Set(damageObj.damage, chgValue);
                }
            }
        }

        public static void FixedUpdate(this DamageComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            while (self.Children.Count > 0)
            {
                foreach (var obj in self.Children.Values)
                {
                    DamageObj damageObj = obj as DamageObj;
                    damageObj.DealWithDamage();
                    damageObj.Dispose();
                    break;
                }
            }
        }

        public static void DealWithDamageQuick(this DamageComponent self, Unit targetUnit, Damage damage, ref ActionContext actionContext, string actionCfgDamageUnitId)
        {
            DamageObj damageObj = self.AddChild<DamageObj>();
            damageObj.Init(0, targetUnit.Id, damage, false, ref actionContext);
            damageObj.SetDamageShowType(actionCfgDamageUnitId);

            damageObj.DealWithDamageQuick();
            damageObj.Dispose();
        }
    }
}