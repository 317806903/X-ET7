using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class PlayerOwnerTowersComponent : Entity, IAwake, IDestroy, ITransferClient
	{
		public bool initPlayerOwnerTowerCard = false;
		/// <summary>
		/// 玩家拥有的卡池(卡片)(playerId, towerCfgId, towerLevel)
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiDictionary<long, string, int> playerOwnerTowerCardIds;

		/// <summary>
		/// 玩家拥有的卡池(当场中购买过的)(playerId, towerCfgId, towerNum)
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiDictionary<long, string, int> playerOwnerTowerId;

		/// <summary>
		/// 玩家可购买的卡池
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiMapSimple<long, string> playerTowerBuyPools;

		/// <summary>
		/// 玩家可购买的卡池是否已购买
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiMapSimple<long, bool> playerTowerBuyPoolBoughts;

		/// <summary>
		/// 玩家刷新卡池消耗金币
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, int> playerRefreshTowerCost;

		/// <summary>
		/// 玩家对应塔列表
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiMapSimple<long, long> playerId2unitTowerId;

		/// <summary>
		/// 玩家对应塔列表(仅AttackTower)
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiMapSimple<long, long> playerId2unitAttackTowerId;

		[BsonIgnore]
		public MultiMap<string, long> existTowerDic;
	}
}