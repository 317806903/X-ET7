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

		    CurrentScenesComponent currentScenesComponent = clientScene.GetComponent<CurrentScenesComponent>();
		    PlayerCacheManagerComponent playerCacheManagerComponent = currentScenesComponent.GetComponent<PlayerCacheManagerComponent>();
		    if (playerCacheManagerComponent == null)
		    {
			    playerCacheManagerComponent = currentScenesComponent.AddComponent<PlayerCacheManagerComponent>();
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
			        try
			        {
				        (bool bRet, byte[] playerModelComponentBytes) = await SendGetPlayerModelAsync(scene, playerId, playerModelType);
				        if (bRet)
				        {
					        Entity entityModel = ResetPlayerModel(scene, playerId, playerModelType, playerModelComponentBytes);
					        entity = entityModel;
				        }
			        }
			        catch (Exception e)
			        {
				        string msg = e.Message;
#if UNITY_EDITOR
				        Log.Error($"ET.Client.PlayerCacheHelper._GetPlayerModel Error [{playerId}][{playerModelType.ToString()}][{msg}]");
#endif
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
	        await GetMyPlayerMail(scene, true);
	        await GetMyPlayerSeasonInfo(scene, true);
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

        public static async ETTask<PlayerOtherInfoComponent> GetMyPlayerOtherInfo(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        Entity entity = await _GetPlayerModel(scene, myPlayerId, PlayerModelType.OtherInfo, forceReGet);
	        return entity as PlayerOtherInfoComponent;
        }

        public static async ETTask<PlayerSeasonInfoComponent> GetMyPlayerSeasonInfo(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        Entity entity = await _GetPlayerModel(scene, myPlayerId, PlayerModelType.SeasonInfo, forceReGet);
	        return entity as PlayerSeasonInfoComponent;
        }

        public static async ETTask<PlayerFunctionMenuComponent> GetMyPlayerFunctionMenu(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        Entity entity = await _GetPlayerModel(scene, myPlayerId, PlayerModelType.FunctionMenu, forceReGet);
	        return entity as PlayerFunctionMenuComponent;
        }

        public static async ETTask<PlayerMailComponent> GetMyPlayerMail(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        Entity entity = await _GetPlayerModel(scene, myPlayerId, PlayerModelType.Mails, forceReGet);
	        return entity as PlayerMailComponent;
        }

        public static async ETTask<PlayerSkillComponent> GetMyPlayerSkill(Scene scene, bool forceReGet = false)
        {
	        long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(scene);
	        Entity entity = await _GetPlayerModel(scene, myPlayerId, PlayerModelType.Skills, forceReGet);
	        return entity as PlayerSkillComponent;
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

        public static async ETTask<int> GetTokenValue(Scene scene, string itemCfgId, bool forceReGet = false)
        {
            PlayerBackPackComponent playerBackPackComponent = await GetMyPlayerBackPack(scene, forceReGet);
            if (playerBackPackComponent == null)
            {
	            return 0;
            }

	        var itemComponent = playerBackPackComponent.GetItemWhenStack(itemCfgId);
	        if (itemComponent == null)
	        {
		        return 0;
	        }

	        return itemComponent.GetCount();
        }

        public static async ETTask<int> GetTokenDiamond(Scene scene, bool forceReGet = false)
        {
	        string itemCfgId = ItemHelper.GetTokenDiamondCfgId();
	        return await GetTokenValue(scene, itemCfgId, forceReGet);
        }

        public static async ETTask<int> GetTokenArcadeCoin(Scene scene, bool forceReGet = false)
        {
	        string itemCfgId = ItemHelper.GetTokenArcadeCoinCfgId();
	        return await GetTokenValue(scene, itemCfgId, forceReGet);
        }

        public static Entity ResetPlayerModel(Scene scene, long playerId, PlayerModelType playerModelType, byte[] playerModelComponentBytes)
        {
	        PlayerDataComponent playerDataComponent = GetPlayerCache(scene, playerId);

	        Entity entityModel = playerDataComponent.SetPlayerModel(playerModelType, playerModelComponentBytes, null, PlayerModelChgType.None);
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
		        }, false) as
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

		/// <summary>
		/// 发送重置养成消息
		/// </summary>
		/// <param name="scene"></param>
		/// <returns>重置是否成功</returns>
		public static async ETTask<bool> ResetAllSeasonBringUp(Scene scene)
		{
            try
            {
                scene = scene.ClientScene();

                G2C_ResetPowerup Msg = await ET.Client.SessionHelper.GetSession(scene).Call(new C2G_ResetPowerup()
                {

                }) as G2C_ResetPowerup;
                if (Msg.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"AddPlayerPhysicalStrenthByAdAsync Error==1 msg={Msg.Message}");
                    return false;
                }
                else
                {
                    Log.Info($"AddPlayerPhysicalStrenthByAdAsync Success");
                    int seasonCfgId = SeasonHelper.GetSeasonCfgId(scene);
                    SeasonInfoCfg seasonInfoCfg = SeasonInfoCfgCategory.Instance.Get(seasonCfgId);
                    string seasonName = seasonInfoCfg.Name;
                    EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
                    {
	                    eventName = "StrengthingReset",
	                    properties = new()
	                    {
		                    {"seasonName", seasonName},
	                    }
                    });

                    return true;
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

		/// <summary>
		/// 发送升级养成消息
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="cfgId"></param>
		/// <param name="cfgLevel"></param>
		/// <returns></returns>
		public static async ETTask<bool> UpdateSeasonBringUp(Scene scene, string cfgId)
        {
            try
            {
                scene = scene.ClientScene();

                G2C_UpdatePowerup Resp = await ET.Client.SessionHelper.GetSession(scene).Call(new C2G_UpdatePowerup()
                {
					PowerUpcfg = cfgId,
                }) as G2C_UpdatePowerup;
                if (Resp.Error != ET.ErrorCode.ERR_Success)
                {
                    Log.Error($"AddPlayerPhysicalStrenthByAdAsync Error==1 msg={Resp.Message}");
                    return false;
                }
                else
                {

	                int seasonCfgId = SeasonHelper.GetSeasonCfgId(scene);
	                SeasonInfoCfg seasonInfoCfg = SeasonInfoCfgCategory.Instance.Get(seasonCfgId);
	                string seasonName = seasonInfoCfg.Name;
	                PlayerSeasonInfoComponent playerSeasonInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerSeasonInfo(scene);
	                int playerBringupLevel = playerSeasonInfoComponent.GetSeasonBringUpLevel(cfgId);
	                SeasonBringUpCfg seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.Get(cfgId, playerBringupLevel);
	                string seasonBringUpCfgName = seasonBringUpCfg.Name;
	                EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
	                {
		                eventName = "SeasonStrengthened",
		                properties = new()
		                {
			                {"seasonName", seasonName},
			                {"seasonBringUpCfgName", seasonBringUpCfgName},
			                {"playerBringupLevel", playerBringupLevel},
		                }
	                });

					return true;
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        public static async ETTask ReDealMyFunctionMenu(Scene scene)
        {
	        await ETTask.CompletedTask;
	        try
	        {
		        scene = scene.ClientScene();

		        ET.Client.SessionHelper.GetSession(scene).Send(new C2G_ReDealMyFunctionMenu()
			        {
			        });
	        }
	        catch (Exception e)
	        {
		        Log.Error(e);
		        return;
	        }
        }

        public static async ETTask<bool> ChkUIRedDotType(Scene scene, UIRedDotType uiRedDotType)
        {
	        PlayerOtherInfoComponent playerOtherInfoComponent = await PlayerCacheHelper.GetMyPlayerOtherInfo(scene);

	        bool isNeedShow = playerOtherInfoComponent.ChkUIRedDotType(uiRedDotType);
	        return isNeedShow;
        }

        public static async ETTask SetUIRedDotType(Scene scene, UIRedDotType uiRedDotType, string itemCfgId = "", string skillCfgId = "")
        {
	        try
	        {
		        if (uiRedDotType != UIRedDotType.None)
		        {
			        bool isShow = await PlayerCacheHelper.ChkUIRedDotType(scene, uiRedDotType);
			        if (isShow == false)
			        {
				        return;
			        }
		        }
		        scene = scene.ClientScene();

		        G2C_SetUIRedDotType Resp = await ET.Client.SessionHelper.GetSession(scene).Call(new C2G_SetUIRedDotType()
		        {
			        UIRedDotType = (int)uiRedDotType,
			        ItemCfgId = itemCfgId,
			        SkillCfgId = skillCfgId,
		        }) as G2C_SetUIRedDotType;
		        if (Resp.Error != ET.ErrorCode.ERR_Success)
		        {
			        Log.Error($"SetUIRedDotType Error==1 msg={Resp.Message}");
			        return;
		        }
		        else
		        {
			        return;
		        }
	        }
	        catch (Exception e)
	        {
		        Log.Error(e);
		        return;
	        }
        }

        public static async ETTask<bool> SetQuestionnaireFinished(Scene scene, string questionnaireCfgId)
        {
	        try
	        {
		        scene = scene.ClientScene();

		        G2C_SetQuestionnaireFinished Resp = await ET.Client.SessionHelper.GetSession(scene).Call(new C2G_SetQuestionnaireFinished()
		        {
			        QuestionnaireCfgId = questionnaireCfgId,
		        }) as G2C_SetQuestionnaireFinished;
		        if (Resp.Error != ET.ErrorCode.ERR_Success)
		        {
			        Log.Error($"SetQuestionnaireFinished Error==1 msg={Resp.Message}");
			        return false;
		        }
		        else
		        {
			        return true;
		        }
	        }
	        catch (Exception e)
	        {
		        Log.Error(e);
		        return false;
	        }
        }

        public static void SendAddDiamondWhenDebug(Scene scene)
        {
	        try
	        {
		        scene = scene.ClientScene();

		        ET.Client.SessionHelper.GetSession(scene).Send(new C2G_AddDiamondWhenDebug()
		        {
		        });
	        }
	        catch (Exception e)
	        {
		        Log.Error(e);
	        }
        }

        public static void SendChkSeasonIndexChg(Scene scene)
        {
	        try
	        {
		        scene = scene.ClientScene();

		        ET.Client.SessionHelper.GetSession(scene).Send(new C2G_ChkSeasonIndexChg()
		        {
		        });
	        }
	        catch (Exception e)
	        {
		        Log.Error(e);
	        }
        }

        public static void SendSetBattleNoticeFinished(Scene scene, string battleNoticeCfgId)
        {
	        try
	        {
		        scene = scene.ClientScene();

		        ET.Client.SessionHelper.GetSession(scene).Send(new C2G_SetBattleNoticeFinished()
		        {
			        BattleNoticeCfgId = battleNoticeCfgId,
		        });
	        }
	        catch (Exception e)
	        {
		        Log.Error(e);
	        }
        }

        public static async ETTask<QuestionnaireCfg> GetNextQuestionnaire(Scene scene)
        {
	        if (ChannelSettingComponent.Instance.ChkIsNeedQuestionnaire())
	        {
		        List<QuestionnaireCfg> questionnaireCfgList = ChannelSettingComponent.Instance.GetQuestionnaireCfgIdList();
		        PlayerOtherInfoComponent playerOtherInfoComponent = await PlayerCacheHelper.GetMyPlayerOtherInfo(scene);
		        foreach (var questionnaireCfg in questionnaireCfgList)
		        {
			        if (playerOtherInfoComponent.ChkNeedQuestionnaire(questionnaireCfg.Id))
			        {
				        return questionnaireCfg;
			        }
		        }
	        }
	        return null;
        }

    }
}