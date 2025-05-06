using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BattleRecoverConfirmWatchAdHandler : AMActorLocationRpcHandler<Unit, C2M_BattleRecoverConfirmWatchAd, M2C_BattleRecoverConfirmWatchAd>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_BattleRecoverConfirmWatchAd request, M2C_BattleRecoverConfirmWatchAd response)
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
				if (isFinished)
				{
					gameRecoverOnceComponent.Dispose();
				}
			}
			if (isFinished)
			{
				if (gamePlayTowerDefenseComponent.ChkIsGameRecovering() == false)
				{
					// response.Error = ErrorCode.ERR_LogicError;
					// response.Message = "Game Is Not in Recover";
					return;
				}

				await gamePlayTowerDefenseComponent.TransToRecoverSucess();
			}
			else
			{
				if (gamePlayTowerDefenseComponent.ChkIsGameRecover() == false)
				{
					// response.Error = ErrorCode.ERR_LogicError;
					// response.Message = "Game Is Not in Recover";
					return;
				}

				await gamePlayTowerDefenseComponent.TransToRecovering();
			}


			await ETTask.CompletedTask;
		}
	}
}