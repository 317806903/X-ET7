using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class PutMonsterCallComponent : Entity, IAwake, IDestroy, ITransferClient
	{
		[BsonIgnore]
		public Dictionary<long, float3> MonsterCallPos;
		
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, long> MonsterCallUnitId;
	}
}