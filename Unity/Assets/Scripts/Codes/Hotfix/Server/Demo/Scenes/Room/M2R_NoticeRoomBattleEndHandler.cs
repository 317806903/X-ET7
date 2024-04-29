using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class M2R_NoticeRoomBattleEndHandler : AMActorRpcHandler<Scene, M2R_NoticeRoomBattleEnd, R2M_NoticeRoomBattleEnd>
	{
		protected override async ETTask Run(Scene scene, M2R_NoticeRoomBattleEnd request, R2M_NoticeRoomBattleEnd response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			long roomId = request.RoomId;
			long playerId = request.PlayerId;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			if (roomComponent == null)
			{
				return;
			}

			if (playerId != -1)
			{
				bool isReady = request.IsReady == 1?true:false;
				RoomMember roomMember = roomComponent.GetRoomMember(playerId);
				if (roomMember != null)
				{
					roomMember.isReady = isReady;
				}

				BattleResult battleResult = BattleResult.Failed;
				M2G_MemberReturnRoomFromBattle _M2G_MemberReturnRoomFromBattle = new()
				{
					BattleResult = (int)battleResult,
				};
				ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
				await oneTypeLocationType.Call(playerId, _M2G_MemberReturnRoomFromBattle, scene.InstanceId);

				return;
			}
			else
			{
				roomManagerComponent.ChgRoomStatus(roomId, RoomStatus.Idle);

				ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, true).Coroutine();

				bool isReady = request.IsReady == 1?true:false;
				List<long> winPlayers = request.WinPlayers;
				List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();
				for (int i = 0; i < roomMemberList.Count; i++)
				{
					RoomMember roomMember = roomMemberList[i];
					long roomMemberPlayerId = roomMember.Id;
					roomMember.isReady = isReady;
					BattleResult battleResult = (winPlayers != null && winPlayers.Contains(roomMemberPlayerId))? BattleResult.Successed : BattleResult.Failed;
					M2G_MemberReturnRoomFromBattle _M2G_MemberReturnRoomFromBattle = new()
					{
						BattleResult = (int)battleResult,
					};
					ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
					await oneTypeLocationType.Call(roomMemberPlayerId, _M2G_MemberReturnRoomFromBattle, scene.InstanceId);
				}

			}
			await ETTask.CompletedTask;
		}
	}
}