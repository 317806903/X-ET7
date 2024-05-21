using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class M2G_GetPlayerSessionInfoHandler : AMActorRpcHandler<Player, M2G_GetPlayerSessionInfo, G2M_GetPlayerSessionInfo>
	{
		protected override async ETTask Run(Player player, M2G_GetPlayerSessionInfo request, G2M_GetPlayerSessionInfo response)
		{
			PlayerSessionComponent playerSessionComponent = player.GetComponent<ET.Server.PlayerSessionComponent>();
			if (playerSessionComponent == null)
			{
				response.Fps = 0;
				response.PingTime = 0;
				return;
			}
			Session session = playerSessionComponent.Session;
			if (session == null)
			{
				response.Fps = 0;
				response.PingTime = 0;
				return;
			}
			SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
			if (sessionPlayerComponent == null)
			{
				response.Fps = 0;
				response.PingTime = 0;
				return;
			}

			response.Fps = sessionPlayerComponent.fps;
			response.PingTime = sessionPlayerComponent.pingTime;

			await ETTask.CompletedTask;
		}
	}
}