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
		public string RoomStatus { get; set; }

		[ProtoMember(6)]
		public int IsARRoom { get; set; }

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

	[Message(InnerMessage.R2G_RoomInfoChgNotice)]
	[ProtoContract]
	public partial class R2G_RoomInfoChgNotice: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

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
		public int IsARRoom { get; set; }

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
		public int IsARRoom { get; set; }

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
		public long DynamicMapId { get; set; }

	}

	[ResponseType(nameof(M2R_DestroyDynamicMap))]
	[Message(InnerMessage.R2M_DestroyDynamicMap)]
	[ProtoContract]
	public partial class R2M_DestroyDynamicMap: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long DynamicMapId { get; set; }

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

	[ResponseType(nameof(G2R_StartBattle))]
	[Message(InnerMessage.R2G_StartBattle)]
	[ProtoContract]
	public partial class R2G_StartBattle: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long DynamicMapId { get; set; }

		[ProtoMember(3)]
		public int RoomSeatIndex { get; set; }

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
		 public const ushort ObjectGetRequest = 20017;
		 public const ushort ObjectGetResponse = 20018;
		 public const ushort R2G_GetLoginKey = 20019;
		 public const ushort G2R_GetLoginKey = 20020;
		 public const ushort G2M_SessionDisconnect = 20021;
		 public const ushort ObjectQueryResponse = 20022;
		 public const ushort M2M_UnitTransferRequest = 20023;
		 public const ushort M2M_UnitTransferResponse = 20024;
		 public const ushort G2R_GetRoomIdByPlayer = 20025;
		 public const ushort R2G_GetRoomIdByPlayer = 20026;
		 public const ushort G2R_GetRoomList = 20027;
		 public const ushort R2G_GetRoomList = 20028;
		 public const ushort G2R_GetRoomInfo = 20029;
		 public const ushort R2G_GetRoomInfo = 20030;
		 public const ushort R2G_RoomInfoChgNotice = 20031;
		 public const ushort G2R_CreateRoom = 20032;
		 public const ushort R2G_CreateRoom = 20033;
		 public const ushort G2R_JoinRoom = 20034;
		 public const ushort R2G_JoinRoom = 20035;
		 public const ushort G2R_QuitRoom = 20036;
		 public const ushort R2G_QuitRoom = 20037;
		 public const ushort G2R_KickMemberOutRoom = 20038;
		 public const ushort R2G_KickMemberOutRoom = 20039;
		 public const ushort G2R_ChgRoomStatus = 20040;
		 public const ushort R2G_ChgRoomStatus = 20041;
		 public const ushort G2R_ChgRoomMemberStatus = 20042;
		 public const ushort R2G_ChgRoomMemberStatus = 20043;
		 public const ushort G2R_ChgRoomMemberSeat = 20044;
		 public const ushort R2G_ChgRoomMemberSeat = 20045;
		 public const ushort R2M_CreateDynamicMap = 20046;
		 public const ushort M2R_CreateDynamicMap = 20047;
		 public const ushort R2M_DestroyDynamicMap = 20048;
		 public const ushort M2R_DestroyDynamicMap = 20049;
		 public const ushort R2G_StartBattle = 20050;
		 public const ushort G2R_StartBattle = 20051;
		 public const ushort R2G_BeKickedMember = 20052;
		 public const ushort G2R_BeKickedMember = 20053;
		 public const ushort M2G_QuitBattle = 20054;
		 public const ushort G2M_QuitBattle = 20055;
		 public const ushort M2G_MemberQuitBattle = 20056;
		 public const ushort G2M_MemberQuitBattle = 20057;
		 public const ushort M2G_MemberReturnRoomFromBattle = 20058;
		 public const ushort G2M_MemberReturnRoomFromBattle = 20059;
		 public const ushort G2R_ReturnBackBattle = 20060;
		 public const ushort R2G_ReturnBackBattle = 20061;
	}
}
