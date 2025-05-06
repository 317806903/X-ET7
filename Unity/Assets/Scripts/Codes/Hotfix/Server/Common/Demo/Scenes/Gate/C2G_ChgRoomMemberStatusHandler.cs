using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ChgRoomMemberStatusHandler : AMRpcHandler<C2G_ChgRoomMemberStatus, G2C_ChgRoomMemberStatus>
	{
		protected override async ETTask Run(Session session, C2G_ChgRoomMemberStatus request, G2C_ChgRoomMemberStatus response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			long playerId = player.Id;
			long roomId = playerStatusComponent.RoomId;
			int isReady = request.IsReady;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_ChgRoomMemberStatus _R2G_ChgRoomMemberStatus = (R2G_ChgRoomMemberStatus) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_ChgRoomMemberStatus()
			{
				PlayerId = playerId,
				RoomId = roomId,
				IsReady = isReady,
			});
			
			response.Error = _R2G_ChgRoomMemberStatus.Error;
			response.Message = _R2G_ChgRoomMemberStatus.Message;
			response.IsReady = _R2G_ChgRoomMemberStatus.IsReady;

			await ETTask.CompletedTask;
		}
	}
}