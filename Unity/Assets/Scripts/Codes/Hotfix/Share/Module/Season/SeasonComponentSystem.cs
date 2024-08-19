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

        public static int GetSeasonCfgId(this SeasonComponent self)
        {
            return self.seasonCfgId;
        }

        public static long GetClearTime(this SeasonComponent self)
        {
            long quickReGetTime = 5000;
            if (self.GetComponent<SeasonComponentStatusInSeason>() != null)
            {
                long disEndTime = ET.TimeHelper.ChgToMillisecondTimeStamp(self.endTime) - TimeHelper.ServerNow();
                disEndTime -= 60000;
                if (disEndTime < 0)
                {
                    return quickReGetTime;
                }
                return disEndTime;
            }
            else if (self.GetComponent<SeasonComponentStatusSettlement>() != null)
            {
                return quickReGetTime;
            }

            return 30000;
        }
    }
}