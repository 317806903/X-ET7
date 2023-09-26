using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_GamePlayModeChgNoticeHandler : AMHandler<M2C_GamePlayModeChgNotice>
	{
		protected override async ETTask Run(Session session, M2C_GamePlayModeChgNotice message)
		{
			Log.Debug($"M2C_GamePlayModeChgNotice 11");
			Scene clientScene = session.DomainScene();
			Scene currentScene = session.DomainScene().CurrentScene();
			while (currentScene == null || currentScene.IsDisposed)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				currentScene = session.DomainScene().CurrentScene();
			}

			Log.Debug($"M2C_GamePlayModeChgNotice 22");

			GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
			while (gamePlayComponent == null || gamePlayComponent.IsDisposed)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
			}

			Log.Debug($"M2C_GamePlayModeChgNotice 33");
			gamePlayComponent.RemoveComponent<GamePlayTowerDefenseComponent>();
			gamePlayComponent.RemoveComponent<GamePlayPKComponent>();

			Entity gamePlayModeComponent = MongoHelper.Deserialize<Entity>(message.GamePlayModeInfo);
			gamePlayComponent.AddComponent(gamePlayModeComponent);
			if (message.Components != null)
			{
				foreach (byte[] bytes in message.Components)
				{
					Entity entity = MongoHelper.Deserialize<Entity>(bytes);
					gamePlayModeComponent.AddComponent(entity);
				}
			}

			Log.Debug($"M2C_GamePlayModeChgNotice 44");
			EventSystem.Instance.Publish(clientScene, new EventType.GamePlayChg());

			Log.Debug($"M2C_GamePlayModeChgNotice 55");
			await ETTask.CompletedTask;
		}
	}
}
