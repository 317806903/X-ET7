using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_ChgRoomMemberStatusHandler : AMActorRpcHandler<Scene, G2R_ChgRoomMemberStatus, R2G_ChgRoomMemberStatus>
	{
		protected override async ETTask Run(Scene scene, G2R_ChgRoomMemberStatus request, R2G_ChgRoomMemberStatus response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			bool isReady = request.IsReady == 1?true:false;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			if (roomComponent.ChkIsOwner(playerId))
			{
				isReady = true;
			}
			roomComponent.ChgRoomMemberStatus(playerId, isReady);
			response.IsReady = isReady?1:0;

			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, false).Coroutine();
			
			if (isReady && roomComponent.ChkIsOwner(playerId) && roomComponent.ChkIsAllReady())
			{
				roomManagerComponent.ChgRoomStatus(roomId, RoomStatus.EnterBattle);
				
				StartSceneConfig dynamicMapConfig = StartSceneConfigCategory.Instance.GetDynamicMap(scene.DomainZone());
					
				byte[] roomInfo = roomComponent.ToBson();
				ListComponent<byte[]> roomMemberInfos = ListComponent<byte[]>.Create();
				foreach (var roomMember in roomComponent.GetRoomMemberList())
				{
					roomMemberInfos.Add(roomMember.ToBson());
				}

				if (roomComponent.sceneMapId > 0)
				{
					R2M_DestroyDynamicMap _R2M_DestroyDynamicMap = new ()
					{
						DynamicMapId = roomComponent.sceneMapId,
					};
					M2R_DestroyDynamicMap _M2R_DestroyDynamicMap = (M2R_DestroyDynamicMap) await ActorMessageSenderComponent.Instance.Call(dynamicMapConfig
							.InstanceId, _R2M_DestroyDynamicMap);
				}
				
				R2M_CreateDynamicMap _R2M_CreateDynamicMap = new ()
				{
					RoomInfo = roomInfo,
					RoomMemberInfos = roomMemberInfos,
				};
				M2R_CreateDynamicMap _M2R_CreateDynamicMap = (M2R_CreateDynamicMap) await ActorMessageSenderComponent.Instance.Call(dynamicMapConfig
						.InstanceId, _R2M_CreateDynamicMap);
				long dynamicMapId = _M2R_CreateDynamicMap.DynamicMapId;

				roomComponent.sceneMapId = dynamicMapId;
				List<RoomMember> roomMemberList2 = roomComponent.GetRoomMemberList();
				foreach (RoomMember roomMember in roomMemberList2)
				{
					R2G_StartBattle _R2G_StartBattle = new ()
					{
						DynamicMapId = dynamicMapId,
						RoomSeatIndex = roomMember.seatIndex,
					};
					ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
					await oneTypeLocationType.Call(roomMember.Id, _R2G_StartBattle);
				}
				
				roomManagerComponent.ChgRoomStatus(roomId, RoomStatus.InTheBattle);
			}
			else
			{
			}
			
			await ETTask.CompletedTask;
		}
	}
}