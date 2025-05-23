﻿using System;
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
			string ARSceneMeshId = request.ARSceneMeshId;
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
			if (roomComponent.arSceneId != ARSceneId || roomComponent.arSceneMeshId != ARSceneMeshId)
			{
				isNeedResetReady = true;
			}
			roomManagerComponent.SetARSceneId(roomId, ARSceneId);
			roomManagerComponent.SetARSceneMeshId(roomId, ARSceneMeshId);
			roomManagerComponent.SetARMeshInfo(roomId, ARMeshType, ARSceneId, ARSceneMeshId, ARMeshDownLoadUrl, ARMeshBytes);
			roomManagerComponent.SetMapScale(roomId, ARMapScale * 0.01f);

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