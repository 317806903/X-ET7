using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_AttackArea: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
			}
			ActionCfg_AttackArea actionCfg_AttackArea = ActionCfg_AttackAreaCategory.Instance.Get(actionId);
			actionContext.isBreakSoftBati = actionCfg_AttackArea.IsBreakSoftBati;
			actionContext.isBreakStrongBati = actionCfg_AttackArea.IsBreakStrongBati;
			await DamageHelper.DoAttackArea(unit, resetPosByUnit, actionCfg_AttackArea, selectHandle, actionContext);
			await ETTask.CompletedTask;
		}
	}
}

