using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_JoinRoomHandler : AMActorRpcHandler<Scene, G2R_JoinRoom, R2G_JoinRoom>
	{
		protected override async ETTask Run(Scene scene, G2R_JoinRoom request, R2G_JoinRoom response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			roomManagerComponent.JoinRoom(playerId, roomId);
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);

			R2C_RoomInfoChgNotice _R2C_RoomInfoChgNotice = new();
			List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();
			for (int i = 0; i < roomMemberList.Count; i++)
			{
				RoomMember roomMember = roomMemberList[i];
				// if (playerId == roomMember.Id)
				// {
				// 	continue;
				// }
				MessageHelper.SendToClient(roomMember.Id, _R2C_RoomInfoChgNotice, false);
			}

			await ETTask.CompletedTask;
		}
	}
}