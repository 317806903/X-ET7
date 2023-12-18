namespace ET.Ability
{
	public static class EventHandler_Bullet
	{
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				if (UnitHelper.ChkUnitAlive(args.createUnit) && UnitHelper.ChkIsBullet(args.createUnit))
				{
					Unit unit = args.createUnit;
					EventHandlerHelper.Run_Bullet(unit, AbilityBulletMonitorTriggerEvent.BulletOnCreate, null, null);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_BulletOnHit: AEvent<Scene, AbilityTriggerEventType.BulletOnHit>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.BulletOnHit args)
			{
				if (UnitHelper.ChkUnitAlive(args.attackerUnit) && UnitHelper.ChkIsBullet(args.attackerUnit))
				{
					Unit unit = args.attackerUnit;
					EventHandlerHelper.Run_Bullet(unit, AbilityBulletMonitorTriggerEvent.BulletOnHit, args.attackerUnit, args.defenderUnit);
				}
				if (UnitHelper.ChkUnitAlive(args.defenderUnit) && UnitHelper.ChkIsBullet(args.defenderUnit))
				{
					Unit unit = args.defenderUnit;
					EventHandlerHelper.Run_Bullet(unit, AbilityBulletMonitorTriggerEvent.BulletOnBeHurt, args.attackerUnit, args.defenderUnit);
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
					Unit unit = args.attackerUnit;
					BulletObj bulletObj = unit.GetComponent<BulletObj>();
					bulletObj.actionContext.hitPosition = args.hitPos;
					EventHandlerHelper.Run_Bullet(unit, AbilityBulletMonitorTriggerEvent.BulletOnHitMesh, args.attackerUnit, null);
				}
				await ETTask.CompletedTask;
			}
		}

		// [Event(SceneType.Map)]
		// public class EventHandler_UnitOnHit: AEvent<Scene, AbilityTriggerEventType.UnitOnHit>
		// {
		// 	protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnHit args)
		// 	{
		// 		if (UnitHelper.ChkUnitAlive(args.attackerUnit) && UnitHelper.ChkIsBullet(args.attackerUnit))
		// 		{
		// 			Unit unit = args.attackerUnit;
		// 			EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnHit, args.attackerUnit, args.defenderUnit);
		// 		}
		// 		if (UnitHelper.ChkUnitAlive(args.defenderUnit) && UnitHelper.ChkIsBullet(args.defenderUnit))
		// 		{
		// 			Unit unit = args.defenderUnit;
		// 			EventHandlerHelper.Run(unit, AbilityBulletMonitorTriggerEvent.BulletOnBeHurt, args.attackerUnit, args.defenderUnit);
		// 		}
		// 		await ETTask.CompletedTask;
		// 	}
		// }

		[Event(SceneType.Map)]
		public class EventHandler_UnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.UnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnRemoved args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsBullet(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Bullet(unit, AbilityBulletMonitorTriggerEvent.BulletOnRemoved, null, null);
				}
				await ETTask.CompletedTask;
			}
		}

	}
}
