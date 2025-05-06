using ET.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
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
				//self.playerOwnerTowerCardIds = new();
				self.playerOwnerTowerId = new();
				self.playerTowerBuyPools = new();
				self.playerTowerBuyPoolBoughts = new();
				self.playerTowerBuyPoolCosts = new();
				self.playerRefreshTowerCost = new();
				self.playerId2unitTowerId = new();
				self.playerId2unitAttackTowerLimitCount = new();
				self.playerId2unitAttackTowerId = new();
			}
		}

		[ObjectSystem]
		public class PlayerOwnerTowersComponentDestroySystem : DestroySystem<PlayerOwnerTowersComponent>
		{
			protected override void Destroy(PlayerOwnerTowersComponent self)
			{
				//self.playerOwnerTowerCardIds.Clear();
				self.playerOwnerTowerId.Clear();
				self.playerTowerBuyPools.Clear();
				self.playerTowerBuyPoolBoughts.Clear();
				self.playerTowerBuyPoolCosts.Clear();
				self.playerRefreshTowerCost.Clear();
				self.playerId2unitTowerId.Clear();
				self.playerId2unitAttackTowerLimitCount.Clear();
				self.playerId2unitAttackTowerId.Clear();
				self.existTowerDic?.Clear();
			}
		}

		public static async ETTask Init(this PlayerOwnerTowersComponent self)
		{
			while (self.initPlayerOwnerTowerCard == false)
			{
				await TimerComponent.Instance.WaitFrameAsync();
			}
			await self.InitPlayerTowerPool();
			await self.InitPlayerTowerLimitCount();
		}

		public static async ETTask InitPlayerTowerPool(this PlayerOwnerTowersComponent self)
		{
			await ETTask.CompletedTask;
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			Dictionary<string, int> initCards = gamePlayTowerDefenseComponent.model.PlayerInitCards;
			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				self.RefreshPlayerTowerPool(playerId);
				self.playerRefreshTowerCost[playerId] = gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost;
				self.playerOwnerTowerId[playerId] = new();
				foreach (var item in initCards)
				{
					self.playerOwnerTowerId[playerId].Add(item.Key, item.Value);
				}
			}

		}

		public static async ETTask InitPlayerTowerLimitCount(this PlayerOwnerTowersComponent self)
		{
			await ETTask.CompletedTask;
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];

				GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerLimitTowerCountBase, gamePlayTowerDefenseComponent.model.LimitTowerCount, true);
				float newLimitTowerCount = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerLimitTowerCount);

				self.playerId2unitAttackTowerLimitCount[playerId] = (int)newLimitTowerCount;
			}
		}

		public static int GetOwnerTowerLevel(this PlayerOwnerTowersComponent self, long playerId, string towerCfgId)
		{
			if (self.playerOwnerTowerCardIds.TryGetValue(playerId, towerCfgId, out var level))
			{
				return level;
			}
			return 1;
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
				self.NoticeToClient(playerId);
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
			if (self.playerTowerBuyPoolCosts.ContainsKey(playerId))
			{
				self.playerTowerBuyPoolCosts[playerId].Clear();
			}

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			int count = gamePlayTowerDefenseComponent.model.BuyTowerPoolCount;
			TowerDefense_BuyTowerRefreshRuleCfg towerDefense_BuyTowerRefreshRuleCfg = TowerDefense_BuyTowerRefreshRuleCfgCategory.Instance.Get(gamePlayTowerDefenseComponent.model.BuyTowerRefreshRuleCfgId);

			Dictionary<string, int> towerPools = DictionaryComponent<string, int>.Create();

			DictionaryComponent<string, int> towerPoolsWhenWeight100 = DictionaryComponent<string, int>.Create();
			ListComponent<string> towerPoolWhenWeight100 = ListComponent<string>.Create();
			if (towerDefense_BuyTowerRefreshRuleCfg.TowerWeight != null && towerDefense_BuyTowerRefreshRuleCfg.TowerWeight.Count > 0)
			{
				MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
				int curMonsterWaveIndex = 1;
				if (monsterWaveCallComponent != null)
				{
					curMonsterWaveIndex = monsterWaveCallComponent.GetWaveIndex() + 1;
				}
				foreach (var item in towerDefense_BuyTowerRefreshRuleCfg.TowerWeight)
				{
					if (item.StartWaveIndex <= curMonsterWaveIndex && item.EndWaveIndex >= curMonsterWaveIndex)
					{
						string key = $"{item.TowerCfgId}|{item.TowerNum}";
						if (towerPools.TryGetValue(item.TowerCfgId, out int weight))
						{
							weight += item.Weight;
							towerPools[key] = weight;
						}
						else
						{
							weight = item.Weight;
							if (weight == 100)
							{
								towerPoolsWhenWeight100.Add(key, weight);
								towerPoolWhenWeight100.Add(key);
							}
							else
							{
								towerPools.Add(key, weight);
							}
						}
					}
				}
			}

			if (towerPoolWhenWeight100.Count >= 0)
			{
				int poolCount = 0;
				bool needRandom = true;
				if (count >= towerPoolWhenWeight100.Count)
				{
					needRandom = false;
					poolCount = towerPoolWhenWeight100.Count;
					count -= towerPoolWhenWeight100.Count;
				}
				else
				{
					poolCount = count;
					count = 0;
				}

				for (int i = 0; i < poolCount; i++)
				{
					string towerCfgId;
					int towerNum;
					if (needRandom)
					{
						string randomInfo = ET.RandomGenerator.GetRandomIndexLinear(towerPoolsWhenWeight100);
						towerCfgId = randomInfo.Split("|")[0];
						towerNum = int.Parse(randomInfo.Split("|")[1]);
					}
					else
					{
						string indexInfo = towerPoolWhenWeight100[i];
						towerCfgId = indexInfo.Split("|")[0];
						towerNum = int.Parse(indexInfo.Split("|")[1]);
					}
					self.playerTowerBuyPools.Add(playerId, (towerCfgId, towerNum));
					self.playerTowerBuyPoolBoughts.Add(playerId, false);

					int costGold = 0;
					if (ItemHelper.ChkIsTower(towerCfgId))
					{
						TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
						costGold = towerCfg.BuyTowerCostGold * towerNum;
					}
					else if (ItemHelper.ChkIsSkill(towerCfgId))
					{
						PlayerSkillCfg playerSkillCfg = PlayerSkillCfgCategory.Instance.Get(towerCfgId);
						costGold = playerSkillCfg.BuySkillCardCostGold * towerNum;
					}

					GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerTowerPriceBase, costGold, true);
					float newCostGold = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerTowerPrice);

					self.playerTowerBuyPoolCosts.Add(playerId, (int)newCostGold);
				}

				towerPoolWhenWeight100.Dispose();
				towerPoolsWhenWeight100.Dispose();
			}

			if (count > 0)
			{
				if (self.playerOwnerTowerCardIds.TryGetValue(playerId, out var ownerTowerCardIds))
				{
					int myTowersWeight = towerDefense_BuyTowerRefreshRuleCfg.MyTowersWeight;
					int mySkillsWeight = towerDefense_BuyTowerRefreshRuleCfg.MySkillsWeight;
					foreach (var item in ownerTowerCardIds)
					{
						string itemCfgId = item.Key;
						int myWeight = 0;
						if (ItemHelper.ChkIsTower(itemCfgId))
						{
							myWeight = myTowersWeight;
						}
						else if (ItemHelper.ChkIsSkill(itemCfgId))
						{
							myWeight = mySkillsWeight;
						}
						string key = $"{itemCfgId}|1";
						if (towerPools.TryGetValue(key, out int weight))
						{
							weight += myWeight;
							towerPools[key] = weight;
						}
						else
						{
							weight = myWeight;
							towerPools.Add(key, weight);
						}
					}
				}

				for (int i = 0; i < count; i++)
				{
					string randomInfo = ET.RandomGenerator.GetRandomIndexLinear(towerPools);
					string towerCfgId = randomInfo.Split("|")[0];
					int towerNum = int.Parse(randomInfo.Split("|")[1]);
					self.playerTowerBuyPools.Add(playerId, (towerCfgId, towerNum));
					self.playerTowerBuyPoolBoughts.Add(playerId, false);

					int costGold = 0;
					if (ItemHelper.ChkIsTower(towerCfgId))
					{
						TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
						costGold = towerCfg.BuyTowerCostGold * towerNum;
					}
					else if (ItemHelper.ChkIsSkill(towerCfgId))
					{
						PlayerSkillCfg playerSkillCfg = PlayerSkillCfgCategory.Instance.Get(towerCfgId);
						costGold = playerSkillCfg.BuySkillCardCostGold * towerNum;
					}

					GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerTowerPriceBase, costGold, true);
					float newCostGold = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerTowerPrice);

					self.playerTowerBuyPoolCosts.Add(playerId, (int)newCostGold);
				}
			}

			EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_RefreshTowerBuyPool()
			{
				playerId = playerId,
			});

		}

		public static void RewardExtendCards(this PlayerOwnerTowersComponent self, long playerId, List<(string towerCfgId, int towerNum)> towerList)
		{
			foreach (var item in towerList)
			{
				string towerCfgId = item.towerCfgId;
				int towerNum = item.towerNum;
				if (self.playerOwnerTowerId.TryGetValue(playerId, towerCfgId, out int ownCount) == false)
				{
					self.playerOwnerTowerId.Add(playerId, towerCfgId, towerNum);
				}
				else
				{
					self.playerOwnerTowerId[playerId][towerCfgId] += towerNum;
				}
			}

			self.NoticeToClient(playerId);
		}

		public static bool _ChkCostWhenRefresh(this PlayerOwnerTowersComponent self, long playerId)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			int chgValue = gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost;
			float curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold);
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
			float curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold);
			if (curGold < chgValue)
			{
				return false;
			}
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold, -chgValue);
			return true;
		}

		public static bool _ChkCostWhenBuyTower(this PlayerOwnerTowersComponent self, long playerId, int costGold)
		{
			float curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold);
			if (curGold < costGold)
			{
				return false;
			}
			return true;
		}

		public static bool CostWhenBuyTower(this PlayerOwnerTowersComponent self, long playerId, int costGold)
		{
			float curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold);
			if (curGold < costGold)
			{
				return false;
			}
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold, -costGold);
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
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_TowerBuy_AlreadyBuy");
				return (false, msg);
			}

			int costGold = self.playerTowerBuyPoolCosts[playerId][index];
			bool success = self._ChkCostWhenBuyTower(playerId, costGold);
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

			(string towerCfgId, int towerNum) = self.playerTowerBuyPools[playerId][index];

			int costGold = self.playerTowerBuyPoolCosts[playerId][index];
			bool success = self.CostWhenBuyTower(playerId, costGold);
			if (success == false)
			{
				return false;
			}

			if (self.playerOwnerTowerId.TryGetValue(playerId, towerCfgId, out int ownCount) == false)
			{
				self.playerOwnerTowerId.Add(playerId, towerCfgId, towerNum);
			}
			else
			{
				self.playerOwnerTowerId[playerId][towerCfgId] += towerNum;
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

		public static void NoticeToClientAll(this PlayerOwnerTowersComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				self.NoticeToClient(playerId);
			}
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

		public static List<Unit> CreateTower(this PlayerOwnerTowersComponent self, long playerId, string towerCfgId, float3 position, float3 forward)
		{
			return GamePlayTowerDefenseHelper.CreateTower(self.DomainScene(), playerId, towerCfgId, position, forward);
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

			bool isAttackTower = ItemHelper.ChkIsAttackTower(towerCfgId);
			bool isCallMonster = ItemHelper.ChkIsCallMonster(towerCfgId);
			if (isAttackTower)
			{
				int limitTowerCount = self.GetPutAttackTowerLimitCount(playerId);
				int attackTowerCount = self.GetPutAttackTowerCount(playerId);
				if (attackTowerCount >= limitTowerCount)
				{
					msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MaxPutTowerCount", limitTowerCount);
					return (false, msg);
				}
			}
			return (true, msg);
		}

		public static List<int> GetPlayerTowerPrice(this PlayerOwnerTowersComponent self, long playerId)
		{
			return self.playerTowerBuyPoolCosts[playerId];
		}

		public static void ResetPutAttackTowerLimitCount(this PlayerOwnerTowersComponent self)
		{
			foreach (var item in self.playerId2unitAttackTowerLimitCount)
			{
				long playerId = item.Key;
				float newLimitTowerCount = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerLimitTowerCount);

				self.playerId2unitAttackTowerLimitCount[playerId] = (int)newLimitTowerCount;
			}

		}

		public static int GetPutAttackTowerLimitCount(this PlayerOwnerTowersComponent self, long playerId)
		{
			int attackTowerLimitCount = self.playerId2unitAttackTowerLimitCount[playerId];
			return attackTowerLimitCount;
		}

		public static int GetPutAttackTowerCount(this PlayerOwnerTowersComponent self, long playerId)
		{
			List<long> towerUnitIdList = self.playerId2unitAttackTowerId[playerId];
			int attackTowerCount = towerUnitIdList.Count;
			return attackTowerCount;
		}

		public static int GetLeftCallPlayerTowerCount(this PlayerOwnerTowersComponent self, long playerId)
		{
			return self.GetPutAttackTowerLimitCount(playerId) - self.GetPutAttackTowerCount(playerId);
		}

		public static bool CallPlayerTower(this PlayerOwnerTowersComponent self, long playerId, string towerCfgId, float3 position, float3 forward)
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

			List<Unit> towerUnitList = self.CreateTower(playerId, towerCfgId, position, forward);
			foreach (var towerUnit in towerUnitList)
			{
				self.playerId2unitTowerId.Add(playerId, towerUnit.Id);
				if (ItemHelper.ChkIsAttackTower(towerCfgId))
				{
					self.playerId2unitAttackTowerId.Add(playerId, towerUnit.Id);
				}

				EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_PutTower()
				{
					playerId = playerId,
					towerUnit = towerUnit,
					towerCfgId = towerCfgId,
				});

			}

			self.NoticeToClient(playerId);

			return true;
		}

		public static (bool, string) ChkIsUpgradeMaxPlayerTower(this PlayerOwnerTowersComponent self, string towerCfgId)
		{
			string msg = "";
			TowerDefense_TowerCfg nextTowerCfg = TowerDefense_TowerCfgCategory.Instance.GetNextTowerCfg(towerCfgId);
			if (nextTowerCfg == null)
			{
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MaxLevelTower");
				return (true, msg);
			}

			return (false, "");
		}


		public static (bool, string, Dictionary<string, int>, List<long>) ChkUpgradePlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId, bool onlyChkPool)
		{
			string msg = "";
			if (self.playerId2unitTowerId.Contains(playerId, towerUnitId) == false)
			{
				Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] not exist in self.playerId2unitTowerId 不存在这个塔");
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_NotHaveThisTower");
				return (false, msg, null, null);
			}

			Unit curTownUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			string curTowerCfgId = curTownUnit.GetComponent<TowerComponent>().towerCfgId;
			return self.ChkUpgradePlayerTower(playerId, towerUnitId, curTowerCfgId, onlyChkPool);
		}

		public static (bool, string, Dictionary<string, int>, List<long>) ChkUpgradePlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId, string towerCfgId, bool onlyChkPool)
		{
			string msg = "";

			string curTowerCfgId = towerCfgId;

			bool bRet = false;
			(bRet, msg) = self.ChkIsUpgradeMaxPlayerTower(curTowerCfgId);
			if (bRet)
			{
				return (false, msg, null, null);
			}

			TowerDefense_TowerCfg curTowerCfg = TowerDefense_TowerCfgCategory.Instance.Get(curTowerCfgId);

			int needCount = 0;
			if (onlyChkPool)
			{
				needCount = curTowerCfg.NewTowerCostCount;
			}
			else
			{
				needCount = curTowerCfg.NewTowerCostCount - 1;
			}

			Dictionary<string, int> costPoolTowers = null;
			(bRet, msg, costPoolTowers) = self.ChkUpgradePlayerTowerOnlyPool(playerId, ref curTowerCfgId, ref needCount);
			if (bRet)
			{
				return (true, msg, costPoolTowers, null);
			}

			if (onlyChkPool == false)
			{
				List<long> existTowerUnitIds = null;
				(bRet, msg, existTowerUnitIds) = self.ChkUpgradePlayerTowerWhenExist(playerId, towerUnitId, curTowerCfgId, needCount, costPoolTowers);
				if (bRet)
				{
					return (true, msg, costPoolTowers, existTowerUnitIds);
				}
			}
			return (false, msg, null, null);
		}

		public static (bool, string, Dictionary<string, int>) ChkUpgradePlayerTowerOnlyPool(this PlayerOwnerTowersComponent self, long playerId, ref string needTowerCfgId, ref int needCount)
		{
			string msg = "";
			Dictionary<string, int> costPoolTowers = DictionaryComponent<string, int>.Create();
			bool bRet = self.GetUpgradePlayerTowerPoolCost(playerId, ref needTowerCfgId, ref needCount, ref costPoolTowers);
			if (bRet == false)
			{
				//Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] GetUpgradePlayerTowerCost == false 材料不足,无法升级");
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CannotLevelUpTower", needCount);
				return (false, msg, costPoolTowers);
			}

			return (true, msg, costPoolTowers);
		}

		public static (bool, string, List<long>) ChkUpgradePlayerTowerWhenExist(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId, string needTowerCfgId, int needCount, Dictionary<string, int> costPoolTowers)
		{
			string msg = "";
			(bool bRet, List<long> existTowerUnitIds) = self.ChkUpgradePlayerTowerWhenExistXXX(playerId, towerUnitId, needTowerCfgId, needCount, costPoolTowers);

			if (bRet == false)
			{
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CannotLevelUpTower", needCount);
				return (false, msg, existTowerUnitIds);
			}

			return (true, msg, existTowerUnitIds);
		}

		public static bool ChkUpgradePlayerTowerWhenExistXXX222(this PlayerOwnerTowersComponent self, string baseTowerCfgId, int needCostBaseTowerCount, Dictionary<string, int> costPoolTowers, Dictionary<string, int> costExistTowers, MultiMap<string, long> existTowerDic)
		{
			foreach (var item in existTowerDic)
			{
				string towerCfgId = item.Key;
				int count = item.Value.Count;
				(string baseTowerCfgIdTmp, int costCountTmp) = self.GetBaseTowerCfgIdAndCount(towerCfgId);
				int needCountTmp = needCostBaseTowerCount / costCountTmp;
				int leftTmp = needCostBaseTowerCount % costCountTmp;
				if (needCountTmp < count)
				{
					if (leftTmp == 0)
					{
						needCostBaseTowerCount -= costCountTmp * needCountTmp;
						costExistTowers.Add(towerCfgId, needCountTmp);
						return true;
					}
					else
					{
						int needCountAddBack = costCountTmp * (needCountTmp + 1) - needCostBaseTowerCount;
						costExistTowers.Add(towerCfgId, needCountTmp + 1);
						//吐掉低级的
						while (true)
						{
							TowerDefense_TowerCfg preTowerCfg = TowerDefense_TowerCfgCategory.Instance.GetPreTowerCfg(towerCfgId);
							if (preTowerCfg != null)
							{
								string preTowerCfgId = preTowerCfg.Id;
								if (costExistTowers.ContainsKey(preTowerCfgId))
								{
									(_, int PreCostCountTmp) = self.GetBaseTowerCfgIdAndCount(preTowerCfgId);
									int PreNeedCountTmp = needCountAddBack / PreCostCountTmp;
									int PreLeftTmp = needCountAddBack % PreCostCountTmp;

									int preExistTowerCount = costExistTowers[preTowerCfgId];
									if (preExistTowerCount < PreNeedCountTmp)
									{
										costExistTowers.Remove(preTowerCfgId);
										needCountAddBack -= PreCostCountTmp * preExistTowerCount;
									}
									else if (preExistTowerCount == PreNeedCountTmp)
									{
										if (PreLeftTmp == 0)
										{
											costExistTowers.Remove(preTowerCfgId);
											needCountAddBack = 0;
											return true;
										}
										else
										{
											costExistTowers.Remove(preTowerCfgId);
											needCountAddBack -= PreCostCountTmp * preExistTowerCount;
										}
									}
									else
									{
										costExistTowers[preTowerCfgId] = preExistTowerCount - PreNeedCountTmp;
										needCountAddBack -= PreCostCountTmp * PreNeedCountTmp;
									}
								}
								towerCfgId = preTowerCfgId;
							}
							else
							{
								if (costPoolTowers.ContainsKey(baseTowerCfgId))
								{
									int nextPoolTowerCount = costPoolTowers[baseTowerCfgId];
									if (nextPoolTowerCount >= needCountAddBack)
									{
										costPoolTowers[baseTowerCfgId] = nextPoolTowerCount - needCountAddBack;
										needCountAddBack = 0;
										return true;
									}
									else
									{
										costPoolTowers.Remove(baseTowerCfgId);
										needCountAddBack -= nextPoolTowerCount;
									}
								}
								while (true)
								{
									TowerDefense_TowerCfg nextTowerCfg = TowerDefense_TowerCfgCategory.Instance.GetNextTowerCfg(towerCfgId);
									if (nextTowerCfg != null)
									{
										string nextTowerCfgId = nextTowerCfg.Id;
										if (costPoolTowers.ContainsKey(nextTowerCfgId))
										{
											(_, int NextCostCountTmp) = self.GetBaseTowerCfgIdAndCount(nextTowerCfgId);
											int NextNeedCountTmp = needCountAddBack / NextCostCountTmp;
											int NextLeftTmp = needCountAddBack % NextCostCountTmp;

											int nextPoolTowerCount = costPoolTowers[nextTowerCfgId];
											if (nextPoolTowerCount < NextNeedCountTmp)
											{
												costPoolTowers.Remove(nextTowerCfgId);
												needCountAddBack -= NextCostCountTmp * nextPoolTowerCount;
											}
											else if (nextPoolTowerCount == NextNeedCountTmp)
											{
												if (NextLeftTmp == 0)
												{
													costPoolTowers.Remove(nextTowerCfgId);
													needCountAddBack = 0;
													return true;
												}
												else
												{
													costPoolTowers.Remove(nextTowerCfgId);
													needCountAddBack -= NextCostCountTmp * nextPoolTowerCount;
												}
											}
											else
											{
												costPoolTowers[nextTowerCfgId] = nextPoolTowerCount - NextNeedCountTmp;
												needCountAddBack -= NextCostCountTmp * NextNeedCountTmp;
											}
										}
										towerCfgId = nextTowerCfgId;
									}
									else
									{
										break;
									}
								}

								break;
							}
						}
					}
				}
				else if (needCountTmp == count)
				{
					if (leftTmp == 0)
					{
						needCostBaseTowerCount -= costCountTmp * count;
						costExistTowers.Add(towerCfgId, count);
						return true;
					}
					else
					{
						needCostBaseTowerCount -= costCountTmp * count;
						costExistTowers.Add(towerCfgId, count);
					}
				}
				else
				{
					needCostBaseTowerCount -= costCountTmp * count;
					costExistTowers.Add(towerCfgId, count);
				}
			}

			return false;
		}

		public static (bool, List<long>) ChkUpgradePlayerTowerWhenExistXXX(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId, string needTowerCfgId, int needCount, Dictionary<string, int> costPoolTowers)
		{
			string msg = "";
			MultiMap<string, long> existTowerDic = self.GetExistTowerList(playerId, towerUnitId, needTowerCfgId);

			(string baseTowerCfgId, int costCount) = self.GetBaseTowerCfgIdAndCount(needTowerCfgId);
			int needCostBaseTowerCountOrg = needCount * costCount;
			int needCostBaseTowerCount = needCostBaseTowerCountOrg;
			foreach (var item in existTowerDic)
			{
				string towerCfgId = item.Key;
				(string baseTowerCfgIdTmp, int costCountTmp) = self.GetBaseTowerCfgIdAndCount(towerCfgId);
				needCostBaseTowerCount -= costCountTmp * item.Value.Count;
				if (needCostBaseTowerCount < 0)
				{
					break;
				}
			}
			if (needCostBaseTowerCount > 0)
			{
				return (false, null);
			}

			needCostBaseTowerCount = needCostBaseTowerCountOrg;
			List<long> existTowerUnitIds = ListComponent<long>.Create();
			Dictionary<string, int> costExistTowers = DictionaryComponent<string, int>.Create();
			bool bRet = self.ChkUpgradePlayerTowerWhenExistXXX222(baseTowerCfgId, needCostBaseTowerCount, costPoolTowers, costExistTowers, existTowerDic);
			if (bRet == false)
			{
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CannotLevelUpTower", needCount);
				return (false, null);
			}

			foreach (var item in costExistTowers)
			{
				string towerCfgIdTmp = item.Key;
				int count = item.Value;
				foreach (long towerUnitIdTmp in existTowerDic[item.Key])
				{
					if (count > 0)
					{
						existTowerUnitIds.Add(towerUnitIdTmp);
					}
					else
					{
						break;
					}
					count--;
				}
			}

			return (true, existTowerUnitIds);
		}

		public static bool UpgradePlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId, bool onlyChkPool)
		{
			(bool bRet, string msg, Dictionary<string, int> costPoolTowers, List<long> existTowerUnitIds) = self.ChkUpgradePlayerTower(playerId, towerUnitId, onlyChkPool);
			if (bRet == false)
			{
				return false;
			}

			Unit curTownUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			string curTowerId = curTownUnit.GetComponent<TowerComponent>().towerCfgId;

			TowerDefense_TowerCfg nextTowerCfg = TowerDefense_TowerCfgCategory.Instance.GetNextTowerCfg(curTowerId);
			if (nextTowerCfg == null)
			{
				return false;
			}

			string nextTowerId = nextTowerCfg.Id;

			if (costPoolTowers != null)
			{
				foreach (var costTower in costPoolTowers)
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
			}

			if (existTowerUnitIds != null)
			{
				foreach (long existTowerUnitId in existTowerUnitIds)
				{
					self.playerId2unitTowerId.Remove(playerId, existTowerUnitId);
					self.playerId2unitAttackTowerId.Remove(playerId, existTowerUnitId);
					Unit existTowerUnit = UnitHelper.GetUnit(self.DomainScene(), existTowerUnitId);
					existTowerUnit.DestroyNotDeathShow();
				}
			}

			float3 position = curTownUnit.Position;
			float3 forward = curTownUnit.Forward;
			self.playerId2unitTowerId.Remove(playerId, towerUnitId);
			self.playerId2unitAttackTowerId.Remove(playerId, towerUnitId);
			curTownUnit.DestroyNotDeathShow();
			float navObstacleRadius = ET.Ability.UnitHelper.GetNavObstacleRadius(curTownUnit);
			float navObstacleHeight = ET.Ability.UnitHelper.GetNavObstacleHeight(curTownUnit);

			List<Unit> towerUnitList = self.CreateTower(playerId, nextTowerId, position, forward);
			foreach (var towerUnit in towerUnitList)
			{
				self.playerId2unitTowerId.Add(playerId, towerUnit.Id);

				if (ItemHelper.ChkIsAttackTower(nextTowerId))
				{
					self.playerId2unitAttackTowerId.Add(playerId, towerUnit.Id);
				}

				towerUnit.AddComponent<UnitResetNavObstacleComponent, float, float>(navObstacleRadius, navObstacleHeight);

				EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_UpgradeTower()
				{
					playerId = playerId,
					oldTowerUnit = curTownUnit,
					oldTowerCfgId = curTowerId,
					newTowerUnit = towerUnit,
					newTowerCfgId = nextTowerId,
				});

			}

			self.NoticeToClient(playerId);
			return true;
		}

		public static bool UpgradePlayerTower(this PlayerOwnerTowersComponent self, long playerId, string towerCfgId, bool onlyChkPool)
		{
			(bool bRet, string msg, Dictionary<string, int> costPoolTowers, List<long> existTowerUnitIds) = self.ChkUpgradePlayerTower(playerId, 0, towerCfgId, onlyChkPool);
			if (bRet == false)
			{
				return false;
			}

			string curTowerId = towerCfgId;

			TowerDefense_TowerCfg nextTowerCfg = TowerDefense_TowerCfgCategory.Instance.GetNextTowerCfg(curTowerId);
			if (nextTowerCfg == null)
			{
				return false;
			}

			string nextTowerId = nextTowerCfg.Id;

			if (costPoolTowers != null)
			{
				foreach (var costTower in costPoolTowers)
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
			}

			if (existTowerUnitIds != null)
			{
			}

			if (self.playerOwnerTowerId.TryGetValue(playerId, nextTowerId, out int ownCount) == false)
			{
				self.playerOwnerTowerId.Add(playerId, nextTowerId, 1);
			}
			else
			{
				self.playerOwnerTowerId[playerId][nextTowerId] += 1;
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
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold, scaleTowerCostGold);

			EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_ScaleTower()
			{
				playerId = playerId,
				towerUnit = curTownUnit,
				towerCfgId = curTowerId,
			});

			self.playerId2unitTowerId.Remove(playerId, towerUnitId);
			self.playerId2unitAttackTowerId.Remove(playerId, towerUnitId);
			curTownUnit.DestroyNotDeathShow();

			self.NoticeToClient(playerId);

			return true;
		}

		public static bool ScalePlayerTowerCard(this PlayerOwnerTowersComponent self, long playerId, string towerCfgId)
		{
			if (self.playerOwnerTowerId.TryGetValue(playerId, towerCfgId, out int count) == false)
			{
				Log.Debug($"playerId[{playerId}], towerUnitId[{towerCfgId}] not exist in self.playerOwnerTowerId");
				return false;
			}

			if (count <= 0)
			{
				Log.Debug($"playerId[{playerId}], the count of towerUnitId[{towerCfgId}] is less than 0");
				return false;
			}

			TowerDefense_TowerCfg curTowerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
			int scaleTowerCostGold = curTowerCfg.ScaleTowerCostGold;
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold, scaleTowerCostGold);

			// EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_ScaleTower()
			// {
			// 	playerId = playerId,
			// 	towerUnit = curTownUnit,
			// 	towerCfgId = curTowerId,
			// });

			self.playerOwnerTowerId[playerId][towerCfgId] = count - 1;

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
			float curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold);
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
			// int curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold);
			// if (curGold < reclaimTowerCostGold)
			// {
			// 	Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] curGold[{curGold}] < reclaimTowerCostGold[{reclaimTowerCostGold}]");
			// 	return false;
			// }
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold, -reclaimTowerCostGold);

			if (self.playerOwnerTowerId.TryGetValue(playerId, curTowerId, out int ownCount) == false)
			{
				self.playerOwnerTowerId.Add(playerId, curTowerId, 1);
			}
			else
			{
				self.playerOwnerTowerId[playerId][curTowerId] += 1;
			}

			EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_ReclaimTower()
			{
				playerId = playerId,
				towerUnit = curTownUnit,
				towerCfgId = curTowerId,
			});

			self.playerId2unitTowerId.Remove(playerId, towerUnitId);
			self.playerId2unitAttackTowerId.Remove(playerId, towerUnitId);
			curTownUnit.DestroyNotDeathShow();

			self.NoticeToClient(playerId);
			return true;
		}

		public static (bool, string) ChkMovePlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId, float3 position)
		{
			string msg = "";
			if (self.playerId2unitTowerId.Contains(playerId, towerUnitId) == false)
			{
				Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] not exist in self.playerId2unitTowerId");
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_NotHaveThisTower");
				return (false, msg);
			}

			return (true, msg);
		}

		public static bool ChkMovePlayerTowerNeedDownTower(this PlayerOwnerTowersComponent self, long towerUnitId, float3 position)
		{
			Unit curUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			float3 curUnitPos = curUnit.Position;
			float curUnitHeight = UnitHelper.GetBodyHeight(curUnit);
			float curUnitRadius = UnitHelper.GetBodyRadius(curUnit);
			long ignoreTowerUnitId = curUnit.Id;

			var unitList = self.GetTowerOnceWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitId);
			if (unitList.Count == 0)
			{
				return false;
			}
			curUnitPos = position;
			var unitListNew = self.GetTowerOnceWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitId);
			foreach (Unit unit in unitList)
			{
				if (unitListNew.Contains(unit) == false)
				{
					return true;
				}
			}

			return false;
		}

		public static bool MovePlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId, float3 position, float3 forward)
		{
			(bool bRet, string msg) = self.ChkMovePlayerTower(playerId, towerUnitId, position);
			if (bRet == false)
			{
				return false;
			}

			Unit curTownUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			string curTowerId = curTownUnit.GetComponent<TowerComponent>().towerCfgId;

			ET.Ability.UnitHelper.ResetPos(curTownUnit, position, forward);

			EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_MoveTower()
			{
				playerId = playerId,
				towerUnit = curTownUnit,
				towerCfgId = curTowerId,
			});

			//self.NoticeToClient(playerId);
			return true;
		}

		public static bool GetUpgradePlayerTowerPoolCost(this PlayerOwnerTowersComponent self, long playerId, ref string needTowerCfgId, ref int needCount, ref Dictionary<string, int> costPoolTowers)
		{
			if (self.playerOwnerTowerId.TryGetValue(playerId, needTowerCfgId, out int count) == false)
			{
				count = 0;
			}
			if (count >= needCount)
			{
				costPoolTowers[needTowerCfgId] = needCount;
				needCount = 0;
				return true;
			}
			else
			{
				costPoolTowers[needTowerCfgId] = count;
				needCount -= count;

				TowerDefense_TowerCfg preTowerCfg = TowerDefense_TowerCfgCategory.Instance.GetPreTowerCfg(needTowerCfgId);
				if (preTowerCfg != null)
				{
					string preTowerId = preTowerCfg.Id;
					needCount *= preTowerCfg.NewTowerCostCount;
					needTowerCfgId = preTowerId;
					return self.GetUpgradePlayerTowerPoolCost(playerId, ref needTowerCfgId, ref needCount, ref costPoolTowers);
				}
				else
				{
					return false;
				}

			}
		}

		public static (string, int) GetBaseTowerCfgIdAndCount(this PlayerOwnerTowersComponent self, string curTowerCfgId)
		{
			return TowerDefense_TowerCfgCategory.Instance.GetBaseTowerCfgIdAndCount(curTowerCfgId);
		}

		public static MultiMap<string, long> GetExistTowerList(this PlayerOwnerTowersComponent self, long playerId, long curTowerUnitId, string curTowerCfgId)
		{
			(string baseTowerCfgId, _) = self.GetBaseTowerCfgIdAndCount(curTowerCfgId);

			if (self.existTowerDic == null)
			{
				self.existTowerDic = new();
			}
			else
			{
				self.existTowerDic.Clear();
			}
			MultiMap<string, long> existTowerDic = self.existTowerDic;
			foreach (var towerUnitId in self.playerId2unitTowerId[playerId])
			{
				if (curTowerUnitId == towerUnitId)
				{
					continue;
				}
				Unit towerUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
				if (towerUnit == null)
				{
					continue;
				}

				TowerComponent towerComponent = towerUnit.GetComponent<TowerComponent>();
				if (towerComponent == null)
				{
					continue;
				}
				string towerCfgId = towerComponent.towerCfgId;
				if (TowerDefense_TowerCfgCategory.Instance.ChkIsSameBaseTowerCfg(baseTowerCfgId, towerCfgId))
				{
					existTowerDic.Add(towerCfgId, towerUnitId);
				}
			}
			return existTowerDic;
		}

		public static List<long> GetPutTowers(this PlayerOwnerTowersComponent self, long playerId)
		{
			return self.playerId2unitTowerId[playerId];
		}

		public static Dictionary<string, int> GetPlayerOwnerTowers(this PlayerOwnerTowersComponent self, long playerId)
		{
			if (self.playerOwnerTowerId.TryGetValue(playerId, out var playerOwnerTowers))
			{
			}
			return playerOwnerTowers;
		}

		public static bool ChkIsNearTower(this PlayerOwnerTowersComponent self, float3 targetPos, float targetUnitRadius, long playerId = (long)ET.PlayerId.PlayerNone, long ignoreTowerUnitId = -1)
		{
			float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene());
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
			var homeUnitList = putHomeComponent.GetHomeUnitList();
			float nearDisBase = 0f;
			foreach (var homeUnits in homeUnitList)
			{
				if (playerId != (long)ET.PlayerId.PlayerNone)
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
					float nearDis = nearDisBase + GlobalSettingCfgCategory.Instance.TowerDefenseNearDisWhenHome * resScale;
					bool isNear1 = UnitHelper.ChkIsNear(homeUnit, targetPos, targetUnitRadius, nearDis, false);
					if (isNear1)
					{
						return true;
					}
				}
			}

			PutMonsterCallComponent putMonsterCallComponent = gamePlayTowerDefenseComponent.GetComponent<PutMonsterCallComponent>();
			if (putMonsterCallComponent != null)
			{
				var monsterCallUnitList = putMonsterCallComponent.GetMonsterCallUnitList();
				nearDisBase = 0f;
				foreach (var item in monsterCallUnitList)
				{
					if (playerId != (long)ET.PlayerId.PlayerNone)
					{
						if (playerId != item.Key)
						{
							continue;
						}
					}
					long monsterCallUnitId = item.Value;
					Unit monsterCallUnit = UnitHelper.GetUnit(self.DomainScene(), monsterCallUnitId);
					if (monsterCallUnit != null)
					{
						float nearDis = nearDisBase + GlobalSettingCfgCategory.Instance.TowerDefenseNearDisWhenMonsterCall * resScale;
						bool isNear1 = UnitHelper.ChkIsNear(monsterCallUnit, targetPos, targetUnitRadius, nearDis, false);
						if (isNear1)
						{
							return true;
						}
					}
				}
			}

			foreach (var list in self.playerId2unitTowerId)
			{
				if (playerId != (long)ET.PlayerId.PlayerNone && playerId != list.Key)
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
					if (ignoreTowerUnitId != -1 && ignoreTowerUnitId == unit.Id)
					{
						continue;
					}
					float nearDis = 0;
					if (UnitHelper.ChkIsAttackTower(unit))
					{
						nearDis = nearDisBase + GlobalSettingCfgCategory.Instance.TowerDefenseNearDisWhenAttackTower * resScale;
					}
					else if (UnitHelper.ChkIsTrapTower(unit))
					{
						nearDis = nearDisBase + GlobalSettingCfgCategory.Instance.TowerDefenseNearDisWhenTrapTower * resScale;
					}
					else if (UnitHelper.ChkIsColliderTower(unit))
					{
						nearDis = nearDisBase + GlobalSettingCfgCategory.Instance.TowerDefenseNearDisWhenColliderTower * resScale;
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

		public static HashSet<Unit> GetTowerListWhenStackedOnTop(this PlayerOwnerTowersComponent self, Unit curUnit)
		{
			float3 curUnitPos = curUnit.Position;
			float curUnitHeight = UnitHelper.GetBodyHeight(curUnit);
			float curUnitRadius = UnitHelper.GetBodyRadius(curUnit);
			long ignoreTowerUnitId = curUnit.Id;

			return self.GetTowerListWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitId);
		}

		public static HashSet<Unit> GetTowerListWhenStackedOnTop(this PlayerOwnerTowersComponent self, float3 curUnitPos, float curUnitHeight, float curUnitRadius, long ignoreTowerUnitId)
		{
			HashSet<Unit> list = HashSetComponent<Unit>.Create();
			do
			{
				var unitList = self.GetTowerOnceWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitId);
				if (unitList.Count == 0)
				{
					break;
				}
				foreach (Unit unit in unitList)
				{
					if (unit != null)
					{
						list.Add(unit);
						curUnitPos = unit.Position;
						curUnitHeight = UnitHelper.GetBodyHeight(unit);
						curUnitRadius = UnitHelper.GetBodyRadius(unit);
						ignoreTowerUnitId = unit.Id;
					}
					else
					{
						break;
					}
				}
			}
			while (true);

			return list;
		}

		public static HashSet<Unit> GetTowerOnceWhenStackedOnTop(this PlayerOwnerTowersComponent self, float3 curUnitPos, float curUnitHeight, float curUnitRadius, long ignoreTowerUnitId)
		{
			HashSet<Unit> unitList = HashSetComponent<Unit>.Create();
			foreach (var list in self.playerId2unitTowerId)
			{
				foreach (long unitId in list.Value)
				{
					Unit unit = UnitHelper.GetUnit(self.DomainScene(), unitId);
					if (UnitHelper.ChkUnitAlive(unit, false) == false)
					{
						continue;
					}
					if (ignoreTowerUnitId != -1 && ignoreTowerUnitId == unit.Id)
					{
						continue;
					}
					bool isNear = UnitHelper.ChkIsStackedOnTop(curUnitPos, curUnitHeight, unit, curUnitRadius);
					if (isNear)
					{
						unitList.Add(unit);
					}
				}
			}

			return unitList;
		}

		public static bool DestroyPlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId)
		{
			if (self.playerId2unitTowerId.Contains(playerId, towerUnitId) == false)
			{
				Log.Debug($"playerId[{playerId}], towerUnitId[{towerUnitId}] not exist in self.playerId2unitTowerId");
				return false;
			}

			Unit curTownUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			string curTowerId = curTownUnit.GetComponent<TowerComponent>().towerCfgId;

			EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_TowerBeKill()
			{
				playerId = playerId,
				towerUnit = curTownUnit,
				towerCfgId = curTowerId,
			});

			self.playerId2unitTowerId.Remove(playerId, towerUnitId);
			self.playerId2unitAttackTowerId.Remove(playerId, towerUnitId);

			self.NoticeToClient(playerId);

			return true;
		}

		public static (bool, string) ChkUpgradeItemUnit(this PlayerOwnerTowersComponent self, long playerId, long itemUnitId, int nextLevel, string itemGiftCfgId)
		{
			string msg = "";

			Unit itemUnit = UnitHelper.GetUnit(self.DomainScene(), itemUnitId);
			if (itemUnit == null)
			{
				msg = "UpgradeItemUnit itemUnit not exist";
				return (false, msg);
			}
			ItemUpgradeComponent itemUpgradeComponent = itemUnit.GetComponent<ItemUpgradeComponent>();
			if (itemUpgradeComponent == null)
			{
				msg = $"UpgradeItemUnit ItemUpgradeComponent not exist";
				return (false, msg);
			}

			string itemCfgId = itemUpgradeComponent.itemCfgId;
			if (ItemHelper.ChkIsHeadQuarter(itemCfgId) == false)
			{
				long itemPlayerId = TeamFlagHelper.GetPlayerId(itemUnit);
				if (playerId != itemPlayerId)
				{
					msg = $"playerId[{playerId}] != itemPlayerId[{itemPlayerId}]";
					return (false, msg);
				}
			}

			var result = itemUpgradeComponent.ChkCanUpgrade(nextLevel, itemGiftCfgId);
			if (result.bRet == false)
			{
				return (false, result.msg);
			}

			(int costGold, int costCard) = itemUpgradeComponent.GetUpgradeCost();
			if (costGold > 0)
			{
				float curGold = ET.GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold);
				if (curGold < costGold)
				{
					msg = "Gold not enough";
					return (false, msg);
				}
			}

			if (costCard > 0)
			{
				if (self.playerOwnerTowerId.TryGetValue(playerId, itemCfgId, out int count) == false)
				{
					msg = "card not enough";
					return (false, msg);
				}
				if (count < costCard)
				{
					msg = "card not enough";
					return (false, msg);
				}

			}

			return (true, "");
		}

		public static bool UpgradeItemUnit(this PlayerOwnerTowersComponent self, long playerId, long itemUnitId, int nextLevel, string itemGiftCfgId)
		{
			(bool bRet, string msg) = self.ChkUpgradeItemUnit(playerId, itemUnitId, nextLevel, itemGiftCfgId);
			if (bRet == false)
			{
				return false;
			}

			Unit itemUnit = UnitHelper.GetUnit(self.DomainScene(), itemUnitId);
			ItemUpgradeComponent itemUpgradeComponent = itemUnit.GetComponent<ItemUpgradeComponent>();
			string itemCfgId = itemUpgradeComponent.itemCfgId;
			(int costGold, int costCard) = itemUpgradeComponent.GetUpgradeCost();
			if (costGold > 0)
			{
				ET.GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold, -costGold);
			}

			if (costCard > 0)
			{
				if (self.playerOwnerTowerId.TryGetValue(playerId, itemCfgId, out int count))
				{
					self.playerOwnerTowerId[playerId][itemCfgId] = count - costCard;
				}
			}

			itemUpgradeComponent.Upgrade(nextLevel, itemGiftCfgId);

			Ability.UnitHelper.AddSyncData_UnitComponent(itemUnit, itemUpgradeComponent.GetType());

			ItemGiftCfg itemGiftCfg = ItemGiftCfgCategory.Instance.Get(itemGiftCfgId);
			ET.GamePlayHelper.DoCreateActions(itemUnit, itemGiftCfg.ActionIds, playerId).Coroutine();

			EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_UpgradeItemUnit()
			{
				playerId = playerId,
				itemUnit = itemUnit,
				itemCfgId = itemCfgId,
			});

			self.NoticeToClient(playerId);
			return true;
		}

	}
}