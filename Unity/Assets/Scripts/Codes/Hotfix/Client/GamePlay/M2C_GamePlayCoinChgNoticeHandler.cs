using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_GamePlayCoinChgNoticeHandler : AMHandler<M2C_GamePlayCoinChgNotice>
	{
		protected override async ETTask Run(Session session, M2C_GamePlayCoinChgNotice message)
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
			gamePlayComponent.RemoveComponent<GamePlayPlayerListComponent>();
			
			GamePlayPlayerListComponent gamePlayPlayerListComponent = MongoHelper.Deserialize<Entity>(message.GamePlayPlayerListComponent) as GamePlayPlayerListComponent;
			gamePlayComponent.AddComponent(gamePlayPlayerListComponent);
			
			EventSystem.Instance.Publish(clientScene, new EventType.GamePlayCoinChg());

			await ETTask.CompletedTask;
		}
	}
}
