using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
	public enum GamePlayTowerDefenseStatus
	{
		ShowStartEffect,
		PutHome,
		PutMonsterPoint,
		RestTime,
		InTheBattle,
		GameSuccess,
		GameFailed,
	}

	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayTowerDefenseComponent : GamePlayModeComponent
	{
		[BsonIgnore]
		public long lastSendTime;
		[BsonIgnore]
		public GamePlayTowerDefenseCfg model
		{
			get
			{
				return GamePlayTowerDefenseCfgCategory.Instance.Get(this.gamePlayModeCfgId);
			}
		}

		public GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus { get; set; }
		public long ownerPlayerId { get; set; }

		[BsonIgnore]
		public long lastMouseDownTime;
		[BsonIgnore]
		public float3 lastMousePosition;
	}
}