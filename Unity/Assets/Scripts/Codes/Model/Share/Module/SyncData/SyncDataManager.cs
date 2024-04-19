using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof (Scene))]
    public class SyncDataManager: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public MultiMapSimple<long, byte[]> player2SyncDataList;
    }
}