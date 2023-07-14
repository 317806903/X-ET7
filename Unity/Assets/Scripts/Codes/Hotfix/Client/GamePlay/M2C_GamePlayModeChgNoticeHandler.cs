using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_GamePlayModeChgNoticeHandler : AMHandler<M2C_GamePlayModeChgNotice>
	{
		protected override async ETTask Run(Session session, M2C_GamePlayModeChgNotice message)
		{
			Scene clientScene = session.DomainScene();
			Scene currentScene = session.DomainScene().CurrentScene();
			while (currentScene == null || currentScene.IsDisposed)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				currentScene = session.DomainScene().CurrentScene();
			}

			GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
			while (gamePlayComponent == null || gamePlayComponent.IsDisposed)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
			}
			gamePlayComponent.RemoveComponent<GamePlayTowerDefenseComponent>();
			
			GamePlayModeComponent gamePlayModeComponent = MongoHelper.Deserialize<Entity>(message.GamePlayModeInfo) as GamePlayModeComponent;
			gamePlayComponent.AddComponent(gamePlayModeComponent);
			if (message.Components != null)
			{
				foreach (byte[] bytes in message.Components)
				{
					Entity entity = MongoHelper.Deserialize<Entity>(bytes);
					gamePlayModeComponent.AddComponent(entity);
				}
			}
			
			EventSystem.Instance.Publish(clientScene, new EventType.GamePlayChg());

			await ETTask.CompletedTask;
		}
	}
}
