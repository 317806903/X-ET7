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

        public static bool ChkIsCollider(string itemCfgId)
        {
            if (ChkIsTower(itemCfgId) == false)
            {
                return false;
            }

            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (itemCfg.ItemSubType != ItemSubType.Collider)
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

        public static bool ChkIsSkill(string itemCfgId)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return false;
            }
            if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return false;
            }

            if (SkillCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                return false;
            }

            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(itemCfgId);
            if (itemCfg.ItemType != ItemType.Skill)
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

        public static int GetTowerItemLevel(string itemCfgId)
        {
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(itemCfgId);
            return towerCfg.Level[0];
        }

        public static int GetSkillItemLevel(string itemCfgId)
        {
            PlayerSkillCfg playerSkillCfg = PlayerSkillCfgCategory.Instance.Get(itemCfgId);
            return playerSkillCfg.Level;
        }

        public static string GetTowerItemPreTowerConfigId(string itemCfgId, int index = 1)
        {
            return TowerDefense_TowerCfgCategory.Instance.GetPreTowerCfgId(itemCfgId, index);
        }

        public static string GetTowerItemNextTowerConfigId(string itemCfgId, int index = 1)
        {
            return TowerDefense_TowerCfgCategory.Instance.GetNextTowerCfgId(itemCfgId, index);
        }

        public static List<(string title, string content)> GetAttributeProperty(string propertyType, int level)
        {
            List<(string, string)> attributesList = new ();
            UnitPropertyCfg unitPropertyCfg =
                    UnitPropertyCfgCategory.Instance.Get(propertyType, level);

            UIAttribute attribute = unitPropertyCfg.UIAttribute1;
            _AddAttribute(attribute, ref attributesList);
            attribute = unitPropertyCfg.UIAttribute2;
            _AddAttribute(attribute, ref attributesList);
            attribute = unitPropertyCfg.UIAttribute3;
            _AddAttribute(attribute, ref attributesList);

            return attributesList;
        }

        static void _AddAttribute(UIAttribute attribute, ref List<(string, string)> attributesList)
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

        public static List<string> GetTowerListInBattleDeck()
        {
            return TowerDefense_TowerCfgCategory.Instance.GetTowerCfgListInBattleDeck();
        }

        public static List<string> GetSkillListInBattleDeck()
        {
            return PlayerSkillCfgCategory.Instance.GetSkillCfgListInBattleDeck();
        }

        public static List<string> GetTowerListInBattleDeckWhenUnLockDefault()
        {
            return TowerDefense_TowerCfgCategory.Instance.GetTowerCfgListInBattleDeckWhenUnLockDefault();
        }

        public static List<string> GetSkillListInBattleDeckWhenUnLockDefault()
        {
            return PlayerSkillCfgCategory.Instance.GetSkillCfgListInBattleDeckWhenUnLockDefault();
        }

        public static string GetItemUnLockTip(string itemCfgId, bool isShowTip)
        {
            UnLockConditionBase unLockCondition;
            if (ItemHelper.ChkIsTower(itemCfgId))
            {
                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(itemCfgId);
                unLockCondition = towerCfg.UnLockCondition;
            }
            else if (ItemHelper.ChkIsSkill(itemCfgId))
            {
                PlayerSkillCfg playerSkillCfg = PlayerSkillCfgCategory.Instance.Get(itemCfgId);
                unLockCondition = playerSkillCfg.UnLockCondition;
            }
            else
            {
                return "not found";
            }

            string tip;
            string tipKey;
            if (unLockCondition is UnLockDefault)
            {
                if (isShowTip)
                {
                    tipKey = "TextCode_Key_ShowTip_UnLockDefault";
                }
                else
                {
                    tipKey = "TextCode_Key_ClickTip_UnLockDefault";
                }
                tip = LocalizeComponent.Instance.GetTextValue(tipKey);
                return tip;
            }
            else if (unLockCondition is UnLockByPVE unLockByPve)
            {
                ChallengeLevelCfg challengeLevelCfg = ET.AbilityConfig.TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByDropItemCfgId(itemCfgId);
                ChallengeLevelCfg challengeLevelCfgSeason = null;
                if (challengeLevelCfg == null)
                {
                    challengeLevelCfgSeason = ET.AbilityConfig.SeasonChallengeLevelCfgCategory.Instance.GetChallengeByDropItemCfgId(itemCfgId);
                }
                if (isShowTip)
                {
                    if (challengeLevelCfg != null)
                    {
                        tipKey = "TextCode_Key_ShowTip_UnLockByPVE";
                        tip = LocalizeComponent.Instance.GetTextValue(tipKey, challengeLevelCfg.Index);
                        return tip;
                    }
                    else if (challengeLevelCfgSeason != null)
                    {
                        SeasonInfoCfg seasonInfoCfg = SeasonInfoCfgCategory.Instance.Get(challengeLevelCfgSeason.SeasonId);
                        tipKey = "TextCode_Key_ShowTip_UnLockByPVESeason";
                        tip = LocalizeComponent.Instance.GetTextValue(tipKey, seasonInfoCfg.Name, challengeLevelCfgSeason.Index);
                        return tip;
                    }
                }
                else
                {
                    if (challengeLevelCfg != null)
                    {
                        tipKey = "TextCode_Key_ClickTip_UnLockByPVE";
                        tip = LocalizeComponent.Instance.GetTextValue(tipKey, challengeLevelCfg.Index);
                        return tip;
                    }
                    else if (challengeLevelCfgSeason != null)
                    {
                        SeasonInfoCfg seasonInfoCfg = SeasonInfoCfgCategory.Instance.Get(challengeLevelCfgSeason.SeasonId);
                        tipKey = "TextCode_Key_ClickTip_UnLockByPVESeason";
                        tip = LocalizeComponent.Instance.GetTextValue(tipKey, seasonInfoCfg.Name, challengeLevelCfgSeason.Index);
                        return tip;
                    }
                }
                return "not found";
            }
            else if (unLockCondition is UnLockByActivity unLockByActivity)
            {
                if (isShowTip)
                {
                    tipKey = "TextCode_Key_ShowTip_UnLockByActivity";
                }
                else
                {
                    tipKey = "TextCode_Key_ClickTip_UnLockByActivity";
                }
                tip = LocalizeComponent.Instance.GetTextValue(tipKey);
                return tip;
            }
            else if (unLockCondition is UnLockByDiamond unLockByDiamond)
            {
                if (isShowTip)
                {
                    tipKey = "TextCode_Key_ShowTip_UnLockByDiamond";
                }
                else
                {
                    tipKey = "TextCode_Key_ClickTip_UnLockByDiamond";
                }
                tip = LocalizeComponent.Instance.GetTextValue(tipKey, unLockByDiamond.DiamondValue);
                return tip;
            }
            else if (unLockCondition is UnLockByPay unLockByPay)
            {
                if (isShowTip)
                {
                    tipKey = "TextCode_Key_ShowTip_UnLockByPay";
                }
                else
                {
                    tipKey = "TextCode_Key_ClickTip_UnLockByPay";
                }
                tip = LocalizeComponent.Instance.GetTextValue(tipKey, unLockByPay.PayValue);
                return tip;
            }
            else if (unLockCondition is UnLockSoon unLockSoon)
            {
                if (isShowTip)
                {
                    tipKey = "TextCode_Key_ShowTip_UnLockSoon";
                }
                else
                {
                    tipKey = "TextCode_Key_ClickTip_UnLockSoon";
                }
                tip = LocalizeComponent.Instance.GetTextValue(tipKey);
                return tip;
            }
            else
            {
                return "not found";
            }

        }

    }
}