using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (MapRandomPointComponent))]
    public static class MapRandomPointComponentSystem
    {
        [ObjectSystem]
        public class MapRandomPointComponentAwakeSystem: AwakeSystem<MapRandomPointComponent>
        {
            protected override void Awake(MapRandomPointComponent self)
            {
                self.pointDis = 1f;
                self.Init();
            }
        }

        [ObjectSystem]
        public class MapRandomPointComponentDestroySystem: DestroySystem<MapRandomPointComponent>
        {
            protected override void Destroy(MapRandomPointComponent self)
            {
                if (self.randomPointList != null)
                {
                    self.randomPointList.Clear();
                    self.randomPointList = null;
                }
            }
        }

        public static void Init(this MapRandomPointComponent self)
        {
            self.randomPointList = ET.RecastHelper.GetRandomPointFromMesh(self.DomainScene(), self.pointDis);

            // List<float3> list = self.GetRandomPointList(100);
            // for (int i = 0; i < list.Count; i++)
            // {
            //     GamePlayHelper.CreateNPC(self.DomainScene(), "Unit_Tower_Flame1", list[i], new float3(0,0,1));
            // }
        }

        public static float3 GetRandomPoint(this MapRandomPointComponent self)
        {
            int randomNumber = RandomGenerator.RandomNumber(0, self.randomPointList.Count);
            return self.randomPointList[randomNumber];
        }

        public static List<float3> GetRandomPointList(this MapRandomPointComponent self, int num)
        {
            HashSet<int> randomNumberHashSet = RandomGenerator.RandomNumber(0, self.randomPointList.Count, num);
            ListComponent<float3> pointList = ListComponent<float3>.Create();
            foreach (int index in randomNumberHashSet)
            {
                pointList.Add(self.randomPointList[index]);
            }
            return pointList;
        }

    }
}