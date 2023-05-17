namespace ET.Ability
{
	public static class EventHandlerBullet
	{
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				if (UnitHelper.ChkUnitAlive(args.createUnit) && UnitHelper.ChkIsBullet(args.createUnit))
				{
					Unit unit = args.createUnit;
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnCreate, null, null);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnHit: AEvent<Scene, AbilityTriggerEventType.UnitOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnHit args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit) && UnitHelper.ChkIsBullet(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnHit, args.attackerUnit, args.defenderUnit);
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit) && UnitHelper.ChkIsBullet(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnBeHurt, args.attackerUnit, args.defenderUnit);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.UnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnRemoved args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsBullet(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnRemoved, null, null);
				}
				await ETTask.CompletedTask;
			}
		}

	}
}
