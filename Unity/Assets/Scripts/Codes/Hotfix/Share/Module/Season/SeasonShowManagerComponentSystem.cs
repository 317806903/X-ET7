using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(SeasonShowManagerComponent))]
    public static class SeasonShowManagerComponentSystem
    {
        [ObjectSystem]
        public class SeasonShowManagerComponentAwakeSystem : AwakeSystem<SeasonShowManagerComponent>
        {
            protected override void Awake(SeasonShowManagerComponent self)
            {
            }
        }

        public static int GetSeasonId(this SeasonShowManagerComponent self)
        {
            if (self.SeasonComponent != null)
            {
                return self.SeasonComponent.seasonId;
            }
            return -1;
        }

        public static long GetClearTime(this SeasonShowManagerComponent self)
        {
            if (self.SeasonComponent != null)
            {
                return self.SeasonComponent.GetClearTime();
            }
            return 0;
        }
    }
}