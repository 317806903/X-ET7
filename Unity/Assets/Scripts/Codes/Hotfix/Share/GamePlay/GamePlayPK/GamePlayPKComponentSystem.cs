using ET.Ability;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayPkComponentBase))]
    [FriendOf(typeof(Unit))]
    public static class GamePlayPKComponentSystem
	{
		[ObjectSystem]
		public class GamePlayPKComponentAwakeSystem : AwakeSystem<GamePlayPkComponentBase>
		{
			protected override void Awake(GamePlayPkComponentBase self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayPKComponentDestroySystem : DestroySystem<GamePlayPkComponentBase>
		{
			protected override void Destroy(GamePlayPkComponentBase self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayPKComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayPkComponentBase>
		{
			protected override void FixedUpdate(GamePlayPkComponentBase self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this GamePlayPkComponentBase self, float fixedDeltaTime)
		{
		}

		public static async ETTask Init(this GamePlayPkComponentBase self, long ownerPlayerId, string gamePlayModeCfgId, RoomTypeInfo roomTypeInfo)
		{
			await ETTask.CompletedTask;
			self.gamePlayModeCfgId = gamePlayModeCfgId;
			self.roomTypeInfo = roomTypeInfo;
			self.ownerPlayerId = ownerPlayerId;

		}

		/// <summary>
		/// 处理阵营关系
		/// </summary>
		/// <param name="self"></param>
		public static void DealFriendTeamFlagType(this GamePlayPkComponentBase self)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.DealFriendTeamFlag(null, false, true);
		}

		public static async ETTask DoReadyForBattle(this GamePlayPkComponentBase self)
		{
			await self.Start();
			await ETTask.CompletedTask;
		}

		public static async ETTask Start(this GamePlayPkComponentBase self)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.Start();

			self.DealFriendTeamFlagType();
			self.NoticeToClientAll();
			await ETTask.CompletedTask;
		}

		public static async ETTask TransToGameSuccess(this GamePlayPkComponentBase self)
		{
			//self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameSuccess;

			await self.GameEnd();

			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			await gamePlayComponent.GameEnd();

			self.NoticeToClientAll();
		}

		public static void DealUnitBeKill(this GamePlayPkComponentBase self, Unit attackerUnit, Unit beKillUnit)
		{
		}

		public static async ETTask GameEnd(this GamePlayPkComponentBase self)
		{
			await ETTask.CompletedTask;
		}

	}
}