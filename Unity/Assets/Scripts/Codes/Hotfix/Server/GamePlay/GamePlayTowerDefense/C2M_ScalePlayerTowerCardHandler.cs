using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ScalePlayerTowerCardHandler : AMActorLocationRpcHandler<Unit, C2M_ScalePlayerTowerCard, M2C_ScalePlayerTowerCard>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ScalePlayerTowerCard request, M2C_ScalePlayerTowerCard response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			string towerCfgId = request.TowerCfgId;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			bool success = gamePlayTowerDefenseComponent.ScalePlayerTowerCard(playerId, towerCfgId);
			if (success == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "ScalePlayerTowerCard Err";
			}
			await ETTask.CompletedTask;
		}
	}
}