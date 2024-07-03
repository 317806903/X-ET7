using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_EffectRecordPointLightningTrailTarget: IActionHandler
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

			ActionCfg_EffectRecordPointLightningTrailTarget actionCfgEffectRecordPointLightningTrailTarget = ActionCfg_EffectRecordPointLightningTrailTargetCategory.Instance.Get(actionId);

			List<EffectObj> effctObjList = ET.Ability.EffectHelper.GetEffectsByEffectShowType(unit, EffectShowType.PointLightningTrail);
			if (effctObjList == null || effctObjList.Count == 0)
			{
				return;
			}

			ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext, true);
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < effctObjList.Count; i++)
			{
				EffectObj effectObj = effctObjList[i];
				for (int i2 = 0; i2 < list.Count; i2++)
				{
					Unit targetUnit = list[i2];
					effectObj.AddPointLightningTrail(targetUnit.Id);
				}
				effectObj.GetEffectComponent().NoticeClientRefreshEffectObj(effectObj);
			}
			list.Dispose();


			await ETTask.CompletedTask;
		}
	}
}

