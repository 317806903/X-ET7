using ET.Ability;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayPkComponent))]
    [FriendOf(typeof(Unit))]
    public static class GamePlayPKComponentSystem
	{
		[ObjectSystem]
		public class GamePlayPKComponentAwakeSystem : AwakeSystem<GamePlayPkComponent>
		{
			protected override void Awake(GamePlayPkComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayPKComponentDestroySystem : DestroySystem<GamePlayPkComponent>
		{
			protected override void Destroy(GamePlayPkComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayPKComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayPkComponent>
		{
			protected override void FixedUpdate(GamePlayPkComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this GamePlayPkComponent self, float fixedDeltaTime)
		{
		}

		public static async ETTask Init(this GamePlayPkComponent self, long ownerPlayerId, string gamePlayModeCfgId, RoomTypeInfo roomTypeInfo)
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
		public static void DealFriendTeamFlagType(this GamePlayPkComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.DealFriendTeamFlag(null, false, true);
		}

		public static async ETTask DoReadyForBattle(this GamePlayPkComponent self)
		{
			await self.Start();
			await ETTask.CompletedTask;
		}

		public static async ETTask Start(this GamePlayPkComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.Start();

			self.DealFriendTeamFlagType();
			self.NoticeToClientAll();
			await ETTask.CompletedTask;
		}

		public static async ETTask TransToGameSuccess(this GamePlayPkComponent self)
		{
			//self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameSuccess;

			await self.GameEnd();

			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			await gamePlayComponent.GameEnd();

			self.NoticeToClientAll();
		}

		public static void DealUnitBeKill(this GamePlayPkComponent self, Unit attackerUnit, Unit beKillUnit)
		{
		}

		public static void DealUnitCallActor(this GamePlayPkComponent self, Unit unit, Unit beCallUnit)
		{
		}

		public static async ETTask GameEnd(this GamePlayPkComponent self)
		{
			await ETTask.CompletedTask;
		}

	}
}