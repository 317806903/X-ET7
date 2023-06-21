using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_PingHandler : AMRpcHandler<C2G_Ping, G2C_Ping>
	{
		protected override async ETTask Run(Session session, C2G_Ping request, G2C_Ping response)
		{
			response.Time = TimeHelper.ServerNow();
            SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
			if (sessionPlayerComponent != null)
            {
                Player player = sessionPlayerComponent.Player;
				if (player != null)
                {
                    ActorLocationSenderComponent.Instance?.Get(LocationType.Player).ResetTime(player.Id);
                }
            }
            await ETTask.CompletedTask;
		}
	}
}