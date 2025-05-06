using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof(RankPVEComponent))]
    public static class RankPVEComponentSystem
    {
        [ObjectSystem]
        public class RankPVEComponentAwakeSystem : AwakeSystem<RankPVEComponent>
        {
            protected override void Awake(RankPVEComponent self)
            {
            }
        }
    }
}