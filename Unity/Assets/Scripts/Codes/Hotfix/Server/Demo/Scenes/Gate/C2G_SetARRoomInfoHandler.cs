using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_SetARRoomInfoHandler : AMRpcHandler<C2G_SetARRoomInfo, G2C_SetARRoomInfo>
	{
		protected override async ETTask Run(Session session, C2G_SetARRoomInfo request, G2C_SetARRoomInfo response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			long playerId = player.Id;
			long roomId = playerStatusComponent.RoomId;
			string ARSceneId = request.ARSceneId;
			string ARMeshDownLoadUrl = request.ARMeshDownLoadUrl;
			int ARMapScale = request.ARMapScale;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_SetARRoomInfo _R2G_SetARRoomInfo = (R2G_SetARRoomInfo) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_SetARRoomInfo()
			{
				PlayerId = playerId,
				RoomId = roomId,
				ARSceneId = ARSceneId,
				ARMeshDownLoadUrl = ARMeshDownLoadUrl,
				ARMapScale = ARMapScale,
			});

			response.Error = _R2G_SetARRoomInfo.Error;
			response.Message = _R2G_SetARRoomInfo.Message;

			await ETTask.CompletedTask;
		}
	}
}