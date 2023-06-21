using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_ReturnBackBattleHandler : AMActorRpcHandler<Scene, G2R_ReturnBackBattle, R2G_ReturnBackBattle>
	{
		protected override async ETTask Run(Scene scene, G2R_ReturnBackBattle request, R2G_ReturnBackBattle response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			RoomComponent roomComponent = roomManagerComponent.GetRoomByPlayerId(playerId);
			if (roomComponent != null)
			{
				long dynamicMapId = roomComponent.sceneMapId;
				
				R2G_StartBattle _R2G_StartBattle = new ()
				{
					DynamicMapId = dynamicMapId,
				};
				
				ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
				await oneTypeLocationType.Call(playerId, _R2G_StartBattle);
			}

			await ETTask.CompletedTask;
		}
	}
}