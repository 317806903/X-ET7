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

    [ChildOf(typeof(PlayerCacheManagerComponent))]
    public class PlayerDataComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        public long playerId;
    }
}