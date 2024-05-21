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
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
			}

			actionContext.motionUnitId = unit.Id;
			if (resetPosByUnit != null)
			{
				actionContext.motionPosition = resetPosByUnit.Position;
			}
			else
			{
				actionContext.motionPosition = unit.Position;
			}
			ActionCfg_BuffAdd actionCfgAddBuff = ActionCfg_BuffAddCategory.Instance.Get(actionId);
			BuffHelper.AddBuff(unit, actionCfgAddBuff, selectHandle, ref actionContext);
			await ETTask.CompletedTask;
		}
	}
}




