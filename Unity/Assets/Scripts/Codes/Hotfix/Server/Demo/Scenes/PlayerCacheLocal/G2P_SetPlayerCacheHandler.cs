using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.PlayerCache)]
	public class G2P_SetPlayerCacheHandler : AMActorRpcHandler<Scene, G2P_SetPlayerCache, P2G_SetPlayerCache>
	{
		protected override async ETTask Run(Scene scene, G2P_SetPlayerCache request, P2G_SetPlayerCache response)
		{
			long playerId = request.PlayerId;
			PlayerModelType playerModelType = (PlayerModelType)request.PlayerModelType;
			byte[] playerModelComponentBytes = request.PlayerModelComponentBytes;
			List<string> setPlayerKeys = request.SetPlayerKeys;
			PlayerModelChgType playerModelChgType = (PlayerModelChgType)request.PlayerModelChgType;
			await ET.Server.PlayerCacheLocalHelper.SetPlayerModel(scene, playerId, playerModelType, playerModelComponentBytes, setPlayerKeys, playerModelChgType);

			await ETTask.CompletedTask;
		}
	}
}