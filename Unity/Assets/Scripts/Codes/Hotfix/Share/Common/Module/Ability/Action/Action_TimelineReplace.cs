using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_TimelineReplace: IActionHandler
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
			// ActionCfg_TimelineReplace actionCfgTimelineReplace = ActionCfg_TimelineReplaceCategory.Instance.Get(actionId);
			// for (int i = 0; i < list.Count; i++)
			// {
			// 	ET.Ability.SkillHelper.ReplaceSkillTimeline(list[i], actionCfgTimelineReplace.NewTimelineCfgId);
			// }
			ActionCfg_TimelineReplace actionCfgTimelineReplace = ActionCfg_TimelineReplaceCategory.Instance.Get(actionId);
			await ET.Ability.SkillHelper.ReplaceSkillTimeline(unit, actionCfgTimelineReplace.NewTimelineCfgId);
			await ETTask.CompletedTask;
		}
	}
}
