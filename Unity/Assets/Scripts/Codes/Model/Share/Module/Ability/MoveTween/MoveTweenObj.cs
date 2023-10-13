using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class MoveTweenObj: Entity, IAwake, IDestroy, IFixedUpdate
    {
        ///<summary>
        ///</summary>
        public long unitId;

        ///<summary>
        ///速度，单位：米/秒
        ///</summary>
        public float speed;

        ///<summary>
        ///面向
        ///</summary>
        public float3 forward;

        public float3 lastPosition;
        public float3 lastTargetPosition;

        ///<summary>
        ///已经存在了多久了，单位：秒
        ///</summary>
        public float timeElapsed = 0;

        public MoveTweenType moveTweenType;

        public SelectHandle selectHandle;
    }
}