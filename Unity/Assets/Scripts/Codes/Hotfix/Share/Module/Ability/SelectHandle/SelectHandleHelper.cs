using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class SelectHandleHelper
    {
        public static (SelectHandle, Unit) DealSelectHandler(Unit triggerUnit, SelectObjectConfig selectObjectConfig, Unit onAttackUnit, Unit beHurtUnit, ref ActionContext actionContext)
        {
            if (onAttackUnit != null)
            {
                actionContext.attackerUnitId = onAttackUnit.Id;
            }

            Unit resetPosByUnit = null;
            SelectHandle selectHandle;
            if (selectObjectConfig.ActionCallParam is ActionCallSelectLast)
            {
                selectHandle = UnitHelper.GetSaveSelectHandle(triggerUnit);
            }
            else if (selectObjectConfig.ActionCallParam is ActionCallAutoUnit actionCallAutoUnit)
            {
                selectHandle = SelectHandleHelper.CreateSelectHandle(triggerUnit, beHurtUnit, selectObjectConfig, ref actionContext);
            }
            else if (selectObjectConfig.ActionCallParam is ActionCallAutoSelf actionCallAutoSelf)
            {
                selectHandle = SelectHandleHelper.CreateSelectHandle(triggerUnit, null, selectObjectConfig, ref actionContext);
            }
            else if (selectObjectConfig.ActionCallParam is ActionCallOnAoeChgUnit actionCallOnAoeChgUnit)
            {
                selectHandle = SelectHandleHelper.CreateUnitNoneSelectHandle();
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
                        bool isFriend = false;
                        bool isOnlyPlayer = false;

                        if (selectObjectConfig.SelectObjectType == 11)
                        {
                            isFriend = true;
                            isOnlyPlayer = true;
                        }
                        else if (selectObjectConfig.SelectObjectType == 1)
                        {
                            isFriend = true;
                            isOnlyPlayer = false;
                        }
                        else if (selectObjectConfig.SelectObjectType == 21)
                        {
                            isFriend = false;
                            isOnlyPlayer = true;
                        }
                        else if (selectObjectConfig.SelectObjectType == 2)
                        {
                            isFriend = false;
                            isOnlyPlayer = false;
                        }
                        foreach (long unitId in aoeObj.chgUnitList)
                        {
                            Unit unitSelect = UnitHelper.GetUnit(triggerUnit.DomainScene(), unitId);
                            if (UnitHelper.ChkUnitAlive(unitSelect, true) == false)
                            {
                                continue;
                            }
                            if (unitSelect == null)
                            {
                                continue;
                            }
                            if (isFriend && ET.GamePlayHelper.ChkIsFriend(unitSelect, triggerUnit) == false)
                            {
                                continue;
                            }
                            if (isOnlyPlayer && UnitHelper.ChkIsPlayer(unitSelect) == false)
                            {
                                continue;
                            }
                            selectHandle.unitIds.Add(unitId);
                        }
                    }
                }
            }
            else if (selectObjectConfig.ActionCallParam is ActionCallOnAoeInUnit actionCallOnAoeInUnit)
            {
                selectHandle = SelectHandleHelper.CreateUnitNoneSelectHandle();
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
                        bool isFriend = false;
                        bool isOnlyPlayer = false;

                        if (selectObjectConfig.SelectObjectType == 11)
                        {
                            isFriend = true;
                            isOnlyPlayer = true;
                        }
                        else if (selectObjectConfig.SelectObjectType == 1)
                        {
                            isFriend = true;
                            isOnlyPlayer = false;
                        }
                        else if (selectObjectConfig.SelectObjectType == 21)
                        {
                            isFriend = false;
                            isOnlyPlayer = true;
                        }
                        else if (selectObjectConfig.SelectObjectType == 2)
                        {
                            isFriend = false;
                            isOnlyPlayer = false;
                        }
                        foreach (long unitId in aoeObj.unitIds)
                        {
                            Unit unitSelect = UnitHelper.GetUnit(triggerUnit.DomainScene(), unitId);
                            if (UnitHelper.ChkUnitAlive(unitSelect, true) == false)
                            {
                                continue;
                            }
                            if (unitSelect == null)
                            {
                                continue;
                            }
                            if (isFriend && ET.GamePlayHelper.ChkIsFriend(unitSelect, triggerUnit) == false)
                            {
                                continue;
                            }
                            if (isOnlyPlayer && UnitHelper.ChkIsPlayer(unitSelect) == false)
                            {
                                continue;
                            }
                            selectHandle.unitIds.Add(unitId);
                        }
                    }
                }
            }
            else
            {
                Unit targetUnit;
                if (selectObjectConfig.ActionCallParam is ActionCallCasterUnit actionCallCasterUnit)
                {
                    targetUnit = UnitHelper.GetCasterUnit(triggerUnit);
                }
                else if (selectObjectConfig.ActionCallParam is ActionCallCasterPlayerUnit actionCallCasterPlayerUnit)
                {
                    targetUnit = UnitHelper.GetCasterActorUnit(triggerUnit);
                }
                else if (selectObjectConfig.ActionCallParam is ActionCallOnAttackUnit actionCallOnAttackUnit)
                {
                    targetUnit = onAttackUnit;
                }
                else if (selectObjectConfig.ActionCallParam is ActionCallBeHurtUnit actionCallBeHurtUnit)
                {
                    targetUnit = beHurtUnit;
                }
                else
                {
                    targetUnit = triggerUnit;
                }

                resetPosByUnit = targetUnit;
                selectHandle = SelectHandleHelper.CreateUnitSelectHandle(triggerUnit, targetUnit, selectObjectConfig);
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

        public static SelectHandle CreateSelectHandle(Unit unit, Unit resetPosByUnit, SelectObjectConfig selectObjectConfig, ref ActionContext actionContext)
        {
            bool isResetPos = false;
            float3 resetPos = float3.zero;
            if (resetPosByUnit != null)
            {
                isResetPos = true;
                resetPos = resetPosByUnit.Position;
            }
            return CreateSelectHandle(unit, isResetPos, resetPos, selectObjectConfig, ref actionContext);
        }

        public static SelectHandle CreateSelectHandle(Unit unit, bool isResetPos, float3 resetPos, SelectObjectConfig selectObjectConfig, ref ActionContext
         actionContext)
        {
            if (unit == null)
            {
                return null;
            }
            ActionCallParam actionCallParam = selectObjectConfig.ActionCallParam;
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
            (bRet, saveSelectHandle) = _ChkIsUseSaveSelectHandle(unit, isResetPos, resetPos, selectObjectConfig, ref actionContext);
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
                    (bool bRecord, ListComponent<long> recordUnitIds) = ChkRecordUnitsByArea(unit, isResetPos, resetPos, selectObjectConfig);
                    if (bRecord)
                    {
                        selectHandle.unitIds.Clear();
                        selectHandle.unitIds.AddRange(recordUnitIds);
                    }
                    else
                    {
                        GetUnitsByArea(unit, isResetPos, resetPos, selectObjectConfig, actionCallAutoUnitArea, selectHandle, ref actionContext);

                        DoRecordUnitsByArea(unit, isResetPos, resetPos, selectObjectConfig, selectHandle.unitIds);
                    }

                }
                else if (actionCallParam is ActionCallAutoUnitOne actionCallAutoUnitOne)
                {
                }

                if (selectObjectConfig.IsChgToSelectPos)
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

            if (selectObjectConfig.IsSaveTarget)
            {
                UnitHelper.SaveSelectHandle(unit, selectHandle, false);
            }
            else if (selectObjectConfig.IsSaveTargetOnce)
            {
                UnitHelper.SaveSelectHandle(unit, selectHandle, true);
            }

            return selectHandle;
        }

        public static (bool, SelectHandle) _ChkIsUseSaveSelectHandle(Unit unit, bool isResetPos, float3 resetPos, SelectObjectConfig selectObjectConfig,
        ref ActionContext actionContext)
        {
            ActionCallParam actionCallParam = selectObjectConfig.ActionCallParam;
            bool chkIsUseSaveSelectHandle = false;
            if (selectObjectConfig.IsSaveTarget || selectObjectConfig.IsSaveTargetOnce)
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
                    bool bRet = ChkUnitsInArea(unit, isResetPos, resetPos, selectObjectConfig, actionCallAutoUnitArea, saveSelectHandle, ref actionContext);
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
                Unit selectUnit = UnitHelper.GetUnit(unit.DomainScene(), saveSelectHandle.unitIds[0]);
                if (UnitHelper.ChkUnitAlive(selectUnit) == false)
                {
                    return (false, null);
                }

                bool isBeFind = ET.Ability.BuffHelper.ChkCanBeFind(selectUnit, unit);
                if (isBeFind == false)
                {
                    return (false, null);
                }

                saveSelectHandle.position = selectUnit.Position;
                saveSelectHandle.direction = saveSelectHandle.position - unit.Position;
            }
            else
            {
                saveSelectHandle.position = unit.Position;
                saveSelectHandle.direction = unit.Forward;
            }
            return (true, saveSelectHandle);
        }

        public static SelectHandle CreateUnitSelectHandle(Unit unit, Unit targetUnit, SelectObjectConfig selectObjectConfig)
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

            if (selectObjectConfig.IsChgToSelectPos)
            {
                DealWhenIsChgToSelectPos(unit, ref selectHandle);
            }

            if (selectObjectConfig.IsSaveTarget)
            {
                UnitHelper.SaveSelectHandle(unit, selectHandle, false);
            }
            if (selectObjectConfig.IsSaveTargetOnce)
            {
                UnitHelper.SaveSelectHandle(unit, selectHandle, true);
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

        private static MultiMapSimple<float, Unit> tmp_dic = new();
        private static MultiMapSimple<float, Unit> tmp_dic2 = new();
        private static List<Unit> tmp_list1 = new();

        public static void DoRecordUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, SelectObjectConfig selectObjectConfig, ListComponent<long> unitIds)
        {
            SelectHandleRecordManager selectHandleRecordManager = unit.DomainScene().GetComponent<SelectHandleRecordManager>();
            selectHandleRecordManager.DoRecordUnitsByArea(unit, isResetPos, resetPos, selectObjectConfig, unitIds);
        }

        public static (bool, ListComponent<long>) ChkRecordUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, SelectObjectConfig selectObjectConfig)
        {
            SelectHandleRecordManager selectHandleRecordManager = unit.DomainScene().GetComponent<SelectHandleRecordManager>();
            return selectHandleRecordManager.ChkRecordUnitsByArea(unit, isResetPos, resetPos, selectObjectConfig);
        }

        public static void GetUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, SelectObjectConfig selectObjectConfig, ActionCallAutoUnitArea actionCallAutoUnitArea, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            List<Unit> list;
            if (selectObjectConfig.SelectObjectType == 11)
            {
                list = UnitHelper.GetFriends(unit, true);
            }
            else if (selectObjectConfig.SelectObjectType == 1)
            {
                list = UnitHelper.GetFriends(unit, false);
            }
            else if (selectObjectConfig.SelectObjectType == 21)
            {
                list = UnitHelper.GetHostileForces(unit, true, false);
            }
            else if (selectObjectConfig.SelectObjectType == 2)
            {
                list = UnitHelper.GetHostileForces(unit, false, false);
            }
            else
            {
                list = UnitHelper.GetFriends(unit, false);
                List<Unit> listHostileForces = UnitHelper.GetHostileForces(unit, false, false);
                list.AddRange(listHostileForces);
            }

            tmp_dic.Clear();
            tmp_dic2.Clear();
            tmp_list1.Clear();

            MultiMapSimple<float, Unit> dic = tmp_dic;
            if (actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate actionCallAutoUnitWhenUmbellate)
            {
                _GetUnitsWhenUmbellate(unit, isResetPos, resetPos, list, dic, selectObjectConfig, actionCallAutoUnitWhenUmbellate, ref actionContext);
            }
            else if (actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle actionCallAutoUnitWhenRectangle)
            {
                _GetUnitsWhenRectangle(unit, isResetPos, resetPos, list, dic, selectObjectConfig, actionCallAutoUnitWhenRectangle, ref actionContext);
            }

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

            (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = ConditionHandleHelper.ChkCondition(unit, selectHandle, selectObjectConfig.SelectPreCondition, ref actionContext);
            if (bRet1 == false)
            {
                selectHandle.unitIds.Clear();
                return;
            }
            if (isChgSelect1)
            {
                selectHandle = newSelectHandle1;
            }

            SelectObjectOrderHandle(unit, isResetPos, resetPos, selectObjectConfig, dic, tmp_list1, selectHandle, ref actionContext);

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
        public static void _GetUnitsWhenUmbellate(Unit unit, bool isResetPos, float3 resetPos, List<Unit> list, MultiMapSimple<float, Unit> dic, SelectObjectConfig selectObjectConfig, ActionCallAutoUnitWhenUmbellate actionCallAutoUnit, ref ActionContext actionContext)
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
        public static void _GetUnitsWhenRectangle(Unit unit, bool isResetPos, float3 resetPos, List<Unit> list, MultiMapSimple<float, Unit> dic, SelectObjectConfig selectObjectConfig, ActionCallAutoUnitWhenRectangle actionCallAutoUnit, ref ActionContext actionContext)
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

        public static void SelectObjectOrderHandle(Unit unit, bool isResetPos, float3 resetPos, SelectObjectConfig selectObjectConfig, MultiMapSimple<float, Unit> dic, List<Unit> listTmp, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            if (selectObjectConfig.SelectNum == -1)
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

            List<SelectObjectOrder> selectOrderList = selectObjectConfig.SelectOrder;
            if (selectOrderList == null)
            {
                ListComponent<long> tmpList = ListComponent<long>.Create();
                foreach (long unitId in selectHandle.unitIds)
                {
                    Unit unitOne = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitOne) && ET.Ability.BuffHelper.ChkCanBeFind(unitOne, unit))
                    {
                        tmpList.Add(unitId);
                        if (tmpList.Count > selectObjectConfig.SelectNum)
                        {
                            selectHandle.unitIds.Dispose();
                            selectHandle.unitIds = tmpList;
                            return;
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

            if (listTmp.Count <= selectObjectConfig.SelectNum)
            {
                ListComponent<long> tmpList = ListComponent<long>.Create();
                foreach (long unitId in selectHandle.unitIds)
                {
                    Unit unitOne = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitOne) && ET.Ability.BuffHelper.ChkCanBeFind(unitOne, unit))
                    {
                        tmpList.Add(unitId);
                        if (tmpList.Count > selectObjectConfig.SelectNum)
                        {
                            selectHandle.unitIds.Dispose();
                            selectHandle.unitIds = tmpList;
                            return;
                        }
                    }
                }
                return;
            }

            selectHandle.unitIds.Clear();
            foreach (SelectObjectOrder selectObjectOrder in selectOrderList)
            {
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
                    bool needExist = false;
                    if (isDescending)
                    {
                        needExist = true;
                    }
                    SelectObjectOrderHandle_BuffExistOrder(needExist, buffExistOrder.BuffDealSelectCondition, listTmp, dic);
                }
                else if (selectObjectOrder is DisHomeOrder)
                {
                    SelectObjectOrderHandle_DisHomeOrder(unit, listTmp, dic);
                }
                else if (selectObjectOrder is RandomOrder)
                {
                    SelectObjectOrderHandle_RandomOrder(unit, listTmp, dic);
                }

                int needCount = selectObjectConfig.SelectNum - selectHandle.unitIds.Count;
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
                    foreach (var item in orderedEnumerable)
                    {
                        foreach (var item2 in item.Value)
                        {
                            selectHandle.unitIds.Add(item2.Id);
                            if (selectHandle.unitIds.Count >= selectObjectConfig.SelectNum)
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    //数量不足够，全部选择，并重新选择
                    foreach (var sortList in dic)
                    {
                        for (int i = 0; i < sortList.Value.Count; i++)
                        {
                            Unit unitOne = sortList.Value[i];
                            selectHandle.unitIds.Add(unitOne.Id);
                            listTmp.Remove(unitOne);
                        }
                    }
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

        public static void SelectObjectOrderHandle_BuffExistOrder(bool needExist, BuffDealSelectCondition buffDealSelectCondition, List<Unit> list, MultiMapSimple<float, Unit> dic)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];

                bool bExist = BuffHelper.ChkBuffByCondition(targetUnit, buffDealSelectCondition);

                if (needExist && bExist)
                {
                    dic.Add(0, targetUnit);
                }
                else if(needExist == false && bExist == false)
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

        public static bool ChkUnitsInArea(Unit unit, bool isResetPos, float3 resetPos, SelectObjectConfig selectObjectConfig, ActionCallAutoUnitArea actionCallAutoUnitArea, SelectHandle
        saveSelectHandle, ref ActionContext actionContext)
        {
            int selectNum = selectObjectConfig.SelectNum;
            if (selectNum != 1)
            {
                return false;
            }


            bool isFriend = false;
            bool isOnlyPlayer = false;

            if (selectObjectConfig.SelectObjectType == 11)
            {
                isFriend = true;
                isOnlyPlayer = true;
            }
            else if (selectObjectConfig.SelectObjectType == 1)
            {
                isFriend = true;
                isOnlyPlayer = false;
            }
            else if (selectObjectConfig.SelectObjectType == 21)
            {
                isFriend = false;
                isOnlyPlayer = true;
            }
            else if (selectObjectConfig.SelectObjectType == 2)
            {
                isFriend = false;
                isOnlyPlayer = false;
            }
            using ListComponent<Unit> list = ListComponent<Unit>.Create();
            foreach (long unitId in saveSelectHandle.unitIds)
            {
                Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                if (UnitHelper.ChkUnitAlive(unitSelect, true) == false)
                {
                    return false;
                }
                if (unitSelect == null)
                {
                    return false;
                }
                if (isFriend && ET.GamePlayHelper.ChkIsFriend(unitSelect, unit) == false)
                {
                    return false;
                }
                if (isOnlyPlayer && UnitHelper.ChkIsPlayer(unitSelect) == false)
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