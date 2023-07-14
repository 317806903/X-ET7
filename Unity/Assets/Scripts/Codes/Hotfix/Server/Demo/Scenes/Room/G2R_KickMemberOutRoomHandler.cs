using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_KickMemberOutRoomHandler : AMActorRpcHandler<Scene, G2R_KickMemberOutRoom, R2G_KickMemberOutRoom>
	{
		protected override async ETTask Run(Scene scene, G2R_KickMemberOutRoom request, R2G_KickMemberOutRoom response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			long beKickPlayerId = request.BeKickPlayerId;
			long roomId = request.RoomId;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			if (roomComponent.ownerRoomMemberId != playerId)
			{
				string msg = $"roomComponent.ownerRoomMemberId[{roomComponent.ownerRoomMemberId}] != playerId[{playerId}] roomId[{roomComponent.Id}]";
				Log.Error(msg);
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			if (roomComponent.sceneMapId > 0)
			{
				if (roomComponent.roomStatus == RoomStatus.InTheBattle)
				{
					R2M_ChkIsBattleEnd _R2M_ChkIsBattleEnd = new ();
					M2R_ChkIsBattleEnd _M2R_ChkIsBattleEnd = (M2R_ChkIsBattleEnd) await ActorMessageSenderComponent.Instance.Call(roomComponent.sceneMapId, _R2M_ChkIsBattleEnd);
					bool isBattleEnd = _M2R_ChkIsBattleEnd.IsBattleEnd == 1? true : false;
					if (isBattleEnd == false)
					{
						string msg = $"isBattleEnd == false playerId[{playerId}] roomId[{roomComponent.Id}]";
						Log.Error(msg);
						response.Error = ET.ErrorCode.ERR_LogicError;
						response.Message = msg;
						return;
					}
					else
					{
						R2M_MemberQuitBattle _R2M_MemberQuitBattle = new ()
						{
							PlayerId = beKickPlayerId,
						};
						ActorMessageSenderComponent.Instance.Send(roomComponent.sceneMapId, _R2M_MemberQuitBattle);
					}
				}
				else
				{
					R2M_MemberQuitBattle _R2M_MemberQuitBattle = new ()
					{
						PlayerId = beKickPlayerId,
					};
					ActorMessageSenderComponent.Instance.Send(roomComponent.sceneMapId, _R2M_MemberQuitBattle);
				}
			}
			roomManagerComponent.QuitRoom(beKickPlayerId, roomId);

			{
				R2G_BeKickedMember _R2G_BeKickedMember = new ();
				ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
				await oneTypeLocationType.Call(beKickPlayerId, _R2G_BeKickedMember);
			}
			
			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, true).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}