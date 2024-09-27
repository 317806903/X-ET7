using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BattleRecoverResultHandler : AMActorLocationRpcHandler<Unit, C2M_BattleRecoverResult, M2C_BattleRecoverResult>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_BattleRecoverResult request, M2C_BattleRecoverResult response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			PlayerRecoverStatus playerRecoverStatus = request.IsConfirm == 1?PlayerRecoverStatus.Confirm:PlayerRecoverStatus.Cancel;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			GameRecoverOnceComponent gameRecoverOnceComponent = gamePlayTowerDefenseComponent.GetComponent<GameRecoverOnceComponent>();
			if (gameRecoverOnceComponent == null)
			{
				return;
			}
			if (playerRecoverStatus == PlayerRecoverStatus.Confirm)
			{
				bool bRet = await gamePlayTowerDefenseComponent.ChkPlayerConfirmRecover(playerId);
				if (bRet == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = $"ChkPlayerConfirmRecover[{playerId}] == false";

					gamePlayTowerDefenseComponent.NoticeToClient(playerId);
					return;
				}
			}
			gameRecoverOnceComponent.SetPlayerRecoverStatus(playerId, playerRecoverStatus);
			if (playerRecoverStatus == PlayerRecoverStatus.Confirm)
			{
				await gamePlayTowerDefenseComponent.DealPlayerConfirmRecover(playerId);
			}
			gamePlayTowerDefenseComponent.NoticeToClientAll();

			await ETTask.CompletedTask;
		}
	}
}