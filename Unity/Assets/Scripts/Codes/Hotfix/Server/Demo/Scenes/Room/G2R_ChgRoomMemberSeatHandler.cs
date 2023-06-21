using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_ChgRoomMemberSeatHandler : AMActorRpcHandler<Scene, G2R_ChgRoomMemberSeat, R2G_ChgRoomMemberSeat>
	{
		protected override async ETTask Run(Scene scene, G2R_ChgRoomMemberSeat request, R2G_ChgRoomMemberSeat response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			int newSeat = request.NewSeat;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			roomComponent.ChgRoomMemberSeat(playerId, newSeat);

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