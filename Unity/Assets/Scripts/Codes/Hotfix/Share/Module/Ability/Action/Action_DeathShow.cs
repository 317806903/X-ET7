using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_DeathShow: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
			}
			ActionCfg_DeathShow actionCfg_DeathShow = ActionCfg_DeathShowCategory.Instance.Get(actionId);
			foreach (AttackActionCall attackActionCall in actionCfg_DeathShow.DeathShowActionCall)
			{
				SelectHandle curSelectHandle = selectHandle;

				bool bRet = ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(unit, unit, attackActionCall.DelayTime, attackActionCall.ActionId, attackActionCall.ActionCondition1, attackActionCall.ActionCondition2, curSelectHandle, null, ref actionContext);

			}
			await ETTask.CompletedTask;
		}
	}
}

