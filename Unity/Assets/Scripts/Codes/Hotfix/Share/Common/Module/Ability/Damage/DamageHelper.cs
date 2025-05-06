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
            //Log.Error($"--zpb DoAttackArea 00000 Bullet unit[{unit.Id}] actionCfg_AttackArea[{actionCfg_AttackArea.Id}]");
            bool isReady = await ET.AOIHelper.ChkAOIReady(null, unit);
            if (isReady == false)
            {
                return;
            }
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
                    selectHandle = SelectHandleHelper.CreateSelectHandle(unit, true, selectHandleOld.position, false, float3.zero, actionCfg_AttackArea.ActionCallAutoUnitArea_Ref, ref actionContext, false);
                }
                else
                {
                    Log.Error($"ET.Ability.DamageHelper.DoAttackArea selectHandleOld.selectHandleType err [{actionCfg_AttackArea.Id}]");
                    return;
                }
                if (selectHandle == null)
                {
                    // Log.Error($"DoAttackArea selectHandle == null [{actionCfg_AttackArea.Id}]");
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

            bool isCriticalStrike = ChkIsCriticalStrike(unit, actionCfg_AttackArea.DamageInfo_Ref);
            actionContext.isCriticalStrike = isCriticalStrike;

            for (int i = 0; i < count; i++)
            {
                Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);

                ActionContext actionContextNew = actionContext;
                actionContextNew.defenderUnitId = targetUnit.Id;
                EventSystem.Instance.Publish(unit.DomainScene(), new AbilityTriggerEventType.UnitOnHit()
                {
                    actionContext = actionContextNew,
                    attackerUnit = unit,
                    defenderUnit = targetUnit,
                });

            }

            SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(unit);
            foreach (AttackActionCall attackActionCall in actionCfg_AttackArea.SelfAttackActionCall)
            {
                bool bRetChk = ET.Ability.ActionHandlerHelper.ChkActionCondition(unit, attackActionCall.ChkCondition1, attackActionCall.ChkCondition2, attackActionCall.ChkCondition1SelectObj_Ref, attackActionCall.ChkCondition2SelectObj_Ref, ref actionContext);
                if (bRetChk == false)
                {
                    continue;
                }

                SelectHandle curSelectHandle = selectHandleSelf;

                bool bRet = ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(unit, unit, attackActionCall.DelayTime, attackActionCall.ActionIds, attackActionCall.FilterCondition1, attackActionCall.FilterCondition2, curSelectHandle, null, ref actionContext);
            }

            Unit nearActor = unit.GetCasterNearActor();
            if (nearActor != null)
            {
                SelectHandle selectHandleSelfActor = SelectHandleHelper.CreateUnitSelfSelectHandle(nearActor);
                foreach (AttackActionCall attackActionCall in actionCfg_AttackArea.SelfActorAttackActionCall)
                {
                    bool bRetChk = ET.Ability.ActionHandlerHelper.ChkActionCondition(unit, attackActionCall.ChkCondition1, attackActionCall.ChkCondition2, attackActionCall.ChkCondition1SelectObj_Ref, attackActionCall.ChkCondition2SelectObj_Ref, ref actionContext);
                    if (bRetChk == false)
                    {
                        continue;
                    }

                    SelectHandle curSelectHandle = selectHandleSelfActor;

                    bool bRet = ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(unit, unit, attackActionCall.DelayTime, attackActionCall.ActionIds, attackActionCall.FilterCondition1, attackActionCall.FilterCondition2, curSelectHandle, null, ref actionContext);
                }
            }

            foreach (AttackActionCall attackActionCall in actionCfg_AttackArea.TargetAttackActionCall)
            {
                foreach (long targetUnitId in selectHandle.unitIds)
                {
                    Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), targetUnitId);

                    ActionContext actionContextNew = actionContext;
                    actionContextNew.defenderUnitId = targetUnit.Id;

                    bool bRetChk = ET.Ability.ActionHandlerHelper.ChkActionCondition(targetUnit, attackActionCall.ChkCondition1, attackActionCall.ChkCondition2, attackActionCall.ChkCondition1SelectObj_Ref, attackActionCall.ChkCondition2SelectObj_Ref, ref actionContextNew);

                    //Log.Error($"--zpb TargetAttackActionCall 11 unit[{unit.Id}] bRetChk[{bRetChk}] attackActionCall[{attackActionCall.ActionId[0]}]");
                    if (bRetChk == false)
                    {
                        continue;
                    }

                    SelectHandle curSelectHandle = SelectHandleHelper.CreateUnitSelfSelectHandle(targetUnit);

                    Unit resetPosByUnitNew = targetUnit;
                    bool bRet = ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(unit, unit, attackActionCall.DelayTime, attackActionCall.ActionIds, attackActionCall.FilterCondition1, attackActionCall.FilterCondition2, curSelectHandle, resetPosByUnitNew, ref actionContextNew);

                    //Log.Error($"--zpb TargetAttackActionCall 22 unit[{unit.Id}] bRet[{bRet}] attackActionCall[{attackActionCall.ActionId[0]}]");
                }
            }

            if (string.IsNullOrEmpty(actionCfg_AttackArea.DamageInfo) == false)
            {
                await DoDamage(unit, actionCfg_AttackArea.DamageInfo_Ref, selectHandle, actionCfg_AttackArea.DamageAllot, isCriticalStrike, actionContext);
            }
        }

        public static async ETTask DoDamage(Unit unit, ActionCfg_DamageUnit actionCfg_DamageUnit, SelectHandle selectHandle, DamageAllot damageAllot, bool isCriticalStrike, ActionContext actionContext)
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
                Damage damage = CreateDamage(unit, targetUnit, actionCfg_DamageUnit, isCriticalStrike, ref actionContext);
                if (damageAllot is DamageAllotChg damageAllotChg)
                {
                    damage *= damageScale;
                    damageScale *= ((100 + damageAllotChg.WeightChg) * 0.01f);
                }
                else
                {
                    damage *= damageScale;
                }

                DamageObj damageObj = CreateDamageObj(unit, targetUnit, damage, isCriticalStrike, ref actionContext);
                damageObj.SetDamageShowType(actionCfg_DamageUnit.Id);
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

        public static Damage CreateDamage(Unit attackerUnit, Unit targetUnit, ActionCfg_DamageUnit actionCfg_DamageUnit, bool isCriticalStrike, ref ActionContext actionContext)
        {
            float damageValue = 0;
            ET.AbilityConfig.DamageInfo damageInfo = actionCfg_DamageUnit.DamageInfo;
            NumericComponent attackNumeric = attackerUnit.GetComponent<NumericComponent>();
            NumericComponent targetNumeric = targetUnit.GetComponent<NumericComponent>();
            switch (damageInfo.DamageType)
            {
                case DamageType.FixedBlood:
                    long curPhysicalAttackBase = attackNumeric.GetAsLong(NumericType.PhysicalAttackBase);
                    long newPhysicalAttackBase = (long)(damageInfo.Value * 10000);
                    attackNumeric.SetNoEvent(NumericType.PhysicalAttackBase, newPhysicalAttackBase);

                    damageValue = attackNumeric.GetAsFloat(NumericType.PhysicalAttack);
                    attackNumeric.SetNoEvent(NumericType.PhysicalAttackBase, curPhysicalAttackBase);

                    break;
                case DamageType.PercentTotalBloodAttacker:
                    damageValue = attackNumeric.GetAsFloat(NumericType.MaxHp) * damageInfo.Value;
                    break;
                case DamageType.PercentTotalBloodBeHurter:
                    damageValue = targetNumeric.GetAsFloat(NumericType.MaxHp) * damageInfo.Value;
                    break;
                case DamageType.PercentCurBloodAttacker:
                    damageValue = attackNumeric.GetAsFloat(NumericType.Hp) * damageInfo.Value;
                    break;
                case DamageType.PercentCurBloodBeHurter:
                    damageValue = targetNumeric.GetAsFloat(NumericType.Hp) * damageInfo.Value;
                    break;
                case DamageType.PropertyBlood:
                    damageValue = attackNumeric.GetAsFloat(NumericType.PhysicalAttack) * damageInfo.Value;
                    break;
                case DamageType.LastSelectBlood:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ChgDamageByProperty(attackerUnit, targetUnit, ref damageValue, isCriticalStrike, damageInfo.DamageAttrType);
            ChgDamageByNumeric(attackerUnit, targetUnit, ref damageValue, ref actionContext);
            ChgDamageByScaleDisOrHeight(attackerUnit,  targetUnit, damageInfo, ref damageValue);

            float valueLimit = damageInfo.ValueLimit;
            if (valueLimit != -1)
            {
                if (damageValue > 0)
                {
                    if (valueLimit < 0)
                    {
                        Log.Error($"--actionCfg_DamageUnit[{actionCfg_DamageUnit.Id}] damageValue[{damageValue}] > 0 but damageInfo.ValueLimit[{valueLimit}] < 0");
                    }
                    else
                    {
                        if (damageValue > valueLimit)
                        {
                            damageValue = valueLimit;
                        }
                    }
                }
                else
                {
                    if (damageValue < 0)
                    {
                        if (valueLimit > 0)
                        {
                            Log.Error($"--actionCfg_DamageUnit[{actionCfg_DamageUnit.Id}] damageValue[{damageValue}] < 0 but damageInfo.ValueLimit[{valueLimit}] > 0");
                        }
                        else
                        {
                            if (damageValue < valueLimit)
                            {
                                damageValue = valueLimit;
                            }
                        }
                    }
                }
            }
            Damage damage = new(NumericType.PhysicalAttack, damageValue);
            return damage;
        }

        public static void ChgDamageByScaleDisOrHeight(Unit attackerUnit, Unit targetUnit, ET.AbilityConfig.DamageInfo damageInfo, ref float damageValue)
        {
            NumericComponent attackNumeric = attackerUnit.GetComponent<NumericComponent>();

            float scaleByDis = damageInfo.ScaleByDis;

            long newPhysicalAttackScaleByDisBase = (long)(scaleByDis * 10000);
            attackNumeric.SetNoEvent(NumericType.PhysicalAttackScaleByDisBase, newPhysicalAttackScaleByDisBase);
            scaleByDis = attackNumeric.GetAsFloat(NumericType.PhysicalAttackScaleByDis);

            if (scaleByDis != 0)
            {
                Unit casterActorUnit = attackerUnit.GetCasterNearActor();
                if (casterActorUnit == null)
                {
                    return;
                }
                //按照距离变化(>0表示越远伤害越高,<0表示越近伤害越高)
                float dis = math.length(casterActorUnit.Position - targetUnit.Position);
                if (scaleByDis > 0)
                {
                    damageValue *= (1 + math.min(dis * math.abs(scaleByDis), math.abs(damageInfo.ScaleByDisMax))*0.01f);
                }
                else
                {
                    damageValue *= (1 + math.max(math.abs(damageInfo.ScaleByDisMax) - dis * math.abs(scaleByDis), 0)*0.01f);
                }
            }

            float scaleByHeight = damageInfo.ScaleByHeight;

            long newPhysicalAttackScaleByHeightBase = (long)(scaleByHeight * 10000);
            attackNumeric.SetNoEvent(NumericType.PhysicalAttackScaleByHeightBase, newPhysicalAttackScaleByHeightBase);
            scaleByHeight = attackNumeric.GetAsFloat(NumericType.PhysicalAttackScaleByHeight);

            if (scaleByHeight != 0)
            {
                Unit casterActorUnit = attackerUnit.GetCasterNearActor();
                if (casterActorUnit == null)
                {
                    return;
                }
                //按照高度变化(>0表示往上越远伤害越高,<0表示往下越远伤害越高)
                if (
                    (scaleByHeight > 0 && casterActorUnit.Position.y < targetUnit.Position.y)
                    || (scaleByHeight < 0 && casterActorUnit.Position.y > targetUnit.Position.y)
                    )
                {
                    float disHeight = math.abs(casterActorUnit.Position.y - targetUnit.Position.y);
                    damageValue *= (1 + math.min(disHeight * math.abs(scaleByHeight), math.abs(damageInfo.ScaleByHeightMax))*0.01f);
                }
            }
        }

        public static void ChgDamageByProperty(Unit attackerUnit, Unit targetUnit, ref float damageValue, bool isCriticalStrike, DamageAttrType damageAttrType)
        {
            NumericComponent attackNumeric = attackerUnit.GetComponent<NumericComponent>();
            NumericComponent targetNumeric = targetUnit.GetComponent<NumericComponent>();

            //attacker_Attack = math.max(0, attacker_Attack);
            float attacker_DamageDeepening = attackNumeric.GetAsFloat(NumericType.DamageDeepening);
            //attacker_DamageDeepening = math.max(0, attacker_DamageDeepening);
            float target_DamageRelief = 0;
            switch (damageAttrType)
            {
                case DamageAttrType.Physical:
                    target_DamageRelief = targetNumeric.GetAsFloat(NumericType.DamageReliefWhenPhysical);
                    break;
                case DamageAttrType.Magic:
                    target_DamageRelief = targetNumeric.GetAsFloat(NumericType.DamageReliefWhenMagic);
                    break;
                case DamageAttrType.Real:
                    target_DamageRelief = 0;
                    break;
            }
            //target_DamageRelief = math.clamp(target_DamageRelief, 0, 100);

            if (isCriticalStrike)
            {
                float attacker_CriticalHitDamage = attackNumeric.GetAsFloat(NumericType.CriticalHitDamage);
                attacker_CriticalHitDamage = math.max(0, attacker_CriticalHitDamage);
                damageValue *= ((100 + attacker_CriticalHitDamage) * 0.01f);
            }
            damageValue *= ((100 + attacker_DamageDeepening) * 0.01f);
            damageValue *= math.max(0, (100 - target_DamageRelief) * 0.01f);
        }

        public static void ChgDamageByNumeric(Unit attackerUnit, Unit targetUnit, ref float damageValue, ref ActionContext actionContext)
        {
            string buffCfgId = actionContext.buffCfgId;
            if (string.IsNullOrEmpty(buffCfgId))
            {
                return;
            }

            NumericComponent attackerNumeric = attackerUnit.GetComponent<NumericComponent>();
            if (attackerNumeric == null)
            {
                return;
            }
            NumericComponent targetNumeric = targetUnit.GetComponent<NumericComponent>();
            if (targetNumeric == null)
            {
                return;
            }

            BuffCfg buffCfg = BuffCfgCategory.Instance.Get(buffCfgId);
            if (buffCfg.BuffType != BuffType.None)
            {
                long newDamageValueBase = (long)(damageValue * 10000);
                attackerNumeric.SetNoEvent(NumericType.BuffDamageModifyBase, newDamageValueBase);
                damageValue = attackerNumeric.GetAsFloat(NumericType.BuffDamageModify);

                newDamageValueBase = (long)(damageValue * 10000);
                targetNumeric.SetNoEvent(NumericType.BuffBeDamageModifyBase, newDamageValueBase);
                damageValue = targetNumeric.GetAsFloat(NumericType.BuffBeDamageModify);
            }
        }

        public static DamageObj CreateDamageObj(Unit unit, Unit targetUnit, Damage damage, bool isCrit, ref ActionContext actionContext)
        {
            Scene scene = unit.DomainScene();
            return scene.GetComponent<DamageComponent>().Add(unit, targetUnit, damage, isCrit, ref actionContext);
        }

        public static void ChgDamageObj(Unit unit, ActionCfg_DamageChg _ActionCfg_DamageChg, ref ActionContext actionContext)
        {
            Scene scene = unit.DomainScene();
            scene.GetComponent<DamageComponent>().ChgDamageObj(_ActionCfg_DamageChg, ref actionContext);
        }

        public static void DoDamageQuick(Unit unit, ActionCfg_DamageUnit actionCfg_DamageUnit, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            if (selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                Log.Error($"ET.Ability.DamageHelper.DoDamageQuick selectHandle.selectHandleType[{selectHandle.selectHandleType}] != SelectHandleType.SelectUnits");
                return;
            }

            int count = selectHandle.unitIds.Count;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);
                if (UnitHelper.ChkUnitAlive(targetUnit, true) == false)
                {
                    continue;
                }
                Damage damage = CreateDamage(unit, targetUnit, actionCfg_DamageUnit, false, ref actionContext);
                CreateDamageObjQuick(targetUnit, damage, ref actionContext, actionCfg_DamageUnit.Id);
            }
        }

        public static void CreateDamageObjQuick(Unit targetUnit, Damage damage, ref ActionContext actionContext, string actionCfgDamageUnitId)
        {
            Scene scene = targetUnit.DomainScene();
            scene.GetComponent<DamageComponent>().DealWithDamageQuick(targetUnit, damage, ref actionContext, actionCfgDamageUnitId);
        }

    }
}