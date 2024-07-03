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
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.SkillOnCast, null, null);
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
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageBeforeOnHit, args.attackerUnit, args.defenderUnit);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitActor = bulletObj?.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.DamageBeforeOnHit, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageBeforeOnBeHurt, args.attackerUnit, args.defenderUnit);
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
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageAfterOnHit, args.attackerUnit, args.defenderUnit);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitActor = bulletObj?.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.DamageAfterOnHit, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageAfterOnBeHurt, args.attackerUnit, args.defenderUnit);
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
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageBeforeOnKill, args.attackerUnit, args.defenderUnit);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitActor = bulletObj?.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.DamageBeforeOnKill, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageBeforeOnBeKilled, args.attackerUnit, args.defenderUnit);
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
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageAfterOnKill, args.attackerUnit, args.defenderUnit);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitActor = bulletObj?.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.DamageAfterOnKill, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				//if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageAfterOnBeKilled, args.attackerUnit, args.defenderUnit);
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
				EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitChgSaveSelectObj, null, null);
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
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnCreate, null, null);
				}

				// AOIEntity aoiEntity = unit.GetComponent<AOIEntity>();
				// if (aoiEntity != null)
				// {
				// 	foreach (var BeSeeUnit in aoiEntity.BeSeeUnits)
				// 	{
				// 		AOIEntity aoiEntityTmp = BeSeeUnit.Value;
				// 		Unit beSeeUnit = aoiEntityTmp?.Unit;
				// 		if (UnitHelper.ChkUnitAlive(beSeeUnit))
				// 		{
				// 			EventHandlerHelper.Run_Buff(beSeeUnit, AbilityConfig.BuffTriggerEvent.NearUnitOnCreate, null, null);
				// 		}
				// 	}
				// }
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
					Unit unitActor = bulletObj?.GetCasterActorUnit();
					if (unitActor != null)
                    {
                        EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.BulletOnHitMesh, bulletUnit, null);
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
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnHitMesh, unit, null);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitActor = bulletObj?.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.UnitOnHitMesh, unit, null);
						}
					}
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_UnitOnHitPos: AEvent<Scene, AbilityTriggerEventType.UnitOnHitPos>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnHitPos args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnHitPos, unit, null);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitActor = bulletObj?.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.UnitOnHitPos, unit, null);
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
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnHit, args.attackerUnit, args.defenderUnit);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						BulletObj bulletObj = unit.GetComponent<BulletObj>();
						Unit unitActor = bulletObj?.GetCasterActorUnit();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.UnitOnHit, args.attackerUnit, args.defenderUnit);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnBeHurt, args.attackerUnit, args.defenderUnit);
				}

				// Unit defenderUnit = args.defenderUnit;
				// if (defenderUnit != null)
				// {
				// 	AOIEntity aoiEntity = defenderUnit.GetComponent<AOIEntity>();
				// 	if (aoiEntity != null)
				// 	{
				// 		foreach (var BeSeeUnit in aoiEntity.BeSeeUnits)
				// 		{
				// 			AOIEntity aoiEntityTmp = BeSeeUnit.Value;
				// 			Unit beSeeUnit = aoiEntityTmp?.Unit;
				// 			if (UnitHelper.ChkUnitAlive(beSeeUnit))
				// 			{
				// 				EventHandlerHelper.Run_Buff(beSeeUnit, AbilityConfig.BuffTriggerEvent.NearUnitOnHit, args.attackerUnit, args.defenderUnit);
				// 			}
				// 		}
				// 	}
				// }
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
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnRemoved, null, null);
				}

				// AOIEntity aoiEntity = unit.GetComponent<AOIEntity>();
				// if (aoiEntity != null)
				// {
				// 	foreach (var BeSeeUnit in aoiEntity.BeSeeUnits)
				// 	{
				// 		AOIEntity aoiEntityTmp = BeSeeUnit.Value;
				// 		Unit beSeeUnit = aoiEntityTmp?.Unit;
				// 		if (UnitHelper.ChkUnitAlive(beSeeUnit))
				// 		{
				// 			EventHandlerHelper.Run_Buff(beSeeUnit, AbilityConfig.BuffTriggerEvent.NearUnitOnRemoved, null, null);
				// 		}
				// 	}
				// }
				await ETTask.CompletedTask;
			}
		}

	}
}
