using System.Collections.Generic;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class SelectHandleHelper
    {
        public static SelectHandle CreateSelectHandle(Unit unit, Unit resetPosByUnit, ActionCallParam actionCallParam, ref ActionContext actionContext)
        {
            bool isResetPos = false;
            float3 resetPos = float3.zero;
            if (resetPosByUnit != null)
            {
                isResetPos = true;
                resetPos = resetPosByUnit.Position;
            }
            return CreateSelectHandle(unit, isResetPos, resetPos, actionCallParam, ref actionContext);
        }

        public static SelectHandle CreateSelectHandle(Unit unit, bool isResetPos, float3 resetPos, ActionCallParam actionCallParam, ref ActionContext
         actionContext)
        {
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
            (bRet, saveSelectHandle) = ChkIsUseSaveSelectHandle(unit, isResetPos, resetPos, actionCallParam, ref actionContext);
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
                bool isChgToSelectPos = false;
                selectHandle.selectHandleType = SelectHandleType.SelectUnits;
                selectHandle.unitIds = ListComponent<long>.Create();
                if (actionCallParam is ActionCallAutoUnitArea actionCallAutoUnitArea)
                {
                    GetUnitsByArea(unit, isResetPos, resetPos, actionCallAutoUnitArea, selectHandle, ref actionContext);
                    if (actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate actionCallAutoUnitWhenUmbellate)
                    {
                        isChgToSelectPos = actionCallAutoUnitWhenUmbellate.IsChgToSelectPos;
                    }
                    else if (actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle actionCallAutoUnitWhenRectangle)
                    {
                        isChgToSelectPos = actionCallAutoUnitWhenRectangle.IsChgToSelectPos;
                    }
                }
                else if (actionCallParam is ActionCallAutoUnitOne actionCallAutoUnitOne)
                {
                }

                if (isChgToSelectPos)
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

            if (actionCallParam is ActionCallAuto actionCallAuto)
            {
                if (actionCallAuto.IsSave)
                {
                    UnitHelper.SaveSelectHandle(unit, selectHandle);
                }
            }
            else if (actionCallParam is ActionCallSelect actionCallSelect)
            {
                if (actionCallSelect.IsSave)
                {
                    UnitHelper.SaveSelectHandle(unit, selectHandle);
                }
            }

            return selectHandle;
        }

        public static (bool, SelectHandle) ChkIsUseSaveSelectHandle(Unit unit, bool isResetPos, float3 resetPos, ActionCallParam actionCallParam,
        ref ActionContext actionContext)
        {
            bool chkIsUseSaveSelectHandle = false;
            if (actionCallParam is ActionCallAuto actionCallAuto)
            {
                if (actionCallAuto.IsSave)
                {
                    chkIsUseSaveSelectHandle = true;
                }
            }
            else if (actionCallParam is ActionCallSelect actionCallSelect)
            {
                if (actionCallSelect.IsSave)
                {
                    chkIsUseSaveSelectHandle = true;
                }
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
                    bool bRet = ChkUnitsInArea(unit, isResetPos, resetPos, actionCallAutoUnitArea, saveSelectHandle, ref actionContext);
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

        public static SelectHandle CreateUnitSelectHandle(Unit unit, Unit targetUnit, ActionCallParam actionCallParam)
        {
            SelectHandle selectHandle = SelectHandle.Create();
            selectHandle.selectHandleType = SelectHandleType.SelectUnits;
            selectHandle.unitIds = ListComponent<long>.Create();
            selectHandle.unitIds.Add(targetUnit.Id);
            selectHandle.position = targetUnit.Position;
            selectHandle.direction = targetUnit.Forward;

            bool isChgToSelectPos = false;
            if (actionCallParam is ActionCallCasterUnit actionCallCasterUnit)
            {
                isChgToSelectPos = actionCallCasterUnit.IsChgToSelectPos;
            }
            else if (actionCallParam is ActionCallCasterPlayerUnit actionCallCasterPlayerUnit)
            {
                isChgToSelectPos = actionCallCasterPlayerUnit.IsChgToSelectPos;
            }
            else if (actionCallParam is ActionCallOnAttackUnit actionCallOnAttackUnit)
            {
                isChgToSelectPos = actionCallOnAttackUnit.IsChgToSelectPos;
            }
            else if (actionCallParam is ActionCallBeHurtUnit actionCallBeHurtUnit)
            {
                isChgToSelectPos = actionCallBeHurtUnit.IsChgToSelectPos;
            }

            if (isChgToSelectPos)
            {
                DealWhenIsChgToSelectPos(unit, ref selectHandle);
            }

            if (actionCallParam is ActionCallAuto actionCallAuto)
            {
                if (actionCallAuto.IsSave)
                {
                    UnitHelper.SaveSelectHandle(unit, selectHandle);
                }
            }
            else if (actionCallParam is ActionCallSelect actionCallSelect)
            {
                if (actionCallSelect.IsSave)
                {
                    UnitHelper.SaveSelectHandle(unit, selectHandle);
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

        private static MultiMap<float, Unit> tmp_dic = new();
        public static void GetUnitsByArea(Unit unit, bool isResetPos, float3 resetPos, ActionCallAutoUnitArea actionCallAutoUnitArea, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            bool isFriend = actionCallAutoUnitArea.IsFriend;
            bool isOnlyPlayer = actionCallAutoUnitArea.IsOnlyPlayer;

            ListComponent<Unit> list;
            if (isFriend)
            {
                list = UnitHelper.GetFriends(unit, isOnlyPlayer);
            }
            else
            {
                list = UnitHelper.GetHostileForces(unit, isOnlyPlayer);
            }

            int selectNum = actionCallAutoUnitArea.SelectNum;

            tmp_dic.Clear();
            MultiMap<float, Unit> dic = tmp_dic;
            if (actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate actionCallAutoUnitWhenUmbellate)
            {
                GetUnitsWhenUmbellate(unit, isResetPos, resetPos, list, dic, actionCallAutoUnitWhenUmbellate, ref actionContext);
            }
            else if (actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle actionCallAutoUnitWhenRectangle)
            {
                GetUnitsWhenRectangle(unit, isResetPos, resetPos, list, dic, actionCallAutoUnitWhenRectangle, ref actionContext);
            }

            int index = 0;
            foreach (var sortList in dic)
            {
                for (int i = 0; i < sortList.Value.Count; i++)
                {
                    if (selectNum == -1)
                    {
                        Unit unitOne = sortList.Value[i];
                        if (UnitHelper.ChkUnitAlive(unitOne))
                        {
                            selectHandle.unitIds.Add(unitOne.Id);
                            index++;
                        }
                    }
                    else if (index <= selectNum - 1)
                    {
                        Unit unitOne = sortList.Value[i];
                        if (UnitHelper.ChkUnitAlive(unitOne) && ET.Ability.BuffHelper.ChkCanBeFind(unitOne, unit))
                        {
                            selectHandle.unitIds.Add(unitOne.Id);
                            index++;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
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
        public static void GetUnitsWhenUmbellate(Unit unit, bool isResetPos, float3 resetPos, List<Unit> list, MultiMap<float, Unit> dic, ActionCallAutoUnitWhenUmbellate actionCallAutoUnit, ref ActionContext actionContext)
        {
            float radius = actionCallAutoUnit.UmbellateArea.Radius;
            ResetDis(ref radius, ref actionContext);
            float radiusSq = radius * radius;
            float angle = actionCallAutoUnit.UmbellateArea.Angle;
            float angleHalf = angle * 0.5f;
            bool IsAngleFirst = actionCallAutoUnit.IsAngleFirst;

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                curUnitPos = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, actionCallAutoUnit.OffSetInfo);
                curUnitForward = unit.Forward;
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, actionCallAutoUnit.OffSetInfo);
            }

            curUnitForward = math.normalize(curUnitForward);
            float curUnitAttackPoint = ET.Ability.UnitHelper.GetAttackPointHeight(unit);

            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                float disSq = math.distancesq(targetUnit.Position, curUnitPos);
                if (disSq <= radiusSq)
                {
                    float3 dir = math.normalize(targetUnit.Position - curUnitPos);
                    float angleTmp = math.degrees(math.acos(math.clamp(math.dot(curUnitForward, dir), -1, 1)));
                    if (angleTmp < angleHalf)
                    {
                        (bool bHitMesh, float3 hitPos) = ET.Ability.UnitHelper.ChkHitMesh(unit, curUnitPos, curUnitAttackPoint, targetUnit);
                        if (bHitMesh)
                        {
                            continue;
                        }

                        if (IsAngleFirst)
                        {
                            dic.Add(angleTmp, targetUnit);
                        }
                        else
                        {
                            dic.Add(disSq, targetUnit);
                        }
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
        public static void GetUnitsWhenRectangle(Unit unit, bool isResetPos, float3 resetPos, List<Unit> list, MultiMap<float, Unit> dic, ActionCallAutoUnitWhenRectangle actionCallAutoUnit, ref ActionContext actionContext)
        {
            float width = actionCallAutoUnit.RectangleArea.Width;
            float length = actionCallAutoUnit.RectangleArea.Length;
            ResetDis(ref length, ref actionContext);

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                curUnitPos = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, actionCallAutoUnit.OffSetInfo);
                curUnitForward = unit.Forward;
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, actionCallAutoUnit.OffSetInfo);
            }
            curUnitForward = math.normalize(curUnitForward);
            float curUnitAttackPoint = ET.Ability.UnitHelper.GetAttackPointHeight(unit);

            float disSq = length * length + (width * 0.5f) * (width * 0.5f);
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                float3 dir = math.normalize(targetUnit.Position - curUnitPos);
                float dot = math.dot(curUnitForward, dir);
                if (dot >= 0)
                {
                    if (math.distancesq(targetUnit.Position, curUnitPos) > disSq)
                    {
                        continue;
                    }
                    float acos = math.acos(math.clamp(dot, -1, 1));
                    float dis = math.distance(targetUnit.Position, curUnitPos);
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

        public static List<Unit> GetSelectUnitList(Unit unit, SelectHandle selectHandle, ActionContext actionContext, bool isContainDeathShow = false)
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

        public static bool ChkUnitsInArea(Unit unit, bool isResetPos, float3 resetPos, ActionCallAutoUnitArea actionCallAutoUnitArea, SelectHandle
        saveSelectHandle, ref ActionContext actionContext)
        {
            bool isFriend = actionCallAutoUnitArea.IsFriend;
            bool isOnlyPlayer = actionCallAutoUnitArea.IsOnlyPlayer;

            int selectNum = actionCallAutoUnitArea.SelectNum;
            if (selectNum != 1)
            {
                return false;
            }

            ListComponent<Unit> list = ListComponent<Unit>.Create();
            foreach (long unitId in saveSelectHandle.unitIds)
            {
                Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
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
            bool IsAngleFirst = actionCallAutoUnit.IsAngleFirst;

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                curUnitPos = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, actionCallAutoUnit.OffSetInfo);
                curUnitForward = unit.Forward;
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, actionCallAutoUnit.OffSetInfo);
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
                float disSq = math.distancesq(targetUnit.Position, curUnitPos);
                if (disSq <= radiusSq)
                {
                    float3 dir = math.normalize(targetUnit.Position - curUnitPos);
                    float angleTmp = math.degrees(math.acos(math.clamp(math.dot(curUnitForward, dir), -1, 1)));
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

            float3 curUnitPos;
            float3 curUnitForward;
            if (isResetPos)
            {
                curUnitPos = ET.Ability.UnitHelper.GetNewNodePosition(unit, resetPos, actionCallAutoUnit.OffSetInfo);
                curUnitForward = unit.Forward;
            }
            else
            {
                (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, actionCallAutoUnit.OffSetInfo);
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
                float dot = math.dot(curUnitForward, dir);
                if (dot >= 0)
                {
                    if (math.distancesq(targetUnit.Position, curUnitPos) > disSq)
                    {
                        return false;
                    }

                    float acos = math.acos(math.clamp(dot, -1, 1));
                    float dis = math.distance(targetUnit.Position, curUnitPos);
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