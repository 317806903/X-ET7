using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Rank)]
	public class G2R_SetPlayerRankHandler : AMActorRpcHandler<Scene, G2R_SetPlayerRank, R2G_SetPlayerRank>
	{
		protected override async ETTask Run(Scene scene, G2R_SetPlayerRank request, R2G_SetPlayerRank response)
		{
			long playerId = request.PlayerId;
			RankType rankType = (RankType)request.RankType;
			long score = request.Score;
			int killNum = request.KillNum;
			await ET.Server.RankHelper.ResetPlayerRank(scene, playerId, rankType, score, killNum);

			await ETTask.CompletedTask;
		}
	}
}