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
			HashSet<Unit> bulletList = self.GetRecordList(UnitType.Bullet);
			if (bulletList == null)
			{
				return;
			}
			foreach (Unit unitBullet in bulletList)
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

				bool preHitPos = bulletObj.ChkPreHitPos();
				if (preHitPos)
				{
					BulletHelper.DoBulletHitPos(unitBullet);
					if (bulletObj.ChkCanTrigHit() == false)
					{
						continue;
					}
				}
				HashSet<long> preHitUnitIds = bulletObj.GetPreHitUnit();
				if (preHitUnitIds != null)
				{
					foreach (long preHitUnitId in preHitUnitIds)
					{
						Unit preHitUnit = UnitHelper.GetUnit(self.DomainScene(), preHitUnitId);
						BulletHelper.DoBulletHitUnit(unitBullet, preHitUnit, UnitHelper.GetBeAttackPointPos(preHitUnit));
					}
					bulletObj.ResetPreHitUnit();
					if (bulletObj.ChkCanTrigHit() == false)
					{
						continue;
					}
				}
				MoveTweenObj moveTweenObj = unitBullet.GetComponent<MoveTweenObj>();
				if (moveTweenObj != null && moveTweenObj.IsNeedChkTouch() == false)
				{
					continue;
				}

				var seeUnits = unitBullet.GetComponent<AOIEntity>().GetSeeUnits();
				foreach (var seeUnit in seeUnits)
				{
					if (bulletObj.ChkCanTrigHit() == false)
					{
						break;
					}
					AOIEntity aoiEntityTmp = seeUnit.Value;
					Unit unit = aoiEntityTmp.Unit;
					if (unit == unitBullet)
					{
						continue;
					}
					bool isChkHit = false;
					if (UnitHelper.ChkIsPlayer(unit)
					    || UnitHelper.ChkIsCameraPlayer(unit)
					    || UnitHelper.ChkIsSkillCaster(unit)
					    || UnitHelper.ChkIsActor(unit))
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
						(bool isHitUnit, bool isHitMesh, float3 hitUnitPos, float3 hitMeshPos) = BulletHelper.ChkBulletHit(unitBullet, unit);
						if (isHitUnit)
						{
							BulletHelper.DoBulletHitUnit(unitBullet, unit, hitUnitPos);
						}
						if (isHitMesh)
						{
							BulletHelper.DoBulletHitMesh(unitBullet, hitMeshPos);
						}
					}
				}

				if (bulletObj.model.CanHitMesh)
				{
					(bool isHitMesh, float3 hitMeshPos) = BulletHelper.ChkBulletHitMesh(unitBullet);
					if (isHitMesh)
					{
						BulletHelper.DoBulletHitMesh(unitBullet, hitMeshPos);
					}
				}
			}
		}

	}
}