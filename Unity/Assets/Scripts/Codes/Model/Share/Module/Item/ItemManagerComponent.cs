using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf]
    public class ItemManagerComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        public Dictionary<string, long> itemCfgId2ChildId;
        public MultiMapSimple<string, long> itemCfgId2ChildIdNoStack;
    }
}