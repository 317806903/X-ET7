namespace ET.Ability
{
	public static class EventHandlerBullet
	{
		[Event(SceneType.Current)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsBullet(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnCreate);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_UnitOnHit: AEvent<Scene, AbilityTriggerEventType.UnitOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnHit args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit) && UnitHelper.ChkIsBullet(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnHit);
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit) && UnitHelper.ChkIsBullet(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnBeHurt);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Current)]
		public class EventHandler_UnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.UnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnRemoved args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsBullet(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnRemoved);
				}
				await ETTask.CompletedTask;
			}
		}

	}
}
