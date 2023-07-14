using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class M2R_NoticeRoomBattleEndHandler : AMActorRpcHandler<Scene, M2R_NoticeRoomBattleEnd, R2M_NoticeRoomBattleEnd>
	{
		protected override async ETTask Run(Scene scene, M2R_NoticeRoomBattleEnd request, R2M_NoticeRoomBattleEnd response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long roomId = request.RoomId;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			if (roomComponent == null)
			{
				return;
			}
			
			roomManagerComponent.ChgRoomStatus(roomId, RoomStatus.Idle);
			
			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, true).Coroutine();

			List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();
			for (int i = 0; i < roomMemberList.Count; i++)
			{
				RoomMember roomMember = roomMemberList[i];
				long playerId = roomMember.Id;
				M2G_MemberReturnRoomFromBattle _M2G_MemberReturnRoomFromBattle = new();
				ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
				await oneTypeLocationType.Call(playerId, _M2G_MemberReturnRoomFromBattle);
			}

			await ETTask.CompletedTask;
		}
	}
}