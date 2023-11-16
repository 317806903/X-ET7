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

        public bool isFinishTutorialFirst;

    }
}