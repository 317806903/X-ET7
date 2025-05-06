using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Season)]
	public class G2S_GetSeasonComponentHandler : AMActorRpcHandler<Scene, G2S_GetSeasonComponent, S2G_GetSeasonComponent>
	{
		protected override async ETTask Run(Scene scene, G2S_GetSeasonComponent request, S2G_GetSeasonComponent response)
		{
			SeasonManagerComponent seasonManagerComponent = scene.GetComponent<SeasonManagerComponent>();
			SeasonComponent seasonComponent = seasonManagerComponent.SeasonComponent;
			if (seasonComponent == null)
			{
				return;
			}
			byte[] bytes = seasonComponent.ToBson();
			response.ComponentBytes = bytes;

			await ETTask.CompletedTask;
		}
	}
}