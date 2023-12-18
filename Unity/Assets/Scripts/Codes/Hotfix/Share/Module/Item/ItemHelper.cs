using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public static class ItemHelper
    {
        public static string GetItemName(string itemCfgId)
        {
            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            return itemCfg.Name;
        }

        public static string GetItemDesc(string itemCfgId)
        {
            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            return itemCfg.Desc;
        }

        public static string GetItemIcon(string itemCfgId)
        {
            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (string.IsNullOrEmpty(itemCfg.Icon))
            {
                return "";
            }
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(itemCfg.Icon);
            return resIconCfg.ResName;
        }

        public static QualityType GetItemQualityType(string itemCfgId)
        {
            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            return itemCfg.QualityType;
        }

        public static QualityRank GetTowerItemQualityRank(string itemCfgId)
        {
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(itemCfgId);
            return towerCfg.QualityRank;
        }

        public static List<string> GetTowerItemLabels(string itemCfgId)
        {
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(itemCfgId);
            return towerCfg.Labels;
        }

    }
}