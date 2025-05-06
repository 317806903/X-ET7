using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Room)]
	public class G2R_ChgRoomBattleLevelCfgHandler : AMActorRpcHandler<Scene, G2R_ChgRoomBattleLevelCfg, R2G_ChgRoomBattleLevelCfg>
	{
		protected override async ETTask Run(Scene scene, G2R_ChgRoomBattleLevelCfg request, R2G_ChgRoomBattleLevelCfg response)
		{
			RoomManagerComponent roomManagerComponent = ET.Server.RoomHelper.GetRoomManager(scene);
			long playerId = request.PlayerId;
			long roomId = request.RoomId;
			RoomTypeInfo roomTypeInfo = ET.RoomTypeInfo.GetFromBytes(request.RoomTypeInfo);
			string newBattleCfgId = roomTypeInfo.gamePlayBattleLevelCfgId;
			RoomComponent roomComponent = roomManagerComponent.GetRoom(roomId);
			if (roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId == newBattleCfgId)
			{
				string msg = $"roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId == newBattleCfgId[{newBattleCfgId}]";
				Log.Error(msg);
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			if (roomComponent.ownerRoomMemberId != playerId)
			{
				string msg = $"roomComponent.ownerRoomMemberId[{roomComponent.ownerRoomMemberId}] != playerId[{playerId}] roomId[{roomComponent.Id}]";
				Log.Error(msg);
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			roomComponent.ChgRoomBattleLevelCfg(roomTypeInfo);
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId);
			roomManagerComponent.SetMapScale(roomId, gamePlayBattleLevelCfg.MapScale);

			List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();
			for (int i = 0; i < roomMemberList.Count; i++)
			{
				RoomMember roomMember = roomMemberList[i];
				roomMember.isReady = false;
			}

			ET.Server.RoomHelper.SendRoomInfoChgNotice(roomComponent, false).Coroutine();

			await ETTask.CompletedTask;
		}
	}
}