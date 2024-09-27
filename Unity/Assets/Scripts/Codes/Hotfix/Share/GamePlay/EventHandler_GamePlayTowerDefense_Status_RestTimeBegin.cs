using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[Event(SceneType.Map)]
	public class EventHandler_GamePlayTowerDefense_Status_RestTimeBegin: AEvent<Scene, ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeBegin>
	{
		protected override async ETTask Run(Scene scene, ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeBegin args)
		{
			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
			if (gamePlayComponent != null)
			{
				RestoreEnergyComponent restoreEnergyComponent = gamePlayComponent.GetComponent<RestoreEnergyComponent>();
				restoreEnergyComponent.DealCurEnergyNumByWave();
				restoreEnergyComponent.DealCurCommonEnergyNumByWave();
			}
			await ETTask.CompletedTask;
		}
	}
}