using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	public enum GamePlayTowerDefenseStatus
	{
		GameBegin,
		WaitMeshFinished,
		LoadMeshErr,
		PutHome,
		PutMonsterPoint,
		ShowStartEffect,
		RestTime,
		InTheBattle,
		InTheBattleEnd,
		Recover,
		Recovering,
		WaitRescan,
		GameEnd,
	}

	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayTowerDefenseComponent : GamePlayModeComponentBase
	{
		[BsonIgnore]
		public bool isInitClient;
		[BsonIgnore]
		public long lastSendTime;
		[BsonIgnore]
		public long lastChkUnitExistTime;
		[BsonIgnore]
		public long lastSendTimeTowerDefense;
		[BsonIgnore]
		public bool isNeedReNoticeTowerDefense;
		[BsonIgnore]
		public GamePlayTowerDefenseStatus previousGamePlayTowerDefenseStatus;
		[BsonIgnore]
		public GamePlayTowerDefenseCfg model
		{
			get
			{
				return GamePlayTowerDefenseCfgCategory.Instance.Get(this.gamePlayModeCfgId);
			}
		}

		public GamePlayModeBase gamePlayModeBase { get; set; }
		public GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus { get; set; }
		public long ownerPlayerId { get; set; }
	}
}