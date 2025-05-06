using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof(SeasonComponent))]
    public static class SeasonComponentSystem
    {
        [ObjectSystem]
        public class SeasonComponentAwakeSystem : AwakeSystem<SeasonComponent>
        {
            protected override void Awake(SeasonComponent self)
            {
            }
        }

        public static async ETTask GoToNextSeason(this SeasonComponent self)
        {
            ET.Server.SeasonHelper.SetCurSeasonByTime(self);
            self.AddComponent<SeasonComponentStatusInSeason>();
            self.initTime = TimeHelper.ServerNow();
            self.SetDataCacheAutoWrite(true);
            await self.ChkDataCacheAutoWriteFinished();
        }

    }
}