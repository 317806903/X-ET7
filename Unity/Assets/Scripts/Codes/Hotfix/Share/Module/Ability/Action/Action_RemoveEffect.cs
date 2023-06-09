using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_RemoveEffect: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			ActionCfg_RemoveEffect actionCfgRemoveEffect = ActionCfg_RemoveEffectCategory.Instance.Get(actionId);
			EffectHelper.RemoveEffect(unit, actionCfgRemoveEffect);
			await ETTask.CompletedTask;
		}
	}
}
