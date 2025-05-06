using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerSeasonInfoComponent : Entity, IAwake, IDestroy
    {
        public int seasonIndex;
        public int seasonCfgId;

        public Dictionary<string, int> seasonBringUpDic;

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, PVELevelDifficulty> pveLevelInfo;

        public int EndlessChallengeScore;
        public int EndlessChallengeKillNum;
        public int rankWhenSeasonFinished;
    }
}