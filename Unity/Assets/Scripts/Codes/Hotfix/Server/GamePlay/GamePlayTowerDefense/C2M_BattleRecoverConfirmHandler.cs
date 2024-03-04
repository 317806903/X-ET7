using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BattleRecoverConfirmHandler : AMActorLocationRpcHandler<Unit, C2M_BattleRecoverConfirm, M2C_BattleRecoverConfirm>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_BattleRecoverConfirm request, M2C_BattleRecoverConfirm response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			bool isFinished = request.IsFinished == 1?true:false;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			if (isFinished)
			{
				if (gamePlayTowerDefenseComponent.ChkIsGameRecovering() == false)
				{
					// response.Error = ErrorCode.ERR_LogicError;
					// response.Message = "Game Is Not in Recover";
					return;
				}

				gamePlayTowerDefenseComponent.DealRecover();
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