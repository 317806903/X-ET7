using ET.AbilityConfig;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    public static class PlayerCacheHelper
    {
	    public static PlayerCacheManagerComponent GetPlayerCacheManager(Scene scene)
	    {
		    PlayerCacheManagerComponent playerCacheManagerComponent = scene.GetComponent<PlayerCacheManagerComponent>();
		    if (playerCacheManagerComponent == null)
		    {
			    playerCacheManagerComponent = scene.AddComponent<PlayerCacheManagerComponent>();
		    }
		    return playerCacheManagerComponent;
	    }

	    public static PlayerDataComponent GetPlayerCache(Scene scene, long playerId)
	    {
		    PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);
		    PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
		    if (playerDataComponent == null)
		    {
			    playerDataComponent = playerCacheManagerComponent.AddChildWithId<PlayerDataComponent>(playerId);
		    }
		    return playerDataComponent;
	    }

        public static async ETTask<Entity> GetPlayerModel(Scene scene, long playerId, PlayerModelType playerModelType, bool forceReGet)
        {
	        PlayerDataComponent playerDataComponent = GetPlayerCache(scene, playerId);

	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.PlayerCache, playerId))
	        {
		        Entity entity = playerDataComponent.GetPlayerModel(playerModelType);
		        if (entity == null || forceReGet)
		        {
			        (bool bRet, byte[] playerModelComponentBytes) = await SendGetPlayerModelAsync(scene, playerId, playerModelType);
			        if (bRet)
			        {
				        Entity entityModel = playerDataComponent.SetPlayerModel(playerModelType, playerModelComponentBytes, null);
				        entityModel.SetDataCacheAutoClear();
				        entity = entityModel;
			        }
		        }
		        return entity;
	        }
        }

        public static async ETTask<PlayerBaseInfoComponent> GetPlayerBaseInfoByPlayerId(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await GetPlayerModel(scene, playerId, PlayerModelType.BaseInfo, forceReGet);
	        return entity as PlayerBaseInfoComponent;
        }

        public static async ETTask<PlayerBackPackComponent> GetPlayerBackPackByPlayerId(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await GetPlayerModel(scene, playerId, PlayerModelType.BackPack, forceReGet);
	        return entity as PlayerBackPackComponent;
        }

        public static async ETTask<PlayerBattleCardComponent> GetPlayerBattleCardByPlayerId(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await GetPlayerModel(scene, playerId, PlayerModelType.BattleCard, forceReGet);
	        return entity as PlayerBattleCardComponent;
        }

        public static async ETTask<PlayerOtherInfoComponent> GetPlayerOtherInfoByPlayerId(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await GetPlayerModel(scene, playerId, PlayerModelType.OtherInfo, forceReGet);
	        return entity as PlayerOtherInfoComponent;
        }

        public static async ETTask<PlayerSeasonInfoComponent> GetPlayerSeasonInfoByPlayerId(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await GetPlayerModel(scene, playerId, PlayerModelType.SeasonInfo, forceReGet);
	        return entity as PlayerSeasonInfoComponent;
        }

        public static async ETTask<PlayerFunctionMenuComponent> GetPlayerFunctionMenuByPlayerId(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await GetPlayerModel(scene, playerId, PlayerModelType.FunctionMenu, forceReGet);
	        return entity as PlayerFunctionMenuComponent;
        }

        public static async ETTask<PlayerMailComponent> GetPlayerMailByPlayerId(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await GetPlayerModel(scene, playerId, PlayerModelType.Mails, forceReGet);
	        return entity as PlayerMailComponent;
        }

        public static async ETTask<List<ItemComponent>> GetBattleCardItemListByPlayerId(Scene scene, long playerId, bool forceReGet = false)
        {
	        PlayerBackPackComponent playerBackPackComponent = await GetPlayerBackPackByPlayerId(scene, playerId, forceReGet);
	        PlayerBattleCardComponent playerBattleCardComponent = await GetPlayerBattleCardByPlayerId(scene, playerId, forceReGet);

	        bool isNeedChg = playerBattleCardComponent.SetBattleCardItemCfgIdList(playerBackPackComponent.GetItemListByItemType(ItemType.Tower, ItemSubType.None));
	        if (isNeedChg)
	        {
		        await SavePlayerModel(scene, playerId, PlayerModelType.BattleCard, null, PlayerModelChgType.PlayerBattleCard_AutoSetByBackPack);
	        }

	        List<ItemComponent> list = ListComponent<ItemComponent>.Create();
	        foreach (var itemCfgId in playerBattleCardComponent.GetBattleCardItemCfgIdList())
	        {
		        ItemComponent itemComponent = playerBackPackComponent.GetItemWhenStack(itemCfgId);
		        list.Add(itemComponent);
	        }
	        return list;
        }

        public static async ETTask<int> GetTokenValueByPlayerId(Scene scene, long playerId, ItemSubType itemSubType, bool forceReGet = false)
        {
	        PlayerBackPackComponent playerBackPackComponent = await GetPlayerBackPackByPlayerId(scene, playerId, forceReGet);
	        var itemList = playerBackPackComponent.GetItemListByItemType(ItemType.Token, itemSubType);
	        if (itemList == null || itemList.Count == 0)
	        {
		        return 0;
	        }

	        return itemList[0].GetCount();
        }

        public static async ETTask<int> GetTokenDiamondByPlayerId(Scene scene, long playerId, bool forceReGet = false)
        {
			 return await GetTokenValueByPlayerId(scene, playerId, ItemSubType.Diamond, forceReGet);
        }

        public static async ETTask<int> GetTokenArcadeCoinByPlayerId(Scene scene, long playerId, bool forceReGet = false)
        {
	        return await GetTokenValueByPlayerId(scene, playerId, ItemSubType.ArcadeCoin, forceReGet);
        }

        public static async ETTask SetPlayerModelByClient(Scene scene, long playerId, PlayerModelType playerModelType, byte[] bytes, List<string> setPlayerKeys)
        {
	        PlayerDataComponent playerDataComponent = GetPlayerCache(scene, playerId);

	        Entity entityModel = playerDataComponent.SetPlayerModel(playerModelType, bytes, setPlayerKeys);
	        entityModel.SetDataCacheAutoClear();
	        await ETTask.CompletedTask;
        }

        public static async ETTask SavePlayerModel(Scene scene, long playerId, PlayerModelType playerModelType, List<string> setPlayerKeys, PlayerModelChgType playerModelChgType)
        {
	        Entity entityModel = await GetPlayerModel(scene, playerId, playerModelType, false);
	        entityModel.SetDataCacheAutoClear();
	        byte[] bytes = entityModel.ToBson();

	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.PlayerCache, playerId))
	        {
		        await SendSavePlayerModelAsync(scene, playerId, playerModelType, bytes, setPlayerKeys, playerModelChgType);
	        }

	        await NoticeClientPlayerCacheChg(scene, playerId, playerModelType);
	        await ETTask.CompletedTask;
        }

        public static async ETTask SavePlayerRank(Scene scene, long playerId, RankType rankType, long score, int killNum)
        {
	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Rank, playerId))
	        {
		        await SendSavePlayerRankAsync(scene, playerId, rankType, score, killNum);
	        }

	        if (rankType == RankType.PVE)
	        {
		        await NoticeClientPlayerCacheChg(scene, playerId, PlayerModelType.RankPVE);
	        }
	        else if (rankType == RankType.EndlessChallenge)
	        {
		        await NoticeClientPlayerCacheChg(scene, playerId, PlayerModelType.RankEndlessChallenge);
	        }
	        await ETTask.CompletedTask;
        }

        public static async ETTask<(bool, byte[])> SendGetPlayerModelAsync(Scene scene, long playerId, PlayerModelType playerModelType)
        {
	        int PlayerModelType = (int)playerModelType;

	        StartSceneConfig playerCacheSceneConfig = StartSceneConfigCategory.Instance.GetPlayerCacheManager(scene.DomainZone());

	        P2G_GetPlayerCache _P2G_GetPlayerCache = (P2G_GetPlayerCache) await ActorMessageSenderComponent.Instance.Call(playerCacheSceneConfig.InstanceId, new G2P_GetPlayerCache()
	        {
		        PlayerId = playerId,
		        PlayerModelType = PlayerModelType,
	        });

	        if (_P2G_GetPlayerCache.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendGetPlayerModelAsync Error==1 msg={_P2G_GetPlayerCache.Message}");
		        return (false, null);
	        }
	        else
	        {
		        byte[] playerModelComponentBytes = _P2G_GetPlayerCache.PlayerModelComponentBytes;
		        return (true, playerModelComponentBytes);
	        }
        }

        public static async ETTask<bool> SendSavePlayerModelAsync(Scene scene, long playerId, PlayerModelType playerModelType, byte[] playerModelComponentBytes, List<string> setPlayerKeys, PlayerModelChgType playerModelChgType)
        {
	        int PlayerModelType = (int)playerModelType;

	        StartSceneConfig playerCacheSceneConfig = StartSceneConfigCategory.Instance.GetPlayerCacheManager(scene.DomainZone());

	        P2G_SetPlayerCache _P2G_SetPlayerCache = (P2G_SetPlayerCache) await ActorMessageSenderComponent.Instance.Call(playerCacheSceneConfig.InstanceId, new G2P_SetPlayerCache()
	        {
		        PlayerId = playerId,
		        PlayerModelType = PlayerModelType,
		        PlayerModelComponentBytes = playerModelComponentBytes,
		        SetPlayerKeys = setPlayerKeys,
		        PlayerModelChgType = (int)playerModelChgType,
	        });

	        if (_P2G_SetPlayerCache.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendSavePlayerModelAsync Error==1 msg={_P2G_SetPlayerCache.Message}");
		        return false;
	        }
	        else
	        {
		        return true;
	        }
        }

        public static async ETTask<bool> SendSavePlayerRankAsync(Scene scene, long playerId, RankType rankType, long score, int killNum)
        {
	        StartSceneConfig rankSceneConfig = StartSceneConfigCategory.Instance.GetRankManager(scene.DomainZone());

	        R2G_SetPlayerRank _R2G_SetPlayerRank = (R2G_SetPlayerRank) await ActorMessageSenderComponent.Instance.Call(rankSceneConfig.InstanceId, new G2R_SetPlayerRank()
	        {
		        PlayerId = playerId,
		        RankType = (int)rankType,
		        Score = score,
		        KillNum = killNum,
	        });

	        if (_R2G_SetPlayerRank.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendSavePlayerRankAsync Error==1 msg={_R2G_SetPlayerRank.Message}");
		        return false;
	        }
	        else
	        {
		        return true;
	        }
        }

		public static async ETTask AddPhysicalStrenth(Scene scene, long playerId, int chgValue)
        {
	        if (chgValue < 0)
	        {
		        Log.Error($"The chgValue cannot be negative, chgValue:{chgValue}");
		        return;
	        }

	        PlayerBaseInfoComponent playerBaseInfoComponent = await GetPlayerModel(scene, playerId, PlayerModelType.BaseInfo, true) as PlayerBaseInfoComponent;
			playerBaseInfoComponent.ChgPhysicalStrength(chgValue);
			PlayerModelChgType playerModelChgType = PlayerModelChgType.PlayerBaseInfo_AddPhysical;
			await SavePlayerModel(scene, playerId, PlayerModelType.BaseInfo, new() { "physicalStrength", "nextRecoverPhysicalTime"}, playerModelChgType);
	        await ETTask.CompletedTask;
        }

		public static async ETTask ReducePhysicalStrenth(Scene scene, long playerId, int chgValue)
		{
			if (chgValue < 0)
			{
				Log.Error($"The chgValue cannot be negative, chgValue:{chgValue}");
				return;
			}

			PlayerBaseInfoComponent playerBaseInfoComponent = await GetPlayerModel(scene, playerId, PlayerModelType.BaseInfo, true) as PlayerBaseInfoComponent;
			playerBaseInfoComponent.ChgPhysicalStrength(-chgValue);

			PlayerModelChgType playerModelChgType = PlayerModelChgType.PlayerBaseInfo_ReducePhysical;
			await SavePlayerModel(scene, playerId, PlayerModelType.BaseInfo, new() { "physicalStrength", "nextRecoverPhysicalTime"}, playerModelChgType);
			await ETTask.CompletedTask;
		}

		/// <summary>
		/// 重置玩家的所有养成等级
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="playerId"></param>
		/// <returns>需要返还的钻石</returns>
		public static async ETTask<int> ResetAllPowerup(Scene scene, long playerId)
		{
			PlayerSeasonInfoComponent playerSeasonInfoComponent = await GetPlayerSeasonInfoByPlayerId(scene, playerId);
			int reward = await playerSeasonInfoComponent.GetSeasonBringupReward();
			await playerSeasonInfoComponent.ResetSeasonBringUpDic();

            PlayerModelChgType playerModelChgType = PlayerModelChgType.PlayerSeasonInfo_PowerUP;
            await SavePlayerModel(scene, playerId, PlayerModelType.SeasonInfo, new() { "seasonBringUpDic" }, playerModelChgType);
			return reward;
        }

        /// <summary>
        ///  升级养成
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="playerId">玩家id</param>
        /// <param name="cfg">养成的配置id</param>
        /// <param name="playerlevel">升级到的等级</param>
        /// <returns>是否升级成功</returns>
        public static async ETTask<bool> UpdatePowerup(Scene scene, long playerId,string cfg)
        {
            PlayerSeasonInfoComponent playerSeasonInfoComponent = await GetPlayerSeasonInfoByPlayerId(scene, playerId);
			int playerBringUpLevel = playerSeasonInfoComponent.GetSeasonBringUpLevel(cfg);
            SeasonBringUpCfg seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetSeasonBringUpCfg(cfg, playerBringUpLevel);
			bool IsPlayerCanUpdate = await PlayerCacheHelper.IsPlayerCanUpdate(scene, playerId, cfg);
            if (IsPlayerCanUpdate)
			{
                bool isChg = playerSeasonInfoComponent.ChgSeasonBringUpDic(cfg, playerBringUpLevel + 1);
                if (isChg)
				{
					await ReduceTokenDiamond(scene, playerId, seasonBringUpCfg.Cost);
                    PlayerModelChgType playerModelChgType = PlayerModelChgType.PlayerSeasonInfo_PowerUP;
                    await SavePlayerModel(scene, playerId, PlayerModelType.SeasonInfo, new() { "seasonBringUpDic" }, playerModelChgType);
					return true;
                }
				else
				{
                    return false;
                }
            }
			else
			{
				return false;
			}
        }

        /// <summary>
        /// 玩家是否能升级
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="playerId"></param>
        /// <param name="seasonBringUpCfgId"></param>
        /// <returns></returns>
        public static async ETTask<bool> IsPlayerCanUpdate(Scene scene, long playerId, string seasonBringUpCfgId)
        {
            int maxLevel = SeasonBringUpCfgCategory.Instance.GetMaxLevel(seasonBringUpCfgId);

            PlayerSeasonInfoComponent playerSeasonInfoComponent = await GetPlayerSeasonInfoByPlayerId(scene, playerId);
            int playerBringUpLevel = playerSeasonInfoComponent.GetSeasonBringUpLevel(seasonBringUpCfgId);
            if (playerBringUpLevel >= maxLevel)
            {
	            return false;
            }

            int playerDiamond = await GetTokenDiamondByPlayerId(scene, playerId);
            SeasonBringUpCfg seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetSeasonBringUpCfg(seasonBringUpCfgId, playerBringUpLevel);

            bool isDiamondEnough = (seasonBringUpCfg.Cost <= playerDiamond);
            return isDiamondEnough;
        }

        /// <summary>
        /// 玩家是否能重置
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="playerId"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static async ETTask<bool> IsPlayerCanReset(Scene scene, long playerId)
        {
            PlayerSeasonInfoComponent playerSeasonInfoComponent = await GetPlayerSeasonInfoByPlayerId(scene, playerId);
            Dictionary<string, int> seasonBringUpDic = playerSeasonInfoComponent.GetSeasonBringUpDic();
            bool isPlayerBringupIsNone = true;
            foreach (KeyValuePair<string, int> kvp in seasonBringUpDic)
            {
                if (kvp.Value != 0)
                {
                    isPlayerBringupIsNone = false;
                }
            }
            int playerDiamond = await GetTokenDiamondByPlayerId(scene, playerId);
            SeasonComponent seasonComponent = await SeasonHelper.GetSeasonComponent(scene, false);
            int resetCost = seasonComponent.cfg.BringUpResetCost;
            bool IsCanReset = (resetCost <= playerDiamond) && !isPlayerBringupIsNone;

            return IsCanReset;
        }

        public static async ETTask AddTokenArcadeCoin(Scene scene, long playerId, int chgValue)

        {
	        if (chgValue < 0)
	        {
		        Log.Error($"The chgValue cannot be negative, chgValue:{chgValue}");
		        return;
	        }

	        string itemCfgId = ItemHelper.GetTokenArcadeCoinCfgId();
	        await AddItem(scene, playerId, itemCfgId, chgValue);

			await NoticeClientPlayerCacheChg(scene, playerId, PlayerModelType.TokenArcadeCoinAdd);

	        await ETTask.CompletedTask;
        }

		public static async ETTask ReduceTokenArcadeCoin(Scene scene, long playerId, int chgValue)
		{
			if (chgValue < 0)
			{
				Log.Error($"The chgValue cannot be negative, chgValue:{chgValue}");
				return;
			}

			string itemCfgId = ItemHelper.GetTokenArcadeCoinCfgId();
			await DeleteItem(scene, playerId, itemCfgId, chgValue);

			await NoticeClientPlayerCacheChg(scene, playerId, PlayerModelType.TokenArcadeCoinReduce);

			await ETTask.CompletedTask;
		}

		public static async ETTask AddTokenDiamond(Scene scene, long playerId, int chgValue)
		{
			if (chgValue < 0)
			{
				Log.Error($"The chgValue cannot be negative, chgValue:{chgValue}");
				return;
			}

			string itemCfgId = ItemHelper.GetTokenDiamondCfgId();
			await AddItem(scene, playerId, itemCfgId, chgValue);

			await NoticeClientPlayerCacheChg(scene, playerId, PlayerModelType.TokenDiamondAdd);

			await ETTask.CompletedTask;
		}

		public static async ETTask ReduceTokenDiamond(Scene scene, long playerId, int chgValue)
		{
			if (chgValue < 0)
			{
				Log.Error($"The chgValue cannot be negative, chgValue:{chgValue}");
				return;
			}

			string itemCfgId = ItemHelper.GetTokenDiamondCfgId();
			await DeleteItem(scene, playerId, itemCfgId, chgValue);

			await NoticeClientPlayerCacheChg(scene, playerId, PlayerModelType.TokenDiamondReduce);

			await ETTask.CompletedTask;
		}

		public static async ETTask AddItem(Scene scene, long playerId, string itemCfgId, int count)
		{
			if (count <= 0)
			{
				Log.Error($"Quantity must be positive, count:{count}");
				return;
			}

			PlayerBackPackComponent playerBackPackComponent =
					await GetPlayerModel(scene, playerId, PlayerModelType.BackPack, true) as
							PlayerBackPackComponent;
			playerBackPackComponent.AddItem(itemCfgId, count);

			PlayerModelChgType playerModelChgType = PlayerModelChgType.PlayerBackPack_AddItem;
			await SavePlayerModel(scene, playerId, PlayerModelType.BackPack, null, playerModelChgType);
			await ETTask.CompletedTask;
		}

		public static async ETTask DeleteItem(Scene scene, long playerId, string itemCfgId, int count)
		{
			if (count <= 0)
			{
				Log.Error($"Quantity must be positive, count:{count}");
				return;
			}

			PlayerBackPackComponent playerBackPackComponent =
					await GetPlayerModel(scene, playerId, PlayerModelType.BackPack, true) as
							PlayerBackPackComponent;
			playerBackPackComponent.AddItem(itemCfgId, -count);

			PlayerModelChgType playerModelChgType = PlayerModelChgType.PlayerBackPack_DeleteItem;
			await SavePlayerModel(scene, playerId, PlayerModelType.BackPack, null, playerModelChgType);
			await ETTask.CompletedTask;
		}

		public static async ETTask AddItems(Scene scene, long playerId, Dictionary<string, int> items)
		{
			PlayerBackPackComponent playerBackPackComponent =
					await GetPlayerModel(scene, playerId, PlayerModelType.BackPack, true) as
							PlayerBackPackComponent;

			foreach((string itemCfgId, int count) in items)
            {
				if (count <= 0)
				{
					Log.Error($"Quantity must be positive, count:{count}");
					continue;
				}
				playerBackPackComponent.AddItem(itemCfgId, count);
			}
			PlayerModelChgType playerModelChgType = PlayerModelChgType.PlayerBackPack_AddItem;
			await SavePlayerModel(scene, playerId, PlayerModelType.BackPack, null, playerModelChgType);
			await ETTask.CompletedTask;
		}

		public static async ETTask NoticeClientPlayerCacheChg(Scene scene, long playerId, PlayerModelType playerModelType)
		{
			long locationActorId = await LocationProxyComponent.Instance.Get(LocationType.Player, playerId, scene.InstanceId);
			if (locationActorId != 0)
			{
				O2G_PlayerCacheChgNoticeClient _O2G_PlayerCacheChgNoticeClient = new()
				{
					PlayerModelType = (int)playerModelType,
				};
				ActorLocationSenderOneType oneTypeLocationTypeTmp = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
				await oneTypeLocationTypeTmp.Call(playerId, _O2G_PlayerCacheChgNoticeClient, scene.InstanceId);
			}

			await ETTask.CompletedTask;
		}

		public static async ETTask DealPlayerFunctionMenuWaitChk(Scene scene, long playerId)
		{
			PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Server.PlayerCacheHelper.GetPlayerFunctionMenuByPlayerId(scene, playerId, true);

			bool isNeedSave = false;
			List<string> lockList = playerFunctionMenuComponent.GetLockFunctionMenuList();
			for (int i = 0; i < lockList.Count; i++)
			{
				string functionMenuCfgId = lockList[i];
				FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);
				if (functionMenuCfg.OpenCondition is FunctionMenuConditionDefault)
				{
					playerFunctionMenuComponent.ChgStatus(functionMenuCfgId, FunctionMenuStatus.Openned);
					isNeedSave = true;
				}
			}

			List<string> waitChkList = playerFunctionMenuComponent.GetWaitChkFunctionMenuList();
			for (int i = 0; i < waitChkList.Count; i++)
			{
				string functionMenuCfgId = waitChkList[i];
				FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);
				bool bRet = await ET.Server.PlayerCacheHelper.DealPlayerFunctionMenuOne(scene, playerId, functionMenuCfg, null);
				if (bRet)
				{
					playerFunctionMenuComponent.ChgStatus(functionMenuCfgId, FunctionMenuStatus.Openning);
				}
				else
				{
					playerFunctionMenuComponent.ChgStatus(functionMenuCfgId, FunctionMenuStatus.Lock);
				}
				isNeedSave = true;
			}
			if (isNeedSave)
			{
				await ET.Server.PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.FunctionMenu, null, PlayerModelChgType.PlayerFunctionMenu_DealWaitChg);
			}
		}

		public static async ETTask DealPlayerFunctionMenu(Scene scene, long playerId, Dictionary<string, int> paramDic)
		{
			PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Server.PlayerCacheHelper.GetPlayerFunctionMenuByPlayerId(scene, playerId, true);

			bool isChgFunctionMenu = false;
			List<string> lockList = playerFunctionMenuComponent.GetLockFunctionMenuList();

			for (int i = 0; i < lockList.Count; i++)
			{
				string functionMenuCfgId = lockList[i];
				FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);
				bool bRet = await ET.Server.PlayerCacheHelper.DealPlayerFunctionMenuOne(scene, playerId, functionMenuCfg, paramDic);
				if (bRet)
				{
					isChgFunctionMenu = true;
					playerFunctionMenuComponent.ChgStatus(functionMenuCfgId, FunctionMenuStatus.Openning);
				}
			}
			if (isChgFunctionMenu)
			{
				await ET.Server.PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.FunctionMenu, null, PlayerModelChgType.PlayerFunctionMenu_BattleEnd);
			}
		}

		public static async ETTask<bool> DealPlayerFunctionMenuOne(Scene scene, long playerId, FunctionMenuCfg functionMenuCfg, Dictionary<string, int> paramDic)
		{
			PlayerBaseInfoComponent playerBaseInfoComponent = await PlayerCacheHelper.GetPlayerBaseInfoByPlayerId(scene, playerId, true);
			if (functionMenuCfg.OpenCondition is FunctionMenuConditionDefault)
			{
				return true;
			}
			else if (functionMenuCfg.OpenCondition is FunctionMenuConditionBattleNumARAny functionMenuConditionBattleNumARAny)
			{
				int num = functionMenuConditionBattleNumARAny.BattleNum;
				if (playerBaseInfoComponent.ARPVEBattleCount >= num
				    || playerBaseInfoComponent.ARPVPBattleCount >= num
				    || playerBaseInfoComponent.AREndlessChallengeBattleCount >= num)
				{
					return true;
				}
			}
			else if (functionMenuCfg.OpenCondition is FunctionMenuConditionBattleNumARPVE functionMenuConditionBattleNumARPVE)
			{
				int num = functionMenuConditionBattleNumARPVE.BattleNum;
				if (playerBaseInfoComponent.ARPVEBattleCount >= num)
				{
					return true;
				}
			}
			else if (functionMenuCfg.OpenCondition is FunctionMenuConditionBattleNumARPVP functionMenuConditionBattleNumARPVP)
			{
				int num = functionMenuConditionBattleNumARPVP.BattleNum;
				if (playerBaseInfoComponent.ARPVPBattleCount >= num)
				{
					return true;
				}
			}
			else if (functionMenuCfg.OpenCondition is FunctionMenuConditionBattleNumAREndlessChallenge functionMenuConditionBattleNumAREndlessChallenge)
			{
				int num = functionMenuConditionBattleNumAREndlessChallenge.BattleNum;
				if (playerBaseInfoComponent.AREndlessChallengeBattleCount >= num)
				{
					return true;
				}
			}
			else if (functionMenuCfg.OpenCondition is FunctionMenuConditionTutorialFirstFinished functionMenuConditionTutorialFirstFinished)
			{
				if (playerBaseInfoComponent.isFinishTutorialFirst)
				{
					return true;
				}
			}
			else if (functionMenuCfg.OpenCondition is FunctionMenuConditionIndexWhenARPVE functionMenuConditionIndexWhenARPVE)
			{
				int index = functionMenuConditionIndexWhenARPVE.Index;
				if (playerBaseInfoComponent.ChallengeClearLevel >= index)
				{
					return true;
				}
			}
			else if (functionMenuCfg.OpenCondition is FunctionMenuConditionIndexWhenAREndlessChallenge functionMenuConditionIndexWhenAREndlessChallenge)
			{
				int index = functionMenuConditionIndexWhenAREndlessChallenge.Index;
				if (paramDic != null && paramDic.TryGetValue("EndlessChallengeWaveIndex", out int value))
				{
					if (value >= index)
					{
						return true;
					}
				}
			}

			return false;
		}

		public static async ETTask SetUIRedDotType(Scene scene, long playerId, UIRedDotType uiRedDotType, bool isNeedShow)
		{
			PlayerOtherInfoComponent playerOtherInfoComponent = await GetPlayerOtherInfoByPlayerId(scene, playerId, true);
			playerOtherInfoComponent.SetUIRedDotType(uiRedDotType, isNeedShow);

			PlayerModelChgType playerModelChgType = PlayerModelChgType.PlayerOtherInfo_SetUIRedDotType;
			await SavePlayerModel(scene, playerId, PlayerModelType.OtherInfo, new() { "uiRedDotTypes" }, playerModelChgType);
		}
    }
}