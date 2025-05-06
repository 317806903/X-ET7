using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[Event(SceneType.Map)]
	public class EventHandler_GamePlayTowerDefense_AddRestoreEnergy: AEvent<Scene, ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_AddRestoreEnergy>
	{
		protected override async ETTask Run(Scene scene, ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_AddRestoreEnergy args)
		{
			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
			if (gamePlayComponent != null)
			{
				RestoreEnergyComponent restoreEnergyComponent = gamePlayComponent.GetComponent<RestoreEnergyComponent>();
				if (restoreEnergyComponent == null)
				{
					return;
				}
				if (args.skillComponent != null)
				{
					restoreEnergyComponent.Add(args.skillComponent);
				}
				if (args.skillObj != null)
				{
					restoreEnergyComponent.Add(args.skillObj);
				}
			}
			await ETTask.CompletedTask;
		}
	}
}