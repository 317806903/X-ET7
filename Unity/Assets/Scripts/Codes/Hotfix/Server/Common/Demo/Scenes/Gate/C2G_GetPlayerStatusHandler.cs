using System;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_GetPlayerStatusHandler : AMRpcHandler<C2G_GetPlayerStatus, G2C_GetPlayerStatus>
	{
		protected override async ETTask Run(Session session, C2G_GetPlayerStatus request, G2C_GetPlayerStatus response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;

			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();

			response.PlayerStatusComponentBytes = playerStatusComponent.ToBson();
			await ETTask.CompletedTask;
		}
	}
}