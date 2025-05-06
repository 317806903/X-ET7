using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_GetSeasonComponentHandler : AMRpcHandler<C2G_GetSeasonComponent, G2C_GetSeasonComponent>
	{
		protected override async ETTask Run(Session session, C2G_GetSeasonComponent request, G2C_GetSeasonComponent response)
		{
			SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
			if (sessionPlayerComponent == null)
			{
				Log.Error($"---zpb C2G_GetSeasonComponent sessionPlayerComponent == null");
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "C2G_GetSeasonComponent sessionPlayerComponent == null";
				return;
			}
			Player player = sessionPlayerComponent.Player;
			if (player == null)
			{
				Log.Error($"---zpb C2G_GetSeasonComponent player == null");
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "C2G_GetSeasonComponent player == null";
				return;
			}

			SeasonComponent seasonComponent = await ET.Server.SeasonHelper.GetSeasonComponent(session.DomainScene(), false);
			response.ComponentBytes = seasonComponent.ToBson();

			await ETTask.CompletedTask;
		}
	}
}