using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_GetRankedMoreThanHandler : AMRpcHandler<C2G_GetRankedMoreThan, G2C_GetRankedMoreThan>
	{
		protected override async ETTask Run(Session session, C2G_GetRankedMoreThan request, G2C_GetRankedMoreThan response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			int RankType = request.RankType;
			long Score = request.Score;

			StartSceneConfig rankSceneConfig = StartSceneConfigCategory.Instance.GetRankManager(session.DomainZone());

			R2G_GetRankedMoreThan _R2G_GetRankedMoreThan = (R2G_GetRankedMoreThan) await ActorMessageSenderComponent.Instance.Call(rankSceneConfig.InstanceId, new G2R_GetRankedMoreThan()
			{
				PlayerId = playerId,
				RankType = RankType,
				Score = Score,
			});

			response.Error = _R2G_GetRankedMoreThan.Error;
			response.Message = _R2G_GetRankedMoreThan.Message;
			response.RankedMoreThan = _R2G_GetRankedMoreThan.RankedMoreThan;
			response.Rank = _R2G_GetRankedMoreThan.Rank;

			await ETTask.CompletedTask;
		}
	}
}