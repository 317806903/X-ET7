using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_QuitRoomHandler : AMRpcHandler<C2G_QuitRoom, G2C_QuitRoom>
	{
		protected override async ETTask Run(Session session, C2G_QuitRoom request, G2C_QuitRoom response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			long playerId = player.Id;
			long roomId = playerStatusComponent.RoomId;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_QuitRoom _R2G_QuitRoom = (R2G_QuitRoom) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_QuitRoom()
			{
				PlayerId = playerId,
				RoomId = roomId,
			});
			
			response.Error = _R2G_QuitRoom.Error;
			response.Message = _R2G_QuitRoom.Message;
			if (response.Error == ET.ErrorCode.ERR_Success)
			{
				playerStatusComponent.PlayerGameMode = PlayerGameMode.None;
				playerStatusComponent.PlayerStatus = PlayerStatus.Hall;
				playerStatusComponent.RoomId = 0;

				await playerStatusComponent.NoticeClient();
			}
			
			await ETTask.CompletedTask;
		}
	}
}