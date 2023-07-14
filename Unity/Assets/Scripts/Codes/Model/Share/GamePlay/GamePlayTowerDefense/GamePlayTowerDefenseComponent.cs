using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
	public enum GamePlayTowerDefenseStatus
	{
		ScanMap,
		DownloadMap,
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
		public GamePlayTowerDefenseCfg model
		{
			get
			{
				return GamePlayTowerDefenseCfgCategory.Instance.Get(this.gamePlayModeCfgId);
			}
		}
		
		public GamePlayTowerDefenseStatus GamePlayTowerDefenseStatus { get; set; }
		public long ownerPlayerId { get; set; }
	}
}