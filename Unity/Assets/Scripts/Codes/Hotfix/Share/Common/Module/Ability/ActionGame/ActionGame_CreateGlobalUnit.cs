using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
	public class ActionGame_CreateGlobalUnit: IActionGameHandler
	{
		public override async ETTask Run(Scene scene, string actionId, float delayTime, ActionGameContext actionGameContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
				if (scene.IsDisposed)
				{
					return;
				}
			}

			ActionGameCfg_CreateGlobalUnit actionGameCfgCreateGlobalUnit = ActionGameCfg_CreateGlobalUnitCategory.Instance.Get(actionId);


			float3 ? position = null;
			List<float3> positionList = null;
			if (actionGameCfgCreateGlobalUnit.CallActorPositionType == CallActorPositionType.ByParent)
			{
				position = null;
			}
			else if (actionGameCfgCreateGlobalUnit.CallActorPositionType == CallActorPositionType.MapRandom)
			{
				positionList = ET.GamePlayHelper.GetRandomPointList(scene, actionGameCfgCreateGlobalUnit.CallActorPositionType, actionGameCfgCreateGlobalUnit.CallCount);
			}
			else if (actionGameCfgCreateGlobalUnit.CallActorPositionType == CallActorPositionType.PathLineRandom)
			{
				positionList = ET.GamePlayHelper.GetRandomPointList(scene, actionGameCfgCreateGlobalUnit.CallActorPositionType, actionGameCfgCreateGlobalUnit.CallCount);
			}
			else if (actionGameCfgCreateGlobalUnit.CallActorPositionType == CallActorPositionType.Center)
			{
				positionList = ET.GamePlayHelper.GetRandomPointList(scene, actionGameCfgCreateGlobalUnit.CallActorPositionType, actionGameCfgCreateGlobalUnit.CallCount);
			}

			ActionContext actionContext = new();
			actionContext.playerId = actionGameContext.playerId;
			for (int j = 0; j < actionGameCfgCreateGlobalUnit.CallCount; j++)
			{
				float3 ? curPosition = null;
				if (position == null && positionList != null)
				{
					curPosition = positionList[j];
				}
				else
				{
					curPosition = position;
				}
				Unit actorUnit = GamePlayHelper.CreateActorByGlobal(scene, actionGameCfgCreateGlobalUnit, ref actionContext, curPosition);
			}

			await ETTask.CompletedTask;
		}
	}
}
