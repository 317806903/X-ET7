using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_TimelineJumpTime: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
				if (unit == null || unit.DomainScene() == null || unit.DomainScene().IsDisposed)
				{
					return;
				}
			}

			// List<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, actionContext);
			// if (list == null)
			// {
			// 	return;
			// }
			// ActionCfg_TimelineJumpTime actionCfgTimelineJumpTime = ActionCfg_TimelineJumpTimeCategory.Instance.Get(actionId);
			// for (int i = 0; i < list.Count; i++)
			// {
			// 	ET.Ability.TimelineHelper.JumpTimeline(list[i], actionCfgTimelineJumpTime.NewTimeElapsed, actionContext);
			// }
			ActionCfg_TimelineJumpTime actionCfgTimelineJumpTime = ActionCfg_TimelineJumpTimeCategory.Instance.Get(actionId);
			ET.Ability.TimelineHelper.JumpTimeline(unit, actionCfgTimelineJumpTime.NewTimeElapsed, ref actionContext);
			await ETTask.CompletedTask;
		}
	}
}
