using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ChgRoomMemberSeatHandler : AMRpcHandler<C2G_ChgRoomMemberSeat, G2C_ChgRoomMemberSeat>
	{
		protected override async ETTask Run(Session session, C2G_ChgRoomMemberSeat request, G2C_ChgRoomMemberSeat response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			long playerId = player.Id;
			long roomId = playerStatusComponent.RoomId;
			int newSeat = request.NewSeat;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_ChgRoomMemberSeat _R2G_ChgRoomMemberSeat = (R2G_ChgRoomMemberSeat) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_ChgRoomMemberSeat()
			{
				PlayerId = playerId,
				RoomId = roomId,
				NewSeat = newSeat,
			});
			
			response.Error = _R2G_ChgRoomMemberSeat.Error;
			response.Message = _R2G_ChgRoomMemberSeat.Message;

			await ETTask.CompletedTask;
		}
	}
}