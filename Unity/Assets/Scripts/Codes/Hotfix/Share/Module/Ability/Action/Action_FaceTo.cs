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
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
			}

			ActionCfg_FaceTo actionCfg_FaceTo = ActionCfg_FaceToCategory.Instance.Get(actionId);
			RotateHelper.DealRotate(unit, actionCfg_FaceTo, selectHandle);
			await ETTask.CompletedTask;
		}
	}
}

