using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_RestoreSkillEnergyHandler : AMActorLocationRpcHandler<Unit, C2M_RestoreSkillEnergy, M2C_RestoreSkillEnergy>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_RestoreSkillEnergy request, M2C_RestoreSkillEnergy response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			Scene scene = observerUnit.DomainScene();
			long unitId = request.unitId;
			Unit unit = Ability.UnitHelper.GetUnit(scene, unitId);

			long playerId = ET.GamePlayHelper.GetPlayerIdByUnitId(unit);
			if (playerId == -1)
			{
				Log.Error($"playerId == -1");
				return;
			}
			if (playerId != observerUnit.Id)
			{
				Log.Error($"playerId[{playerId}] != observerUnit.Id[{observerUnit.Id}]");
				return;
			}

			string skillCfgId = request.SkillCfgId;
			(bool ret, string msg) = await SkillHelper.RestoreSkillEnergy(unit, skillCfgId);
			if (ret == false)
			{
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			await ETTask.CompletedTask;
		}
	}
}