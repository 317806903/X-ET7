using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class PutHomeComponent : Entity, IAwake, IDestroy, ITransferClient, IFixedUpdate
	{
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<TeamFlagType, long> HomeUnitIdList;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<TeamFlagType, long> TeamFlagType2PlayerIdCanPutHome;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<TeamFlagType, bool> TeamFlagType2Result;

		[BsonIgnore]
		public Dictionary<long, (string homeCfgId, float3 homePos, float3 homeForward, int maxHp, int hp)> RecordHomeInfo = new ();
	}
}