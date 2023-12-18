using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Rank)]
	public class G2R_GetRankedMoreThanHandler : AMActorRpcHandler<Scene, G2R_GetRankedMoreThan, R2G_GetRankedMoreThan>
	{
		protected override async ETTask Run(Scene scene, G2R_GetRankedMoreThan request, R2G_GetRankedMoreThan response)
		{
			long playerId = request.PlayerId;
			RankType rankType = (RankType)request.RankType;
			long score = request.Score;
			int rankedMoreThan = await ET.Server.RankHelper.GetRankedMoreThan(scene, rankType, score);
			response.RankedMoreThan = rankedMoreThan;

			await ETTask.CompletedTask;
		}
	}
}