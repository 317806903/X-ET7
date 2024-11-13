using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class DeathShowComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        ///<summary>
        ///生命周期，单位：秒
        ///</summary>
        public float duration;

        ///<summary>
        ///已经存在了多久了，单位：秒
        ///</summary>
        public float timeElapsed = 0;

        public bool isDestroying;
    }
}