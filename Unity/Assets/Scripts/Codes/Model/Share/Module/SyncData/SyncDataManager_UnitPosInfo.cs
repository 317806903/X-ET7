using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof (SyncDataManager))]
    public class SyncDataManager_UnitPosInfo : Entity, IAwake, IDestroy
    {
        public HashSet<Unit> NeedSyncPosUnits;

        public int waitFrameSyncPos = 2;
        public int curFrameSyncPos = 0;

    }
}