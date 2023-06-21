using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_GetRoomInfoHandler : AMActorRpcHandler<Scene, G2R_GetRoomInfo, R2G_GetRoomInfo>
	{
		protected override async ETTask Run(Scene scene, G2R_GetRoomInfo request, R2G_GetRoomInfo response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();

			RoomComponent roomComponent = roomManagerComponent.GetChild<RoomComponent>(request.RoomId);
			response.RoomInfo = roomComponent.ToBson();
			response.RoomMemberInfos = ListComponent<byte[]>.Create();
			foreach (var roomMember in roomComponent.GetRoomMemberList())
			{
				response.RoomMemberInfos.Add(roomMember.ToBson());
			}
			await ETTask.CompletedTask;
		}
	}
}