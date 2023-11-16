using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_JoinRoomHandler : AMRpcHandler<C2G_JoinRoom, G2C_JoinRoom>
	{
		protected override async ETTask Run(Session session, C2G_JoinRoom request, G2C_JoinRoom response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			long roomId = request.RoomId;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_JoinRoom _R2G_JoinRoom = (R2G_JoinRoom) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_JoinRoom()
			{
				PlayerId = playerId,
				RoomId = roomId,
			});

			response.Error = _R2G_JoinRoom.Error;
			response.Message = _R2G_JoinRoom.Message;

			if (response.Error == ET.ErrorCode.ERR_Success)
			{
				PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
				playerStatusComponent.RoomType = (RoomType)_R2G_JoinRoom.RoomType;
				playerStatusComponent.SubRoomType = (SubRoomType)_R2G_JoinRoom.SubRoomType;
				playerStatusComponent.PlayerStatus = PlayerStatus.Room;
				playerStatusComponent.RoomId = roomId;

				await playerStatusComponent.NoticeClient();
			}

			await ETTask.CompletedTask;
		}
	}
}