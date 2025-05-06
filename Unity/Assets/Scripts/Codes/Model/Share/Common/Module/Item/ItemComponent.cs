using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ChildOf]
    public class ItemComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        public string CfgId { get; set; } //配置表id

        [BsonIgnore]
        public ItemCfg model
        {
            get
            {
                return ItemCfgCategory.Instance.Get(this.CfgId);
            }
        }

        public int count;
    }
}