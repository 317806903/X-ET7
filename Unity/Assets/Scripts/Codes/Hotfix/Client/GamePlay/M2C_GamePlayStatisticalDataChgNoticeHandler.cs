using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_GamePlayStatisticalDataChgNoticeHandler : AMHandler<M2C_GamePlayStatisticalDataChgNotice>
	{
		protected override async ETTask Run(Session session, M2C_GamePlayStatisticalDataChgNotice message)
		{
			//Log.Debug($"zpb M2C_GamePlayStatisticalDataChgNotice 11");
			Scene clientScene = session.DomainScene();
			Scene currentScene = session.DomainScene().CurrentScene();
			while (currentScene == null || currentScene.IsDisposed)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				if (clientScene.IsDisposed)
				{
					return;
				}
				currentScene = session.DomainScene().CurrentScene();
			}

			GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
			while (gamePlayComponent == null || gamePlayComponent.IsDisposed)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				if (currentScene.IsDisposed)
				{
					return;
				}
				gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
			}

			GamePlayStatisticalDataManagerComponent gamePlayStatisticalDataManagerComponent = gamePlayComponent.GetComponent<GamePlayStatisticalDataManagerComponent>();
			if (gamePlayStatisticalDataManagerComponent == null)
			{
				gamePlayStatisticalDataManagerComponent = gamePlayComponent.AddComponent<GamePlayStatisticalDataManagerComponent>();
			}

			long myPlayerId = PlayerStatusHelper.GetMyPlayerId(currentScene);

			Entity component = MongoHelper.Deserialize<Entity>(message.GamePlayStatisticalDataComponent);
			gamePlayStatisticalDataManagerComponent.RemoveChild(myPlayerId);
			gamePlayStatisticalDataManagerComponent.AddChild(component);

			//Log.Debug($"zpb M2C_GamePlayStatisticalDataChgNotice end");
			await ETTask.CompletedTask;
		}
	}
}
