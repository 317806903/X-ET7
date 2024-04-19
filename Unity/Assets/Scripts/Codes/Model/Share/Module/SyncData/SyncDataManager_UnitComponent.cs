using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof (SyncDataManager))]
    public class SyncDataManager_UnitComponent : Entity, IAwake, IDestroy
    {
        public MultiMapSetSimple<long, Type> NeedSyncList;

        public int waitFrameSync = 2;
        public int curFrameSync = 0;

    }
}