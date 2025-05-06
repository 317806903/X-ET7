using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_GetARRoomInfoHandler : AMActorRpcHandler<Scene, G2R_GetARRoomInfo, R2G_GetARRoomInfo>
	{
		protected override async ETTask Run(Scene scene, G2R_GetARRoomInfo request, R2G_GetARRoomInfo response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			long roomId = request.RoomId;
			bool isWithARMeshBytes = request.IsWithARMeshBytes == 1;

			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);

			(ARMeshType _ARMeshType, string _ARSceneId, string _ARSceneMeshId, string _ARMeshDownLoadUrl, byte[] _ARMeshBytes) = roomManagerComponent.GetARMeshInfo(roomId);

			response.ARMapScale = (int)(roomComponent.mapScale * 100);
			response.ARMeshType = (int)_ARMeshType;
			response.ARSceneId = _ARSceneId;
			response.ARSceneMeshId = _ARSceneMeshId;
			response.ARMeshDownLoadUrl = _ARMeshDownLoadUrl;
			if (isWithARMeshBytes)
			{
				response.ARMeshBytes = _ARMeshBytes;
			}
			else
			{
				response.ARMeshBytes = null;
			}

			await ETTask.CompletedTask;
		}
	}
}