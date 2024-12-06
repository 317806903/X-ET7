using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using ET;
using Unity.Mathematics;

namespace ET.Client
{
    public static class UnitViewHelper
    {
        public static async ETTask<bool> ChkGameObjectShowReady(Entity self, Unit unit)
        {
            bool isReady = false;
            while (isReady == false)
            {
                GameObjectShowComponent gameObjectShowComponent = unit.GetComponent<GameObjectShowComponent>();
                if (gameObjectShowComponent != null && gameObjectShowComponent.GetGo() != null)
                {
                    isReady = true;
                    return true;
                }
                await TimerComponent.Instance.WaitFrameAsync();
                if ((self != null && self.IsDisposed) || unit.IsDisposed)
                {
                    return false;
                }
            }
            return true;
        }

        public static SelectHandle CreateSelectHandle(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg, ref ActionContext actionContext)
        {
            if (unit == null)
            {
                return null;
            }
            ActionCallParam actionCallParam = selectObjectCfg.ActionCallParam;
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
            else if (actionCallParam is ActionCallAutoUnit)
            {
                selectHandle.selectHandleType = SelectHandleType.SelectUnits;
                selectHandle.unitIds = ListComponent<long>.Create();
                if (actionCallParam is ActionCallAutoUnitArea actionCallAutoUnitArea)
                {
                    GetUnitsByArea(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, actionCallAutoUnitArea, selectHandle, ref actionContext);
                }
            }

            return selectHandle;
        }

        public static void GetUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitArea actionCallAutoUnitArea, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            List<Unit> list = GetUnitList(unit, selectObjectCfg.SelectObjectUnitType, selectObjectCfg.IsNeedChkCanBeFind);

            if(list == null || list.Count == 0)
            {
                selectHandle.unitIds.Clear();
                return;
            }

            SelectHandleHelper.tmp_dic.Clear();
            SelectHandleHelper.tmp_dic2.Clear();
            SelectHandleHelper.tmp_list1.Clear();

            MultiMapSimple<float, Unit> dic = SelectHandleHelper.tmp_dic;
            if (actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate actionCallAutoUnitWhenUmbellate)
            {
                SelectHandleHelper._GetUnitsWhenUmbellate(unit, isResetPos, resetPos, isResetForward, resetForward, list, dic, selectObjectCfg, actionCallAutoUnitWhenUmbellate, ref actionContext);
            }
            else if (actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle actionCallAutoUnitWhenRectangle)
            {
                SelectHandleHelper._GetUnitsWhenRectangle(unit, isResetPos, resetPos, isResetForward, resetForward, list, dic, selectObjectCfg, actionCallAutoUnitWhenRectangle, ref actionContext);
            }

            selectHandle.unitIds.Clear();
            foreach (var sortList in dic)
            {
                for (int i = 0; i < sortList.Value.Count; i++)
                {
                    Unit unitOne = sortList.Value[i];
                    if (ET.Ability.UnitHelper.ChkUnitAlive(unitOne))
                    {
                        selectHandle.unitIds.Add(unitOne.Id);
                    }
                }
            }

            (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = UnitConditionHandleHelper.ChkCondition(unit, selectHandle, selectObjectCfg.SelectPreCondition, ref actionContext);
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

            SelectHandleHelper.SelectObjectOrderHandle(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, dic, SelectHandleHelper.tmp_list1, selectHandle, ref actionContext);

            dic.Clear();
            SelectHandleHelper.tmp_dic.Clear();
            SelectHandleHelper.tmp_dic2.Clear();
            SelectHandleHelper.tmp_list1.Clear();
        }

        public static List<Unit> GetUnitList(Unit curUnit, SelectObjectUnitTypeBase selectObjectUnitTypeBase, bool isNeedChkCanBeFind)
        {
            bool isContainHome = false;
            bool isContainSelf = false;
            bool isContainFriend = false;
            bool isContainHostile = false;
            SelectObjectUnitType selectObjectUnitType;
            if (selectObjectUnitTypeBase is SelectObjectUnitTypeSelf selectObjectUnitTypeSelf)
            {
                isContainHome = false;
                isContainSelf = true;
                isContainFriend = false;
                isContainHostile = false;
                selectObjectUnitType = selectObjectUnitTypeSelf.UnitType;
            }
            else if (selectObjectUnitTypeBase is SelectObjectUnitTypeSelfAndFriend selectObjectUnitTypeSelfAndFriend)
            {
                isContainHome = false;
                isContainSelf = true;
                isContainFriend = true;
                isContainHostile = false;
                selectObjectUnitType = selectObjectUnitTypeSelfAndFriend.UnitType;
            }
            else if (selectObjectUnitTypeBase is SelectObjectUnitTypeFriend selectObjectUnitTypeFriend)
            {
                isContainHome = false;
                isContainSelf = false;
                isContainFriend = true;
                isContainHostile = false;
                selectObjectUnitType = selectObjectUnitTypeFriend.UnitType;
            }
            else if (selectObjectUnitTypeBase is SelectObjectUnitTypeHostile selectObjectUnitTypeHostile)
            {
                isContainHome = false;
                isContainSelf = false;
                isContainFriend = false;
                isContainHostile = true;
                selectObjectUnitType = selectObjectUnitTypeHostile.UnitType;
            }
            else if (selectObjectUnitTypeBase is SelectObjectUnitTypeSelfHome selectObjectUnitTypeSelfHome)
            {
                isContainHome = true;
                isContainSelf = true;
                isContainFriend = false;
                isContainHostile = false;
                selectObjectUnitType = SelectObjectUnitType.All;
            }
            else if (selectObjectUnitTypeBase is SelectObjectUnitTypeSelfAndFriendHome selectObjectUnitTypeSelfAndFriendHome)
            {
                isContainHome = true;
                isContainSelf = true;
                isContainFriend = true;
                isContainHostile = false;
                selectObjectUnitType = SelectObjectUnitType.All;
            }
            else if (selectObjectUnitTypeBase is SelectObjectUnitTypeFriendHome selectObjectUnitTypeFriendHome)
            {
                isContainHome = true;
                isContainSelf = false;
                isContainFriend = true;
                isContainHostile = false;
                selectObjectUnitType = SelectObjectUnitType.All;
            }
            else if (selectObjectUnitTypeBase is SelectObjectUnitTypeHostileHome selectObjectUnitTypeHostileHome)
            {
                isContainHome = true;
                isContainSelf = false;
                isContainFriend = false;
                isContainHostile = true;
                selectObjectUnitType = SelectObjectUnitType.All;
            }
            else
            {
                // isContainHome = false;
                // isContainSelf = false;
                // isContainFriend = false;
                // isContainHostile = false;
                // selectObjectUnitType = SelectObjectUnitType.All;
                return null;
            }

            List<Unit> unitList = ListComponent<Unit>.Create();

            if (isContainHome)
            {
            }
            else
            {
                UnitComponent unitComponent = ET.Client.UnitHelper.GetUnitComponent(curUnit.DomainScene());
                foreach (Unit unit in unitComponent.Children.Values)
                {
                    bool isContinue = false;
                    if (curUnit == unit)
                    {
                        continue;
                    }
                    if (ET.Ability.UnitHelper.ChkIsPlayer(unit))
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
                    else if (ET.Ability.UnitHelper.ChkIsActor(unit))
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

                    if (ET.Ability.UnitHelper.ChkUnitAlive(unit) == false)
                    {
                        continue;
                    }

                    if (isContainHostile)
                    {
                        bool isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit);
                        if (isFriend)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (isContainSelf && isContainFriend)
                        {
                            bool isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else if (isContainSelf)
                        {
                            bool isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit, true);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else if (isContainFriend)
                        {
                            bool isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit, true);
                            if (isFriend)
                            {
                                continue;
                            }
                            isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit, false);
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

                    if (isNeedChkCanBeFind)
                    {
                        bool isBeFind = true;//ET.Ability.BuffHelper.ChkCanBeFind(unit, curUnit);
                        if (isBeFind == false)
                        {
                            continue;
                        }
                    }

                    unitList.Add(unit);
                }

            }
            return unitList;
        }

    }
}