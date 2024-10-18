using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof(GamePlayTowerDefenseComponent))]
    public static class GamePlayHelper
	{
		public static GamePlayComponent GetGamePlay(Scene scene)
		{
			return ET.GamePlayHelper.GetGamePlay(scene);
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(Scene scene)
		{
			return ET.GamePlayHelper.GetGamePlayTowerDefense(scene);
		}

		public static async ETTask LoadCameraPlayerSkillList(long playerId, Unit cameraPlayerUnit)
		{
			PlayerSkillComponent playerSkillComponent = await PlayerCacheHelper.GetPlayerSkillByPlayerId(cameraPlayerUnit.DomainScene(), playerId);
			List<string> skillCfgIdList = playerSkillComponent.GetUsingSkillCfgList();
			foreach (string skillCfgId in skillCfgIdList)
			{
				ET.AbilityConfig.SkillSlotType skillSlotType = SkillSlotType.ManualSkill;
				ET.Ability.SkillHelper.LearnSkill(cameraPlayerUnit, skillCfgId, 1, skillSlotType);
			}
		}

	}
}