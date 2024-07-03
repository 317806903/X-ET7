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

            PlayerBaseInfoComponent playerBaseInfoComponent = await self.InitByDBOne<PlayerBaseInfoComponent>(self.playerId);

            PlayerBackPackComponent playerBackPackComponent = await self.InitByDBOne<PlayerBackPackComponent>(self.playerId);
            playerBackPackComponent.Init();

            PlayerBattleCardComponent playerBattleCardComponent = await self.InitByDBOne<PlayerBattleCardComponent>(self.playerId);

            PlayerOtherInfoComponent playerOtherInfoComponent = await self.InitByDBOne<PlayerOtherInfoComponent>(self.playerId);
            playerOtherInfoComponent.Init();

            PlayerSeasonInfoComponent playerSeasonInfoComponent = await self.InitByDBOne<PlayerSeasonInfoComponent>(self.playerId);
            playerSeasonInfoComponent.Init();

            PlayerFunctionMenuComponent playerFunctionMenuComponent = await self.InitByDBOne<PlayerFunctionMenuComponent>(self.playerId);
            playerFunctionMenuComponent.Init();

            PlayerMailComponent playerMailComponent = await self.InitByDBOne<PlayerMailComponent>(self.playerId);
            playerMailComponent.Init();
        }

        public static async ETTask<Entity> AddPlayerModelData(this PlayerDataComponent self, PlayerModelType playerModelType)
        {
            switch (playerModelType)
            {
                case PlayerModelType.BaseInfo:
                    PlayerBaseInfoComponent playerBaseInfoComponent = await self.InitByDBOne<PlayerBaseInfoComponent>(self.playerId);
                    return playerBaseInfoComponent;
                case PlayerModelType.BackPack:
                    PlayerBackPackComponent playerBackPackComponent = await self.InitByDBOne<PlayerBackPackComponent>(self.playerId);
                    playerBackPackComponent.Init();
                    return playerBackPackComponent;
                case PlayerModelType.BattleCard:
                    PlayerBattleCardComponent playerBattleCardComponent = await self.InitByDBOne<PlayerBattleCardComponent>(self.playerId);
                    return playerBattleCardComponent;
                case PlayerModelType.OtherInfo:
                    PlayerOtherInfoComponent playerOtherInfoComponent = await self.InitByDBOne<PlayerOtherInfoComponent>(self.playerId);
                    playerOtherInfoComponent.Init();
                    return playerOtherInfoComponent;
                case PlayerModelType.SeasonInfo:
                    PlayerSeasonInfoComponent playerSeasonInfoComponent = await self.InitByDBOne<PlayerSeasonInfoComponent>(self.playerId);
                    playerSeasonInfoComponent.Init();
                    return playerSeasonInfoComponent;
                case PlayerModelType.FunctionMenu:
                    PlayerFunctionMenuComponent playerFunctionMenuComponent = await self.InitByDBOne<PlayerFunctionMenuComponent>(self.playerId);
                    playerFunctionMenuComponent.Init();
                    return playerFunctionMenuComponent;
                case PlayerModelType.Mails:
                    PlayerMailComponent playerMailComponent = await self.InitByDBOne<PlayerMailComponent>(self.playerId);
                    playerMailComponent.Init();
                    return playerMailComponent;
                default:
                    break;
            }

            return null;
        }

        public static async ETTask<T> InitByDBOne<T>(this PlayerDataComponent self, long playerId) where T :Entity, IAwake, new()
        {
            return await ET.Server.DBHelper.LoadDBWithParent2Component<T>(self, playerId, true);
        }

    }
}