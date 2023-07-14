using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_BuffRemove: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			
			ActionCfg_BuffRemove actionCfgRemoveBuff = ActionCfg_BuffRemoveCategory.Instance.Get(actionId);
			BuffHelper.RemoveBuff(unit, actionCfgRemoveBuff, selectHandle, actionContext);
			await ETTask.CompletedTask;
		}
	}
}




