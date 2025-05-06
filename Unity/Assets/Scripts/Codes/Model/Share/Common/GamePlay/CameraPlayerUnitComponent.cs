using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class CameraPlayerUnitComponent: Entity, IAwake, IDestroy, ITransferClient
    {
        public long playerId;
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, long> skillIndex2PlayerSkillUnitId = new();
    }
}