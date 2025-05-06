using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(ItemManagerComponent))]
    public static class ItemManagerComponentSystem
    {
        [ObjectSystem]
        public class ItemManagerComponentAwakeSystem : AwakeSystem<ItemManagerComponent>
        {
            protected override void Awake(ItemManagerComponent self)
            {
                self.itemCfgId2ChildId = new();
                self.itemCfgId2ChildIdNoStack = new();
            }
        }

        [ObjectSystem]
        public class ItemManagerComponentDestroySystem: DestroySystem<ItemManagerComponent>
        {
            protected override void Destroy(ItemManagerComponent self)
            {
                self.itemCfgId2ChildId.Clear();
                self.itemCfgId2ChildIdNoStack.Clear();
            }
        }

        public static void Init(this ItemManagerComponent self)
        {
        }

        public static List<ItemComponent> GetItemList(this ItemManagerComponent self)
        {
            List<ItemComponent> itemList = ListComponent<ItemComponent>.Create();
            foreach (var child in self.Children)
            {
                long itemId = child.Key;
                ItemComponent itemComponent = self.GetChild<ItemComponent>(itemId);
                itemList.Add(itemComponent);
            }
            return itemList;
        }

        public static List<ItemComponent> GetItemList(this ItemManagerComponent self, ItemType itemType, ItemSubType itemSubType)
        {
            List<ItemComponent> itemList = ListComponent<ItemComponent>.Create();
            foreach (var child in self.Children)
            {
                long itemId = child.Key;
                ItemComponent itemComponent = self.GetChild<ItemComponent>(itemId);
                if (itemComponent.model.ItemType == itemType
                    && (itemSubType == ItemSubType.None || ET.ItemHelper.HasSameItemSubType(itemSubType, itemComponent.model.ItemSubType)))
                {
                    itemList.Add(itemComponent);
                }
            }
            return itemList;
        }

        public static HashSet<string> GetItemHashSet(this ItemManagerComponent self)
        {
            HashSet<string> itemHashSet = HashSetComponent<string>.Create();
            foreach (ItemComponent itemComponent in self.Children.Values)
            {
                itemHashSet.Add(itemComponent.CfgId);
            }
            return itemHashSet;
        }

        public static HashSet<string> GetItemHashSetByItemType(this ItemManagerComponent self, ItemType itemType, ItemSubType itemSubType)
        {
            HashSet<string> itemHashSet = HashSetComponent<string>.Create();
            foreach (ItemComponent itemComponent in self.Children.Values)
            {
                if (itemComponent.model.ItemType == itemType
                    && (itemSubType == ItemSubType.None
                        || ET.ItemHelper.HasSameItemSubType(itemSubType, itemComponent.model.ItemSubType)))
                {
                    itemHashSet.Add(itemComponent.CfgId);
                }
            }
            return itemHashSet;
        }

        public static ItemComponent GetItem(this ItemManagerComponent self, long itemId)
        {
            ItemComponent itemComponent = self.GetChild<ItemComponent>(itemId);
            return itemComponent;
        }

        public static ItemComponent GetItemWhenStack(this ItemManagerComponent self, string itemCfgId)
        {
            if (self.itemCfgId2ChildId.TryGetValue(itemCfgId, out long itemId))
            {
                ItemComponent itemComponent = self.GetChild<ItemComponent>(itemId);
                return itemComponent;
            }

            return null;
        }

        public static int GetItemLevelWhenStack(this ItemManagerComponent self, string itemCfgId)
        {
            if (self.itemCfgId2ChildId.TryGetValue(itemCfgId, out long itemId))
            {
                ItemComponent itemComponent = self.GetChild<ItemComponent>(itemId);
                if (ItemHelper.ChkIsTower(itemCfgId))
                {
                    ItemTowerComponent itemTowerComponent = itemComponent.GetComponent<ItemTowerComponent>();
                    return itemTowerComponent.level;
                }
                else if (ItemHelper.ChkIsSkill(itemCfgId))
                {
                    ItemSkillComponent itemSkillComponent = itemComponent.GetComponent<ItemSkillComponent>();
                    return itemSkillComponent.level;
                }
                return 1;
            }

            return 1;
        }

        public static List<ItemComponent> GetItemWhenNoStack(this ItemManagerComponent self, string itemCfgId)
        {
            if (self.itemCfgId2ChildIdNoStack.TryGetValue(itemCfgId, out var list))
            {
                List<ItemComponent> itemList = ListComponent<ItemComponent>.Create();
                foreach (var itemId in list)
                {
                    ItemComponent itemComponent = self.GetChild<ItemComponent>(itemId);
                    itemList.Add(itemComponent);
                }
                return itemList;
            }
            return null;
        }

        public static ItemComponent AddItem(this ItemManagerComponent self, string itemCfgId, int count)
        {
            if (count == 0)
            {
                return null;
            }

            if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
            {
                Log.Error($"ET.ItemManagerComponentSystem.AddItem ItemCfgCategory.Instance.Contain({itemCfgId}) == false");
                return null;
            }
            ItemComponent itemComponent = null;
            bool canStack = ET.ItemHelper.ChkItemCanStack(itemCfgId);
            if (canStack)
            {
                if (self.itemCfgId2ChildId.TryGetValue(itemCfgId, out long itemId))
                {
                    itemComponent = self.GetChild<ItemComponent>(itemId);
                    itemComponent.AddCount(count);
                    if (itemComponent.GetCount() <= 0)
                    {
                        itemComponent.Dispose();
                        self.itemCfgId2ChildId.Remove(itemCfgId);
                    }
                }
                else
                {
                    if (count <= 0)
                    {
                        return null;
                    }
                    itemComponent = self.AddChild<ItemComponent>();
                    itemComponent.Init(itemCfgId, count);
                    self.itemCfgId2ChildId[itemCfgId] = itemComponent.Id;
                    if (itemComponent.GetCount() <= 0)
                    {
                        itemComponent.Dispose();
                        self.itemCfgId2ChildId.Remove(itemCfgId);
                    }
                }
            }
            else
            {
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        itemComponent = self.AddChild<ItemComponent>();
                        itemComponent.Init(itemCfgId, count);
                        self.itemCfgId2ChildIdNoStack.Add(itemCfgId, itemComponent.Id);
                    }
                }
                else
                {
                    self.itemCfgId2ChildIdNoStack.TryGetValue(itemCfgId, out var list);
                    for (int i = 0; i < count; i++)
                    {
                        if (list.Count == 0)
                        {
                            break;
                        }
                        long itemId = list[0];
                        list.Remove(itemId);
                        self.itemCfgId2ChildIdNoStack.Remove(itemCfgId, itemId);
                    }
                }
            }
            return itemComponent;
        }

        public static void SetItem(this ItemManagerComponent self, string itemCfgId, int count)
        {
            if (count < 0)
            {
                //return;
                count = 0;
            }
            ItemComponent itemComponent = null;
            bool canStack = ET.ItemHelper.ChkItemCanStack(itemCfgId);
            if (canStack)
            {
                if (self.itemCfgId2ChildId.TryGetValue(itemCfgId, out long itemId))
                {
                    itemComponent = self.GetChild<ItemComponent>(itemId);
                    itemComponent.SetCount(count);
                    if (itemComponent.GetCount() <= 0)
                    {
                        itemComponent.Dispose();
                        self.itemCfgId2ChildId.Remove(itemCfgId);
                    }
                }
                else
                {
                    if (count <= 0)
                    {
                        return;
                    }
                    itemComponent = self.AddChild<ItemComponent>();
                    itemComponent.Init(itemCfgId, count);
                    self.itemCfgId2ChildId[itemCfgId] = itemComponent.Id;
                }
            }
            else
            {
                self.itemCfgId2ChildIdNoStack.TryGetValue(itemCfgId, out var list);
                if (count == list.Count)
                {
                    return;
                }
                else if (count > list.Count)
                {
                    for (int i = list.Count; i < count; i++)
                    {
                        itemComponent = self.AddChild<ItemComponent>();
                        itemComponent.Init(itemCfgId, count);
                        self.itemCfgId2ChildIdNoStack.Add(itemCfgId, itemComponent.Id);
                    }
                }
                else
                {
                    for (int i = list.Count - count; i > 0; i--)
                    {
                        long itemId = list[0];
                        list.Remove(itemId);
                        self.itemCfgId2ChildIdNoStack.Remove(itemCfgId, itemId);
                    }
                }
            }
        }

        public static void RemoveItem(this ItemManagerComponent self, string itemCfgId)
        {
            self.SetItem(itemCfgId, 0);
        }

        public static void RemoveItem(this ItemManagerComponent self, long itemId)
        {
            ItemComponent itemComponent = self.GetItem(itemId);
            if (itemComponent.ChkItemCanStack())
            {
                Log.Error($"itemComponent.ChkItemCanStack");
                return;
            }

            self.itemCfgId2ChildIdNoStack.Remove(itemComponent.CfgId, itemId);
            itemComponent.Dispose();
        }

        public static void ClearAllItem(this ItemManagerComponent self)
        {
            while (self.Children.Count > 0)
            {
                foreach (var child in self.Children)
                {
                    child.Value.Dispose();
                    break;
                }
            }

            self.itemCfgId2ChildId.Clear();
            self.itemCfgId2ChildIdNoStack.Clear();
        }

    }
}