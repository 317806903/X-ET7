using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
	public class ActionGame_ChgGamePlayNumeric: IActionGameHandler
	{
		public override async ETTask Run(Scene scene, string actionId, float delayTime, ActionGameContext actionGameContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
				if (scene.IsDisposed)
				{
					return;
				}
			}

			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
			if (gamePlayComponent == null)
			{
				return;
			}
			ActionGameCfg_ChgGamePlayNumeric actionGameCfgChgGamePlayNumeric = ActionGameCfg_ChgGamePlayNumericCategory.Instance.Get(actionId);

			bool needPublish = false;
			long playerId = actionGameContext.playerId;
			if (playerId != (long)ET.PlayerId.PlayerNone)
			{
				GamePlayHelper.ChgGamePlayNumericValueByPlayerId(scene, playerId, actionGameCfgChgGamePlayNumeric.GameNumericType, actionGameCfgChgGamePlayNumeric.ChgValue, false);
				needPublish = true;
			}

			TeamFlagType teamFlagType = actionGameContext.teamFlagType;
			if (teamFlagType != TeamFlagType.None)
			{
				GamePlayHelper.ChgGamePlayNumericValueByHomeTeamFlagType(scene, teamFlagType, actionGameCfgChgGamePlayNumeric.GameNumericType, actionGameCfgChgGamePlayNumeric.ChgValue, false);
				needPublish = true;
			}

			if (needPublish)
			{
				EventSystem.Instance.Publish(scene, new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_ChgGamePlayNumeric()
				{
					actionGameContext = actionGameContext,
					gameNumericType = actionGameCfgChgGamePlayNumeric.GameNumericType,
				});
			}
			await ETTask.CompletedTask;
		}
	}
}
