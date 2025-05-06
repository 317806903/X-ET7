using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof(RankEndlessChallengeComponent))]
    public static class RankEndlessChallengeComponentSystem
    {
        [ObjectSystem]
        public class RankEndlessChallengeComponentAwakeSystem : AwakeSystem<RankEndlessChallengeComponent>
        {
            protected override void Awake(RankEndlessChallengeComponent self)
            {

            }
        }
    }
}