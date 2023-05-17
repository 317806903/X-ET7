namespace ET.Ability
{
	public static class EventHandlerAoe
	{
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityAoeMonitorTriggerEvent.AoeOnCreate);
				}
				await ETTask.CompletedTask;
			}
		}
		
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.UnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnRemoved args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityAoeMonitorTriggerEvent.AoeOnRemoved);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_AoeOnCharacterEnter: AEvent<Scene, AbilityTriggerEventType.AoeOnCharacterEnter>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.AoeOnCharacterEnter args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityAoeMonitorTriggerEvent.AoeOnCharacterEnter);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_AoeOnCharacterLeave: AEvent<Scene, AbilityTriggerEventType.AoeOnCharacterLeave>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.AoeOnCharacterLeave args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityAoeMonitorTriggerEvent.AoeOnCharacterLeave);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_AoeOnBulletEnter: AEvent<Scene, AbilityTriggerEventType.AoeOnBulletEnter>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.AoeOnBulletEnter args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityAoeMonitorTriggerEvent.AoeOnBulletEnter);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_AoeOnBulletLeave: AEvent<Scene, AbilityTriggerEventType.AoeOnBulletLeave>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.AoeOnBulletLeave args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run(unit, AbilityAoeMonitorTriggerEvent.AoeOnBulletLeave);
				}
				await ETTask.CompletedTask;
			}
		}

	}
}
