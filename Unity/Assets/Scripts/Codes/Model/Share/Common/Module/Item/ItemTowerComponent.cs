using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(ItemComponent))]
    public class ItemTowerComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        public string CfgId { get; set; } //配置表id

        [BsonIgnore]
        public TowerDefense_TowerCfg model
        {
            get
            {
                return TowerDefense_TowerCfgCategory.Instance.Get(this.CfgId);
            }
        }

        public int level;
    }
}