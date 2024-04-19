using System;
using System.Collections.Generic;
using ET.AbilityConfig;
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

        public static async ETTask<Entity> _GetPlayerModel(Scene scene, long playerId, PlayerModelType playerModelType, bool forceReGet)
        {
	        PlayerDataComponent playerDataComponent = GetPlayerCache(scene, playerId);

	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.PlayerCacheClient, playerId))
	        {
		        Entity entity = playerDataComponent.GetPlayerModel(playerModelType);
		        if (entity == null || forceReGet)
		        {
			        (bool bRet, byte[] playerModelComponentBytes) = await SendGetPlayerModelAsync(scene, playerId, playerModelType);
			        if (bRet)
			        {
				        Entity entityModel = ResetPlayerModel(scene, playerId, playerModelType, playerModelComponentBytes);
				        entity = entityModel;
			        }
		        }
		        return entity;
	        }
        }

        public static void ClearPlayerModel(Scene scene, long playerId, PlayerModelType playerModelType)
        {
	        PlayerDataComponent playerDataComponent = GetPlayerCache(scene, playerId);

	        Entity entityModel = playerDataComponent.GetPlayerModel(playerModelType);
	        if (entityModel == null)
	        {
		        return;
	        }

	        entityModel.ClearDataCache();
        }

        public static async ETTask GetMyPlayerModelAll(Scene scene)
        {
	        await GetMyPlayerBaseInfo(scene, true);
	        await GetMyPlayerBackPack(scene, true);
	        await GetMyPlayerBattleCard(scene, true);
	        await GetMyPlayerFunctionMenu(scene, true);
        }

        public static async ETTask<Entity> GetMyPlayerModel(Scene scene, PlayerModelType playerModelType, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        return await _GetPlayerModel(scene, myPlayerId, playerModelType, forceReGet);
        }

        public static async ETTask<PlayerBaseInfoComponent> GetMyPlayerBaseInfo(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        Entity entity = await _GetPlayerModel(scene, myPlayerId, PlayerModelType.BaseInfo, forceReGet);
	        return entity as PlayerBaseInfoComponent;
        }

        public static async ETTask<PlayerBackPackComponent> GetMyPlayerBackPack(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        Entity entity = await _GetPlayerModel(scene, myPlayerId, PlayerModelType.BackPack, forceReGet);
	        return entity as PlayerBackPackComponent;
        }

        public static async ETTask<PlayerBattleCardComponent> GetMyPlayerBattleCard(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        Entity entity = await _GetPlayerModel(scene, myPlayerId, PlayerModelType.BattleCard, forceReGet);
	        return entity as PlayerBattleCardComponent;
        }

        public static async ETTask<PlayerFunctionMenuComponent> GetMyPlayerFunctionMenu(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        Entity entity = await _GetPlayerModel(scene, myPlayerId, PlayerModelType.FunctionMenu, forceReGet);
	        return entity as PlayerFunctionMenuComponent;
        }

        public static async ETTask<List<ItemComponent>> GetMyBattleCardItemList(Scene scene, bool forceReGet = false)
        {
	        PlayerBackPackComponent playerBackPackComponent = await GetMyPlayerBackPack(scene, forceReGet);
	        PlayerBattleCardComponent playerBattleCardComponent = await GetMyPlayerBattleCard(scene, forceReGet);

	        playerBattleCardComponent.SetBattleCardItemCfgIdList(playerBackPackComponent.GetItemListByItemType(ItemType.Tower, ItemSubType.None));

	        List<ItemComponent> list = ListComponent<ItemComponent>.Create();
	        foreach (var itemCfgId in playerBattleCardComponent.GetBattleCardItemCfgIdList())
	        {
		        ItemComponent itemComponent = playerBackPackComponent.GetItemWhenStack(itemCfgId);
		        list.Add(itemComponent);
	        }
	        return list;
        }

        public static Entity ResetPlayerModel(Scene scene, long playerId, PlayerModelType playerModelType, byte[] playerModelComponentBytes)
        {
	        PlayerDataComponent playerDataComponent = GetPlayerCache(scene, playerId);

	        Entity entityModel = playerDataComponent.SetPlayerModel(playerModelType, playerModelComponentBytes, null);
	        entityModel.SetDataCacheAutoClear();

	        return entityModel;
        }

        public static async ETTask SaveMyPlayerModel(Scene scene, PlayerModelType playerModelType, List<string> setPlayerKeys)
        {
	        PlayerCacheManagerComponent playerCacheManagerComponent = GetPlayerCacheManager(scene);

	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        // PlayerDataComponent playerDataComponent = playerCacheManagerComponent.GetPlayerData(myPlayerId);
	        // if (playerDataComponent == null)
	        // {
		       //  return;
	        // }

	        Entity entityModel = await _GetPlayerModel(scene, myPlayerId, playerModelType, false);
	        entityModel.SetDataCacheAutoClear();
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
	        Entity entity = await _GetPlayerModel(scene, playerId, PlayerModelType.BaseInfo, forceReGet);
	        return entity as PlayerBaseInfoComponent;
        }

        public static async ETTask<PlayerBackPackComponent> GetOtherPlayerBackPack(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await _GetPlayerModel(scene, playerId, PlayerModelType.BackPack, forceReGet);
	        return entity as PlayerBackPackComponent;
        }

        public static async ETTask<PlayerBattleCardComponent> GetOtherPlayerBattleCard(Scene scene, long playerId, bool forceReGet = false)
        {
	        Entity entity = await _GetPlayerModel(scene, playerId, PlayerModelType.BattleCard, forceReGet);
	        return entity as PlayerBattleCardComponent;
        }

        public static async ETTask<(bool, byte[])> SendGetPlayerModelAsync(Scene clientScene, long playerId, PlayerModelType playerModelType)
        {
	        Session session = ET.Client.SessionHelper.GetSession(clientScene);
	        if (session == null)
	        {
		        return (false, null);
	        }
	        G2C_GetPlayerCache _G2C_GetPlayerCache = await session.Call(new C2G_GetPlayerCache()
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
		        return (true, playerModelComponentBytes);
	        }
        }

        public static async ETTask SendSavePlayerModelAsync(Scene clientScene, long playerId, PlayerModelType playerModelType, byte[] playerModelComponentBytes, List<string> setPlayerKeys)
        {
	        ET.Client.SessionHelper.GetSession(clientScene).Send(new C2G_SetPlayerCache()
		        {
			        PlayerId = playerId,
			        PlayerModelType = (int)playerModelType,
			        PlayerModelComponentBytes = playerModelComponentBytes,
			        SetPlayerKeys = setPlayerKeys,
		        });
	        await ETTask.CompletedTask;
        }

		public static async ETTask<bool> AddPlayerPhysicalStrenthByAdAsync(Scene scene)
        {
	        try
	        {
		        scene = scene.ClientScene();
		        EventSystem.Instance.Publish(scene, new EventType.NoticeAdmobSDKStatus()
		        {
			        IsAdmobAvailable = false,
		        });

		        while (true)
		        {
			        if (scene.IsDisposed)
			        {
				        return false;
			        }
			        if (ReLoginComponent.Instance != null && ReLoginComponent.Instance.isReCreateSessioning == false)
			        {
				        break;
			        }
			        await TimerComponent.Instance.WaitFrameAsync();
		        }

		        long playerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
		        G2C_AddPhysicalStrenthByAd _G2C_AddPhysicalStrenthByAd = await ET.Client.SessionHelper.GetSession(scene).Call(new C2G_AddPhysicalStrenthByAd()
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
	        catch (Exception e)
	        {
		        Log.Error(e);
		        return false;
	        }
	        finally
	        {
		        EventSystem.Instance.Publish(scene, new EventType.NoticeAdmobSDKStatus()
		        {
			        IsAdmobAvailable = true,
		        });
	        }

        }
    }
}