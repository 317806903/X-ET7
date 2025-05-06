using System.Collections.Generic;
using ET.Ability;
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
			if (cameraPlayerUnit.IsDisposed)
			{
				return;
			}

			CameraPlayerUnitComponent cameraPlayerUnitComponent = cameraPlayerUnit.GetComponent<CameraPlayerUnitComponent>();

			for (int i = 0; i < itemList.Count; i++)
			{
				int skillIndex = i;
				ItemComponent itemComponent = itemList[i];
				string skillCfgId = itemComponent.CfgId;
				//
				// ET.AbilityConfig.SkillSlotType skillSlotType = SkillSlotType.ManualSkill;
				// (bool ret, string msg, SkillObj skillObj) = ET.Ability.SkillHelper.LearnSkill(cameraPlayerUnit, skillCfgId, 1, skillSlotType);

				// if (ret)
				{
					Unit skillCasterUnit = ET.GamePlayHelper.CreateSkillCasterUnit(playerId, cameraPlayerUnit);
					//skillObj.skillCasterUnit = skillCasterUnit;

					ET.AbilityConfig.SkillSlotType skillSlotType = SkillSlotType.ManualSkill;
					int level = 1;
					(bool ret, string msg, SkillObj skillObj) = ET.Ability.SkillHelper.LearnSkill(skillCasterUnit, skillCfgId, level, skillSlotType);

					ItemUpgradeComponent itemUpgradeComponent = skillCasterUnit.AddComponent<ItemUpgradeComponent>();
					itemUpgradeComponent.Init(playerId, skillCfgId, level);

					skillCasterUnit.AddCaster(cameraPlayerUnit);
					cameraPlayerUnit.AddOwnCaller(skillCasterUnit);

					cameraPlayerUnitComponent.skillIndex2PlayerSkillUnitId[skillIndex] = skillCasterUnit.Id;
				}
			}

			Ability.UnitHelper.AddSyncData_UnitComponent(cameraPlayerUnit, cameraPlayerUnitComponent.GetType());
		}

	}
}