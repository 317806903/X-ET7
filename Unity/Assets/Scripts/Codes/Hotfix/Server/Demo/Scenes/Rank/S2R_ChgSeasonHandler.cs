using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Rank)]
	public class S2R_ChgSeasonHandler : AMActorRpcHandler<Scene, S2R_ChgSeason, R2S_ChgSeason>
	{
		protected override async ETTask Run(Scene scene, S2R_ChgSeason request, R2S_ChgSeason response)
		{
			int seasonId = request.SeasonId;
			RankType rankType = RankType.EndlessChallenge;
			await ET.Server.RankHelper.RecordWhenSeasonFinished(scene, rankType, seasonId);

			await ETTask.CompletedTask;
		}
	}
}