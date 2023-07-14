using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CastSkillHandler : AMActorLocationRpcHandler<Unit, C2M_CastSkill, M2C_CastSkill>
	{
		protected override async ETTask Run(Unit unit, C2M_CastSkill request, M2C_CastSkill response)
		{
			long unitId = request.UnitId;
			string skillId = request.SkillId;
			(bool ret, string msg) = SkillHelper.CastSkill(unit, skillId);
			if (ret == false)
			{
				//response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
			}
			await ETTask.CompletedTask;
		}
	}
}