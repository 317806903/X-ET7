using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BattleRecoverCancelHandler : AMActorLocationRpcHandler<Unit, C2M_BattleRecoverCancel, M2C_BattleRecoverCancel>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_BattleRecoverCancel request, M2C_BattleRecoverCancel response)
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