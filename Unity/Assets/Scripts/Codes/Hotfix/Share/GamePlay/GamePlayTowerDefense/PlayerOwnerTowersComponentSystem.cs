﻿using ET.Ability;
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
			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				self.RefreshPlayerTowerPool(playerId);
				self.playerRefreshTowerCost[playerId] = gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost;
				self.playerOwnerTowerId[playerId] = new();
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
						if (towerPools.TryGetValue(item.TowerCfgId, out int weight))
						{
							weight += item.Weight;
							towerPools[item.TowerCfgId] = weight;
						}
						else
						{
							weight = item.Weight;
							towerPools.Add(item.TowerCfgId, weight);
						}
					}
				}
			}

			if (self.playerOwnerTowerCardIds.TryGetValue(playerId, out var ownerTowerCardIds))
			{
				int myTowersWeight = towerDefense_BuyTowerRefreshRuleCfg.MyTowersWeight;
				foreach (var item in ownerTowerCardIds)
				{
					if (towerPools.TryGetValue(item.Key, out int weight))
					{
						weight += myTowersWeight;
						towerPools[item.Key] = weight;
					}
					else
					{
						weight = myTowersWeight;
						towerPools.Add(item.Key, weight);
					}
				}
			}

			for (int i = 0; i < count; i++)
			{
				string towerCfgId = ET.RandomGenerator.GetRandomIndexLinear(towerPools);
				self.playerTowerBuyPools.Add(playerId, towerCfgId);
				self.playerTowerBuyPoolBoughts.Add(playerId, false);

				TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
				int costGold = towerCfg.BuyTowerCostGold;

				GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerTowerPriceBase, costGold, true);
				float newCostGold = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerTowerPrice);

				self.playerTowerBuyPoolCosts.Add(playerId, (int)newCostGold);
			}

			EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_RefreshTowerBuyPool()
			{
				playerId = playerId,
			});

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

			string towerCfgId = self.playerTowerBuyPools[playerId][index];

			int costGold = self.playerTowerBuyPoolCosts[playerId][index];
			bool success = self.CostWhenBuyTower(playerId, costGold);
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
			return (true, msg);;
		}

		public static List<int> GetPlayerTowerPrice(this PlayerOwnerTowersComponent self, long playerId)
		{
			return self.playerTowerBuyPoolCosts[playerId];
		}

		public static void SetPutAttackTowerLimitCount(this PlayerOwnerTowersComponent self, long playerId, GameNumericType gameNumericType, float chgValue)
		{
			if (gameNumericType == GameNumericType.TowerDefense_PlayerLimitTowerCountAdd ||
			    gameNumericType == GameNumericType.TowerDefense_PlayerLimitTowerCountPct ||
			    gameNumericType == GameNumericType.TowerDefense_PlayerLimitTowerCountFinalAdd ||
			    gameNumericType == GameNumericType.TowerDefense_PlayerLimitTowerCountFinalPct)
			{
			}
			else
			{
				return;
			}
			GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, gameNumericType, chgValue, true);
			float newLimitTowerCount = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerLimitTowerCount);

			self.playerId2unitAttackTowerLimitCount[playerId] = (int)newLimitTowerCount;
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
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CannotLevelUpTower");
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
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CannotLevelUpTower");
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
				msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CannotLevelUpTower");
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
			self.playerId2unitTowerId.Remove(playerId, towerUnitId);
			self.playerId2unitAttackTowerId.Remove(playerId, towerUnitId);
			curTownUnit.DestroyNotDeathShow();

			List<Unit> towerUnitList = self.CreateTower(playerId, nextTowerId, position);
			foreach (var towerUnit in towerUnitList)
			{
				self.playerId2unitTowerId.Add(playerId, towerUnit.Id);

				if (ItemHelper.ChkIsAttackTower(nextTowerId))
				{
					self.playerId2unitAttackTowerId.Add(playerId, towerUnit.Id);
				}

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

		public static bool MovePlayerTower(this PlayerOwnerTowersComponent self, long playerId, long towerUnitId, float3 position)
		{
			(bool bRet, string msg) = self.ChkMovePlayerTower(playerId, towerUnitId, position);
			if (bRet == false)
			{
				return false;
			}

			Unit curTownUnit = UnitHelper.GetUnit(self.DomainScene(), towerUnitId);

			ET.Ability.UnitHelper.ResetPos(curTownUnit, position, float3.zero);


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

		public static bool ChkIsNearTower(this PlayerOwnerTowersComponent self, float3 targetPos, float targetUnitRadius, long playerId = -1, long ignoreTowerUnitId = -1)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
			var homeUnitList = putHomeComponent.GetHomeUnitList();
			//float nearDis = 0.3f;
			float nearDis = GlobalSettingCfgCategory.Instance.TowerDefenseNearTowerDis;
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
					if (ignoreTowerUnitId != -1 && ignoreTowerUnitId == unit.Id)
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

	}
}