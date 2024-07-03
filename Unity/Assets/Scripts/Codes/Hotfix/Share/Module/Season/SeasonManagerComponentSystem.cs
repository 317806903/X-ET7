using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(SeasonManagerComponent))]
    public static class SeasonManagerComponentSystem
    {
        [ObjectSystem]
        public class SeasonManagerComponentAwakeSystem : AwakeSystem<SeasonManagerComponent>
        {
            protected override void Awake(SeasonManagerComponent self)
            {
            }
        }

        public static int GetSeasonId(this SeasonManagerComponent self)
        {
            if (self.SeasonComponent != null)
            {
                return self.SeasonComponent.seasonId;
            }
            return -1;
        }

        public static long GetClearTime(this SeasonManagerComponent self)
        {
            if (self.SeasonComponent != null)
            {
                return self.SeasonComponent.GetClearTime();
            }
            return 0;
        }
    }
}