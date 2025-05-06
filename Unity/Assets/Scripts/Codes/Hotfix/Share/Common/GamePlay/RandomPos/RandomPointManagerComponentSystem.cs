using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (RandomPointManagerComponent))]
    public static class RandomPointManagerComponentSystem
    {
        [ObjectSystem]
        public class RandomPointManagerComponentAwakeSystem: AwakeSystem<RandomPointManagerComponent>
        {
            protected override void Awake(RandomPointManagerComponent self)
            {
            }
        }

        [ObjectSystem]
        public class RandomPointManagerComponentDestroySystem: DestroySystem<RandomPointManagerComponent>
        {
            protected override void Destroy(RandomPointManagerComponent self)
            {
            }
        }

        public static float3 GetRandomPoint(this RandomPointManagerComponent self, CallActorPositionType callActorPositionType)
        {
            switch (callActorPositionType)
            {
                case CallActorPositionType.ByParent:
                    return float3.zero;
                case CallActorPositionType.MapRandom:
                    MapRandomPointComponent mapRandomPointComponent = self.GetComponent<MapRandomPointComponent>();
                    return mapRandomPointComponent.GetRandomPoint();
                case CallActorPositionType.PathLineRandom:
                    PathLineRandomPointComponent pathLineRandomPointComponent = self.GetComponent<PathLineRandomPointComponent>();
                    if (pathLineRandomPointComponent == null)
                    {
                        return self.GetRandomPoint(CallActorPositionType.MapRandom);
                    }
                    return pathLineRandomPointComponent.GetRandomPoint();
                case CallActorPositionType.Center:
                    CenterPointComponent centerPointComponent = self.GetComponent<CenterPointComponent>();
                    if (centerPointComponent == null)
                    {
                        return self.GetRandomPoint(CallActorPositionType.MapRandom);
                    }
                    return centerPointComponent.GetRandomPoint();
            }
            return float3.zero;
        }

        public static List<float3> GetRandomPointList(this RandomPointManagerComponent self, CallActorPositionType callActorPositionType, int num)
        {
            switch (callActorPositionType)
            {
                case CallActorPositionType.ByParent:
                    return null;
                case CallActorPositionType.MapRandom:
                    MapRandomPointComponent mapRandomPointComponent = self.GetComponent<MapRandomPointComponent>();
                    return mapRandomPointComponent.GetRandomPointList(num);
                case CallActorPositionType.PathLineRandom:
                    PathLineRandomPointComponent pathLineRandomPointComponent = self.GetComponent<PathLineRandomPointComponent>();
                    if (pathLineRandomPointComponent == null)
                    {
                        return self.GetRandomPointList(CallActorPositionType.MapRandom, num);
                    }
                    return pathLineRandomPointComponent.GetRandomPointList(num);
                case CallActorPositionType.Center:
                    CenterPointComponent centerPointComponent = self.GetComponent<CenterPointComponent>();
                    if (centerPointComponent == null)
                    {
                        return self.GetRandomPointList(CallActorPositionType.MapRandom, num);
                    }
                    return centerPointComponent.GetRandomPointList(num);
            }
            return null;
        }

    }
}