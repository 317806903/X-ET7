namespace ET.Ability
{
	public static class EventHandlerBullet
	{
		[Event(SceneType.Current)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				if (UnitHelper.ChkUnitAlive(scene, args.unitId) && UnitHelper.ChkIsBullet(scene, args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(scene, args.unitId);
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
				if (UnitHelper.ChkUnitAlive(scene, args.attackerUnitId) && UnitHelper.ChkIsBullet(scene, args.attackerUnitId))
				{
					Unit unit = UnitHelper.GetUnit(scene, args.attackerUnitId);
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnHit);
				}
				if (UnitHelper.ChkUnitAlive(scene, args.defenderUnitId) && UnitHelper.ChkIsBullet(scene, args.defenderUnitId))
				{
					Unit unit = UnitHelper.GetUnit(scene, args.defenderUnitId);
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
				if (UnitHelper.ChkUnitAlive(scene, args.unitId) && UnitHelper.ChkIsBullet(scene, args.unitId))
				{
					Unit unit = UnitHelper.GetUnit(scene, args.unitId);
					EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnRemoved);
				}
				await ETTask.CompletedTask;
			}
		}

	}
}
