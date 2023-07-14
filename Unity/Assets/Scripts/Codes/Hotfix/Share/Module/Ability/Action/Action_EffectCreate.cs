using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_EffectCreate: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			ActionCfg_EffectCreate actionCfgCreateEffect = ActionCfg_EffectCreateCategory.Instance.Get(actionId);
			EffectHelper.AddEffect(unit, actionCfgCreateEffect, selectHandle);
			await ETTask.CompletedTask;
		}
	}
}


