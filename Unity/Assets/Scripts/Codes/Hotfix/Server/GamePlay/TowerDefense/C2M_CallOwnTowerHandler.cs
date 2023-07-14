using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CallOwnTowerHandler : AMActorLocationRpcHandler<Unit, C2M_CallOwnTower, M2C_CallOwnTower>
	{
		protected override async ETTask Run(Unit unit, C2M_CallOwnTower request, M2C_CallOwnTower response)
		{
			string towerUnitCfgId = request.TowerUnitCfgId;
			float3 position = request.Position;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
			gamePlayTowerDefenseComponent.CallPlayerTower(unit, towerUnitCfgId, position);

			await ETTask.CompletedTask;
		}
	}
}