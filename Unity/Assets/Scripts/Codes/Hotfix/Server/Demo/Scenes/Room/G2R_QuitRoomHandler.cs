using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_QuitRoomHandler : AMActorRpcHandler<Scene, G2R_QuitRoom, R2G_QuitRoom>
	{
		protected override async ETTask Run(Scene scene, G2R_QuitRoom request, R2G_QuitRoom response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			roomManagerComponent.QuitRoom(playerId, roomId);
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			
			
			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, true).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}