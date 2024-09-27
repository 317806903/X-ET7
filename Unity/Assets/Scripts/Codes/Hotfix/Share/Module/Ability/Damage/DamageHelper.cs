using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    public static class DamageHelper
    {
        public static async ETTask DoAttackArea(Unit unit, Unit resetPosByUnit, ActionCfg_AttackArea actionCfg_AttackArea, SelectHandle selectHandleOld, ActionContext actionContext)
        {
            SelectHandle selectHandle;
            if (actionCfg_AttackArea.ActionCallAutoUnitArea_Ref.ActionCallParam is ActionCallSelectLast)
            {
                selectHandle = SelectHandleHelper.CreateUnitNoneSelectHandle();
                selectHandle.unitIds.AddRange(selectHandleOld.unitIds);
            }
            else
            {
                if (selectHandleOld.selectHandleType == SelectHandleType.SelectUnits)
                {
                    selectHandle = SelectHandleHelper.CreateSelectHandle(unit, resetPosByUnit, actionCfg_AttackArea.ActionCallAutoUnitArea_Ref, ref actionContext);
                }
                else if(selectHandleOld.selectHandleType == SelectHandleType.SelectPosition)
                {
                    selectHandle = SelectHandleHelper.CreateSelectHandle(unit, true, selectHandleOld.position, false, float3.zero, actionCfg_AttackArea.ActionCallAutoUnitArea_Ref, ref actionContext);
                }
                else
                {
                    Log.Error($"ET.Ability.DamageHelper.DoAttackArea selectHandleOld.selectHandleType err [{actionCfg_AttackArea.Id}]");
                    return;
                }
                if (selectHandle == null)
                {
                    Log.Error($"DoAttackArea selectHandle == null [{actionCfg_AttackArea.Id}]");
                    return;
                }
                if (selectHandle.selectHandleType != SelectHandleType.SelectUnits)
                {
                    Log.Error($"DoAttackArea selectHandle.selectHandleType != SelectHandleType.SelectUnits");
                    return;
                }
            }
            actionContext.attackerUnitId = unit.Id;
            int count = selectHandle.unitIds.Count;
            actionContext.selectUnitNum = count;

            for (int i = 0; i < count; i++)
            {
                Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);

                EventSystem.Instance.Publish(unit.DomainScene(), new AbilityTriggerEventType.UnitOnHit()
                {
                    attackerUnit = unit,
                    defenderUnit = targetUnit,
                });

            }

            bool isCriticalStrike = ChkIsCriticalStrike(unit, actionCfg_AttackArea.DamageInfo_Ref);
            actionContext.isCriticalStrike = isCriticalStrike;

            SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(unit);
            foreach (AttackActionCall attackActionCall in actionCfg_AttackArea.SelfAttackActionCall)
            {
                SelectHandle curSelectHandle = selectHandleSelf;

                bool bRet = ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(unit, unit, attackActionCall.DelayTime, attackActionCall.ActionId, attackActionCall.ActionCondition1, attackActionCall.ActionCondition2, curSelectHandle, null, ref actionContext);
            }

            foreach (AttackActionCall attackActionCall in actionCfg_AttackArea.TargetAttackActionCall)
            {
                SelectHandle curSelectHandle = selectHandle;

                Unit resetPosByUnitNew = null;
                if (curSelectHandle.unitIds.Count > 0)
                {
                    resetPosByUnitNew = UnitHelper.GetUnit(unit.DomainScene(), curSelectHandle.unitIds[0]);
                }
                bool bRet = ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(unit, unit, attackActionCall.DelayTime, attackActionCall.ActionId, attackActionCall.ActionCondition1, attackActionCall.ActionCondition2, curSelectHandle, resetPosByUnitNew, ref actionContext);

            }

            if (string.IsNullOrEmpty(actionCfg_AttackArea.DamageInfo) == false)
            {
                await DoDamage(unit, actionCfg_AttackArea.DamageInfo_Ref, selectHandle, actionCfg_AttackArea.DamageAllot, isCriticalStrike);
            }
        }

        public static async ETTask DoDamage(Unit unit, ActionCfg_DamageUnit actionCfg_DamageUnit, SelectHandle selectHandle, DamageAllot damageAllot, bool isCriticalStrike)
        {
            if (selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                Log.Error($"ET.Ability.DamageHelper.DoDamage selectHandle.selectHandleType[{selectHandle.selectHandleType}] != SelectHandleType.SelectUnits");
                return;
            }

            float damageScale = 0;
            int count = selectHandle.unitIds.Count;
            if (count == 0)
            {
                return;
            }
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

            int stopNum = 300;
            for (int i = 0; i < count; i++)
            {
                Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);
                if (UnitHelper.ChkUnitAlive(targetUnit) == false)
                {
                    continue;
                }
                Damage damage = GetDamage(unit, targetUnit, actionCfg_DamageUnit, isCriticalStrike);
                if (damageAllot is DamageAllotChg damageAllotChg)
                {
                    damage *= damageScale;
                    damageScale *= ((100 + damageAllotChg.WeightChg) * 0.01f);
                }
                else
                {
                    damage *= damageScale;
                }
                CreateDamageInfo(unit, targetUnit, damage, isCriticalStrike);
                if (i >= stopNum && i % stopNum == 0)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                }
                else
                {
                    while (IdGenerater.Instance.ChkGenerateIdFull())
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                    }
                }
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

        public static bool ChkIsCriticalStrike(Unit attackerUnit, ActionCfg_DamageUnit actionCfg_DamageUnit)
        {
            if (actionCfg_DamageUnit == null)
            {
                NumericComponent attackNumeric = attackerUnit.GetComponent<NumericComponent>();
                float attacker_CriticalStrikeRate = attackNumeric.GetAsFloat(NumericType.CriticalStrikeRate);
                if (RandomGenerator.RandFloat01() < attacker_CriticalStrikeRate * 0.01f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            ET.AbilityConfig.DamageInfo damageInfo = actionCfg_DamageUnit.DamageInfo;
            switch (damageInfo.DamageType)
            {
                case DamageType.FixedBlood:
                    return false;
                case DamageType.PercentTotalBloodAttacker:
                    return false;
                case DamageType.PercentTotalBloodBeHurter:
                    return false;
                case DamageType.PercentCurBloodAttacker:
                    return false;
                case DamageType.PercentCurBloodBeHurter:
                    return false;
                case DamageType.PropertyBlood:
                    NumericComponent attackNumeric = attackerUnit.GetComponent<NumericComponent>();
                    float attacker_CriticalStrikeRate = attackNumeric.GetAsFloat(NumericType.CriticalStrikeRate);
                    if (RandomGenerator.RandFloat01() < attacker_CriticalStrikeRate * 0.01f)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case DamageType.LastSelectBlood:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }

        public static Damage GetDamage(Unit attackerUnit, Unit targetUnit, ActionCfg_DamageUnit actionCfg_DamageUnit, bool isCriticalStrike)
        {
            float damageValue = 0;
            ET.AbilityConfig.DamageInfo damageInfo = actionCfg_DamageUnit.DamageInfo;
            switch (damageInfo.DamageType)
            {
                case DamageType.FixedBlood:
                    damageValue = damageInfo.Value;
                    break;
                case DamageType.PercentTotalBloodAttacker:
                    damageValue = attackerUnit.GetComponent<NumericComponent>().GetAsFloat(NumericType.MaxHp) * damageInfo.Value;
                    break;
                case DamageType.PercentTotalBloodBeHurter:
                    damageValue = targetUnit.GetComponent<NumericComponent>().GetAsFloat(NumericType.MaxHp) * damageInfo.Value;
                    break;
                case DamageType.PercentCurBloodAttacker:
                    damageValue = attackerUnit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Hp) * damageInfo.Value;
                    break;
                case DamageType.PercentCurBloodBeHurter:
                    damageValue = targetUnit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Hp) * damageInfo.Value;
                    break;
                case DamageType.PropertyBlood:
                    damageValue = GetDamageByProperty(attackerUnit, targetUnit, damageInfo.Value, isCriticalStrike);
                    break;
                case DamageType.LastSelectBlood:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ChgDamageByScaleDisOrHeight(attackerUnit,  targetUnit, damageInfo, ref damageValue);

            Damage damage = new(NumericType.PhysicalAttack, damageValue);
            return damage;
        }

        public static void ChgDamageByScaleDisOrHeight(Unit attackerUnit, Unit targetUnit, ET.AbilityConfig.DamageInfo damageInfo, ref float damageValue)
        {
            if (damageInfo.ScaleByDis != 0)
            {
                Unit casterActorUnit = attackerUnit.GetCasterActor();

                //按照距离变化(>0表示越远伤害越高,<0表示越近伤害越高)
                float dis = math.length(casterActorUnit.Position - targetUnit.Position);
                if (damageInfo.ScaleByDis > 0)
                {
                    damageValue *= (1 + math.min(dis * math.abs(damageInfo.ScaleByDis), math.abs(damageInfo.ScaleByDisMax))*0.01f);
                }
                else
                {
                    damageValue *= (1 + math.max(math.abs(damageInfo.ScaleByDisMax) - dis * math.abs(damageInfo.ScaleByDis), 0)*0.01f);
                }
            }

            if (damageInfo.ScaleByHeight != 0)
            {
                Unit casterActorUnit = attackerUnit.GetCasterActor();

                //按照高度变化(>0表示往上越远伤害越高,<0表示往下越远伤害越高)
                if (
                    (damageInfo.ScaleByHeight > 0 && casterActorUnit.Position.y < targetUnit.Position.y)
                    || (damageInfo.ScaleByHeight < 0 && casterActorUnit.Position.y > targetUnit.Position.y)
                    )
                {
                    float disHeight = math.abs(casterActorUnit.Position.y - targetUnit.Position.y);
                    damageValue *= (1 + math.min(disHeight * math.abs(damageInfo.ScaleByHeight), math.abs(damageInfo.ScaleByHeightMax))*0.01f);
                }
            }
        }

        public static float GetDamageByProperty(Unit attackerUnit, Unit targetUnit, float attackScale, bool isCriticalStrike)
        {
            NumericComponent attackNumeric = attackerUnit.GetComponent<NumericComponent>();
            NumericComponent targetNumeric = targetUnit.GetComponent<NumericComponent>();
            float attacker_Attack = attackNumeric.GetAsFloat(NumericType.PhysicalAttack);
            attacker_Attack *= attackScale;
            //attacker_Attack = math.max(0, attacker_Attack);
            float attacker_DamageDeepening = attackNumeric.GetAsFloat(NumericType.DamageDeepening);
            //attacker_DamageDeepening = math.max(0, attacker_DamageDeepening);
            float target_DamageRelief = targetNumeric.GetAsFloat(NumericType.DamageRelief);
            //target_DamageRelief = math.clamp(target_DamageRelief, 0, 100);

            float damageValue = attacker_Attack;
            if (isCriticalStrike)
            {
                float attacker_CriticalHitDamage = attackNumeric.GetAsFloat(NumericType.CriticalHitDamage);
                attacker_CriticalHitDamage = math.max(0, attacker_CriticalHitDamage);
                damageValue *= ((100 + attacker_CriticalHitDamage) * 0.01f);
            }
            damageValue *= ((100 + attacker_DamageDeepening) * 0.01f);
            damageValue *= math.max(0, (100 - target_DamageRelief) * 0.01f);

            return damageValue;
        }

        public static DamageInfo CreateDamageInfo(Unit unit, Unit targetUnit, Damage damage, bool isCrit)
        {
            Scene scene = unit.DomainScene();
            return scene.GetComponent<DamageComponent>().Add(unit, targetUnit, damage, isCrit);
        }
    }
}