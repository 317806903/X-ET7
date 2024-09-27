using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public static class ItemHelper
    {
        public static string GetTokenDiamondCfgId()
        {
            return "Token_Diamond";
        }

        public static string GetTokenArcadeCoinCfgId()
        {
            return "Token_ArcadeCoin";
        }

        public static string GetAvatarFrameNoneCfgId()
        {
            return "AvatarFrame_None";
        }

        public static bool ChkIsToken(string itemCfgId)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return false;
            }
            if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return false;
            }

            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (itemCfg.ItemType != ItemType.Token)
            {
                return false;
            }

            return true;
        }

        public static bool ChkIsAvatarFrame(string itemCfgId)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return false;
            }
            if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return false;
            }

            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (itemCfg.ItemType != ItemType.AvatarFrame)
            {
                return false;
            }

            return true;
        }

        public static bool ChkIsTower(string itemCfgId)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return false;
            }
            if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return false;
            }

            if (TowerDefense_TowerCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return false;
            }

            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (itemCfg.ItemType != ItemType.Tower)
            {
                return false;
            }

            return true;
        }

        public static bool ChkIsAttackTower(string itemCfgId)
        {
            if (ChkIsTower(itemCfgId) == false)
            {
                return false;
            }

            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (itemCfg.ItemSubType != ItemSubType.AttackTower)
            {
                return false;
            }

            return true;
        }

        public static bool ChkIsTrap(string itemCfgId)
        {
            if (ChkIsTower(itemCfgId) == false)
            {
                return false;
            }

            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (itemCfg.ItemSubType != ItemSubType.Trap)
            {
                return false;
            }

            return true;
        }

        public static bool ChkIsCallMonster(string itemCfgId)
        {
            if (ChkIsTower(itemCfgId) == false)
            {
                return false;
            }

            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (itemCfg.ItemSubType != ItemSubType.CallMonster)
            {
                return false;
            }

            return true;
        }

        public static bool ChkIsMonster(string itemCfgId)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return false;
            }
            if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return false;
            }

            if (TowerDefense_MonsterCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return false;
            }

            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (itemCfg.ItemType != ItemType.Monster)
            {
                return false;
            }

            return true;
        }

        public static string GetItemName(string itemCfgId)
        {
            if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return "";
            }
            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            return itemCfg.Name;
        }

        public static string GetItemDesc(string itemCfgId)
        {
            if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return "";
            }
            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            return itemCfg.Desc;
        }

        public static string GetItemIcon(string itemCfgId)
        {
            if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return "";
            }
            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (string.IsNullOrEmpty(itemCfg.Icon))
            {
                return "";
            }
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(itemCfg.Icon);
            return resIconCfg.ResName;
        }

        public static bool ChkItemCanStack(string itemCfgId)
        {
            if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return false;
            }
            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            return itemCfg.CanStack;
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

        public static string GetTowerItemPreTowerConfigId(string itemCfgId, int index = 1)
        {
            return TowerDefense_TowerCfgCategory.Instance.GetPreTowerCfgId(itemCfgId, index);
        }

        public static string GetTowerItemNextTowerConfigId(string itemCfgId, int index = 1)
        {
            return TowerDefense_TowerCfgCategory.Instance.GetNextTowerCfgId(itemCfgId, index);
        }

        public static List<(string title, string content)> GetTowerAttribute(string itemCfgId, int level)
        {
            List<(string, string)> attributesList = new ();
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(itemCfgId);
            UnitPropertyCfg unitPropertyCfg =
                    UnitPropertyCfgCategory.Instance.Get(UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]).PropertyType, level);

            UIAttribute attribute = unitPropertyCfg.UIAttribute1;
            AddAttribute(attribute, ref attributesList);
            attribute = unitPropertyCfg.UIAttribute2;
            AddAttribute(attribute, ref attributesList);
            attribute = unitPropertyCfg.UIAttribute3;
            AddAttribute(attribute, ref attributesList);

            return attributesList;
        }

        static void AddAttribute(UIAttribute attribute, ref List<(string, string)> attributesList)
        {
            string title = attribute.Title;
            if (!string.IsNullOrEmpty(title))
            {
                string content = attribute.Content;
                List<float> replaceValue = attribute.ContentValue;
                for (int i = 0; i < replaceValue.Count; i++)
                {
                    content = content.Replace($"{{{i}}}", replaceValue[i].ToString());
                }
                attributesList.Add((title, content));
            }
        }
    }
}