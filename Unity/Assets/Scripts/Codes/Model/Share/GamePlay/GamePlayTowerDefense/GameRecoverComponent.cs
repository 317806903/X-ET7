using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	public enum GameRecoverType
	{
		ByWatchAd,
		Free,
		CostArcadeCoin,
	}

	public enum PlayerRecoverStatus
	{
		Default,
		Confirm,
		Cancel,
	}

	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class GameRecoverComponent : Entity, IAwake, IDestroy, IFixedUpdate, ITransferClient
	{
		public float recoverTimeoutTime;
		public int recoverFreeTimes;
		public int recoverByWatchAdTimes;
		public int recoverCostArcadeCoinTimes;
		public int recoverCostArcadeCoinNumOrg;
		public int recoverAddHp;
		public int recoverAddGold;
	}
}