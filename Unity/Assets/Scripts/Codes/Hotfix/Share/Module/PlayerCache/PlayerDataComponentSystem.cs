using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(PlayerDataComponent))]
    public static class PlayerDataComponentSystem
    {
        [ObjectSystem]
        public class PlayerDataComponentAwakeSystem : AwakeSystem<PlayerDataComponent>
        {
            protected override void Awake(PlayerDataComponent self)
            {
                self.playerId = self.Id;
            }
        }

        public static Entity GetPlayerModel(this PlayerDataComponent self, PlayerModelType playerModelType)
        {
            switch (playerModelType)
            {
                case PlayerModelType.BaseInfo:
                    return self.GetComponent<PlayerBaseInfoComponent>();
                case PlayerModelType.BackPack:
                    return self.GetComponent<PlayerBackPackComponent>();
                case PlayerModelType.BattleCard:
                    return self.GetComponent<PlayerBattleCardComponent>();
                default:
                    break;
            }

            return null;
        }

        public static Entity SetPlayerModel(this PlayerDataComponent self, PlayerModelType playerModelType, byte[] bytes)
        {
            Entity entity = MongoHelper.Deserialize<Entity>(bytes);
            switch (playerModelType)
            {
                case PlayerModelType.BaseInfo:
                    self.RemoveComponent<PlayerBaseInfoComponent>();
                    break;
                case PlayerModelType.BackPack:
                    self.RemoveComponent<PlayerBackPackComponent>();
                    break;
                case PlayerModelType.BattleCard:
                    self.RemoveComponent<PlayerBattleCardComponent>();
                    break;
                default:
                    break;
            }
            return self.AddComponent(entity);
        }

    }
}