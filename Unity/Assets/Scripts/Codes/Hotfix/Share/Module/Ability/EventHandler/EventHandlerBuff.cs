namespace ET.Ability
{
	public static class EventHandlerBuff
	{
		[Event(SceneType.Current)]
		public class EventHandler_SkillOnCast: AEvent<Scene, AbilityTriggerEventType.SkillOnCast>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.SkillOnCast args)
			{
				if (UnitHelper.ChkUnitAlive(args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(args.unitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.SkillOnCast);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_BuffOnAwake: AEvent<Scene, AbilityTriggerEventType.BuffOnAwake>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.BuffOnAwake args)
			{
				if (UnitHelper.ChkUnitAlive(args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(args.unitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.BuffOnAwake);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_BuffOnStart: AEvent<Scene, AbilityTriggerEventType.BuffOnStart>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.BuffOnStart args)
			{
				if (UnitHelper.ChkUnitAlive(args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(args.unitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.BuffOnStart);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_BuffOnRefresh: AEvent<Scene, AbilityTriggerEventType.BuffOnRefresh>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.BuffOnRefresh args)
			{
				if (UnitHelper.ChkUnitAlive(args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(args.unitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.BuffOnRefresh);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_BuffOnRemoved: AEvent<Scene, AbilityTriggerEventType.BuffOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.BuffOnRemoved args)
			{
				if (UnitHelper.ChkUnitAlive(args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(args.unitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.BuffOnRemoved);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_BuffOnDestroy: AEvent<Scene, AbilityTriggerEventType.BuffOnDestroy>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.BuffOnDestroy args)
			{
				if (UnitHelper.ChkUnitAlive(args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(args.unitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.BuffOnDestroy);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_DamageBeforeOnHit: AEvent<Scene, AbilityTriggerEventType.DamageBeforeOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageBeforeOnHit args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.attackerUnitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageBeforeOnHit);
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.defenderUnitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageBeforeOnBeHurt);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_DamageAfterOnHit: AEvent<Scene, AbilityTriggerEventType.DamageAfterOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageAfterOnHit args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.attackerUnitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageAfterOnHit);
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.defenderUnitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageAfterOnBeHurt);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_DamageBeforeOnKill: AEvent<Scene, AbilityTriggerEventType.DamageBeforeOnKill>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageBeforeOnKill args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.attackerUnitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageBeforeOnKill);
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.defenderUnitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageBeforeOnBeKilled);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_DamageAfterOnKill: AEvent<Scene, AbilityTriggerEventType.DamageAfterOnKill>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.DamageAfterOnKill args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.attackerUnitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageAfterOnKill);
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.defenderUnitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.DamageAfterOnBeKilled);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				if (UnitHelper.ChkUnitAlive(args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(args.unitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnCreate);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_UnitOnHit: AEvent<Scene, AbilityTriggerEventType.UnitOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnHit args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.attackerUnitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnHit);
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.defenderUnitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnBeHurt);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_UnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.UnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnRemoved args)
			{
				if (UnitHelper.ChkUnitAlive(args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(args.unitId);
					EventHandlerHelper.Run(unit, AbilityBuffMonitorTriggerEvent.UnitOnRemoved);
				}
				await ETTask.CompletedTask;
			}
		}

	}
}
