using System;
using System.Collections.Generic;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_SetPlayerCacheHandler : AMRpcHandler<C2G_SetPlayerCache, G2C_SetPlayerCache>
	{
		protected override async ETTask Run(Session session, C2G_SetPlayerCache request, G2C_SetPlayerCache response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			if (playerId != request.PlayerId)
			{
				string msg = $"playerId[{playerId}] != request.PlayerId[{request.PlayerId}]";
				Log.Error(msg);
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}
			PlayerModelType playerModelType = (PlayerModelType)request.PlayerModelType;
			byte[] PlayerModelComponentBytes = request.PlayerModelComponentBytes;
			List<string> setPlayerKeys = request.SetPlayerKeys;

			await ET.Server.PlayerCacheHelper.SetPlayerModelByClient(session.DomainScene(), playerId, playerModelType, PlayerModelComponentBytes, setPlayerKeys);

			PlayerModelChgType playerModelChgType = PlayerModelChgType.None;
			if (playerModelType == PlayerModelType.BaseInfo)
			{
				playerModelChgType = PlayerModelChgType.PlayerBaseInfo_Client;
			}
			else if (playerModelType == PlayerModelType.BackPack)
			{
				playerModelChgType = PlayerModelChgType.PlayerBackPack_Client;
			}
			else if (playerModelType == PlayerModelType.BattleCard)
			{
				playerModelChgType = PlayerModelChgType.PlayerBattleCard_Client;
			}
			await ET.Server.PlayerCacheHelper.SavePlayerModel(session.DomainScene(), playerId, playerModelType, setPlayerKeys, playerModelChgType);

			await ETTask.CompletedTask;
		}
	}
}