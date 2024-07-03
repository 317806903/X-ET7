using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf]
    public class DataCacheWriteComponent : Entity, IAwake, IDestroy
    {
        public long Timer;
        public bool waitingForWrite;
    }
}