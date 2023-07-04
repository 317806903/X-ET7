using ET.Ability;
using System;
using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(UnitComponent))]
    [FriendOf(typeof(Unit))]
    public static class UnitHitComponentSystem
	{
		public static void DoUnitHit(this UnitComponent self, float fixedDeltaTime)
		{
			foreach (Unit unitBullet in self.bulletList)
			{
				if (unitBullet.GetComponent<TeamFlagObj>() == null)
				{
					return;
				}
				foreach (Unit unitPlayer in self.playerList)
				{
					if (TeamFlagHelper.ChkIsFriend(unitBullet, unitPlayer) == false)
					{
						if (BulletHelper.ChkBulletHit(unitBullet, unitPlayer))
						{
							BulletHelper.DoBulletHit(unitBullet, unitPlayer);
						}
					}
				}
				foreach (Unit unitMonster in self.actorList)
				{
					if (TeamFlagHelper.ChkIsFriend(unitBullet, unitMonster) == false)
					{
						if (BulletHelper.ChkBulletHit(unitBullet, unitMonster))
						{
							BulletHelper.DoBulletHit(unitBullet, unitMonster);
						}
					}
				}
			}
		}
		
	}
}