using System;
using System.Collections.Generic;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class UnitConditionHandleHelper
    {
        public static (bool bPass, bool isChgSelect, SelectHandle newSelectHandle) ChkCondition(Unit unit, SelectHandle selectHandle, List<SequenceUnitCondition> sequenceUnitConditions, ref ActionContext actionContext)
        {
            if (sequenceUnitConditions.Count == 0)
            {
                return (true, false, null);
            }
            if (selectHandle.selectHandleType is SelectHandleType.SelectDirection)
            {
                return (false, false, null);
            }
            else if (selectHandle.selectHandleType is SelectHandleType.SelectPosition)
            {
                return (false, false, null);
            }
            else if (selectHandle.selectHandleType is SelectHandleType.SelectUnits)
            {
                bool bRet = false;
                int count = selectHandle.unitIds.Count;
                bool isChgSelect = false;
                SelectHandle newSelectHandle = SelectHandleHelper.CreateUnitNoneSelectHandle();
                newSelectHandle.position = selectHandle.position;
                newSelectHandle.direction = selectHandle.direction;
                for (int i = count - 1; i >= 0; i--)
                {
                    long unitId = selectHandle.unitIds[i];
                    Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitSelect, true) == false)
                    {
                        continue;
                    }
                    actionContext.defenderUnitId = unitSelect.Id;
                    bool bRetOne = ChkCondition(unitSelect, sequenceUnitConditions, ref actionContext);
                    if (bRetOne == false)
                    {
                        isChgSelect = true;
                    }
                    else
                    {
                        newSelectHandle.unitIds.Add(unitId);
                    }
                }

                if (newSelectHandle.unitIds.Count > 0)
                {
                    bRet = true;
                }
                else
                {
                    newSelectHandle.Dispose();
                }
                if (bRet)
                {
                    return (true, isChgSelect, newSelectHandle);
                }
                return (false, isChgSelect, newSelectHandle);
            }
            return (false, false, null);
        }

        public static bool ChkCondition(Unit unit, List<SequenceUnitCondition> sequenceUnitConditions, ref ActionContext actionContext)
        {
            if (sequenceUnitConditions.Count == 0)
            {
                return true;
            }
            for (int i = 0; i < sequenceUnitConditions.Count; i++)
            {
                SequenceUnitCondition sequenceUnitCondition = sequenceUnitConditions[i];
                bool bRetSub = true;
                for (int j = 0; j < sequenceUnitCondition.Conditions.Count; j++)
                {
                    UnitConditionBase unitCondition = sequenceUnitCondition.Conditions[j];
                    bool bRet = ChkConditionOne(unit, unitCondition, ref actionContext);
                    if (bRet == false)
                    {
                        bRetSub = false;
                        break;
                    }
                }
                if (bRetSub)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ChkCompare(string value, ConditionCompare conditionCompare, string conditionValue)
        {
            if (conditionCompare == ConditionCompare.eq)
            {
                return value == conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ne)
            {
                return value != conditionValue;
            }

            return false;
        }

        public static bool ChkCompare(float value, ConditionCompare conditionCompare, float conditionValue)
        {
            if (conditionCompare == ConditionCompare.gt)
            {
                return value > conditionValue;
            }
            else if (conditionCompare == ConditionCompare.eq)
            {
                return value == conditionValue;
            }
            else if (conditionCompare == ConditionCompare.lt)
            {
                return value < conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ge)
            {
                return value >= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.le)
            {
                return value <= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ne)
            {
                return value != conditionValue;
            }

            return false;
        }

        public static bool ChkCompare(int value, ConditionCompare conditionCompare, int conditionValue)
        {
            if (conditionCompare == ConditionCompare.gt)
            {
                return value > conditionValue;
            }
            else if (conditionCompare == ConditionCompare.eq)
            {
                return value == conditionValue;
            }
            else if (conditionCompare == ConditionCompare.lt)
            {
                return value < conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ge)
            {
                return value >= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.le)
            {
                return value <= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ne)
            {
                return value != conditionValue;
            }

            return false;
        }

        public static bool ChkCompare(int value, ConditionCompare conditionCompare, long conditionValue)
        {
            if (conditionCompare == ConditionCompare.gt)
            {
                return value > conditionValue;
            }
            else if (conditionCompare == ConditionCompare.eq)
            {
                return value == conditionValue;
            }
            else if (conditionCompare == ConditionCompare.lt)
            {
                return value < conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ge)
            {
                return value >= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.le)
            {
                return value <= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ne)
            {
                return value != conditionValue;
            }

            return false;
        }

        public static bool ChkConditionOne(Unit unit, UnitConditionBase unitCondition, ref ActionContext actionContext)
        {
            if (unitCondition is BuffStackCountCondition buffStackCountCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, ref actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                return ChkCompare(buffObj.stack, unitCondition.ConditionCompare, buffStackCountCondition.StackCount);
            }
            else if (unitCondition is BuffStackCountRecordCondition buffStackCountRecordCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, ref actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                float recordIntValue = RecordHandleHelper.GetRecordInt(unit, buffStackCountRecordCondition.RecordKey);
                return ChkCompare(buffObj.stack, unitCondition.ConditionCompare, recordIntValue);
            }
            else if (unitCondition is BuffPassTimeCondition buffPassTimeCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, ref actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                bool isPercent = buffPassTimeCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(buffObj.timeElapsed, unitCondition.ConditionCompare, buffPassTimeCondition.PassTime);
                }
                else
                {
                    return ChkCompare(buffObj.timeElapsed/buffObj.orgDuration, unitCondition.ConditionCompare, buffPassTimeCondition.PassTime);
                }
            }
            else if (unitCondition is BuffPassTimeRecordCondition buffPassTimeRecordCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, ref actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                float recordIntValue = RecordHandleHelper.GetRecordInt(unit, buffPassTimeRecordCondition.RecordKey);
                bool isPercent = buffPassTimeRecordCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(buffObj.timeElapsed, unitCondition.ConditionCompare, recordIntValue);
                }
                else
                {
                    return ChkCompare(buffObj.timeElapsed/buffObj.orgDuration, unitCondition.ConditionCompare, recordIntValue);
                }
            }
            else if (unitCondition is BuffLeftTimeCondition buffLeftTimeCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, ref actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                bool isPercent = buffLeftTimeCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(buffObj.duration, unitCondition.ConditionCompare, buffLeftTimeCondition.LeftTime);
                }
                else
                {
                    return ChkCompare(buffObj.duration/buffObj.orgDuration, unitCondition.ConditionCompare, buffLeftTimeCondition.LeftTime);
                }
            }
            else if (unitCondition is BuffLeftTimeRecordCondition buffLeftTimeRecordCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, ref actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                float recordIntValue = RecordHandleHelper.GetRecordInt(unit, buffLeftTimeRecordCondition.RecordKey);
                bool isPercent = buffLeftTimeRecordCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(buffObj.duration, unitCondition.ConditionCompare, recordIntValue);
                }
                else
                {
                    return ChkCompare(buffObj.duration/buffObj.orgDuration, unitCondition.ConditionCompare, recordIntValue);
                }
            }
            else if (unitCondition is BuffCfgIdCondition buffCfgIdCondition)
            {
                string buffCfgId = buffCfgIdCondition.BuffCfgId;
                bool buffObjExist = BuffHelper.ChkBuffByBuffCfgId(unit, buffCfgId);

                if (unitCondition.ConditionCompare == ConditionCompare.eq)
                {
                    if (buffObjExist)
                    {
                        return true;
                    }
                    return false;
                }
                else if (unitCondition.ConditionCompare == ConditionCompare.ne)
                {
                    if (buffObjExist == false)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else if (unitCondition is BuffTypeCondition buffTypeCondition)
            {
                BuffType buffType = buffTypeCondition.BuffType;
                bool buffObjExist = BuffHelper.ChkBuffByBuffType(unit, buffType);

                if (unitCondition.ConditionCompare == ConditionCompare.eq)
                {
                    if (buffObjExist)
                    {
                        return true;
                    }
                    return false;
                }
                else if (unitCondition.ConditionCompare == ConditionCompare.ne)
                {
                    if (buffObjExist == false)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else if (unitCondition is BuffTagTypeCondition buffTagTypeCondition)
            {
                BuffTagType buffTagType = buffTagTypeCondition.BuffTagType;
                bool buffObjExist = BuffHelper.ChkBuffByTagType(unit, buffTagType);

                if (unitCondition.ConditionCompare == ConditionCompare.eq)
                {
                    if (buffObjExist)
                    {
                        return true;
                    }
                    return false;
                }
                else if (unitCondition.ConditionCompare == ConditionCompare.ne)
                {
                    if (buffObjExist == false)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else if (unitCondition is BuffTagGroupTypeCondition buffTagGroupTypeCondition)
            {
                BuffTagGroupType buffTagGroupType = buffTagGroupTypeCondition.BuffTagGroupType;
                bool buffObjExist = BuffHelper.ChkBuffByTagGroupType(unit, buffTagGroupType);

                if (unitCondition.ConditionCompare == ConditionCompare.eq)
                {
                    if (buffObjExist)
                    {
                        return true;
                    }
                    return false;
                }
                else if (unitCondition.ConditionCompare == ConditionCompare.ne)
                {
                    if (buffObjExist == false)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else if (unitCondition is SkillIdCondition skillIdCondition)
            {
                string skillCfgId = actionContext.skillCfgId;
                if (ChkCompare(skillIdCondition.SkillId, unitCondition.ConditionCompare, skillCfgId))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is SkillSlotTypeCondition skillSlotTypeCondition)
            {
                if (skillSlotTypeCondition.SkillSlotType == SkillSlotType.None)
                {
                    return true;
                }
                SkillSlotType skillSlotType = actionContext.skillSlotType;
                int skillSlotIndex = actionContext.skillSlotIndex;
                if (ChkCompare((int)skillSlotTypeCondition.SkillSlotType, unitCondition.ConditionCompare, (int)skillSlotType))
                {
                    if (skillSlotTypeCondition.SkillSlotIndex == -1)
                    {
                        return true;
                    }
                    else
                    {
                        if (ChkCompare(skillSlotTypeCondition.SkillSlotIndex, unitCondition.ConditionCompare, skillSlotIndex))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return false;
            }
            else if (unitCondition is SkillGroupTypeCondition skillGroupTypeCondition)
            {
                SkillGroupType skillGroupType = actionContext.skillGroupType;
                if (ChkCompare((int)skillGroupTypeCondition.SkillGroupType, unitCondition.ConditionCompare, (int)skillGroupType))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is AttributeCondition attributeCondition)
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    return false;
                }
                float numericValue = numericComponent.GetAsFloat((int)attributeCondition.NumericType);
                return ChkCompare(numericValue, unitCondition.ConditionCompare, attributeCondition.Value);
            }
            else if (unitCondition is AttributeRecordCondition attributeRecordCondition)
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    return false;
                }
                float numericValue = numericComponent.GetAsFloat((int)attributeRecordCondition.NumericType);
                float recordIntValue = RecordHandleHelper.GetRecordInt(unit, attributeRecordCondition.RecordKey);
                return ChkCompare(numericValue, unitCondition.ConditionCompare, recordIntValue);
            }
            else if (unitCondition is CurHpCondition curHpCondition)
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    return false;
                }
                float maxHp = numericComponent.GetAsFloat(NumericType.MaxHp);
                float curHp = numericComponent.GetAsFloat(NumericType.Hp);
                bool isPercent = curHpCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(curHp, unitCondition.ConditionCompare, curHpCondition.Value);
                }
                else
                {
                    return ChkCompare(curHp/maxHp, unitCondition.ConditionCompare, curHpCondition.Value);
                }
            }
            else if (unitCondition is CurHpRecordCondition curHpRecordCondition)
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    return false;
                }
                float maxHp = numericComponent.GetAsFloat(NumericType.MaxHp);
                float curHp = numericComponent.GetAsFloat(NumericType.Hp);

                float recordIntValue = RecordHandleHelper.GetRecordInt(unit, curHpRecordCondition.RecordKey);
                bool isPercent = curHpRecordCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(curHp, unitCondition.ConditionCompare, recordIntValue);
                }
                else
                {
                    return ChkCompare(curHp/maxHp, unitCondition.ConditionCompare, recordIntValue);
                }
            }
            else if (unitCondition is OwerCountCondition owerCountCondition)
            {
                int count = ET.Ability.OwnCallerHelper.GetOwnCallerCount(unit, owerCountCondition.OwnActor, owerCountCondition.OwnBullet, owerCountCondition.OwnActor);

                return ChkCompare(count, unitCondition.ConditionCompare, owerCountCondition.Value);
            }
            else if (unitCondition is OwerCountRecordCondition owerCountRecordCondition)
            {
                int count = ET.Ability.OwnCallerHelper.GetOwnCallerCount(unit, owerCountRecordCondition.OwnActor, owerCountRecordCondition.OwnBullet, owerCountRecordCondition.OwnActor);

                float recordIntValue = RecordHandleHelper.GetRecordInt(unit, owerCountRecordCondition.RecordKey);
                return ChkCompare(count, unitCondition.ConditionCompare, recordIntValue);
            }
            else if (unitCondition is RecordIntCondition recordIntCondition)
            {
                float curValue = RecordHandleHelper.GetRecordInt(unit, recordIntCondition.RecordKey);
                if (ChkCompare(curValue, unitCondition.ConditionCompare, recordIntCondition.Value))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is RecordIntRecordCondition recordIntRecordCondition)
            {
                float recordIntValue1 = RecordHandleHelper.GetRecordInt(unit, recordIntRecordCondition.RecordKey1);
                float recordIntValue2 = RecordHandleHelper.GetRecordInt(unit, recordIntRecordCondition.RecordKey2);
                if (ChkCompare(recordIntValue1, unitCondition.ConditionCompare, recordIntValue2))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is RecordStringCondition recordStringCondition)
            {
                string curValue = RecordHandleHelper.GetRecordString(unit, recordStringCondition.RecordKey);
                if (ChkCompare(curValue, unitCondition.ConditionCompare, recordStringCondition.Value))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is RecordStringRecordCondition recordStringRecordCondition)
            {
                string recordStringValue1 = RecordHandleHelper.GetRecordString(unit, recordStringRecordCondition.RecordKey1);
                string recordStringValue2 = RecordHandleHelper.GetRecordString(unit, recordStringRecordCondition.RecordKey2);
                if (ChkCompare(recordStringValue1, unitCondition.ConditionCompare, recordStringValue2))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is OnHitChkCanBeControlCondition onHitChkCanBeControlCondition)
            {
                bool beControl = BuffHelper.ChkCanBeControl(unit.DomainScene(), ref actionContext);
                return onHitChkCanBeControlCondition.BeControl == beControl;
            }
            else if (unitCondition is OnHitChkIsCriticalStrikeCondition onHitChkIsCriticalStrikeCondition)
            {
                return onHitChkIsCriticalStrikeCondition.IsCriticalStrike == actionContext.isCriticalStrike;
            }
            else if (unitCondition is OnChkCanHitByBulletCondition onChkCanHitByBulletCondition)
            {
                Unit unitAction = UnitHelper.GetUnit(unit.DomainScene(), actionContext.attackerUnitId);
                if (UnitHelper.ChkIsBullet(unitAction) == false)
                {
                    return false;
                }

                BulletObj bulletObj = unitAction.GetComponent<BulletObj>();
                bool canHit = bulletObj.CanHitUnit(unit);
                return onChkCanHitByBulletCondition.CanHit == canHit;
            }
            else if (unitCondition is ChkSelectUnitNumCondition chkSelectUnitNumCondition)
            {
                int curValue = actionContext.selectUnitNum;
                if (ChkCompare(curValue, unitCondition.ConditionCompare, chkSelectUnitNumCondition.Num))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is ChkSelectUnitNumRecordCondition chkSelectUnitNumRecordCondition)
            {
                float recordIntValue = RecordHandleHelper.GetRecordInt(unit, chkSelectUnitNumRecordCondition.RecordKey);
                int selectUnitNum = actionContext.selectUnitNum;
                if (ChkCompare(selectUnitNum, unitCondition.ConditionCompare, recordIntValue))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is AngleCondition angleCondition)
            {
                Unit curUnit = UnitHelper.GetUnit(unit.DomainScene(), actionContext.unitId);
                if (curUnit == null)
                {
                    return false;
                }
                float curRadian = UnitHelper.GetTargetUnitRadian(curUnit, unit);
                float curAngle = math.abs(curRadian * 180 / math.PI);
                if (ChkCompare(curAngle, unitCondition.ConditionCompare, angleCondition.Angle))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is AngleRecordCondition angleRecordCondition)
            {
                Unit curUnit = UnitHelper.GetUnit(unit.DomainScene(), actionContext.unitId);
                if (curUnit == null)
                {
                    return false;
                }
                float curRadian = UnitHelper.GetTargetUnitRadian(curUnit, unit);
                float curAngle = math.abs(curRadian * 180 / math.PI);

                float recordIntValue = RecordHandleHelper.GetRecordInt(unit, angleRecordCondition.RecordKey);
                if (ChkCompare(curAngle, unitCondition.ConditionCompare, recordIntValue))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is DisCondition disCondition)
            {
                Unit curUnit = UnitHelper.GetUnit(unit.DomainScene(), actionContext.unitId);
                if (curUnit == null)
                {
                    return false;
                }
                float curDisSqr = UnitHelper.GetTargetUnitDisSqr(curUnit, unit);
                if (ChkCompare(curDisSqr, unitCondition.ConditionCompare, disCondition.Dis * disCondition.Dis))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is DisRecordCondition disRecordCondition)
            {
                Unit curUnit = UnitHelper.GetUnit(unit.DomainScene(), actionContext.unitId);
                if (curUnit == null)
                {
                    return false;
                }
                float curDisSqr = UnitHelper.GetTargetUnitDisSqr(curUnit, unit);

                float recordIntValue = RecordHandleHelper.GetRecordInt(unit, disRecordCondition.RecordKey);
                if (ChkCompare(curDisSqr, unitCondition.ConditionCompare, recordIntValue * recordIntValue))
                {
                    return true;
                }
                return false;
            }
            else if (unitCondition is ProbabilityCondition probabilityCondition)
            {
                int random = RandomGenerator.RandomNumber(0, 100);
                return ChkCompare(random, unitCondition.ConditionCompare, probabilityCondition.Value);
            }
            else if (unitCondition is ProbabilityRecordCondition probabilityRecordCondition)
            {
                int random = RandomGenerator.RandomNumber(0, 100);
                float recordIntValue = RecordHandleHelper.GetRecordInt(unit, probabilityRecordCondition.RecordKey);
                return ChkCompare(random, unitCondition.ConditionCompare, recordIntValue);
            }
            return false;
        }
    }
}