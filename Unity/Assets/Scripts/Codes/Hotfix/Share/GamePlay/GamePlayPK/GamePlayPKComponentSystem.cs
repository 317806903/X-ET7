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
				if (self.DomainScene().SceneType != SceneType.Map)
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

		public static void Init(this GamePlayPKComponent self, long ownerPlayerId, string gamePlayModeCfgId)
		{
			self.gamePlayModeCfgId = gamePlayModeCfgId;
			self.ownerPlayerId = ownerPlayerId;

			self.Start();
		}

		/// <summary>
		/// 处理阵营关系
		/// </summary>
		/// <param name="self"></param>
		public static void DealFriendTeamFlagType(this GamePlayPKComponent self)
		{
			ListComponent<TeamFlagType> teamFlagTypes = ListComponent<TeamFlagType>.Create();
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.DealFriendTeamFlag(null, false, true);
		}

		public static void Start(this GamePlayPKComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.Start();

			self.DealFriendTeamFlagType();
			self.NoticeToClientAll();
		}

		public static void TransToGameSuccess(this GamePlayPKComponent self)
		{
			//self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameSuccess;

			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.GameEnd();

			self.NoticeToClientAll();
		}

		public static void DealUnitBeKill(this GamePlayPKComponent self, Unit attackerUnit, Unit beKillUnit)
		{
		}

	}
}