using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_BuffResetDuration: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			
			ActionCfg_BuffResetDuration actionCfgBuffResetDuration = ActionCfg_BuffResetDurationCategory.Instance.Get(actionId);
			BuffHelper.ChgBuffDurationChg(unit, actionCfgBuffResetDuration.BuffDurationChgType, actionContext);
			await ETTask.CompletedTask;
		}
	}
}




