using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_GamePlayChgNoticeHandler : AMHandler<M2C_GamePlayChgNotice>
	{
		protected override async ETTask Run(Session session, M2C_GamePlayChgNotice message)
		{
			Log.Debug($"M2C_GamePlayChgNotice 11");
			Scene clientScene = session.DomainScene();
			Scene currentScene = session.DomainScene().CurrentScene();
			while (currentScene == null || currentScene.IsDisposed)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				currentScene = session.DomainScene().CurrentScene();
			}
			Log.Debug($"M2C_GamePlayChgNotice 22");
			currentScene.RemoveComponent<GamePlayComponent>();
			Log.Debug($"M2C_GamePlayChgNotice 22 1");

			Entity gamePlayComponent = MongoHelper.Deserialize<Entity>(message.GamePlayInfo);
			currentScene.AddComponent(gamePlayComponent);
			if (message.Components != null)
			{
				foreach (byte[] bytes in message.Components)
				{
					Entity entity = MongoHelper.Deserialize<Entity>(bytes);
					gamePlayComponent.AddComponent(entity);
				}
			}

			Log.Debug($"M2C_GamePlayChgNotice 33");
			await ETTask.CompletedTask;
		}
	}
}
