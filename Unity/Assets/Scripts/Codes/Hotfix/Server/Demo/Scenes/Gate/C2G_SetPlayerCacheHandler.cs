using System;


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

			await ET.Server.PlayerCacheHelper.SetPlayerModel(session.DomainScene(), playerId, playerModelType, PlayerModelComponentBytes);
			await ET.Server.PlayerCacheHelper.SavePlayerModel(session.DomainScene(), playerId, playerModelType);

			//
			// StartSceneConfig playerCacheSceneConfig = StartSceneConfigCategory.Instance.GetPlayerCacheManager(session.DomainZone());
			//
			// P2G_SetPlayerCache _P2G_SetPlayerCache = (P2G_SetPlayerCache) await ActorMessageSenderComponent.Instance.Call(playerCacheSceneConfig.InstanceId, new G2P_SetPlayerCache()
			// {
			// 	PlayerId = playerId,
			// 	PlayerModelType = (int)playerModelType,
			// 	PlayerModelComponentBytes = PlayerModelComponentBytes,
			// });
			//
			// response.Error = _P2G_SetPlayerCache.Error;
			// response.Message = _P2G_SetPlayerCache.Message;

			await ETTask.CompletedTask;
		}
	}
}