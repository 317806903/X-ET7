﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

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

        public int ChallengeClearLevel;

        public int physicalStrength;
        public long nextRecoverPhysicalTime;

        public LoginType BindLoginType;
        public string BindEmail;
    }
}