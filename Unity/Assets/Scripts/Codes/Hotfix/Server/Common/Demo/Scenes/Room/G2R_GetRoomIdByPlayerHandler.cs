using System;
using System.Collections.Generic;
using MongoDB.Bson;

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
			int mapScale = 0;
			int roomStatus = 0;
			byte[] roomTypeInfo = null;
			if (roomComponent != null)
			{
				roomId = roomComponent.Id;
				mapScale = (int)(roomComponent.mapScale * 100);
				roomStatus = (int)roomComponent.roomStatus;
				roomTypeInfo = ET.RoomTypeInfo.ToBytes(roomComponent.roomTypeInfo);
			}
			response.RoomId = roomId;
			response.MapScale = mapScale;
			response.RoomStatus = roomStatus;
			response.RoomTypeInfo = roomTypeInfo;
			await ETTask.CompletedTask;
		}
	}
}