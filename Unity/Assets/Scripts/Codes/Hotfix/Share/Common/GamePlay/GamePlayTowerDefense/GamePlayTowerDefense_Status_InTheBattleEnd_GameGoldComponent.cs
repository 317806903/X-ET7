using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[Event(SceneType.Map)]
	public class GamePlayTowerDefense_Status_InTheBattleEnd_GameGoldComponent: AEvent<Scene, ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleEnd>
	{
		protected override async ETTask Run(Scene scene, ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleEnd args)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
			if (gamePlayTowerDefenseComponent != null)
			{
				GameGoldComponent gameGoldComponent = gamePlayTowerDefenseComponent.GetComponent<GameGoldComponent>();
				if (gameGoldComponent.ChkIsNeedIncreaseCoinWhenWave())
				{
					gameGoldComponent.DoPlayerIncreaseCoinWhenWave();
				}
			}
			await ETTask.CompletedTask;
		}
	}
}