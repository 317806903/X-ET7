using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_ChgRoomBattleLevelCfgHandler : AMActorRpcHandler<Scene, G2R_ChgRoomBattleLevelCfg, R2G_ChgRoomBattleLevelCfg>
	{
		protected override async ETTask Run(Scene scene, G2R_ChgRoomBattleLevelCfg request, R2G_ChgRoomBattleLevelCfg response)
		{
			RoomManagerComponent roomManagerComponent = scene.GetComponent<RoomManagerComponent>();
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			string newBattleCfgId = request.NewBattleCfgId;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			if (roomComponent.ownerRoomMemberId != playerId)
			{
				string msg = $"roomComponent.ownerRoomMemberId[{roomComponent.ownerRoomMemberId}] != playerId[{playerId}] roomId[{roomComponent.Id}]";
				Log.Error(msg);
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			roomComponent.ChgRoomBattleLevelCfg(newBattleCfgId);

			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, false).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}