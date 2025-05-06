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
				if (unit == null || unit.DomainScene() == null || unit.DomainScene().IsDisposed)
				{
					return;
				}
			}
			ActionCfg_DeathShow actionCfg_DeathShow = ActionCfg_DeathShowCategory.Instance.Get(actionId);
			foreach (AttackActionCall attackActionCall in actionCfg_DeathShow.DeathShowActionCall)
			{
				bool bRetChk = ET.Ability.ActionHandlerHelper.ChkActionCondition(unit, attackActionCall.ChkCondition1, attackActionCall.ChkCondition2, attackActionCall.ChkCondition1SelectObj_Ref, attackActionCall.ChkCondition2SelectObj_Ref, ref actionContext);
				if (bRetChk == false)
				{
					continue;
				}

				SelectHandle curSelectHandle = selectHandle;

				bool bRet = ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(unit, unit, attackActionCall.DelayTime, attackActionCall.ActionIds, attackActionCall.FilterCondition1, attackActionCall.FilterCondition2, curSelectHandle, null, ref actionContext);

			}
			await ETTask.CompletedTask;
		}
	}
}

