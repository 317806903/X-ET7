using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class TowerComponent: Entity, IAwake, IDestroy, ITransferClient
    {
        public string towerCfgId;
        public long playerId;

        [BsonIgnore]
        public TowerType towerType
        {
            get
            {
                return this.model.Type;
            }
        }
        [BsonIgnore]
        public TowerDefense_TowerCfg model
        {
            get
            {
                return TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
            }
        }
    }
}