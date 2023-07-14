using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_FaceTo: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			RotateHelper.AddRotate(unit, selectHandle, true);
			await ETTask.CompletedTask;
		}
	}
}

