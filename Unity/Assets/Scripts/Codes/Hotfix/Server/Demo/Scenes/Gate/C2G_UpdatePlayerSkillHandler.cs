using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_UpdatePlayerSkillHandler : AMRpcHandler<C2G_UpdatePlayerSkill, G2C_UpdatePlayerSkill>
	{
		protected override async ETTask Run(Session session, C2G_UpdatePlayerSkill request, G2C_UpdatePlayerSkill response)
		{
			Scene scene = session.DomainScene();
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			string skillCfgId = request.SkillCfgId;
			PlayerSkillComponent playerSkillComponent = await ET.Server.PlayerCacheHelper.GetPlayerSkillByPlayerId(scene, playerId);

			playerSkillComponent.UpdateSkill(skillCfgId);

			await PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.Skills, new() { "learnSkillCfgList", "usingSkillCfgList" }, PlayerModelChgType.PlayerSkill_LearnOrUpdate);

		}
	}
}