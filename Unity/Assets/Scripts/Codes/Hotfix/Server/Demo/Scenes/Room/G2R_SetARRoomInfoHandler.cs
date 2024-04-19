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
			int ARMapScale = request.ARMapScale;

			ARMeshType ARMeshType = (ARMeshType)request.ARMeshType;
			string ARSceneId = request.ARSceneId;
			string ARMeshDownLoadUrl = request.ARMeshDownLoadUrl;
			byte[] ARMeshBytes = request.ARMeshBytes;

			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);

			if (roomComponent.ownerRoomMemberId != playerId)
			{
				string msg = $"roomComponent.ownerRoomMemberId[{roomComponent.ownerRoomMemberId}] != playerId[{playerId}] roomId[{roomComponent.Id}]";
				Log.Error(msg);
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			bool isNeedResetReady = false;
			if (roomComponent.arSceneId != ARSceneId)
			{
				isNeedResetReady = true;
			}
			roomManagerComponent.SetARSceneId(roomId, ARSceneId);
			roomManagerComponent.SetARMeshInfo(roomId, ARMeshType, ARMeshDownLoadUrl, ARMeshBytes);
			roomManagerComponent.SetARMapScale(roomId, ARMapScale);

			if (isNeedResetReady)
			{
				List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();
				for (int i = 0; i < roomMemberList.Count; i++)
				{
					RoomMember roomMember = roomMemberList[i];
					roomMember.isReady = false;
				}
			}

			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, false).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}