using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public static class EventHandler_CallActor
	{
		[Event(SceneType.Map)]
		public class EventHandler_CallActor2: AEvent<Scene, ET.Ability.AbilityTriggerEventType.CallActor>
		{
			protected override async ETTask Run(Scene scene, ET.Ability.AbilityTriggerEventType.CallActor args)
			{
				Unit unit = args.unit;
				Unit beCallUnit = args.beCallUnit;
				if (unit == null || beCallUnit == null)
				{
					return;
				}
				GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
				if (gamePlayComponent != null)
				{
					gamePlayComponent.DealUnitCallActor(unit, beCallUnit);
				}
				await ETTask.CompletedTask;
			}
		}
	}
}