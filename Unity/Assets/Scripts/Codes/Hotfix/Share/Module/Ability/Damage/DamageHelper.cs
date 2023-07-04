using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    public static class DamageHelper
    {
        public static void DoAttackArea(Unit unit, ActionCfg_AttackArea actionCfg_AttackArea, SelectHandle selectHandleOld, ActionContext actionContext)
        {
            SelectHandle selectHandle = SelectHandleHelper.CreateSelectHandle(unit, actionCfg_AttackArea.ActionCallAutoUnitArea);
            if (selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                return;
            }
            if (actionCfg_AttackArea.ActionCallAutoUnitArea is ActionCallAutoUnitOne actionCallAutoUnitOne)
            {
                selectHandle.unitIds.AddRange(selectHandleOld.unitIds);
            }
            actionContext.attackerUnitId = unit.Id;
            int count = selectHandle.unitIds.Count;
            for (int i = 0; i < count; i++)
            {
                Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);
                
                EventSystem.Instance.Publish(unit.DomainScene(), new AbilityTriggerEventType.UnitOnHit()
                {
                    attackerUnit = unit,
                    defenderUnit = targetUnit,
                });

            }

            SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(unit);
            foreach (AttackActionCall attackActionCall in actionCfg_AttackArea.SelfAttackActionCall)
            {
                SelectHandle curSelectHandle = selectHandleSelf;
                (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = ConditionHandleHelper.ChkCondition(unit, curSelectHandle, attackActionCall.ActionCondition1, actionContext);
                if (isChgSelect1)
                {
                    curSelectHandle = newSelectHandle1;
                }
                (bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = ConditionHandleHelper.ChkCondition(unit, curSelectHandle, attackActionCall.ActionCondition2, actionContext);
                if (isChgSelect2)
                {
                    curSelectHandle = newSelectHandle2;
                }
                if (bRet1 && bRet2)
                {
                    ActionHandlerHelper.CreateAction(unit, attackActionCall.ActionId, attackActionCall.DelayTime, curSelectHandle, actionContext);
                }
            }
            
            foreach (AttackActionCall attackActionCall in actionCfg_AttackArea.TargetAttackActionCall)
            {
                SelectHandle curSelectHandle = selectHandle;
                (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = ConditionHandleHelper.ChkCondition(unit, curSelectHandle, attackActionCall.ActionCondition1, actionContext);
                if (isChgSelect1)
                {
                    curSelectHandle = newSelectHandle1;
                }
                (bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = ConditionHandleHelper.ChkCondition(unit, curSelectHandle, attackActionCall.ActionCondition2, actionContext);
                if (isChgSelect2)
                {
                    curSelectHandle = newSelectHandle2;
                }
                if (bRet1 && bRet2)
                {
                    ActionHandlerHelper.CreateAction(unit, attackActionCall.ActionId, attackActionCall.DelayTime, curSelectHandle, actionContext);
                }
                
            }

            if (string.IsNullOrEmpty(actionCfg_AttackArea.DamageInfo) == false)
            {
                DoDamage(unit, actionCfg_AttackArea.DamageInfo_Ref, selectHandle, actionCfg_AttackArea.DamageAllot);
            }
        }
        
        public static void DoDamage(Unit unit, ActionCfg_DamageUnit actionCfg_DamageUnit, SelectHandle selectHandle, DamageAllot damageAllot)
        {
            if (selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                return;
            }

            float damageScale = 0;
            int count = selectHandle.unitIds.Count;
            if (damageAllot == null)
            {
                damageScale = 1;
            }
            else if (damageAllot is DamageAllotTotal)
            {
                damageScale = 1;
            }
            else if (damageAllot is DamageAllotShare)
            {
                damageScale = 1 / count;
            }
            else if (damageAllot is DamageAllotChg damageAllotChg)
            {
                if (damageAllotChg.IsKeepTotal)
                {
                    float total = GetIncreaseNum(1, damageAllotChg.WeightChg, count);
                    damageScale = 1 / total;
                }
                else
                {
                    damageScale = 1;
                }
            }

            for (int i = 0; i < count; i++)
            {
                Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);
                if (UnitHelper.ChkUnitAlive(targetUnit) == false)
                {
                    continue;
                }
                Damage damage = GetDamage(unit, targetUnit, actionCfg_DamageUnit);
                if (damageAllot is DamageAllotChg damageAllotChg)
                {
                    damage = damage * damageScale;
                    damageScale *= ((100 + damageAllotChg.WeightChg) * 0.01f);
                }
                else
                {
                    damage = damage * damageScale;
                }
                CreateDamageInfo(unit, targetUnit, damage, 0, 0, null);
            }
        }

        private static float GetIncreaseNum(float initValue, int increasePer, int count)
        {
            if (count == 1)
            {
                return initValue;
            }
            else
            {
                float valueNew = initValue * ((100 + increasePer) * 0.01f);
                return initValue + GetIncreaseNum(valueNew, increasePer, count - 1);
            }
        }
        
        public static Damage GetDamage(Unit attackerUnit, Unit targetUnit, ActionCfg_DamageUnit actionCfg_DamageUnit)
        {
            float value = 0;
            ET.AbilityConfig.DamageInfo damageInfo = actionCfg_DamageUnit.DamageInfo;
            switch (damageInfo.DamageType)
            {
                case DamageType.FixedBlood:
                    value = damageInfo.Value;
                    break;
                case DamageType.PercentTotalBloodAttacker:
                    value = attackerUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.MaxHp) * damageInfo.Value;
                    break;
                case DamageType.PercentTotalBloodBeHurter:
                    value = targetUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.MaxHp) * damageInfo.Value;
                    break;
                case DamageType.PercentCurBloodAttacker:
                    value = attackerUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Hp) * damageInfo.Value;
                    break;
                case DamageType.PercentCurBloodBeHurter:
                    value = targetUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Hp) * damageInfo.Value;
                    break;
                case DamageType.PropertyBlood:
                    value = attackerUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.PhysicalAttack);
                    break;
                case DamageType.LastSelectBlood:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Damage damage = new(NumericType.PhysicalAttack, value);
            return damage;
        }
        
        public static DamageInfo CreateDamageInfo(Unit unit, Unit targetUnit, Damage damage, float damageDegree, float criticalRate,
        DamageSourceTag[] tags)
        {
            Scene scene = unit.DomainScene();
            return scene.GetComponent<DamageComponent>().Add(unit, targetUnit, damage, damageDegree, criticalRate, tags);
        }
    }
}