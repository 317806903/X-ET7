using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_BuffStackCountChg: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			
			ActionCfg_BuffStackCountChg actionCfgBuffStackCountChg = ActionCfg_BuffStackCountChgCategory.Instance.Get(actionId);
			BuffHelper.ChgBuffStackCount(unit, actionCfgBuffStackCountChg, actionContext);
			await ETTask.CompletedTask;
		}
	}
}




