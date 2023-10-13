using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_ChgRoomMemberStatusHandler : AMActorRpcHandler<Scene, G2R_ChgRoomMemberStatus, R2G_ChgRoomMemberStatus>
	{
		protected override async ETTask Run(Scene scene, G2R_ChgRoomMemberStatus request, R2G_ChgRoomMemberStatus response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			bool isReady = request.IsReady == 1?true:false;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			if (roomComponent.ChkIsOwner(playerId))
			{
				(bool bRet, string msg) = roomComponent.ChkOwnerStartGame();
				if (bRet == false)
				{
					Log.Error(msg);
					response.Error = ET.ErrorCode.ERR_LogicError;
					response.Message = msg;
					return;
				}
				isReady = true;
			}
			roomComponent.ChgRoomMemberStatus(playerId, isReady);
			response.IsReady = isReady?1:0;

			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, false).Coroutine();

			if (isReady && roomComponent.ChkIsOwner(playerId))
			{
				List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();

				roomManagerComponent.ChgRoomStatus(roomId, RoomStatus.EnteringBattle);

				StartSceneConfig dynamicMapConfig = StartSceneConfigCategory.Instance.GetDynamicMap(scene.DomainZone());

				byte[] roomInfo = roomComponent.ToBson();
				ListComponent<byte[]> roomMemberInfos = ListComponent<byte[]>.Create();
				foreach (var roomMember in roomMemberList)
				{
					roomMemberInfos.Add(roomMember.ToBson());
				}

				if (roomComponent.dynamicMapInstanceId > 0)
				{
					R2M_DestroyDynamicMap _R2M_DestroyDynamicMap = new ()
					{
						DynamicMapInstanceId = roomComponent.dynamicMapInstanceId,
					};
					M2R_DestroyDynamicMap _M2R_DestroyDynamicMap = (M2R_DestroyDynamicMap) await ActorMessageSenderComponent.Instance.Call(dynamicMapConfig
							.InstanceId, _R2M_DestroyDynamicMap);
				}

				R2M_CreateDynamicMap _R2M_CreateDynamicMap = new ()
				{
					RoomInfo = roomInfo,
					RoomMemberInfos = roomMemberInfos,
					ARMeshDownLoadUrl = roomManagerComponent.GetARMeshDownLoadUrl(roomId),
				};
				M2R_CreateDynamicMap _M2R_CreateDynamicMap = (M2R_CreateDynamicMap) await ActorMessageSenderComponent.Instance.Call(dynamicMapConfig
						.InstanceId, _R2M_CreateDynamicMap);
				long dynamicMapInstanceId = _M2R_CreateDynamicMap.DynamicMapInstanceId;

				roomComponent.dynamicMapInstanceId = dynamicMapInstanceId;

				foreach (RoomMember roomMember in roomMemberList)
				{
					R2G_StartBattle _R2G_StartBattle = new ()
					{
						DynamicMapInstanceId = dynamicMapInstanceId,
						GamePlayBattleLevelCfgId = roomComponent.gamePlayBattleLevelCfgId,
					};
					ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
					await oneTypeLocationType.Call(roomMember.Id, _R2G_StartBattle);
				}

				roomManagerComponent.ChgRoomStatus(roomId, RoomStatus.InTheBattle);
				foreach (RoomMember roomMember in roomMemberList)
				{
					roomComponent.ChgRoomMemberStatus(roomMember.Id, false);
				}
			}
			else
			{
			}

			await ETTask.CompletedTask;
		}
	}
}