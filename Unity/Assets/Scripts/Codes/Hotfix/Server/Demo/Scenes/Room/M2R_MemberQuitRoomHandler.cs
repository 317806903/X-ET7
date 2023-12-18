using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class M2R_MemberQuitRoomHandler : AMActorRpcHandler<Scene, M2R_MemberQuitRoom, R2M_MemberQuitRoom>
	{
		protected override async ETTask Run(Scene scene, M2R_MemberQuitRoom request, R2M_MemberQuitRoom response)
		{
			long playerId = request.PlayerId;
			long roomId = request.RoomId;

			await KickMember(scene, playerId, roomId);

			await ETTask.CompletedTask;
		}

		public static async ETTask KickMember(Scene scene, long playerId, long roomId)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			RoomComponent roomComponent1 = roomManagerComponent.GetRoom(roomId);
			if (roomComponent1 == null)
			{
				return;
			}
			RoomComponent roomComponent2 = roomManagerComponent.GetRoomByPlayerId(playerId);
			if (roomComponent2 == null || roomComponent1 == roomComponent2)
			{
				try
				{
					R2G_BeKickedMember _R2G_BeKickedMember = new ();
					ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
					await oneTypeLocationType.Call(playerId, _R2G_BeKickedMember);
				}
				catch (Exception e)
				{
					Log.Error($"M2R_MemberQuitRoom {e}");
				}
			}
			roomManagerComponent.QuitRoom(playerId, roomId);
			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent1, true).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}