using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (DamageObj))]
    public static class DamageObjSystem
    {
        [ObjectSystem]
        public class DamageInfoAwakeSystem: AwakeSystem<DamageObj>
        {
            protected override void Awake(DamageObj self)
            {
            }
        }

        [ObjectSystem]
        public class DamageInfoDestroySystem: DestroySystem<DamageObj>
        {
            protected override void Destroy(DamageObj self)
            {
            }
        }

        public static void Init(this DamageObj self, long attackerUnitId, long targetUnitId, Damage damage, bool isCrit, ref ActionContext actionContext)
        {
            self.attackerUnitId = attackerUnitId;
            self.defenderUnitId = targetUnitId;
            self.damage = damage;
            self.isCrit = isCrit;

            self.actionContext = actionContext;
            self.actionContext.attackerUnitId = attackerUnitId;
            self.actionContext.defenderUnitId = targetUnitId;
            self.actionContext.isCriticalStrike = isCrit;
            self.actionContext.damageObjId = self.Id;
        }

        public static void SetDamageShowType(this DamageObj self, string damageUnitCfgId)
        {
            self.damageUnitCfgId = damageUnitCfgId;
        }

        ///<summary>
        ///这里再结合是否闪避，是否暴击进行处理
        ///</summary>
        public static int DamageValue(this DamageObj self){
            return self.damage.Overall();
        }

        /// <summary>
        /// 获取攻击者
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetAttackerUnit(this DamageObj self)
        {
            Scene scene = self.DomainScene();
            Unit unit = UnitHelper.GetUnit(scene, self.attackerUnitId);
            return unit;
        }

        /// <summary>
        /// 获取受击者
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetDefenderUnit(this DamageObj self)
        {
            Scene scene = self.DomainScene();
            Unit unit = UnitHelper.GetUnit(scene, self.defenderUnitId);
            return unit;
        }

        public static void DealWithDamage(this DamageObj self)
        {
            Scene scene = self.DomainScene();
            //如果目标已经挂了，就直接return了
            if (UnitHelper.ChkUnitAlive(scene, self.defenderUnitId) == false)
                return;

            Unit attackerUnit = self.GetAttackerUnit();
            Unit defenderUnit = self.GetDefenderUnit();
            EventSystem.Instance.Publish(scene, new AbilityTriggerEventType.DamageBeforeOnHit()
                {
                    actionContext = self.actionContext,
                    attackerUnit = attackerUnit,
                    defenderUnit = defenderUnit,
                });

            int damageValue = self.DamageValue();
            bool canBeDamage = ET.Ability.BuffHelper.ChkCanBeDamage(defenderUnit);
            if (canBeDamage)
            {
                // if (self.CanBeKilledByDamageInfo(defenderUnit))
                // {
                //     EventSystem.Instance.Publish(scene, new AbilityTriggerEventType.DamageBeforeOnKill()
                //     {
                //         actionContext = self.actionContext,
                //         attackerUnit = attackerUnit,
                //         defenderUnit = defenderUnit,
                //     });
                // }

                NumericComponent numericComponent = defenderUnit.GetComponent<NumericComponent>();
                float curHp = numericComponent.GetAsFloat(NumericType.Hp);
                float maxHp = numericComponent.GetAsFloat(NumericType.MaxHp);
                float newHp = math.min(math.max(0, curHp - damageValue), maxHp);
                numericComponent.SetAsFloatToBase(NumericType.Hp, newHp);
            }
            else
            {
                damageValue = 0;
            }

            if (damageValue != 0)
            {
                if (self.model.FloatingTextId == "FloatingText_None")
                {
                }
                else if (self.model.FloatingTextId == "FloatingText_Default")
                {
                    ET.Ability.UnitHelper.AddSyncData_DamageShow(defenderUnit, -damageValue, self.isCrit);
                }
                else
                {
                    SelectHandle selectHandle = SelectHandleHelper.CreateUnitSelfSelectHandle(defenderUnit);
                    FloatingTextHelper.AddFloatingText(defenderUnit, self.model.FloatingTextId, damageValue, selectHandle, ref self.actionContext);
                }
            }

            EventSystem.Instance.Publish(scene, new AbilityTriggerEventType.DamageAfterOnHit()
            {
                actionContext = self.actionContext,
                attackerUnit = attackerUnit,
                defenderUnit = defenderUnit,
            });

            if (UnitHelper.ChkUnitAlive(defenderUnit) == false)
            {
                EventSystem.Instance.Publish(scene, new AbilityTriggerEventType.DamageBeforeOnKill()
                {
                    actionContext = self.actionContext,
                    attackerUnit = attackerUnit,
                    defenderUnit = defenderUnit,
                });

                if (UnitHelper.ChkUnitAlive(defenderUnit) == false)
                {
                    defenderUnit.DestroyWithDeathShow(false);

                    EventSystem.Instance.Publish(scene, new AbilityTriggerEventType.DamageAfterOnKill()
                    {
                        actionContext = self.actionContext,
                        attackerUnit = attackerUnit,
                        defenderUnit = defenderUnit,
                    });

                }
            }
        }

        public static void DealWithDamageQuick(this DamageObj self)
        {
            Scene scene = self.DomainScene();
            //如果目标已经挂了，就直接return了
            if (UnitHelper.ChkUnitAlive(scene, self.defenderUnitId, true) == false)
                return;

            Unit defenderUnit = self.GetDefenderUnit();

            int damageValue = self.DamageValue();
            bool canBeDamage = ET.Ability.BuffHelper.ChkCanBeDamage(defenderUnit);
            if (canBeDamage)
            {
                NumericComponent numericComponent = defenderUnit.GetComponent<NumericComponent>();
                float curHp = numericComponent.GetAsFloat(NumericType.Hp);
                float maxHp = numericComponent.GetAsFloat(NumericType.MaxHp);
                float newHp = math.min(math.max(0, curHp - damageValue), maxHp);
                numericComponent.SetAsFloatToBase(NumericType.Hp, newHp);
            }
            else
            {
                damageValue = 0;
            }

            if (damageValue != 0)
            {
                if (self.model.FloatingTextId == "FloatingText_None")
                {
                }
                else if (self.model.FloatingTextId == "FloatingText_Default")
                {
                    ET.Ability.UnitHelper.AddSyncData_DamageShow(defenderUnit, -damageValue, self.isCrit);
                }
                else
                {
                    SelectHandle selectHandle = SelectHandleHelper.CreateUnitSelfSelectHandle(defenderUnit);
                    FloatingTextHelper.AddFloatingText(defenderUnit, self.model.FloatingTextId, damageValue, selectHandle, ref self.actionContext);
                }
            }
        }

        // public static bool CanBeKilledByDamageInfo(this DamageObj self, Unit targetUnit)
        // {
        //     NumericComponent numericComponent = targetUnit.GetComponent<NumericComponent>();
        //     int curHp = numericComponent.GetAsInt(NumericType.Hp);
        //     int damageValue = self.damage.Overall();
        //     if (damageValue >= curHp)
        //     {
        //         return true;
        //     }
        //     return false;
        // }
    }
}