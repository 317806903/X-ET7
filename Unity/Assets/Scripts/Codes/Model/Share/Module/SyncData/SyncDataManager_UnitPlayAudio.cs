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
        public List<(Unit, string, bool)> NeedSyncPlayAudioList;

        public int waitFrameSyncPlayAudio = 1;
        public int curFrameSyncPlayAudio = 0;

    }
}