using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(PlayerBackPackComponent))]
    public static class PlayerBackPackComponentSystem
    {
        [ObjectSystem]
        public class PlayerBackPackComponentAwakeSystem : AwakeSystem<PlayerBackPackComponent>
        {
            protected override void Awake(PlayerBackPackComponent self)
            {
                self.newItemList = new();
            }
        }

        public static void Init(this PlayerBackPackComponent self)
        {
            ItemManagerComponent itemManagerComponent = self.GetComponent<ItemManagerComponent>();
            if (itemManagerComponent == null)
            {
                itemManagerComponent = self.AddComponent<ItemManagerComponent>();
#if UNITY_EDITOR
                List<string> initialItemList;
                if (false)
                {
                    string tmp =
                        "Tow25_1|Tow12_3|Tow13_1|Tow13_2|Tow13_3|Tow14_1|Tow14_2|Tow14_3|Tow15_1|Tow15_2|Tow15_3|Tow16_1|Tow16_2|Tow16_3|Tow17_1|Tow17_2|Tow17_3|Tow18_1|Tow18_2|Tow18_3|Tow19_1|Tow19_2|Tow19_3|Tow20_1|Tow20_2|Tow20_3|Tow21_1|Tow21_2|Tow21_3|Tow22_1|Tow22_2|Tow22_3|Tow23_1|AvatarFrame_None|AvatarFrame_Season1_1|AvatarFrame_Season1_2|AvatarFrame_Season1_3|AvatarFrame_Season1_4|AvatarFrame_Season1_5|Token_Diamond|Token_ArcadeCoin";
                    initialItemList = new(tmp.Split("|"));
                }
                else
                {
                    if (ET.SceneHelper.ChkIsGameModeArcade())
                    {
                        initialItemList = GlobalSettingCfgCategory.Instance.GameModeArcadeInitialBackpackItem;
                    }
                    else if (ET.SceneHelper.ChkIsDemoShow())
                    {
                        initialItemList = GlobalSettingCfgCategory.Instance.DemoShowInitialBackpackItem;
                    }
                    else
                    {
                        initialItemList = GlobalSettingCfgCategory.Instance.InitialBackpackItem;
                    }
                }
#else
                List<string> initialItemList;
                if (ET.SceneHelper.ChkIsGameModeArcade())
                {
                    initialItemList = GlobalSettingCfgCategory.Instance.GameModeArcadeInitialBackpackItem;
                }
                else if (ET.SceneHelper.ChkIsDemoShow())
                {
                    initialItemList = GlobalSettingCfgCategory.Instance.DemoShowInitialBackpackItem;
                }
                else
                {
                    initialItemList = GlobalSettingCfgCategory.Instance.InitialBackpackItem;
                }
#endif
                foreach (var itemCfgId in initialItemList)
                {
                    if (string.IsNullOrEmpty(itemCfgId))
                    {
                        continue;
                    }
                    self.AddItem(itemCfgId, 1);
                }

                List<string> towerCfgList = ET.ItemHelper.GetTowerListInBattleDeckWhenUnLockDefault();
                foreach (var itemCfgId in towerCfgList)
                {
                    if (string.IsNullOrEmpty(itemCfgId))
                    {
                        continue;
                    }
                    self.AddItem(itemCfgId, 1);
                }

                List<string> skillCfgList = ET.ItemHelper.GetSkillListInBattleDeckWhenUnLockDefault();
                foreach (var itemCfgId in skillCfgList)
                {
                    if (string.IsNullOrEmpty(itemCfgId))
                    {
                        continue;
                    }
                    self.AddItem(itemCfgId, 1);
                }
            }
        }

        public static long GetPlayerId(this PlayerBackPackComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
        }

        public static ItemManagerComponent _GetItemManagerComponent(this PlayerBackPackComponent self)
        {
            ItemManagerComponent itemManagerComponent = self.GetComponent<ItemManagerComponent>();
            if (itemManagerComponent == null)
            {
                itemManagerComponent = self.AddComponent<ItemManagerComponent>();
            }
            return itemManagerComponent;
        }

        public static List<ItemComponent> GetItemList(this PlayerBackPackComponent self)
        {
            return self._GetItemManagerComponent().GetItemList();
        }

        public static List<ItemComponent> GetItemListByItemType(this PlayerBackPackComponent self, ItemType itemType, ItemSubType itemSubType)
        {
            return self._GetItemManagerComponent().GetItemList(itemType, itemSubType);
        }

        public static HashSet<string> GetItemHashSet(this PlayerBackPackComponent self)
        {
            return self._GetItemManagerComponent().GetItemHashSet();
        }

        public static HashSet<string> GetItemHashSetByItemType(this PlayerBackPackComponent self, ItemType itemType, ItemSubType itemSubType)
        {
            return self._GetItemManagerComponent().GetItemHashSetByItemType(itemType, itemSubType);
        }

        public static ItemComponent GetItem(this PlayerBackPackComponent self, long itemId)
        {
            return self._GetItemManagerComponent().GetItem(itemId);
        }

        public static ItemComponent GetItemWhenStack(this PlayerBackPackComponent self, string itemCfgId)
        {
            return self._GetItemManagerComponent().GetItemWhenStack(itemCfgId);
        }

        public static List<ItemComponent> GetItemWhenNoStack(this PlayerBackPackComponent self, string itemCfgId)
        {
            return self._GetItemManagerComponent().GetItemWhenNoStack(itemCfgId);
        }

        public static void AddItem(this PlayerBackPackComponent self, string itemCfgId, int count)
        {
            self.AddNewItemRecord(itemCfgId);

            self._GetItemManagerComponent().AddItem(itemCfgId, count);
        }

        public static void SetItem(this PlayerBackPackComponent self, string itemCfgId, int count)
        {
            self._GetItemManagerComponent().SetItem(itemCfgId, count);
        }

        public static void RemoveItem(this PlayerBackPackComponent self, string itemCfgId)
        {
            self._GetItemManagerComponent().RemoveItem(itemCfgId);
        }

        public static void ClearAllItem(this PlayerBackPackComponent self)
        {
            self._GetItemManagerComponent().ClearAllItem();
        }

        public static bool _ChkItemExist(this PlayerBackPackComponent self, string itemCfgId)
        {
            bool canStack = ET.ItemHelper.ChkItemCanStack(itemCfgId);
            if (canStack)
            {
                ItemComponent itemComponent = self.GetItemWhenStack(itemCfgId);
                if (itemComponent != null)
                {
                    return true;
                }
            }
            else
            {
                List<ItemComponent> list = self.GetItemWhenNoStack(itemCfgId);
                if (list != null && list.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static void AddNewItemRecord(this PlayerBackPackComponent self, string itemCfgId)
        {
            if (self._ChkItemExist(itemCfgId) == false)
            {
                if (ET.ItemHelper.GetAvatarFrameNoneCfgId() == itemCfgId)
                {
                    return;
                }
                self.newItemList.Add(itemCfgId);
            }
        }

        public static void RemoveNewItemRecord(this PlayerBackPackComponent self, string itemCfgId)
        {
            self.newItemList.Remove(itemCfgId);
        }

        public static bool ChkIsNewItem(this PlayerBackPackComponent self, string itemCfgId)
        {
            return self.newItemList.Contains(itemCfgId);
        }

        public static bool ChkIsNewTower(this PlayerBackPackComponent self)
        {
            foreach (string itemCfgId in self.newItemList)
            {
                if (ItemHelper.ChkIsTower(itemCfgId))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ChkIsNewSkill(this PlayerBackPackComponent self)
        {
            foreach (string itemCfgId in self.newItemList)
            {
                if (ItemHelper.ChkIsSkill(itemCfgId))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ChkIsNewAvatarFrame(this PlayerBackPackComponent self)
        {
            foreach (string itemCfgId in self.newItemList)
            {
                if (ET.ItemHelper.GetAvatarFrameNoneCfgId() == itemCfgId)
                {
                    continue;
                }
                if (ItemHelper.ChkIsAvatarFrame(itemCfgId))
                {
                    return true;
                }
            }

            return false;
        }

    }
}