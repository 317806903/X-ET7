using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
// using
	[ResponseType(nameof(ObjectQueryResponse))]
	[Message(InnerMessage.ObjectQueryRequest)]
	[ProtoContract]
	public partial class ObjectQueryRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long InstanceId { get; set; }

	}

	[ResponseType(nameof(A2M_Reload))]
	[Message(InnerMessage.M2A_Reload)]
	[ProtoContract]
	public partial class M2A_Reload: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.A2M_Reload)]
	[ProtoContract]
	public partial class A2M_Reload: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2G_LockResponse))]
	[Message(InnerMessage.G2G_LockRequest)]
	[ProtoContract]
	public partial class G2G_LockRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Id { get; set; }

		[ProtoMember(3)]
		public string Address { get; set; }

	}

	[Message(InnerMessage.G2G_LockResponse)]
	[ProtoContract]
	public partial class G2G_LockResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2G_LockReleaseResponse))]
	[Message(InnerMessage.G2G_LockReleaseRequest)]
	[ProtoContract]
	public partial class G2G_LockReleaseRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Id { get; set; }

		[ProtoMember(3)]
		public string Address { get; set; }

	}

	[Message(InnerMessage.G2G_LockReleaseResponse)]
	[ProtoContract]
	public partial class G2G_LockReleaseResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(ObjectAddResponse))]
	[Message(InnerMessage.ObjectAddRequest)]
	[ProtoContract]
	public partial class ObjectAddRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Type { get; set; }

		[ProtoMember(3)]
		public long Key { get; set; }

		[ProtoMember(4)]
		public long InstanceId { get; set; }

	}

	[Message(InnerMessage.ObjectAddResponse)]
	[ProtoContract]
	public partial class ObjectAddResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(ObjectLockResponse))]
	[Message(InnerMessage.ObjectLockRequest)]
	[ProtoContract]
	public partial class ObjectLockRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Type { get; set; }

		[ProtoMember(3)]
		public long Key { get; set; }

		[ProtoMember(4)]
		public long InstanceId { get; set; }

		[ProtoMember(5)]
		public int Time { get; set; }

	}

	[Message(InnerMessage.ObjectLockResponse)]
	[ProtoContract]
	public partial class ObjectLockResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(ObjectUnLockResponse))]
	[Message(InnerMessage.ObjectUnLockRequest)]
	[ProtoContract]
	public partial class ObjectUnLockRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Type { get; set; }

		[ProtoMember(3)]
		public long Key { get; set; }

		[ProtoMember(4)]
		public long OldInstanceId { get; set; }

		[ProtoMember(5)]
		public long InstanceId { get; set; }

	}

	[Message(InnerMessage.ObjectUnLockResponse)]
	[ProtoContract]
	public partial class ObjectUnLockResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(ObjectRemoveResponse))]
	[Message(InnerMessage.ObjectRemoveRequest)]
	[ProtoContract]
	public partial class ObjectRemoveRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Type { get; set; }

		[ProtoMember(3)]
		public long Key { get; set; }

	}

	[Message(InnerMessage.ObjectRemoveResponse)]
	[ProtoContract]
	public partial class ObjectRemoveResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(L2A_RemoveObjectLocationResponse))]
	[Message(InnerMessage.L2A_RemoveObjectLocationRequest)]
	[ProtoContract]
	public partial class L2A_RemoveObjectLocationRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Type { get; set; }

		[ProtoMember(3)]
		public long Key { get; set; }

	}

	[Message(InnerMessage.L2A_RemoveObjectLocationResponse)]
	[ProtoContract]
	public partial class L2A_RemoveObjectLocationResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(ObjectGetResponse))]
	[Message(InnerMessage.ObjectGetRequest)]
	[ProtoContract]
	public partial class ObjectGetRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Type { get; set; }

		[ProtoMember(3)]
		public long Key { get; set; }

		[ProtoMember(4)]
		public long SceneInstanceId { get; set; }

	}

	[Message(InnerMessage.ObjectGetResponse)]
	[ProtoContract]
	public partial class ObjectGetResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int Type { get; set; }

		[ProtoMember(5)]
		public long InstanceId { get; set; }

	}

	[ResponseType(nameof(G2R_GetLoginKey))]
	[Message(InnerMessage.R2G_GetLoginKey)]
	[ProtoContract]
	public partial class R2G_GetLoginKey: ProtoObject, IActorRequest
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
		public string LoginIP { get; set; }

	}

	[Message(InnerMessage.G2R_GetLoginKey)]
	[ProtoContract]
	public partial class G2R_GetLoginKey: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long Key { get; set; }

		[ProtoMember(5)]
		public long GateId { get; set; }

		[ProtoMember(6)]
		public int IsFirstLogin { get; set; }

	}

	[ResponseType(nameof(G2R_GetGatePlayerCount))]
	[Message(InnerMessage.R2G_GetGatePlayerCount)]
	[ProtoContract]
	public partial class R2G_GetGatePlayerCount: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.G2R_GetGatePlayerCount)]
	[ProtoContract]
	public partial class G2R_GetGatePlayerCount: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int PlayerCount { get; set; }

	}

	[ResponseType(nameof(A2G_LoginByAccount))]
	[Message(InnerMessage.G2A_LoginByAccount)]
	[ProtoContract]
	public partial class G2A_LoginByAccount: ProtoObject, IActorRequest
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
		public string LoginIP { get; set; }

	}

	[Message(InnerMessage.A2G_LoginByAccount)]
	[ProtoContract]
	public partial class A2G_LoginByAccount: ProtoObject, IActorResponse
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
		public int IsFirstLogin { get; set; }

	}

	[ResponseType(nameof(A2G_BindAccountWithAuth))]
	[Message(InnerMessage.G2A_BindAccountWithAuth)]
	[ProtoContract]
	public partial class G2A_BindAccountWithAuth: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string BindAccount { get; set; }

		[ProtoMember(4)]
		public int LoginType { get; set; }

	}

	[Message(InnerMessage.A2G_BindAccountWithAuth)]
	[ProtoContract]
	public partial class A2G_BindAccountWithAuth: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(5)]
		public int IsBindSuccess { get; set; }

	}

	[ResponseType(nameof(A2G_GetAccountInfo))]
	[Message(InnerMessage.G2A_GetAccountInfo)]
	[ProtoContract]
	public partial class G2A_GetAccountInfo: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string AccountId { get; set; }

	}

	[Message(InnerMessage.A2G_GetAccountInfo)]
	[ProtoContract]
	public partial class A2G_GetAccountInfo: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public byte[] AccountComponentBytes { get; set; }

	}

	[Message(InnerMessage.G2M_SessionDisconnect)]
	[ProtoContract]
	public partial class G2M_SessionDisconnect: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.ObjectQueryResponse)]
	[ProtoContract]
	public partial class ObjectQueryResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public byte[] Entity { get; set; }

	}

	[ResponseType(nameof(M2M_UnitTransferResponse))]
	[Message(InnerMessage.M2M_UnitTransferRequest)]
	[ProtoContract]
	public partial class M2M_UnitTransferRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long OldInstanceId { get; set; }

		[ProtoMember(3)]
		public byte[] Unit { get; set; }

		[ProtoMember(4)]
		public List<byte[]> Entitys { get; set; }

	}

	[Message(InnerMessage.M2M_UnitTransferResponse)]
	[ProtoContract]
	public partial class M2M_UnitTransferResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2G_GetRoomIdByPlayer))]
	[Message(InnerMessage.G2R_GetRoomIdByPlayer)]
	[ProtoContract]
	public partial class G2R_GetRoomIdByPlayer: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

	}

	[Message(InnerMessage.R2G_GetRoomIdByPlayer)]
	[ProtoContract]
	public partial class R2G_GetRoomIdByPlayer: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long RoomId { get; set; }

		[ProtoMember(5)]
		public int RoomStatus { get; set; }

		[ProtoMember(6)]
		public int RoomType { get; set; }

		[ProtoMember(7)]
		public int SubRoomType { get; set; }

		[ProtoMember(8)]
		public string BattleCfgId { get; set; }

	}

	[ResponseType(nameof(R2G_GetRoomList))]
	[Message(InnerMessage.G2R_GetRoomList)]
	[ProtoContract]
	public partial class G2R_GetRoomList: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int IsARRoom { get; set; }

	}

	[Message(InnerMessage.R2G_GetRoomList)]
	[ProtoContract]
	public partial class R2G_GetRoomList: ProtoObject, IActorResponse
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

	[ResponseType(nameof(R2G_GetRoomInfo))]
	[Message(InnerMessage.G2R_GetRoomInfo)]
	[ProtoContract]
	public partial class G2R_GetRoomInfo: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long RoomId { get; set; }

	}

	[Message(InnerMessage.R2G_GetRoomInfo)]
	[ProtoContract]
	public partial class R2G_GetRoomInfo: ProtoObject, IActorResponse
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

	[ResponseType(nameof(R2G_CreateRoom))]
	[Message(InnerMessage.G2R_CreateRoom)]
	[ProtoContract]
	public partial class G2R_CreateRoom: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public string BattleCfgId { get; set; }

		[ProtoMember(4)]
		public int RoomType { get; set; }

		[ProtoMember(5)]
		public int SubRoomType { get; set; }

	}

	[Message(InnerMessage.R2G_CreateRoom)]
	[ProtoContract]
	public partial class R2G_CreateRoom: ProtoObject, IActorResponse
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

	[ResponseType(nameof(R2G_JoinRoom))]
	[Message(InnerMessage.G2R_JoinRoom)]
	[ProtoContract]
	public partial class G2R_JoinRoom: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public long RoomId { get; set; }

	}

	[Message(InnerMessage.R2G_JoinRoom)]
	[ProtoContract]
	public partial class R2G_JoinRoom: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int RoomType { get; set; }

		[ProtoMember(5)]
		public int SubRoomType { get; set; }

	}

	[ResponseType(nameof(R2G_QuitRoom))]
	[Message(InnerMessage.G2R_QuitRoom)]
	[ProtoContract]
	public partial class G2R_QuitRoom: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public long RoomId { get; set; }

	}

	[Message(InnerMessage.R2G_QuitRoom)]
	[ProtoContract]
	public partial class R2G_QuitRoom: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2G_KickMemberOutRoom))]
	[Message(InnerMessage.G2R_KickMemberOutRoom)]
	[ProtoContract]
	public partial class G2R_KickMemberOutRoom: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public long BeKickPlayerId { get; set; }

		[ProtoMember(4)]
		public long RoomId { get; set; }

	}

	[Message(InnerMessage.R2G_KickMemberOutRoom)]
	[ProtoContract]
	public partial class R2G_KickMemberOutRoom: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2G_ChgRoomStatus))]
	[Message(InnerMessage.G2R_ChgRoomStatus)]
	[ProtoContract]
	public partial class G2R_ChgRoomStatus: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(3)]
		public long RoomId { get; set; }

		[ProtoMember(4)]
		public int RoomStatus { get; set; }

	}

	[Message(InnerMessage.R2G_ChgRoomStatus)]
	[ProtoContract]
	public partial class R2G_ChgRoomStatus: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2G_ChgRoomMemberStatus))]
	[Message(InnerMessage.G2R_ChgRoomMemberStatus)]
	[ProtoContract]
	public partial class G2R_ChgRoomMemberStatus: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public long RoomId { get; set; }

		[ProtoMember(4)]
		public int IsReady { get; set; }

	}

	[Message(InnerMessage.R2G_ChgRoomMemberStatus)]
	[ProtoContract]
	public partial class R2G_ChgRoomMemberStatus: ProtoObject, IActorResponse
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

	[ResponseType(nameof(R2G_ChgRoomMemberSeat))]
	[Message(InnerMessage.G2R_ChgRoomMemberSeat)]
	[ProtoContract]
	public partial class G2R_ChgRoomMemberSeat: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public long RoomId { get; set; }

		[ProtoMember(4)]
		public int NewSeat { get; set; }

	}

	[Message(InnerMessage.R2G_ChgRoomMemberSeat)]
	[ProtoContract]
	public partial class R2G_ChgRoomMemberSeat: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2G_ChgRoomMemberTeam))]
	[Message(InnerMessage.G2R_ChgRoomMemberTeam)]
	[ProtoContract]
	public partial class G2R_ChgRoomMemberTeam: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public long RoomId { get; set; }

		[ProtoMember(4)]
		public int NewTeam { get; set; }

	}

	[Message(InnerMessage.R2G_ChgRoomMemberTeam)]
	[ProtoContract]
	public partial class R2G_ChgRoomMemberTeam: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2G_SetARRoomInfo))]
	[Message(InnerMessage.G2R_SetARRoomInfo)]
	[ProtoContract]
	public partial class G2R_SetARRoomInfo: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public long RoomId { get; set; }

		[ProtoMember(4)]
		public int ARMapScale { get; set; }

		[ProtoMember(5)]
		public int ARMeshType { get; set; }

		[ProtoMember(6)]
		public string ARSceneId { get; set; }

		[ProtoMember(7)]
		public string ARMeshDownLoadUrl { get; set; }

		[ProtoMember(8)]
		public byte[] ARMeshBytes { get; set; }

	}

	[Message(InnerMessage.R2G_SetARRoomInfo)]
	[ProtoContract]
	public partial class R2G_SetARRoomInfo: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2G_ChgRoomBattleLevelCfg))]
	[Message(InnerMessage.G2R_ChgRoomBattleLevelCfg)]
	[ProtoContract]
	public partial class G2R_ChgRoomBattleLevelCfg: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public long RoomId { get; set; }

		[ProtoMember(4)]
		public string NewBattleCfgId { get; set; }

	}

	[Message(InnerMessage.R2G_ChgRoomBattleLevelCfg)]
	[ProtoContract]
	public partial class R2G_ChgRoomBattleLevelCfg: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2R_GetDynamicMapCount))]
	[Message(InnerMessage.R2M_GetDynamicMapCount)]
	[ProtoContract]
	public partial class R2M_GetDynamicMapCount: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.M2R_GetDynamicMapCount)]
	[ProtoContract]
	public partial class M2R_GetDynamicMapCount: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int DynamicMapCount { get; set; }

	}

	[ResponseType(nameof(M2R_CreateDynamicMap))]
	[Message(InnerMessage.R2M_CreateDynamicMap)]
	[ProtoContract]
	public partial class R2M_CreateDynamicMap: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public byte[] RoomInfo { get; set; }

		[ProtoMember(3)]
		public List<byte[]> RoomMemberInfos { get; set; }

		[ProtoMember(4)]
		public int ARMeshType { get; set; }

		[ProtoMember(5)]
		public string ARMeshDownLoadUrl { get; set; }

		[ProtoMember(6)]
		public byte[] ARMeshBytes { get; set; }

	}

	[Message(InnerMessage.M2R_CreateDynamicMap)]
	[ProtoContract]
	public partial class M2R_CreateDynamicMap: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long DynamicMapInstanceId { get; set; }

	}

	[ResponseType(nameof(M2R_DestroyDynamicMap))]
	[Message(InnerMessage.R2M_DestroyDynamicMap)]
	[ProtoContract]
	public partial class R2M_DestroyDynamicMap: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long DynamicMapInstanceId { get; set; }

	}

	[Message(InnerMessage.M2R_DestroyDynamicMap)]
	[ProtoContract]
	public partial class M2R_DestroyDynamicMap: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2M_EnterMapResponse))]
	[Message(InnerMessage.M2M_EnterMapRequest)]
	[ProtoContract]
	public partial class M2M_EnterMapRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public int PlayerLevel { get; set; }

		[ProtoMember(4)]
		public string GamePlayBattleLevelCfgId { get; set; }

	}

	[Message(InnerMessage.M2M_EnterMapResponse)]
	[ProtoContract]
	public partial class M2M_EnterMapResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2R_StartBattle))]
	[Message(InnerMessage.R2G_StartBattle)]
	[ProtoContract]
	public partial class R2G_StartBattle: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string GamePlayBattleLevelCfgId { get; set; }

		[ProtoMember(3)]
		public long DynamicMapInstanceId { get; set; }

	}

	[Message(InnerMessage.G2R_StartBattle)]
	[ProtoContract]
	public partial class G2R_StartBattle: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2R_BeKickedMember))]
	[Message(InnerMessage.R2G_BeKickedMember)]
	[ProtoContract]
	public partial class R2G_BeKickedMember: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.G2R_BeKickedMember)]
	[ProtoContract]
	public partial class G2R_BeKickedMember: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2M_QuitBattle))]
	[Message(InnerMessage.M2G_QuitBattle)]
	[ProtoContract]
	public partial class M2G_QuitBattle: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.G2M_QuitBattle)]
	[ProtoContract]
	public partial class G2M_QuitBattle: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2M_MemberQuitBattle))]
	[Message(InnerMessage.M2G_MemberQuitBattle)]
	[ProtoContract]
	public partial class M2G_MemberQuitBattle: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.G2M_MemberQuitBattle)]
	[ProtoContract]
	public partial class G2M_MemberQuitBattle: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2M_MemberReturnRoomFromBattle))]
	[Message(InnerMessage.M2G_MemberReturnRoomFromBattle)]
	[ProtoContract]
	public partial class M2G_MemberReturnRoomFromBattle: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int BattleResult { get; set; }

	}

	[Message(InnerMessage.G2M_MemberReturnRoomFromBattle)]
	[ProtoContract]
	public partial class G2M_MemberReturnRoomFromBattle: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2G_ReturnBackBattle))]
	[Message(InnerMessage.G2R_ReturnBackBattle)]
	[ProtoContract]
	public partial class G2R_ReturnBackBattle: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

	}

	[Message(InnerMessage.R2G_ReturnBackBattle)]
	[ProtoContract]
	public partial class R2G_ReturnBackBattle: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2R_ChkIsBattleEnd))]
	[Message(InnerMessage.R2M_ChkIsBattleEnd)]
	[ProtoContract]
	public partial class R2M_ChkIsBattleEnd: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.M2R_ChkIsBattleEnd)]
	[ProtoContract]
	public partial class M2R_ChkIsBattleEnd: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int IsBattleEnd { get; set; }

	}

	[ResponseType(nameof(M2R_MemberQuitBattle))]
	[Message(InnerMessage.R2M_MemberQuitBattle)]
	[ProtoContract]
	public partial class R2M_MemberQuitBattle: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

	}

	[Message(InnerMessage.M2R_MemberQuitBattle)]
	[ProtoContract]
	public partial class M2R_MemberQuitBattle: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2M_MemberQuitRoom))]
	[Message(InnerMessage.M2R_MemberQuitRoom)]
	[ProtoContract]
	public partial class M2R_MemberQuitRoom: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public long RoomId { get; set; }

	}

	[Message(InnerMessage.R2M_MemberQuitRoom)]
	[ProtoContract]
	public partial class R2M_MemberQuitRoom: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2M_NoticeRoomBattleEnd))]
	[Message(InnerMessage.M2R_NoticeRoomBattleEnd)]
	[ProtoContract]
	public partial class M2R_NoticeRoomBattleEnd: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long RoomId { get; set; }

		[ProtoMember(3)]
		public long PlayerId { get; set; }

		[ProtoMember(4)]
		public int IsReady { get; set; }

		[ProtoMember(5)]
		public List<long> WinPlayers { get; set; }

	}

	[Message(InnerMessage.R2M_NoticeRoomBattleEnd)]
	[ProtoContract]
	public partial class R2M_NoticeRoomBattleEnd: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2G_GetRank))]
	[Message(InnerMessage.G2R_GetRank)]
	[ProtoContract]
	public partial class G2R_GetRank: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public int RankType { get; set; }

	}

	[Message(InnerMessage.R2G_GetRank)]
	[ProtoContract]
	public partial class R2G_GetRank: ProtoObject, IActorResponse
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

	[ResponseType(nameof(R2G_GetRankedMoreThan))]
	[Message(InnerMessage.G2R_GetRankedMoreThan)]
	[ProtoContract]
	public partial class G2R_GetRankedMoreThan: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public int RankType { get; set; }

		[ProtoMember(4)]
		public long Score { get; set; }

	}

	[Message(InnerMessage.R2G_GetRankedMoreThan)]
	[ProtoContract]
	public partial class R2G_GetRankedMoreThan: ProtoObject, IActorResponse
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

	[ResponseType(nameof(R2G_SetPlayerRank))]
	[Message(InnerMessage.G2R_SetPlayerRank)]
	[ProtoContract]
	public partial class G2R_SetPlayerRank: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public int RankType { get; set; }

		[ProtoMember(4)]
		public long Score { get; set; }

		[ProtoMember(5)]
		public int KillNum { get; set; }

	}

	[Message(InnerMessage.R2G_SetPlayerRank)]
	[ProtoContract]
	public partial class R2G_SetPlayerRank: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2G_ClearRankWhenDebug))]
	[Message(InnerMessage.G2R_ClearRankWhenDebug)]
	[ProtoContract]
	public partial class G2R_ClearRankWhenDebug: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int RankType { get; set; }

	}

	[Message(InnerMessage.R2G_ClearRankWhenDebug)]
	[ProtoContract]
	public partial class R2G_ClearRankWhenDebug: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(P2G_GetPlayerCache))]
	[Message(InnerMessage.G2P_GetPlayerCache)]
	[ProtoContract]
	public partial class G2P_GetPlayerCache: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public int PlayerModelType { get; set; }

	}

	[Message(InnerMessage.P2G_GetPlayerCache)]
	[ProtoContract]
	public partial class P2G_GetPlayerCache: ProtoObject, IActorResponse
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

	[ResponseType(nameof(P2G_SetPlayerCache))]
	[Message(InnerMessage.G2P_SetPlayerCache)]
	[ProtoContract]
	public partial class G2P_SetPlayerCache: ProtoObject, IActorRequest
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

		[ProtoMember(6)]
		public int PlayerModelChgType { get; set; }

	}

	[Message(InnerMessage.P2G_SetPlayerCache)]
	[ProtoContract]
	public partial class P2G_SetPlayerCache: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(OG2G_LoginInAtOtherWhere))]
	[Message(InnerMessage.G2OG_LoginInAtOtherWhere)]
	[ProtoContract]
	public partial class G2OG_LoginInAtOtherWhere: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.OG2G_LoginInAtOtherWhere)]
	[ProtoContract]
	public partial class OG2G_LoginInAtOtherWhere: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2O_PlayerCacheChgNoticeClient))]
	[Message(InnerMessage.O2G_PlayerCacheChgNoticeClient)]
	[ProtoContract]
	public partial class O2G_PlayerCacheChgNoticeClient: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int PlayerModelType { get; set; }

	}

	[Message(InnerMessage.G2O_PlayerCacheChgNoticeClient)]
	[ProtoContract]
	public partial class G2O_PlayerCacheChgNoticeClient: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(P2G_GetArcadeCoinQrCode))]
	[Message(InnerMessage.G2P_GetArcadeCoinQrCode)]
	[ProtoContract]
	public partial class G2P_GetArcadeCoinQrCode: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long PlayerId { get; set; }

		[ProtoMember(3)]
		public int ArcadeCoinNum { get; set; }

	}

	[Message(InnerMessage.P2G_GetArcadeCoinQrCode)]
	[ProtoContract]
	public partial class P2G_GetArcadeCoinQrCode: ProtoObject, IActorResponse
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

	public static class InnerMessage
	{
		 public const ushort ObjectQueryRequest = 20002;
		 public const ushort M2A_Reload = 20003;
		 public const ushort A2M_Reload = 20004;
		 public const ushort G2G_LockRequest = 20005;
		 public const ushort G2G_LockResponse = 20006;
		 public const ushort G2G_LockReleaseRequest = 20007;
		 public const ushort G2G_LockReleaseResponse = 20008;
		 public const ushort ObjectAddRequest = 20009;
		 public const ushort ObjectAddResponse = 20010;
		 public const ushort ObjectLockRequest = 20011;
		 public const ushort ObjectLockResponse = 20012;
		 public const ushort ObjectUnLockRequest = 20013;
		 public const ushort ObjectUnLockResponse = 20014;
		 public const ushort ObjectRemoveRequest = 20015;
		 public const ushort ObjectRemoveResponse = 20016;
		 public const ushort L2A_RemoveObjectLocationRequest = 20017;
		 public const ushort L2A_RemoveObjectLocationResponse = 20018;
		 public const ushort ObjectGetRequest = 20019;
		 public const ushort ObjectGetResponse = 20020;
		 public const ushort R2G_GetLoginKey = 20021;
		 public const ushort G2R_GetLoginKey = 20022;
		 public const ushort R2G_GetGatePlayerCount = 20023;
		 public const ushort G2R_GetGatePlayerCount = 20024;
		 public const ushort G2A_LoginByAccount = 20025;
		 public const ushort A2G_LoginByAccount = 20026;
		 public const ushort G2A_BindAccountWithAuth = 20027;
		 public const ushort A2G_BindAccountWithAuth = 20028;
		 public const ushort G2A_GetAccountInfo = 20029;
		 public const ushort A2G_GetAccountInfo = 20030;
		 public const ushort G2M_SessionDisconnect = 20031;
		 public const ushort ObjectQueryResponse = 20032;
		 public const ushort M2M_UnitTransferRequest = 20033;
		 public const ushort M2M_UnitTransferResponse = 20034;
		 public const ushort G2R_GetRoomIdByPlayer = 20035;
		 public const ushort R2G_GetRoomIdByPlayer = 20036;
		 public const ushort G2R_GetRoomList = 20037;
		 public const ushort R2G_GetRoomList = 20038;
		 public const ushort G2R_GetRoomInfo = 20039;
		 public const ushort R2G_GetRoomInfo = 20040;
		 public const ushort G2R_CreateRoom = 20041;
		 public const ushort R2G_CreateRoom = 20042;
		 public const ushort G2R_JoinRoom = 20043;
		 public const ushort R2G_JoinRoom = 20044;
		 public const ushort G2R_QuitRoom = 20045;
		 public const ushort R2G_QuitRoom = 20046;
		 public const ushort G2R_KickMemberOutRoom = 20047;
		 public const ushort R2G_KickMemberOutRoom = 20048;
		 public const ushort G2R_ChgRoomStatus = 20049;
		 public const ushort R2G_ChgRoomStatus = 20050;
		 public const ushort G2R_ChgRoomMemberStatus = 20051;
		 public const ushort R2G_ChgRoomMemberStatus = 20052;
		 public const ushort G2R_ChgRoomMemberSeat = 20053;
		 public const ushort R2G_ChgRoomMemberSeat = 20054;
		 public const ushort G2R_ChgRoomMemberTeam = 20055;
		 public const ushort R2G_ChgRoomMemberTeam = 20056;
		 public const ushort G2R_SetARRoomInfo = 20057;
		 public const ushort R2G_SetARRoomInfo = 20058;
		 public const ushort G2R_ChgRoomBattleLevelCfg = 20059;
		 public const ushort R2G_ChgRoomBattleLevelCfg = 20060;
		 public const ushort R2M_GetDynamicMapCount = 20061;
		 public const ushort M2R_GetDynamicMapCount = 20062;
		 public const ushort R2M_CreateDynamicMap = 20063;
		 public const ushort M2R_CreateDynamicMap = 20064;
		 public const ushort R2M_DestroyDynamicMap = 20065;
		 public const ushort M2R_DestroyDynamicMap = 20066;
		 public const ushort M2M_EnterMapRequest = 20067;
		 public const ushort M2M_EnterMapResponse = 20068;
		 public const ushort R2G_StartBattle = 20069;
		 public const ushort G2R_StartBattle = 20070;
		 public const ushort R2G_BeKickedMember = 20071;
		 public const ushort G2R_BeKickedMember = 20072;
		 public const ushort M2G_QuitBattle = 20073;
		 public const ushort G2M_QuitBattle = 20074;
		 public const ushort M2G_MemberQuitBattle = 20075;
		 public const ushort G2M_MemberQuitBattle = 20076;
		 public const ushort M2G_MemberReturnRoomFromBattle = 20077;
		 public const ushort G2M_MemberReturnRoomFromBattle = 20078;
		 public const ushort G2R_ReturnBackBattle = 20079;
		 public const ushort R2G_ReturnBackBattle = 20080;
		 public const ushort R2M_ChkIsBattleEnd = 20081;
		 public const ushort M2R_ChkIsBattleEnd = 20082;
		 public const ushort R2M_MemberQuitBattle = 20083;
		 public const ushort M2R_MemberQuitBattle = 20084;
		 public const ushort M2R_MemberQuitRoom = 20085;
		 public const ushort R2M_MemberQuitRoom = 20086;
		 public const ushort M2R_NoticeRoomBattleEnd = 20087;
		 public const ushort R2M_NoticeRoomBattleEnd = 20088;
		 public const ushort G2R_GetRank = 20089;
		 public const ushort R2G_GetRank = 20090;
		 public const ushort G2R_GetRankedMoreThan = 20091;
		 public const ushort R2G_GetRankedMoreThan = 20092;
		 public const ushort G2R_SetPlayerRank = 20093;
		 public const ushort R2G_SetPlayerRank = 20094;
		 public const ushort G2R_ClearRankWhenDebug = 20095;
		 public const ushort R2G_ClearRankWhenDebug = 20096;
		 public const ushort G2P_GetPlayerCache = 20097;
		 public const ushort P2G_GetPlayerCache = 20098;
		 public const ushort G2P_SetPlayerCache = 20099;
		 public const ushort P2G_SetPlayerCache = 20100;
		 public const ushort G2OG_LoginInAtOtherWhere = 20101;
		 public const ushort OG2G_LoginInAtOtherWhere = 20102;
		 public const ushort O2G_PlayerCacheChgNoticeClient = 20103;
		 public const ushort G2O_PlayerCacheChgNoticeClient = 20104;
		 public const ushort G2P_GetArcadeCoinQrCode = 20105;
		 public const ushort P2G_GetArcadeCoinQrCode = 20106;
	}
}
