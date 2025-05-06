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
		/// <summary>
		/// 普通获得
		/// </summary>
		Normal,

		/// <summary>
		/// 波次结束平分阵营金币池
		/// </summary>
		DivideEquallyTeamCoin,

		/// <summary>
		/// 波次结束结算存款利息
		/// </summary>
		InterestOnDeposit,

		/// <summary>
		/// 波次结束奖励金币
		/// </summary>
		WaveRewardGold,

        /// <summary>
		/// 每n秒获得金币
		/// </summary>
		IntervalRewardGold,
    }

    [ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayPlayerListComponent : Entity, IAwake, IDestroy, ITransferClient, IFixedUpdate
	{

		[BsonIgnore]
		public int waitFrameChk = 60;
		[BsonIgnore]
		public int curFrameChk = 0;

		/// <summary>
		/// playerId对应的unitId列表
		/// </summary>
		[BsonIgnore]
		public MultiMapSetSimple<long, long> playerId2UnitIds;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiMapSetSimple<long, long> playerId2PlayerUnitIdList;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, long> playerId2CameraPlayerUnitId;

		/// <summary>
		/// playerId是否已经退出战斗
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, bool> playerId2IsQuit;

		/// <summary>
		/// playerId对应的出生点
		/// </summary>
		[BsonIgnore]
		public Dictionary<long, float3> playerId2BirthPos;

		/// <summary>
		/// playerId对应的货币列表
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiDictionary<long, string, float> playerId2CoinList;

		/// <summary>
		/// team对应的货币列表
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiDictionary<TeamFlagType, string, float> team2CoinList;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, float> lastPlayerGold = new ();
	}
}