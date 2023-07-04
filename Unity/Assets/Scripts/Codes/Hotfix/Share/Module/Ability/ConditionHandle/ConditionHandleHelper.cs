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
            else if (condition is BuffTagTypeCondition buffTagTypeCondition)
            {
                BuffObj buffObj = BuffHelper.GetBuffObj(unit, actionContext);
                if (buffObj == null)
                {
                    return false;
                }

                if (condition.ConditionCompare == ConditionCompare.eq)
                {
                    foreach (BuffTagType buffTagType in buffObj.model.Tags)
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
                    foreach (BuffTagType buffTagType in buffObj.model.Tags)
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

                if (condition.ConditionCompare == ConditionCompare.eq)
                {
                    if (ChkCompare((int)buffObj.model.BuffType, ConditionCompare.eq, (int)buffTypeCondition.BuffType))
                    {
                        return true;
                    }
                    return false;
                }
                else if (condition.ConditionCompare == ConditionCompare.ne)
                {
                    if (ChkCompare((int)buffObj.model.BuffType, ConditionCompare.eq, (int)buffTypeCondition.BuffType))
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
            else if (condition is AttributeCondition attributeCondition)
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    return false;
                }
                float numericValue = numericComponent.Get((int)attributeCondition.NumericType);
                return ChkCompare(numericValue, attributeCondition.ConditionCompare, attributeCondition.Value);
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
            else if (condition is OnHitChkCanBeControl onHitChkCanBeControl)
            {
                bool beControl = BuffHelper.ChkCanBeControl(unit, actionContext);
                return onHitChkCanBeControl.BeControl == beControl;
            }
            else if (condition is ProbabilityCondition probabilityCondition)
            {
                int random = RandomGenerator.RandomNumber(1, 100);
                return ChkCompare(random, condition.ConditionCompare, probabilityCondition.Value);
            }
            return false;
        }
    }
}