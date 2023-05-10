namespace ET.Ability
{
	public static class EventHandlerBullet
	{
		[Event(SceneType.Current)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				if (UnitHelper.ChkUnitAlive(args.unitId) && UnitHelper.ChkIsBullet(args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(args.unitId);
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
				if (UnitHelper.ChkUnitAlive(args.attackerUnitId) && UnitHelper.ChkIsBullet(args.attackerUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.attackerUnitId);
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnHit);
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnitId) && UnitHelper.ChkIsBullet(args.defenderUnitId))
				{
					Unit unit = UnitHelper.GetUnit(args.defenderUnitId);
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
				if (UnitHelper.ChkUnitAlive(args.unitId) && UnitHelper.ChkIsBullet(args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(args.unitId);
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnRemoved);
				}
				await ETTask.CompletedTask;
			}
		}

	}
}
