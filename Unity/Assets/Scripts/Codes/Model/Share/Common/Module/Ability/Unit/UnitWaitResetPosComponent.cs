using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class UnitWaitResetPosComponent: Entity, IAwake<float3>, IDestroy, IFixedUpdate
    {
        public float chkTime = 1.5f;

        public float3 startPos;
        public float3 resetPos;

        ///<summary>
        ///已经存在了多久了，单位：秒
        ///</summary>
        public float timeElapsed = 0;
    }
}