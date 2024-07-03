using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.PlayerCache)]
	public class S2P_ChgSeasonHandler : AMActorRpcHandler<Scene, S2P_ChgSeason, P2S_ChgSeason>
	{
		protected override async ETTask Run(Scene scene, S2P_ChgSeason request, P2S_ChgSeason response)
		{
			await ET.Server.PlayerCacheLocalHelper.RecordWhenSeasonFinished(scene, request.SeasonId);

			await ETTask.CompletedTask;
		}
	}
}