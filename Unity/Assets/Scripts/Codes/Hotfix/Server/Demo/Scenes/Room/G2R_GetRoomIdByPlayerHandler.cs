using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_GetRoomIdByPlayerHandler : AMActorRpcHandler<Scene, G2R_GetRoomIdByPlayer, R2G_GetRoomIdByPlayer>
	{
		protected override async ETTask Run(Scene scene, G2R_GetRoomIdByPlayer request, R2G_GetRoomIdByPlayer response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			RoomComponent roomComponent = roomManagerComponent.GetRoomByPlayerId(request.PlayerId);
			long roomId = 0;
			RoomStatus roomStatus = RoomStatus.Idle;
			if (roomComponent != null)
			{
				roomId = roomComponent.Id;
				roomStatus = roomComponent.roomStatus;
			}
			response.RoomId = roomId;
			response.RoomStatus = roomStatus.ToString();
			await ETTask.CompletedTask;
		}
	}
}