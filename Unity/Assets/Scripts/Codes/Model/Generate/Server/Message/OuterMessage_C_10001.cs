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
		public Unity.Mathematics.quaternion Rotation { get; set; }

		[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
		[ProtoMember(7)]
		public Dictionary<int, long> KV { get; set; }
		[ProtoMember(8)]
		public List<byte[]> Components { get; set; }

		[ProtoMember(9)]
		public List<byte[]> EffectComponents { get; set; }

	}

	[Message(OuterMessage.UnitBaseInfo)]
	[ProtoContract]
	public partial class UnitBaseInfo: ProtoObject
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
		public Unity.Mathematics.quaternion Rotation { get; set; }

	}

	[Message(OuterMessage.M2C_CreateUnits)]
	[ProtoContract]
	public partial class M2C_CreateUnits: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<UnitInfo> Units { get; set; }

	}

	[Message(OuterMessage.M2C_RemoveUnits)]
	[ProtoContract]
	public partial class M2C_RemoveUnits: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<long> Units { get; set; }

	}

	[Message(OuterMessage.M2C_SyncDataList)]
	[ProtoContract]
	public partial class M2C_SyncDataList: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<byte[]> SyncDataList { get; set; }

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

	[Message(OuterMessage.C2M_NeedReNoticeTowerDefense)]
	[ProtoContract]
	public partial class C2M_NeedReNoticeTowerDefense: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

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

		[ProtoMember(2)]
		public int Fps { get; set; }

		[ProtoMember(3)]
		public long PingTime { get; set; }

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

		[ProtoMember(4)]
		public int IsFirstLogin { get; set; }

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
		public string SkillCfgId { get; set; }

		[ProtoMember(3)]
		public long unitId { get; set; }

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
		public int IsCameraSkill { get; set; }

		[ProtoMember(3)]
		public string SkillCfgId { get; set; }

		[ProtoMember(4)]
		public long unitId { get; set; }

		[ProtoMember(5)]
		public Unity.Mathematics.float3 CameraPosition { get; set; }

		[ProtoMember(6)]
		public Unity.Mathematics.float3 CameraDirect { get; set; }

		[ProtoMember(7)]
		public byte[] SelectHandleBytes { get; set; }

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

	[ResponseType(nameof(M2C_BuySkillEnergy))]
	[Message(OuterMessage.C2M_BuySkillEnergy)]
	[ProtoContract]
	public partial class C2M_BuySkillEnergy: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string SkillCfgId { get; set; }

		[ProtoMember(3)]
		public long unitId { get; set; }

	}

	[Message(OuterMessage.M2C_BuySkillEnergy)]
	[ProtoContract]
	public partial class M2C_BuySkillEnergy: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_RestoreSkillEnergy))]
	[Message(OuterMessage.C2M_RestoreSkillEnergy)]
	[ProtoContract]
	public partial class C2M_RestoreSkillEnergy: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string SkillCfgId { get; set; }

		[ProtoMember(3)]
		public long unitId { get; set; }

	}

	[Message(OuterMessage.M2C_RestoreSkillEnergy)]
	[ProtoContract]
	public partial class M2C_RestoreSkillEnergy: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_BuyItem))]
	[Message(OuterMessage.C2G_BuyItem)]
	[ProtoContract]
	public partial class C2G_BuyItem: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string ItemCfgId { get; set; }

	}

	[Message(OuterMessage.G2C_BuyItem)]
	[ProtoContract]
	public partial class G2C_BuyItem: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_ReplaceBattleDeck))]
	[Message(OuterMessage.C2G_ReplaceBattleDeck)]
	[ProtoContract]
	public partial class C2G_ReplaceBattleDeck: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int ReplaceIndex { get; set; }

		[ProtoMember(3)]
		public string ItemCfgId { get; set; }

	}

	[Message(OuterMessage.G2C_ReplaceBattleDeck)]
	[ProtoContract]
	public partial class G2C_ReplaceBattleDeck: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_UpgradeItem))]
	[Message(OuterMessage.C2G_UpgradeItem)]
	[ProtoContract]
	public partial class C2G_UpgradeItem: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string ItemCfgId { get; set; }

	}

	[Message(OuterMessage.G2C_UpgradeItem)]
	[ProtoContract]
	public partial class G2C_UpgradeItem: ProtoObject, IResponse
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
		public Unity.Mathematics.float3 Forward { get; set; }

		[ProtoMember(5)]
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

	[ResponseType(nameof(M2C_CallTowerActions))]
	[Message(OuterMessage.C2M_CallTowerActions)]
	[ProtoContract]
	public partial class C2M_CallTowerActions: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long TowerUnitId { get; set; }

		[ProtoMember(3)]
		public string AddActionIds { get; set; }

	}

	[Message(OuterMessage.M2C_CallTowerActions)]
	[ProtoContract]
	public partial class M2C_CallTowerActions: ProtoObject, IActorLocationResponse
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
		public Unity.Mathematics.float3 Forward { get; set; }

		[ProtoMember(5)]
		public int Count { get; set; }

		[ProtoMember(6)]
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

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Forward { get; set; }

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

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Forward { get; set; }

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

		[ProtoMember(2)]
		public long TowerUnitId { get; set; }

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
		public int NeedARRoom { get; set; }

		[ProtoMember(3)]
		public int NeedNotARRoom { get; set; }

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
		public byte[] RoomTypeInfo { get; set; }

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
		public string ARSceneMeshId { get; set; }

		[ProtoMember(6)]
		public string ARMeshDownLoadUrl { get; set; }

		[ProtoMember(7)]
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

	[ResponseType(nameof(G2C_GetARRoomInfo))]
	[Message(OuterMessage.C2G_GetARRoomInfo)]
	[ProtoContract]
	public partial class C2G_GetARRoomInfo: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long RoomId { get; set; }

		[ProtoMember(3)]
		public int IsWithARMeshBytes { get; set; }

	}

	[Message(OuterMessage.G2C_GetARRoomInfo)]
	[ProtoContract]
	public partial class G2C_GetARRoomInfo: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int ARMapScale { get; set; }

		[ProtoMember(5)]
		public int ARMeshType { get; set; }

		[ProtoMember(6)]
		public string ARSceneId { get; set; }

		[ProtoMember(7)]
		public string ARSceneMeshId { get; set; }

		[ProtoMember(8)]
		public string ARMeshDownLoadUrl { get; set; }

		[ProtoMember(9)]
		public byte[] ARMeshBytes { get; set; }

	}

	[ResponseType(nameof(G2C_ChgRoomBattleLevelCfg))]
	[Message(OuterMessage.C2G_ChgRoomBattleLevelCfg)]
	[ProtoContract]
	public partial class C2G_ChgRoomBattleLevelCfg: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public byte[] RoomTypeInfo { get; set; }

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

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Forward { get; set; }

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

	[ResponseType(nameof(M2C_ResetHome))]
	[Message(OuterMessage.C2M_ResetHome)]
	[ProtoContract]
	public partial class C2M_ResetHome: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_ResetHome)]
	[ProtoContract]
	public partial class M2C_ResetHome: ProtoObject, IActorLocationResponse
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

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Forward { get; set; }

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

	[ResponseType(nameof(M2C_UpgradeItemUnit))]
	[Message(OuterMessage.C2M_UpgradeItemUnit)]
	[ProtoContract]
	public partial class C2M_UpgradeItemUnit: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long ItemUnitId { get; set; }

		[ProtoMember(3)]
		public int NextLevel { get; set; }

		[ProtoMember(4)]
		public string ItemGiftCfgId { get; set; }

	}

	[Message(OuterMessage.M2C_UpgradeItemUnit)]
	[ProtoContract]
	public partial class M2C_UpgradeItemUnit: ProtoObject, IActorLocationResponse
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
		public string TowerCfgId { get; set; }

		[ProtoMember(4)]
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

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Forward { get; set; }

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

	[ResponseType(nameof(M2C_GetNearestNavMeshPoint))]
	[Message(OuterMessage.C2M_GetNearestNavMeshPoint)]
	[ProtoContract]
	public partial class C2M_GetNearestNavMeshPoint: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.M2C_GetNearestNavMeshPoint)]
	[ProtoContract]
	public partial class M2C_GetNearestNavMeshPoint: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public Unity.Mathematics.float3 NavMeshPosition { get; set; }

		[ProtoMember(5)]
		public long PolygonRef { get; set; }

	}

	[ResponseType(nameof(M2C_GetNavMeshFromPosition))]
	[Message(OuterMessage.C2M_GetNavMeshFromPosition)]
	[ProtoContract]
	public partial class C2M_GetNavMeshFromPosition: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.M2C_GetNavMeshFromPosition)]
	[ProtoContract]
	public partial class M2C_GetNavMeshFromPosition: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public List<Unity.Mathematics.float3> Vertices { get; set; }

		[ProtoMember(5)]
		public List<int> Indices { get; set; }

		[ProtoMember(6)]
		public List<long> PolygonRefs { get; set; }

	}

	[ResponseType(nameof(M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath))]
	[Message(OuterMessage.C2M_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath)]
	[ProtoContract]
	public partial class C2M_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

// Optional. If set the unit will be moved to the target position temporarily, otherwise a unit with UnitCfgId will
// be created temporarily at the target position when finding the path.
		[ProtoMember(2)]
		public long UnitId { get; set; }

		[ProtoMember(3)]
		public string UnitCfgId { get; set; }

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.NavPath)]
	[ProtoContract]
	public partial class NavPath: ProtoObject
	{
		[ProtoMember(1)]
		public Unity.Mathematics.float3 TargetPosition { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public long MonsterCallUnitId { get; set; }

		[ProtoMember(4)]
		public List<Unity.Mathematics.float3> Points { get; set; }

	}

	[Message(OuterMessage.M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath)]
	[ProtoContract]
	public partial class M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public List<NavPath> Path { get; set; }

	}

	[Message(OuterMessage.M2C_DrawAllMonsterCall2HeadQuarterPath)]
	[ProtoContract]
	public partial class M2C_DrawAllMonsterCall2HeadQuarterPath: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Message { get; set; }

		[ProtoMember(3)]
		public List<NavPath> Path { get; set; }

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

	[ResponseType(nameof(M2C_BattleRecoverCancelWatchAd))]
	[Message(OuterMessage.C2M_BattleRecoverCancelWatchAd)]
	[ProtoContract]
	public partial class C2M_BattleRecoverCancelWatchAd: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int IsFinished { get; set; }

	}

	[Message(OuterMessage.M2C_BattleRecoverCancelWatchAd)]
	[ProtoContract]
	public partial class M2C_BattleRecoverCancelWatchAd: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_BattleRecoverConfirmWatchAd))]
	[Message(OuterMessage.C2M_BattleRecoverConfirmWatchAd)]
	[ProtoContract]
	public partial class C2M_BattleRecoverConfirmWatchAd: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int IsFinished { get; set; }

	}

	[Message(OuterMessage.M2C_BattleRecoverConfirmWatchAd)]
	[ProtoContract]
	public partial class M2C_BattleRecoverConfirmWatchAd: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_BattleRecoverResult))]
	[Message(OuterMessage.C2M_BattleRecoverResult)]
	[ProtoContract]
	public partial class C2M_BattleRecoverResult: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int IsConfirm { get; set; }

	}

	[Message(OuterMessage.M2C_BattleRecoverResult)]
	[ProtoContract]
	public partial class M2C_BattleRecoverResult: ProtoObject, IActorLocationResponse
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

	[Message(OuterMessage.C2G_ReDealMyFunctionMenu)]
	[ProtoContract]
	public partial class C2G_ReDealMyFunctionMenu: ProtoObject, IMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

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

	[Message(OuterMessage.C2M_ForceNextWaveWhenDebug)]
	[ProtoContract]
	public partial class C2M_ForceNextWaveWhenDebug: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.C2M_ForceAddGameGoldWhenDebug)]
	[ProtoContract]
	public partial class C2M_ForceAddGameGoldWhenDebug: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.C2M_ForceAddHomeHpWhenDebug)]
	[ProtoContract]
	public partial class C2M_ForceAddHomeHpWhenDebug: ProtoObject, IActorLocationMessage
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

	[ResponseType(nameof(G2C_GetArcadeCoinQrCode))]
	[Message(OuterMessage.C2G_GetArcadeCoinQrCode)]
	[ProtoContract]
	public partial class C2G_GetArcadeCoinQrCode: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int ArcadeCoinNum { get; set; }

	}

	[Message(OuterMessage.G2C_GetArcadeCoinQrCode)]
	[ProtoContract]
	public partial class G2C_GetArcadeCoinQrCode: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public byte[] PayComponentBytes { get; set; }

	}

	[ResponseType(nameof(G2C_GetSeasonComponent))]
	[Message(OuterMessage.C2G_GetSeasonComponent)]
	[ProtoContract]
	public partial class C2G_GetSeasonComponent: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_GetSeasonComponent)]
	[ProtoContract]
	public partial class G2C_GetSeasonComponent: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public byte[] ComponentBytes { get; set; }

	}

	[Message(OuterMessage.C2G_InsertMailWhenDebug)]
	[ProtoContract]
	public partial class C2G_InsertMailWhenDebug: ProtoObject, IMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.C2G_AddDiamondWhenDebug)]
	[ProtoContract]
	public partial class C2G_AddDiamondWhenDebug: ProtoObject, IMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[ResponseType(nameof(G2C_DealMyMail))]
	[Message(OuterMessage.C2G_DealMyMail)]
	[ProtoContract]
	public partial class C2G_DealMyMail: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long MailId { get; set; }

		[ProtoMember(3)]
		public int DealMailType { get; set; }

	}

	[Message(OuterMessage.G2C_DealMyMail)]
	[ProtoContract]
	public partial class G2C_DealMyMail: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_ResetPowerup))]
	[Message(OuterMessage.C2G_ResetPowerup)]
	[ProtoContract]
	public partial class C2G_ResetPowerup: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_ResetPowerup)]
	[ProtoContract]
	public partial class G2C_ResetPowerup: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_UpdatePowerup))]
	[Message(OuterMessage.C2G_UpdatePowerup)]
	[ProtoContract]
	public partial class C2G_UpdatePowerup: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string PowerUpcfg { get; set; }

	}

	[Message(OuterMessage.G2C_UpdatePowerup)]
	[ProtoContract]
	public partial class G2C_UpdatePowerup: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_GetUIRedDotType))]
	[Message(OuterMessage.C2G_GetUIRedDotType)]
	[ProtoContract]
	public partial class C2G_GetUIRedDotType: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_GetUIRedDotType)]
	[ProtoContract]
	public partial class G2C_GetUIRedDotType: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_SetUIRedDotType))]
	[Message(OuterMessage.C2G_SetUIRedDotType)]
	[ProtoContract]
	public partial class C2G_SetUIRedDotType: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int UIRedDotType { get; set; }

		[ProtoMember(3)]
		public string ItemCfgId { get; set; }

	}

	[Message(OuterMessage.G2C_SetUIRedDotType)]
	[ProtoContract]
	public partial class G2C_SetUIRedDotType: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.C2G_ChkSeasonIndexChg)]
	[ProtoContract]
	public partial class C2G_ChkSeasonIndexChg: ProtoObject, IMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[ResponseType(nameof(G2C_SetQuestionnaireFinished))]
	[Message(OuterMessage.C2G_SetQuestionnaireFinished)]
	[ProtoContract]
	public partial class C2G_SetQuestionnaireFinished: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string QuestionnaireCfgId { get; set; }

	}

	[Message(OuterMessage.G2C_SetQuestionnaireFinished)]
	[ProtoContract]
	public partial class G2C_SetQuestionnaireFinished: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_SetBattleNoticeFinished))]
	[Message(OuterMessage.C2G_SetBattleNoticeFinished)]
	[ProtoContract]
	public partial class C2G_SetBattleNoticeFinished: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string BattleNoticeCfgId { get; set; }

	}

	[Message(OuterMessage.G2C_SetBattleNoticeFinished)]
	[ProtoContract]
	public partial class G2C_SetBattleNoticeFinished: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_ResetAllUnitPos))]
	[Message(OuterMessage.C2M_ResetAllUnitPos)]
	[ProtoContract]
	public partial class C2M_ResetAllUnitPos: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_ResetAllUnitPos)]
	[ProtoContract]
	public partial class M2C_ResetAllUnitPos: ProtoObject, IActorLocationResponse
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
		 public const ushort C2G_GetPlayerStatus = 10011;
		 public const ushort G2C_GetPlayerStatus = 10012;
		 public const ushort G2C_PlayerStatusChgNotice = 10013;
		 public const ushort G2C_LoginInAtOtherWhere = 10014;
		 public const ushort UnitInfo = 10015;
		 public const ushort UnitBaseInfo = 10016;
		 public const ushort M2C_CreateUnits = 10017;
		 public const ushort M2C_RemoveUnits = 10018;
		 public const ushort M2C_SyncDataList = 10019;
		 public const ushort M2C_CreateMyUnit = 10020;
		 public const ushort C2M_NeedReNoticeUnitIds = 10021;
		 public const ushort C2M_NeedReNoticeTowerDefense = 10022;
		 public const ushort M2C_StartSceneChange = 10023;
		 public const ushort C2M_PathfindingResult = 10024;
		 public const ushort C2M_Stop = 10025;
		 public const ushort M2C_PathfindingResult = 10026;
		 public const ushort M2C_Stop = 10027;
		 public const ushort C2G_Ping = 10028;
		 public const ushort G2C_Ping = 10029;
		 public const ushort G2C_Test = 10030;
		 public const ushort C2M_Reload = 10031;
		 public const ushort M2C_Reload = 10032;
		 public const ushort C2R_Login = 10033;
		 public const ushort R2C_Login = 10034;
		 public const ushort C2R_LoginWithAuth = 10035;
		 public const ushort R2C_LoginWithAuth = 10036;
		 public const ushort C2G_BindAccountWithAuth = 10037;
		 public const ushort G2C_BindAccountWithAuth = 10038;
		 public const ushort C2G_LoginGate = 10039;
		 public const ushort G2C_LoginGate = 10040;
		 public const ushort C2G_LoginOut = 10041;
		 public const ushort G2C_LoginOut = 10042;
		 public const ushort C2G_ReLoginGate = 10043;
		 public const ushort G2C_ReLoginGate = 10044;
		 public const ushort G2C_TestHotfixMessage = 10045;
		 public const ushort C2M_TestRobotCase = 10046;
		 public const ushort M2C_TestRobotCase = 10047;
		 public const ushort C2M_TestRobotCase2 = 10048;
		 public const ushort M2C_TestRobotCase2 = 10049;
		 public const ushort C2M_TransferMap = 10050;
		 public const ushort M2C_TransferMap = 10051;
		 public const ushort C2G_Benchmark = 10052;
		 public const ushort G2C_Benchmark = 10053;
		 public const ushort C2M_LearnSkill = 10054;
		 public const ushort M2C_LearnSkill = 10055;
		 public const ushort C2M_CastSkill = 10056;
		 public const ushort M2C_CastSkill = 10057;
		 public const ushort C2M_BuySkillEnergy = 10058;
		 public const ushort M2C_BuySkillEnergy = 10059;
		 public const ushort C2M_RestoreSkillEnergy = 10060;
		 public const ushort M2C_RestoreSkillEnergy = 10061;
		 public const ushort C2G_BuyItem = 10062;
		 public const ushort G2C_BuyItem = 10063;
		 public const ushort C2G_ReplaceBattleDeck = 10064;
		 public const ushort G2C_ReplaceBattleDeck = 10065;
		 public const ushort C2G_UpgradeItem = 10066;
		 public const ushort G2C_UpgradeItem = 10067;
		 public const ushort C2M_CallTower = 10068;
		 public const ushort M2C_CallTower = 10069;
		 public const ushort C2M_CallTowerActions = 10070;
		 public const ushort M2C_CallTowerActions = 10071;
		 public const ushort C2M_CallMonster = 10072;
		 public const ushort M2C_CallMonster = 10073;
		 public const ushort C2M_PKMovePlayer = 10074;
		 public const ushort M2C_PKMovePlayer = 10075;
		 public const ushort C2M_PKMoveTower = 10076;
		 public const ushort M2C_PKMoveTower = 10077;
		 public const ushort C2M_ClearMyTower = 10078;
		 public const ushort M2C_ClearMyTower = 10079;
		 public const ushort C2M_ClearAllMonster = 10080;
		 public const ushort M2C_ClearAllMonster = 10081;
		 public const ushort C2G_GetRoomList = 10082;
		 public const ushort G2C_GetRoomList = 10083;
		 public const ushort C2G_GetRoomInfo = 10084;
		 public const ushort G2C_GetRoomInfo = 10085;
		 public const ushort R2C_RoomInfoChgNotice = 10086;
		 public const ushort C2G_CreateRoom = 10087;
		 public const ushort G2C_CreateRoom = 10088;
		 public const ushort C2G_JoinRoom = 10089;
		 public const ushort G2C_JoinRoom = 10090;
		 public const ushort C2G_QuitRoom = 10091;
		 public const ushort G2C_QuitRoom = 10092;
		 public const ushort C2G_KickMemberOutRoom = 10093;
		 public const ushort G2C_KickMemberOutRoom = 10094;
		 public const ushort G2C_BeKickMemberOutRoom = 10095;
		 public const ushort C2G_ChgRoomMemberStatus = 10096;
		 public const ushort G2C_ChgRoomMemberStatus = 10097;
		 public const ushort C2G_ChgRoomMemberSeat = 10098;
		 public const ushort G2C_ChgRoomMemberSeat = 10099;
		 public const ushort C2G_ChgRoomMemberTeam = 10100;
		 public const ushort G2C_ChgRoomMemberTeam = 10101;
		 public const ushort C2G_SetARRoomInfo = 10102;
		 public const ushort G2C_SetARRoomInfo = 10103;
		 public const ushort C2G_GetARRoomInfo = 10104;
		 public const ushort G2C_GetARRoomInfo = 10105;
		 public const ushort C2G_ChgRoomBattleLevelCfg = 10106;
		 public const ushort G2C_ChgRoomBattleLevelCfg = 10107;
		 public const ushort C2G_ReturnBackBattle = 10108;
		 public const ushort G2C_ReturnBackBattle = 10109;
		 public const ushort C2M_MemberQuitBattle = 10110;
		 public const ushort M2C_MemberQuitBattle = 10111;
		 public const ushort C2M_MemberReturnRoomFromBattle = 10112;
		 public const ushort M2C_MemberReturnRoomFromBattle = 10113;
		 public const ushort M2C_GamePlayChgNotice = 10114;
		 public const ushort M2C_GamePlayCoinChgNotice = 10115;
		 public const ushort M2C_GamePlayStatisticalDataChgNotice = 10116;
		 public const ushort M2C_GamePlayModeChgNotice = 10117;
		 public const ushort C2M_PutHome = 10118;
		 public const ushort M2C_PutHome = 10119;
		 public const ushort C2M_ResetHome = 10120;
		 public const ushort M2C_ResetHome = 10121;
		 public const ushort C2M_PutMonsterCall = 10122;
		 public const ushort M2C_PutMonsterCall = 10123;
		 public const ushort C2M_BuyPlayerTower = 10124;
		 public const ushort M2C_BuyPlayerTower = 10125;
		 public const ushort C2M_RefreshBuyPlayerTower = 10126;
		 public const ushort M2C_RefreshBuyPlayerTower = 10127;
		 public const ushort C2M_CallOwnTower = 10128;
		 public const ushort M2C_CallOwnTower = 10129;
		 public const ushort C2M_UpgradeItemUnit = 10130;
		 public const ushort M2C_UpgradeItemUnit = 10131;
		 public const ushort C2M_UpgradePlayerTower = 10132;
		 public const ushort M2C_UpgradePlayerTower = 10133;
		 public const ushort C2M_ScalePlayerTower = 10134;
		 public const ushort M2C_ScalePlayerTower = 10135;
		 public const ushort C2M_ScalePlayerTowerCard = 10136;
		 public const ushort M2C_ScalePlayerTowerCard = 10137;
		 public const ushort C2M_ReclaimPlayerTower = 10138;
		 public const ushort M2C_ReclaimPlayerTower = 10139;
		 public const ushort C2M_MovePlayerTower = 10140;
		 public const ushort M2C_MovePlayerTower = 10141;
		 public const ushort C2M_ReadyWhenRestTime = 10142;
		 public const ushort M2C_ReadyWhenRestTime = 10143;
		 public const ushort C2M_ReScan = 10144;
		 public const ushort M2C_ReScan = 10145;
		 public const ushort C2M_GetMonsterCall2HeadQuarterPath = 10146;
		 public const ushort M2C_GetMonsterCall2HeadQuarterPath = 10147;
		 public const ushort C2M_GetNearestNavMeshPoint = 10148;
		 public const ushort M2C_GetNearestNavMeshPoint = 10149;
		 public const ushort C2M_GetNavMeshFromPosition = 10150;
		 public const ushort M2C_GetNavMeshFromPosition = 10151;
		 public const ushort C2M_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath = 10152;
		 public const ushort NavPath = 10153;
		 public const ushort M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath = 10154;
		 public const ushort M2C_DrawAllMonsterCall2HeadQuarterPath = 10155;
		 public const ushort C2M_ChkRay = 10156;
		 public const ushort M2C_ChkRay = 10157;
		 public const ushort C2M_SendARCameraPos = 10158;
		 public const ushort C2M_GetNumericUnit = 10159;
		 public const ushort C2G_GetRank = 10160;
		 public const ushort G2C_GetRank = 10161;
		 public const ushort C2G_GetRankedMoreThan = 10162;
		 public const ushort G2C_GetRankedMoreThan = 10163;
		 public const ushort C2G_GetPlayerCache = 10164;
		 public const ushort G2C_GetPlayerCache = 10165;
		 public const ushort G2C_PlayerCacheChgNotice = 10166;
		 public const ushort C2G_SetPlayerCache = 10167;
		 public const ushort G2C_SetPlayerCache = 10168;
		 public const ushort C2G_AddPhysicalStrenthByAd = 10169;
		 public const ushort G2C_AddPhysicalStrenthByAd = 10170;
		 public const ushort C2M_BattleRecoverCancelWatchAd = 10171;
		 public const ushort M2C_BattleRecoverCancelWatchAd = 10172;
		 public const ushort C2M_BattleRecoverConfirmWatchAd = 10173;
		 public const ushort M2C_BattleRecoverConfirmWatchAd = 10174;
		 public const ushort C2M_BattleRecoverResult = 10175;
		 public const ushort M2C_BattleRecoverResult = 10176;
		 public const ushort C2G_ChkGameJudgeChoose = 10177;
		 public const ushort G2C_ChkGameJudgeChoose = 10178;
		 public const ushort C2G_RecordGameJudgeChoose = 10179;
		 public const ushort G2C_RecordGameJudgeChoose = 10180;
		 public const ushort C2G_ReDealMyFunctionMenu = 10181;
		 public const ushort C2M_SetStopActorMoveWhenDebug = 10182;
		 public const ushort C2M_ForceGameEndWhenDebug = 10183;
		 public const ushort C2M_ForceNextWaveWhenDebug = 10184;
		 public const ushort C2M_ForceAddGameGoldWhenDebug = 10185;
		 public const ushort C2M_ForceAddHomeHpWhenDebug = 10186;
		 public const ushort C2G_SetMyRankScoreWhenDebug = 10187;
		 public const ushort C2G_ClearRankWhenDebug = 10188;
		 public const ushort C2G_ClearPlayerRankWhenDebug = 10189;
		 public const ushort C2G_ResetPlayerFunctionMenuStatusWhenDebug = 10190;
		 public const ushort C2G_GetArcadeCoinQrCode = 10191;
		 public const ushort G2C_GetArcadeCoinQrCode = 10192;
		 public const ushort C2G_GetSeasonComponent = 10193;
		 public const ushort G2C_GetSeasonComponent = 10194;
		 public const ushort C2G_InsertMailWhenDebug = 10195;
		 public const ushort C2G_AddDiamondWhenDebug = 10196;
		 public const ushort C2G_DealMyMail = 10197;
		 public const ushort G2C_DealMyMail = 10198;
		 public const ushort C2G_ResetPowerup = 10199;
		 public const ushort G2C_ResetPowerup = 10200;
		 public const ushort C2G_UpdatePowerup = 10201;
		 public const ushort G2C_UpdatePowerup = 10202;
		 public const ushort C2G_GetUIRedDotType = 10203;
		 public const ushort G2C_GetUIRedDotType = 10204;
		 public const ushort C2G_SetUIRedDotType = 10205;
		 public const ushort G2C_SetUIRedDotType = 10206;
		 public const ushort C2G_ChkSeasonIndexChg = 10207;
		 public const ushort C2G_SetQuestionnaireFinished = 10208;
		 public const ushort G2C_SetQuestionnaireFinished = 10209;
		 public const ushort C2G_SetBattleNoticeFinished = 10210;
		 public const ushort G2C_SetBattleNoticeFinished = 10211;
		 public const ushort C2M_ResetAllUnitPos = 10212;
		 public const ushort M2C_ResetAllUnitPos = 10213;
	}
}
