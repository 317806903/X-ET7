using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_CreateEffect: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			ActionCfg_CreateEffect actionCfgCreateEffect = ActionCfg_CreateEffectCategory.Instance.Get(actionId);
			EffectHelper.AddEffect(unit, actionCfgCreateEffect, selectHandle);
			await ETTask.CompletedTask;
		}
	}
}


