using System;
using ET.AbilityConfig;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    public static class SeasonHelper
    {
	    public static SeasonShowManagerComponent _GetSeasonManager(Scene scene)
	    {
		    SeasonShowManagerComponent seasonShowManagerComponent = scene.GetComponent<SeasonShowManagerComponent>();
		    if (seasonShowManagerComponent == null)
		    {
			    seasonShowManagerComponent = scene.AddComponent<SeasonShowManagerComponent>();
		    }
		    return seasonShowManagerComponent;
	    }

	    public static async ETTask<int> GetSeasonCfgId(Scene scene, bool forceReGet = false)
	    {
		    SeasonComponent seasonComponent = await GetSeasonComponent(scene, forceReGet);
		    return seasonComponent.seasonCfgId;
	    }

	    public static async ETTask<int> GetSeasonIndex(Scene scene, bool forceReGet = false)
	    {
		    SeasonComponent seasonComponent = await GetSeasonComponent(scene, forceReGet);
		    return seasonComponent.seasonIndex;
	    }

        public static async ETTask<SeasonComponent> GetSeasonComponent(Scene scene, bool forceReGet)
        {
	        SeasonShowManagerComponent seasonShowManagerComponent = _GetSeasonManager(scene);

	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Season, scene.InstanceId))
	        {
		        SeasonComponent entity = seasonShowManagerComponent.SeasonComponent;
		        if (entity == null || forceReGet)
		        {
			        (bool bRet, SeasonComponent seasonComponent) = await SendGetSeasonComponentAsync(scene);
			        if (bRet)
			        {
				        if (forceReGet && entity != null)
				        {
					        entity.Dispose();
				        }
				        seasonShowManagerComponent.AddChild(seasonComponent);
				        seasonShowManagerComponent.SeasonComponent = seasonComponent;
				        seasonComponent.SetDataCacheAutoClear(seasonComponent.GetClearTime()*0.001f);
				        entity = seasonComponent;
			        }
		        }
		        return entity;
	        }
        }

        public static void SetCurSeasonByTime(SeasonComponent seasonComponent)
        {
	        DateTime firstDateTIme = TimeHelper.ToDateTime(GlobalSettingCfgCategory.Instance.SeasonStartTime);
	        if (TimeHelper.DateTimeNow() < firstDateTIme)
	        {
		        return;
	        }
	        float seasonDurationTime = GlobalSettingCfgCategory.Instance.SeasonDurationTime;
	        //seasonDurationTime = 1/24f/60 * 3;
	        var list = SeasonInfoCfgCategory.Instance.DataList;
	        int seasonIndex = 1;
	        int seasonCfgId = 1;
	        DateTime startTime = firstDateTIme;
	        DateTime endTime = firstDateTIme.AddDays(seasonDurationTime);
	        while (TimeHelper.DateTimeNow() > startTime)
	        {
		        if (TimeHelper.DateTimeNow() < endTime)
		        {
			        break;
		        }
		        startTime = startTime.AddDays(seasonDurationTime);
		        endTime = endTime.AddDays(seasonDurationTime);
		        seasonIndex++;
		        seasonCfgId++;
		        if (seasonCfgId > list.Count)
		        {
			        seasonCfgId = 1;
		        }
	        }

	        seasonComponent.seasonIndex = seasonIndex;
	        seasonComponent.seasonCfgId = seasonCfgId;
	        seasonComponent.startTime = TimeHelper.ToTimeStamp(startTime);
	        seasonComponent.endTime = TimeHelper.ToTimeStamp(endTime);
        }

        public static async ETTask<(bool, SeasonComponent)> SendGetSeasonComponentAsync(Scene scene)
        {
	        StartSceneConfig seasonSceneConfig = StartSceneConfigCategory.Instance.GetSeasonManager(scene.DomainZone());

	        S2G_GetSeasonComponent _S2G_GetSeasonComponent = (S2G_GetSeasonComponent) await ActorMessageSenderComponent.Instance.Call(seasonSceneConfig.InstanceId, new G2S_GetSeasonComponent()
	        {
	        });

	        if (_S2G_GetSeasonComponent.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendGetSeasonComponentAsync Error==1 msg={_S2G_GetSeasonComponent.Message}");
		        return (false, null);
	        }
	        else
	        {
		        byte[] componentBytes = _S2G_GetSeasonComponent.ComponentBytes;
		        if (componentBytes == null)
		        {
			        Log.Error($"SendGetSeasonComponentAsync componentBytes == null");
			        return (false, null);
		        }
		        SeasonComponent seasonComponent = MongoHelper.Deserialize<SeasonComponent>(componentBytes);
		        return (true, seasonComponent);
	        }
        }

    }
}