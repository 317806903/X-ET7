using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ChildOf(typeof(RankShowPlayerComponent))]
    public class RankShowComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public SortedDictionary<int, long> rankIndex2PlayerId = new();
    }
}