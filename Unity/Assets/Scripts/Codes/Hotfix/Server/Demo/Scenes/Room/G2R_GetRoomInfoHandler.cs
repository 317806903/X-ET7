using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_GetRoomInfoHandler : AMActorRpcHandler<Scene, G2R_GetRoomInfo, R2G_GetRoomInfo>
	{
		protected override async ETTask Run(Scene scene, G2R_GetRoomInfo request, R2G_GetRoomInfo response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);

			RoomComponent roomComponent = roomManagerComponent.GetChild<RoomComponent>(request.RoomId);
			if (roomComponent == null)
			{
				string msg = $"roomComponent == null roomId=[{request.RoomId}]";
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}
			response.RoomInfo = roomComponent.ToBson();
			response.RoomMemberInfos = new();
			foreach (var roomMember in roomComponent.GetRoomMemberList())
			{
				response.RoomMemberInfos.Add(roomMember.ToBson());
			}
			await ETTask.CompletedTask;
		}
	}
}