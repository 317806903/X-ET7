using ET.AbilityConfig;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Client
{
    public static class SeasonHelper
    {
	    public static SeasonShowManagerComponent _GetSeasonComponent(Scene scene)
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
		    SeasonShowManagerComponent seasonShowManagerComponent = currentScenesComponent.GetComponent<SeasonShowManagerComponent>();
		    if (seasonShowManagerComponent == null)
		    {
			    seasonShowManagerComponent = currentScenesComponent.AddComponent<SeasonShowManagerComponent>();
		    }
		    return seasonShowManagerComponent;
	    }

	    public static int GetSeasonCfgId(Scene scene)
	    {
		    SeasonShowManagerComponent seasonShowManagerComponent = _GetSeasonComponent(scene);
		    SeasonComponent seasonComponent = seasonShowManagerComponent.SeasonComponent;
		    if (seasonComponent == null)
		    {
			    return -1;
		    }
		    return seasonComponent.seasonCfgId;
	    }

	    public static int GetSeasonIndex(Scene scene)
	    {
		    SeasonShowManagerComponent seasonShowManagerComponent = _GetSeasonComponent(scene);
		    SeasonComponent seasonComponent = seasonShowManagerComponent.SeasonComponent;
		    if (seasonComponent == null)
		    {
			    return -1;
		    }
		    return seasonComponent.seasonIndex;
	    }

	    public static SeasonComponent GetSeasonComponent(Scene scene)
	    {
		    SeasonShowManagerComponent seasonShowManagerComponent = _GetSeasonComponent(scene);
		    SeasonComponent seasonComponent = seasonShowManagerComponent.SeasonComponent;
		    return seasonComponent;
	    }

	    public static string GetSeasonLeftTime(Scene scene)
	    {
		    SeasonShowManagerComponent seasonShowManagerComponent = _GetSeasonComponent(scene);
		    SeasonComponent seasonComponent = seasonShowManagerComponent.SeasonComponent;
		    var tmp = TimeHelper.ToDateTime(seasonComponent.endTime) - TimeHelper.DateTimeNow();
		    if (tmp.Days > 0)
		    {
			    string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_SeasonRemaining_Time_Days", tmp.Days);
			    return msgTxt;
		    }
		    if (tmp.Hours > 0)
		    {
			    string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_SeasonRemaining_Time_Hours", tmp.Hours);
			    return msgTxt;
		    }
		    if (tmp.Minutes > 0)
		    {
			    string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_SeasonRemaining_Time_Minutes", tmp.Minutes);
			    return msgTxt;
		    }
		    else
		    {
			    string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_SeasonRemaining_Time_Minutes", 1);
			    return msgTxt;
		    }
	    }

	    public static async ETTask Init(Scene scene)
	    {
		    await GetSeasonComponentAsync(scene, true);
	    }

        public static async ETTask<SeasonComponent> GetSeasonComponentAsync(Scene scene, bool forceReGet = false)
        {
	        SeasonShowManagerComponent seasonShowManagerComponent = _GetSeasonComponent(scene);
	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.SeasonClient, scene.InstanceId))
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

        public static async ETTask<(bool, SeasonComponent)> SendGetSeasonComponentAsync(Scene clientScene)
        {
	        Session session = ET.Client.SessionHelper.GetSession(clientScene);
	        if (session == null)
	        {
		        return (false, null);
	        }
	        G2C_GetSeasonComponent _G2C_GetSeasonComponent = await session.Call(new C2G_GetSeasonComponent()
		        {
		        }, false) as
		        G2C_GetSeasonComponent;
	        if (_G2C_GetSeasonComponent.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendGetSeasonComponentAsync Error==1 msg={_G2C_GetSeasonComponent.Message}");
		        return (false, null);
	        }
	        else
	        {
		        byte[] componentBytes = _G2C_GetSeasonComponent.ComponentBytes;
		        SeasonComponent seasonComponent = MongoHelper.Deserialize<SeasonComponent>(componentBytes);
		        return (true, seasonComponent);
	        }
        }

    }
}