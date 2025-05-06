using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Rank)]
	public class G2R_GetRankHandler : AMActorRpcHandler<Scene, G2R_GetRank, R2G_GetRank>
	{
		protected override async ETTask Run(Scene scene, G2R_GetRank request, R2G_GetRank response)
		{
			long playerId = request.PlayerId;
			RankType rankType = (RankType)request.RankType;
			RankShowComponent rankShowComponent = await ET.Server.RankHelper.GetRankShow(scene, playerId, rankType);
			response.RankShowComponentBytes = rankShowComponent.ToBson();

			await ETTask.CompletedTask;
		}
	}
}