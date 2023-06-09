using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_AttackArea: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			ActionCfg_AttackArea actionCfg_AttackArea = ActionCfg_AttackAreaCategory.Instance.Get(actionId);
			actionContext.isBreakSoftBati = actionCfg_AttackArea.IsBreakSoftBati;
			actionContext.isBreakStrongBati = actionCfg_AttackArea.IsBreakStrongBati;
			DamageHelper.DoAttackArea(unit, actionCfg_AttackArea, selectHandle, actionContext);
			await ETTask.CompletedTask;
		}
	}
}

