using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class MonsterCallComponent: Entity, IAwake, IDestroy, ITransferClient
    {
        public long playerId;
        public string monsterCallCfgId;
    }
}