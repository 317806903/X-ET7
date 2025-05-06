using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[Event(SceneType.Map)]
	public class GamePlayTowerDefense_Status_ShowStartEffectBegin_RandomPointManagerComponent: AEvent<Scene, ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectBegin>
	{
		protected override async ETTask Run(Scene scene, ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectBegin args)
		{
			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
			if (gamePlayComponent != null)
			{
				RandomPointManagerComponent randomPointManagerComponent = gamePlayComponent.GetComponent<RandomPointManagerComponent>();
				if (randomPointManagerComponent != null)
				{
					randomPointManagerComponent.AddComponent<PathLineRandomPointComponent>();
					randomPointManagerComponent.AddComponent<CenterPointComponent>();
				}
			}
			await ETTask.CompletedTask;
		}
	}
}