using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public class SyncData_UnitPlayAudio : Entity, IAwake, IDestroy
    {
        public long unitId { get; set; }
        public string playAudioActionId;
    }
}