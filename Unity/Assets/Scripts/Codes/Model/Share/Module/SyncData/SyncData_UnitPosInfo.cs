using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public class SyncData_UnitPosInfo : Entity, IAwake, IDestroy
    {
        public long unitId { get; set; }
        public int posX;
        public int posY;
        public int posZ;
        public int forwardX;
        public int forwardY;
        public int forwardZ;
    }
}