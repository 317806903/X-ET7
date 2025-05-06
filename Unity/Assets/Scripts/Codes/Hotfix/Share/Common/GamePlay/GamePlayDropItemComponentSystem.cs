using ET.Ability;
using System;
using System.Collections.Generic;
using System.Numerics;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (GamePlayDropItemComponent))]
    public static class GamePlayDropItemComponentSystem
    {
        [ObjectSystem]
        public class GamePlayDropItemComponentAwakeSystem: AwakeSystem<GamePlayDropItemComponent>
        {
            protected override void Awake(GamePlayDropItemComponent self)
            {
                self.playerId2DropItems = new();
            }
        }

        [ObjectSystem]
        public class GamePlayDropItemComponentDestroySystem: DestroySystem<GamePlayDropItemComponent>
        {
            protected override void Destroy(GamePlayDropItemComponent self)
            {
                self.playerId2DropItems?.Clear();
            }
        }

        public static void Init(this GamePlayDropItemComponent self)
        {
        }

        public static void RecordPlayerDropItemsInfo(this GamePlayDropItemComponent self, long playerId, Dictionary<string, int> items)
        {
            DictionaryComponent<string, int> tmp = DictionaryComponent<string, int>.Create();
            foreach((string itemID, int itemCnt) in items)
            {
                if (tmp.TryGetValue(itemID, out int count) == false)
                {
                    tmp[itemID] = itemCnt;
                }
                else
                {
                    tmp[itemID] += itemCnt;
                }
            }

            foreach((string itemID, int itemCnt) in tmp)
            {
                self.playerId2DropItems.Add(playerId, itemID, itemCnt);
            }

        }

        public static Dictionary<string, int> GetPlayerDropItemList(this GamePlayDropItemComponent self, long playerId)
        {
            if (self.playerId2DropItems == null)
            {
                return null;
            }
            self.playerId2DropItems.TryGetValue(playerId, out var dropItemList);
            return dropItemList;
        }
    }
}