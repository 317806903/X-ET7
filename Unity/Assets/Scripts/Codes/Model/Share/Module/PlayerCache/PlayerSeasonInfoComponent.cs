using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerSeasonInfoComponent : Entity, IAwake, IDestroy
    {
        public int seasonIndex;
        public int seasonCfgId;

        public Dictionary<string, int> seasonBringUpDic;

        public int pveIndex;
        public int EndlessChallengeScore;
        public int EndlessChallengeKillNum;
        public int rankWhenSeasonFinished;
    }
}