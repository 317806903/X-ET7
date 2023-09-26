using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	public enum GetCoinType
	{
		Normal,
		/// <summary>
		/// 波次结束结算存款利息
		/// </summary>
		InterestOnDeposit,

		/// <summary>
		/// 波次结束奖励金币
		/// </summary>
		WaveRewardGold,
	}

	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayPlayerListComponent : Entity, IAwake, IDestroy, ITransferClient
	{
		/// <summary>
		/// playerId对应的unitId列表
		/// </summary>
		[BsonIgnore]
		public MultiMapSet<long, long> playerId2UnitIds;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, long> playerId2PlayerUnitId;

		/// <summary>
		/// unitId对应playerId
		/// </summary>
		[BsonIgnore]
		public Dictionary<long, long> unitId2PlayerId;

		/// <summary>
		/// playerId是否已经退出战斗
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, bool> playerId2IsQuit;

		public List<long> playerList;

		/// <summary>
		/// playerId对应的出生点
		/// </summary>
		[BsonIgnore]
		public Dictionary<long, float3> playerId2BirthPos;

		/// <summary>
		/// playerId对应的货币列表
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiDictionary<long, string, int> playerId2CoinList;
	}
}