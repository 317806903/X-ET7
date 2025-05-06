using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (CenterPointComponent))]
    public static class CenterPointComponentSystem
    {
        [ObjectSystem]
        public class CenterPointComponentAwakeSystem: AwakeSystem<CenterPointComponent>
        {
            protected override void Awake(CenterPointComponent self)
            {
                self.nearDis = 5f;
                self.Init();
            }
        }

        [ObjectSystem]
        public class CenterPointComponentDestroySystem: DestroySystem<CenterPointComponent>
        {
            protected override void Destroy(CenterPointComponent self)
            {
            }
        }

        public static void Init(this CenterPointComponent self)
        {
            self.centerPoint = ET.RecastHelper.GetCenterPointFromMonsterCallsToHeadQuarter(self.DomainScene());


            // GamePlayHelper.CreateNPC(self.DomainScene(), "Unit_Tower_Flame1", self.centerPoint, new float3(0,0,1));
        }

        public static float3 GetRandomPoint(this CenterPointComponent self)
        {
            Unit observerUnit = UnitHelper.GetOneObserverUnit(self.DomainScene());
            return ET.RecastHelper.GetNearNavmeshPos(observerUnit, self.centerPoint, self.nearDis);
        }

        public static List<float3> GetRandomPointList(this CenterPointComponent self, int num)
        {
            Unit observerUnit = UnitHelper.GetOneObserverUnit(self.DomainScene());
            if (observerUnit == null)
            {
                return null;
            }
            ListComponent<float3> pointList = ListComponent<float3>.Create();
            for (int i = 0; i < num; i++)
            {
                float3 nearPos = ET.RecastHelper.GetNearNavmeshPos(observerUnit, self.centerPoint, self.nearDis);
                pointList.Add(nearPos);
            }
            return pointList;
        }
    }
}