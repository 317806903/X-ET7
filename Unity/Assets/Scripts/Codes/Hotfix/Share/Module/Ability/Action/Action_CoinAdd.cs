using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_CoinAdd: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}

			ActionCfg_CoinAdd actionCfgCoinAdd = ActionCfg_CoinAddCategory.Instance.Get(actionId);
			CoinHelper.DealCoinAdd(unit, actionCfgCoinAdd, selectHandle, ref actionContext);
			await ETTask.CompletedTask;
		}
	}
}

