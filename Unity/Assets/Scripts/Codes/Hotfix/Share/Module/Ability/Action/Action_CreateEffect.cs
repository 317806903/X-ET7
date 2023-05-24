using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_CreateEffect: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, SelectHandle selectHandle)
		{
			//await TimerComponent.Instance.WaitFrameAsync();
			ActionCfg_CreateEffect actionCfgCreateEffect = ActionCfg_CreateEffectCategory.Instance.Get(actionId);
			EffectHelper.AddEffect(unit, actionCfgCreateEffect, selectHandle);
			await ETTask.CompletedTask;
		}
	}
}


