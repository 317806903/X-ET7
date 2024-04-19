using ET.Ability;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayPKComponent))]
    [FriendOf(typeof(Unit))]
    public static class GamePlayPKComponentSystem
	{
		[ObjectSystem]
		public class GamePlayPKComponentAwakeSystem : AwakeSystem<GamePlayPKComponent>
		{
			protected override void Awake(GamePlayPKComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayPKComponentDestroySystem : DestroySystem<GamePlayPKComponent>
		{
			protected override void Destroy(GamePlayPKComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayPKComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayPKComponent>
		{
			protected override void FixedUpdate(GamePlayPKComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this GamePlayPKComponent self, float fixedDeltaTime)
		{
		}

		// public static GamePlayComponent GetGamePlay(this GamePlayPKComponent self)
		// {
		// 	GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
		// 	return gamePlayComponent;
		// }
		//
		// public static List<long> GetPlayerList(this GamePlayPKComponent self)
		// {
		// 	GamePlayComponent gamePlayComponent = self.GetGamePlay();
		// 	return gamePlayComponent.GetPlayerList();
		// }
		//
		// public static void NoticeToClientAll(this GamePlayPKComponent self)
		// {
		// 	List<long> playerList = self.GetPlayerList();
		// 	for (int i = 0; i < playerList.Count; i++)
		// 	{
		// 		self.NoticeToClient(playerList[i]);
		// 	}
		// }
		//
		// public static void NoticeToClient(this GamePlayPKComponent self, long playerId)
		// {
		// 	EventType.WaitNoticeGamePlayModeToClient _WaitNoticeGamePlayModeChgToClient = new ()
		// 	{
		// 		playerId = playerId,
		// 		gamePlayComponent = self.GetGamePlay(),
		// 	};
		// 	EventSystem.Instance.Publish(self.DomainScene(), _WaitNoticeGamePlayModeChgToClient);
		// }

		public static async ETTask Init(this GamePlayPKComponent self, long ownerPlayerId, string gamePlayModeCfgId)
		{
			await ETTask.CompletedTask;
			self.gamePlayModeCfgId = gamePlayModeCfgId;
			self.ownerPlayerId = ownerPlayerId;

			self.Start().Coroutine();
		}

		/// <summary>
		/// 处理阵营关系
		/// </summary>
		/// <param name="self"></param>
		public static void DealFriendTeamFlagType(this GamePlayPKComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.DealFriendTeamFlag(null, false, true);
		}

		public static async ETTask Start(this GamePlayPKComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.Start();

			self.DealFriendTeamFlagType();
			self.NoticeToClientAll();
			await ETTask.CompletedTask;
		}

		public static async ETTask TransToGameSuccess(this GamePlayPKComponent self)
		{
			//self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameSuccess;

			await self.GameEnd();

			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			await gamePlayComponent.GameEnd();

			self.NoticeToClientAll();
		}

		public static void DealUnitBeKill(this GamePlayPKComponent self, Unit attackerUnit, Unit beKillUnit)
		{
		}

		public static async ETTask GameEnd(this GamePlayPKComponent self)
		{
			await ETTask.CompletedTask;
		}

	}
}