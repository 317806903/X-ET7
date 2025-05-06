using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class UnitResetNavObstacleComponent: Entity, IAwake<float, float>, IDestroy
    {
        public float resetNavObstacleRadius;
        public float resetNavObstacleHeight;
    }
}