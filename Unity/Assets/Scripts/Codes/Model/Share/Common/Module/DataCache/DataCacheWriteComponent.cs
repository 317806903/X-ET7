using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf]
    public class DataCacheWriteComponent : Entity, IAwake, IDestroy
    {
        public static long DefaultSaveWaitTime = 2000;
        public long Timer;
        public bool waitingForWrite;
    }
}