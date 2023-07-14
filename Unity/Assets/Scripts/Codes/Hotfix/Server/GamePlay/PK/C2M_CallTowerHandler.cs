using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CallTowerHandler : AMActorLocationRpcHandler<Unit, C2M_CallTower, M2C_CallTower>
	{
		protected override async ETTask Run(Unit unit, C2M_CallTower request, M2C_CallTower response)
		{
			string towerUnitCfgId = request.TowerUnitCfgId;
			float3 position = request.Position;
			
			ET.GamePlayPKHelper.CreateTower(unit.DomainScene(), unit.Id, towerUnitCfgId, position);
			await ETTask.CompletedTask;
		}
	}
}