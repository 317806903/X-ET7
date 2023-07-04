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
			response.IsARRoom = roomComponent.isARRoom?1:0;

			
			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, true).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}