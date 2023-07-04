using System.Collections.Generic;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class SelectHandleHelper
    {
        public static SelectHandle CreateSelectHandle(Unit unit, ActionCallParam actionCallParam)
        {
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
                    GetUnitsByArea(unit, actionCallAutoUnitArea, selectHandle);
                }
                else if (actionCallParam is ActionCallAutoUnitOne actionCallAutoUnitOne)
                {
                }
                if (selectHandle.unitIds.Count > 0)
                {
                    Unit selectUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[0]);
                    selectHandle.position = selectUnit.Position;
                    selectHandle.direction = selectUnit.Position - unit.Position;
                }
                else
                {
                    selectHandle.position = unit.Position;
                    selectHandle.direction = unit.Forward;
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
        
        public static SelectHandle CreateUnitSelectHandle(Unit unit, Unit targetUnit, ActionCallParam actionCallParam)
        {
            SelectHandle selectHandle = SelectHandle.Create();
            selectHandle.selectHandleType = SelectHandleType.SelectUnits;
            selectHandle.unitIds = ListComponent<long>.Create();
            selectHandle.unitIds.Add(targetUnit.Id);
            selectHandle.position = targetUnit.Position;
            selectHandle.direction = targetUnit.Forward;

            
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
        
        public static void GetUnitsByArea(Unit unit, ActionCallAutoUnitArea actionCallAutoUnitArea, SelectHandle selectHandle)
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

            MultiMap<float, Unit> dic = new();
            if (actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate actionCallAutoUnitWhenUmbellate)
            {
                GetUnitsWhenUmbellate(unit, list, dic, actionCallAutoUnitWhenUmbellate);
            }
            else if (actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle actionCallAutoUnitWhenRectangle)
            {
                GetUnitsWhenRectangle(unit, list, dic, actionCallAutoUnitWhenRectangle);
            }

            int index = 0;
            foreach (var sortList in dic)
            {
                for (int i = 0; i < sortList.Value.Count; i++)
                {
                    if (index <= selectNum - 1 || selectNum == -1)
                    {
                        Unit unitOne = sortList.Value[i];
                        if (UnitHelper.ChkUnitAlive(unitOne))
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
        
        public static void GetUnitsWhenUmbellate(Unit unit, ListComponent<Unit> list, MultiMap<float, Unit> dic, ActionCallAutoUnitWhenUmbellate actionCallAutoUnit)
        {
            float radius = actionCallAutoUnit.UmbellateArea.Radius;
            float radiusSq = radius * radius;
            float angle = actionCallAutoUnit.UmbellateArea.Angle;
            float angleHalf = angle * 0.5f;
            bool IsAngleFirst = actionCallAutoUnit.IsAngleFirst;

            float3 curUnitPos;
            float3 curUnitForward;
            (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, actionCallAutoUnit.OffSetInfo);
            curUnitForward = math.normalize(curUnitForward);

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
        
        public static void GetUnitsWhenRectangle(Unit unit, ListComponent<Unit> list, MultiMap<float, Unit> dic, ActionCallAutoUnitWhenRectangle actionCallAutoUnit)
        {
            float width = actionCallAutoUnit.RectangleArea.Width;
            float length = actionCallAutoUnit.RectangleArea.Length;

            float3 curUnitPos;
            float3 curUnitForward;
            (curUnitPos, curUnitForward) = ET.Ability.UnitHelper.GetNewNodePosition(unit, actionCallAutoUnit.OffSetInfo);
            curUnitForward = math.normalize(curUnitForward);
            
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                float3 dir = math.normalize(targetUnit.Position - curUnitPos);
                float dot = math.dot(curUnitForward, dir);
                if (dot >= 0)
                {
                    float acos = math.acos(math.clamp(dot, -1, 1));
                    float dis = math.distance(targetUnit.Position, curUnitPos);
                    float curHalfWidth = math.sin(acos) * dis;
                    float curLength = math.cos(acos) * dis;
                    //float angleTmp = math.degrees(acos);
                    Log.Debug($" GetUnitsWhenRectangle dis={dis} curHalfWidth={curHalfWidth} curLength={curLength} angle={math.degrees(acos)}");
                    if (curHalfWidth <= width * 0.5f && curLength <= length)
                    {
                        dic.Add(dot, targetUnit);
                    }
                }
            }

            return;
        }
    }
}