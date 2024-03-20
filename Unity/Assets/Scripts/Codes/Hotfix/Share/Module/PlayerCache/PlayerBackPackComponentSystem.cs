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
                self.AddComponent<ItemManagerComponent>();
                List<string> initialItemList = GlobalSettingCfgCategory.Instance.InitialBackpackItem;
                foreach (var itemCfgId in initialItemList)
                {
                    self.AddItem(itemCfgId, 1);
                }
            }
        }

        public static long GetPlayerId(this PlayerBackPackComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
        }

        public static List<ItemComponent> GetItemList(this PlayerBackPackComponent self)
        {
            return self.GetComponent<ItemManagerComponent>().GetItemList();
        }

        public static List<ItemComponent> GetItemList(this PlayerBackPackComponent self, ItemType itemType)
        {
            return self.GetComponent<ItemManagerComponent>().GetItemList(itemType);
        }

        public static ItemComponent GetItem(this PlayerBackPackComponent self, long itemId)
        {
            return self.GetComponent<ItemManagerComponent>().GetItem(itemId);
        }

        public static ItemComponent GetItemWhenStack(this PlayerBackPackComponent self, string itemCfgId)
        {
            return self.GetComponent<ItemManagerComponent>().GetItemWhenStack(itemCfgId);
        }

        public static List<ItemComponent> GetItemWhenNoStack(this PlayerBackPackComponent self, string itemCfgId)
        {
            return self.GetComponent<ItemManagerComponent>().GetItemWhenNoStack(itemCfgId);
        }

        public static void AddItem(this PlayerBackPackComponent self, string itemCfgId, int count)
        {
            self.GetComponent<ItemManagerComponent>().AddItem(itemCfgId, count);
        }

        public static void SetItem(this PlayerBackPackComponent self, string itemCfgId, int count)
        {
            self.GetComponent<ItemManagerComponent>().SetItem(itemCfgId, count);
        }

        public static void RemoveItem(this PlayerBackPackComponent self, string itemCfgId)
        {
            self.GetComponent<ItemManagerComponent>().RemoveItem(itemCfgId);
        }

        public static void ClearAllItem(this PlayerBackPackComponent self)
        {
            self.GetComponent<ItemManagerComponent>().ClearAllItem();
        }
    }
}