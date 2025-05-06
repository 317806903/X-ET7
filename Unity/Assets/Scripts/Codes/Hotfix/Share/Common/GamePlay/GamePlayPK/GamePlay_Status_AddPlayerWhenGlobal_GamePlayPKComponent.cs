using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[Event(SceneType.Map)]
	public class GamePlay_Status_AddPlayerWhenGlobal_GamePlayPKComponent: AEvent<Scene, ET.Ability.AbilityTriggerEventType.GamePlay_Status_AddPlayerWhenGlobal>
	{
		protected override async ETTask Run(Scene scene, ET.Ability.AbilityTriggerEventType.GamePlay_Status_AddPlayerWhenGlobal args)
		{
			GamePlayPkComponent gamePlayPkComponent = GamePlayHelper.GetGamePlayPk(scene);
			if (gamePlayPkComponent != null)
			{
				long playerId = args.actionContext.playerId;
				foreach (var actionCfgGlobalBuffAddImmediately in gamePlayPkComponent.model.AddPlayerGlobalBuffAddList_Ref)
				{
					ET.Ability.ActionGameHandlerHelper.AddGameAction(scene, playerId, actionCfgGlobalBuffAddImmediately, null);
				}
			}
			await ETTask.CompletedTask;
		}
	}
}