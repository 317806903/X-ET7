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
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			ActionCfg_DeathShow actionCfg_DeathShow = ActionCfg_DeathShowCategory.Instance.Get(actionId);
			foreach (AttackActionCall attackActionCall in actionCfg_DeathShow.DeathShowActionCall)
			{
				SelectHandle curSelectHandle = selectHandle;
				(bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = ConditionHandleHelper.ChkCondition(unit, curSelectHandle, attackActionCall.ActionCondition1, actionContext);
				if (isChgSelect1)
				{
					curSelectHandle = newSelectHandle1;
				}
				(bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = ConditionHandleHelper.ChkCondition(unit, curSelectHandle, attackActionCall.ActionCondition2, actionContext);
				if (isChgSelect2)
				{
					curSelectHandle = newSelectHandle2;
				}
				if (bRet1 && bRet2)
				{
					ActionHandlerHelper.CreateAction(unit, null, attackActionCall.ActionId, attackActionCall.DelayTime, curSelectHandle, 
					actionContext);
				}
			}
			await ETTask.CompletedTask;
		}
	}
}

