using System.Collections.Generic;

namespace ET.Ability
{
	public class Action_FireBullet: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, SelectHandle selectHandle)
		{
			await TimerComponent.Instance.WaitFrameAsync();
			BulletHelper.CreateBullet(unit, 1);
		}
	}
}

