using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_FireBullet: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			await TimerComponent.Instance.WaitFrameAsync();
			ActionCfg_FireBullet actionCfgFireBullet = ActionCfg_FireBulletCategory.Instance.Get(actionId);
			BulletHelper.CreateBullet(unit, actionCfgFireBullet, selectHandle, actionContext);
			await ETTask.CompletedTask;
		}
	}
}

