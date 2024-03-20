using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum PlayerModelType: byte
    {
        BaseInfo,
        BackPack,
        BattleCard,
    }

    public enum PlayerModelChgType
    {
        PlayerBaseInfo_111 = 100,
        PlayerBackPack_111 = 200,
        PlayerBattleCard_111 = 300,
    }

    [ChildOf(typeof(PlayerCacheManagerComponent))]
    public class PlayerDataComponent : Entity, IAwake, IDestroy
    {
        public long playerId;
    }
}