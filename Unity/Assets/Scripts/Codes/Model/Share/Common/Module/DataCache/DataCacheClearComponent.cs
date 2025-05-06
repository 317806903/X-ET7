using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf]
    public class DataCacheClearComponent : Entity, IAwake, IDestroy
    {
        public long Timer;
        public long lastChkTime;
        public float ChkTimeInterval;
    }
}