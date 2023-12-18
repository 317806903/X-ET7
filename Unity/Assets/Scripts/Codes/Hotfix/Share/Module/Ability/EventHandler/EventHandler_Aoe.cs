namespace ET.Ability
{
	public static class EventHandler_Aoe
	{
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Aoe(unit, AbilityAoeMonitorTriggerEvent.AoeOnCreate);
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
					EventHandlerHelper.Run_Aoe(unit, AbilityAoeMonitorTriggerEvent.AoeOnRemoved);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_AoeOnEnter: AEvent<Scene, AbilityTriggerEventType.AoeOnEnter>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.AoeOnEnter args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Aoe(unit, AbilityAoeMonitorTriggerEvent.AoeOnEnter);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_AoeOnExist: AEvent<Scene, AbilityTriggerEventType.AoeOnExist>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.AoeOnExist args)
			{
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Aoe(unit, AbilityAoeMonitorTriggerEvent.AoeOnExist);
				}
				await ETTask.CompletedTask;
			}
		}
	}
}
