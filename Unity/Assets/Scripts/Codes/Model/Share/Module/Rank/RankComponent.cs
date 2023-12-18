using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ChildOf(typeof(RankManagerComponent))]
    public class RankComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        [BsonIgnore]
        public ulong topRankPlayerCount;
        public HashSet<long> topRankPlayerList;
        [BsonIgnore]
        public SkipList SkipList;
        [BsonIgnore]
        public Dictionary<long, long> playerId2Score;

        public ulong rankTotalNum;
    }
}