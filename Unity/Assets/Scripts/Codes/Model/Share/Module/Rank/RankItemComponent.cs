using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ChildOf(typeof(RankComponent))]
    public class RankItemComponent : Entity, IAwake, IDestroy
    {
        public long score;
        public long playerId;
        public long recordTime;
    }
}