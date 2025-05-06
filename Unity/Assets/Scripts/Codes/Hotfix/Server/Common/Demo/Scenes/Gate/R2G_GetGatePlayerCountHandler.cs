using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class R2G_GetGatePlayerCountHandler : AMActorRpcHandler<Scene, R2G_GetGatePlayerCount, G2R_GetGatePlayerCount>
	{
		protected override async ETTask Run(Scene scene, R2G_GetGatePlayerCount request, G2R_GetGatePlayerCount response)
		{
			if (scene.GetComponent<PlayerComponent>() != null)
			{
				response.PlayerCount = scene.GetComponent<PlayerComponent>().ChildrenCount();
			}
			else
			{
				response.PlayerCount = 0;
			}

			await ETTask.CompletedTask;
		}
	}
}