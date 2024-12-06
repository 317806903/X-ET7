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
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GamePlay, 1))
			{
				await Deal(clientScene, message);
			}
		}

		protected async ETTask Deal(Scene clientScene, M2C_GamePlayModeChgNotice message)
		{
			Log.Debug($"zpb M2C_GamePlayModeChgNotice 11 message.RpcId={message.RpcId}");
			Scene currentScene = clientScene.CurrentScene();
			while (currentScene == null || currentScene.IsDisposed)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				if (clientScene.IsDisposed)
				{
					return;
				}
				currentScene = clientScene.CurrentScene();
			}

			Log.Debug($"zpb M2C_GamePlayModeChgNotice 22 message.RpcId={message.RpcId}");

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

			Log.Debug($"zpb M2C_GamePlayModeChgNotice 33 message.RpcId={message.RpcId}");
			gamePlayComponent.RemoveComponent<GamePlayTowerDefenseComponent>();
			gamePlayComponent.RemoveComponent<GamePlayPkComponent>();

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

			Log.Debug($"zpb M2C_GamePlayModeChgNotice 44");
			EventSystem.Instance.Publish(clientScene, new EventType.GamePlayChg());

			Log.Debug($"zpb M2C_GamePlayModeChgNotice 55");
			await ETTask.CompletedTask;
		}
	}
}
