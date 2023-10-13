using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_ChgRoomMemberTeamHandler : AMActorRpcHandler<Scene, G2R_ChgRoomMemberTeam, R2G_ChgRoomMemberTeam>
	{
		protected override async ETTask Run(Scene scene, G2R_ChgRoomMemberTeam request, R2G_ChgRoomMemberTeam response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			RoomTeamId newTeam = (RoomTeamId)request.NewTeam;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			roomComponent.ChgRoomMemberTeam(playerId, newTeam);

			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, false).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}