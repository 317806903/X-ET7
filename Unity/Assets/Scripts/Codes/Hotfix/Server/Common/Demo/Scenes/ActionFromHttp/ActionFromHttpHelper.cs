using System;
using ET.AbilityConfig;
using System.Collections.Generic;
using System.Text;
using Unity.Mathematics;

namespace ET.Server
{
    public static class ActionFromHttpHelper
    {
	    public static ActionFromHttpManagerComponent _GetActionFromHttpManager(Scene scene)
	    {
		    ActionFromHttpManagerComponent ActionFromHttpManagerComponent = scene.GetComponent<ActionFromHttpManagerComponent>();
		    if (ActionFromHttpManagerComponent == null)
		    {
			    ActionFromHttpManagerComponent = scene.AddComponent<ActionFromHttpManagerComponent>();
		    }
		    return ActionFromHttpManagerComponent;
	    }

	    public static async ETTask<(bool bRet, string msg)> Run(Scene scene, ActionFromHttpStatus actionFromHttpStatus, Dictionary<string, string> paramDic)
	    {
		    ActionFromHttpManagerComponent actionFromHttpManagerComponent = _GetActionFromHttpManager(scene);
		    return await actionFromHttpManagerComponent.Run(actionFromHttpStatus, paramDic);
	    }

	    public static async ETTask<bool> ChkPlayerIsValid(Scene scene, long playerId)
	    {
		    long locationActorId = await LocationProxyComponent.Instance.Get(LocationType.Player, playerId, scene.InstanceId);
		    if (locationActorId != 0)
		    {
			    return true;
		    }
		    PlayerBaseInfoComponent entityDB = await ET.Server.DBHelper._LoadDB<PlayerBaseInfoComponent>(scene, playerId);
		    if (entityDB != null)
		    {
			    return true;
		    }
		    return false;
	    }
    }
}