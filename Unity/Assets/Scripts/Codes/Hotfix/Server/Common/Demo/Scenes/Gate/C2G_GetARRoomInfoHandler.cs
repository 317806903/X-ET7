using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_GetARRoomInfoHandler : AMRpcHandler<C2G_GetARRoomInfo, G2C_GetARRoomInfo>
	{
		protected override async ETTask Run(Session session, C2G_GetARRoomInfo request, G2C_GetARRoomInfo response)
		{
			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_GetARRoomInfo _R2G_GetARRoomInfo = (R2G_GetARRoomInfo) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_GetARRoomInfo()
			{
				RoomId = request.RoomId,
				IsWithARMeshBytes = request.IsWithARMeshBytes,
			});

			response.Error = _R2G_GetARRoomInfo.Error;
			response.Message = _R2G_GetARRoomInfo.Message;

			response.ARMapScale = _R2G_GetARRoomInfo.ARMapScale;
			response.ARMeshType = _R2G_GetARRoomInfo.ARMeshType;
			response.ARSceneId = _R2G_GetARRoomInfo.ARSceneId;
			response.ARSceneMeshId = _R2G_GetARRoomInfo.ARSceneMeshId;
			response.ARMeshDownLoadUrl = _R2G_GetARRoomInfo.ARMeshDownLoadUrl;
			response.ARMeshBytes = _R2G_GetARRoomInfo.ARMeshBytes;

			await ETTask.CompletedTask;
		}
	}
}