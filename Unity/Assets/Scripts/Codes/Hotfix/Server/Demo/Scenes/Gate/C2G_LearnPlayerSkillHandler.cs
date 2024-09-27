using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_LearnPlayerSkillHandler : AMRpcHandler<C2G_LearnPlayerSkill, G2C_LearnPlayerSkill>
	{
		protected override async ETTask Run(Session session, C2G_LearnPlayerSkill request, G2C_LearnPlayerSkill response)
		{
			Scene scene = session.DomainScene();
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			string skillCfgId = request.SkillCfgId;
			PlayerSkillComponent playerSkillComponent = await ET.Server.PlayerCacheHelper.GetPlayerSkillByPlayerId(scene, playerId);

			playerSkillComponent.LearnSkill(skillCfgId);

			await PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.Skills, new() { "learnSkillCfgList", "usingSkillCfgList" }, PlayerModelChgType.PlayerSkill_LearnOrUpdate);

		}
	}
}