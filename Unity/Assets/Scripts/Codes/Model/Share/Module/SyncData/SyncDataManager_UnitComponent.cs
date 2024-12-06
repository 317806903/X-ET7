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
        public MultiMapSimple<long, (Unit, HashSet<Type>)> player2SyncUnit;

        public MultiMapSetSimple<long, Type> NeedSyncList;

        public Dictionary<long, int> waitFrameSync = new();
        public Dictionary<long, int> curFrameSync = new();

        public List<long> removePlayerIds = new();
    }
}