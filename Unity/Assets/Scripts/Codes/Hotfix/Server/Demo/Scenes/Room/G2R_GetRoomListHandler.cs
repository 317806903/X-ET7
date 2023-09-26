using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_GetRoomListHandler : AMActorRpcHandler<Scene, G2R_GetRoomList, R2G_GetRoomList>
	{
		protected override async ETTask Run(Scene scene, G2R_GetRoomList request, R2G_GetRoomList response)
		{
			bool isARRoom = request.IsARRoom == 1? true : false;
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			response.RoomInfos = ListComponent<byte[]>.Create();
			foreach (long roomId in roomManagerComponent.GetIdleRoomList(isARRoom))
			{
				RoomComponent roomComponent = roomManagerComponent.GetChild<RoomComponent>(roomId);
				response.RoomInfos.Add(roomComponent.ToBson());
			}
			
			await ETTask.CompletedTask;
		}
	}
}