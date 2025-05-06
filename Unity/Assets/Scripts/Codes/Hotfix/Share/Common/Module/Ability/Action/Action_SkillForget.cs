using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_SkillForget: IActionHandler
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

			ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext, true);
			if (list == null)
			{
				return;
			}
			ActionCfg_SkillForget actionCfg_SkillForget = ActionCfg_SkillForgetCategory.Instance.Get(actionId);

			for (int i = 0; i < list.Count; i++)
			{
				Unit targetUnit = list[i];

				ET.Ability.SkillHelper.ForgetSkill(targetUnit, actionCfg_SkillForget.SkillId, actionCfg_SkillForget.SkillSlotType, actionCfg_SkillForget.SkillSlotIndex, actionCfg_SkillForget.SkillGroupType);
			}
			list.Dispose();

			await ETTask.CompletedTask;
		}
	}
}
