using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_GetRoomIdByPlayerHandler : AMActorRpcHandler<Scene, G2R_GetRoomIdByPlayer, R2G_GetRoomIdByPlayer>
	{
		protected override async ETTask Run(Scene scene, G2R_GetRoomIdByPlayer request, R2G_GetRoomIdByPlayer response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			RoomComponent roomComponent = roomManagerComponent.GetRoomByPlayerId(request.PlayerId);
			long roomId = 0;
			int roomStatus = 0;
			int roomType = 0;
			int subRoomType = 0;
			if (roomComponent != null)
			{
				roomId = roomComponent.Id;
				roomStatus = (int)roomComponent.roomStatus;
				roomType = (int)roomComponent.roomType;
				subRoomType = (int)roomComponent.subRoomType;
			}
			response.RoomId = roomId;
			response.RoomStatus = roomStatus;
			response.RoomType = roomType;
			response.SubRoomType = subRoomType;
			await ETTask.CompletedTask;
		}
	}
}