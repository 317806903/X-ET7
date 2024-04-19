using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof (SyncDataManager))]
    public class SyncDataManager_UnitNumericInfo : Entity, IAwake, IDestroy
    {
        public MultiMapSetSimple<long, int> NeedSyncNumericUnitsKey;
        public HashSet<Unit> NeedSyncNumericUnits;

        public int waitFrameSyncNumericKey = 4;
        public int curFrameSyncNumericKey = 0;

        public int waitFrameSyncNumeric = 4;
        public int curFrameSyncNumeric = 0;

    }
}