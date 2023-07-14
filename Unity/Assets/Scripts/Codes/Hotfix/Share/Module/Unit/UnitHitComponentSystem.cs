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
				Dictionary<long, AOIEntity> seeUnits = unitBullet.GetComponent<AOIEntity>().GetSeeUnits();
				foreach (var seeUnit in seeUnits)
				{
					Unit unit = seeUnit.Value.Unit;
					bool isChkHit = false;
					if (UnitHelper.ChkIsPlayer(unit) || UnitHelper.ChkIsActor(unit))
					{
						isChkHit = true;
					}
					else
					{
						isChkHit = false;
					}
					if (isChkHit == false)
					{
						continue;
					}
					ProfilerSample.BeginSample("seeUnits ET.GamePlayHelper.ChkIsFriend");
					bool isFriend = ET.GamePlayHelper.ChkIsFriend(unitBullet, unit);
					ProfilerSample.EndSample();
					if (isFriend == false)
					{
						ProfilerSample.BeginSample("seeUnits BulletHelper.ChkBulletHit");
						bool isHit = BulletHelper.ChkBulletHit(unitBullet, unit);
						ProfilerSample.EndSample();
						if (isHit)
						{
							BulletHelper.DoBulletHit(unitBullet, unit);
						}
					}
				}
				
				// foreach (Unit unitPlayer in self.playerList)
				// {
				// 	ProfilerSample.BeginSample("playerList ET.GamePlayHelper.ChkIsFriend");
				// 	bool isFriend = ET.GamePlayHelper.ChkIsFriend(unitBullet, unitPlayer);
				// 	ProfilerSample.EndSample();
				// 	if (isFriend == false)
				// 	{
				// 		ProfilerSample.BeginSample("playerList BulletHelper.ChkBulletHit");
				// 		bool isHit = BulletHelper.ChkBulletHit(unitBullet, unitPlayer);
				// 		ProfilerSample.EndSample();
				// 		if (isHit)
				// 		{
				// 			BulletHelper.DoBulletHit(unitBullet, unitPlayer);
				// 		}
				// 	}
				// }
				// foreach (Unit unitMonster in self.actorList)
				// {
				// 	ProfilerSample.BeginSample("actorList ET.GamePlayHelper.ChkIsFriend");
				// 	bool isFriend = ET.GamePlayHelper.ChkIsFriend(unitBullet, unitMonster);
				// 	ProfilerSample.EndSample();
				// 	if (isFriend == false)
				// 	{
				// 		ProfilerSample.BeginSample("actorList BulletHelper.ChkBulletHit");
				// 		bool isHit = BulletHelper.ChkBulletHit(unitBullet, unitMonster);
				// 		ProfilerSample.EndSample();
				// 		if (isHit)
				// 		{
				// 			BulletHelper.DoBulletHit(unitBullet, unitMonster);
				// 		}
				// 	}
				// }
			}
		}
		
	}
}