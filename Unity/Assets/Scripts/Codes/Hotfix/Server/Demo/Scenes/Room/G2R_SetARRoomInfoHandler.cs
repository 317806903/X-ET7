using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_SetARRoomInfoHandler : AMActorRpcHandler<Scene, G2R_SetARRoomInfo, R2G_SetARRoomInfo>
	{
		protected override async ETTask Run(Scene scene, G2R_SetARRoomInfo request, R2G_SetARRoomInfo response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			string ARSceneId = request.ARSceneId;
			string ARMeshDownLoadUrl = request.ARMeshDownLoadUrl;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);

			if (roomComponent.ownerRoomMemberId != playerId)
			{
				string msg = $"roomComponent.ownerRoomMemberId[{roomComponent.ownerRoomMemberId}] != playerId[{playerId}] roomId[{roomComponent.Id}]";
				Log.Error(msg);
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			roomManagerComponent.SetARSceneId(roomId, ARSceneId);
			roomManagerComponent.SetARMeshDownLoadUrl(roomId, ARMeshDownLoadUrl);

			await ETTask.CompletedTask;
		}
	}
}