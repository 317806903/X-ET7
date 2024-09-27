using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BattleRecoverCancelWatchAdHandler : AMActorLocationRpcHandler<Unit, C2M_BattleRecoverCancelWatchAd, M2C_BattleRecoverCancelWatchAd>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_BattleRecoverCancelWatchAd request, M2C_BattleRecoverCancelWatchAd response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			bool isFinished = request.IsFinished == 1?true:false;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());

			GameRecoverOnceComponent gameRecoverOnceComponent = gamePlayTowerDefenseComponent.GetComponent<GameRecoverOnceComponent>();
			if (gameRecoverOnceComponent == null)
			{
				return;
			}
			else
			{
				gameRecoverOnceComponent.Dispose();
			}
			if (isFinished)
			{
				if (gamePlayTowerDefenseComponent.ChkIsGameRecovering() == false)
				{
					// response.Error = ErrorCode.ERR_LogicError;
					// response.Message = "Game Is Not in Recover";
					return;
				}

				await gamePlayTowerDefenseComponent.TransToGameEnd();
			}
			else
			{
				if (gamePlayTowerDefenseComponent.ChkIsGameRecover() == false)
				{
					// response.Error = ErrorCode.ERR_LogicError;
					// response.Message = "Game Is Not in Recover";
					return;
				}

				await gamePlayTowerDefenseComponent.TransToGameEnd();
			}

			await ETTask.CompletedTask;
		}
	}
}