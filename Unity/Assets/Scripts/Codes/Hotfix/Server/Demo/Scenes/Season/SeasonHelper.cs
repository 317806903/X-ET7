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

	    public static async ETTask<int> GetSeasonId(Scene scene, bool forceReGet = false)
	    {
		    SeasonComponent seasonComponent = await GetSeasonComponent(scene, forceReGet);
		    return seasonComponent.seasonId;
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