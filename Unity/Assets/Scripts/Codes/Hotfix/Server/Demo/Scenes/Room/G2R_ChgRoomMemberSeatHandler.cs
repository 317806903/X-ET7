using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_ChgRoomMemberSeatHandler : AMActorRpcHandler<Scene, G2R_ChgRoomMemberSeat, R2G_ChgRoomMemberSeat>
	{
		protected override async ETTask Run(Scene scene, G2R_ChgRoomMemberSeat request, R2G_ChgRoomMemberSeat response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			int newSeat = request.NewSeat;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			roomComponent.ChgRoomMemberSeat(playerId, newSeat);

			
			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, false).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}