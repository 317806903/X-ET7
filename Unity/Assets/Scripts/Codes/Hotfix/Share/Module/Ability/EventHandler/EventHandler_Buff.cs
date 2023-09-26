namespace ET.Ability
{
	public static class EventHandler_Buff
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
						Unit unitActor = bulletObj.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run(unitActor, AbilityBuffMonitorTriggerEvent.DamageBeforeOnHit, args.attackerUnit, args.defenderUnit);
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
						Unit unitActor = bulletObj.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run(unitActor, AbilityBuffMonitorTriggerEvent.DamageAfterOnHit, args.attackerUnit, args.defenderUnit);
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
						Unit unitActor = bulletObj.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run(unitActor, AbilityBuffMonitorTriggerEvent.DamageBeforeOnKill, args.attackerUnit, args.defenderUnit);
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
						Unit unitActor = bulletObj.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run(unitActor, AbilityBuffMonitorTriggerEvent.DamageAfterOnKill, args.attackerUnit, args.defenderUnit);
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
		public class EventHandler_UnitChgSaveSelectObj: AEvent<Scene, AbilityTriggerEventType.UnitChgSaveSelectObj>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitChgSaveSelectObj args)
			{
				Unit unit = args.unit;
				EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitChgSaveSelectObj, null, null);
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				Unit unit = args.unit;
				if (UnitHelper.ChkUnitAlive(args.unit))
				{
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnCreate, null, null);
				}

				AOIEntity aoiEntity = unit.GetComponent<AOIEntity>();
				if (aoiEntity != null)
				{
					foreach (var BeSeeUnit in aoiEntity.BeSeeUnits)
					{
						AOIEntity aoiEntityTmp = BeSeeUnit.Value;
						Unit beSeeUnit = aoiEntityTmp?.Unit;
						if (UnitHelper.ChkUnitAlive(beSeeUnit))
						{
							EventHandlerHelper.Run(beSeeUnit, AbilityBuffMonitorTriggerEvent.NearUnitOnCreate, null, null);
						}
					}
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_BulletOnHitMesh: AEvent<Scene, AbilityTriggerEventType.BulletOnHitMesh>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.BulletOnHitMesh args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit) && UnitHelper.ChkIsBullet(args.attackerUnit))
				{
					Unit bulletUnit = args.attackerUnit;
					BulletObj bulletObj = bulletUnit.GetComponent<BulletObj>();
					Unit unitActor = bulletObj.GetCasterActorUnit();
					if (unitActor != null)
                    {
                        EventHandlerHelper.Run(unitActor, AbilityBuffMonitorTriggerEvent.BulletOnHitMesh, bulletUnit, null);
                    }
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_UnitOnHitMesh: AEvent<Scene, AbilityTriggerEventType.UnitOnHitMesh>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnHitMesh args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnHitMesh, unit, null);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitActor = bulletObj.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run(unitActor, AbilityBuffMonitorTriggerEvent.UnitOnHitMesh, unit, null);
						}
					}
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
						Unit unitActor = bulletObj.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run(unitActor, AbilityBuffMonitorTriggerEvent.UnitOnHit, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnBeHurt, args.attackerUnit, args.defenderUnit);
				}

				Unit defenderUnit = args.defenderUnit;
				AOIEntity aoiEntity = defenderUnit.GetComponent<AOIEntity>();
				if (aoiEntity != null)
				{
					foreach (var BeSeeUnit in aoiEntity.BeSeeUnits)
					{
						AOIEntity aoiEntityTmp = BeSeeUnit.Value;
						Unit beSeeUnit = aoiEntityTmp?.Unit;
						if (UnitHelper.ChkUnitAlive(beSeeUnit))
						{
							EventHandlerHelper.Run(beSeeUnit, AbilityBuffMonitorTriggerEvent.NearUnitOnHit, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_UnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.UnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnRemoved args)
			{
				Unit unit = args.unit;
				if (UnitHelper.ChkUnitAlive(unit))
				{
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnRemoved, null, null);
				}

				AOIEntity aoiEntity = unit.GetComponent<AOIEntity>();
				if (aoiEntity != null)
				{
					foreach (var BeSeeUnit in aoiEntity.BeSeeUnits)
					{
						AOIEntity aoiEntityTmp = BeSeeUnit.Value;
						Unit beSeeUnit = aoiEntityTmp?.Unit;
						if (UnitHelper.ChkUnitAlive(beSeeUnit))
						{
							EventHandlerHelper.Run(beSeeUnit, AbilityBuffMonitorTriggerEvent.NearUnitOnRemoved, null, null);
						}
					}
				}
				await ETTask.CompletedTask;
			}
		}

	}
}
