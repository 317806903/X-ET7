using System;
using System.Collections.Generic;
using Unity.Mathematics;
using ET.AbilityConfig;

namespace ET.Ability
{
    public static class UnitConditionHandleHelper
    {
        public static (bool bPass, bool isChgSelect, SelectHandle newSelectHandle) ChkConditionWhenFilter(Unit unit, SelectHandle selectHandle,
        List<SequenceUnitCondition> sequenceUnitConditions, ref ActionContext actionContext)
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

                long defenderUnitId = actionContext.defenderUnitId;
                for (int i = count - 1; i >= 0; i--)
                {
                    long unitId = selectHandle.unitIds[i];
                    Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitSelect, true) == false)
                    {
                        continue;
                    }

                    if (defenderUnitId == 0)
                    {
                        actionContext.defenderUnitId = unitSelect.Id;
                    }

                    bool bRetOne = _ChkConditionWhenFilter(unitSelect, sequenceUnitConditions, ref actionContext);
                    if (bRetOne == false)
                    {
                        isChgSelect = true;
                    }
                    else
                    {
                        newSelectHandle.unitIds.Add(unitId);
                    }
                }
                actionContext.defenderUnitId = defenderUnitId;

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

        public static bool _ChkConditionWhenFilter(Unit unit, List<SequenceUnitCondition> sequenceUnitConditions, ref ActionContext actionContext)
        {
            if (sequenceUnitConditions.Count == 0)
            {
                return true;
            }

            for (int i = 0; i < sequenceUnitConditions.Count; i++)
            {
                SequenceUnitCondition sequenceUnitCondition = sequenceUnitConditions[i];
                bool bRet = _ChkSequenceConditionWhenFilter(unit, sequenceUnitCondition, ref actionContext);
                if (bRet)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool _ChkSequenceConditionWhenFilter(Unit unit, SequenceUnitCondition sequenceUnitCondition, ref ActionContext actionContext)
        {
            for (int i = 0; i < sequenceUnitCondition.Conditions.Count; i++)
            {
                UnitConditionBase unitCondition = sequenceUnitCondition.Conditions[i];
                bool bRet = ChkConditionOne(unit, unitCondition, ref actionContext);
                if (bRet == false)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ChkConditionWhenChk(Unit unit, SelectHandle selectHandle,
        List<SequenceUnitCondition> sequenceUnitConditions, ref ActionContext actionContext)
        {
            if (sequenceUnitConditions.Count == 0)
            {
                return true;
            }

            if (selectHandle.selectHandleType is SelectHandleType.SelectDirection)
            {
                return false;
            }
            else if (selectHandle.selectHandleType is SelectHandleType.SelectPosition)
            {
                return false;
            }
            else if (selectHandle.selectHandleType is SelectHandleType.SelectUnits)
            {
                return _ChkConditionWhenChk(unit, selectHandle, sequenceUnitConditions, ref actionContext);
            }

            return false;
        }

        public static bool _ChkConditionWhenChk(Unit unit, SelectHandle selectHandle, List<SequenceUnitCondition> sequenceUnitConditions, ref ActionContext actionContext)
        {
            if (sequenceUnitConditions.Count == 0)
            {
                return true;
            }

            for (int i = 0; i < sequenceUnitConditions.Count; i++)
            {
                SequenceUnitCondition sequenceUnitCondition = sequenceUnitConditions[i];
                bool bRet = _ChkSequenceConditionWhenChk(unit, selectHandle, sequenceUnitCondition, ref actionContext);
                if (bRet)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool _ChkSequenceConditionWhenChk(Unit unit, SelectHandle selectHandle, SequenceUnitCondition sequenceUnitCondition, ref ActionContext actionContext)
        {
            long defenderUnitId = actionContext.defenderUnitId;
            try
            {
                for (int i = 0; i < sequenceUnitCondition.Conditions.Count; i++)
                {
                    UnitConditionBase unitCondition = sequenceUnitCondition.Conditions[i];
                    bool bRetOne = false;
                    foreach (long unitId in selectHandle.unitIds)
                    {
                        Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                        if (UnitHelper.ChkUnitAlive(unitSelect, true) == false)
                        {
                            continue;
                        }

                        if (defenderUnitId == 0)
                        {
                            actionContext.defenderUnitId = unitSelect.Id;
                        }

                        bool bRet = ChkConditionOne(unitSelect, unitCondition, ref actionContext);
                        if (bRet)
                        {
                            bRetOne = true;
                            break;
                        }
                    }
                    if (bRetOne == false)
                    {
                        return false;
                    }
                }
                return true;
            }
            finally
            {
                actionContext.defenderUnitId = defenderUnitId;
            }
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

        public static bool ChkCompare(bool value, ConditionCompare conditionCompare, bool conditionValue)
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

        public static bool ChkConditionOne(Unit unit, UnitConditionBase unitCondition, ref ActionContext actionContext)
        {
            switch (unitCondition)
            {
                case BuffStackCountCondition buffStackCountCondition:
                {
                    BuffObj buffObj = BuffHelper.GetBuffObj(unit, ref actionContext);
                    if (buffObj == null)
                    {
                        return false;
                    }

                    return ChkCompare(buffObj.stack, unitCondition.ConditionCompare, buffStackCountCondition.StackCount);
                }
                case BuffStackCountRecordCondition buffStackCountRecordCondition:
                {
                    BuffObj buffObj = BuffHelper.GetBuffObj(unit, ref actionContext);
                    if (buffObj == null)
                    {
                        return false;
                    }

                    float recordIntValue = RecordHandleHelper.GetRecordInt(unit, buffStackCountRecordCondition.RecordKey);
                    return ChkCompare(buffObj.stack, unitCondition.ConditionCompare, recordIntValue);
                }
                case BuffPassTimeCondition buffPassTimeCondition:
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
                        return ChkCompare(buffObj.timeElapsed / buffObj.orgDuration, unitCondition.ConditionCompare, buffPassTimeCondition.PassTime);
                    }
                }
                case BuffPassTimeRecordCondition buffPassTimeRecordCondition:
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
                        return ChkCompare(buffObj.timeElapsed / buffObj.orgDuration, unitCondition.ConditionCompare, recordIntValue);
                    }
                }
                case BuffLeftTimeCondition buffLeftTimeCondition:
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
                        return ChkCompare(buffObj.duration / buffObj.orgDuration, unitCondition.ConditionCompare, buffLeftTimeCondition.LeftTime);
                    }
                }
                case BuffLeftTimeRecordCondition buffLeftTimeRecordCondition:
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
                        return ChkCompare(buffObj.duration / buffObj.orgDuration, unitCondition.ConditionCompare, recordIntValue);
                    }
                }
                case BuffCfgIdCondition buffCfgIdCondition:
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
                case BuffTypeCondition buffTypeCondition:
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
                case BuffTagTypeCondition buffTagTypeCondition:
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
                case BuffTagGroupTypeCondition buffTagGroupTypeCondition:
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
                case SkillIdCondition skillIdCondition:
                {
                    string skillCfgId = actionContext.skillCfgId;
                    if (ChkCompare(skillIdCondition.SkillId, unitCondition.ConditionCompare, skillCfgId))
                    {
                        return true;
                    }

                    return false;
                }
                case SkillSlotTypeCondition skillSlotTypeCondition:
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
                case SkillGroupTypeCondition skillGroupTypeCondition:
                {
                    SkillGroupType skillGroupType = actionContext.skillGroupType;
                    if (ChkCompare((int)skillGroupTypeCondition.SkillGroupType, unitCondition.ConditionCompare, (int)skillGroupType))
                    {
                        return true;
                    }

                    return false;
                }
                case AttributeCondition attributeCondition:
                {
                    NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                    if (numericComponent == null)
                    {
                        return false;
                    }

                    float numericValue = numericComponent.GetAsFloat((int)attributeCondition.NumericType);
                    return ChkCompare(numericValue, unitCondition.ConditionCompare, attributeCondition.Value);
                }
                case AttributeRecordCondition attributeRecordCondition:
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
                case CurHpCondition curHpCondition:
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
                        return ChkCompare(curHp / maxHp, unitCondition.ConditionCompare, curHpCondition.Value);
                    }
                }
                case CurHpRecordCondition curHpRecordCondition:
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
                        return ChkCompare(curHp / maxHp, unitCondition.ConditionCompare, recordIntValue);
                    }
                }
                case OwerCountCondition owerCountCondition:
                {
                    int count = ET.Ability.OwnCallerHelper.GetOwnCallerCount(unit, owerCountCondition.OwnActor, owerCountCondition.OwnBullet,
                        owerCountCondition.OwnActor, owerCountCondition.OwnSkillCaster);

                    return ChkCompare(count, unitCondition.ConditionCompare, owerCountCondition.Value);
                }
                case OwerCountRecordCondition owerCountRecordCondition:
                {
                    int count = ET.Ability.OwnCallerHelper.GetOwnCallerCount(unit, owerCountRecordCondition.OwnActor,
                        owerCountRecordCondition.OwnBullet, owerCountRecordCondition.OwnActor, owerCountRecordCondition.OwnSkillCaster);

                    float recordIntValue = RecordHandleHelper.GetRecordInt(unit, owerCountRecordCondition.RecordKey);
                    return ChkCompare(count, unitCondition.ConditionCompare, recordIntValue);
                }
                case RecordIntCondition recordIntCondition:
                {
                    float curValue = RecordHandleHelper.GetRecordInt(unit, recordIntCondition.RecordKey);
                    if (ChkCompare(curValue, unitCondition.ConditionCompare, recordIntCondition.Value))
                    {
                        return true;
                    }

                    return false;
                }
                case RecordIntRecordCondition recordIntRecordCondition:
                {
                    float recordIntValue1 = RecordHandleHelper.GetRecordInt(unit, recordIntRecordCondition.RecordKey1);
                    float recordIntValue2 = RecordHandleHelper.GetRecordInt(unit, recordIntRecordCondition.RecordKey2);
                    if (ChkCompare(recordIntValue1, unitCondition.ConditionCompare, recordIntValue2))
                    {
                        return true;
                    }

                    return false;
                }
                case RecordStringCondition recordStringCondition:
                {
                    string curValue = RecordHandleHelper.GetRecordString(unit, recordStringCondition.RecordKey);
                    if (ChkCompare(curValue, unitCondition.ConditionCompare, recordStringCondition.Value))
                    {
                        return true;
                    }

                    return false;
                }
                case RecordStringRecordCondition recordStringRecordCondition:
                {
                    string recordStringValue1 = RecordHandleHelper.GetRecordString(unit, recordStringRecordCondition.RecordKey1);
                    string recordStringValue2 = RecordHandleHelper.GetRecordString(unit, recordStringRecordCondition.RecordKey2);
                    if (ChkCompare(recordStringValue1, unitCondition.ConditionCompare, recordStringValue2))
                    {
                        return true;
                    }

                    return false;
                }
                case OnHitChkCanBeControlCondition onHitChkCanBeControlCondition:
                {
                    bool beControl = BuffHelper.ChkCanBeControl(unit.DomainScene(), ref actionContext);

                    if (ChkCompare(beControl, unitCondition.ConditionCompare, onHitChkCanBeControlCondition.BeControl))
                    {
                        return true;
                    }

                    return false;
                }
                case OnHitChkIsCriticalStrikeCondition onHitChkIsCriticalStrikeCondition:
                {
                    if (ChkCompare(actionContext.isCriticalStrike, unitCondition.ConditionCompare,
                            onHitChkIsCriticalStrikeCondition.IsCriticalStrike))
                    {
                        return true;
                    }

                    return false;
                }
                case OnChkCanHitByBulletCondition onChkCanHitByBulletCondition:
                {
                    Unit unitAction = UnitHelper.GetUnit(unit.DomainScene(), actionContext.attackerUnitId);
                    if (UnitHelper.ChkIsBullet(unitAction) == false)
                    {
                        return false;
                    }

                    BulletObj bulletObj = unitAction.GetComponent<BulletObj>();
                    bool canHit = bulletObj.CanHitUnit(unit);
                    if (ChkCompare(canHit, unitCondition.ConditionCompare, onChkCanHitByBulletCondition.CanHit))
                    {
                        return true;
                    }

                    return false;
                }
                case ChkSelectUnitNumCondition chkSelectUnitNumCondition:
                {
                    int curValue = actionContext.selectUnitNum;
                    if (ChkCompare(curValue, unitCondition.ConditionCompare, chkSelectUnitNumCondition.Num))
                    {
                        return true;
                    }

                    return false;
                }
                case ChkSelectUnitNumRecordCondition chkSelectUnitNumRecordCondition:
                {
                    float recordIntValue = RecordHandleHelper.GetRecordInt(unit, chkSelectUnitNumRecordCondition.RecordKey);
                    int selectUnitNum = actionContext.selectUnitNum;
                    if (ChkCompare(selectUnitNum, unitCondition.ConditionCompare, recordIntValue))
                    {
                        return true;
                    }

                    return false;
                }
                case AngleCondition angleCondition:
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
                case AngleRecordCondition angleRecordCondition:
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
                case DisCondition disCondition:
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
                case DisRecordCondition disRecordCondition:
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
                case TargetAltitudeDifferenceCondition targetAltitudeDifferenceCondition:
                {
                    Unit curUnit = UnitHelper.GetUnit(unit.DomainScene(), actionContext.unitId);
                    if (curUnit == null)
                    {
                        return false;
                    }

                    float curDisHeight = unit.Position.y - curUnit.Position.y;
                    if (ChkCompare(curDisHeight, unitCondition.ConditionCompare, targetAltitudeDifferenceCondition.DisHeight))
                    {
                        return true;
                    }

                    return false;
                }
                case TargetAltitudeDifferenceRecordCondition targetAltitudeDifferenceRecordCondition:
                {
                    Unit curUnit = UnitHelper.GetUnit(unit.DomainScene(), actionContext.unitId);
                    if (curUnit == null)
                    {
                        return false;
                    }

                    float curDisHeight = unit.Position.y - curUnit.Position.y;
                    float recordIntValue = RecordHandleHelper.GetRecordInt(unit, targetAltitudeDifferenceRecordCondition.RecordKey);
                    if (ChkCompare(curDisHeight, unitCondition.ConditionCompare, recordIntValue))
                    {
                        return true;
                    }

                    return false;
                }
                case ProbabilityCondition probabilityCondition:
                {
                    RandomNumberComponent randomRumberComponent = unit.GetComponent<RandomNumberComponent>();
                    if (randomRumberComponent == null)
                    {
                        randomRumberComponent = unit.AddComponent<RandomNumberComponent>();
                    }

                    int random = randomRumberComponent.GetRandomNumber();
                    return ChkCompare(random, unitCondition.ConditionCompare, probabilityCondition.Value);
                }
                case ProbabilityRecordCondition probabilityRecordCondition:
                {
                    RandomNumberComponent randomRumberComponent = unit.GetComponent<RandomNumberComponent>();
                    if (randomRumberComponent == null)
                    {
                        randomRumberComponent = unit.AddComponent<RandomNumberComponent>();
                    }

                    int random = randomRumberComponent.GetRandomNumber();
                    float recordIntValue = RecordHandleHelper.GetRecordInt(unit, probabilityRecordCondition.RecordKey);
                    return ChkCompare(random, unitCondition.ConditionCompare, recordIntValue);
                }
                case ChkIsTowerCondition chkIsTowerCondition:
                {
                    TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
                    if (unitCondition.ConditionCompare == ConditionCompare.eq)
                    {
                        if (towerComponent == null)
                        {
                            return false;
                        }

                        if (chkIsTowerCondition.TowerType == TowerType.None)
                        {
                            return true;
                        }

                        if (chkIsTowerCondition.TowerType == towerComponent.towerType)
                        {
                            return true;
                        }

                        return false;
                    }
                    else if (unitCondition.ConditionCompare == ConditionCompare.ne)
                    {
                        if (chkIsTowerCondition.TowerType == TowerType.None)
                        {
                            if (towerComponent == null)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (towerComponent == null)
                            {
                                return true;
                            }
                            else
                            {
                                if (chkIsTowerCondition.TowerType != towerComponent.towerType)
                                {
                                    return true;
                                }
                            }
                        }

                        return false;
                    }

                    return false;
                }
                case ChkIsMonsterCondition chkIsMonsterCondition:
                {
                    MonsterComponent monsterComponent = unit.GetComponent<MonsterComponent>();
                    if (unitCondition.ConditionCompare == ConditionCompare.eq)
                    {
                        if (monsterComponent == null)
                        {
                            return false;
                        }

                        if (chkIsMonsterCondition.MonsterType == MonsterType.None)
                        {
                            return true;
                        }

                        if (chkIsMonsterCondition.MonsterType == monsterComponent.monsterType)
                        {
                            return true;
                        }

                        return false;
                    }
                    else if (unitCondition.ConditionCompare == ConditionCompare.ne)
                    {
                        if (chkIsMonsterCondition.MonsterType == MonsterType.None)
                        {
                            if (monsterComponent == null)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (monsterComponent == null)
                            {
                                return true;
                            }
                            else
                            {
                                if (chkIsMonsterCondition.MonsterType != monsterComponent.monsterType)
                                {
                                    return true;
                                }
                            }
                        }

                        return false;
                    }

                    return false;
                }
            }
            return false;
        }
    }
}