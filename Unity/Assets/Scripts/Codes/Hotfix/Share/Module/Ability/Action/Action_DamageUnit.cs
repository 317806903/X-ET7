﻿using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_DamageUnit: IActionHandler
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
			ActionCfg_DamageUnit actionCfg_DamageUnit = ActionCfg_DamageUnitCategory.Instance.Get(actionId);

			bool isCriticalStrike = DamageHelper.ChkIsCriticalStrike(unit, null);
			actionContext.isCriticalStrike = isCriticalStrike;
			await DamageHelper.DoDamage(unit, actionCfg_DamageUnit, selectHandle, null, isCriticalStrike, actionContext);
			await ETTask.CompletedTask;
		}
	}
}

