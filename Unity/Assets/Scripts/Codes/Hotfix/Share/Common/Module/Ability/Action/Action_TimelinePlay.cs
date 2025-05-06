using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_TimelinePlay: IActionHandler
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

			ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext);
			if (list == null)
			{
				return;
			}
			ActionCfg_TimelinePlay actionCfgTimelinePlay = ActionCfg_TimelinePlayCategory.Instance.Get(actionId);
			for (int i = 0; i < list.Count; i++)
			{
				await ET.Ability.TimelineHelper.PlayTimeline(list[i], unit.Id, actionCfgTimelinePlay.NewTimelineCfgId, actionContext);
			}
			list.Dispose();
			await ETTask.CompletedTask;
		}
	}
}
