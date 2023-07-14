using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_QuitRoomHandler : AMActorRpcHandler<Scene, G2R_QuitRoom, R2G_QuitRoom>
	{
		protected override async ETTask Run(Scene scene, G2R_QuitRoom request, R2G_QuitRoom response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			if (roomId == 0)
			{
				return;
			}
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			if (roomComponent == null)
			{
				return;
			}
			long dynamicMapId = roomComponent.sceneMapId;
			bool isEmptyMember = roomManagerComponent.QuitRoom(playerId, roomId);
			if (isEmptyMember)
			{
				// if (dynamicMapId > 0)
				// {
				// 	R2M_DestroyDynamicMap _R2M_DestroyDynamicMap = new ()
				// 	{
				// 		DynamicMapId = dynamicMapId,
				// 	};
				// 	StartSceneConfig dynamicMapConfig = StartSceneConfigCategory.Instance.GetDynamicMap(scene.DomainZone());
				// 	M2R_DestroyDynamicMap _M2R_DestroyDynamicMap = (M2R_DestroyDynamicMap) await ActorMessageSenderComponent.Instance.Call(dynamicMapConfig
				// 			.InstanceId, _R2M_DestroyDynamicMap);
				// }
			}
			else
			{
				ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, true).Coroutine();
			}
			
			await ETTask.CompletedTask;
		}
	}
}