namespace ET.Ability
{
	public static class EventHandlerBuff
	{
		[Event(SceneType.Map)]
		public class EventHandler_SkillOnCast: AEvent<Scene, AbilityTriggerEventType.SkillOnCast>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.SkillOnCast args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.SkillOnCast, null, null);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Map)]
		public class EventHandler_DamageBeforeOnHit: AEvent<Scene, AbilityTriggerEventType.DamageBeforeOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageBeforeOnHit args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageBeforeOnHit, args.attackerUnit, args.defenderUnit);
					
					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitPlayer = bulletObj.GetCasterPlayerUnit();
						if (unitPlayer != null)
						{
							EventHandlerHelper.Run(unitPlayer, AbilityBuffMonitorTriggerEvent.DamageBeforeOnHit, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageBeforeOnBeHurt, args.attackerUnit, args.defenderUnit);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Map)]
		public class EventHandler_DamageAfterOnHit: AEvent<Scene, AbilityTriggerEventType.DamageAfterOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageAfterOnHit args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageAfterOnHit, args.attackerUnit, args.defenderUnit);
					
					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitPlayer = bulletObj.GetCasterPlayerUnit();
						if (unitPlayer != null)
						{
							EventHandlerHelper.Run(unitPlayer, AbilityBuffMonitorTriggerEvent.DamageAfterOnHit, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageAfterOnBeHurt, args.attackerUnit, args.defenderUnit);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Map)]
		public class EventHandler_DamageBeforeOnKill: AEvent<Scene, AbilityTriggerEventType.DamageBeforeOnKill>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageBeforeOnKill args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageBeforeOnKill, args.attackerUnit, args.defenderUnit);
					
					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitPlayer = bulletObj.GetCasterPlayerUnit();
						if (unitPlayer != null)
						{
							EventHandlerHelper.Run(unitPlayer, AbilityBuffMonitorTriggerEvent.DamageBeforeOnKill, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageBeforeOnBeKilled, args.attackerUnit, args.defenderUnit);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Map)]
		public class EventHandler_DamageAfterOnKill: AEvent<Scene, AbilityTriggerEventType.DamageAfterOnKill>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageAfterOnKill args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageAfterOnKill, args.attackerUnit, args.defenderUnit);
					
					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitPlayer = bulletObj.GetCasterPlayerUnit();
						if (unitPlayer != null)
						{
							EventHandlerHelper.Run(unitPlayer, AbilityBuffMonitorTriggerEvent.DamageAfterOnKill, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				//if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageAfterOnBeKilled, args.attackerUnit, args.defenderUnit);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnCreate, null, null);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnHit: AEvent<Scene, AbilityTriggerEventType.UnitOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnHit args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnHit, args.attackerUnit, args.defenderUnit);
					
					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitPlayer = bulletObj.GetCasterPlayerUnit();
						if (unitPlayer != null)
						{
							EventHandlerHelper.Run(unitPlayer, AbilityBuffMonitorTriggerEvent.UnitOnHit, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnBeHurt, args.attackerUnit, args.defenderUnit);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.UnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnRemoved args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnRemoved, null, null);
				}
				await ETTask.CompletedTask;
			}
		}

	}
}
