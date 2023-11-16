using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_JoinRoomHandler : AMActorRpcHandler<Scene, G2R_JoinRoom, R2G_JoinRoom>
	{
		protected override async ETTask Run(Scene scene, G2R_JoinRoom request, R2G_JoinRoom response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			if (roomComponent == null)
			{
				string msg = $"roomComponent == null roomId=[{roomId}]";
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			RoomComponent roomComponentOld = roomManagerComponent.GetRoomByPlayerId(playerId);
			if (roomComponentOld != null)
			{
				roomManagerComponent.QuitRoom(playerId, roomComponentOld.Id);
			}
			roomManagerComponent.JoinRoom(playerId, roomId);
			response.RoomType = (int)roomComponent.roomType;
			response.SubRoomType = (int)roomComponent.subRoomType;


			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, true).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}