using System.Collections.Generic;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class SelectHandleHelper
    {
        public static SelectHandle GetSelectHandle(Unit unit, ActionCallParam actionCallParam)
        {
            SelectHandle selectHandle = new();
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
                GetSelectUnits(unit, actionCallParam as ActionCallAutoUnit, selectHandle);
                if (selectHandle.unitIds.Count > 0)
                {
                    Unit selectUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[0]);
                    selectHandle.position = selectUnit.Position;
                    selectHandle.direction = selectUnit.Forward;
                }
            }

            return selectHandle;
        }
        
        public static SelectHandle GetSelectHandle(Unit unit, Unit targetUnit)
        {
            SelectHandle selectHandle = new();
            selectHandle.selectHandleType = SelectHandleType.SelectUnits;
            selectHandle.unitIds = ListComponent<long>.Create();
            selectHandle.unitIds.Add(targetUnit.Id);
            selectHandle.position = targetUnit.Position;
            selectHandle.direction = targetUnit.Forward;
            return selectHandle;
        }
        
        public static void GetSelectUnits(Unit unit, ActionCallAutoUnit actionCallAutoUnit, SelectHandle selectHandle)
        {
            bool isFriend = actionCallAutoUnit.IsFriend;
            bool isOnlyPlayer = actionCallAutoUnit.IsOnlyPlayer;

            ListComponent<Unit> list;
            if (isFriend)
            {
                list = UnitHelper.GetFriends(unit, isOnlyPlayer);
            }
            else
            {
                list = UnitHelper.GetHostileForces(unit, isOnlyPlayer);
            }
            
            int selectNum = actionCallAutoUnit.SelectNum;
            float radius = actionCallAutoUnit.Radius;
            float radiusSq = radius * radius;
            float angle = actionCallAutoUnit.Angle;
            float angleHalf = angle * 0.5f;
            bool IsAngleFirst = actionCallAutoUnit.IsAngleFirst;

            MultiMap<float, Unit> dic = new();
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                float disSq = math.distancesq(targetUnit.Position, unit.Position);
                if (disSq <= radiusSq)
                {
                    float3 dir = math.normalize(targetUnit.Position - unit.Position);
                    float angleTmp = math.degrees(math.acos(math.clamp(math.dot(unit.Forward, dir), -1, 1)));
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

            int index = 0;
            foreach (var sortList in dic)
            {
                for (int i = 0; i < sortList.Value.Count; i++)
                {
                    if (index <= selectNum || selectNum == -1)
                    {
                        selectHandle.unitIds.Add(sortList.Value[i].Id);
                        index++;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}