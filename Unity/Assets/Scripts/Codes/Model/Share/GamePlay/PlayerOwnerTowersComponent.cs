using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class PlayerOwnerTowersComponent : Entity, IAwake, IDestroy, ITransferClient
	{
		/// <summary>
		/// 玩家拥有的卡池
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiDictionary<long, string, int> playerOwnerTowerId;

		/// <summary>
		/// 玩家可购买的卡池
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiMap<long, string> playerTowerBuyPools;

		/// <summary>
		/// 玩家可购买的卡池是否已购买
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiMap<long, bool> playerTowerBuyPoolBoughts;

		/// <summary>
		/// 玩家刷新卡池消耗金币
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, int> playerRefreshTowerCost;
		
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, string> unitId2TowerCfgId;
	}
}