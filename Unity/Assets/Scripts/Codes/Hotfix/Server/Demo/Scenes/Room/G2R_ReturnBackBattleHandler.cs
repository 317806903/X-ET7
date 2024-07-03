using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_ReturnBackBattleHandler : AMActorRpcHandler<Scene, G2R_ReturnBackBattle, R2G_ReturnBackBattle>
	{
		protected override async ETTask Run(Scene scene, G2R_ReturnBackBattle request, R2G_ReturnBackBattle response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			long playerId = request.PlayerId;
			RoomComponent roomComponent = roomManagerComponent.GetRoomByPlayerId(playerId);
			if (roomComponent != null)
			{
				if (roomComponent.roomStatus != RoomStatus.InTheBattle)
				{
					string msg = $"roomComponent.roomStatus[{roomComponent.roomStatus}] != RoomStatus.InTheBattle [playerId={playerId}][roomId={roomComponent.Id}]";
					Log.Error(msg);
					response.Error = ET.ErrorCode.ERR_LogicError;
					response.Message = msg;
					return;
				}
				long dynamicMapInstanceId = roomComponent.dynamicMapInstanceId;
				RoomMember roomMember = roomComponent.GetRoomMember(playerId);
				R2G_StartBattle _R2G_StartBattle = new ()
				{
					DynamicMapInstanceId = dynamicMapInstanceId,
					GamePlayBattleLevelCfgId = roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId,
				};

				ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
				await oneTypeLocationType.Call(playerId, _R2G_StartBattle, scene.InstanceId);
			}
			else
			{
				string msg = $"not find room when roomManagerComponent.GetRoomByPlayerId[{playerId}]";
				Log.Error(msg);
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
			}

			await ETTask.CompletedTask;
		}
	}
}