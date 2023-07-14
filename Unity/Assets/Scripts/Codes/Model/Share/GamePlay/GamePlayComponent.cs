using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
	public enum GamePlayMode
	{
		TowerDefense,
		PK,
	}
	
	public enum GamePlayStatus
	{
		WaitForStart,
		Gaming,
		GameEnd,
	}
	
	[ComponentOf(typeof(Scene))]
	public class GamePlayComponent : Entity, IAwake, IDestroy
	{
		[BsonIgnore]
		public long Timer;
		public string gamePlayBattleLevelCfgId { get; set; }
		public GamePlayMode gamePlayMode { get; set; }
		public long dynamicMapInstanceId;
		public long roomId;
		public long ownerPlayerId;
		public GamePlayStatus gamePlayStatus;

		[BsonIgnore]
		public long gamePlayWaitDestroyTime;
		[BsonIgnore]
		public Dictionary<long, long> playerWaitQuitTime;
	}
}