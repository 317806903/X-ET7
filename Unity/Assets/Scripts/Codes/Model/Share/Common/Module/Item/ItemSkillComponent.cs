using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(ItemComponent))]
    public class ItemSkillComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        public string CfgId { get; set; } //配置表id

        [BsonIgnore]
        public SkillCfg model
        {
            get
            {
                return SkillCfgCategory.Instance.Get(this.CfgId);
            }
        }

        public int level;
    }
}