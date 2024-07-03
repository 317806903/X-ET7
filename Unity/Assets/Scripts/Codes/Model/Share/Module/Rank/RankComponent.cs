using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ChildOf(typeof(RankManagerComponent))]
    public class RankComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        /// <summary>
        /// 上榜人数
        /// </summary>
        public ulong topRankPlayerCount;
        /// <summary>
        /// 上榜的具体playerId
        /// </summary>
        public HashSet<long> topRankPlayerList;

        /// <summary>
        /// 数据链
        /// </summary>
        [BsonIgnore]
        public SkipList SkipList;
        /// <summary>
        /// playerId 对应 分数
        /// </summary>
        [BsonIgnore]
        public Dictionary<long, long> playerId2Score;

        /// <summary>
        /// 有成绩的共有多少人
        /// </summary>
        public ulong rankTotalNum;

        public List<RankItemComponent> recordWhenFinished;
    }
}