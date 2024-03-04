using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ReScanHandler : AMActorLocationRpcHandler<Unit, C2M_ReScan, M2C_ReScan>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ReScan request, M2C_ReScan response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			if (gamePlayTowerDefenseComponent.ownerPlayerId != playerId)
			{
				response.Error = ErrorCode.ERR_LogicError;
				//response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Tip_NoWayToConnectHome");
				response.Message = "need ower";
				return;
			}
			await gamePlayTowerDefenseComponent.TransToWaitRescan();
			await ETTask.CompletedTask;
		}
	}
}