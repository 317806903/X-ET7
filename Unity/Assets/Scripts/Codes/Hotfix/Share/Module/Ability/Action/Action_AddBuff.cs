using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_AddBuff: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			
			ActionCfg_AddBuff actionCfgAddBuff = ActionCfg_AddBuffCategory.Instance.Get(actionId);
			BuffHelper.AddBuff(unit, actionCfgAddBuff, selectHandle, actionContext);
			await ETTask.CompletedTask;
		}
	}
}




