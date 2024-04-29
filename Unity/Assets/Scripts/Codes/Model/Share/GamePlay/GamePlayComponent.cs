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

	public enum GamePlaySubMode
	{
		None,
		SingleMap,
		TowerDefense_Normal,
		TowerDefense_PVE,
		TowerDefense_PVP,
		TowerDefense_EndlessChallenge,
		TowerDefense_TutorialFirst,
		TowerDefense_ArcadeScanMesh,
	}

	public enum GamePlayStatus
	{
		WaitForStart,
		Gaming,
		GameEnd,
	}

	public enum ARMeshType
	{
		FromRemoteURL,
		FromClientObj,
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
		public float arMapScale;

		public ARMeshType _ARMeshType;
		[BsonIgnore]
		public string _ARMeshDownLoadUrl;
		[BsonIgnore]
		public byte[] _ARMeshBytes;

		[BsonIgnore]
		public long gamePlayWaitDestroyTime;
		[BsonIgnore]
		public Dictionary<long, long> playerWaitQuitTime;

		[BsonIgnore]
		public bool isTestARMesh = false;
		[BsonIgnore]
		public string isTestARMeshUrl = @"http://prod-cn-bj-alicloud-arsession-arsession-deepmirror-s3.oss-cn-beijing.aliyuncs.com/657167fb5252b55795bc943c.space_mesh?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=LTAI5tPk1NHZtLxk3N1nm8nT%2F20231207%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20231207T063711Z&X-Amz-Expires=172800&X-Amz-SignedHeaders=host&X-Amz-Signature=d7c5dcf14c2c67131ccf4eb6d6b5771cbdca1d8aa1ea14c54c300ae6ed0e4b28";
		[BsonIgnore]
		public bool isTestARObj = false;
		[BsonIgnore]
		public string isTestARObjUrl = @"http://192.168.10.58/CDN/home-2.obj";

		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayPlayerListToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayStatisticalToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayModeToClientList;

		[BsonIgnore]
		public int waitFrameSyncPos = 10;
		[BsonIgnore]
		public int curFrameSyncPos = 0;
		[BsonIgnore]
		public HashSet<long> RecordSendGetNumericUnit;

		[BsonIgnore]
		public int waitFrameChk = 60;
		[BsonIgnore]
		public int curFrameChk = 0;
		[BsonIgnore]
		public List<long> playerListTmp;

		public bool isFirstSendGamePlayToClient;
		public bool isFirstSendGamePlayModeToClient;
		public bool isFirstSendGamePlayPlayerListToClient;
	}
}