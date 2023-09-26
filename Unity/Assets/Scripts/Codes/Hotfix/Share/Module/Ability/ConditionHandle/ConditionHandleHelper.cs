using System;
using System.Collections.Generic;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class ConditionHandleHelper
    {
        public static (bool bPass, bool isChgSelect, SelectHandle newSelectHandle) ChkCondition(Unit unit, SelectHandle selectHandle, List<SubCondition>
        conditions, ActionContext
         actionContext)
        {
            if (conditions.Count == 0)
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
                SelectHandle newSelectHandle = null;
                for (int i = count - 1; i >= 0; i--)
                {
                    Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);
                    if (UnitHelper.ChkUnitAlive(unitSelect, true) == false)
                    {
                        continue;
                    }
                    actionContext.defenderUnitId = unitSelect.Id;
                    bool bRetOne = ChkCondition(unitSelect, conditions, actionContext);
                    if (bRetOne == false)
                    {
                        isChgSelect = true;
                        if (newSelectHandle == null)
                        {
                            newSelectHandle = SelectHandleHelper.CreateUnitNoneSelectHandle();
                            newSelectHandle.position = selectHandle.position;
                            newSelectHandle.direction = selectHandle.direction;
                        }
                        newSelectHandle.unitIds.Add(i);
                    }
                    else
                    {
                        bRet = true;
                    }
                }
                if (bRet)
                {
                    return (true, isChgSelect, newSelectHandle);
                }
                return (false, isChgSelect, newSelectHandle);
            }
            return (false, false, null);
        }

        public static bool ChkCondition(Unit unit, List<SubCondition> conditions, ActionContext actionContext)
        {
            if (conditions.Count == 0)
            {
                return true;
            }
            for (int i = 0; i < conditions.Count; i++)
            {
                SubCondition subCondition = conditions[i];
                bool bRetSub = true;
                for (int j = 0; j < subCondition.Conditions.Count; j++)
                {
                    Condition condition = subCondition.Conditions[j];
                    bool bRet = ChkConditionOne(unit, condition, actionContext);
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

        public static bool ChkConditionOne(Unit unit, Condition condition, ActionContext actionContext)
        {
            if (condition is BuffStackCountCondition buffStackCountCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                return ChkCompare(buffObj.stack, condition.ConditionCompare, buffStackCountCondition.StackCount);
            }
            else if (condition is BuffStackCountRecordCondition buffStackCountRecordCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                int recordIntValue = RecordHandleHelper.GetRecordInt(unit, buffStackCountRecordCondition.RecordKey);
                return ChkCompare(buffObj.stack, condition.ConditionCompare, recordIntValue);
            }
            else if (condition is BuffPassTimeCondition buffPassTimeCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                bool isPercent = buffPassTimeCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(buffObj.timeElapsed, condition.ConditionCompare, buffPassTimeCondition.PassTime);
                }
                else
                {
                    return ChkCompare(buffObj.timeElapsed/buffObj.orgDuration, condition.ConditionCompare, buffPassTimeCondition.PassTime);
                }
            }
            else if (condition is BuffPassTimeRecordCondition buffPassTimeRecordCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                int recordIntValue = RecordHandleHelper.GetRecordInt(unit, buffPassTimeRecordCondition.RecordKey);
                bool isPercent = buffPassTimeRecordCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(buffObj.timeElapsed, condition.ConditionCompare, recordIntValue);
                }
                else
                {
                    return ChkCompare(buffObj.timeElapsed/buffObj.orgDuration, condition.ConditionCompare, recordIntValue);
                }
            }
            else if (condition is BuffLeftTimeCondition buffLeftTimeCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                bool isPercent = buffLeftTimeCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(buffObj.duration, condition.ConditionCompare, buffLeftTimeCondition.LeftTime);
                }
                else
                {
                    return ChkCompare(buffObj.duration/buffObj.orgDuration, condition.ConditionCompare, buffLeftTimeCondition.LeftTime);
                }
            }
            else if (condition is BuffLeftTimeRecordCondition buffLeftTimeRecordCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                int recordIntValue = RecordHandleHelper.GetRecordInt(unit, buffLeftTimeRecordCondition.RecordKey);
                bool isPercent = buffLeftTimeRecordCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(buffObj.duration, condition.ConditionCompare, recordIntValue);
                }
                else
                {
                    return ChkCompare(buffObj.duration/buffObj.orgDuration, condition.ConditionCompare, recordIntValue);
                }
            }
            else if (condition is BuffIdCondition buffIdCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                string buffCfgId = buffObj.model.Id;
                if (ChkCompare(buffIdCondition.BuffId, condition.ConditionCompare, buffCfgId))
                {
                    return true;
                }
                return false;
            }
            else if (condition is BuffTagTypeCondition buffTagTypeCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                List<BuffTagType> tags = buffObj.model.Tags;
                if (condition.ConditionCompare == ConditionCompare.eq)
                {
                    foreach (BuffTagType buffTagType in tags)
                    {
                        if (ChkCompare((int)buffTagType, ConditionCompare.eq, (int)buffTagTypeCondition.BuffTagType))
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else if (condition.ConditionCompare == ConditionCompare.ne)
                {
                    foreach (BuffTagType buffTagType in tags)
                    {
                        if (ChkCompare((int)buffTagType, ConditionCompare.eq, (int)buffTagTypeCondition.BuffTagType))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (condition is BuffTypeCondition buffTypeCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                BuffType buffType = buffObj.model.BuffType;
                if (condition.ConditionCompare == ConditionCompare.eq)
                {
                    if (ChkCompare((int)buffType, ConditionCompare.eq, (int)buffTypeCondition.BuffType))
                    {
                        return true;
                    }
                    return false;
                }
                else if (condition.ConditionCompare == ConditionCompare.ne)
                {
                    if (ChkCompare((int)buffType, ConditionCompare.eq, (int)buffTypeCondition.BuffType))
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (condition is SkillIdCondition skillIdCondition)
            {
                string skillCfgId = actionContext.skillCfgId;
                if (ChkCompare(skillIdCondition.SkillId, condition.ConditionCompare, skillCfgId))
                {
                    return true;
                }
                return false;
            }
            else if (condition is SkillSlotTypeCondition skillSlotTypeCondition)
            {
                SkillSlotType skillSlotType = actionContext.skillSlotType;
                if (ChkCompare((int)skillSlotTypeCondition.SkillSlotType, condition.ConditionCompare, (int)skillSlotType))
                {
                    return true;
                }
                return false;
            }
            else if (condition is AttributeCondition attributeCondition)
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    return false;
                }
                float numericValue = numericComponent.Get((int)attributeCondition.NumericType);
                return ChkCompare(numericValue, condition.ConditionCompare, attributeCondition.Value);
            }
            else if (condition is AttributeRecordCondition attributeRecordCondition)
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    return false;
                }
                float numericValue = numericComponent.Get((int)attributeRecordCondition.NumericType);
                int recordIntValue = RecordHandleHelper.GetRecordInt(unit, attributeRecordCondition.RecordKey);
                return ChkCompare(numericValue, condition.ConditionCompare, recordIntValue);
            }
            else if (condition is CurHpCondition curHpCondition)
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
                    return ChkCompare(curHp, condition.ConditionCompare, curHpCondition.Value);
                }
                else
                {
                    return ChkCompare(curHp/maxHp, condition.ConditionCompare, curHpCondition.Value);
                }
            }
            else if (condition is CurHpRecordCondition curHpRecordCondition)
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    return false;
                }
                float maxHp = numericComponent.GetAsFloat(NumericType.MaxHp);
                float curHp = numericComponent.GetAsFloat(NumericType.Hp);

                int recordIntValue = RecordHandleHelper.GetRecordInt(unit, curHpRecordCondition.RecordKey);
                bool isPercent = curHpRecordCondition.IsPercent;
                if (isPercent == false)
                {
                    return ChkCompare(curHp, condition.ConditionCompare, recordIntValue);
                }
                else
                {
                    return ChkCompare(curHp/maxHp, condition.ConditionCompare, recordIntValue);
                }
            }
            else if (condition is RecordIntCondition recordIntCondition)
            {
                int curValue = RecordHandleHelper.GetRecordInt(unit, recordIntCondition.RecordKey);
                if (ChkCompare(curValue, condition.ConditionCompare, recordIntCondition.Value))
                {
                    return true;
                }
                return false;
            }
            else if (condition is RecordIntRecordCondition recordIntRecordCondition)
            {
                int recordIntValue1 = RecordHandleHelper.GetRecordInt(unit, recordIntRecordCondition.RecordKey1);
                int recordIntValue2 = RecordHandleHelper.GetRecordInt(unit, recordIntRecordCondition.RecordKey2);
                if (ChkCompare(recordIntValue1, condition.ConditionCompare, recordIntValue2))
                {
                    return true;
                }
                return false;
            }
            else if (condition is RecordStringCondition recordStringCondition)
            {
                string curValue = RecordHandleHelper.GetRecordString(unit, recordStringCondition.RecordKey);
                if (ChkCompare(curValue, condition.ConditionCompare, recordStringCondition.Value))
                {
                    return true;
                }
                return false;
            }
            else if (condition is RecordStringRecordCondition recordStringRecordCondition)
            {
                string recordStringValue1 = RecordHandleHelper.GetRecordString(unit, recordStringRecordCondition.RecordKey1);
                string recordStringValue2 = RecordHandleHelper.GetRecordString(unit, recordStringRecordCondition.RecordKey2);
                if (ChkCompare(recordStringValue1, condition.ConditionCompare, recordStringValue2))
                {
                    return true;
                }
                return false;
            }
            else if (condition is OnHitChkCanBeControlCondition onHitChkCanBeControlCondition)
            {
                bool beControl = BuffHelper.ChkCanBeControl(unit.DomainScene(), actionContext);
                return onHitChkCanBeControlCondition.BeControl == beControl;
            }
            else if (condition is ChkSelectUnitNumCondition chkSelectUnitNumCondition)
            {
                int curValue = actionContext.selectUnitNum;
                if (ChkCompare(curValue, condition.ConditionCompare, chkSelectUnitNumCondition.Num))
                {
                    return true;
                }
                return false;
            }
            else if (condition is ChkSelectUnitNumRecordCondition chkSelectUnitNumRecordCondition)
            {
                int recordIntValue = RecordHandleHelper.GetRecordInt(unit, chkSelectUnitNumRecordCondition.RecordKey);
                int selectUnitNum = actionContext.selectUnitNum;
                if (ChkCompare(selectUnitNum, condition.ConditionCompare, recordIntValue))
                {
                    return true;
                }
                return false;
            }
            else if (condition is AngleCondition angleCondition)
            {
                Unit curUnit = UnitHelper.GetUnit(unit.DomainScene(), actionContext.unitId);
                float curRadian = UnitHelper.GetTargetUnitRadian(curUnit, unit);
                float curAngle = math.abs(curRadian * 180 / math.PI);
                if (ChkCompare(curAngle, condition.ConditionCompare, angleCondition.Angle))
                {
                    return true;
                }
                return false;
            }
            else if (condition is AngleRecordCondition angleRecordCondition)
            {
                Unit curUnit = UnitHelper.GetUnit(unit.DomainScene(), actionContext.unitId);
                float curRadian = UnitHelper.GetTargetUnitRadian(curUnit, unit);
                float curAngle = math.abs(curRadian * 180 / math.PI);

                int recordIntValue = RecordHandleHelper.GetRecordInt(unit, angleRecordCondition.RecordKey);
                if (ChkCompare(curAngle, condition.ConditionCompare, recordIntValue))
                {
                    return true;
                }
                return false;
            }
            else if (condition is ProbabilityCondition probabilityCondition)
            {
                int random = RandomGenerator.RandomNumber(1, 100);
                return ChkCompare(random, condition.ConditionCompare, probabilityCondition.Value);
            }
            else if (condition is ProbabilityRecordCondition probabilityRecordCondition)
            {
                int random = RandomGenerator.RandomNumber(1, 100);
                int recordIntValue = RecordHandleHelper.GetRecordInt(unit, probabilityRecordCondition.RecordKey);
                return ChkCompare(random, condition.ConditionCompare, recordIntValue);
            }
            return false;
        }
    }
}