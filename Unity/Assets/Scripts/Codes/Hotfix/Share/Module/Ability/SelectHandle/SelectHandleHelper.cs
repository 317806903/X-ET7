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
            }
            else if (actionCallParam is ActionCallAutoUnit)
            {
                selectHandle.selectHandleType = SelectHandleType.SelectUnits;
                selectHandle.unitIds = ListComponent<long>.Create();
                GetSelectUnits(unit, actionCallParam as ActionCallAutoUnit, selectHandle);
            }

            return selectHandle;
        }
        
        public static void GetSelectUnits(Unit unit, ActionCallAutoUnit actionCallAutoUnit, SelectHandle selectHandle)
        {
            bool isFriend = actionCallAutoUnit.IsFriend;
            bool isOnlyPlayer = actionCallAutoUnit.IsOnlyPlayer;

            ListComponent<Unit> list;
            if (isFriend)
            {
                list = UnitHelper.GetHostileForces(unit, isOnlyPlayer);
            }
            else
            {
                list = UnitHelper.GetFriends(unit, isOnlyPlayer);
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
                Unit curUnit = list[i];
                float disSq = math.distancesq(curUnit.Position, unit.Position);
                if (disSq <= radiusSq)
                {
                    float3 dir = curUnit.Position - unit.Position;
                    float angleTmp = math.degrees(math.acos(math.dot(unit.Forward, dir)));
                    if (angleTmp < angleHalf)
                    {
                        if (IsAngleFirst)
                        {
                            dic.Add(angleTmp, curUnit);
                        }
                        else
                        {
                            dic.Add(disSq, curUnit);
                        }
                    }
                }
            }

            int index = 0;
            foreach (var sortList in dic)
            {
                for (int i = 0; i < sortList.Value.Count; i++)
                {
                    if (index <= selectNum)
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