using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_FireBullet: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, SelectHandle selectHandle)
		{
			await TimerComponent.Instance.WaitFrameAsync();
			ActionCfg_FireBullet actionCfgFireBullet = ActionCfg_FireBulletCategory.Instance.Get(actionId);
			BulletHelper.CreateBullet(unit, actionCfgFireBullet, selectHandle);
		}
	}
}

