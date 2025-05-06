using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_SkillCast: IActionHandler
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
			ActionCfg_SkillCast actionCfg_SkillCast = ActionCfg_SkillCastCategory.Instance.Get(actionId);

			for (int i = 0; i < list.Count; i++)
			{
				Unit targetUnit = list[i];

				List<SkillObj> skillList = ET.Ability.SkillHelper.GetSkillList(targetUnit, actionCfg_SkillCast.SkillId, actionCfg_SkillCast.SkillSlotType, actionCfg_SkillCast.SkillSlotIndex, actionCfg_SkillCast.SkillGroupType);
				if (skillList == null)
				{
					continue;
				}
				foreach (var skillObj in skillList)
				{
					await SkillHelper.CastSkill(targetUnit, skillObj.skillCfgId, null);
				}
			}
			list.Dispose();

			await ETTask.CompletedTask;
		}
	}
}
