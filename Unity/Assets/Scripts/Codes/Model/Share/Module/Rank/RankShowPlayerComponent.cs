using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	[ChildOf(typeof(RankShowManagerComponent))]
	public class RankShowPlayerComponent : Entity, IAwake, IDestroy, ISerializeToEntity
	{
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<RankType, long> RankList = new();
	}
}