using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_GamePlayChgNoticeHandler : AMHandler<M2C_GamePlayChgNotice>
	{
		protected override async ETTask Run(Session session, M2C_GamePlayChgNotice message)
		{
			Scene clientScene = session.DomainScene();
			Scene currentScene = session.DomainScene().CurrentScene();
			while (currentScene == null || currentScene.IsDisposed)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				currentScene = session.DomainScene().CurrentScene();
			}
			currentScene.RemoveComponent<GamePlayComponent>();
			
			GamePlayComponent gamePlayComponent = MongoHelper.Deserialize<Entity>(message.GamePlayInfo) as GamePlayComponent;
			currentScene.AddComponent(gamePlayComponent);
			if (message.Components != null)
			{
				foreach (byte[] bytes in message.Components)
				{
					Entity entity = MongoHelper.Deserialize<Entity>(bytes);
					gamePlayComponent.AddComponent(entity);
				}
			}
			
			EventSystem.Instance.Publish(clientScene, new EventType.GamePlayChg());

			await ETTask.CompletedTask;
		}
	}
}
