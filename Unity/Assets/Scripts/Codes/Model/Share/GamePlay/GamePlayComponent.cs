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
		public string isTestARMeshUrl = @"https://prod-us-nva-aws-arsession-arsession-deepmirror-s3.s3.us-east-1.amazonaws.com/65448b48a15af04c30d83e78.space_mesh?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVTJNBT3MTK7JNKFA%2F20231103%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20231103T055605Z&X-Amz-Expires=172800&X-Amz-SignedHeaders=host&X-Amz-Signature=f1d3606f9a64e775e6baaad1bd865e2d9353e3faf600a4f6dd70743899f8f079";

		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayPlayerListToClientList;
		[BsonIgnore]
		public HashSet<long> waitNoticeGamePlayModeToClientList;
	}
}