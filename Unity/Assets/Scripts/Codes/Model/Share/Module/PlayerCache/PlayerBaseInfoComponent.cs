using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerBaseInfoComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        public string PlayerName;
        public int IconIndex;
        public int EndlessChallengeScore;
        public int EndlessChallengeKillNum;

        public bool isFinishTutorialFirst;

        public int ChallengeClearLevel;
        public int physicalStrength;
        public long nextRecoverTime;

        public LoginType BindLoginType;
        public string BindEmail;
    }
}