using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public class SyncData_UnitComponent : Entity, IAwake, IDestroy
    {
        public long unitId;

        public List<byte[]> unitComponents;
    }
}