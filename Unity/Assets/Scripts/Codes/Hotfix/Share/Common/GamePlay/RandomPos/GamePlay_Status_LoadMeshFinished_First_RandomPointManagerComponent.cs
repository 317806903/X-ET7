using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[Event(SceneType.Map)]
	public class GamePlay_Status_LoadMeshFinished_First_RandomPointManagerComponent: AEvent<Scene, ET.Ability.AbilityTriggerEventType.GamePlay_Status_LoadMeshFinished_First>
	{
		protected override async ETTask Run(Scene scene, ET.Ability.AbilityTriggerEventType.GamePlay_Status_LoadMeshFinished_First args)
		{
			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
			if (gamePlayComponent != null)
			{
				RandomPointManagerComponent randomPointManagerComponent = gamePlayComponent.GetComponent<RandomPointManagerComponent>();
				if (randomPointManagerComponent != null)
				{
					randomPointManagerComponent.AddComponent<MapRandomPointComponent>();
				}
			}
			await ETTask.CompletedTask;
		}
	}
}