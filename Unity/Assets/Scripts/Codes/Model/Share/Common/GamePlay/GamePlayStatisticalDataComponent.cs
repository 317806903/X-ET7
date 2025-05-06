using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayStatisticalDataManagerComponent))]
	public class GamePlayStatisticalDataComponent : Entity, IAwake, IDestroy
	{
		public long playerId;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiDictionary<long, string, int> towerUnitId2MonsterType2Num;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiDictionary<string, string, int> towerCfgId2MonsterType2Num;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiDictionary<TowerType, string, int> towerType2MonsterType2Num;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<string, int> playerAllMonsterType2Num;

		public int playerMonsterEscapeAttackValue;
	}
}