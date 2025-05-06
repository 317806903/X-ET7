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

	public enum ARMeshType
	{
		FromRemoteURL_DM,
		FromRemoteURL_AliyunOSS,
		FromClientObj,
	}

	[ComponentOf(typeof(Scene))]
	public class GamePlayComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		// [BsonIgnore]
		// public long Timer;
		public GamePlayMode gamePlayMode { get; set; }
		public long dynamicMapInstanceId;
		public long ownerPlayerId;
		public GamePlayStatus gamePlayStatus;
		public long roomId;
		public bool isAR;
		public float mapScale;
		public RoomTypeInfo roomTypeInfo { get; set; }

		public ARMeshType _ARMeshType;
		public string _ARSceneId;
		public string _ARSceneMeshId;
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
		public string isTestARMeshUrl = @"http://prod-cn-szh-alicloud-arsession-arsession-deepmirror-s3.oss-cn-shenzhen.aliyuncs.com/67c803f4a6ec0901e7b186ba.space_mesh?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=LTAI5tJxp9P7mSgzy4R9o5h5%2F20250305%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250305T075754Z&X-Amz-Expires=86400&X-Amz-SignedHeaders=host&X-Amz-Signature=e6f7ef1c3ab2a501303144e9c17aea2a8c288bde24e82b7656d4135f15e52540";
		[BsonIgnore]
		public bool isTestARObj = false;
		[BsonIgnore]
		public int isTestARObjScale = 35;
		[BsonIgnore]
		public string isTestARObjUrl = @"http://192.168.10.50/CDN/676e1feca6ec0901e7b1801f.obj";

		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayPlayerListToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayStatisticalToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayModeToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayModeToClientListForceSend;

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

		public bool isChkPlayerConnect;
		public bool isFirstSendGamePlayToClient;
		public bool isFirstSendGamePlayModeToClient;
		public bool isFirstSendGamePlayPlayerListToClient;
	}
}