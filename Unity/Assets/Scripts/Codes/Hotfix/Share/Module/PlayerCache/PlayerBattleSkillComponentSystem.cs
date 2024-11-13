using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(PlayerBattleSkillComponent))]
    public static class PlayerBattleSkillComponentSystem
    {
        [ObjectSystem]
        public class PlayerBattleSkillComponentAwakeSystem : AwakeSystem<PlayerBattleSkillComponent>
        {
            protected override void Awake(PlayerBattleSkillComponent self)
            {
                self.skillCfgIdList = new();
            }
        }

        public static void Init(this PlayerBattleSkillComponent self)
        {
        }

        public static long GetPlayerId(this PlayerBattleSkillComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
        }

        public static void ReplaceBattleSkillItemCfgId(this PlayerBattleSkillComponent self, int index, string itemCfgId)
        {
            if (self.skillCfgIdList.Count < GlobalSettingCfgCategory.Instance.MaxBattleSkillNum)
            {
                self.skillCfgIdList.Add(itemCfgId);
            }
            else if (self.skillCfgIdList.Count - 1 >= index)
            {
                self.skillCfgIdList[index] = itemCfgId;
            }
            else
            {
                self.skillCfgIdList.Add(itemCfgId);
            }
        }

        public static List<string> GetBattleSkillItemCfgIdList(this PlayerBattleSkillComponent self)
        {
            return self.skillCfgIdList;
        }

        public static HashSet<string> GetBattleSkillItemCfgIdHashSet(this PlayerBattleSkillComponent self)
        {
            HashSetComponent<string> itemCfgIdHashSet = HashSetComponent<string>.Create();
            foreach (string itemCfgId in self.skillCfgIdList)
            {
                itemCfgIdHashSet.Add(itemCfgId);
            }
            return itemCfgIdHashSet;
        }

        public static bool SetBattleSkillItemCfgIdList(this PlayerBattleSkillComponent self, List<ItemComponent> itemList)
        {
            bool isNeedChg = false;
            if (self.skillCfgIdList.Count < GlobalSettingCfgCategory.Instance.MaxBattleSkillNum)
            {
                HashSet<string> itemCfgIdHashSet = self.GetBattleSkillItemCfgIdHashSet();
                foreach (ItemComponent itemComponent in itemList)
                {
                    if (ET.ItemHelper.ChkIsSkill(itemComponent.CfgId) == false)
                    {
                        continue;
                    }

                    if (itemCfgIdHashSet.Contains(itemComponent.CfgId))
                    {
                        continue;
                    }

                    if (self.skillCfgIdList.Count >= GlobalSettingCfgCategory.Instance.MaxBattleSkillNum)
                    {
                        continue;
                    }
                    self.skillCfgIdList.Add(itemComponent.CfgId);
                    isNeedChg = true;
                }
            }

            return isNeedChg;
        }
    }
}