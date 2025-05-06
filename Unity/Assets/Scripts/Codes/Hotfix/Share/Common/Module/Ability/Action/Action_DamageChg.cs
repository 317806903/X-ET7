using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_DamageChg: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
				if (unit == null || unit.DomainScene() == null || unit.DomainScene().IsDisposed)
				{
					return;
				}
			}

			ActionCfg_DamageChg _ActionCfg_DamageChg = ActionCfg_DamageChgCategory.Instance.Get(actionId);
			DamageHelper.ChgDamageObj(unit, _ActionCfg_DamageChg, ref actionContext);
			await ETTask.CompletedTask;
		}
	}
}




