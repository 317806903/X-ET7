using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerBaseInfoComponent : Entity, IAwake, IDestroy
    {
        public int seasonIndex;
        public int seasonCfgId;

        public string PlayerName;
        public int IconIndex;
        public string AvatarFrameItemCfgId;
        public int EndlessChallengeScore;
        public int EndlessChallengeKillNum;

        public int ARPVEBattleCount;
        public int ARPVPBattleCount;
        public int AREndlessChallengeBattleCount;

        public bool isFinishTutorialFirst;

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, PVELevelDifficulty> pveLevelInfo;

        public int physicalStrength;
        public long nextRecoverPhysicalTime;

        public LoginType BindLoginType;
        public string BindEmail;
    }
}