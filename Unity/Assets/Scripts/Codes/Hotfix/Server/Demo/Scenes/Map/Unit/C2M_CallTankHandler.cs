using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CallTankHandler : AMActorLocationRpcHandler<Unit, C2M_CallTank, M2C_CallTank>
	{
		protected override async ETTask Run(Unit unit, C2M_CallTank request, M2C_CallTank response)
		{
			string tankUnitCfgId = request.TankUnitCfgId;
			float3 position = request.Position;
			float3 forward = new float3(0, 0, 1);
			Unit monsterUnit = ET.Ability.UnitHelper_Create.CreateWhenServer_Monster(unit.DomainScene(), tankUnitCfgId, TeamFlagType.Monster, position, forward);
			await ETTask.CompletedTask;
		}
	}
}