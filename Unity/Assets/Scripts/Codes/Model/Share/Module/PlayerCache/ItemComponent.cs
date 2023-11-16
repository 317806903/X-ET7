using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum ItemType: byte
    {
    }

    [ChildOf]
    public class ItemComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        public ItemType itemType;
    }
}