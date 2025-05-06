using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_SetMyRankScoreWhenDebugHandler : AMHandler<C2G_SetMyRankScoreWhenDebug>
	{
		protected override async ETTask Run(Session session, C2G_SetMyRankScoreWhenDebug message)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			Scene scene = session.DomainScene();

			RankType rankType = (RankType)message.RankType;
			if (rankType == RankType.EndlessChallenge)
			{
				PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Server.PlayerCacheHelper.GetPlayerBaseInfoByPlayerId(scene, playerId, true);

				playerBaseInfoComponent.EndlessChallengeScore = message.Score;
				playerBaseInfoComponent.EndlessChallengeKillNum = message.KillNum;
				await ET.Server.PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.BaseInfo,
					new() { "EndlessChallengeScore", "EndlessChallengeKillNum"}, PlayerModelChgType.PlayerBaseInfo_EndlessChallengeScore);
				await ET.Server.PlayerCacheHelper.SavePlayerRank(scene, playerId, RankType.EndlessChallenge,
					playerBaseInfoComponent.EndlessChallengeScore, playerBaseInfoComponent.EndlessChallengeKillNum);
			}
            await ETTask.CompletedTask;
		}
	}
}