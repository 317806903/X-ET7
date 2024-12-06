using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof (SyncDataManager))]
    public class SyncDataManager_UnitPlayAudio : Entity, IAwake, IDestroy
    {
        public MultiMapSetSimple<long, (Unit, string, bool)> player2SyncUnit;
        public List<(Unit, string, bool)> NeedSyncPlayAudioList;

        public Dictionary<long, int> waitFrameSync = new();
        public Dictionary<long, int> curFrameSync = new();

        public List<long> removePlayerIds = new();
    }
}