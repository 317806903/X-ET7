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
			ET.GamePlayPKHelper.CreateMonster(unit.DomainScene(), tankUnitCfgId, 1, position, forward);
			
			await ETTask.CompletedTask;
		}
	}
}