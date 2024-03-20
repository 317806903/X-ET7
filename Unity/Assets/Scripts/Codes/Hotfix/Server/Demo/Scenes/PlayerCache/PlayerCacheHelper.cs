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

        public static async ETTask<Entity> GetPlayerModel(Scene scene, long playerId, PlayerModelType playerModelType, bool forceReGet)
        {
	        PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);

	        PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
	        if (playerDataComponent == null)
	        {
		        playerDataComponent = playerCacheManagerComponent.AddChildWithId<PlayerDataComponent>(playerId);
	        }

	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.PlayerCache, playerId))
	        {
		        Entity entity = playerDataComponent.GetPlayerModel(playerModelType);
		        if (entity == null || forceReGet)
		        {
			        (bool bRet, Entity entityModel) = await SendGetPlayerModelAsync(scene, playerId, playerModelType);
			        if (bRet)
			        {
				        if (entity != null)
				        {
					        playerDataComponent.RemoveComponent(entity);
				        }
				        playerDataComponent.AddComponent(entityModel);
				        entityModel.AddComponent<DataCacheClearComponent>();
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

        public static async ETTask SetPlayerModelByClient(Scene scene, long playerId, PlayerModelType playerModelType, byte[] bytes, List<string> setPlayerKeys)
        {
	        PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);

	        PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
	        if (playerDataComponent == null)
	        {
		        playerDataComponent = playerCacheManagerComponent.AddChildWithId<PlayerDataComponent>(playerId);
	        }

	        Entity entityModel = playerDataComponent.SetPlayerModel(playerModelType, bytes, setPlayerKeys);
	        if (entityModel.GetComponent<DataCacheClearComponent>() == null)
	        {
		        entityModel.AddComponent<DataCacheClearComponent>();
	        }
	        await ETTask.CompletedTask;
        }

        public static async ETTask SavePlayerModel(Scene scene, long playerId, PlayerModelType playerModelType, List<string> setPlayerKeys)
        {
	        // PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);
	        //
	        // PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
	        // if (playerDataComponent == null)
	        // {
		       //  return;
	        // }

	        Entity entityModel = await GetPlayerModel(scene, playerId, playerModelType, false);
	        entityModel.GetComponent<DataCacheClearComponent>().RefreshTime();
	        byte[] bytes = entityModel.ToBson();

	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.PlayerCache, playerId))
	        {
		        await SendSavePlayerModelAsync(scene, playerId, playerModelType, bytes, setPlayerKeys);
	        }
	        await ETTask.CompletedTask;
        }

        public static async ETTask SavePlayerRank(Scene scene, long playerId, RankType rankType, long score, int killNum)
        {
	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Rank, playerId))
	        {
		        await SendSavePlayerRankAsync(scene, playerId, rankType, score, killNum);
	        }
	        await ETTask.CompletedTask;
        }

        public static async ETTask<(bool, Entity)> SendGetPlayerModelAsync(Scene scene, long playerId, PlayerModelType playerModelType)
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
		        Entity entity = MongoHelper.Deserialize<Entity>(playerModelComponentBytes);
		        return (true, entity);
	        }
        }

        public static async ETTask<bool> SendSavePlayerModelAsync(Scene scene, long playerId, PlayerModelType playerModelType, byte[] playerModelComponentBytes, List<string> setPlayerKeys)
        {
	        int PlayerModelType = (int)playerModelType;

	        StartSceneConfig playerCacheSceneConfig = StartSceneConfigCategory.Instance.GetPlayerCacheManager(scene.DomainZone());

	        P2G_SetPlayerCache _P2G_SetPlayerCache = (P2G_SetPlayerCache) await ActorMessageSenderComponent.Instance.Call(playerCacheSceneConfig.InstanceId, new G2P_SetPlayerCache()
	        {
		        PlayerId = playerId,
		        PlayerModelType = PlayerModelType,
		        PlayerModelComponentBytes = playerModelComponentBytes,
		        SetPlayerKeys = setPlayerKeys,
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

		public static async ETTask AddPhysicalStrenth(Scene scene, long playerId, int chgValue, PlayerModelChgType playerModelChgType)
        {
	        if (chgValue < 0)
	        {
		        Log.Error($"The chgValue cannot be negative, chgValue:{chgValue}");
		        return;
	        }
	        // PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);
	        //
	        // PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
	        // if (playerDataComponent == null)
	        // {
		       //  return;
	        // }

	        PlayerBaseInfoComponent playerBaseInfoComponent =
				await GetPlayerModel(scene, playerId, PlayerModelType.BaseInfo, true) as
					PlayerBaseInfoComponent;
			playerBaseInfoComponent.ChgPhysicalStrength(chgValue);

			await SavePlayerModel(scene, playerId, PlayerModelType.BaseInfo,
                        new() { "physicalStrength", "nextRecoverTime"});
	        await ETTask.CompletedTask;
        }

		public static async ETTask ReducePhysicalStrenth(Scene scene, long playerId, int chgValue, PlayerModelChgType playerModelChgType)
		{
			if (chgValue < 0)
			{
				Log.Error($"The chgValue cannot be negative, chgValue:{chgValue}");
				return;
			}
			// PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);
			//
			// PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
			// if (playerDataComponent == null)
			// {
			// 	return;
			// }

			PlayerBaseInfoComponent playerBaseInfoComponent =
					await GetPlayerModel(scene, playerId, PlayerModelType.BaseInfo, true) as
							PlayerBaseInfoComponent;
			playerBaseInfoComponent.ChgPhysicalStrength(-chgValue);

			await SavePlayerModel(scene, playerId, PlayerModelType.BaseInfo,
				new() { "physicalStrength", "nextRecoverTime"});
			await ETTask.CompletedTask;
		}

		public static async ETTask AddItem(Scene scene, long playerId, string itemCfgId, int count)
		{
			if (count <= 0)
			{
				Log.Error($"Quantity must be positive, count:{count}");
				return;
			}
			// PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);
			//
			// PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
			// if (playerDataComponent == null)
			// {
			// 	return;
			// }

			PlayerBackPackComponent playerBackPackComponent =
					await GetPlayerModel(scene, playerId, PlayerModelType.BackPack, true) as
							PlayerBackPackComponent;
			playerBackPackComponent.AddItem(itemCfgId, count);

			await SavePlayerModel(scene, playerId, PlayerModelType.BackPack, null);
			await ETTask.CompletedTask;
		}

		public static async ETTask DeleteItem(Scene scene, long playerId, string itemCfgId, int count)
		{
			if (count <= 0)
			{
				Log.Error($"Quantity must be positive, count:{count}");
				return;
			}
			// PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);
			//
			// PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
			// if (playerDataComponent == null)
			// {
			// 	return;
			// }

			PlayerBackPackComponent playerBackPackComponent =
					await GetPlayerModel(scene, playerId, PlayerModelType.BackPack, true) as
							PlayerBackPackComponent;
			playerBackPackComponent.AddItem(itemCfgId, -count);

			await SavePlayerModel(scene, playerId, PlayerModelType.BackPack, null);
			await ETTask.CompletedTask;
		}

		public static async ETTask AddItems(Scene scene, long playerId, Dictionary<string, int> items)
		{
			// PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);
			//
			// PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(playerId);
			// if (playerDataComponent == null)
			// {
			// 	return;
			// }

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
			await SavePlayerModel(scene, playerId, PlayerModelType.BackPack, null);
			await ETTask.CompletedTask;
		}
    }
}