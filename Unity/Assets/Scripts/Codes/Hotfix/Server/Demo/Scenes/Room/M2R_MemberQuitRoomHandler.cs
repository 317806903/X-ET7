using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class M2R_MemberQuitRoomHandler : AMActorRpcHandler<Scene, M2R_MemberQuitRoom, R2M_MemberQuitRoom>
	{
		protected override async ETTask Run(Scene scene, M2R_MemberQuitRoom request, R2M_MemberQuitRoom response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			RoomComponent roomComponent1 = roomManagerComponent.GetRoom(roomId);
			if (roomComponent1 == null)
			{
				return;
			}
			RoomComponent roomComponent2 = roomManagerComponent.GetRoomByPlayerId(playerId);
			if (roomComponent2 == null || roomComponent1 == roomComponent2)
			{
				{
					R2G_BeKickedMember _R2G_BeKickedMember = new ();
					ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
					await oneTypeLocationType.Call(playerId, _R2G_BeKickedMember);
				}
			}
			roomManagerComponent.QuitRoom(playerId, roomId);
			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent1, true).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}