using System.Collections.Generic;

namespace ET.Ability
{
	public class Action_FireBullet: IActionHandler
	{
		public override async ETTask Run(Unit unit, string actionId, Dictionary<string, object> param)
		{
			BulletHelper.CreateBullet(unit, 1);
		}
	}
}

