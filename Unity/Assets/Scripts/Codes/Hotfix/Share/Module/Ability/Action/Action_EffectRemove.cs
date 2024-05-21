using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_EffectRemove: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
			}
			ActionCfg_EffectRemove actionCfgRemoveEffect = ActionCfg_EffectRemoveCategory.Instance.Get(actionId);
			EffectHelper.RemoveEffect(unit, actionCfgRemoveEffect);
			await ETTask.CompletedTask;
		}
	}
}
