using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_BuffReduceStackCount: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			
			ActionCfg_BuffReduceStackCount actionCfgBuffReduceStackCount = ActionCfg_BuffReduceStackCountCategory.Instance.Get(actionId);
			int reduceStack = actionCfgBuffReduceStackCount.ReduceStack;
			BuffHelper.ChgBuffStackCount(unit, -reduceStack, actionContext);
			await ETTask.CompletedTask;
		}
	}
}




