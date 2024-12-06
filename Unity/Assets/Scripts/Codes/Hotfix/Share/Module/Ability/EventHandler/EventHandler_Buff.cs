namespace ET.Ability
{
	public static class EventHandler_Buff
	{
		[Event(SceneType.Map)]
		public class EventHandler_SkillOnCast: AEvent<Scene, AbilityTriggerEventType.SkillOnCast>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.SkillOnCast args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.SkillOnCast, null, null, ref actionContext);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_DamageBeforeOnHit: AEvent<Scene, AbilityTriggerEventType.DamageBeforeOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageBeforeOnHit args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageBeforeOnHit, args.attackerUnit, args.defenderUnit, ref actionContext);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						Unit unitActor = unit.GetCasterFirstActor();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.DamageBeforeOnHit, args.attackerUnit, args.defenderUnit, ref actionContext);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageBeforeOnBeHurt, args.attackerUnit, args.defenderUnit, ref actionContext);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_DamageAfterOnHit: AEvent<Scene, AbilityTriggerEventType.DamageAfterOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageAfterOnHit args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageAfterOnHit, args.attackerUnit, args.defenderUnit, ref actionContext);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						Unit unitActor = unit.GetCasterFirstActor();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.DamageAfterOnHit, args.attackerUnit, args.defenderUnit, ref actionContext);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageAfterOnBeHurt, args.attackerUnit, args.defenderUnit, ref actionContext);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_DamageBeforeOnKill: AEvent<Scene, AbilityTriggerEventType.DamageBeforeOnKill>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageBeforeOnKill args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageBeforeOnKill, args.attackerUnit, args.defenderUnit, ref actionContext);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						Unit unitActor = unit.GetCasterFirstActor();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.DamageBeforeOnKill, args.attackerUnit, args.defenderUnit, ref actionContext);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit, true))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageBeforeOnBeKilled, args.attackerUnit, args.defenderUnit, ref actionContext);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_DamageAfterOnKill: AEvent<Scene, AbilityTriggerEventType.DamageAfterOnKill>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageAfterOnKill args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageAfterOnKill, args.attackerUnit, args.defenderUnit, ref actionContext);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						Unit unitActor = unit.GetCasterFirstActor();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.DamageAfterOnKill, args.attackerUnit, args.defenderUnit, ref actionContext);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit, true))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.DamageAfterOnBeKilled, args.attackerUnit, args.defenderUnit, ref actionContext);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_UnitChgSaveSelectObj: AEvent<Scene, AbilityTriggerEventType.UnitChgSaveSelectObj>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitChgSaveSelectObj args)
			{
				ActionContext actionContext = args.actionContext;
				Unit unit = args.unit;
				EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitChgSaveSelectObj, null, null, ref actionContext);
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				ActionContext actionContext = args.actionContext;
				Unit unit = args.unit;
				if (UnitHelper.ChkUnitAlive(args.unit))
				{
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnCreate, null, null, ref actionContext);
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
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.attackerUnit) && UnitHelper.ChkIsBullet(args.attackerUnit))
				{
					Unit bulletUnit = args.attackerUnit;
					BulletObj bulletObj = bulletUnit.GetComponent<BulletObj>();
					Unit unitActor = bulletUnit.GetCasterFirstActor();
					if (unitActor != null)
                    {
                        EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.BulletOnHitMesh, bulletUnit, null, ref actionContext);
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
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnHitMesh, unit, null, ref actionContext);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						Unit unitActor = unit.GetCasterFirstActor();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.UnitOnHitMesh, unit, null, ref actionContext);
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
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnHitPos, unit, null, ref actionContext);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						Unit unitActor = unit.GetCasterFirstActor();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.UnitOnHitPos, unit, null, ref actionContext);
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
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnHit, args.attackerUnit, args.defenderUnit, ref actionContext);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						Unit unitActor = unit.GetCasterFirstActor();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.UnitOnHit, args.attackerUnit, args.defenderUnit, ref actionContext);
						}
					}
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnBeHurt, args.attackerUnit, args.defenderUnit, ref actionContext);
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
				ActionContext actionContext = args.actionContext;
				Unit unit = args.unit;
				if (UnitHelper.ChkUnitAlive(unit, true))
				{
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.UnitOnRemoved, null, null, ref actionContext);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_CallBullet: AEvent<Scene, AbilityTriggerEventType.CallBullet>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.CallBullet args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.CallBullet, null, null, ref actionContext);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						Unit unitActor = unit.GetCasterFirstActor();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.CallBullet, null, null, ref actionContext);
						}
					}
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_CallAoe: AEvent<Scene, AbilityTriggerEventType.CallAoe>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.CallAoe args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.CallAoe, null, null, ref actionContext);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						Unit unitActor = unit.GetCasterFirstActor();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.CallAoe, null, null, ref actionContext);
						}
					}
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_CallActor: AEvent<Scene, AbilityTriggerEventType.CallActor>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.CallActor args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Buff(unit, AbilityConfig.BuffTriggerEvent.CallActor, null, null, ref actionContext);

					if (UnitHelper.ChkUnitAlive(unit) && UnitHelper.ChkIsBullet(unit))
					{
						Unit unitActor = unit.GetCasterFirstActor();
						if (unitActor != null)
						{
							EventHandlerHelper.Run_Buff(unitActor, AbilityConfig.BuffTriggerEvent.CallActor, null, null, ref actionContext);
						}
					}
				}
				await ETTask.CompletedTask;
			}
		}

	}
}
