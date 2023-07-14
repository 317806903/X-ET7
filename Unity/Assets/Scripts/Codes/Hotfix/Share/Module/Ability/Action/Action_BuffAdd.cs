using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_BuffAdd: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			
			ActionCfg_BuffAdd actionCfgAddBuff = ActionCfg_BuffAddCategory.Instance.Get(actionId);
			BuffHelper.AddBuff(unit, actionCfgAddBuff, selectHandle, actionContext);
			await ETTask.CompletedTask;
		}
	}
}




