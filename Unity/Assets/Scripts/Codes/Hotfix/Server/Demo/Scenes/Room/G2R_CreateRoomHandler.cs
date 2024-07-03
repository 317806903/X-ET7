using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_CreateRoomHandler : AMActorRpcHandler<Scene, G2R_CreateRoom, R2G_CreateRoom>
	{
		protected override async ETTask Run(Scene scene, G2R_CreateRoom request, R2G_CreateRoom response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			long playerId = request.PlayerId;
			byte[] roomTypeInfoBytes = request.RoomTypeInfo;
			RoomTypeInfo roomTypeInfo = ET.RoomTypeInfo.GetFromBytes(roomTypeInfoBytes);

			RoomComponent roomComponentOld = roomManagerComponent.GetRoomByPlayerId(playerId);
			if (roomComponentOld != null)
			{
				roomManagerComponent.QuitRoom(playerId, roomComponentOld.Id);
				await ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponentOld, false);
			}

			RoomTeamMode roomTeamMode = RoomTeamMode.Single;
			RoomComponent roomComponent = roomManagerComponent.CreateRoom(roomTypeInfo, playerId, roomTeamMode);

			response.RoomId = roomComponent.Id;

			await ETTask.CompletedTask;
		}
	}
}