using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ClearPlayerRankWhenDebugHandler : AMHandler<C2G_ClearPlayerRankWhenDebug>
	{
		protected override async ETTask Run(Session session, C2G_ClearPlayerRankWhenDebug message)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = message.PlayerId;
			Scene scene = session.DomainScene();

			RankType rankType = (RankType)message.RankType;
			if (rankType == RankType.EndlessChallenge)
			{
				PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Server.PlayerCacheHelper.GetPlayerBaseInfoByPlayerId(scene, playerId, true);

				playerBaseInfoComponent.EndlessChallengeScore = 0;
				playerBaseInfoComponent.EndlessChallengeKillNum = 0;
				await ET.Server.PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.BaseInfo,
					new() { "EndlessChallengeScore", "EndlessChallengeKillNum"}, PlayerModelChgType.PlayerBaseInfo_EndlessChallengeScore);
				await ET.Server.PlayerCacheHelper.SavePlayerRank(scene, playerId, RankType.EndlessChallenge,
					playerBaseInfoComponent.EndlessChallengeScore, playerBaseInfoComponent.EndlessChallengeKillNum);
			}
            await ETTask.CompletedTask;
		}
	}
}