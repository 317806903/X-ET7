﻿using System.Collections.Generic;
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

        public static void Init(this PlayerBattleCardComponent self)
        {
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

        public static HashSet<string> GetBattleCardItemCfgIdHashSet(this PlayerBattleCardComponent self)
        {
            HashSetComponent<string> itemCfgIdHashSet = HashSetComponent<string>.Create();
            foreach (string itemCfgId in self.itemCfgIdList)
            {
                itemCfgIdHashSet.Add(itemCfgId);
            }
            return itemCfgIdHashSet;
        }

        public static bool SetBattleCardItemCfgIdList(this PlayerBattleCardComponent self, List<ItemComponent> itemList)
        {
            bool isNeedChg = false;
            if (self.itemCfgIdList.Count < GlobalSettingCfgCategory.Instance.MaxBattleCardNum)
            {
                HashSet<string> itemCfgIdHashSet = self.GetBattleCardItemCfgIdHashSet();
                foreach (ItemComponent itemComponent in itemList)
                {
                    if (ET.ItemHelper.ChkIsTower(itemComponent.CfgId) == false)
                    {
                        continue;
                    }

                    if (itemCfgIdHashSet.Contains(itemComponent.CfgId))
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