using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof(SeasonHistoryComponent))]
    public static class SeasonHistoryComponentSystem
    {
        [ObjectSystem]
        public class SeasonHistoryComponentAwakeSystem : AwakeSystem<SeasonHistoryComponent>
        {
            protected override void Awake(SeasonHistoryComponent self)
            {
            }
        }

        public static void Init(this SeasonHistoryComponent self, int seasonIndex, int seasonCfgId, long startTime, long endTime, long initTime, long recordTime)
        {
            self.seasonIndex = seasonIndex;
            self.seasonCfgId = seasonCfgId;
            self.startTime = TimeHelper.ToDateTime(startTime).ToString();
            self.endTime = TimeHelper.ToDateTime(endTime).ToString();
            self.initTime = TimeHelper.ToDateTime(initTime).ToString();
            self.recordTime = TimeHelper.ToDateTime(recordTime).ToString();
        }
    }
}