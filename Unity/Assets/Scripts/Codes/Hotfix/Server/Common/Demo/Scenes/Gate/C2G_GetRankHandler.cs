using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_GetRankHandler : AMRpcHandler<C2G_GetRank, G2C_GetRank>
	{
		protected override async ETTask Run(Session session, C2G_GetRank request, G2C_GetRank response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			int RankType = request.RankType;

			StartSceneConfig rankSceneConfig = StartSceneConfigCategory.Instance.GetRankManager(session.DomainZone());

			R2G_GetRank _R2G_GetRank = (R2G_GetRank) await ActorMessageSenderComponent.Instance.Call(rankSceneConfig.InstanceId, new G2R_GetRank()
			{
				PlayerId = playerId,
				RankType = RankType,
			});

			response.Error = _R2G_GetRank.Error;
			response.Message = _R2G_GetRank.Message;
			response.RankShowComponentBytes = _R2G_GetRank.RankShowComponentBytes;

			await ETTask.CompletedTask;
		}
	}
}