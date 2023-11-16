using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(PlayerCacheManagerComponent))]
    public static class PlayerCacheManagerComponentSystem
    {
        [ObjectSystem]
        public class PlayerCacheManagerComponentAwakeSystem : AwakeSystem<PlayerCacheManagerComponent>
        {
            protected override void Awake(PlayerCacheManagerComponent self)
            {
            }
        }

        public static PlayerDataComponent GetPlayerData(this PlayerCacheManagerComponent self, long playerId)
        {
            PlayerDataComponent playerDataComponent = self.GetChild<PlayerDataComponent>(playerId);
            return playerDataComponent;
        }
    }
}