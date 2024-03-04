using ET.Ability;
using System;
using System.Collections.Generic;
using Unity.Mathematics;

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
				if (UnitHelper.ChkUnitAlive(unitBullet) == false)
				{
					continue;
				}

				BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();
				if (bulletObj.ChkCanTrigHit() == false)
				{
					continue;
				}

				HashSet<long> preHitUnitIds = bulletObj.GetPreHitUnit();
				if (preHitUnitIds != null)
				{
					foreach (long preHitUnitId in preHitUnitIds)
					{
						Unit preHitUnit = UnitHelper.GetUnit(self.DomainScene(), preHitUnitId);
						BulletHelper.DoBulletHitUnit(unitBullet, preHitUnit);
					}
					bulletObj.ResetPreHitUnit();
					if (bulletObj.ChkCanTrigHit() == false)
					{
						continue;
					}
				}

				var seeUnits = unitBullet.GetComponent<AOIEntity>().GetSeeUnits();
				foreach (var seeUnit in seeUnits)
				{
					AOIEntity aoiEntityTmp = seeUnit.Value;
					Unit unit = aoiEntityTmp.Unit;
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
					bool isFriend = ET.GamePlayHelper.ChkIsFriend(unitBullet, unit);
					if (isFriend == false)
					{
						(bool bHitUnit, bool bHitMesh, float3 hitPos) = BulletHelper.ChkBulletHit(unitBullet, unit);
						if (bHitUnit)
						{
							BulletHelper.DoBulletHitUnit(unitBullet, unit);
						}
						if (bHitMesh)
						{
							BulletHelper.DoBulletHitMesh(unitBullet, hitPos);
						}
					}
				}
			}
		}

	}
}