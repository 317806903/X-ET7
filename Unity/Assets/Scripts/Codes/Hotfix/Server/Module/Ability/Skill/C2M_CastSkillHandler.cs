using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CastSkillHandler : AMActorLocationRpcHandler<Unit, C2M_CastSkill, M2C_CastSkill>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_CastSkill request, M2C_CastSkill response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			string skillId = request.SkillId;
			(bool ret, string msg) = await SkillHelper.CastSkill(playerUnit, skillId);
			if (ret == false)
			{
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
			}
			await ETTask.CompletedTask;
		}
	}
}