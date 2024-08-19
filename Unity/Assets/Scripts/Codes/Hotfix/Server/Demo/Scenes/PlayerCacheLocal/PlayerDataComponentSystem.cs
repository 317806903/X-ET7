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

            // await self.AddPlayerModelData(PlayerModelType.BaseInfo);
            // await self.AddPlayerModelData(PlayerModelType.BackPack);
            // await self.AddPlayerModelData(PlayerModelType.BattleCard);
            // await self.AddPlayerModelData(PlayerModelType.OtherInfo);
            // await self.AddPlayerModelData(PlayerModelType.SeasonInfo);
            // await self.AddPlayerModelData(PlayerModelType.FunctionMenu);
            // await self.AddPlayerModelData(PlayerModelType.Mails);
            await ETTask.CompletedTask;
        }

        public static async ETTask<Entity> AddPlayerModelData(this PlayerDataComponent self, PlayerModelType playerModelType)
        {
            switch (playerModelType)
            {
                case PlayerModelType.BaseInfo:
                    PlayerBaseInfoComponent playerBaseInfoComponent = await self.InitByDBOne<PlayerBaseInfoComponent>(self.playerId);
                    playerBaseInfoComponent.Init();
                    if (playerBaseInfoComponent.seasonIndex == 0)
                    {
                        playerBaseInfoComponent.seasonIndex = await SeasonHelper.GetSeasonIndex(self.DomainScene());
                        playerBaseInfoComponent.seasonCfgId = await SeasonHelper.GetSeasonCfgId(self.DomainScene());
                        playerBaseInfoComponent.SetDataCacheAutoWrite();
                    }
                    return playerBaseInfoComponent;
                case PlayerModelType.BackPack:
                    PlayerBackPackComponent playerBackPackComponent = await self.InitByDBOne<PlayerBackPackComponent>(self.playerId);
                    playerBackPackComponent.Init();
                    return playerBackPackComponent;
                case PlayerModelType.BattleCard:
                    PlayerBattleCardComponent playerBattleCardComponent = await self.InitByDBOne<PlayerBattleCardComponent>(self.playerId);
                    playerBattleCardComponent.Init();
                    return playerBattleCardComponent;
                case PlayerModelType.OtherInfo:
                    PlayerOtherInfoComponent playerOtherInfoComponent = await self.InitByDBOne<PlayerOtherInfoComponent>(self.playerId);
                    playerOtherInfoComponent.Init();
                    return playerOtherInfoComponent;
                case PlayerModelType.SeasonInfo:
                    PlayerSeasonInfoComponent playerSeasonInfoComponent = await self.InitByDBOne<PlayerSeasonInfoComponent>(self.playerId);
                    playerSeasonInfoComponent.Init();
                    if (playerSeasonInfoComponent.seasonIndex == 0)
                    {
                        playerSeasonInfoComponent.seasonIndex = await SeasonHelper.GetSeasonIndex(self.DomainScene());
                        playerSeasonInfoComponent.seasonCfgId = await SeasonHelper.GetSeasonCfgId(self.DomainScene());
                        playerSeasonInfoComponent.SetDataCacheAutoWrite();
                    }

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