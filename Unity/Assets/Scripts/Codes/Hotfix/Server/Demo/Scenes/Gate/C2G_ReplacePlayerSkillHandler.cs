using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ReplacePlayerSkillHandler : AMRpcHandler<C2G_ReplacePlayerSkill, G2C_ReplacePlayerSkill>
	{
		protected override async ETTask Run(Session session, C2G_ReplacePlayerSkill request, G2C_ReplacePlayerSkill response)
		{
			Scene scene = session.DomainScene();
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			string skillCfgId = request.SkillCfgId;
			int index = request.Index;
			PlayerSkillComponent playerSkillComponent = await ET.Server.PlayerCacheHelper.GetPlayerSkillByPlayerId(scene, playerId);

			playerSkillComponent.ReplaceUsingSkillCfgId(index, skillCfgId);

			await PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.Skills, new() { "usingSkillCfgList" }, PlayerModelChgType.PlayerSkill_Replace);

		}
	}
}