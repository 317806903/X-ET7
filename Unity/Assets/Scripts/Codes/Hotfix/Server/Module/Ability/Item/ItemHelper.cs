using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    public static class ItemHelper
    {
        public static async ETTask<(bool, string)> BuyItem(Scene scene, long playerId, string itemCfgId)
        {
            UnLockConditionBase unLockCondition;
            if (ET.ItemHelper.ChkIsTower(itemCfgId))
            {
                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(itemCfgId);
                unLockCondition = towerCfg.UnLockCondition;
            }
            else if (ET.ItemHelper.ChkIsSkill(itemCfgId))
            {
                PlayerSkillCfg playerSkillCfg = PlayerSkillCfgCategory.Instance.Get(itemCfgId);
                unLockCondition = playerSkillCfg.UnLockCondition;
            }
            else
            {
                return (false, "not found");
            }

            if (unLockCondition is UnLockDefault)
            {
                await ET.Server.PlayerCacheHelper.AddItem(scene, playerId, itemCfgId, 1);
                return (true, "");
            }
            else if (unLockCondition is UnLockByPVE unLockByPve)
            {
                await ET.Server.PlayerCacheHelper.AddItem(scene, playerId, itemCfgId, 1);
                return (true, "");
            }
            else if (unLockCondition is UnLockByActivity unLockByActivity)
            {
                await ET.Server.PlayerCacheHelper.AddItem(scene, playerId, itemCfgId, 1);
                return (true, "");
            }
            else if (unLockCondition is UnLockByDiamond unLockByDiamond)
            {
                int costDiamond = unLockByDiamond.DiamondValue;
                int curDiamond = await PlayerCacheHelper.GetTokenDiamondByPlayerId(scene, playerId);
                if (curDiamond < costDiamond)
                {
                    string message = $"Diamond不足[{curDiamond}] < {costDiamond}";
                    return (false, message);
                }

                await ET.Server.PlayerCacheHelper.AddItem(scene, playerId, itemCfgId, 1);

                await PlayerCacheHelper.ReduceTokenDiamond(scene, playerId, costDiamond);

                return (true, "");
            }
            else if (unLockCondition is UnLockByPay unLockByPay)
            {
                await ET.Server.PlayerCacheHelper.AddItem(scene, playerId, itemCfgId, 1);
                return (true, "");
            }
            else if (unLockCondition is UnLockSoon unLockSoon)
            {
                await ET.Server.PlayerCacheHelper.AddItem(scene, playerId, itemCfgId, 1);
                return (true, "");
            }
            else
            {
                return (false, "not found");
            }

        }
    }
}