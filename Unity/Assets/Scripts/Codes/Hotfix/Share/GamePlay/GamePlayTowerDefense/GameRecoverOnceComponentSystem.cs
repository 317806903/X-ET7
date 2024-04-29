using ET.Ability;
using System;
using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(GameRecoverOnceComponent))]
    public static class GameRecoverOnceComponentSystem
	{
		[ObjectSystem]
		public class GameRecoverOnceComponentAwakeSystem : AwakeSystem<GameRecoverOnceComponent>
		{
			protected override void Awake(GameRecoverOnceComponent self)
			{
				self.playerRecoverStatusDic = new();
			}
		}

		[ObjectSystem]
		public class GameRecoverOnceComponentDestroySystem : DestroySystem<GameRecoverOnceComponent>
		{
			protected override void Destroy(GameRecoverOnceComponent self)
			{
				self.playerRecoverStatusDic.Clear();
			}
		}

		public static GamePlayComponent GetGamePlay(this GameRecoverOnceComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			GamePlayComponent gamePlayComponent = gamePlayTowerDefenseComponent.GetParent<GamePlayComponent>();
			return gamePlayComponent;
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this GameRecoverOnceComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			return gamePlayTowerDefenseComponent;
		}

		public static void Init(this GameRecoverOnceComponent self, float duration, GameRecoverType gameRecoverType, int recoverCostArcadeCoinNum)
		{
			self.timeOutTime = TimeHelper.ServerNow() + (long)(duration * 1000);
			self.recoverCostArcadeCoinNum = recoverCostArcadeCoinNum;

			self.playerRecoverStatusDic.Clear();
			List<long> playerList = self.GetGamePlayTowerDefense().GetPlayerList();
			foreach (long playerId in playerList)
			{
				self.playerRecoverStatusDic[playerId] = PlayerRecoverStatus.Default;
			}

			self.gameRecoverType = gameRecoverType;
		}

		public static bool ChkPlayerRecoverStatusIsDefault(this GameRecoverOnceComponent self, long playerId)
		{
			if (self.playerRecoverStatusDic.TryGetValue(playerId, out var playerRecoverStatus))
			{
				if (playerRecoverStatus == PlayerRecoverStatus.Default)
				{
					return true;
				}
			}
			return false;
		}
	}
}