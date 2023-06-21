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
	public partial class M2C_TestResponse: ProtoObject, IActorResponse
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
		public string MapName { get; set; }

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

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.G2C_PlayerStatusChgNotice)]
	[ProtoContract]
	public partial class G2C_PlayerStatusChgNotice: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public string PlayerStatus { get; set; }

		[ProtoMember(5)]
		public long RoomId { get; set; }

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
		public int Type { get; set; }

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(5)]
		public Unity.Mathematics.float3 Forward { get; set; }

		[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
		[ProtoMember(6)]
		public Dictionary<int, long> KV { get; set; }
		[ProtoMember(7)]
		public MoveInfo MoveInfo { get; set; }

		[ProtoMember(8)]
		public List<byte[]> Components { get; set; }

		[ProtoMember(9)]
		public List<byte[]> EffectComponents { get; set; }

	}

	[Message(OuterMessage.M2C_CreateUnits)]
	[ProtoContract]
	public partial class M2C_CreateUnits: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<UnitInfo> Units { get; set; }

	}

	[Message(OuterMessage.UnitPosInfo)]
	[ProtoContract]
	public partial class UnitPosInfo: ProtoObject
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(5)]
		public Unity.Mathematics.float3 Forward { get; set; }

	}

	[Message(OuterMessage.M2C_SyncPosUnits)]
	[ProtoContract]
	public partial class M2C_SyncPosUnits: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<UnitPosInfo> Units { get; set; }

	}

	[Message(OuterMessage.UnitNumericInfo)]
	[ProtoContract]
	public partial class UnitNumericInfo: ProtoObject
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

		[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
		[ProtoMember(6)]
		public Dictionary<int, long> KV { get; set; }
	}

	[Message(OuterMessage.M2C_SyncNumericUnits)]
	[ProtoContract]
	public partial class M2C_SyncNumericUnits: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<UnitNumericInfo> Units { get; set; }

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

	[Message(OuterMessage.M2C_CreateMyUnit)]
	[ProtoContract]
	public partial class M2C_CreateMyUnit: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public UnitInfo Unit { get; set; }

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
		public string PlayerStatus { get; set; }

		[ProtoMember(6)]
		public long RoomId { get; set; }

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

	[ResponseType(nameof(G2C_ReLogin))]
	[Message(OuterMessage.C2G_ReLogin)]
	[ProtoContract]
	public partial class C2G_ReLogin: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long GateId { get; set; }

	}

	[Message(OuterMessage.G2C_ReLogin)]
	[ProtoContract]
	public partial class G2C_ReLogin: ProtoObject, IResponse
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
		public string PlayerStatus { get; set; }

		[ProtoMember(6)]
		public long RoomId { get; set; }

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
		public long UnitId { get; set; }

		[ProtoMember(3)]
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
		public long UnitId { get; set; }

		[ProtoMember(3)]
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

	[ResponseType(nameof(M2C_CallTank))]
	[Message(OuterMessage.C2M_CallTank)]
	[ProtoContract]
	public partial class C2M_CallTank: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string TankUnitCfgId { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.M2C_CallTank)]
	[ProtoContract]
	public partial class M2C_CallTank: ProtoObject, IActorLocationResponse
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
		 public const ushort G2C_PlayerStatusChgNotice = 10011;
		 public const ushort MoveInfo = 10012;
		 public const ushort UnitInfo = 10013;
		 public const ushort M2C_CreateUnits = 10014;
		 public const ushort UnitPosInfo = 10015;
		 public const ushort M2C_SyncPosUnits = 10016;
		 public const ushort UnitNumericInfo = 10017;
		 public const ushort M2C_SyncNumericUnits = 10018;
		 public const ushort M2C_SyncUnitEffects = 10019;
		 public const ushort M2C_CreateMyUnit = 10020;
		 public const ushort M2C_StartSceneChange = 10021;
		 public const ushort M2C_RemoveUnits = 10022;
		 public const ushort C2M_PathfindingResult = 10023;
		 public const ushort C2M_Stop = 10024;
		 public const ushort M2C_PathfindingResult = 10025;
		 public const ushort M2C_Stop = 10026;
		 public const ushort C2G_Ping = 10027;
		 public const ushort G2C_Ping = 10028;
		 public const ushort G2C_Test = 10029;
		 public const ushort C2M_Reload = 10030;
		 public const ushort M2C_Reload = 10031;
		 public const ushort C2R_Login = 10032;
		 public const ushort R2C_Login = 10033;
		 public const ushort C2G_LoginGate = 10034;
		 public const ushort G2C_LoginGate = 10035;
		 public const ushort C2G_LoginOut = 10036;
		 public const ushort G2C_LoginOut = 10037;
		 public const ushort C2G_ReLogin = 10038;
		 public const ushort G2C_ReLogin = 10039;
		 public const ushort G2C_TestHotfixMessage = 10040;
		 public const ushort C2M_TestRobotCase = 10041;
		 public const ushort M2C_TestRobotCase = 10042;
		 public const ushort C2M_TestRobotCase2 = 10043;
		 public const ushort M2C_TestRobotCase2 = 10044;
		 public const ushort C2M_TransferMap = 10045;
		 public const ushort M2C_TransferMap = 10046;
		 public const ushort C2G_Benchmark = 10047;
		 public const ushort G2C_Benchmark = 10048;
		 public const ushort C2M_LearnSkill = 10049;
		 public const ushort M2C_LearnSkill = 10050;
		 public const ushort C2M_CastSkill = 10051;
		 public const ushort M2C_CastSkill = 10052;
		 public const ushort C2M_CallTower = 10053;
		 public const ushort M2C_CallTower = 10054;
		 public const ushort C2M_CallTank = 10055;
		 public const ushort M2C_CallTank = 10056;
		 public const ushort C2G_GetRoomList = 10057;
		 public const ushort G2C_GetRoomList = 10058;
		 public const ushort C2G_GetRoomInfo = 10059;
		 public const ushort G2C_GetRoomInfo = 10060;
		 public const ushort R2C_RoomInfoChgNotice = 10061;
		 public const ushort C2G_CreateRoom = 10062;
		 public const ushort G2C_CreateRoom = 10063;
		 public const ushort C2G_JoinRoom = 10064;
		 public const ushort G2C_JoinRoom = 10065;
		 public const ushort C2G_QuitRoom = 10066;
		 public const ushort G2C_QuitRoom = 10067;
		 public const ushort C2G_ChgRoomMemberStatus = 10068;
		 public const ushort G2C_ChgRoomMemberStatus = 10069;
		 public const ushort C2G_ChgRoomMemberSeat = 10070;
		 public const ushort G2C_ChgRoomMemberSeat = 10071;
		 public const ushort C2G_ReturnBackBattle = 10072;
		 public const ushort G2C_ReturnBackBattle = 10073;
		 public const ushort C2M_MemberQuitBattle = 10074;
		 public const ushort M2C_MemberQuitBattle = 10075;
	}
}
