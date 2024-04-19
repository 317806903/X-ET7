using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
	[Message(OuterMessage.HttpGetRouterResponse)]
	[ProtoContract]
	public partial class HttpGetRouterResponse: ProtoObject
	{
		[ProtoMember(1)]
		public List<string> Realms { get; set; }

		[ProtoMember(2)]
		public List<string> Routers { get; set; }

	}

	[Message(OuterMessage.RouterSync)]
	[ProtoContract]
	public partial class RouterSync: ProtoObject
	{
		[ProtoMember(1)]
		public uint ConnectId { get; set; }

		[ProtoMember(2)]
		public string Address { get; set; }

	}

	[ResponseType(nameof(M2C_TestResponse))]
	[Message(OuterMessage.C2M_TestRequest)]
	[ProtoContract]
	public partial class C2M_TestRequest: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string request { get; set; }

	}

	[Message(OuterMessage.M2C_TestResponse)]
	[ProtoContract]
	public partial class M2C_TestResponse: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public string response { get; set; }

	}

	[ResponseType(nameof(Actor_TransferResponse))]
	[Message(OuterMessage.Actor_TransferRequest)]
	[ProtoContract]
	public partial class Actor_TransferRequest: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int MapIndex { get; set; }

	}

	[Message(OuterMessage.Actor_TransferResponse)]
	[ProtoContract]
	public partial class Actor_TransferResponse: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_EnterMap))]
	[Message(OuterMessage.C2G_EnterMap)]
	[ProtoContract]
	public partial class C2G_EnterMap: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string GamePlayBattleLevelCfgId { get; set; }

	}

	[Message(OuterMessage.G2C_EnterMap)]
	[ProtoContract]
	public partial class G2C_EnterMap: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.G2C_EnterBattleNotice)]
	[ProtoContract]
	public partial class G2C_EnterBattleNotice: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[ResponseType(nameof(G2C_GetPlayerStatus))]
	[Message(OuterMessage.C2G_GetPlayerStatus)]
	[ProtoContract]
	public partial class C2G_GetPlayerStatus: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_GetPlayerStatus)]
	[ProtoContract]
	public partial class G2C_GetPlayerStatus: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public byte[] PlayerStatusComponentBytes { get; set; }

	}

	[Message(OuterMessage.G2C_PlayerStatusChgNotice)]
	[ProtoContract]
	public partial class G2C_PlayerStatusChgNotice: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public byte[] PlayerStatusComponentBytes { get; set; }

	}

	[Message(OuterMessage.G2C_LoginInAtOtherWhere)]
	[ProtoContract]
	public partial class G2C_LoginInAtOtherWhere: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.MoveInfo)]
	[ProtoContract]
	public partial class MoveInfo: ProtoObject
	{
		[ProtoMember(1)]
		public List<Unity.Mathematics.float3> Points { get; set; }

		[ProtoMember(2)]
		public Unity.Mathematics.quaternion Rotation { get; set; }

		[ProtoMember(3)]
		public int TurnSpeed { get; set; }

	}

	[Message(OuterMessage.UnitInfo)]
	[ProtoContract]
	public partial class UnitInfo: ProtoObject
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

		[ProtoMember(2)]
		public string ConfigId { get; set; }

		[ProtoMember(3)]
		public int Level { get; set; }

		[ProtoMember(4)]
		public int Type { get; set; }

		[ProtoMember(5)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(6)]
		public Unity.Mathematics.float3 Forward { get; set; }

		[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
		[ProtoMember(7)]
		public Dictionary<int, long> KV { get; set; }
		[ProtoMember(8)]
		public MoveInfo MoveInfo { get; set; }

		[ProtoMember(9)]
		public List<byte[]> Components { get; set; }

		[ProtoMember(10)]
		public List<byte[]> EffectComponents { get; set; }

	}

	[Message(OuterMessage.M2C_CreateUnits)]
	[ProtoContract]
	public partial class M2C_CreateUnits: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<UnitInfo> Units { get; set; }

	}

	[Message(OuterMessage.M2C_SyncDataList)]
	[ProtoContract]
	public partial class M2C_SyncDataList: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<byte[]> SyncDataList { get; set; }

	}

	[Message(OuterMessage.M2C_SyncUnitEffects)]
	[ProtoContract]
	public partial class M2C_SyncUnitEffects: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

		[ProtoMember(2)]
		public int AddOrRemove { get; set; }

		[ProtoMember(3)]
		public long EffectObjId { get; set; }

		[ProtoMember(4)]
		public byte[] EffectComponent { get; set; }

	}

	[Message(OuterMessage.M2C_SyncGetCoinShow)]
	[ProtoContract]
	public partial class M2C_SyncGetCoinShow: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

		[ProtoMember(2)]
		public int CoinType { get; set; }

		[ProtoMember(3)]
		public int ChgValue { get; set; }

	}

	[Message(OuterMessage.M2C_CreateMyUnit)]
	[ProtoContract]
	public partial class M2C_CreateMyUnit: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public UnitInfo Unit { get; set; }

	}

	[Message(OuterMessage.C2M_NeedReNoticeUnitIds)]
	[ProtoContract]
	public partial class C2M_NeedReNoticeUnitIds: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public List<long> UnitIds { get; set; }

	}

	[Message(OuterMessage.M2C_StartSceneChange)]
	[ProtoContract]
	public partial class M2C_StartSceneChange: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long SceneInstanceId { get; set; }

		[ProtoMember(2)]
		public string SceneName { get; set; }

	}

	[Message(OuterMessage.M2C_RemoveUnits)]
	[ProtoContract]
	public partial class M2C_RemoveUnits: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<long> Units { get; set; }

	}

	[Message(OuterMessage.C2M_PathfindingResult)]
	[ProtoContract]
	public partial class C2M_PathfindingResult: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.C2M_Stop)]
	[ProtoContract]
	public partial class C2M_Stop: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_PathfindingResult)]
	[ProtoContract]
	public partial class M2C_PathfindingResult: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long Id { get; set; }

		[ProtoMember(2)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(3)]
		public List<Unity.Mathematics.float3> Points { get; set; }

	}

	[Message(OuterMessage.M2C_Stop)]
	[ProtoContract]
	public partial class M2C_Stop: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int Error { get; set; }

		[ProtoMember(2)]
		public long Id { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(4)]
		public Unity.Mathematics.quaternion Rotation { get; set; }

	}

	[ResponseType(nameof(G2C_Ping))]
	[Message(OuterMessage.C2G_Ping)]
	[ProtoContract]
	public partial class C2G_Ping: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_Ping)]
	[ProtoContract]
	public partial class G2C_Ping: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long Time { get; set; }

	}

	[Message(OuterMessage.G2C_Test)]
	[ProtoContract]
	public partial class G2C_Test: ProtoObject, IMessage
	{
	}

	[ResponseType(nameof(M2C_Reload))]
	[Message(OuterMessage.C2M_Reload)]
	[ProtoContract]
	public partial class C2M_Reload: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string Password { get; set; }

	}

	[Message(OuterMessage.M2C_Reload)]
	[ProtoContract]
	public partial class M2C_Reload: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2C_Login))]
	[Message(OuterMessage.C2R_Login)]
	[ProtoContract]
	public partial class C2R_Login: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string Password { get; set; }

		[ProtoMember(4)]
		public int LoginType { get; set; }

	}

	[Message(OuterMessage.R2C_Login)]
	[ProtoContract]
	public partial class R2C_Login: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public string Address { get; set; }

		[ProtoMember(5)]
		public long Key { get; set; }

		[ProtoMember(6)]
		public long GateId { get; set; }

		[ProtoMember(7)]
		public int IsFirstLogin { get; set; }

	}

	[ResponseType(nameof(R2C_LoginWithAuth))]
	[Message(OuterMessage.C2R_LoginWithAuth)]
	[ProtoContract]
	public partial class C2R_LoginWithAuth: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string Password { get; set; }

		[ProtoMember(4)]
		public int LoginType { get; set; }

		[ProtoMember(5)]
		public string Name { get; set; }

		[ProtoMember(6)]
		public string Token { get; set; }

	}

	[Message(OuterMessage.R2C_LoginWithAuth)]
	[ProtoContract]
	public partial class R2C_LoginWithAuth: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public string Address { get; set; }

		[ProtoMember(5)]
		public long Key { get; set; }

		[ProtoMember(6)]
		public long GateId { get; set; }

		[ProtoMember(7)]
		public int IsFirstLogin { get; set; }

	}

	[ResponseType(nameof(G2C_BindAccountWithAuth))]
	[Message(OuterMessage.C2G_BindAccountWithAuth)]
	[ProtoContract]
	public partial class C2G_BindAccountWithAuth: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string BindAccount { get; set; }

		[ProtoMember(4)]
		public int LoginType { get; set; }

		[ProtoMember(5)]
		public string Name { get; set; }

		[ProtoMember(6)]
		public string Token { get; set; }

	}

	[Message(OuterMessage.G2C_BindAccountWithAuth)]
	[ProtoContract]
	public partial class G2C_BindAccountWithAuth: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int IsBindSuccess { get; set; }

	}

	[ResponseType(nameof(G2C_LoginGate))]
	[Message(OuterMessage.C2G_LoginGate)]
	[ProtoContract]
	public partial class C2G_LoginGate: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long GateId { get; set; }

	}

	[Message(OuterMessage.G2C_LoginGate)]
	[ProtoContract]
	public partial class G2C_LoginGate: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long PlayerId { get; set; }

		[ProtoMember(5)]
		public byte[] PlayerComponentBytes { get; set; }

		[ProtoMember(6)]
		public byte[] PlayerStatusComponentBytes { get; set; }

	}

	[ResponseType(nameof(G2C_LoginOut))]
	[Message(OuterMessage.C2G_LoginOut)]
	[ProtoContract]
	public partial class C2G_LoginOut: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_LoginOut)]
	[ProtoContract]
	public partial class G2C_LoginOut: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_ReLoginGate))]
	[Message(OuterMessage.C2G_ReLoginGate)]
	[ProtoContract]
	public partial class C2G_ReLoginGate: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

	}

	[Message(OuterMessage.G2C_ReLoginGate)]
	[ProtoContract]
	public partial class G2C_ReLoginGate: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.G2C_TestHotfixMessage)]
	[ProtoContract]
	public partial class G2C_TestHotfixMessage: ProtoObject, IMessage
	{
		[ProtoMember(1)]
		public string Info { get; set; }

	}

	[ResponseType(nameof(M2C_TestRobotCase))]
	[Message(OuterMessage.C2M_TestRobotCase)]
	[ProtoContract]
	public partial class C2M_TestRobotCase: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int N { get; set; }

	}

	[Message(OuterMessage.M2C_TestRobotCase)]
	[ProtoContract]
	public partial class M2C_TestRobotCase: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int N { get; set; }

	}

	[Message(OuterMessage.C2M_TestRobotCase2)]
	[ProtoContract]
	public partial class C2M_TestRobotCase2: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int N { get; set; }

	}

	[Message(OuterMessage.M2C_TestRobotCase2)]
	[ProtoContract]
	public partial class M2C_TestRobotCase2: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int N { get; set; }

	}

	[ResponseType(nameof(M2C_TransferMap))]
	[Message(OuterMessage.C2M_TransferMap)]
	[ProtoContract]
	public partial class C2M_TransferMap: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_TransferMap)]
	[ProtoContract]
	public partial class M2C_TransferMap: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_Benchmark))]
	[Message(OuterMessage.C2G_Benchmark)]
	[ProtoContract]
	public partial class C2G_Benchmark: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_Benchmark)]
	[ProtoContract]
	public partial class G2C_Benchmark: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_LearnSkill))]
	[Message(OuterMessage.C2M_LearnSkill)]
	[ProtoContract]
	public partial class C2M_LearnSkill: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string SkillId { get; set; }

	}

	[Message(OuterMessage.M2C_LearnSkill)]
	[ProtoContract]
	public partial class M2C_LearnSkill: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_CastSkill))]
	[Message(OuterMessage.C2M_CastSkill)]
	[ProtoContract]
	public partial class C2M_CastSkill: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string SkillId { get; set; }

	}

	[Message(OuterMessage.M2C_CastSkill)]
	[ProtoContract]
	public partial class M2C_CastSkill: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_CallTower))]
	[Message(OuterMessage.C2M_CallTower)]
	[ProtoContract]
	public partial class C2M_CallTower: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string TowerUnitCfgId { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(4)]
		public string CreateActionIds { get; set; }

	}

	[Message(OuterMessage.M2C_CallTower)]
	[ProtoContract]
	public partial class M2C_CallTower: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_CallMonster))]
	[Message(OuterMessage.C2M_CallMonster)]
	[ProtoContract]
	public partial class C2M_CallMonster: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string MonsterUnitCfgId { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(4)]
		public int Count { get; set; }

		[ProtoMember(5)]
		public string CreateActionIds { get; set; }

	}

	[Message(OuterMessage.M2C_CallMonster)]
	[ProtoContract]
	public partial class M2C_CallMonster: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_PKMovePlayer))]
	[Message(OuterMessage.C2M_PKMovePlayer)]
	[ProtoContract]
	public partial class C2M_PKMovePlayer: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long TowerUnitId { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.M2C_PKMovePlayer)]
	[ProtoContract]
	public partial class M2C_PKMovePlayer: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_PKMoveTower))]
	[Message(OuterMessage.C2M_PKMoveTower)]
	[ProtoContract]
	public partial class C2M_PKMoveTower: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long TowerUnitId { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.M2C_PKMoveTower)]
	[ProtoContract]
	public partial class M2C_PKMoveTower: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_ClearMyTower))]
	[Message(OuterMessage.C2M_ClearMyTower)]
	[ProtoContract]
	public partial class C2M_ClearMyTower: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_ClearMyTower)]
	[ProtoContract]
	public partial class M2C_ClearMyTower: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_ClearAllMonster))]
	[Message(OuterMessage.C2M_ClearAllMonster)]
	[ProtoContract]
	public partial class C2M_ClearAllMonster: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_ClearAllMonster)]
	[ProtoContract]
	public partial class M2C_ClearAllMonster: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_GetRoomList))]
	[Message(OuterMessage.C2G_GetRoomList)]
	[ProtoContract]
	public partial class C2G_GetRoomList: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int IsARRoom { get; set; }

	}

	[Message(OuterMessage.G2C_GetRoomList)]
	[ProtoContract]
	public partial class G2C_GetRoomList: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public List<byte[]> RoomInfos { get; set; }

	}

	[ResponseType(nameof(G2C_GetRoomInfo))]
	[Message(OuterMessage.C2G_GetRoomInfo)]
	[ProtoContract]
	public partial class C2G_GetRoomInfo: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long RoomId { get; set; }

	}

	[Message(OuterMessage.G2C_GetRoomInfo)]
	[ProtoContract]
	public partial class G2C_GetRoomInfo: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public byte[] RoomInfo { get; set; }

		[ProtoMember(5)]
		public List<byte[]> RoomMemberInfos { get; set; }

	}

	[Message(OuterMessage.R2C_RoomInfoChgNotice)]
	[ProtoContract]
	public partial class R2C_RoomInfoChgNotice: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[ResponseType(nameof(G2C_CreateRoom))]
	[Message(OuterMessage.C2G_CreateRoom)]
	[ProtoContract]
	public partial class C2G_CreateRoom: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string BattleCfgId { get; set; }

		[ProtoMember(3)]
		public int RoomType { get; set; }

		[ProtoMember(4)]
		public int SubRoomType { get; set; }

	}

	[Message(OuterMessage.G2C_CreateRoom)]
	[ProtoContract]
	public partial class G2C_CreateRoom: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long RoomId { get; set; }

	}

	[ResponseType(nameof(G2C_JoinRoom))]
	[Message(OuterMessage.C2G_JoinRoom)]
	[ProtoContract]
	public partial class C2G_JoinRoom: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long RoomId { get; set; }

	}

	[Message(OuterMessage.G2C_JoinRoom)]
	[ProtoContract]
	public partial class G2C_JoinRoom: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_QuitRoom))]
	[Message(OuterMessage.C2G_QuitRoom)]
	[ProtoContract]
	public partial class C2G_QuitRoom: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_QuitRoom)]
	[ProtoContract]
	public partial class G2C_QuitRoom: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_KickMemberOutRoom))]
	[Message(OuterMessage.C2G_KickMemberOutRoom)]
	[ProtoContract]
	public partial class C2G_KickMemberOutRoom: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long BeKickPlayerId { get; set; }

	}

	[Message(OuterMessage.G2C_KickMemberOutRoom)]
	[ProtoContract]
	public partial class G2C_KickMemberOutRoom: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.G2C_BeKickMemberOutRoom)]
	[ProtoContract]
	public partial class G2C_BeKickMemberOutRoom: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int IsKickWhenBattle { get; set; }

	}

	[ResponseType(nameof(G2C_ChgRoomMemberStatus))]
	[Message(OuterMessage.C2G_ChgRoomMemberStatus)]
	[ProtoContract]
	public partial class C2G_ChgRoomMemberStatus: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int IsReady { get; set; }

	}

	[Message(OuterMessage.G2C_ChgRoomMemberStatus)]
	[ProtoContract]
	public partial class G2C_ChgRoomMemberStatus: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int IsReady { get; set; }

	}

	[ResponseType(nameof(G2C_ChgRoomMemberSeat))]
	[Message(OuterMessage.C2G_ChgRoomMemberSeat)]
	[ProtoContract]
	public partial class C2G_ChgRoomMemberSeat: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int NewSeat { get; set; }

	}

	[Message(OuterMessage.G2C_ChgRoomMemberSeat)]
	[ProtoContract]
	public partial class G2C_ChgRoomMemberSeat: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_ChgRoomMemberTeam))]
	[Message(OuterMessage.C2G_ChgRoomMemberTeam)]
	[ProtoContract]
	public partial class C2G_ChgRoomMemberTeam: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int NewTeam { get; set; }

	}

	[Message(OuterMessage.G2C_ChgRoomMemberTeam)]
	[ProtoContract]
	public partial class G2C_ChgRoomMemberTeam: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_SetARRoomInfo))]
	[Message(OuterMessage.C2G_SetARRoomInfo)]
	[ProtoContract]
	public partial class C2G_SetARRoomInfo: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int ARMapScale { get; set; }

		[ProtoMember(3)]
		public int ARMeshType { get; set; }

		[ProtoMember(4)]
		public string ARSceneId { get; set; }

		[ProtoMember(5)]
		public string ARMeshDownLoadUrl { get; set; }

		[ProtoMember(6)]
		public byte[] ARMeshBytes { get; set; }

	}

	[Message(OuterMessage.G2C_SetARRoomInfo)]
	[ProtoContract]
	public partial class G2C_SetARRoomInfo: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_ChgRoomBattleLevelCfg))]
	[Message(OuterMessage.C2G_ChgRoomBattleLevelCfg)]
	[ProtoContract]
	public partial class C2G_ChgRoomBattleLevelCfg: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string NewBattleCfgId { get; set; }

	}

	[Message(OuterMessage.G2C_ChgRoomBattleLevelCfg)]
	[ProtoContract]
	public partial class G2C_ChgRoomBattleLevelCfg: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_ReturnBackBattle))]
	[Message(OuterMessage.C2G_ReturnBackBattle)]
	[ProtoContract]
	public partial class C2G_ReturnBackBattle: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_ReturnBackBattle)]
	[ProtoContract]
	public partial class G2C_ReturnBackBattle: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_MemberQuitBattle))]
	[Message(OuterMessage.C2M_MemberQuitBattle)]
	[ProtoContract]
	public partial class C2M_MemberQuitBattle: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_MemberQuitBattle)]
	[ProtoContract]
	public partial class M2C_MemberQuitBattle: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_MemberReturnRoomFromBattle))]
	[Message(OuterMessage.C2M_MemberReturnRoomFromBattle)]
	[ProtoContract]
	public partial class C2M_MemberReturnRoomFromBattle: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_MemberReturnRoomFromBattle)]
	[ProtoContract]
	public partial class M2C_MemberReturnRoomFromBattle: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.M2C_GamePlayChgNotice)]
	[ProtoContract]
	public partial class M2C_GamePlayChgNotice: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public byte[] GamePlayInfo { get; set; }

		[ProtoMember(3)]
		public List<byte[]> Components { get; set; }

	}

	[Message(OuterMessage.M2C_GamePlayCoinChgNotice)]
	[ProtoContract]
	public partial class M2C_GamePlayCoinChgNotice: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int GetCoinType { get; set; }

		[ProtoMember(3)]
		public byte[] GamePlayPlayerListComponent { get; set; }

	}

	[Message(OuterMessage.M2C_GamePlayStatisticalDataChgNotice)]
	[ProtoContract]
	public partial class M2C_GamePlayStatisticalDataChgNotice: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public byte[] GamePlayStatisticalDataComponent { get; set; }

	}

	[Message(OuterMessage.M2C_GamePlayModeChgNotice)]
	[ProtoContract]
	public partial class M2C_GamePlayModeChgNotice: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public byte[] GamePlayModeInfo { get; set; }

		[ProtoMember(3)]
		public List<byte[]> Components { get; set; }

		[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
		[ProtoMember(4)]
		public Dictionary<int, int> CoinKV { get; set; }
	}

	[ResponseType(nameof(M2C_PutHome))]
	[Message(OuterMessage.C2M_PutHome)]
	[ProtoContract]
	public partial class C2M_PutHome: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string UnitCfgId { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.M2C_PutHome)]
	[ProtoContract]
	public partial class M2C_PutHome: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_PutMonsterCall))]
	[Message(OuterMessage.C2M_PutMonsterCall)]
	[ProtoContract]
	public partial class C2M_PutMonsterCall: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string UnitCfgId { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.M2C_PutMonsterCall)]
	[ProtoContract]
	public partial class M2C_PutMonsterCall: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_BuyPlayerTower))]
	[Message(OuterMessage.C2M_BuyPlayerTower)]
	[ProtoContract]
	public partial class C2M_BuyPlayerTower: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Index { get; set; }

	}

	[Message(OuterMessage.M2C_BuyPlayerTower)]
	[ProtoContract]
	public partial class M2C_BuyPlayerTower: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_RefreshBuyPlayerTower))]
	[Message(OuterMessage.C2M_RefreshBuyPlayerTower)]
	[ProtoContract]
	public partial class C2M_RefreshBuyPlayerTower: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_RefreshBuyPlayerTower)]
	[ProtoContract]
	public partial class M2C_RefreshBuyPlayerTower: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_CallOwnTower))]
	[Message(OuterMessage.C2M_CallOwnTower)]
	[ProtoContract]
	public partial class C2M_CallOwnTower: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string TowerUnitCfgId { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.M2C_CallOwnTower)]
	[ProtoContract]
	public partial class M2C_CallOwnTower: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_UpgradePlayerTower))]
	[Message(OuterMessage.C2M_UpgradePlayerTower)]
	[ProtoContract]
	public partial class C2M_UpgradePlayerTower: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long TowerUnitId { get; set; }

		[ProtoMember(3)]
		public int OnlyChkPool { get; set; }

	}

	[Message(OuterMessage.M2C_UpgradePlayerTower)]
	[ProtoContract]
	public partial class M2C_UpgradePlayerTower: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_ScalePlayerTower))]
	[Message(OuterMessage.C2M_ScalePlayerTower)]
	[ProtoContract]
	public partial class C2M_ScalePlayerTower: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long TowerUnitId { get; set; }

	}

	[Message(OuterMessage.M2C_ScalePlayerTower)]
	[ProtoContract]
	public partial class M2C_ScalePlayerTower: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_ScalePlayerTowerCard))]
	[Message(OuterMessage.C2M_ScalePlayerTowerCard)]
	[ProtoContract]
	public partial class C2M_ScalePlayerTowerCard: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string TowerCfgId { get; set; }

	}

	[Message(OuterMessage.M2C_ScalePlayerTowerCard)]
	[ProtoContract]
	public partial class M2C_ScalePlayerTowerCard: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_ReclaimPlayerTower))]
	[Message(OuterMessage.C2M_ReclaimPlayerTower)]
	[ProtoContract]
	public partial class C2M_ReclaimPlayerTower: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long TowerUnitId { get; set; }

	}

	[Message(OuterMessage.M2C_ReclaimPlayerTower)]
	[ProtoContract]
	public partial class M2C_ReclaimPlayerTower: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_MovePlayerTower))]
	[Message(OuterMessage.C2M_MovePlayerTower)]
	[ProtoContract]
	public partial class C2M_MovePlayerTower: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long TowerUnitId { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.M2C_MovePlayerTower)]
	[ProtoContract]
	public partial class M2C_MovePlayerTower: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_ReadyWhenRestTime))]
	[Message(OuterMessage.C2M_ReadyWhenRestTime)]
	[ProtoContract]
	public partial class C2M_ReadyWhenRestTime: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_ReadyWhenRestTime)]
	[ProtoContract]
	public partial class M2C_ReadyWhenRestTime: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_ReScan))]
	[Message(OuterMessage.C2M_ReScan)]
	[ProtoContract]
	public partial class C2M_ReScan: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_ReScan)]
	[ProtoContract]
	public partial class M2C_ReScan: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_GetMonsterCall2HeadQuarterPath))]
	[Message(OuterMessage.C2M_GetMonsterCall2HeadQuarterPath)]
	[ProtoContract]
	public partial class C2M_GetMonsterCall2HeadQuarterPath: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int HomeTeamFlagType { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.M2C_GetMonsterCall2HeadQuarterPath)]
	[ProtoContract]
	public partial class M2C_GetMonsterCall2HeadQuarterPath: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(5)]
		public List<Unity.Mathematics.float3> Points { get; set; }

	}

	[ResponseType(nameof(M2C_ChkRay))]
	[Message(OuterMessage.C2M_ChkRay)]
	[ProtoContract]
	public partial class C2M_ChkRay: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public Unity.Mathematics.float3 StartPosition { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 EndPosition { get; set; }

	}

	[Message(OuterMessage.M2C_ChkRay)]
	[ProtoContract]
	public partial class M2C_ChkRay: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int HitRet { get; set; }

		[ProtoMember(5)]
		public Unity.Mathematics.float3 HitPosition { get; set; }

	}

	[Message(OuterMessage.C2M_SendARCameraPos)]
	[ProtoContract]
	public partial class C2M_SendARCameraPos: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public Unity.Mathematics.float3 ARCameraPosition { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 ARCameraHitPosition { get; set; }

	}

	[Message(OuterMessage.C2M_GetNumericUnit)]
	[ProtoContract]
	public partial class C2M_GetNumericUnit: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public List<long> UnitIdList { get; set; }

		[ProtoMember(3)]
		public List<int> NumericKeyList { get; set; }

	}

	[ResponseType(nameof(G2C_GetRank))]
	[Message(OuterMessage.C2G_GetRank)]
	[ProtoContract]
	public partial class C2G_GetRank: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int RankType { get; set; }

	}

	[Message(OuterMessage.G2C_GetRank)]
	[ProtoContract]
	public partial class G2C_GetRank: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public byte[] RankShowComponentBytes { get; set; }

	}

	[ResponseType(nameof(G2C_GetRankedMoreThan))]
	[Message(OuterMessage.C2G_GetRankedMoreThan)]
	[ProtoContract]
	public partial class C2G_GetRankedMoreThan: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int RankType { get; set; }

		[ProtoMember(3)]
		public long Score { get; set; }

	}

	[Message(OuterMessage.G2C_GetRankedMoreThan)]
	[ProtoContract]
	public partial class G2C_GetRankedMoreThan: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long Rank { get; set; }

		[ProtoMember(5)]
		public int RankedMoreThan { get; set; }

	}

	[ResponseType(nameof(G2C_GetPlayerCache))]
	[Message(OuterMessage.C2G_GetPlayerCache)]
	[ProtoContract]
	public partial class C2G_GetPlayerCache: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public int PlayerModelType { get; set; }

	}

	[Message(OuterMessage.G2C_GetPlayerCache)]
	[ProtoContract]
	public partial class G2C_GetPlayerCache: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public byte[] PlayerModelComponentBytes { get; set; }

	}

	[Message(OuterMessage.G2C_PlayerCacheChgNotice)]
	[ProtoContract]
	public partial class G2C_PlayerCacheChgNotice: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int PlayerModelType { get; set; }

	}

	[ResponseType(nameof(G2C_SetPlayerCache))]
	[Message(OuterMessage.C2G_SetPlayerCache)]
	[ProtoContract]
	public partial class C2G_SetPlayerCache: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public int PlayerModelType { get; set; }

		[ProtoMember(4)]
		public byte[] PlayerModelComponentBytes { get; set; }

		[ProtoMember(5)]
		public List<string> SetPlayerKeys { get; set; }

	}

	[Message(OuterMessage.G2C_SetPlayerCache)]
	[ProtoContract]
	public partial class G2C_SetPlayerCache: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_AddPhysicalStrenthByAd))]
	[Message(OuterMessage.C2G_AddPhysicalStrenthByAd)]
	[ProtoContract]
	public partial class C2G_AddPhysicalStrenthByAd: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

	}

	[Message(OuterMessage.G2C_AddPhysicalStrenthByAd)]
	[ProtoContract]
	public partial class G2C_AddPhysicalStrenthByAd: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_BattleRecoverCancel))]
	[Message(OuterMessage.C2M_BattleRecoverCancel)]
	[ProtoContract]
	public partial class C2M_BattleRecoverCancel: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int IsFinished { get; set; }

	}

	[Message(OuterMessage.M2C_BattleRecoverCancel)]
	[ProtoContract]
	public partial class M2C_BattleRecoverCancel: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_BattleRecoverConfirm))]
	[Message(OuterMessage.C2M_BattleRecoverConfirm)]
	[ProtoContract]
	public partial class C2M_BattleRecoverConfirm: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int IsFinished { get; set; }

	}

	[Message(OuterMessage.M2C_BattleRecoverConfirm)]
	[ProtoContract]
	public partial class M2C_BattleRecoverConfirm: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_ChkGameJudgeChoose))]
	[Message(OuterMessage.C2G_ChkGameJudgeChoose)]
	[ProtoContract]
	public partial class C2G_ChkGameJudgeChoose: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_ChkGameJudgeChoose)]
	[ProtoContract]
	public partial class G2C_ChkGameJudgeChoose: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int IsNeed { get; set; }

	}

	[ResponseType(nameof(G2C_RecordGameJudgeChoose))]
	[Message(OuterMessage.C2G_RecordGameJudgeChoose)]
	[ProtoContract]
	public partial class C2G_RecordGameJudgeChoose: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int GameJudgeChooseType { get; set; }

		[ProtoMember(3)]
		public string ComplainMsg { get; set; }

	}

	[Message(OuterMessage.G2C_RecordGameJudgeChoose)]
	[ProtoContract]
	public partial class G2C_RecordGameJudgeChoose: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.C2M_SetStopActorMoveWhenDebug)]
	[ProtoContract]
	public partial class C2M_SetStopActorMoveWhenDebug: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int IsStopActorMove { get; set; }

	}

	[Message(OuterMessage.C2M_ForceGameEndWhenDebug)]
	[ProtoContract]
	public partial class C2M_ForceGameEndWhenDebug: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.C2G_SetMyRankScoreWhenDebug)]
	[ProtoContract]
	public partial class C2G_SetMyRankScoreWhenDebug: ProtoObject, IMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int RankType { get; set; }

		[ProtoMember(3)]
		public int Score { get; set; }

		[ProtoMember(4)]
		public int KillNum { get; set; }

	}

	[Message(OuterMessage.C2G_ClearRankWhenDebug)]
	[ProtoContract]
	public partial class C2G_ClearRankWhenDebug: ProtoObject, IMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int RankType { get; set; }

	}

	[Message(OuterMessage.C2G_ClearPlayerRankWhenDebug)]
	[ProtoContract]
	public partial class C2G_ClearPlayerRankWhenDebug: ProtoObject, IMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int RankType { get; set; }

		[ProtoMember(3)]
		public long PlayerId { get; set; }

	}

	[Message(OuterMessage.C2G_ResetPlayerFunctionMenuStatusWhenDebug)]
	[ProtoContract]
	public partial class C2G_ResetPlayerFunctionMenuStatusWhenDebug: ProtoObject, IMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public int OperateType { get; set; }

		[ProtoMember(4)]
		public string FunctionMenuCfgIds { get; set; }

	}

	public static class OuterMessage
	{
		 public const ushort HttpGetRouterResponse = 10002;
		 public const ushort RouterSync = 10003;
		 public const ushort C2M_TestRequest = 10004;
		 public const ushort M2C_TestResponse = 10005;
		 public const ushort Actor_TransferRequest = 10006;
		 public const ushort Actor_TransferResponse = 10007;
		 public const ushort C2G_EnterMap = 10008;
		 public const ushort G2C_EnterMap = 10009;
		 public const ushort G2C_EnterBattleNotice = 10010;
		 public const ushort C2G_GetPlayerStatus = 10011;
		 public const ushort G2C_GetPlayerStatus = 10012;
		 public const ushort G2C_PlayerStatusChgNotice = 10013;
		 public const ushort G2C_LoginInAtOtherWhere = 10014;
		 public const ushort MoveInfo = 10015;
		 public const ushort UnitInfo = 10016;
		 public const ushort M2C_CreateUnits = 10017;
		 public const ushort M2C_SyncDataList = 10018;
		 public const ushort M2C_SyncUnitEffects = 10019;
		 public const ushort M2C_SyncGetCoinShow = 10020;
		 public const ushort M2C_CreateMyUnit = 10021;
		 public const ushort C2M_NeedReNoticeUnitIds = 10022;
		 public const ushort M2C_StartSceneChange = 10023;
		 public const ushort M2C_RemoveUnits = 10024;
		 public const ushort C2M_PathfindingResult = 10025;
		 public const ushort C2M_Stop = 10026;
		 public const ushort M2C_PathfindingResult = 10027;
		 public const ushort M2C_Stop = 10028;
		 public const ushort C2G_Ping = 10029;
		 public const ushort G2C_Ping = 10030;
		 public const ushort G2C_Test = 10031;
		 public const ushort C2M_Reload = 10032;
		 public const ushort M2C_Reload = 10033;
		 public const ushort C2R_Login = 10034;
		 public const ushort R2C_Login = 10035;
		 public const ushort C2R_LoginWithAuth = 10036;
		 public const ushort R2C_LoginWithAuth = 10037;
		 public const ushort C2G_BindAccountWithAuth = 10038;
		 public const ushort G2C_BindAccountWithAuth = 10039;
		 public const ushort C2G_LoginGate = 10040;
		 public const ushort G2C_LoginGate = 10041;
		 public const ushort C2G_LoginOut = 10042;
		 public const ushort G2C_LoginOut = 10043;
		 public const ushort C2G_ReLoginGate = 10044;
		 public const ushort G2C_ReLoginGate = 10045;
		 public const ushort G2C_TestHotfixMessage = 10046;
		 public const ushort C2M_TestRobotCase = 10047;
		 public const ushort M2C_TestRobotCase = 10048;
		 public const ushort C2M_TestRobotCase2 = 10049;
		 public const ushort M2C_TestRobotCase2 = 10050;
		 public const ushort C2M_TransferMap = 10051;
		 public const ushort M2C_TransferMap = 10052;
		 public const ushort C2G_Benchmark = 10053;
		 public const ushort G2C_Benchmark = 10054;
		 public const ushort C2M_LearnSkill = 10055;
		 public const ushort M2C_LearnSkill = 10056;
		 public const ushort C2M_CastSkill = 10057;
		 public const ushort M2C_CastSkill = 10058;
		 public const ushort C2M_CallTower = 10059;
		 public const ushort M2C_CallTower = 10060;
		 public const ushort C2M_CallMonster = 10061;
		 public const ushort M2C_CallMonster = 10062;
		 public const ushort C2M_PKMovePlayer = 10063;
		 public const ushort M2C_PKMovePlayer = 10064;
		 public const ushort C2M_PKMoveTower = 10065;
		 public const ushort M2C_PKMoveTower = 10066;
		 public const ushort C2M_ClearMyTower = 10067;
		 public const ushort M2C_ClearMyTower = 10068;
		 public const ushort C2M_ClearAllMonster = 10069;
		 public const ushort M2C_ClearAllMonster = 10070;
		 public const ushort C2G_GetRoomList = 10071;
		 public const ushort G2C_GetRoomList = 10072;
		 public const ushort C2G_GetRoomInfo = 10073;
		 public const ushort G2C_GetRoomInfo = 10074;
		 public const ushort R2C_RoomInfoChgNotice = 10075;
		 public const ushort C2G_CreateRoom = 10076;
		 public const ushort G2C_CreateRoom = 10077;
		 public const ushort C2G_JoinRoom = 10078;
		 public const ushort G2C_JoinRoom = 10079;
		 public const ushort C2G_QuitRoom = 10080;
		 public const ushort G2C_QuitRoom = 10081;
		 public const ushort C2G_KickMemberOutRoom = 10082;
		 public const ushort G2C_KickMemberOutRoom = 10083;
		 public const ushort G2C_BeKickMemberOutRoom = 10084;
		 public const ushort C2G_ChgRoomMemberStatus = 10085;
		 public const ushort G2C_ChgRoomMemberStatus = 10086;
		 public const ushort C2G_ChgRoomMemberSeat = 10087;
		 public const ushort G2C_ChgRoomMemberSeat = 10088;
		 public const ushort C2G_ChgRoomMemberTeam = 10089;
		 public const ushort G2C_ChgRoomMemberTeam = 10090;
		 public const ushort C2G_SetARRoomInfo = 10091;
		 public const ushort G2C_SetARRoomInfo = 10092;
		 public const ushort C2G_ChgRoomBattleLevelCfg = 10093;
		 public const ushort G2C_ChgRoomBattleLevelCfg = 10094;
		 public const ushort C2G_ReturnBackBattle = 10095;
		 public const ushort G2C_ReturnBackBattle = 10096;
		 public const ushort C2M_MemberQuitBattle = 10097;
		 public const ushort M2C_MemberQuitBattle = 10098;
		 public const ushort C2M_MemberReturnRoomFromBattle = 10099;
		 public const ushort M2C_MemberReturnRoomFromBattle = 10100;
		 public const ushort M2C_GamePlayChgNotice = 10101;
		 public const ushort M2C_GamePlayCoinChgNotice = 10102;
		 public const ushort M2C_GamePlayStatisticalDataChgNotice = 10103;
		 public const ushort M2C_GamePlayModeChgNotice = 10104;
		 public const ushort C2M_PutHome = 10105;
		 public const ushort M2C_PutHome = 10106;
		 public const ushort C2M_PutMonsterCall = 10107;
		 public const ushort M2C_PutMonsterCall = 10108;
		 public const ushort C2M_BuyPlayerTower = 10109;
		 public const ushort M2C_BuyPlayerTower = 10110;
		 public const ushort C2M_RefreshBuyPlayerTower = 10111;
		 public const ushort M2C_RefreshBuyPlayerTower = 10112;
		 public const ushort C2M_CallOwnTower = 10113;
		 public const ushort M2C_CallOwnTower = 10114;
		 public const ushort C2M_UpgradePlayerTower = 10115;
		 public const ushort M2C_UpgradePlayerTower = 10116;
		 public const ushort C2M_ScalePlayerTower = 10117;
		 public const ushort M2C_ScalePlayerTower = 10118;
		 public const ushort C2M_ScalePlayerTowerCard = 10119;
		 public const ushort M2C_ScalePlayerTowerCard = 10120;
		 public const ushort C2M_ReclaimPlayerTower = 10121;
		 public const ushort M2C_ReclaimPlayerTower = 10122;
		 public const ushort C2M_MovePlayerTower = 10123;
		 public const ushort M2C_MovePlayerTower = 10124;
		 public const ushort C2M_ReadyWhenRestTime = 10125;
		 public const ushort M2C_ReadyWhenRestTime = 10126;
		 public const ushort C2M_ReScan = 10127;
		 public const ushort M2C_ReScan = 10128;
		 public const ushort C2M_GetMonsterCall2HeadQuarterPath = 10129;
		 public const ushort M2C_GetMonsterCall2HeadQuarterPath = 10130;
		 public const ushort C2M_ChkRay = 10131;
		 public const ushort M2C_ChkRay = 10132;
		 public const ushort C2M_SendARCameraPos = 10133;
		 public const ushort C2M_GetNumericUnit = 10134;
		 public const ushort C2G_GetRank = 10135;
		 public const ushort G2C_GetRank = 10136;
		 public const ushort C2G_GetRankedMoreThan = 10137;
		 public const ushort G2C_GetRankedMoreThan = 10138;
		 public const ushort C2G_GetPlayerCache = 10139;
		 public const ushort G2C_GetPlayerCache = 10140;
		 public const ushort G2C_PlayerCacheChgNotice = 10141;
		 public const ushort C2G_SetPlayerCache = 10142;
		 public const ushort G2C_SetPlayerCache = 10143;
		 public const ushort C2G_AddPhysicalStrenthByAd = 10144;
		 public const ushort G2C_AddPhysicalStrenthByAd = 10145;
		 public const ushort C2M_BattleRecoverCancel = 10146;
		 public const ushort M2C_BattleRecoverCancel = 10147;
		 public const ushort C2M_BattleRecoverConfirm = 10148;
		 public const ushort M2C_BattleRecoverConfirm = 10149;
		 public const ushort C2G_ChkGameJudgeChoose = 10150;
		 public const ushort G2C_ChkGameJudgeChoose = 10151;
		 public const ushort C2G_RecordGameJudgeChoose = 10152;
		 public const ushort G2C_RecordGameJudgeChoose = 10153;
		 public const ushort C2M_SetStopActorMoveWhenDebug = 10154;
		 public const ushort C2M_ForceGameEndWhenDebug = 10155;
		 public const ushort C2G_SetMyRankScoreWhenDebug = 10156;
		 public const ushort C2G_ClearRankWhenDebug = 10157;
		 public const ushort C2G_ClearPlayerRankWhenDebug = 10158;
		 public const ushort C2G_ResetPlayerFunctionMenuStatusWhenDebug = 10159;
	}
}
