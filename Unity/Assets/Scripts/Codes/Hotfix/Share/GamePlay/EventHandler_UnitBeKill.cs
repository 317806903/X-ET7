using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public static class EventHandler_UnitBeKill
	{
		[Event(SceneType.Map)]
		public class EventHandler_DamageAfterOnKill: AEvent<Scene, ET.Ability.AbilityTriggerEventType.DamageAfterOnKill>
		{
			protected override async ETTask Run(Scene scene, ET.Ability.AbilityTriggerEventType.DamageAfterOnKill args)
			{
				Unit attackerUnit = args.attackerUnit;
				Unit beKillUnit = args.defenderUnit;
				if (attackerUnit == null || beKillUnit == null)
				{
					return;
				}
				// if (UnitHelper.ChkUnitAlive(attackerUnit) && UnitHelper.ChkIsBullet(attackerUnit))
				// {
				// 	BulletObj bulletObj = attackerUnit.GetComponent<BulletObj>();
				// 	attackerUnit = bulletObj.GetCasterActorUnit();
				// }
				// else if (UnitHelper.ChkUnitAlive(attackerUnit) && UnitHelper.ChkIsAoe(attackerUnit))
				// {
				// 	AoeObj aoeObj = attackerUnit.GetComponent<AoeObj>();
				// 	attackerUnit = aoeObj.GetCasterActorUnit();
				// }
				attackerUnit = UnitHelper.GetCasterActorUnit(attackerUnit);
				GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
				gamePlayComponent.DealUnitBeKill(attackerUnit, beKillUnit);
				await ETTask.CompletedTask;
			}
		}
	}
}