using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_CreateRoomHandler : AMActorRpcHandler<Scene, G2R_CreateRoom, R2G_CreateRoom>
	{
		protected override async ETTask Run(Scene scene, G2R_CreateRoom request, R2G_CreateRoom response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			string battleCfgId = request.BattleCfgId;
			bool isARRoom = request.IsARRoom == 1? true : false;
			RoomTeamMode roomTeamMode = RoomTeamMode.Single;
			RoomComponent roomComponent = roomManagerComponent.CreateRoom(isARRoom, playerId, roomTeamMode, battleCfgId);
			
			response.RoomId = roomComponent.Id;

			await ETTask.CompletedTask;
		}
	}
}