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

			long playerId = actionGameContext.playerId;
			if (playerId != 0)
			{
				GamePlayHelper.ChgGamePlayNumericValueByPlayerId(scene, playerId, actionGameCfgChgGamePlayNumeric.GameNumericType, actionGameCfgChgGamePlayNumeric.ChgValue, false);
			}

			TeamFlagType teamFlagType = actionGameContext.teamFlagType;
			if (teamFlagType != TeamFlagType.None)
			{
				GamePlayHelper.ChgGamePlayNumericValueByHomeTeamFlagType(scene, teamFlagType, actionGameCfgChgGamePlayNumeric.GameNumericType, actionGameCfgChgGamePlayNumeric.ChgValue, false);
			}

			await ETTask.CompletedTask;
		}
	}
}
