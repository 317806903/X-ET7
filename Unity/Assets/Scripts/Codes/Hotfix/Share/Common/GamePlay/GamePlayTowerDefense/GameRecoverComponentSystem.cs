using ET.Ability;
using System;
using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(GameRecoverComponent))]
    public static class GameRecoverComponentSystem
	{
		[ObjectSystem]
		public class GameRecoverComponentAwakeSystem : AwakeSystem<GameRecoverComponent>
		{
			protected override void Awake(GameRecoverComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GameRecoverComponentDestroySystem : DestroySystem<GameRecoverComponent>
		{
			protected override void Destroy(GameRecoverComponent self)
			{
			}
		}

		public static void Init(this GameRecoverComponent self, int recoverTimeoutTime, int recoverFreeTimes, int recoverByWatchAdTimes, int recoverCostArcadeCoinTimes, int recoverCostArcadeCoinNum, int recoverAddHp, int recoverAddGold)
		{
			self.recoverTimeoutTime = recoverTimeoutTime;
			self.recoverFreeTimes = recoverFreeTimes;
			self.recoverByWatchAdTimes = recoverByWatchAdTimes;
			self.recoverCostArcadeCoinTimes = recoverCostArcadeCoinTimes;
			self.recoverCostArcadeCoinNumOrg = recoverCostArcadeCoinNum;
			self.recoverAddHp = recoverAddHp;
			self.recoverAddGold = recoverAddGold;
		}

		public static GamePlayComponent GetGamePlay(this GameRecoverComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			GamePlayComponent gamePlayComponent = gamePlayTowerDefenseComponent.GetParent<GamePlayComponent>();
			return gamePlayComponent;
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this GameRecoverComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			return gamePlayTowerDefenseComponent;
		}

		public static bool ChkNeedToRecover(this GameRecoverComponent self)
		{
			if (self.recoverFreeTimes > 0)
			{
				GameRecoverOnceComponent gameRecoverOnceComponent = self.GetGamePlayTowerDefense().AddComponent<GameRecoverOnceComponent>();
				gameRecoverOnceComponent.Init(self.recoverTimeoutTime, GameRecoverType.Free, 0);
				self.recoverFreeTimes--;
				return true;
			}

			if (self.recoverByWatchAdTimes > 0)
			{
				GameRecoverOnceComponent gameRecoverOnceComponent = self.GetGamePlayTowerDefense().AddComponent<GameRecoverOnceComponent>();
				gameRecoverOnceComponent.Init(self.recoverTimeoutTime, GameRecoverType.ByWatchAd, 0);
				self.recoverByWatchAdTimes--;
				return true;
			}

			if (self.recoverCostArcadeCoinTimes > 0)
			{
				GameRecoverOnceComponent gameRecoverOnceComponent = self.GetGamePlayTowerDefense().AddComponent<GameRecoverOnceComponent>();
				gameRecoverOnceComponent.Init(self.recoverTimeoutTime, GameRecoverType.CostArcadeCoin, self.recoverCostArcadeCoinNumOrg);
				self.recoverCostArcadeCoinTimes--;
				return true;
			}

			return false;
		}

	}
}