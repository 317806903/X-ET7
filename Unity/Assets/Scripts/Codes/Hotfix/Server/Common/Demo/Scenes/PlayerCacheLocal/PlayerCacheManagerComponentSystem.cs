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

        public static async ETTask RenameCollection(this PlayerCacheManagerComponent self, int seasonIndex)
        {
            await self.RenameCollection_SeasonInfo(seasonIndex);
        }

        public static async ETTask RenameCollection_SeasonInfo(this PlayerCacheManagerComponent self, int seasonIndex)
        {
            PlayerSeasonInfoComponent playerSeasonInfoComponent = await ET.Server.DBHelper._LoadDBFirst<PlayerSeasonInfoComponent>(self.DomainScene());
            if (playerSeasonInfoComponent != null)
            {
                if (playerSeasonInfoComponent.seasonIndex > seasonIndex)
                {
                    Log.Error($"ET.Server.PlayerCacheManagerComponentSystem.RenameCollection playerSeasonInfoComponent.seasonIndex[{playerSeasonInfoComponent.seasonIndex}] > seasonIndex[{seasonIndex}]");
                    return;
                }
            }
            await ET.Server.DBHelper.RenameCollection(self.DomainScene(), typeof(PlayerSeasonInfoComponent), seasonIndex);
            foreach (PlayerDataComponent playerDataComponent in self.Children.Values)
            {
                playerDataComponent.RemoveComponent<PlayerSeasonInfoComponent>();
                playerDataComponent.RemoveComponent<PlayerMailComponent>();
            }
        }
    }
}