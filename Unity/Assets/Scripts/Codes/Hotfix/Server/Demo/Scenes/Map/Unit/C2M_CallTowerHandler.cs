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
			float3 forward = new float3(0, 0, 1);
			TeamFlagType teamFlagType = unit.GetComponent<TeamFlagObj>().GetTeamFlagType();
			Unit monsterUnit = ET.Ability.UnitHelper_Create.CreateWhenServer_Monster(unit.DomainScene(), towerUnitCfgId, teamFlagType, position, forward);
			await ETTask.CompletedTask;
		}
	}
}