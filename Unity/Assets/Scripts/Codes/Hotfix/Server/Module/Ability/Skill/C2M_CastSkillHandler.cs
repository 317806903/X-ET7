using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CastSkillHandler : AMActorLocationRpcHandler<Unit, C2M_CastSkill, M2C_CastSkill>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_CastSkill request, M2C_CastSkill response)
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

			Unit cameraPlayerUnit = ET.GamePlayHelper.GetCameraPlayerUnit(observerUnit);
			if (cameraPlayerUnit == unit)
			{
				float3 cameraPosition = request.CameraPosition;
				float3 cameraDirect = request.CameraDirect;
				Ability.UnitHelper.ResetPos(unit, cameraPosition, cameraDirect);
			}

			string skillCfgId = request.SkillCfgId;
			SelectHandle selectHandleIn = null;
			if (request.SelectHandleBytes != null)
			{
				selectHandleIn = MongoHelper.Deserialize<SelectHandle>(request.SelectHandleBytes);
			}
			//Log.Error($"---zpb selectHandle={selectHandleIn.ToString()}");
			(bool ret, string msg) = await SkillHelper.CastSkill(unit, skillCfgId, selectHandleIn);
			if (ret == false)
			{
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
			}
			await ETTask.CompletedTask;
		}
	}
}