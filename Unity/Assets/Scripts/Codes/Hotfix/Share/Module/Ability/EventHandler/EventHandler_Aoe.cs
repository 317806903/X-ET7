namespace ET.Ability
{
	public static class EventHandler_Aoe
	{
		[Event(SceneType.Map)]
		public class EventHandler_UnitOnCreate: AEvent<Scene, AbilityTriggerEventType.UnitOnCreate>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnCreate args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Aoe(unit, AbilityConfig.AoeTriggerEvent.AoeOnCreate, ref actionContext);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_UnitOnRemoved: AEvent<Scene, AbilityTriggerEventType.UnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.UnitOnRemoved args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Aoe(unit, AbilityConfig.AoeTriggerEvent.AoeOnRemoved, ref actionContext);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_AoeOnEnter: AEvent<Scene, AbilityTriggerEventType.AoeOnEnter>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.AoeOnEnter args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Aoe(unit, AbilityConfig.AoeTriggerEvent.AoeOnEnter, ref actionContext);
				}
				await ETTask.CompletedTask;
			}
		}

		[Event(SceneType.Map)]
		public class EventHandler_AoeOnExist: AEvent<Scene, AbilityTriggerEventType.AoeOnExist>
		{
			protected override async ETTask Run(Scene scene, AbilityTriggerEventType.AoeOnExist args)
			{
				ActionContext actionContext = args.actionContext;
				if (UnitHelper.ChkUnitAlive(args.unit) && UnitHelper.ChkIsAoe(args.unit))
				{
					Unit unit = args.unit;
					EventHandlerHelper.Run_Aoe(unit, AbilityConfig.AoeTriggerEvent.AoeOnExist, ref actionContext);
				}
				await ETTask.CompletedTask;
			}
		}
	}
}
