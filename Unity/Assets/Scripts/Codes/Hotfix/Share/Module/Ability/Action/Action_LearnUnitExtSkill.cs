using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_LearnUnitExtSkill: IActionHandler
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
			ActionCfg_LearnUnitExtSkill actionCfg_LearnUnitExtSkill = ActionCfg_LearnUnitExtSkillCategory.Instance.Get(actionId);

			for (int i = 0; i < list.Count; i++)
			{
				Unit targetUnit = list[i];

				bool isSkillClearWhenExt = false;
				Dictionary<string, SkillSlotType> extSkillList = null;
				UnitCfg unitCfg = targetUnit.model;
				if (actionCfg_LearnUnitExtSkill.ExtSkillIndex == 1)
				{
					isSkillClearWhenExt = unitCfg.IsSkillClearWhenExt1;
					extSkillList = unitCfg.ExtSkillList1;
				}
				else if (actionCfg_LearnUnitExtSkill.ExtSkillIndex == 2)
				{
					isSkillClearWhenExt = unitCfg.IsSkillClearWhenExt2;
					extSkillList = unitCfg.ExtSkillList2;
				}
				else
				{
					return;
				}

				if (isSkillClearWhenExt)
				{
					SkillHelper.ForgetSkill(targetUnit, "", ET.AbilityConfig.SkillSlotType.None, -1, SkillGroupType.None);
				}
				foreach (var item in extSkillList)
				{
					string skillCfgId = item.Key;
					ET.AbilityConfig.SkillSlotType skillSlotType = item.Value;
					SkillHelper.LearnSkill(targetUnit, skillCfgId, 1, skillSlotType);
				}
			}
			list.Dispose();

			await ETTask.CompletedTask;
		}
	}
}
