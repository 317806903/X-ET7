using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(PlayerOwnerTowersComponent))]
    public static class PlayerOwnerTowersComponentSystem
	{
		[ObjectSystem]
		public class PlayerOwnerTowersComponentAwakeSystem : AwakeSystem<PlayerOwnerTowersComponent>
		{
			protected override void Awake(PlayerOwnerTowersComponent self)
			{
				self.playerOwnerTowerId = new();
				self.playerTowerBuyPools = new();
				self.playerTowerBuyPoolBoughts = new();
				self.playerRefreshTowerCost = new();
				self.playerId2unitTowerId = new();
				self.towerCfgId2PreTowerCfgId = new();
				self.towerCfgId2NextTowerCfgId = new();

				self.Init();
			}
		}

		[ObjectSystem]
		public class PlayerOwnerTowersComponentDestroySystem : DestroySystem<PlayerOwnerTowersComponent>
		{
			protected override void Destroy(PlayerOwnerTowersComponent self)
			{
				self.playerOwnerTowerId.Clear();
				self.playerTowerBuyPools.Clear();
				self.playerTowerBuyPoolBoughts.Clear();
				self.playerRefreshTowerCost.Clear();
				self.playerId2unitTowerId.Clear();
				self.towerCfgId2PreTowerCfgId.Clear();
				self.towerCfgId2NextTowerCfgId.Clear();
			}
		}

		public static void Init(this PlayerOwnerTowersComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				self.RefreshPlayerTowerPool(playerId);
				self.playerRefreshTowerCost[playerId] = gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost;
				self.playerOwnerTowerId[playerId] = new();
			}

			self.InitTowerUpgradeConfig();
		}

		public static void InitTowerUpgradeConfig(this PlayerOwnerTowersComponent self)
		{
			List<TowerDefense_TowerCfg> towerCfgList = TowerDefense_TowerCfgCategory.Instance.DataList;
			for (int i = 0; i < towerCfgList.Count; i++)
			{
				string curTowerId = towerCfgList[i].Id;
				string nextTowerId = towerCfgList[i].NextTowerId;
				if (string.IsNullOrEmpty(nextTowerId))
				{
					continue;
				}
				self.towerCfgId2PreTowerCfgId[nextTowerId] = curTowerId;
				self.towerCfgId2NextTowerCfgId[curTowerId] = nextTowerId;
			}
		}

		public static (bool, string) ChkRefreshTowerPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			string msg = "";
			bool success = self._ChkCostWhenRefresh(playerId);
			if (success == false)
			{
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_TowerBuy_NotEnough");
				return (false, msg);
			}
			return (true, msg);
		}

		public static bool RefreshTowerPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			(bool bRet, string msg) = self.ChkRefreshTowerPool(playerId);
			if (bRet == false)
			{
				return false;
			}
			bool success = self.CostWhenRefresh(playerId);
			if (success == false)
			{
				return false;
			}
			self.RefreshPlayerTowerPool(playerId);

			self.NoticeToClient(playerId);
			return true;
		}

		public static void RefreshAllPlayerTowerPool(this PlayerOwnerTowersComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				self.RefreshPlayerTowerPool(playerId);
			}
		}

		public static void RefreshPlayerTowerPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			if (self.playerTowerBuyPools.ContainsKey(playerId))
			{
				self.playerTowerBuyPools[playerId].Clear();
			}
			if (self.playerTowerBuyPoolBoughts.ContainsKey(playerId))
			{
				self.playerTowerBuyPoolBoughts[playerId].Clear();
			}

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			int count = gamePlayTowerDefenseComponent.model.BuyTowerPoolCount;
			TowerDefense_BuyTowerRefreshRuleCfg towerDefense_BuyTowerRefreshRuleCfg = TowerDefense_BuyTowerRefreshRuleCfgCategory.Instance.Get(gamePlayTowerDefenseComponent.model.BuyTowerRefreshRuleCfgId);
			for (int i = 0; i < count; i++)
			{
				string towerCfgId = ET.RandomGenerator.GetRandomIndexLinear(towerDefense_BuyTowerRefreshRuleCfg.TowerWeight);
				self.playerTowerBuyPools.Add(playerId, towerCfgId);
				self.playerTowerBuyPoolBoughts.Add(playerId, false);
			}
		}

		public static bool _ChkCostWhenRefresh(this PlayerOwnerTowersComponent self, long playerId)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			int chgValue = gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost;
			int curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinType.Gold);
			if (curGold < chgValue)
			{
				return false;
			}
			return true;
		}

		public static bool CostWhenRefresh(this PlayerOwnerTowersComponent self, long playerId)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			int chgValue = gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost;
			int curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinType.Gold);
			if (curGold < chgValue)
			{
				return false;
			}
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinType.Gold, -chgValue);
			return true;
		}

		public static bool _ChkCostWhenBuyTower(this PlayerOwnerTowersComponent self, long playerId, string towerCfgId)
		{
			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
			int chgValue = towerCfg.BuyTowerCostGold;
			int curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinType.Gold);
			if (curGold < chgValue)
			{
				return false;
			}
			return true;
		}

		public static bool CostWhenBuyTower(this PlayerOwnerTowersComponent self, long playerId, string towerCfgId)
		{
			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
			int chgValue = towerCfg.BuyTowerCostGold;
			int curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinType.Gold);
			if (curGold < chgValue)
			{
				return false;
			}
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinType.Gold, -chgValue);
			return true;
		}

		public static (bool, string) ChkBuyPlayerTower(this PlayerOwnerTowersComponent self, long playerId, int index)
		{
			string msg = "";
			if (self.playerTowerBuyPools.ContainsKey(playerId) == false)
			{
				Log.Debug($" BuyPlayerTower self.playerTowerPools.ContainsKey(playerId) == false 不存在这个用户");
				msg = "不存在这个用户";
				return (false, msg);
			}
			if (self.playerTowerBuyPools[playerId].Count <= index)
			{
				Log.Debug($" BuyPlayerTower self.playerTowerPools[playerId].Count <= index 没有这个序列的塔");
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_NotHaveThisTower");
				return (false, msg);
			}

			bool isBought = self.playerTowerBuyPoolBoughts[playerId][index];
			if (isBought)
			{
				Log.Debug($" BuyPlayerTower isBought==true 已不可购买");
				msg = "已不可购买";
				return (false, msg);
			}

			string towerCfgId = self.playerTowerBuyPools[playerId][index];

			bool success = self._ChkCostWhenBuyTower(playerId, towerCfgId);
			if (success == false)
			{
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_TowerBuy_NotEnough");
				return (false, msg);
			}

			return (true, msg);
		}

		public static bool BuyPlayerTower(this PlayerOwnerTowersComponent self, long playerId, int index)
		{
			(bool bRet, string msg) = self.ChkBuyPlayerTower(playerId, index);
			if (bRet == false)
			{
				return false;
			}

			string towerCfgId = self.playerTowerBuyPools[playerId][index];

			bool success = self.CostWhenBuyTower(playerId, towerCfgId);
			if (success == false)
			{
				return false;
			}

			if (self.playerOwnerTowerId.TryGetValue(playerId, towerCfgId, out int ownCount) == false)
			{
				self.playerOwnerTowerId.Add(playerId, towerCfgId, 1);
			}
			else
			{
				self.playerOwnerTowerId[playerId][towerCfgId] += 1;
			}
			self.playerTowerBuyPoolBoughts[playerId][index] = true;

			if (self.ChkRefreshPlayerTowerPool(playerId))
			{
				self.RefreshPlayerTowerPool(playerId);
			}

			self.NoticeToClient(playerId);
			return true;
		}

		public static void NoticeToClient(this PlayerOwnerTowersComponent self, long playerId)
		{
			self.GetParent<GamePlayTowerDefenseComponent>().NoticeToClient(playerId);
		}

		/// <summary>
		/// 检查如果已经都被购买则触发刷新
		/// </summary>
		/// <param name="self"></param>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static bool ChkRefreshPlayerTowerPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			List<bool> list = self.playerTowerBuyPoolBoughts[playerId];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == false)
				{
					return false;
				}
			}
			return true;
		}

		public static List<Unit> CreateTower(this PlayerOwnerTowersComponent self, long playerId, string towerCfgId, float3 position)
		{
			return GamePlayTowerDefenseHelper.CreateTower(self.DomainScene(), playerId, towerCfgId, position);
		}

		public static (bool, string) ChkCallPlayerTower(this PlayerOwnerTowersComponent self, long playerId, string towerCfgId)
		{
			string msg = "";
			if (self.playerOwnerTowerId.TryGetValue(playerId, towerCfgId, out int count) == false)
			{
				count = 0;
			}

			if (count <= 0)
			{
				Log.Debug($" CallPlayerTower self.playerOwnerTowerId count <= 0");
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_NotHaveThisTower");
				return (false, msg);
			}

			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);

			bool isTower = towerCfg.Type is PlayerTowerType.Tower;
			bool isCallMonster = towerCfg.Type is PlayerTowerType.CallMonster;
			if (isTower)
			{
				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
				int limitTowerCount = gamePlayTowerDefenseComponent.model.LimitTowerCount;
				List<long> towerUnitIdList = self.playerId2unitTowerId[playerId];
				if (towerUnitIdList.Count >= limitTowerCount)
				{
					msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MaxPutTowerCount", limitTowerCount);
					return (false, msg);
				}
			}
			return (true, msg);;
		}

		public static int GetLeftCallPlayerTowerCount(this PlayerOwnerTowersComponent self, long playerId)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			int limitTowerCount = gamePlayTowerDefenseComponent.model.LimitTowerCount;
			List<long> towerUnitIdList = self.playerId2unitTowerId[playerId];
			return limitTowerCount - towerUnitIdList.Count;
		}

		public static bool CallPlayerTower(this PlayerOwnerTowersComponent self, long playerId, string towerCfgId, float3 position)
		{
			(bool bRet, string msg) = self.ChkCallPlayerTower(playerId, towerCfgId);
			if (bRet == false)
			{
				return false;
			}

			if (self.playerOwnerTowerId.TryGetValue(playerId, towerCfgId, out int count) == false)
			{
				Log.Debug($" CallPlayerTower self.playerOwnerTowerId.TryGetValue(playerId, towerCfgId, out int count) == false");
				return false;
			}

			if (count <= 0)
			{
				Log.Debug($" CallPlayerTower count <= 0");
				return false;
			}

			self.playerOwnerTowerId[playerId][towerCfgId] = count - 1;

			List<Unit> towerUnitList = self.CreateTower(playerId, towerCfgId, position);
			foreach (var towerUnit in towerUnitList)
			{
				self.playerId2unitTowerId.Add(playerId, towerUnit.Id);
			}

			self.NoticeToClient(playerId);
			return true;
		}

		public static (bool, string) ChkIsUpgradeMaxPlayerTower(this PlayerOwnerTowersComponent self, string towerCfgId)
		{
			string msg = "";
			if (self.towerCfgId2NextTowerCfgId.TryGetValue(towerCfgId, out string nextTowerId) == false)
			{
				msg = "已经是最高级";
				return (true, msg);
			}

			return (false, "");
		}

		public static (bool, string, Dictionary<string, int>) ChkUpgradePlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId)
		{
			string msg = "";
			Dictionary<string, int> costTowers = null;
			if (self.playerId2unitTowerId.Contains(playerId, towerUnitId) == false)
			{
				Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] not exist in self.playerId2unitTowerId 不存在这个塔");
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_NotHaveThisTower");
				return (false, msg, null);
			}

			Unit curTownUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			string curTowerId = curTownUnit.GetComponent<TowerComponent>().towerCfgId;

			(bool bRet1, string msg1) = self.ChkIsUpgradeMaxPlayerTower(curTowerId);
			if (bRet1)
			{
				return (false, msg1, null);
			}

			TowerDefense_TowerCfg curTowerCfg = TowerDefense_TowerCfgCategory.Instance.Get(curTowerId);

			costTowers = new();
			bool bRet = self.GetUpgradePlayerTowerCost(playerId, curTowerId, curTowerCfg.NewTowerCostCount - 1, ref costTowers);
			if (bRet == false)
			{
				Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] GetUpgradePlayerTowerCost == false 材料不足,无法升级");
				msg = "材料不足,无法升级";
				return (false, msg, costTowers);
			}

			return (true, msg, costTowers);
		}

		public static bool UpgradePlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId)
		{
			(bool bRet, string msg, Dictionary<string, int> costTowers) = self.ChkUpgradePlayerTower(playerId, towerUnitId);
			if (bRet == false)
			{
				return false;
			}

			Unit curTownUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			string curTowerId = curTownUnit.GetComponent<TowerComponent>().towerCfgId;

			if (self.towerCfgId2NextTowerCfgId.TryGetValue(curTowerId, out string nextTowerId) == false)
			{
				Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] string.IsNullOrEmpty(nextTowerId)");
				return false;
			}

			foreach (var costTower in costTowers)
			{
				string costTowerId = costTower.Key;
				int costTowerCount = costTower.Value;
				if (costTowerCount > 0)
				{
					if (self.playerOwnerTowerId.TryGetValue(playerId, costTowerId, out int count))
					{
						self.playerOwnerTowerId[playerId][costTowerId] = count - costTowerCount;
					}
				}
			}

			float3 position = curTownUnit.Position;
			self.playerId2unitTowerId.Remove(playerId, towerUnitId);
			curTownUnit.DestroyNotDeathShow();

			List<Unit> towerUnitList = self.CreateTower(playerId, nextTowerId, position);
			foreach (var towerUnit in towerUnitList)
			{
				self.playerId2unitTowerId.Add(playerId, towerUnit.Id);
			}

			self.NoticeToClient(playerId);
			return true;
		}

		public static bool ScalePlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId)
		{
			if (self.playerId2unitTowerId.Contains(playerId, towerUnitId) == false)
			{
				Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] not exist in self.playerId2unitTowerId");
				return false;
			}

			Unit curTownUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			string curTowerId = curTownUnit.GetComponent<TowerComponent>().towerCfgId;

			TowerDefense_TowerCfg curTowerCfg = TowerDefense_TowerCfgCategory.Instance.Get(curTowerId);
			int scaleTowerCostGold = curTowerCfg.ScaleTowerCostGold;
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinType.Gold, scaleTowerCostGold);

			self.playerId2unitTowerId.Remove(playerId, towerUnitId);
			curTownUnit.DestroyNotDeathShow();

			self.NoticeToClient(playerId);
			return true;
		}

		public static (bool, string) ChkReclaimPlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId)
		{
			string msg = "";
			if (self.playerId2unitTowerId.Contains(playerId, towerUnitId) == false)
			{
				Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] not exist in self.playerId2unitTowerId");
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_NotHaveThisTower");
				return (false, msg);
			}

			Unit curTownUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			string curTowerId = curTownUnit.GetComponent<TowerComponent>().towerCfgId;

			TowerDefense_TowerCfg curTowerCfg = TowerDefense_TowerCfgCategory.Instance.Get(curTowerId);
			int reclaimTowerCostGold = curTowerCfg.ReclaimTowerCostGold;
			int curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinType.Gold);
			if (curGold < reclaimTowerCostGold)
			{
				Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] curGold[{curGold}] < reclaimTowerCostGold[{reclaimTowerCostGold}]");
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_TowerBuy_NotEnough");
				return (false, msg);
			}

			return (true, msg);
		}

		public static bool ReclaimPlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId)
		{
			(bool bRet, string msg) = self.ChkReclaimPlayerTower(playerId, towerUnitId);
			if (bRet == false)
			{
				return false;
			}

			Unit curTownUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			string curTowerId = curTownUnit.GetComponent<TowerComponent>().towerCfgId;

			TowerDefense_TowerCfg curTowerCfg = TowerDefense_TowerCfgCategory.Instance.Get(curTowerId);
			int reclaimTowerCostGold = curTowerCfg.ReclaimTowerCostGold;
			// int curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinType.Gold);
			// if (curGold < reclaimTowerCostGold)
			// {
			// 	Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] curGold[{curGold}] < reclaimTowerCostGold[{reclaimTowerCostGold}]");
			// 	return false;
			// }
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinType.Gold, -reclaimTowerCostGold);

			if (self.playerOwnerTowerId.TryGetValue(playerId, curTowerId, out int ownCount) == false)
			{
				self.playerOwnerTowerId.Add(playerId, curTowerId, 1);
			}
			else
			{
				self.playerOwnerTowerId[playerId][curTowerId] += 1;
			}

			self.playerId2unitTowerId.Remove(playerId, towerUnitId);
			curTownUnit.DestroyNotDeathShow();

			self.NoticeToClient(playerId);
			return true;
		}

		public static bool GetUpgradePlayerTowerCost(this PlayerOwnerTowersComponent self, long playerId, string curTowerId, int needCount, ref Dictionary<string, int> costTowers)
		{
			if (self.playerOwnerTowerId.TryGetValue(playerId, curTowerId, out int count) == false)
			{
				count = 0;
			}
			if (count >= needCount)
			{
				costTowers[curTowerId] = needCount;
				return true;
			}
			else
			{
				costTowers[curTowerId] = count;

				if (self.towerCfgId2PreTowerCfgId.TryGetValue(curTowerId, out string preTowerId))
				{
					TowerDefense_TowerCfg preTowerCfg = TowerDefense_TowerCfgCategory.Instance.Get(preTowerId);
					int needPreCount = (needCount - count) * preTowerCfg.NewTowerCostCount;
					return self.GetUpgradePlayerTowerCost(playerId, preTowerId, needPreCount, ref costTowers);
				}
				else
				{
					return false;
				}

			}
		}

		public static List<long> GetPutTowers(this PlayerOwnerTowersComponent self, long playerId)
		{
			return self.playerId2unitTowerId[playerId];
		}

		public static bool ChkIsNearTower(this PlayerOwnerTowersComponent self, float3 targetPos, float targetUnitRadius, long playerId = -1)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
			var homeUnitList = putHomeComponent.GetHomeUnitList();
			float nearDis = 0.3f;
			foreach (var homeUnits in homeUnitList)
			{
				if (playerId != -1)
				{
					TeamFlagType homeTeamFlagType = gamePlayTowerDefenseComponent.GetHomeTeamFlagTypeByPlayer(playerId);
					if (homeTeamFlagType != homeUnits.Key)
					{
						continue;
					}
				}
				long homeUnitId = homeUnits.Value;
				Unit homeUnit = UnitHelper.GetUnit(self.DomainScene(), homeUnitId);
				if (homeUnit != null)
				{
					bool isNear1 = UnitHelper.ChkIsNear(homeUnit, targetPos, targetUnitRadius, nearDis, false);
					if (isNear1)
					{
						return true;
					}
				}
			}

			foreach (var list in self.playerId2unitTowerId)
			{
				if (playerId != -1 && playerId != list.Key)
				{
					continue;
				}
				foreach (long unitId in list.Value)
				{
					Unit unit = UnitHelper.GetUnit(self.DomainScene(), unitId);
					if (unit == null)
					{
						continue;
					}
					bool isNear = UnitHelper.ChkIsNear(unit, targetPos, targetUnitRadius, nearDis, false);
					if (isNear)
					{
						return true;
					}
				}
			}

			return false;
		}

	}
}