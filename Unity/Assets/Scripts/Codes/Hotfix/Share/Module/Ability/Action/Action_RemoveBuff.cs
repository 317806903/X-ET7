using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_RemoveBuff: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			
			ActionCfg_RemoveBuff actionCfgRemoveBuff = ActionCfg_RemoveBuffCategory.Instance.Get(actionId);
			BuffHelper.RemoveBuff(unit, actionCfgRemoveBuff, selectHandle, actionContext);
			await ETTask.CompletedTask;
		}
	}
}




