using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class AttackTargetComponent: Entity, IAwake, IDestroy, IFixedUpdate, ITransferClient
    {
        [BsonIgnore]
        public int waitFrameChk = 5;
        [BsonIgnore]
        public int curFrameChk = 0;


        public float maxTiltedDownAngle;
        public float maxTiltedUpAngle;

        public long attackTargetUnitId;
    }
}