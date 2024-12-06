using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof (SyncDataManager))]
    public class SyncDataManager_DamageShow : Entity, IAwake, IDestroy
    {
        public MultiMapSimple<long, (Unit unit, int damageValue, bool isCrt)> player2SyncUnit;
        public List<(Unit unit, int damageValue, bool isCrt)> NeedSyncDamageShowList;

        public Dictionary<long, int> waitFrameSync = new();
        public Dictionary<long, int> curFrameSync = new();

        public List<long> removePlayerIds = new();
    }
}