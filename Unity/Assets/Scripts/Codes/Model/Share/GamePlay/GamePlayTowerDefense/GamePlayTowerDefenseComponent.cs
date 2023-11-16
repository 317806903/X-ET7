using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
	public enum GamePlayTowerDefenseMode
	{
		TowerDefense_Normal,
		TowerDefense_PVE,
		TowerDefense_PVP,
		TowerDefense_EndlessChallenge,
		TowerDefense_TutorialFirst,
	}

	public enum GamePlayTowerDefenseStatus
	{
		ShowStartEffect,
		PutHome,
		PutMonsterPoint,
		RestTime,
		InTheBattle,
		GameEnd,
	}

	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayTowerDefenseComponent : GamePlayModeComponent
	{
		[BsonIgnore]
		public bool isInitClient;
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

		public GamePlayTowerDefenseMode gamePlayTowerDefenseMode { get; set; }
		public GamePlayModeBase gamePlayModeBase { get; set; }
		public GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus { get; set; }
		public long ownerPlayerId { get; set; }

	}
}