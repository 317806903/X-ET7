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

		public static GamePlayPkComponent GetGamePlayPK(Scene scene)
		{
			return ET.GamePlayHelper.GetGamePlayPk(scene);
		}

		public static async ETTask LoadCameraPlayerSkillList(long playerId, Unit cameraPlayerUnit)
		{
			List<ItemComponent> itemList = await ET.Server.PlayerCacheHelper.GetBattleSkillItemListByPlayerId(cameraPlayerUnit.DomainScene(), playerId, true);
			foreach (var itemComponent in itemList)
			{
				ET.AbilityConfig.SkillSlotType skillSlotType = SkillSlotType.ManualSkill;
				ET.Ability.SkillHelper.LearnSkill(cameraPlayerUnit, itemComponent.CfgId, 1, skillSlotType);
			}

		}

	}
}