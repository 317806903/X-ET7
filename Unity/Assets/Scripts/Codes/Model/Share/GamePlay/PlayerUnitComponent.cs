using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class PlayerUnitComponent: Entity, IAwake, IDestroy, ITransferClient
    {
        public long playerId;
    }
}