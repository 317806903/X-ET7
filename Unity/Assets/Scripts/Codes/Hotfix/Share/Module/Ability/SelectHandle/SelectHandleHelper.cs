using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class SelectHandleHelper
    {
        public static (SelectHandle, Unit) DealSelectHandler(Unit triggerUnit, SelectObjectCfg selectObjectCfg, Unit onAttackUnit, Unit beHurtUnit, ref ActionContext actionContext)
        {
            if (onAttackUnit != null)
            {
                actionContext.attackerUnitId = onAttackUnit.Id;
            }

            Unit resetPosByUnit = null;
            SelectHandle selectHandle;
            if (selectObjectCfg.ActionCallParam is ActionCallSelectLast)
            {
                selectHandle = UnitHelper.GetSaveSelectHandle(triggerUnit);
            }
            else if (selectObjectCfg.ActionCallParam is ActionCallAutoUnit actionCallAutoUnit)
            {
                selectHandle = SelectHandleHelper.CreateSelectHandle(triggerUnit, beHurtUnit, selectObjectCfg, ref actionContext);
            }
            else if (selectObjectCfg.ActionCallParam is ActionCallAutoSelf actionCallAutoSelf)
            {
                selectHandle = SelectHandleHelper.CreateSelectHandle(triggerUnit, null, selectObjectCfg, ref actionContext);
            }
            else if (selectObjectCfg.ActionCallParam is ActionCallOnAoeChgUnit actionCallOnAoeChgUnit)
            {
                selectHandle = SelectHandleHelper.CreateAoeUnitSelectHandle(triggerUnit, selectObjectCfg, true);
            }
            else if (selectObjectCfg.ActionCallParam is ActionCallOnAoeInUnit actionCallOnAoeInUnit)
            {
                selectHandle = SelectHandleHelper.CreateAoeUnitSelectHandle(triggerUnit, selectObjectCfg, false);
            }
            else
            {
                Unit targetUnit;
                if (selectObjectCfg.ActionCallParam is ActionCallCasterUnit actionCallCasterUnit)
                {
                    targetUnit = UnitHelper.GetCasterUnit(triggerUnit);
                }
                else if (selectObjectCfg.ActionCallParam is ActionCallCasterPlayerUnit actionCallCasterPlayerUnit)
                {
                    targetUnit = UnitHelper.GetCasterActorUnit(triggerUnit);
                }
                else if (selectObjectCfg.ActionCallParam is ActionCallOnAttackUnit actionCallOnAttackUnit)
                {
                    targetUnit = onAttackUnit;
                }
                else if (selectObjectCfg.ActionCallParam is ActionCallBeHurtUnit actionCallBeHurtUnit)
                {
                    targetUnit = beHurtUnit;
                }
                else
                {
                    targetUnit = triggerUnit;
                }

                resetPosByUnit = targetUnit;
                selectHandle = SelectHandleHelper.CreateUnitSelectHandle(triggerUnit, targetUnit, selectObjectCfg);
            }

            if (selectHandle == null)
            {
                return (null, null);
            }

            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits && selectHandle.unitIds.Count == 0)
            {
#if UNITY_EDITOR
                Log.Error($"selectHandle.selectHandleType == SelectHandleType.SelectUnits && selectHandle.unitIds.Count == 0");
#endif
                return (null, null);
            }
            return (selectHandle, resetPosByUnit);
        }

        public static SelectHandle CreateSelectHandle(Unit unit, Unit resetPosByUnit, SelectObjectCfg selectObjectCfg, ref ActionContext actionContext, bool canUseRecordResult = true)
        {
            bool isResetPos = false;
            float3 resetPos = float3.zero;
            if (resetPosByUnit != null)
            {
                isResetPos = true;
                resetPos = resetPosByUnit.Position;
            }
            return CreateSelectHandle(unit, isResetPos, resetPos, selectObjectCfg, ref actionContext, canUseRecordResult);
        }

        public static SelectHandle CreateSelectHandle(Unit unit, bool isResetPos, float3 resetPos, SelectObjectCfg selectObjectCfg, ref ActionContext actionContext, bool canUseRecordResult = true)
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
                    Log.Error($" ActionCallSelectLast saveSelectHandle == null");
                }
                return saveSelectHandle;
            }

            bool bRet;
            (bRet, saveSelectHandle) = _ChkIsUseSaveSelectHandle(unit, isResetPos, resetPos, selectObjectCfg, ref actionContext);
            if (bRet)
            {
                return saveSelectHandle;
            }

            SelectHandle selectHandle = SelectHandle.Create();
            if (actionCallParam is ActionCallSelectDirection)
            {
                selectHandle.selectHandleType = SelectHandleType.SelectDirection;
                //消息
            }
            else if (actionCallParam is ActionCallSelectPosition)
            {
                selectHandle.selectHandleType = SelectHandleType.SelectPosition;
                //消息
            }
            else if (actionCallParam is ActionCallSelectUnit)
            {
                selectHandle.selectHandleType = SelectHandleType.SelectUnits;
                //消息
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
                    bool bRecord = false;
                    ListComponent<long > recordUnitIds = null;
                    if (canUseRecordResult)
                    {
                        (bRecord, recordUnitIds) = ChkRecordUnitsByArea(unit, isResetPos, resetPos, selectObjectCfg);
                    }
                    if (bRecord)
                    {
                        selectHandle.unitIds.Clear();
                        selectHandle.unitIds.AddRange(recordUnitIds);
                    }
                    else
                    {
                        GetUnitsByArea(unit, isResetPos, resetPos, selectObjectCfg, actionCallAutoUnitArea, selectHandle, ref actionContext);

                        DoRecordUnitsByArea(unit, isResetPos, resetPos, selectObjectCfg, selectHandle.unitIds);
                    }

                }
                else if (actionCallParam is ActionCallAutoUnitOne actionCallAutoUnitOne)
                {
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

            return selectHandle;
        }

        public static (bool, SelectHandle) _ChkIsUseSaveSelectHandle(Unit unit, bool isResetPos, float3 resetPos, SelectObjectCfg selectObjectCfg,
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

            if (actionCallParam is ActionCallSelectDirection)
            {
                if (saveSelectHandle.selectHandleType != SelectHandleType.SelectDirection)
                {
                    return (false, null);
                }
            }
            else if (actionCallParam is ActionCallSelectPosition)
            {
                if (saveSelectHandle.selectHandleType != SelectHandleType.SelectPosition)
                {
                    return (false, null);
                }
            }
            else if (actionCallParam is ActionCallSelectUnit)
            {
                if (saveSelectHandle.selectHandleType != SelectHandleType.SelectUnits)
                {
                    return (false, null);
                }
            }
            else if (actionCallParam is ActionCallAutoSelf)
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
                    bool bRet = ChkUnitsInArea(unit, isResetPos, resetPos, selectObjectCfg, actionCallAutoUnitArea, saveSelectHandle, ref actionContext);
                    if (bRet == false)
                    {
                        return (false, null);
                    }
                }
                else if (actionCallParam is ActionCallAutoUnitOne actionCallAutoUnitOne)
                {
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
                    bool isBeFind = ET.Ability.BuffHelper.ChkCanBeFind(selectUnit, unit);
                    if (isBeFind == false)
                    {
                        return (false, null);
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

        public static SelectHandle CreateAoeUnitSelectHandle(Unit triggerUnit, SelectObjectCfg selectObjectCfg, bool isChgAoe)
        {
            SelectHandle selectHandle = SelectHandleHelper.CreateUnitNoneSelectHandle();
            if (UnitHelper.ChkIsAoe(triggerUnit))
            {
                AoeObj aoeObj = triggerUnit.GetComponent<AoeObj>();
                if (aoeObj == null)
                {
#if UNITY_EDITOR
                    Log.Error($"aoeObj == null");
#endif
                }
                else
                {
                    bool isNeedChkIsFriend = false;
                    bool isNeedChkIsPlayer = false;
                    bool isFriend = false;
                    bool isOnlyPlayer = false;

                    if (selectObjectCfg.SelectObjectType == SelectObjectType.FriendPlayers)
                    {
                        isNeedChkIsFriend = true;
                        isNeedChkIsPlayer = true;
                        isFriend = true;
                        isOnlyPlayer = true;
                    }
                    else if (selectObjectCfg.SelectObjectType == SelectObjectType.FriendButNotPlayers)
                    {
                        isNeedChkIsFriend = true;
                        isNeedChkIsPlayer = true;
                        isFriend = true;
                        isOnlyPlayer = false;
                    }
                    else if (selectObjectCfg.SelectObjectType == SelectObjectType.Friends)
                    {
                        isNeedChkIsFriend = true;
                        isNeedChkIsPlayer = false;
                        isFriend = true;
                        isOnlyPlayer = false;
                    }
                    else if (selectObjectCfg.SelectObjectType == SelectObjectType.HostilePlayers)
                    {
                        isNeedChkIsFriend = true;
                        isNeedChkIsPlayer = true;
                        isFriend = false;
                        isOnlyPlayer = true;
                    }
                    else if (selectObjectCfg.SelectObjectType == SelectObjectType.HostileButNotPlayers)
                    {
                        isNeedChkIsFriend = true;
                        isNeedChkIsPlayer = true;
                        isFriend = false;
                        isOnlyPlayer = false;
                    }
                    else if (selectObjectCfg.SelectObjectType == SelectObjectType.Hostiles)
                    {
                        isNeedChkIsFriend = true;
                        isNeedChkIsPlayer = false;
                        isFriend = false;
                        isOnlyPlayer = false;
                    }
                    else if (selectObjectCfg.SelectObjectType == SelectObjectType.AllPlayers)
                    {
                        isNeedChkIsFriend = false;
                        isNeedChkIsPlayer = true;
                        isFriend = false;
                        isOnlyPlayer = true;
                    }
                    else if (selectObjectCfg.SelectObjectType == SelectObjectType.AllButNotPlayers)
                    {
                        isNeedChkIsFriend = false;
                        isNeedChkIsPlayer = true;
                        isFriend = false;
                        isOnlyPlayer = false;
                    }

                    HashSet<long> unitList;
                    if (isChgAoe)
                    {
                        unitList = aoeObj.chgUnitList;
                    }
                    else
                    {
                        unitList = aoeObj.unitIds;
                    }
                    foreach (long unitId in unitList)
                    {
                        Unit unitSelect = UnitHelper.GetUnit(triggerUnit.DomainScene(), unitId);
                        if (UnitHelper.ChkUnitAlive(unitSelect) == false)
                        {
                            continue;
                        }

                        if (isNeedChkIsFriend)
                        {
                            if (isFriend && ET.GamePlayHelper.ChkIsFriend(unitSelect, triggerUnit) == false)
                            {
                                continue;
                            }
                            else if (isFriend == false && ET.GamePlayHelper.ChkIsFriend(unitSelect, triggerUnit))
                            {
                                continue;
                            }
                        }
                        if (isNeedChkIsPlayer)
                        {
                            if (isOnlyPlayer && UnitHelper.ChkIsPlayer(unitSelect) == false)
                            {
                                continue;
                            }
                            else if (isOnlyPlayer == false && UnitHelper.ChkIsPlayer(unitSelect))
                            {
                                continue;
                            }
                        }
                        selectHandle.unitIds.Add(unitId);
                    }
                }
            }

            return selectHandle;
        }

        private static MultiMapSimple<float, Unit> tmp_dic = new();
        private static MultiMapSimple<float, Unit> tmp_dic2 = new();
        private static List<Unit> tmp_list1 = new();

        public static void DoRecordUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, SelectObjectCfg selectObjectCfg, ListComponent<long> unitIds)
        {
            SelectHandleRecordManager selectHandleRecordManager = unit.DomainScene().GetComponent<SelectHandleRecordManager>();
            selectHandleRecordManager.DoRecordUnitsByArea(unit, isResetPos, resetPos, selectObjectCfg, unitIds);
        }

        public static (bool, ListComponent<long>) ChkRecordUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, SelectObjectCfg selectObjectCfg)
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
            return selectHandleRecordManager.ChkRecordUnitsByArea(unit, isResetPos, resetPos, selectObjectCfg);
        }

        public static void GetUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitArea actionCallAutoUnitArea, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            List<Unit> list = UnitHelper.GetUnitListBySelectObjectType(unit, selectObjectCfg.SelectObjectType, false);

            if(list == null)
            {
                return;
            }

            tmp_dic.Clear();
            tmp_dic2.Clear();
            tmp_list1.Clear();

            MultiMapSimple<float, Unit> dic = tmp_dic;
            if (actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate actionCallAutoUnitWhenUmbellate)
            {
                _GetUnitsWhenUmbellate(unit, isResetPos, resetPos, list, dic, selectObjectCfg, actionCallAutoUnitWhenUmbellate, ref actionContext);
            }
            else if (actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle actionCallAutoUnitWhenRectangle)
            {
                _GetUnitsWhenRectangle(unit, isResetPos, resetPos, list, dic, selectObjectCfg, actionCallAutoUnitWhenRectangle, ref actionContext);
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

            (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = ConditionHandleHelper.ChkCondition(unit, selectHandle, selectObjectCfg.SelectPreCondition, ref actionContext);
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

            SelectObjectOrderHandle(unit, isResetPos, resetPos, selectObjectCfg, dic, tmp_list1, selectHandle, ref actionContext);

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
        public static void _GetUnitsWhenUmbellate(Unit unit, bool isResetPos, float3 resetPos, List<Unit> list, MultiMapSimple<float, Unit> dic, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitWhenUmbellate actionCallAutoUnit, ref ActionContext actionContext)
        {
            float radius = actionCallAutoUnit.UmbellateArea.Radius;
            ResetDis(ref radius, ref actionContext);
            float radiusSq = radius * radius;
            float angle = actionCallAutoUnit.UmbellateArea.Angle;
            float angleHalf = angle * 0.5f;

            bool IgnoringHeight = actionCallAutoUnit.UmbellateArea.IgnoringHeight;
            bool KeepHorizontal = actionCallAutoUnit.UmbellateArea.KeepHorizontal;

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                curUnitPos = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
                curUnitForward = unit.Forward;
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
            }

            if (IgnoringHeight || KeepHorizontal)
            {
                curUnitForward.y = 0;
            }
            curUnitForward = math.normalize(curUnitForward);
            float curUnitAttackPoint = ET.Ability.UnitHelper.GetAttackPointHeight(unit);

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
                        (bool bHitMesh, float3 hitPos) = ET.Ability.UnitHelper.ChkHitMesh(unit, curUnitPos, curUnitAttackPoint, targetUnit);
                        if (bHitMesh)
                        {
                            continue;
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
        public static void _GetUnitsWhenRectangle(Unit unit, bool isResetPos, float3 resetPos, List<Unit> list, MultiMapSimple<float, Unit> dic, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitWhenRectangle actionCallAutoUnit, ref ActionContext actionContext)
        {
            float width = actionCallAutoUnit.RectangleArea.Width;
            float length = actionCallAutoUnit.RectangleArea.Length;
            ResetDis(ref length, ref actionContext);

            bool IgnoringHeight = actionCallAutoUnit.RectangleArea.IgnoringHeight;
            bool KeepHorizontal = actionCallAutoUnit.RectangleArea.KeepHorizontal;

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                curUnitPos = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
                curUnitForward = unit.Forward;
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
            }

            if (IgnoringHeight || KeepHorizontal)
            {
                curUnitForward.y = 0;
            }
            curUnitForward = math.normalize(curUnitForward);
            float curUnitAttackPoint = ET.Ability.UnitHelper.GetAttackPointHeight(unit);

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
                        (bool bHitMesh, float3 hitPos) = ET.Ability.UnitHelper.ChkHitMesh(unit, curUnitPos, curUnitAttackPoint, targetUnit);
                        if (bHitMesh)
                        {
                            continue;
                        }

                        dic.Add(dot, targetUnit);
                    }
                }
            }

            return;
        }

        public static void SelectObjectOrderHandle(Unit unit, bool isResetPos, float3 resetPos, SelectObjectCfg selectObjectCfg, MultiMapSimple<float, Unit> dic, List<Unit> listTmp, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            if (selectObjectCfg.SelectNum == -1)
            {
                ListComponent<long> tmpList = ListComponent<long>.Create();
                foreach (long unitId in selectHandle.unitIds)
                {
                    Unit unitOne = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitOne))
                    {
                        tmpList.Add(unitId);
                    }
                }
                selectHandle.unitIds.Dispose();
                selectHandle.unitIds = tmpList;
                return;
            }

            List<SelectObjectOrder> selectOrderList = selectObjectCfg.SelectOrder;
            if (selectOrderList == null)
            {
                ListComponent<long> tmpList = ListComponent<long>.Create();
                foreach (long unitId in selectHandle.unitIds)
                {
                    Unit unitOne = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitOne) && ET.Ability.BuffHelper.ChkCanBeFind(unitOne, unit))
                    {
                        tmpList.Add(unitId);
                        if (tmpList.Count >= selectObjectCfg.SelectNum)
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
                if (UnitHelper.ChkUnitAlive(unitOne) && ET.Ability.BuffHelper.ChkCanBeFind(unitOne, unit))
                {
                    listTmp.Add(unitOne);
                }
            }

            if (listTmp.Count <= selectObjectCfg.SelectNum)
            {
                ListComponent<long> tmpList = ListComponent<long>.Create();
                foreach (long unitId in selectHandle.unitIds)
                {
                    Unit unitOne = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitOne) && ET.Ability.BuffHelper.ChkCanBeFind(unitOne, unit))
                    {
                        tmpList.Add(unitId);
                        if (tmpList.Count >= selectObjectCfg.SelectNum)
                        {
                            break;
                        }
                    }
                }
                selectHandle.unitIds.Dispose();
                selectHandle.unitIds = tmpList;
                return;
            }

            selectHandle.unitIds.Clear();
            for (int i = 0; i < selectOrderList.Count; i++)
            {
                SelectObjectOrder selectObjectOrder = selectOrderList[i];

                bool isDescending = selectObjectOrder.IsDescending;
                dic.Clear();
                if (selectObjectOrder is DisOrder)
                {
                    SelectObjectOrderHandle_DisOrder(unit, isResetPos, resetPos, listTmp, dic);
                }
                else if (selectObjectOrder is AngleOrder)
                {
                    SelectObjectOrderHandle_AngleOrder(unit, isResetPos, resetPos, listTmp, dic);
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
                else if (selectObjectOrder is RandomOrder)
                {
                    SelectObjectOrderHandle_RandomOrder(unit, listTmp, dic);
                }

                int needCount = selectObjectCfg.SelectNum - selectHandle.unitIds.Count;
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
                                if (selectHandle.unitIds.Count >= selectObjectCfg.SelectNum)
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
                            if (selectHandle.unitIds.Count >= selectObjectCfg.SelectNum)
                            {
                                return;
                            }
                            if (selectHandle.unitIds.Count + item.Value.Count > selectObjectCfg.SelectNum)
                            {
                                //这一部分继续应用下个规则进行筛选
                                listTmp.Clear();
                                foreach (var item2 in item.Value)
                                {
                                    Unit unitOne = item2;
                                    listTmp.Add(unitOne);
                                }
                                continue;
                            }
                            else
                            {
                                foreach (var item2 in item.Value)
                                {
                                    Unit unitOne = item2;
                                    selectHandle.unitIds.Add(unitOne.Id);
                                    listTmp.Remove(unitOne);
                                    if (selectHandle.unitIds.Count >= selectObjectCfg.SelectNum)
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
                    foreach (var sortList in dic)
                    {
                        for (int i2 = 0; i2 < sortList.Value.Count; i2++)
                        {
                            Unit unitOne = sortList.Value[i2];
                            selectHandle.unitIds.Add(unitOne.Id);
                        }
                    }
                    return;
                }
            }

        }

        public static void SelectObjectOrderHandle_DisOrder(Unit unit, bool isResetPos, float3 resetPos, List<Unit> list, MultiMapSimple<float, Unit> dic)
        {
            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                curUnitPos = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
                curUnitForward = unit.Forward;
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
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

        public static void SelectObjectOrderHandle_AngleOrder(Unit unit, bool isResetPos, float3 resetPos, List<Unit> list, MultiMapSimple<float, Unit> dic)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                float3 curUnitPos;
                float3 curUnitForward;
                if (isResetPos)
                {
                    curUnitPos = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
                    curUnitForward = unit.Forward;
                }
                else
                {
                    (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
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
                selectHandle.position = unit.Position;
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

        public static bool ChkUnitsInArea(Unit unit, bool isResetPos, float3 resetPos, SelectObjectCfg selectObjectCfg, ActionCallAutoUnitArea actionCallAutoUnitArea, SelectHandle saveSelectHandle, ref ActionContext actionContext)
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


            bool isNeedChkIsFriend = false;
            bool isNeedChkIsPlayer = false;
            bool isFriend = false;
            bool isOnlyPlayer = false;

            if (selectObjectCfg.SelectObjectType == SelectObjectType.FriendPlayers)
            {
                isNeedChkIsFriend = true;
                isNeedChkIsPlayer = true;
                isFriend = true;
                isOnlyPlayer = true;
            }
            else if (selectObjectCfg.SelectObjectType == SelectObjectType.FriendButNotPlayers)
            {
                isNeedChkIsFriend = true;
                isNeedChkIsPlayer = true;
                isFriend = true;
                isOnlyPlayer = false;
            }
            else if (selectObjectCfg.SelectObjectType == SelectObjectType.Friends)
            {
                isNeedChkIsFriend = true;
                isNeedChkIsPlayer = false;
                isFriend = true;
                isOnlyPlayer = false;
            }
            else if (selectObjectCfg.SelectObjectType == SelectObjectType.HostilePlayers)
            {
                isNeedChkIsFriend = true;
                isNeedChkIsPlayer = true;
                isFriend = false;
                isOnlyPlayer = true;
            }
            else if (selectObjectCfg.SelectObjectType == SelectObjectType.HostileButNotPlayers)
            {
                isNeedChkIsFriend = true;
                isNeedChkIsPlayer = true;
                isFriend = false;
                isOnlyPlayer = false;
            }
            else if (selectObjectCfg.SelectObjectType == SelectObjectType.Hostiles)
            {
                isNeedChkIsFriend = true;
                isNeedChkIsPlayer = false;
                isFriend = false;
                isOnlyPlayer = false;
            }
            else if (selectObjectCfg.SelectObjectType == SelectObjectType.AllPlayers)
            {
                isNeedChkIsFriend = false;
                isNeedChkIsPlayer = true;
                isFriend = false;
                isOnlyPlayer = true;
            }
            else if (selectObjectCfg.SelectObjectType == SelectObjectType.AllButNotPlayers)
            {
                isNeedChkIsFriend = false;
                isNeedChkIsPlayer = true;
                isFriend = false;
                isOnlyPlayer = false;
            }

            using ListComponent<Unit> list = ListComponent<Unit>.Create();
            foreach (long unitId in saveSelectHandle.unitIds)
            {
                Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                if (UnitHelper.ChkUnitAlive(unitSelect) == false)
                {
                    return false;
                }

                if (isNeedChkIsFriend)
                {
                    if (isFriend && ET.GamePlayHelper.ChkIsFriend(unitSelect, unit) == false)
                    {
                        return false;
                    }
                    else if (isFriend == false && ET.GamePlayHelper.ChkIsFriend(unitSelect, unit))
                    {
                        return false;
                    }
                }
                if (isNeedChkIsPlayer)
                {
                    if (isOnlyPlayer && UnitHelper.ChkIsPlayer(unitSelect) == false)
                    {
                        return false;
                    }
                    else if (isOnlyPlayer == false && UnitHelper.ChkIsPlayer(unitSelect))
                    {
                        return false;
                    }
                }
                if (ET.Ability.BuffHelper.ChkCanBeFind(unitSelect, unit) == false)
                {
                    return false;
                }
                list.Add(unitSelect);
            }

            if (actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate actionCallAutoUnitWhenUmbellate)
            {
                return ChkUnitsInAreaWhenUmbellate(unit, isResetPos, resetPos, list, actionCallAutoUnitWhenUmbellate, ref actionContext);
            }
            else if (actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle actionCallAutoUnitWhenRectangle)
            {
                return ChkUnitsInAreaWhenRectangle(unit, isResetPos, resetPos, list, actionCallAutoUnitWhenRectangle, ref actionContext);
            }

            return true;
        }

        public static bool ChkUnitsInAreaWhenUmbellate(Unit unit, bool isResetPos, float3 resetPos, List<Unit> list, ActionCallAutoUnitWhenUmbellate actionCallAutoUnit, ref ActionContext actionContext)
        {
            float radius = actionCallAutoUnit.UmbellateArea.Radius;
            ResetDis(ref radius, ref actionContext);

            float radiusSq = radius * radius;
            float angle = actionCallAutoUnit.UmbellateArea.Angle;
            float angleHalf = angle * 0.5f;

            bool IgnoringHeight = actionCallAutoUnit.UmbellateArea.IgnoringHeight;
            bool KeepHorizontal = actionCallAutoUnit.UmbellateArea.KeepHorizontal;

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                curUnitPos = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
                curUnitForward = unit.Forward;
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
            }

            if (IgnoringHeight || KeepHorizontal)
            {
                curUnitForward.y = 0;
            }
            curUnitForward = math.normalize(curUnitForward);
            float curUnitAttackPoint = ET.Ability.UnitHelper.GetAttackPointHeight(unit);

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
                        (bool bHitMesh, float3 hitPos) = ET.Ability.UnitHelper.ChkHitMesh(unit, curUnitPos, curUnitAttackPoint, targetUnit);
                        if (bHitMesh)
                        {
                            return false;
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

        public static bool ChkUnitsInAreaWhenRectangle(Unit unit, bool isResetPos, float3 resetPos, List<Unit> list, ActionCallAutoUnitWhenRectangle actionCallAutoUnit, ref ActionContext actionContext)
        {
            float width = actionCallAutoUnit.RectangleArea.Width;
            float length = actionCallAutoUnit.RectangleArea.Length;
            ResetDis(ref length, ref actionContext);

            bool IgnoringHeight = actionCallAutoUnit.RectangleArea.IgnoringHeight;
            bool KeepHorizontal = actionCallAutoUnit.RectangleArea.KeepHorizontal;

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                curUnitPos = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, null);
                curUnitForward = unit.Forward;
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, null);
            }
            if (IgnoringHeight || KeepHorizontal)
            {
                curUnitForward.y = 0;
            }
            curUnitForward = math.normalize(curUnitForward);

            float curUnitAttackPoint = ET.Ability.UnitHelper.GetAttackPointHeight(unit);
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
                        (bool bHitMesh, float3 hitPos) = ET.Ability.UnitHelper.ChkHitMesh(unit, curUnitPos, curUnitAttackPoint, targetUnit);
                        if (bHitMesh)
                        {
                            return false;
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

        public static void ResetDis(ref float dis, ref ActionContext actionContext)
        {
            if (dis > actionContext.skillDis)
            {
                dis = actionContext.skillDis * 1.2f;
            }
        }
    }
}