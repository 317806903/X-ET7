using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_GamePlayCoinChgNoticeHandler : AMHandler<M2C_GamePlayCoinChgNotice>
	{
		protected override async ETTask Run(Session session, M2C_GamePlayCoinChgNotice message)
		{
			//Log.Debug($"zpb M2C_GamePlayCoinChgNotice 11");
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
			if (currentScene == null || currentScene.IsDisposed)
			{
				return;
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

			GetCoinType getCoinType = (GetCoinType)message.GetCoinType;
			Dictionary<string, float> myCoinListOld = null;

			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			if (gamePlayPlayerListComponent != null)
			{
				long myPlayerId = PlayerStatusHelper.GetMyPlayerId(clientScene);
				gamePlayPlayerListComponent.playerId2CoinList.TryGetDic(myPlayerId, out myCoinListOld);
				gamePlayComponent.RemoveComponent<GamePlayPlayerListComponent>();
			}

			Entity component = MongoHelper.Deserialize<Entity>(message.GamePlayPlayerListComponent);
			//Log.Error($"---zpb List 111 {gamePlayPlayerListComponent}");
			gamePlayComponent.AddComponent(component);
			//Log.Error($"---zpb List 222 {component}");

			if (getCoinType == GetCoinType.Normal)
			{
				EventType.GamePlayCoinChg _GamePlayCoinChg = new()
				{
					getCoinType = getCoinType,
				};
				EventSystem.Instance.Publish(clientScene, _GamePlayCoinChg);
			}
			else
			{
				GamePlayPlayerListComponent gamePlayPlayerListComponentNew = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();

				long myPlayerId = PlayerStatusHelper.GetMyPlayerId(clientScene);
				gamePlayPlayerListComponentNew.playerId2CoinList.TryGetDic(myPlayerId, out Dictionary<string, float> myCoinListNew);

				Dictionary<string, float> myCoinListChg = new();
				if (myCoinListOld == null)
				{
					myCoinListChg = myCoinListNew;
				}
				else
				{
					foreach (var coinList in myCoinListNew)
					{
						if (myCoinListOld.TryGetValue(coinList.Key, out float oldValue) == false)
						{
							oldValue = 0;
						}
						if (coinList.Value != oldValue)
						{
							myCoinListChg[coinList.Key] = coinList.Value - oldValue;
						}
					}
				}
				EventType.GamePlayCoinChg _GamePlayCoinChg = new()
				{
					getCoinType = getCoinType,
					myCoinListChg = myCoinListChg,
				};
				EventSystem.Instance.Publish(clientScene, _GamePlayCoinChg);
			}

			//Log.Debug($"zpb M2C_GamePlayCoinChgNotice end");
			await ETTask.CompletedTask;
		}
	}
}
