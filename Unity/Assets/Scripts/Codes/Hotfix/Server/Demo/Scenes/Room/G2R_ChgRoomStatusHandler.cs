using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_ChgRoomStatusHandler : AMActorRpcHandler<Scene, G2R_ChgRoomStatus, R2G_ChgRoomStatus>
	{
		protected override async ETTask Run(Scene scene, G2R_ChgRoomStatus request, R2G_ChgRoomStatus response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long roomId = request.RoomId;
			RoomStatus roomStatus = (RoomStatus)request.RoomStatus;
			roomManagerComponent.ChgRoomStatus(roomId, roomStatus);

			await ETTask.CompletedTask;
		}
	}
}