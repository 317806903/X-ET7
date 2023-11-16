using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_GetPlayerCacheHandler : AMRpcHandler<C2G_GetPlayerCache, G2C_GetPlayerCache>
	{
		protected override async ETTask Run(Session session, C2G_GetPlayerCache request, G2C_GetPlayerCache response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = request.PlayerId;
			PlayerModelType playerModelType = (PlayerModelType)request.PlayerModelType;

			bool forceReGet = playerId == player.Id;
			Entity entity = await ET.Server.PlayerCacheHelper.GetPlayerModel(session.DomainScene(), playerId, playerModelType, forceReGet);

			response.PlayerModelComponentBytes = entity.ToBson();

			await ETTask.CompletedTask;
		}
	}
}