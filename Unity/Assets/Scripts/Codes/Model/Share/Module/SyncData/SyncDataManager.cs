using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof (Scene))]
    public class SyncDataManager: Entity, IAwake, IDestroy, ILateUpdate
    {
        public MultiMapSimple<long, byte[]> player2SyncDataList;
        public int waitFrameSync = 60;
        public int curFrameSync = 0;
        public Dictionary<long, int> playerSessionInfoList;
        public List<long> syncData2Players;
    }
}