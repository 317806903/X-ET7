using System.Collections.Generic;
using System.Linq;

namespace ET.Server
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

        public static async ETTask<PlayerDataComponent> AddPlayerData(this PlayerCacheManagerComponent self, long playerId)
        {
            PlayerDataComponent playerDataComponent = self.AddChildWithId<PlayerDataComponent>(playerId);
            await playerDataComponent.InitByDB(playerId);
            return playerDataComponent;
        }

        public static async ETTask RenameCollection(this PlayerCacheManagerComponent self, int seasonId)
        {
            await ET.Server.DBHelper.RenameCollection(self.DomainScene(), typeof(PlayerSessionComponent), seasonId);
        }
    }
}