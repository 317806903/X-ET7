using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_LearnSkillHandler : AMActorLocationRpcHandler<Unit, C2M_LearnSkill, M2C_LearnSkill>
	{
		protected override async ETTask Run(Unit unit, C2M_LearnSkill request, M2C_LearnSkill response)
		{
			long unitId = request.UnitId;
			string skillId = request.SkillId;
			SkillHelper.LearnSkill(unit, skillId, 1, SkillSlotType.NormalAttack);
			await ETTask.CompletedTask;
		}
	}
}