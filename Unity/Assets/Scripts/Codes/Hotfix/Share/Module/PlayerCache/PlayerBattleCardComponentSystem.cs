using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(PlayerBattleCardComponent))]
    public static class PlayerBattleCardComponentSystem
    {
        [ObjectSystem]
        public class PlayerBattleCardComponentAwakeSystem : AwakeSystem<PlayerBattleCardComponent>
        {
            protected override void Awake(PlayerBattleCardComponent self)
            {
                self.itemCfgIdList = new();
            }
        }

        public static long GetPlayerId(this PlayerBattleCardComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
        }

        public static void ReplaceBattleCardItemCfgId(this PlayerBattleCardComponent self, int index, string itemCfgId)
        {
            if (self.itemCfgIdList.Count < GlobalSettingCfgCategory.Instance.MaxBattleCardNum)
            {
                self.itemCfgIdList.Add(itemCfgId);
            }
            else if (self.itemCfgIdList.Count - 1 >= index)
            {
                self.itemCfgIdList[index] = itemCfgId;
            }
            else
            {
                self.itemCfgIdList.Add(itemCfgId);
            }
        }

        public static List<string> GetBattleCardItemCfgIdList(this PlayerBattleCardComponent self)
        {
            return self.itemCfgIdList;
        }

        public static bool SetBattleCardItemCfgIdList(this PlayerBattleCardComponent self, List<ItemComponent> itemList)
        {
            bool isNeedChg = false;
            if (self.itemCfgIdList.Count < GlobalSettingCfgCategory.Instance.MaxBattleCardNum)
            {
                foreach (ItemComponent itemComponent in itemList)
                {
                    if (ET.ItemHelper.ChkIsTower(itemComponent.CfgId) == false)
                    {
                        continue;
                    }

                    if (self.itemCfgIdList.Contains(itemComponent.CfgId))
                    {
                        continue;
                    }

                    if (self.itemCfgIdList.Count >= GlobalSettingCfgCategory.Instance.MaxBattleCardNum)
                    {
                        continue;
                    }
                    self.itemCfgIdList.Add(itemComponent.CfgId);
                    isNeedChg = true;
                }
            }

            return isNeedChg;
        }
    }
}