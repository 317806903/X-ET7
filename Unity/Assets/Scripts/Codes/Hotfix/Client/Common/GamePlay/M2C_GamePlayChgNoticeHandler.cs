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
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GamePlay, 0))
			{
				await Deal(clientScene, message);
			}
		}

		protected async ETTask Deal(Scene clientScene, M2C_GamePlayChgNotice message)
		{
			Log.Debug($"zpb M2C_GamePlayChgNotice 11 message.RpcId={message.RpcId}");
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
			Log.Debug($"zpb M2C_GamePlayChgNotice 22 message.RpcId={message.RpcId}");
			currentScene.RemoveComponent<GamePlayComponent>();
			//Log.Debug($"zpb M2C_GamePlayChgNotice 22 1");

			Entity gamePlayComponent = MongoHelper.Deserialize<Entity>(message.GamePlayInfo);
			currentScene.AddComponent(gamePlayComponent);
			if (message.Components != null)
			{
				foreach (byte[] bytes in message.Components)
				{
					Entity entity = MongoHelper.Deserialize<Entity>(bytes);
					gamePlayComponent.AddComponent(entity);
					//Log.Debug($"zpb M2C_GamePlayChgNotice 22 2 {entity}");
				}
			}

			//Log.Debug($"zpb M2C_GamePlayChgNotice 33");
			await ETTask.CompletedTask;
		}
	}
}
