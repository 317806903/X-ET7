using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_DamageUnit: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			ActionCfg_DamageUnit actionCfg_DamageUnit = ActionCfg_DamageUnitCategory.Instance.Get(actionId);
			DamageHelper.DoDamage(unit, actionCfg_DamageUnit, selectHandle, null);
			await ETTask.CompletedTask;
		}
	}
}

