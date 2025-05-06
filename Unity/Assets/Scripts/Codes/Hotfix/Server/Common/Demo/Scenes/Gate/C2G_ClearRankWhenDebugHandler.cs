using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ClearRankWhenDebugHandler : AMHandler<C2G_ClearRankWhenDebug>
	{
		protected override async ETTask Run(Session session, C2G_ClearRankWhenDebug message)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			Scene scene = session.DomainScene();

			RankType rankType = (RankType)message.RankType;

			StartSceneConfig rankSceneConfig = StartSceneConfigCategory.Instance.GetRankManager(scene.DomainZone());

			R2G_ClearRankWhenDebug _R2G_ClearRankWhenDebug = (R2G_ClearRankWhenDebug) await ActorMessageSenderComponent.Instance.Call(rankSceneConfig.InstanceId, new G2R_ClearRankWhenDebug()
			{
				RankType = (int)rankType,
			});

            await ETTask.CompletedTask;
		}
	}
}