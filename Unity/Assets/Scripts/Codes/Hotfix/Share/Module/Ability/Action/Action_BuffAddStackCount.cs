using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_BuffAddStackCount: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			
			ActionCfg_BuffAddStackCount actionCfgBuffAddStackCount = ActionCfg_BuffAddStackCountCategory.Instance.Get(actionId);
			int addStackCount = actionCfgBuffAddStackCount.AddStack;
			BuffHelper.ChgBuffStackCount(unit, addStackCount, actionContext);
			await ETTask.CompletedTask;
		}
	}
}




