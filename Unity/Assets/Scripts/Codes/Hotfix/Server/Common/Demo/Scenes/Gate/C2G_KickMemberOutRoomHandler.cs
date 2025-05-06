using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_KickMemberOutRoomHandler : AMRpcHandler<C2G_KickMemberOutRoom, G2C_KickMemberOutRoom>
	{
		protected override async ETTask Run(Session session, C2G_KickMemberOutRoom request, G2C_KickMemberOutRoom response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			long playerId = player.Id;
			long roomId = playerStatusComponent.RoomId;
			long beKickPlayerId = request.BeKickPlayerId;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_KickMemberOutRoom _R2G_KickMemberOutRoom = (R2G_KickMemberOutRoom) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_KickMemberOutRoom()
			{
				PlayerId = playerId,
				BeKickPlayerId = beKickPlayerId,
				RoomId = roomId,
			});
			
			response.Error = _R2G_KickMemberOutRoom.Error;
			response.Message = _R2G_KickMemberOutRoom.Message;

			await ETTask.CompletedTask;
		}
	}
}