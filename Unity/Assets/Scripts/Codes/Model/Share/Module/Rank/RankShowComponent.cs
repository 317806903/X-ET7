using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ChildOf(typeof(RankShowPlayerComponent))]
    public class RankShowComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        public RankType rankType;
        public List<long> rankList = new();
        [BsonIgnore]
        public List<EntityRef<RankShowItemComponent>> rankListTmp = new();

        public long myRankShowItemComponentId;
    }
}