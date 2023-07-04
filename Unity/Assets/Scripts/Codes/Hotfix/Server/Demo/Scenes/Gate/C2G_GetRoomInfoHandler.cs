using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_GetRoomInfoHandler : AMRpcHandler<C2G_GetRoomInfo, G2C_GetRoomInfo>
	{
		protected override async ETTask Run(Session session, C2G_GetRoomInfo request, G2C_GetRoomInfo response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			long playerId = player.Id;
			long roomId = playerStatusComponent.RoomId;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_GetRoomInfo _R2G_GetRoomInfo = (R2G_GetRoomInfo) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_GetRoomInfo()
			{
				RoomId = roomId,
			});
			
			response.Error = _R2G_GetRoomInfo.Error;
			response.Message = _R2G_GetRoomInfo.Message;
			response.RoomInfo = _R2G_GetRoomInfo.RoomInfo;
			response.RoomMemberInfos = _R2G_GetRoomInfo.RoomMemberInfos;

			await ETTask.CompletedTask;
		}
	}
}