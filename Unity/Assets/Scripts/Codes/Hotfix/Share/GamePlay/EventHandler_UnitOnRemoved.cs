using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public static class EventHandler_UnitOnRemoved
	{
		[Event(SceneType.Map)]
		public class EventHandler_DamageAfterOnKill: AEvent<Scene, ET.Ability.AbilityTriggerEventType.UnitOnRemoved>
		{
			protected override async ETTask Run(Scene scene, ET.Ability.AbilityTriggerEventType.UnitOnRemoved args)
			{
				Unit unit = args.unit;
				if (unit == null)
				{
					return;
				}
				GamePlayHelper.RemoveUnitInfo(unit);

				await ETTask.CompletedTask;
			}
		}
	}
}