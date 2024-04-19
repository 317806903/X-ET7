using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Rank)]
	public class G2R_ClearRankWhenDebugHandler : AMActorRpcHandler<Scene, G2R_ClearRankWhenDebug, R2G_ClearRankWhenDebug>
	{
		protected override async ETTask Run(Scene scene, G2R_ClearRankWhenDebug request, R2G_ClearRankWhenDebug response)
		{
			RankType rankType = (RankType)request.RankType;
			HashSet<long> allPlayer = await ET.Server.RankHelper.ClearRankWhenDebug(scene, rankType);

			if (rankType == RankType.EndlessChallenge)
			{
				foreach (long playerId in allPlayer)
				{
					PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Server.PlayerCacheHelper.GetPlayerBaseInfoByPlayerId(scene, playerId, true);

					playerBaseInfoComponent.EndlessChallengeScore = 0;
					playerBaseInfoComponent.EndlessChallengeKillNum = 0;
					await ET.Server.PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.BaseInfo,
						new() { "EndlessChallengeScore", "EndlessChallengeKillNum"}, PlayerModelChgType.PlayerBaseInfo_EndlessChallengeScore);
					await ET.Server.PlayerCacheHelper.SavePlayerRank(scene, playerId, RankType.EndlessChallenge,
						playerBaseInfoComponent.EndlessChallengeScore, playerBaseInfoComponent.EndlessChallengeKillNum);
				}
			}
			await ETTask.CompletedTask;
		}
	}
}