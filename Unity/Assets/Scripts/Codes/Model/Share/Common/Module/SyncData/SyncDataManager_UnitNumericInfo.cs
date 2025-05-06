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
        public MultiDictionary<long, Unit, HashSet<int>> player2SyncUnit;
        public MultiMapSetSimple<long, Unit> player2SyncUnit_AllKey;

        public MultiMapSetSimple<long, int> NeedSyncNumericUnit;
        public HashSet<Unit> NeedSyncNumericUnit_AllKey;

        public Dictionary<long, int> waitFrameSyncNumericKey = new();
        public Dictionary<long, int> curFrameSyncNumericKey = new();

        public Dictionary<long, int> waitFrameSyncNumeric = new();
        public Dictionary<long, int> curFrameSyncNumeric = new();

        public List<long> removePlayerIds = new();
        public List<long> removePlayerIds_AllKey = new();
    }
}