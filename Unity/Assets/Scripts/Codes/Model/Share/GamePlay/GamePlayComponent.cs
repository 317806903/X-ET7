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
	public class GamePlayComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		// [BsonIgnore]
		// public long Timer;
		public string gamePlayBattleLevelCfgId { get; set; }
		public GamePlayMode gamePlayMode { get; set; }
		public long dynamicMapInstanceId;
		public long roomId;
		public long ownerPlayerId;
		public GamePlayStatus gamePlayStatus;
		public bool isAR;
		[BsonIgnore]
		public string _ARMeshDownLoadUrl;

		[BsonIgnore]
		public long gamePlayWaitDestroyTime;
		[BsonIgnore]
		public Dictionary<long, long> playerWaitQuitTime;

		[BsonIgnore]
		public bool isTestARMesh = false;
		[BsonIgnore]
		public string isTestARMeshUrl = @"http://prod-cn-bj-alicloud-arsession-arsession-deepmirror-s3.oss-cn-beijing.aliyuncs.com/6501fe335252b55795bc8981.space_mesh?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=LTAI5tPk1NHZtLxk3N1nm8nT%2F20230913%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20230913T182419Z&X-Amz-Expires=172800&X-Amz-SignedHeaders=host&X-Amz-Signature=d5e5dbde71f0a58fff07bda9383cf281b74fd8d3ca30a3ce186b6d9f72c6c8ad";

		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayPlayerListToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayModeToClientList;
	}
}