using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Client
{
    public static class PlayerCacheHelper
    {
	    public static PlayerCacheManagerComponent GetPlayerCacheManager(Scene scene)
	    {
		    Scene currentScene = null;
		    Scene clientScene = null;
		    if (scene == scene.ClientScene())
		    {
			    currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
			    clientScene = scene;
		    }
		    else
		    {
			    currentScene = scene;
			    clientScene = currentScene.Parent.GetParent<Scene>();
		    }

		    PlayerCacheManagerComponent playerCacheManagerComponent = clientScene.GetComponent<PlayerCacheManagerComponent>();
		    if (playerCacheManagerComponent == null)
		    {
			    playerCacheManagerComponent = clientScene.AddComponent<PlayerCacheManagerComponent>();
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

	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.PlayerCacheClient, playerId))
	        {
		        Entity entity = playerDataComponent.GetPlayerModel(playerModelType);
		        if (entity == null || forceReGet)
		        {
			        (bool bRet, Entity entityModel) = await SendGetPlayerModelAsync(scene, playerId, playerModelType);
			        if (bRet)
			        {
				        if (entity != null)
				        {
					        entity.Dispose();
				        }
				        playerDataComponent.AddComponent(entityModel);
				        entityModel.AddComponent<DataCacheClearComponent>();
				        entity = entityModel;
			        }
		        }
		        return entity;
	        }
        }

        public static async ETTask GetMyPlayerModelAll(Scene scene)
        {
	        await GetMyPlayerBaseInfo(scene, true);
	        await GetMyPlayerBackPack(scene, true);
	        await GetMyPlayerBattleCard(scene, true);
        }

        public static async ETTask<Entity> GetMyPlayerModel(Scene scene, PlayerModelType playerModelType, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerHelper.GetMyPlayerId(scene);
	        return await GetPlayerModel(scene, myPlayerId, playerModelType, forceReGet);
        }

        public static async ETTask<PlayerBaseInfoComponent> GetMyPlayerBaseInfo(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerHelper.GetMyPlayerId(scene);
	        Entity entity = await GetPlayerModel(scene, myPlayerId, PlayerModelType.BaseInfo, forceReGet);
	        return entity as PlayerBaseInfoComponent;
        }

        public static async ETTask<PlayerBackPackComponent> GetMyPlayerBackPack(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerHelper.GetMyPlayerId(scene);
	        Entity entity = await GetPlayerModel(scene, myPlayerId, PlayerModelType.BackPack, forceReGet);
	        return entity as PlayerBackPackComponent;
        }

        public static async ETTask<PlayerBattleCardComponent> GetMyPlayerBattleCard(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerHelper.GetMyPlayerId(scene);
	        Entity entity = await GetPlayerModel(scene, myPlayerId, PlayerModelType.BattleCard, forceReGet);
	        return entity as PlayerBattleCardComponent;
        }

        public static async ETTask SaveMyPlayerModel(Scene scene, PlayerModelType playerModelType, List<string> setPlayerKeys)
        {
	        PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);

	        long myPlayerId = ET.Client.PlayerHelper.GetMyPlayerId(scene);
	        PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(myPlayerId);
	        if (playerDataComponent == null)
	        {
		        return;
	        }

	        Entity entityModel = await GetPlayerModel(scene, myPlayerId, playerModelType, false);
	        entityModel.GetComponent<DataCacheClearComponent>().RefreshTime();
	        byte[] bytes = entityModel.ToBson();

	        await SendSavePlayerModelAsync(scene, myPlayerId, playerModelType, bytes, setPlayerKeys);

	        if (playerModelType == PlayerModelType.BaseInfo)
	        {
		        PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
		        EventSystem.Instance.Publish(scene, new EventType.NoticeEventLoggingSetUserProperties()
		        {
			        properties = new()
			        {
				        {"artd_infinity_max_num", playerBaseInfoComponent.GetEndlessChallengeScore()},
				        {"artd_account_icon_id_code", playerBaseInfoComponent.GetIconIndex()},
			        }
		        });
	        }
	        await ETTask.CompletedTask;
        }

        public static async ETTask<PlayerBaseInfoComponent> GetOtherPlayerBaseInfo(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await GetPlayerModel(scene, playerId, PlayerModelType.BaseInfo, forceReGet);
	        return entity as PlayerBaseInfoComponent;
        }

        public static async ETTask<PlayerBackPackComponent> GetOtherPlayerBackPack(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await GetPlayerModel(scene, playerId, PlayerModelType.BackPack, forceReGet);
	        return entity as PlayerBackPackComponent;
        }

        public static async ETTask<PlayerBattleCardComponent> GetOtherPlayerBattleCard(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await GetPlayerModel(scene, playerId, PlayerModelType.BattleCard, forceReGet);
	        return entity as PlayerBattleCardComponent;
        }

        public static async ETTask<(bool, Entity)> SendGetPlayerModelAsync(Scene clientScene, long playerId, PlayerModelType playerModelType)
        {
	        G2C_GetPlayerCache _G2C_GetPlayerCache = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_GetPlayerCache()
		        {
			        PlayerId = playerId,
			        PlayerModelType = (int)playerModelType,
		        }) as
		        G2C_GetPlayerCache;
	        if (_G2C_GetPlayerCache.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"GetPlayerModelAsync Error==1 msg={_G2C_GetPlayerCache.Message}");
		        return (false, null);
	        }
	        else
	        {
		        byte[] playerModelComponentBytes = _G2C_GetPlayerCache.PlayerModelComponentBytes;
		        Entity entity = MongoHelper.Deserialize<Entity>(playerModelComponentBytes);
		        return (true, entity);
	        }
        }

        public static async ETTask<bool> SendSavePlayerModelAsync(Scene clientScene, long playerId, PlayerModelType playerModelType, byte[] playerModelComponentBytes, List<string> setPlayerKeys)
        {
	        G2C_SetPlayerCache _G2C_SetPlayerCache = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_SetPlayerCache()
		        {
			        PlayerId = playerId,
			        PlayerModelType = (int)playerModelType,
			        PlayerModelComponentBytes = playerModelComponentBytes,
			        SetPlayerKeys = setPlayerKeys,
		        }) as
		        G2C_SetPlayerCache;
	        if (_G2C_SetPlayerCache.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SetPlayerModelAsync Error==1 msg={_G2C_SetPlayerCache.Message}");
		        return false;
	        }
	        else
	        {
		        return true;
	        }
        }

		public static async ETTask<bool> AddPlayerPhysicalStrenthByAdAsync(Scene clientScene)
        {
	        long playerId = ET.Client.PlayerHelper.GetMyPlayerId(clientScene);
			G2C_AddPhysicalStrenthByAd _G2C_AddPhysicalStrenthByAd = await ET.Client.SessionHelper.GetSession(clientScene).Call(new C2G_AddPhysicalStrenthByAd()
		        {
			        PlayerId = playerId,
		        }) as
		        G2C_AddPhysicalStrenthByAd;
	        if (_G2C_AddPhysicalStrenthByAd.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"AddPlayerPhysicalStrenthByAdAsync Error==1 msg={_G2C_AddPhysicalStrenthByAd.Message}");
		        return false;
	        }
	        else
	        {
				Log.Info($"AddPlayerPhysicalStrenthByAdAsync Success");
				return true;
	        }
        }
    }
}