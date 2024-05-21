using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_PingHandler : AMRpcHandler<C2G_Ping, G2C_Ping>
	{
		protected override async ETTask Run(Session session, C2G_Ping request, G2C_Ping response)
		{
			int fps = request.Fps;
			long pingTime = request.PingTime;

			response.Time = TimeHelper.ServerNow();
			//Log.Debug($"zpb fps[{fps}] pingTime[{pingTime}]");
            SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
			if (sessionPlayerComponent != null)
			{
				sessionPlayerComponent.fps = fps;
				sessionPlayerComponent.pingTime = pingTime;
                Player player = sessionPlayerComponent.Player;
				if (player != null)
				{
					long playerId = player.Id;
                    ActorLocationSenderComponent.Instance?.Get(LocationType.Player).ResetTime(playerId);
                }
            }
            await ETTask.CompletedTask;
		}
	}
}