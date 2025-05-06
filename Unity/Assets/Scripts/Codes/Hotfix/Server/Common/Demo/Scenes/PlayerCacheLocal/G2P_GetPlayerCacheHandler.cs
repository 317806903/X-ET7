using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.PlayerCache)]
	public class G2P_GetPlayerCacheHandler : AMActorRpcHandler<Scene, G2P_GetPlayerCache, P2G_GetPlayerCache>
	{
		protected override async ETTask Run(Scene scene, G2P_GetPlayerCache request, P2G_GetPlayerCache response)
		{
			long playerId = request.PlayerId;
			PlayerModelType playerModelType = (PlayerModelType)request.PlayerModelType;
			Entity entity = await ET.Server.PlayerCacheLocalHelper.GetPlayerModel(scene, playerId, playerModelType);
			byte[] bytes = entity.ToBson();
			response.PlayerModelComponentBytes = bytes;

			await ETTask.CompletedTask;
		}
	}
}