using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (PathLineRandomPointComponent))]
    public static class PathLineRandomPointComponentSystem
    {
        [ObjectSystem]
        public class PathLineRandomPointComponentAwakeSystem: AwakeSystem<PathLineRandomPointComponent>
        {
            protected override void Awake(PathLineRandomPointComponent self)
            {
                self.pointDis = 1f;
                self.nearDis = 5f;
                self.Init();
            }
        }

        [ObjectSystem]
        public class PathLineRandomPointComponentDestroySystem: DestroySystem<PathLineRandomPointComponent>
        {
            protected override void Destroy(PathLineRandomPointComponent self)
            {
                if (self.randomPointList != null)
                {
                    self.randomPointList.Clear();
                    self.randomPointList = null;
                }
            }
        }

        public static void Init(this PathLineRandomPointComponent self)
        {
            // List<float3> list = self.GetRandomPointList(10);
            // for (int i = 0; i < list.Count; i++)
            // {
            //     GamePlayHelper.CreateNPC(self.DomainScene(), "Unit_Tower_Flame1", list[i], new float3(0,0,1));
            // }
        }

        public static void Reset(this PathLineRandomPointComponent self)
        {
            self.isInit = false;
        }

        public static List<float3> GetRandomPointList(this PathLineRandomPointComponent self)
        {
            if (self.isInit)
            {
                return self.randomPointList;
            }

            self.isInit = true;
            self.randomPointList = ET.RecastHelper.GetRandomPointFromMonsterCallsToHeadQuarter(self.DomainScene(), self.pointDis);
            return self.randomPointList;
        }

        public static float3 GetRandomPoint(this PathLineRandomPointComponent self)
        {
            List<float3> randomPointList = self.GetRandomPointList();
            int randomNumber = RandomGenerator.RandomNumber(0, randomPointList.Count);
            float3 randomPoint = self.randomPointList[randomNumber];

            Unit observerUnit = UnitHelper.GetOneObserverUnit(self.DomainScene());
            return ET.RecastHelper.GetNearNavmeshPos(observerUnit, randomPoint, self.nearDis);
        }

        public static List<float3> GetRandomPointList(this PathLineRandomPointComponent self, int num)
        {
            Unit observerUnit = UnitHelper.GetOneObserverUnit(self.DomainScene());
            if (observerUnit == null)
            {
                return null;
            }
            List<float3> randomPointList = self.GetRandomPointList();
            HashSet<int> randomNumberHashSet = RandomGenerator.RandomNumber(0, randomPointList.Count, num);
            ListComponent<float3> pointList = ListComponent<float3>.Create();
            foreach (int index in randomNumberHashSet)
            {
                float3 nearPos = ET.RecastHelper.GetNearNavmeshPos(observerUnit, self.randomPointList[index], self.nearDis);
                pointList.Add(nearPos);
            }
            return pointList;
        }

    }
}