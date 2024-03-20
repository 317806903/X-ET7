using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    public static class DropItemRuleHelper
    {
        public static Dictionary<string, int> Drop(string dropRule)
        {
            Dictionary<string, int> dropItems = new Dictionary<string, int>();
            if (DropRuleCfgCategory.Instance.Contain(dropRule) == false)
            {
                return dropItems;
            }

            DropRuleCfg cfg = DropRuleCfgCategory.Instance.Get(dropRule);
            if (cfg.DropRuleType == DropRuleType.DropOne)
            {
                dropItems = DropOne(dropRule);
            }
            else if (cfg.DropRuleType == DropRuleType.DropAll)
            {
                dropItems = DropAll(dropRule);
            }

            return dropItems;
        }

        public static Dictionary<string, int> DropOne(string dropRule)
        {
            Dictionary<string, int> items = new Dictionary<string, int>();
            DropRuleCfg cfg = DropRuleCfgCategory.Instance.Get(dropRule);
            List<DropItemBase> dropItemsCfg = cfg.DropItems;
            int totalWeight = 0;
            foreach (DropItemBase itemCfg in dropItemsCfg)
            {
                totalWeight += itemCfg.DropRatio;
            }

            int range = RandomGenerator.RandomNumber(0, totalWeight);
            foreach (DropItemBase itemCfg in dropItemsCfg)
            {
                if (range < itemCfg.DropRatio)
                {
                    items = DropItems(items, itemCfg);
                    break;
                }
                else
                {
                    range -= itemCfg.DropRatio;
                }
            }

            return items;
        }

        public static Dictionary<string, int> DropAll(string dropRule)
        {
            Dictionary<string, int> items = new Dictionary<string, int>();
            DropRuleCfg cfg = DropRuleCfgCategory.Instance.Get(dropRule);
            List<DropItemBase> dropItemsCfg = cfg.DropItems;
            foreach (DropItemBase itemCfg in dropItemsCfg)
            {
                int range = RandomGenerator.RandomNumber(0, 10000);
                if (range < itemCfg.DropRatio)
                {
                    items = DropItems(items, itemCfg);
                }
            }

            return items;
        }

        public static Dictionary<string, int> DropItems(Dictionary<string, int> totalItems, DropItemBase dropItemCfg)
        {
            if (dropItemCfg is DropItemOne dropItemOne)
            {
                return DropItemOne(totalItems, dropItemOne);
            }
            else if (dropItemCfg is DropRuleOne dropRuleOne)
            {
                return DropRuleOne(totalItems, dropRuleOne);
            }

            return totalItems;
        }

        public static Dictionary<string, int> DropItemOne(Dictionary<string, int> totalItems, DropItemOne dropItemOne)
        {
            if (totalItems.TryGetValue(dropItemOne.ItemId, out int count) == false)
            {
                totalItems[dropItemOne.ItemId] = dropItemOne.DropNum;
            }
            else
            {
                totalItems[dropItemOne.ItemId] += dropItemOne.DropNum;
            }

            return totalItems;
        }

        public static Dictionary<string, int> DropRuleOne(Dictionary<string, int> totalItems, DropRuleOne dropRuleOne)
        {
            Dictionary<string, int> innerDropItems = Drop(dropRuleOne.DropRuleId);
            foreach ((string itemID, int itemCnt) in innerDropItems)
            {
                if (totalItems.TryGetValue(itemID, out int count) == false)
                {
                    totalItems[itemID] = itemCnt;
                }
                else
                {
                    totalItems[itemID] += itemCnt;
                }
            }
            return totalItems;
        }

        public static List<string> GetPreviewDropItems(string dropRule){
            List<string> dropItems = new List<string>();
            if (DropRuleCfgCategory.Instance.Contain(dropRule) == false)
            {
                return dropItems;
            }

            DropRuleCfg cfg = DropRuleCfgCategory.Instance.Get(dropRule);
            List<DropItemBase> dropItemsCfg = cfg.DropItems;
            foreach (DropItemBase itemCfg in dropItemsCfg)
            {
                if (itemCfg is DropItemOne dropItemOne)
                {
                    dropItems.Add(dropItemOne.ItemId);
                }
                else if (itemCfg is DropRuleOne dropRuleOne)
                {
                    dropItems = GetPreviewDropItems(dropRuleOne.DropRuleId);
                }
            }

            return dropItems; 
        }
    }
}