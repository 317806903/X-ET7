using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_KickMemberOutRoomHandler : AMActorRpcHandler<Scene, G2R_KickMemberOutRoom, R2G_KickMemberOutRoom>
	{
		protected override async ETTask Run(Scene scene, G2R_KickMemberOutRoom request, R2G_KickMemberOutRoom response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			long beKickPlayerId = request.BeKickPlayerId;
			long roomId = request.RoomId;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			if (roomComponent.ownerRoomMemberId != playerId)
			{
				Log.Error($"roomComponent.ownerRoomMemberId[{roomComponent.ownerRoomMemberId}] != playerId[{playerId}]");
				response.Error = 1;
			}
			roomManagerComponent.QuitRoom(beKickPlayerId, roomId);

			{
				R2G_BeKickedMember _R2G_BeKickedMember = new ();
				ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
				await oneTypeLocationType.Call(beKickPlayerId, _R2G_BeKickedMember);
			}
			
			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, true).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}