using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_LearnSkillHandler : AMActorLocationRpcHandler<Unit, C2M_LearnSkill, M2C_LearnSkill>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_LearnSkill request, M2C_LearnSkill response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			string skillId = request.SkillId;
			(bool ret, string msg) = SkillHelper.LearnSkill(playerUnit, skillId, 1, SkillSlotType.NormalAttack);
			if (ret == false)
			{
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
			}
			await ETTask.CompletedTask;
		}
	}
}