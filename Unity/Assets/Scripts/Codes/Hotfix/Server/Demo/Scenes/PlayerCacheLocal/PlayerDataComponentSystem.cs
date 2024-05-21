using System.Collections.Generic;
using System.Linq;
using ET.Server;

namespace ET.Server
{
    [FriendOf(typeof(PlayerDataComponent))]
    public static class PlayerDataComponentSystem
    {
        [ObjectSystem]
        public class PlayerDataComponentAwakeSystem : AwakeSystem<PlayerDataComponent>
        {
            protected override void Awake(PlayerDataComponent self)
            {
            }
        }

        public static async ETTask InitByDB(this PlayerDataComponent self, long playerId)
        {
            self.playerId = playerId;

            PlayerBaseInfoComponent playerBaseInfoComponent = await self.InitByDBOne<PlayerBaseInfoComponent>(playerId);

            PlayerBackPackComponent playerBackPackComponent = await self.InitByDBOne<PlayerBackPackComponent>(playerId);
            playerBackPackComponent.Init();

            PlayerBattleCardComponent playerBattleCardComponent = await self.InitByDBOne<PlayerBattleCardComponent>(playerId);

            PlayerOtherInfoComponent playerOtherInfoComponent = await self.InitByDBOne<PlayerOtherInfoComponent>(playerId);
            playerOtherInfoComponent.Init();

            PlayerFunctionMenuComponent playerFunctionMenuComponent = await self.InitByDBOne<PlayerFunctionMenuComponent>(playerId);
            playerFunctionMenuComponent.Init();

            PlayerMailComponent playerMailComponent = await self.InitByDBOne<PlayerMailComponent>(playerId);
            playerMailComponent.Init();
        }

        public static async ETTask<T> InitByDBOne<T>(this PlayerDataComponent self, long playerId) where T :Entity, IAwake, new()
        {
            return await ET.Server.DBHelper.LoadDBWithParent2Component<T>(self, playerId, true);
        }

    }
}