using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class SelectHandleHelper
    {
        public static bool ChkIsNullSelectHandle(SelectHandle selectHandle)
        {
            if (selectHandle == null
                || (selectHandle.selectHandleType == SelectHandleType.SelectUnits && selectHandle.unitIds.Count == 0)
                || (selectHandle.selectHandleType == SelectHandleType.SelectPosition && selectHandle.position.Equals(float3.zero)))
            {
                return true;
            }
            return false;
        }

        public static (SelectHandle, Unit) DealSelectHandler(Unit triggerUnit, SelectObjectCfg selectObjectCfg, Unit onAttackUnit, Unit beHurtUnit, ref ActionContext actionContext)
        {
            if (onAttackUnit != null)
            {
                actionContext.attackerUnitId = onAttackUnit.Id;
            }

            Unit resetPosByUnit = null;
            SelectHandle selectHandle;
            switch (selectObjectCfg.ActionCallParam)
            {
                case ActionCallSelectLast:
                {
                    selectHandle = UnitHelper.GetSaveSelectHandle(triggerUnit);
                    break;
                }
                case ActionCallAutoUnit actionCallAutoUnit:
                {
                    selectHandle = SelectHandleHelper.CreateSelectHandle(triggerUnit, beHurtUnit, selectObjectCfg, ref actionContext);
                    break;
                }
                case ActionCallAutoSelf actionCallAutoSelf:
                {
                    selectHandle = SelectHandleHelper.CreateSelectHandle(triggerUnit, null, selectObjectCfg, ref actionContext);
                    break;
                }
                case ActionCallOnAoeChgUnit actionCallOnAoeChgUnit:
                {
                    return (null, null);
                }
                case ActionCallOnAoeInUnit actionCallOnAoeInUnit:
                {
                    return (null, null);
                }
                case ActionCallOwnCaller actionCallOwnCaller:
                {
                    HashSet<long> unitList = triggerUnit.GetOwnCaller(actionCallOwnCaller.OwnActor, actionCallOwnCaller.OwnBullet, actionCallOwnCaller.OwnAoe, actionCallOwnCaller.OwnSkillCaster);

                    selectHandle = ET.Ability.SelectHandleHelper.GetSelectHandleWithSelectObjectType(triggerUnit, selectObjectCfg.SelectObjectTeamFlagType, selectObjectCfg.SelectObjectUnitType, unitList);

                    return (selectHandle, null);
                }
                default:
                {
                    Unit targetUnit;
                    switch (selectObjectCfg.ActionCallParam)
                    {
                        case ActionCallCasterUnit actionCallCasterUnit:
                        {
                            targetUnit = triggerUnit.GetCaster();
                            break;
                        }
                        case ActionCallCasterFirstActorUnit actionCallCasterFirstActorUnit:
                        {
                            targetUnit = triggerUnit.GetCasterNearActor();
                            break;
                        }
                        case ActionCallCasterActorUnit actionCallCasterActorUnit:
                        {
                            targetUnit = triggerUnit.GetCasterRootActor();
                            break;
                        }
                        case ActionCallOnAttackUnit actionCallOnAttackUnit:
                        {
                            targetUnit = onAttackUnit;
                            break;
                        }
                        case ActionCallBeHurtUnit actionCallBeHurtUnit:
                        {
                            targetUnit = beHurtUnit;
                            break;
                        }
                        case ActionCallOnAttackHitPos actionCallOnAttackHitPos:
                        {
                            float3 hitPos = actionContext.hitPosition;
                            if (hitPos.Equals(float3.zero) == false)
                            {
                                selectHandle = SelectHandleHelper.CreatePositionSelectHandle(triggerUnit, hitPos, float3.zero, selectObjectCfg);
                                return (selectHandle, resetPosByUnit);
                            }
                            else
                            {
                                return (null, null);
                            }
                        }
                        default:
                        {
                            targetUnit = triggerUnit;
                            break;
                        }
                    }

                    resetPosByUnit = targetUnit;
                    selectHandle = SelectHandleHelper.CreateUnitSelectHandle(triggerUnit, targetUnit, selectObjectCfg);
                    break;
                }
            }

            if (selectHandle == null)
            {
                return (null, null);
            }

            if (ET.Ability.SelectHandleHelper.ChkIsNullSelectHandle(selectHandle))
            {
#if UNITY_EDITOR
                //Log.Error($"triggerUnit[{triggerUnit}] [{selectObjectCfg}] selectHandle.selectHandleType == SelectHandleType.SelectUnits && selectHandle.unitIds.Count == 0");
#endif
                return (null, null);
            }
            return (selectHandle, resetPosByUnit);
        }

        public static SelectHandle CreateSelectHandleWhenAoe(Unit aoeUnit, SelectObjectTeamFlagType selectObjectTeamFlagType, AoeSelectObjectType aoeSelectObjectType)
        {
            SelectHandle selectHandle;
            if (aoeSelectObjectType is AoeSelectObjectType.AoeSelf)
            {
                selectHandle = SelectHandleHelper.CreateUnitSelfSelectHandle(aoeUnit);
            }
            else if (aoeSelectObjectType is AoeSelectObjectType.AoeChgList)
            {
                selectHandle = SelectHandleHelper.CreateAoeUnitSelectHandle(aoeUnit, selectObjectTeamFlagType, null, true);
            }
            else if (aoeSelectObjectType is AoeSelectObjectType.AoeInList)
            {
                selectHandle = SelectHandleHelper.CreateAoeUnitSelectHandle(aoeUnit, selectObjectTeamFlagType, null, false);
            }
            else
            {
                return null;
            }

            if (ET.Ability.SelectHandleHelper.ChkIsNullSelectHandle(selectHandle))
            {
                return null;
            }
            return selectHandle;
        }

        public static SelectHandle CreateSelectHandleWhenClient(Unit unit, SelectObjectCfg selectObjectCfg, ref ActionContext actionContext, SelectHandle selectHandleShow, SelectObjectCfg selectObjectCfgShow)
        {
            if (unit == null)
            {
                return null;
            }
            ActionCallParam actionCallParam = selectObjectCfgShow.ActionCallParam;
            if (actionCallParam is ActionCallShow_Drag_SelfUnit actionCallShowDragSelfUnit)
            {
                return CreateUnitSelfSelectHandle(unit);
            }
            else if(actionCallParam is ActionCallShow_Drag_SelfArea actionCallShowDragSelfArea)
            {
                float3 resetPos = selectHandleShow.position;
                float3 resetForward = float3.zero;
                return CreatePositionSelectHandle(unit, resetPos, resetForward, selectObjectCfg);
            }
            else if(actionCallParam is ActionCallShow_Drag_OtherUnit actionCallShowDragOtherUnit)
            {
                if (selectHandleShow.unitIds == null)
                {
#if UNITY_EDITOR
                    Log.Error($"ActionCallShow_Drag_OtherUnit selectHandleShow.unitIds == null");
#endif
                    return null;
                }
                else if (selectHandleShow.unitIds.Count == 0)
                {
#if UNITY_EDITOR
                    Log.Error($"ActionCallShow_Drag_OtherUnit selectHandleShow.unitIds.Count[{selectHandleShow.unitIds.Count}] == 0");
#endif
                    return null;
                }
                else if (selectHandleShow.unitIds.Count > 1)
                {
#if UNITY_EDITOR
                    Log.Error($"ActionCallShow_Drag_OtherUnit selectHandleShow.unitIds.Count[{selectHandleShow.unitIds.Count}] > 1");
#endif
                    return null;
                }

                long targetUnitId = selectHandleShow.unitIds[0];
                Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), targetUnitId);
                return CreateUnitSelectHandle(unit, targetUnit, selectObjectCfg);
            }
            else if (actionCallParam is ActionCallShow_Drag_OtherArea actionCallShowDragOtherArea)
            {
                float3 resetPos = selectHandleShow.position;
                float3 resetForward = float3.zero;
                return CreatePositionSelectHandle(unit, resetPos, resetForward, selectObjectCfg);
            }
            else if (actionCallParam is ActionCallShow_Drag_RectangleArea actionCallShowDragRectangleArea)
            {
                float3 resetPos = selectHandleShow.position;
                float3 resetForward = float3.zero;
                return CreatePositionSelectHandle(unit, resetPos, resetForward, selectObjectCfg);
            }
            else if (actionCallParam is ActionCallShow_Drag_UmbellateArea actionCallShowDragUmbellateArea)
            {
                float3 resetPos = selectHandleShow.position;
                float3 resetForward = float3.zero;
                return CreatePositionSelectHandle(unit, resetPos, resetForward, selectObjectCfg);
            }
            else if (actionCallParam is ActionCallShow_Camera_OtherUnit actionCallShowCameraOtherUnit)
            {
                if (selectHandleShow.unitIds == null)
                {
#if UNITY_EDITOR
                    Log.Error($"ActionCallShow_Camera_OtherUnit selectHandleShow.unitIds == null");
#endif
                    return null;
                }
                else if (selectHandleShow.unitIds.Count == 0)
                {
#if UNITY_EDITOR
                    Log.Error($"ActionCallShow_Camera_OtherUnit selectHandleShow.unitIds.Count[{selectHandleShow.unitIds.Count}] == 0");
#endif
                    return null;
                }
                else if (selectHandleShow.unitIds.Count > 1)
                {
#if UNITY_EDITOR
                    Log.Error($"ActionCallShow_Camera_OtherUnit selectHandleShow.unitIds.Count[{selectHandleShow.unitIds.Count}] > 1");
#endif
                    return null;
                }

                long targetUnitId = selectHandleShow.unitIds[0];
                Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), targetUnitId);
                return CreateUnitSelectHandle(unit, targetUnit, selectObjectCfg);
            }
            else if (actionCallParam is ActionCallShow_Camera_OtherArea actionCallShowCameraOtherArea)
            {
                float3 resetPos = selectHandleShow.position;
                float3 resetForward = float3.zero;
                return CreatePositionSelectHandle(unit, resetPos, resetForward, selectObjectCfg);
            }
            else if (actionCallParam is ActionCallShow_Camera_RectangleArea actionCallShowCameraRectangleArea)
            {
                float3 resetPos = selectHandleShow.position;
                float3 resetForward = selectHandleShow.direction;
                return CreatePositionSelectHandle(unit, resetPos, resetForward, selectObjectCfg);
            }
            else if (actionCallParam is ActionCallShow_Camera_UmbellateArea actionCallShowCameraUmbellateArea)
            {
                float3 resetPos = selectHandleShow.position;
                float3 resetForward = selectHandleShow.direction;
                return CreatePositionSelectHandle(unit, resetPos, resetForward, selectObjectCfg);
            }

            return null;
        }

        public static SelectHandle CreateSelectHandle(Unit unit, Unit resetPosByUnit, SelectObjectCfg selectObjectCfg, ref ActionContext actionContext, bool canUseRecordResult = true, bool isNeedChgSelectNumByNumeric = false)
        {
            bool isResetPos = false;
            float3 resetPos = float3.zero;
            if (resetPosByUnit != null)
            {
                isResetPos = true;
                resetPos = resetPosByUnit.Position;
            }

            bool isResetForward = false;
            float3 resetForward = float3.zero;
            return CreateSelectHandle(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, ref actionContext, canUseRecordResult, isNeedChgSelectNumByNumeric);
        }

        public static SelectHandle CreateSelectHandle(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg, ref ActionContext actionContext, bool canUseRecordResult = true, bool isNeedChgSelectNumByNumeric = false)
        {
            if (unit == null)
            {
                return null;
            }
            ActionCallParam actionCallParam = selectObjectCfg.ActionCallParam;
            SelectHandle saveSelectHandle;
            if (actionCallParam is ActionCallSelectLast)
            {
                saveSelectHandle = UnitHelper.GetSaveSelectHandle(unit);
                if (saveSelectHandle == null)
                {
#if UNITY_EDITOR
                    Log.Error($" ActionCallSelectLast saveSelectHandle == null [{unit.CfgId}] [{selectObjectCfg.Id}]");
#endif
                }
                return saveSelectHandle;
            }

            bool bRet;
            (bRet, saveSelectHandle) = _ChkIsUseSaveSelectHandle(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, ref actionContext);
            if (bRet)
            {
                return saveSelectHandle;
            }

            SelectHandle selectHandle = SelectHandle.Create();
            if (actionCallParam is ActionCallCasterShow)
            {
            }
            else if (actionCallParam is ActionCallAutoSelf)
            {
                selectHandle.selectHandleType = SelectHandleType.SelectUnits;
                selectHandle.unitIds = ListComponent<long>.Create();
                selectHandle.unitIds.Add(unit.Id);
                selectHandle.position = unit.Position;
                selectHandle.direction = unit.Forward;
            }
            else if (actionCallParam is ActionCallBeHurtUnit)
            {
                Unit defenderUnit = UnitHelper.GetUnit(unit.DomainScene(), actionContext.defenderUnitId);
                selectHandle.selectHandleType = SelectHandleType.SelectUnits;
                selectHandle.unitIds = ListComponent<long>.Create();
                if (defenderUnit != null)
                {
                    selectHandle.unitIds.Add(defenderUnit.Id);
                    selectHandle.position = defenderUnit.Position;
                    selectHandle.direction = defenderUnit.Forward;
                }
            }
            else if (actionCallParam is ActionCallAutoUnit)
            {
                selectHandle.selectHandleType = SelectHandleType.SelectUnits;
                selectHandle.unitIds = ListComponent<long>.Create();
                if (actionCallParam is ActionCallAutoUnitArea actionCallAutoUnitArea)
                {
                    bool bRecord = false;
                    ListComponent<long > recordUnitIds = null;
                    if (canUseRecordResult)
                    {
                        (bRecord, recordUnitIds) = ChkRecordUnitsByArea(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg);
                    }
                    if (bRecord)
                    {
                        selectHandle.unitIds.Clear();
                        selectHandle.unitIds.AddRange(recordUnitIds);
                    }
                    else
                    {
                        GetUnitsByArea(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, actionCallAutoUnitArea, ref selectHandle, ref actionContext, isNeedChgSelectNumByNumeric);

                        DoRecordUnitsByArea(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, selectHandle);
                    }

                }

                if (selectObjectCfg.IsChgToSelectPos)
                {
                    DealWhenIsChgToSelectPos(unit, ref selectHandle);
                }
                else
                {
                    if (selectHandle.unitIds.Count > 0)
                    {
                        Unit selectUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[0]);
                        selectHandle.position = selectUnit.Position;
                        selectHandle.direction = selectHandle.position - unit.Position;
                    }
                    else
                    {
                        selectHandle.position = unit.Position;
                        selectHandle.direction = unit.Forward;
                    }
                }
            }

            if (ET.Ability.SelectHandleHelper.ChkIsNullSelectHandle(selectHandle))
            {
                return null;
            }

            if (selectObjectCfg.IsSaveTarget)
            {
                if (selectObjectCfg.SelectNum != -1)
                {
                    UnitHelper.SaveSelectHandle(unit, selectHandle, false);
                }
            }
            else if (selectObjectCfg.IsSaveTargetOnce)
            {
                UnitHelper.SaveSelectHandle(unit, selectHandle, true);
            }

            else if (selectObjectCfg.IsSaveExcludeTarget)
            {
                UnitHelper.SaveExcludeSelectHandle(unit, selectHandle);
            }

            return selectHandle;
        }

        public static (bool, SelectHandle) _ChkIsUseSaveSelectHandle(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg,
        ref ActionContext actionContext)
        {
            ActionCallParam actionCallParam = selectObjectCfg.ActionCallParam;
            bool chkIsUseSaveSelectHandle = false;
            if (selectObjectCfg.IsSaveTarget || selectObjectCfg.IsSaveTargetOnce)
            {
                chkIsUseSaveSelectHandle = true;
            }
            if (chkIsUseSaveSelectHandle == false)
            {
                return (false, null);
            }
            SelectHandle saveSelectHandle = UnitHelper.GetSaveSelectHandle(unit);
            if (saveSelectHandle == null)
            {
                return (false, null);
            }
            if (saveSelectHandle.selectHandleType == SelectHandleType.SelectUnits
                && (saveSelectHandle.unitIds == null || saveSelectHandle.unitIds.Count == 0))
            {
                return (false, null);
            }

            // if (actionCallParam is ActionCallSelectDirection)
            // {
            //     if (saveSelectHandle.selectHandleType != SelectHandleType.SelectDirection)
            //     {
            //         return (false, null);
            //     }
            // }
            // else if (actionCallParam is ActionCallSelectPosition)
            // {
            //     if (saveSelectHandle.selectHandleType != SelectHandleType.SelectPosition)
            //     {
            //         return (false, null);
            //     }
            // }
            // else if (actionCallParam is ActionCallSelectUnit)
            // {
            //     if (saveSelectHandle.selectHandleType != SelectHandleType.SelectUnits)
            //     {
            //         return (false, null);
            //     }
            // }
            // else
            if (actionCallParam is ActionCallAutoSelf)
            {
                if (saveSelectHandle.selectHandleType != SelectHandleType.SelectUnits)
                {
                    return (false, null);
                }

                if (saveSelectHandle.unitIds == null || saveSelectHandle.unitIds[0] != unit.Id)
                {
                    return (false, null);
                }
            }
            else if (actionCallParam is ActionCallAutoUnit)
            {
                if (saveSelectHandle.selectHandleType != SelectHandleType.SelectUnits)
                {
                    return (false, null);
                }
                if (actionCallParam is ActionCallAutoUnitArea actionCallAutoUnitArea)
                {
                    bool bRet = ChkUnitsInArea(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, actionCallAutoUnitArea, saveSelectHandle, ref actionContext);
                    if (bRet == false)
                    {
                        return (false, null);
                    }
                }
            }

            if (saveSelectHandle.unitIds.Count > 0)
            {
                foreach (long unitId in saveSelectHandle.unitIds)
                {
                    Unit selectUnit = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(selectUnit) == false)
                    {
                        return (false, null);
                    }

                    if (selectObjectCfg.IsNeedIgnoreCannotBeTargeted == false)
                    {
                        bool isCannotBeTargeted = ET.Ability.BuffHelper.ChkCannotBeTargeted(selectUnit);
                        if (isCannotBeTargeted)
                        {
                            return (false, null);
                        }
                    }
                    if (selectObjectCfg.IsNeedChkInvisible)
                    {
                        bool isBeFind = ET.Ability.BuffHelper.ChkCanBeSee(selectUnit, unit);
                        if (isBeFind == false)
                        {
                            return (false, null);
                        }
                    }
                    if (selectObjectCfg.IsNeedChkFly)
                    {
                        bool isBeFind = ET.Ability.BuffHelper.ChkCanBeTouchWhenFly(selectUnit, unit);
                        if (isBeFind == false)
                        {
                            return (false, null);
                        }
                    }
                }
            }

            if (saveSelectHandle.unitIds.Count > 0)
            {
                Unit selectUnitFirst = UnitHelper.GetUnit(unit.DomainScene(), saveSelectHandle.unitIds[0]);
                saveSelectHandle.position = selectUnitFirst.Position;
                saveSelectHandle.direction = saveSelectHandle.position - unit.Position;
            }
            else
            {
                saveSelectHandle.position = unit.Position;
                saveSelectHandle.direction = unit.Forward;
            }
            return (true, saveSelectHandle);
        }


        public static SelectHandle CreatePositionSelectHandle(Unit unit, float3 position, float3 direction, SelectObjectCfg selectObjectCfg)
        {
            SelectHandle selectHandle = SelectHandle.Create();
            if (direction.Equals(float3.zero))
            {
                selectHandle.selectHandleType = SelectHandleType.SelectPosition;
                selectHandle.position = position;
            }
            else
            {
                selectHandle.selectHandleType = SelectHandleType.SelectDirection;
                selectHandle.position = position;
                selectHandle.direction = direction;
            }

            if (selectObjectCfg != null)
            {
                if (selectObjectCfg.IsSaveTarget)
                {
                    UnitHelper.SaveSelectHandle(unit, selectHandle, false);
                }
                if (selectObjectCfg.IsSaveTargetOnce)
                {
                    UnitHelper.SaveSelectHandle(unit, selectHandle, true);
                }
            }

            return selectHandle;
        }

        public static SelectHandle CreateUnitSelectHandle(Unit unit, Unit targetUnit, SelectObjectCfg selectObjectCfg)
        {
            if (targetUnit == null)
            {
                return null;
            }
            SelectHandle selectHandle = SelectHandle.Create();
            selectHandle.selectHandleType = SelectHandleType.SelectUnits;
            if (selectHandle.unitIds == null)
            {
                selectHandle.unitIds = ListComponent<long>.Create();
            }
            else
            {
                selectHandle.unitIds.Clear();
            }
            selectHandle.unitIds.Add(targetUnit.Id);
            selectHandle.position = targetUnit.Position;
            selectHandle.direction = targetUnit.Forward;

            if (selectObjectCfg != null)
            {
                if (selectObjectCfg.IsChgToSelectPos)
                {
                    DealWhenIsChgToSelectPos(unit, ref selectHandle);
                }
                if (selectObjectCfg.IsSaveTarget)
                {
                    UnitHelper.SaveSelectHandle(unit, selectHandle, false);
                }
                if (selectObjectCfg.IsSaveTargetOnce)
                {
                    UnitHelper.SaveSelectHandle(unit, selectHandle, true);
                }
            }

            return selectHandle;
        }

        public static SelectHandle CreateUnitSelfSelectHandle(Unit unit)
        {
            SelectHandle selectHandle = SelectHandle.Create();
            selectHandle.selectHandleType = SelectHandleType.SelectUnits;
            selectHandle.unitIds = ListComponent<long>.Create();
            selectHandle.unitIds.Add(unit.Id);
            selectHandle.position = unit.Position;
            selectHandle.direction = unit.Forward;

            return selectHandle;
        }

        public static SelectHandle CreateUnitNoneSelectHandle()
        {
            SelectHandle selectHandle = SelectHandle.Create();
            selectHandle.selectHandleType = SelectHandleType.SelectUnits;
            selectHandle.unitIds = ListComponent<long>.Create();

            return selectHandle;
        }

        public static SelectHandle CreateAoeUnitSelectHandle(Unit unit, SelectObjectTeamFlagType selectObjectTeamFlagType, SelectObjectUnitTypeBase selectObjectUnitTypeBase, bool isChgAoe)
        {
            if (UnitHelper.ChkIsAoe(unit) == false)
            {
                return null;
            }

            AoeObj aoeObj = unit.GetComponent<AoeObj>();
            if (aoeObj == null)
            {
#if UNITY_EDITOR
                Log.Error($"aoeObj == null");
#endif
                return null;
            }
            else
            {
                HashSet<long> unitList;
                if (isChgAoe)
                {
                    unitList = aoeObj.aoeChgUnitList;
                }
                else
                {
                    unitList = aoeObj.aoeInUnitList;
                }
                return GetSelectHandleWithSelectObjectType(unit, selectObjectTeamFlagType, selectObjectUnitTypeBase, unitList);
            }
        }

        public static SelectHandle GetSelectHandleWithSelectObjectType(Unit unit, SelectObjectTeamFlagType selectObjectTeamFlagType, SelectObjectUnitTypeBase selectObjectUnitTypeBase, HashSet<long> unitList)
        {
            SelectHandle selectHandle = SelectHandleHelper.CreateUnitNoneSelectHandle();
            if (unitList != null)
            {
                List<long> unitListTmp = SelectHandleHelper.GetUnitListWithSelectObjectType(unit, selectObjectTeamFlagType, selectObjectUnitTypeBase, unitList);
                selectHandle.unitIds.AddRange(unitListTmp);
            }
            return selectHandle;
        }

        public static List<long> GetUnitListWithSelectObjectType(Unit unit, SelectObjectTeamFlagType selectObjectTeamFlagType, SelectObjectUnitTypeBase selectObjectUnitTypeBase, HashSet<long> unitList)
        {
            ListComponent<long> unitListTmp = ListComponent<long>.Create();
            if (selectObjectUnitTypeBase == null)
            {
                foreach (long unitId in unitList)
                {
                    Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitSelect) == false)
                    {
                        continue;
                    }

                    bool bRet = ET.GamePlayHelper.ChkIsSelectObjectTeamFlagType(unit.DomainScene(), selectObjectTeamFlagType, unit, unitSelect);
                    if (bRet == false)
                    {
                        continue;
                    }

                    unitListTmp.Add(unitId);
                }
                return unitListTmp;
            }

            bool isContainHome = false;
            bool isContainSelf = false;
            bool isContainFriend = false;
            bool isContainHostile = false;
            SelectObjectUnitType selectObjectUnitType;
            switch (selectObjectUnitTypeBase)
            {
                case SelectObjectUnitTypeSelf selectObjectUnitTypeSelf:
                {
                    isContainHome = false;
                    isContainSelf = true;
                    isContainFriend = false;
                    isContainHostile = false;
                    selectObjectUnitType = selectObjectUnitTypeSelf.UnitType;
                    break;
                }
                case SelectObjectUnitTypeSelfAndFriend selectObjectUnitTypeSelfAndFriend:
                {
                    isContainHome = false;
                    isContainSelf = true;
                    isContainFriend = true;
                    isContainHostile = false;
                    selectObjectUnitType = selectObjectUnitTypeSelfAndFriend.UnitType;
                    break;
                }
                case SelectObjectUnitTypeFriend selectObjectUnitTypeFriend:
                {
                    isContainHome = false;
                    isContainSelf = false;
                    isContainFriend = true;
                    isContainHostile = false;
                    selectObjectUnitType = selectObjectUnitTypeFriend.UnitType;
                    break;
                }
                case SelectObjectUnitTypeHostile selectObjectUnitTypeHostile:
                {
                    isContainHome = false;
                    isContainSelf = false;
                    isContainFriend = false;
                    isContainHostile = true;
                    selectObjectUnitType = selectObjectUnitTypeHostile.UnitType;
                    break;
                }
                case SelectObjectUnitTypeSelfHome selectObjectUnitTypeSelfHome:
                {
                    isContainHome = true;
                    isContainSelf = true;
                    isContainFriend = false;
                    isContainHostile = false;
                    selectObjectUnitType = SelectObjectUnitType.All;
                    break;
                }
                case SelectObjectUnitTypeSelfAndFriendHome selectObjectUnitTypeSelfAndFriendHome:
                {
                    isContainHome = true;
                    isContainSelf = true;
                    isContainFriend = true;
                    isContainHostile = false;
                    selectObjectUnitType = SelectObjectUnitType.All;
                    break;
                }
                case SelectObjectUnitTypeFriendHome selectObjectUnitTypeFriendHome:
                {
                    isContainHome = true;
                    isContainSelf = false;
                    isContainFriend = true;
                    isContainHostile = false;
                    selectObjectUnitType = SelectObjectUnitType.All;
                    break;
                }
                case SelectObjectUnitTypeHostileHome selectObjectUnitTypeHostileHome:
                {
                    isContainHome = true;
                    isContainSelf = false;
                    isContainFriend = false;
                    isContainHostile = true;
                    selectObjectUnitType = SelectObjectUnitType.All;
                    break;
                }
                default:
                {
                    return null;
                }
            }

            if (isContainHome)
            {

                GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(unit.DomainScene());
                if (gamePlayComponent == null)
                {
                    return null;
                }
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
                if (gamePlayTowerDefenseComponent == null)
                {
                    return null;
                }
                PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                if (putHomeComponent == null)
                {
                    return null;
                }
                Dictionary<TeamFlagType, long> homeUnitList = putHomeComponent.GetHomeUnitList();
                TeamFlagType teamFlagType = ET.Ability.TeamFlagHelper.GetTeamFlag(unit);
                foreach (var homeUnits in homeUnitList)
                {
                    TeamFlagType curHomeTeamFlagType = homeUnits.Key;
                    long curHomeUnitId = homeUnits.Value;
                    Unit curHomeUnit = UnitHelper.GetUnit(unit.DomainScene(), curHomeUnitId);
                    if (UnitHelper.ChkUnitAlive(curHomeUnit) == false)
                    {
                        continue;
                    }

                    bool bRet = ET.GamePlayHelper.ChkIsSelectObjectTeamFlagType(unit.DomainScene(), selectObjectTeamFlagType, unit, curHomeUnit);
                    if (bRet == false)
                    {
                        continue;
                    }

                    if (isContainHostile)
                    {
                        bool isFriend = gamePlayComponent.ChkIsFriend(teamFlagType, curHomeTeamFlagType);
                        if (isFriend)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (isContainSelf && isContainFriend)
                        {
                            bool isFriend = gamePlayComponent.ChkIsFriend(teamFlagType, curHomeTeamFlagType);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else if (isContainSelf)
                        {
                            if (GamePlayHelper.GetHomeTeamFlagType(teamFlagType) != curHomeTeamFlagType)
                            {
                                continue;
                            }
                        }
                        else if (isContainFriend)
                        {
                            if (GamePlayHelper.GetHomeTeamFlagType(teamFlagType) == curHomeTeamFlagType)
                            {
                                continue;
                            }
                            bool isFriend = gamePlayComponent.ChkIsFriend(teamFlagType, curHomeTeamFlagType);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    unitListTmp.Add(curHomeUnitId);
                }
            }
            else
            {
                foreach (long unitId in unitList)
                {
                    Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    bool isContinue = false;
                    if (UnitHelper.ChkIsPlayer(unitSelect))
                    {
                        if (selectObjectUnitType == SelectObjectUnitType.All || selectObjectUnitType == SelectObjectUnitType.OnlyPlayer)
                        {
                            isContinue = true;
                        }
                        else
                        {
                            isContinue = false;
                        }
                    }
                    else if (UnitHelper.ChkIsActor(unitSelect))
                    {
                        if (selectObjectUnitType == SelectObjectUnitType.All || selectObjectUnitType == SelectObjectUnitType.NotPlayer)
                        {
                            isContinue = true;
                        }
                        else
                        {
                            isContinue = false;
                        }
                    }
                    else
                    {
                        isContinue = false;
                    }

                    if (isContinue == false)
                    {
                        continue;
                    }

                    if (UnitHelper.ChkUnitAlive(unitSelect) == false)
                    {
                        continue;
                    }

                    bool bRet = ET.GamePlayHelper.ChkIsSelectObjectTeamFlagType(unit.DomainScene(), selectObjectTeamFlagType, unit, unitSelect);
                    if (bRet == false)
                    {
                        continue;
                    }

                    if (isContainHostile)
                    {
                        bool isFriend = ET.GamePlayHelper.ChkIsFriend(unit, unitSelect);
                        if (isFriend)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (isContainSelf && isContainFriend)
                        {
                            bool isFriend = ET.GamePlayHelper.ChkIsFriend(unit, unitSelect);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else if (isContainSelf)
                        {
                            bool isFriend = ET.GamePlayHelper.ChkIsFriend(unit, unitSelect, true);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else if (isContainFriend)
                        {
                            bool isFriend = ET.GamePlayHelper.ChkIsFriend(unit, unitSelect, true);
                            if (isFriend)
                            {
                                continue;
                            }
                            isFriend = ET.GamePlayHelper.ChkIsFriend(unit, unitSelect, false);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    unitListTmp.Add(unitId);
                }
            }
            return unitListTmp;
        }

        public static MultiMapSimple<float, Unit> tmp_dic = new();
        public static MultiMapSimple<float, Unit> tmp_dic2 = new();
        public static List<Unit> tmp_list1 = new();

        public static void DoRecordUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg, SelectHandle selectHandle)
        {
            SelectHandleRecordManager selectHandleRecordManager = unit.DomainScene().GetComponent<SelectHandleRecordManager>();
            selectHandleRecordManager.DoRecordUnitsByArea(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, selectHandle);
        }

        public static (bool, ListComponent<long>) ChkRecordUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg)
        {
            if (unit == null || unit.DomainScene() == null)
            {
                return (false, null);
            }
            SelectHandleRecordManager selectHandleRecordManager = unit.DomainScene().GetComponent<SelectHandleRecordManager>();
            if (selectHandleRecordManager == null)
            {
                return (false, null);
            }
            return selectHandleRecordManager.ChkRecordUnitsByArea(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg);
        }

        public static void GetUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitArea actionCallAutoUnitArea, ref SelectHandle selectHandle, ref ActionContext actionContext, bool isNeedChgSelectNumByNumeric)
        {
            List<Unit> list = UnitHelper.GetUnitList(unit, selectObjectCfg.SelectObjectTeamFlagType, selectObjectCfg.SelectObjectUnitType, selectObjectCfg.IsNeedChkInvisible, selectObjectCfg.IsNeedChkFly, selectObjectCfg.IsNeedIgnoreCannotBeTargeted);

            if(list == null || list.Count == 0)
            {
                selectHandle.unitIds.Clear();
                return;
            }

            if (selectObjectCfg.IsNeedChkExcludeTarget)
            {
                HashSet<long> excludeUnitList = UnitHelper.GetSaveExcludeSelectHandle(unit);
                if (excludeUnitList != null && excludeUnitList.Count > 0)
                {
                    ListComponent<Unit> newList = ListComponent<Unit>.Create();
                    foreach (Unit unitTmp in list)
                    {
                        if (excludeUnitList.Contains(unitTmp.Id) == false)
                        {
                            newList.Add(unitTmp);
                        }
                    }
                    list = newList;
                    if(list == null || list.Count == 0)
                    {
                        selectHandle.unitIds.Clear();
                        return;
                    }
                }
            }

            tmp_dic.Clear();
            tmp_dic2.Clear();
            tmp_list1.Clear();

            MultiMapSimple<float, Unit> dic = tmp_dic;
            if (actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate actionCallAutoUnitWhenUmbellate)
            {
                _GetUnitsWhenUmbellate(unit, isResetPos, resetPos, isResetForward, resetForward, list, ref dic, selectObjectCfg, actionCallAutoUnitWhenUmbellate, ref actionContext);
            }
            else if (actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle actionCallAutoUnitWhenRectangle)
            {
                _GetUnitsWhenRectangle(unit, isResetPos, resetPos, isResetForward, resetForward, list, ref dic, selectObjectCfg, actionCallAutoUnitWhenRectangle, ref actionContext);
            }

            selectHandle.unitIds.Clear();
            foreach (var sortList in dic)
            {
                for (int i = 0; i < sortList.Value.Count; i++)
                {
                    Unit unitOne = sortList.Value[i];
                    if (UnitHelper.ChkUnitAlive(unitOne))
                    {
                        selectHandle.unitIds.Add(unitOne.Id);
                    }
                }
            }

            (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = UnitConditionHandleHelper.ChkConditionWhenFilter(unit, selectHandle, selectObjectCfg.SelectPreCondition, ref actionContext);
            if (bRet1 == false)
            {
                selectHandle.unitIds.Clear();
                return;
            }
            if (isChgSelect1)
            {
                selectHandle.unitIds.Clear();
                selectHandle.unitIds.AddRange(newSelectHandle1.unitIds);
            }

            SelectObjectOrderHandle(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, dic, tmp_list1, ref selectHandle, ref actionContext, isNeedChgSelectNumByNumeric);

            dic.Clear();
            tmp_dic.Clear();
            tmp_dic2.Clear();
            tmp_list1.Clear();
        }

        /// <summary>
        /// 获取伞型范围对象列表
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="isResetPos"></param>
        /// <param name="resetPos"></param>
        /// <param name="list"></param>
        /// <param name="dic"></param>
        /// <param name="actionCallAutoUnit"></param>
        public static void _GetUnitsWhenUmbellate(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, List<Unit> list, ref MultiMapSimple<float, Unit> dic, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitWhenUmbellate actionCallAutoUnit, ref ActionContext actionContext)
        {
            float gameResScale = UnitHelper.GetGameResScale(unit.DomainScene());
            float radius = actionCallAutoUnit.UmbellateArea.Radius;
            ResetDis(unit.DomainScene(), ref radius, ref actionContext);
            radius *= gameResScale;

            float radiusSq = radius * radius;
            float angle = actionCallAutoUnit.UmbellateArea.Angle;
            float angleHalf = angle * 0.5f;

            bool IgnoringHeight = actionCallAutoUnit.UmbellateArea.IgnoringHeight;
            bool KeepHorizontal = actionCallAutoUnit.UmbellateArea.KeepHorizontal;

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
            }

            if (isResetForward)
            {
                curUnitForward = resetForward;
            }

            if (IgnoringHeight || KeepHorizontal)
            {
                curUnitForward.y = 0;
            }
            curUnitForward = math.normalize(curUnitForward);
            float curUnitAttackPointHeight = ET.Ability.UnitHelper.GetAttackPointHeight(unit);

            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                float disSq;
                if (IgnoringHeight)
                {
                    float3 tmp1 = targetUnit.Position - curUnitPos;
                    tmp1.y = 0;
                    disSq = math.lengthsq(tmp1);
                }
                else
                {
                    disSq = math.lengthsq(targetUnit.Position - curUnitPos);
                }
                if (disSq <= radiusSq)
                {
                    float3 dir = math.normalize(targetUnit.Position - curUnitPos);
                    float angleTmp;
                    if (IgnoringHeight || KeepHorizontal)
                    {
                        dir.y = 0;
                        dir = math.normalize(dir);
                        angleTmp = math.degrees(math.acos(math.clamp(math.dot(curUnitForward, dir), -1, 1)));
                    }
                    else
                    {
                        angleTmp = math.degrees(math.acos(math.clamp(math.dot(curUnitForward, dir), -1, 1)));
                    }
                    if (angleTmp < angleHalf)
                    {
                        if (selectObjectCfg.IsNeedChkMesh)
                        {
                            (bool bHitMesh, float3 hitPos) = ET.Ability.UnitHelper.ChkHitMesh(unit, curUnitPos, curUnitAttackPointHeight, targetUnit);
                            if (bHitMesh)
                            {
                                continue;
                            }
                        }

                        dic.Add(angleTmp, targetUnit);
                    }
                }
            }

            return;
        }

        /// <summary>
        /// 获取方形范围对象列表
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="isResetPos"></param>
        /// <param name="resetPos"></param>
        /// <param name="list"></param>
        /// <param name="dic"></param>
        /// <param name="actionCallAutoUnit"></param>
        public static void _GetUnitsWhenRectangle(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, List<Unit> list, ref MultiMapSimple<float, Unit> dic, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitWhenRectangle actionCallAutoUnit, ref ActionContext actionContext)
        {
            float gameResScale = UnitHelper.GetGameResScale(unit.DomainScene());
            float width = actionCallAutoUnit.RectangleArea.Width;
            float length = actionCallAutoUnit.RectangleArea.Length;
            ResetDis(unit.DomainScene(), ref length, ref actionContext);
            width *= gameResScale;
            length *= gameResScale;

            bool IgnoringHeight = actionCallAutoUnit.RectangleArea.IgnoringHeight;
            bool KeepHorizontal = actionCallAutoUnit.RectangleArea.KeepHorizontal;

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
            }

            if (isResetForward)
            {
                curUnitForward = resetForward;
            }

            if (IgnoringHeight || KeepHorizontal)
            {
                curUnitForward.y = 0;
            }
            curUnitForward = math.normalize(curUnitForward);
            float curUnitAttackPointHeight = ET.Ability.UnitHelper.GetAttackPointHeight(unit);

            float disSq = length * length + (width * 0.5f) * (width * 0.5f);
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                float3 dir = math.normalize(targetUnit.Position - curUnitPos);
                if (IgnoringHeight || KeepHorizontal)
                {
                    dir.y = 0;
                    dir = math.normalize(dir);
                }
                float dot = math.dot(curUnitForward, dir);
                if (dot >= 0)
                {
                    float disSq2;
                    if (IgnoringHeight)
                    {
                        float3 tmp1 = targetUnit.Position - curUnitPos;
                        tmp1.y = 0;
                        disSq2 = math.lengthsq(tmp1);
                    }
                    else
                    {
                        disSq2 = math.lengthsq(targetUnit.Position - curUnitPos);
                    }
                    if (disSq2 > disSq)
                    {
                        continue;
                    }
                    float acos = math.acos(math.clamp(dot, -1, 1));
                    float dis;
                    if (IgnoringHeight)
                    {
                        float3 tmp1 = targetUnit.Position - curUnitPos;
                        tmp1.y = 0;
                        dis = math.length(tmp1);
                    }
                    else
                    {
                        dis = math.length(targetUnit.Position - curUnitPos);
                    }
                    float curHalfWidth = math.sin(acos) * dis;
                    float curLength = math.cos(acos) * dis;
                    //float angleTmp = math.degrees(acos);
                    //Log.Debug($" GetUnitsWhenRectangle dis={dis} curHalfWidth={curHalfWidth} curLength={curLength} angle={math.degrees(acos)}");
                    if (curHalfWidth <= width * 0.5f && curLength <= length)
                    {
                        if (selectObjectCfg.IsNeedChkMesh)
                        {
                            (bool bHitMesh, float3 hitPos) = ET.Ability.UnitHelper.ChkHitMesh(unit, curUnitPos, curUnitAttackPointHeight, targetUnit);
                            if (bHitMesh)
                            {
                                continue;
                            }
                        }

                        dic.Add(dot, targetUnit);
                    }
                }
            }

            return;
        }

        public static void SelectObjectOrderHandle(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg, MultiMapSimple<float, Unit> dic, List<Unit> listTmp, ref SelectHandle selectHandle, ref ActionContext actionContext, bool isNeedChgSelectNumByNumeric)
        {
            int needSelectNum = selectObjectCfg.SelectNum;
            if (needSelectNum == -1)
            {
                needSelectNum = 999;
            }
            else
            {
                if (isNeedChgSelectNumByNumeric)
                {
                    NumericComponent numeric = unit.GetComponent<NumericComponent>();
                    long newSkillSelectNumModifyBase = (long)(needSelectNum * 10000);
                    numeric.SetNoEvent(NumericType.SkillSelectNumModifyBase, newSkillSelectNumModifyBase);

                    needSelectNum = (int)numeric.GetAsFloat(NumericType.SkillSelectNumModify);
                }
            }

            List<SelectObjectOrder> selectOrderList = selectObjectCfg.SelectOrder;
            if (selectOrderList == null || selectOrderList.Count == 0)
            {
                ListComponent<long> tmpList = ListComponent<long>.Create();
                foreach (long unitId in selectHandle.unitIds)
                {
                    Unit unitOne = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitOne))
                    {
                        if (selectObjectCfg.IsNeedIgnoreCannotBeTargeted == false)
                        {
                            bool isCannotBeTargeted = ET.Ability.BuffHelper.ChkCannotBeTargeted(unitOne);
                            if (isCannotBeTargeted)
                            {
                                continue;
                            }
                        }
                        if (selectObjectCfg.IsNeedChkInvisible)
                        {
                            if (ET.Ability.BuffHelper.ChkCanBeSee(unitOne, unit) == false)
                            {
                                continue;
                            }
                        }
                        if (selectObjectCfg.IsNeedChkFly)
                        {
                            if (ET.Ability.BuffHelper.ChkCanBeTouchWhenFly(unitOne, unit) == false)
                            {
                                continue;
                            }
                        }
                        tmpList.Add(unitId);
                        if (tmpList.Count >= needSelectNum)
                        {
                            break;
                        }
                    }
                }
                selectHandle.unitIds.Dispose();
                selectHandle.unitIds = tmpList;
                return;
            }

            listTmp.Clear();
            foreach (long unitId in selectHandle.unitIds)
            {
                Unit unitOne = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                if (UnitHelper.ChkUnitAlive(unitOne))
                {
                    if (selectObjectCfg.IsNeedIgnoreCannotBeTargeted == false)
                    {
                        bool isCannotBeTargeted = ET.Ability.BuffHelper.ChkCannotBeTargeted(unitOne);
                        if (isCannotBeTargeted)
                        {
                            continue;
                        }
                    }
                    if (selectObjectCfg.IsNeedChkInvisible)
                    {
                        if (ET.Ability.BuffHelper.ChkCanBeSee(unitOne, unit) == false)
                        {
                            continue;
                        }
                    }
                    if (selectObjectCfg.IsNeedChkFly)
                    {
                        if (ET.Ability.BuffHelper.ChkCanBeTouchWhenFly(unitOne, unit) == false)
                        {
                            continue;
                        }
                    }
                    listTmp.Add(unitOne);
                }
            }

            selectHandle.unitIds.Clear();
            for (int i = 0; i < selectOrderList.Count; i++)
            {
                SelectObjectOrder selectObjectOrder = selectOrderList[i];

                bool isDescending = selectObjectOrder.IsDescending;
                dic.Clear();
                if (selectObjectOrder is DisOrder)
                {
                    SelectObjectOrderHandle_DisOrder(unit, isResetPos, resetPos, isResetForward, resetForward, listTmp, dic);
                }
                else if (selectObjectOrder is AngleOrder)
                {
                    SelectObjectOrderHandle_AngleOrder(unit, isResetPos, resetPos, isResetForward, resetForward, listTmp, dic);
                }
                else if (selectObjectOrder is CurHpOrder)
                {
                    SelectObjectOrderHandle_CurHpOrder(listTmp, dic);
                }
                else if (selectObjectOrder is BuffStackOrder buffStackOrder)
                {
                    SelectObjectOrderHandle_BuffStackOrder(buffStackOrder.BuffDealSelectCondition, listTmp, dic, actionContext);
                }
                else if (selectObjectOrder is BuffExistOrder buffExistOrder)
                {
                    SelectObjectOrderHandle_BuffExistOrder(buffExistOrder.BuffDealSelectCondition, listTmp, dic);
                }
                else if (selectObjectOrder is DisHomeOrder)
                {
                    SelectObjectOrderHandle_DisHomeOrder(unit, listTmp, dic);
                }
                else if (selectObjectOrder is TargetExcludeOrder)
                {
                    SelectObjectOrderHandle_TargetExcludeOrder(unit, listTmp, dic);
                }
                else if (selectObjectOrder is RandomOrder)
                {
                    SelectObjectOrderHandle_RandomOrder(unit, listTmp, dic);
                }

                int needCount = needSelectNum - selectHandle.unitIds.Count;
                foreach (var sortList in dic)
                {
                    needCount -= sortList.Value.Count;
                }
                if (needCount < 0)
                {
                    //数量足够，排序筛选
                    IOrderedEnumerable<KeyValuePair<float, List<Unit>>> orderedEnumerable = null;
                    if (isDescending)
                    {
                        orderedEnumerable = dic.OrderByDescending(kv => kv.Key);
                    }
                    else
                    {
                        orderedEnumerable = dic.OrderBy(kv => kv.Key);
                    }

                    if (i == selectOrderList.Count - 1)
                    {
                        //已经是最后一条规则
                        foreach (var item in orderedEnumerable)
                        {
                            foreach (var item2 in item.Value)
                            {
                                selectHandle.unitIds.Add(item2.Id);
                                if (selectHandle.unitIds.Count >= needSelectNum)
                                {
                                    return;
                                }
                            }
                        }
                        return;
                    }
                    else
                    {
                        foreach (var item in orderedEnumerable)
                        {
                            if (selectHandle.unitIds.Count >= needSelectNum)
                            {
                                return;
                            }
                            if (selectHandle.unitIds.Count + item.Value.Count > needSelectNum)
                            {
                                //这一部分继续应用下个规则进行筛选
                                listTmp.Clear();
                                foreach (var item2 in item.Value)
                                {
                                    Unit unitOne = item2;
                                    listTmp.Add(unitOne);
                                }
                                break;
                            }
                            else
                            {
                                foreach (var item2 in item.Value)
                                {
                                    Unit unitOne = item2;
                                    selectHandle.unitIds.Add(unitOne.Id);
                                    listTmp.Remove(unitOne);
                                    if (selectHandle.unitIds.Count >= needSelectNum)
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //数量不足够，全部选择
                    IOrderedEnumerable<KeyValuePair<float, List<Unit>>> orderedEnumerable = null;
                    if (isDescending)
                    {
                        orderedEnumerable = dic.OrderByDescending(kv => kv.Key);
                    }
                    else
                    {
                        orderedEnumerable = dic.OrderBy(kv => kv.Key);
                    }
                    foreach (var item in orderedEnumerable)
                    {
                        foreach (var item2 in item.Value)
                        {
                            selectHandle.unitIds.Add(item2.Id);
                        }
                    }

                    return;
                }
            }

        }

        public static void SelectObjectOrderHandle_DisOrder(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, List<Unit> list, MultiMapSimple<float, Unit> dic)
        {
            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
            }

            if (isResetForward)
            {
                curUnitForward = resetForward;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                float disSq;
                float3 tmp1 = targetUnit.Position - curUnitPos;
                tmp1.y = 0;
                disSq = math.lengthsq(tmp1);
                dic.Add(disSq, targetUnit);
            }
        }

        public static void SelectObjectOrderHandle_AngleOrder(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, List<Unit> list, MultiMapSimple<float, Unit> dic)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                float3 curUnitPos;
                float3 curUnitForward;
                if (isResetPos)
                {
                    (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
                }
                else
                {
                    (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
                }

                if (isResetForward)
                {
                    curUnitForward = resetForward;
                }

                float3 dir = math.normalize(targetUnit.Position - curUnitPos);
                float angleTmp;
                dir.y = 0;
                dir = math.normalize(dir);
                angleTmp = math.degrees(math.acos(math.clamp(math.dot(curUnitForward, dir), -1, 1)));

                dic.Add(angleTmp, targetUnit);
            }
        }

        public static void SelectObjectOrderHandle_CurHpOrder(List<Unit> list, MultiMapSimple<float, Unit> dic)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                NumericComponent numericComponent = targetUnit.GetComponent<NumericComponent>();
                float curHp = numericComponent.GetAsFloat(NumericType.Hp);

                dic.Add(curHp, targetUnit);
            }
        }

        public static void SelectObjectOrderHandle_BuffStackOrder(BuffDealSelectCondition buffDealSelectCondition, List<Unit> list, MultiMapSimple<float, Unit> dic, ActionContext actionContext)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];

                List<BuffObj> buffObjList = BuffHelper.GetBuffListByCondition(targetUnit, buffDealSelectCondition, ref actionContext);
                if (buffObjList != null)
                {
                    int maxStack = 0;
                    foreach (BuffObj buffObj in buffObjList)
                    {
                        if (buffObj.isEnabled && maxStack < buffObj.stack)
                        {
                            maxStack = buffObj.stack;
                        }
                    }
                    dic.Add(maxStack, targetUnit);
                }

            }
        }

        public static void SelectObjectOrderHandle_BuffExistOrder(BuffDealSelectCondition buffDealSelectCondition, List<Unit> list, MultiMapSimple<float, Unit> dic)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];

                bool bExist = BuffHelper.ChkBuffByCondition(targetUnit, buffDealSelectCondition);
                if (bExist)
                {
                    dic.Add(1, targetUnit);
                }
                else
                {
                    dic.Add(0, targetUnit);
                }
            }
        }

        public static void SelectObjectOrderHandle_TargetExcludeOrder(Unit unit, List<Unit> list, MultiMapSimple<float, Unit> dic)
        {
            HashSet<long> excludeUnitList = UnitHelper.GetSaveExcludeSelectHandle(unit);
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];

                bool bExistExclude = false;
                if (excludeUnitList != null && excludeUnitList.Count > 0)
                {
                    if (excludeUnitList.Contains(targetUnit.Id))
                    {
                        bExistExclude = true;
                    }
                }
                if (bExistExclude)
                {
                    dic.Add(1, targetUnit);
                }
                else
                {
                    dic.Add(2, targetUnit);
                }
            }
        }

        public static void SelectObjectOrderHandle_DisHomeOrder(Unit unit, List<Unit> list, MultiMapSimple<float, Unit> dic)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
            if (gamePlayTowerDefenseComponent == null)
            {
                return;
            }
            PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
            if (putHomeComponent == null)
            {
                return;
            }

            Unit homeUnit = putHomeComponent.GetHomeUnit(unit);
            float3 curUnitPos = homeUnit.Position;

            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                float disSq;
                float3 tmp1 = targetUnit.Position - curUnitPos;
                tmp1.y = 0;
                disSq = math.lengthsq(tmp1);
                dic.Add(disSq, targetUnit);
            }
        }

        public static void SelectObjectOrderHandle_RandomOrder(Unit unit, List<Unit> list, MultiMapSimple<float, Unit> dic)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                int randomIndex = RandomGenerator.RandomNumber(0, list.Count);
                dic.Add(randomIndex, targetUnit);
            }
        }

        /// <summary>
        /// 将选择的unit转换为位置
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="selectHandle"></param>
        public static void DealWhenIsChgToSelectPos(Unit unit, ref SelectHandle selectHandle)
        {
            selectHandle.selectHandleType = SelectHandleType.SelectPosition;
            if (selectHandle.unitIds.Count > 0)
            {
                selectHandle.position = float3.zero;
                for (int i = 0; i < selectHandle.unitIds.Count; i++)
                {
                    Unit selectUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);
                    selectHandle.position += selectUnit.Position;
                }
                selectHandle.position /= selectHandle.unitIds.Count;
                selectHandle.direction = selectHandle.position - unit.Position;
            }
            else
            {
                selectHandle.position = float3.zero;
                selectHandle.direction = unit.Forward;
            }
        }

        public static ListComponent<Unit> GetSelectUnitList(Unit unit, SelectHandle selectHandle, ref ActionContext actionContext, bool isContainDeathShow = false)
        {
            if (selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                Log.Error($"ET.Ability.SelectHandleHelper.GetSelectUnitList selectHandle.selectHandleType[{selectHandle.selectHandleType}] != SelectHandleType.SelectUnits");
                return null;
            }

            ListComponent<Unit> selectUnits = ListComponent<Unit>.Create();
            for (int i = 0; i < selectHandle.unitIds.Count; i++)
            {
                Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);
                if (UnitHelper.ChkUnitAlive(targetUnit, isContainDeathShow) == false)
                {
                    continue;
                }
                selectUnits.Add(targetUnit);
            }

            return selectUnits;
        }

        ///--------------------

        public static bool ChkUnitsInArea(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitArea actionCallAutoUnitArea, SelectHandle saveSelectHandle, ref ActionContext actionContext)
        {
            int selectNum = selectObjectCfg.SelectNum;
            if (selectNum == -1)
            {
                return false;
            }
            if (selectNum != saveSelectHandle.unitIds.Count)
            {
                return false;
            }

            if (selectObjectCfg.IsNeedChkExcludeTarget)
            {
                HashSet<long> excludeUnitList = UnitHelper.GetSaveExcludeSelectHandle(unit);
                if (excludeUnitList != null && excludeUnitList.Count > 0)
                {
                    foreach (long unitId in saveSelectHandle.unitIds)
                    {
                        if (excludeUnitList.Contains(unitId))
                        {
                            return false;
                        }
                    }
                }
            }

            using ListComponent<Unit> list = ListComponent<Unit>.Create();
            foreach (long unitId in saveSelectHandle.unitIds)
            {
                Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                if (UnitHelper.ChkUnitAlive(unitSelect) == false)
                {
                    return false;
                }

                if (selectObjectCfg.IsNeedIgnoreCannotBeTargeted == false)
                {
                    bool isCannotBeTargeted = ET.Ability.BuffHelper.ChkCannotBeTargeted(unitSelect);
                    if (isCannotBeTargeted)
                    {
                        return false;
                    }
                }
                if (selectObjectCfg.IsNeedChkInvisible)
                {
                    if (ET.Ability.BuffHelper.ChkCanBeSee(unitSelect, unit) == false)
                    {
                        return false;
                    }
                }
                if (selectObjectCfg.IsNeedChkFly)
                {
                    if (ET.Ability.BuffHelper.ChkCanBeTouchWhenFly(unitSelect, unit) == false)
                    {
                        return false;
                    }
                }
                list.Add(unitSelect);
            }

            if (actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate actionCallAutoUnitWhenUmbellate)
            {
                return ChkUnitsInAreaWhenUmbellate(unit, isResetPos, resetPos, isResetForward, resetForward, list, selectObjectCfg, actionCallAutoUnitWhenUmbellate, ref actionContext);
            }
            else if (actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle actionCallAutoUnitWhenRectangle)
            {
                return ChkUnitsInAreaWhenRectangle(unit, isResetPos, resetPos, isResetForward, resetForward, list, selectObjectCfg, actionCallAutoUnitWhenRectangle, ref actionContext);
            }

            return true;
        }

        public static bool ChkUnitsInAreaWhenUmbellate(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, List<Unit> list, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitWhenUmbellate actionCallAutoUnit, ref ActionContext actionContext)
        {
            float gameResScale = UnitHelper.GetGameResScale(unit.DomainScene());
            float radius = actionCallAutoUnit.UmbellateArea.Radius;
            ResetDis(unit.DomainScene(), ref radius, ref actionContext);
            radius *= gameResScale;

            float radiusSq = radius * radius;
            float angle = actionCallAutoUnit.UmbellateArea.Angle;
            float angleHalf = angle * 0.5f;

            bool IgnoringHeight = actionCallAutoUnit.UmbellateArea.IgnoringHeight;
            bool KeepHorizontal = actionCallAutoUnit.UmbellateArea.KeepHorizontal;

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
            }

            if (isResetForward)
            {
                curUnitForward = resetForward;
            }

            if (IgnoringHeight || KeepHorizontal)
            {
                curUnitForward.y = 0;
            }
            curUnitForward = math.normalize(curUnitForward);
            float curUnitAttackPointHeight = ET.Ability.UnitHelper.GetAttackPointHeight(unit);

            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                if (UnitHelper.ChkUnitAlive(targetUnit) == false)
                {
                    return false;
                }
                float disSq;
                if (IgnoringHeight)
                {
                    float3 tmp1 = targetUnit.Position - curUnitPos;
                    tmp1.y = 0;
                    disSq = math.lengthsq(tmp1);
                }
                else
                {
                    disSq = math.lengthsq(targetUnit.Position - curUnitPos);
                }
                if (disSq <= radiusSq)
                {
                    float3 dir = math.normalize(targetUnit.Position - curUnitPos);
                    float angleTmp;
                    if (IgnoringHeight || KeepHorizontal)
                    {
                        dir.y = 0;
                        angleTmp = math.degrees(math.acos(math.clamp(math.dot(curUnitForward, dir), -1, 1)));
                    }
                    else
                    {
                        angleTmp = math.degrees(math.acos(math.clamp(math.dot(curUnitForward, dir), -1, 1)));
                    }
                    if (angleTmp < angleHalf)
                    {
                        if (selectObjectCfg.IsNeedChkMesh)
                        {
                            (bool bHitMesh, float3 hitPos) = ET.Ability.UnitHelper.ChkHitMesh(unit, curUnitPos, curUnitAttackPointHeight, targetUnit);
                            if (bHitMesh)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ChkUnitsInAreaWhenRectangle(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, List<Unit> list, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitWhenRectangle actionCallAutoUnit, ref ActionContext actionContext)
        {
            float gameResScale = UnitHelper.GetGameResScale(unit.DomainScene());
            float width = actionCallAutoUnit.RectangleArea.Width;
            float length = actionCallAutoUnit.RectangleArea.Length;
            ResetDis(unit.DomainScene(), ref length, ref actionContext);
            width *= gameResScale;
            length *= gameResScale;

            bool IgnoringHeight = actionCallAutoUnit.RectangleArea.IgnoringHeight;
            bool KeepHorizontal = actionCallAutoUnit.RectangleArea.KeepHorizontal;

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
            }

            if (isResetForward)
            {
                curUnitForward = resetForward;
            }

            if (IgnoringHeight || KeepHorizontal)
            {
                curUnitForward.y = 0;
            }
            curUnitForward = math.normalize(curUnitForward);

            float curUnitAttackPointHeight = ET.Ability.UnitHelper.GetAttackPointHeight(unit);
            float disSq = length * length + (width * 0.5f) * (width * 0.5f);
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                if (UnitHelper.ChkUnitAlive(targetUnit) == false)
                {
                    return false;
                }
                float3 dir = math.normalize(targetUnit.Position - curUnitPos);
                if (IgnoringHeight || KeepHorizontal)
                {
                    dir.y = 0;
                }
                float dot = math.dot(curUnitForward, dir);
                if (dot >= 0)
                {
                    float disSq2;
                    if (IgnoringHeight)
                    {
                        float3 tmp1 = targetUnit.Position - curUnitPos;
                        tmp1.y = 0;
                        disSq2 = math.lengthsq(tmp1);
                    }
                    else
                    {
                        disSq2 = math.lengthsq(targetUnit.Position - curUnitPos);
                    }
                    if (disSq2 > disSq)
                    {
                        return false;
                    }

                    float acos = math.acos(math.clamp(dot, -1, 1));
                    float dis;
                    if (IgnoringHeight)
                    {
                        float3 tmp1 = targetUnit.Position - curUnitPos;
                        tmp1.y = 0;
                        dis = math.length(tmp1);
                    }
                    else
                    {
                        dis = math.length(targetUnit.Position - curUnitPos);
                    }
                    float curHalfWidth = math.sin(acos) * dis;
                    float curLength = math.cos(acos) * dis;
                    //float angleTmp = math.degrees(acos);
                    //Log.Debug($" GetUnitsWhenRectangle dis={dis} curHalfWidth={curHalfWidth} curLength={curLength} angle={math.degrees(acos)}");
                    if (curHalfWidth <= width * 0.5f && curLength <= length)
                    {
                        if (selectObjectCfg.IsNeedChkMesh)
                        {
                            (bool bHitMesh, float3 hitPos) = ET.Ability.UnitHelper.ChkHitMesh(unit, curUnitPos, curUnitAttackPointHeight, targetUnit);
                            if (bHitMesh)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static void ResetDis(Scene scene, ref float dis, ref ActionContext actionContext)
        {
            if (string.IsNullOrEmpty(actionContext.skillCfgId) || actionContext.skillDis <= 0)
            {
                return;
            }
            if (dis > actionContext.skillDis)
            {
                //dis = actionContext.skillDis * 1.2f;
                if (actionContext.skillDis <= 2)
                {
                    dis = actionContext.skillDis + 1.5f;
                }
                else
                {
                    dis = actionContext.skillDis + 1.5f;
                }
            }

            Unit unit = UnitHelper.GetUnit(scene, actionContext.unitId);
            if (unit == null)
            {
                return;
            }
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            if (numericComponent == null)
            {
                return;
            }
            float maxAttackDis = numericComponent.GetAsFloat(NumericType.MaxAttackDis);
            if (dis > maxAttackDis && maxAttackDis > 0)
            {
                dis = maxAttackDis;
            }
        }
    }
}