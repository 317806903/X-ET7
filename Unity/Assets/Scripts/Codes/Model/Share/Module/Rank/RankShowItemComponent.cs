using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ChildOf(typeof(RankShowComponent))]
    public class RankShowItemComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        public long score;
        public long playerId;
        public long recordTime;
    }
}