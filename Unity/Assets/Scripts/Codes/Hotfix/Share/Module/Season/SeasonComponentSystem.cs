using System.Collections.Generic;
using System.Linq;

namespace ET
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

        public static int GetSeasonId(this SeasonComponent self)
        {
            return self.seasonId;
        }

        public static long GetClearTime(this SeasonComponent self)
        {
            long quickReGetTime = 2000;
            if (self.seasonStatus == SeasonStatus.InSeason)
            {
                long disEndTime = ET.TimeHelper.ChgToMillisecondTimeStamp(self.endTime) - TimeHelper.ServerNow();
                disEndTime -= 60000;
                if (disEndTime < 0)
                {
                    return quickReGetTime;
                }
                return disEndTime;
            }
            else if (self.seasonStatus == SeasonStatus.SettlementSeason)
            {
                return quickReGetTime;
            }
            else if (self.seasonStatus == SeasonStatus.WaitingNewSeason)
            {
                long disEndTime = self.startTime - TimeHelper.ServerNow();
                disEndTime -= 60000;
                if (disEndTime < 0)
                {
                    return quickReGetTime;
                }
                return disEndTime;
            }

            return 30000;
        }
    }
}