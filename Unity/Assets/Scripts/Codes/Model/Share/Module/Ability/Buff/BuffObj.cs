﻿using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof (BuffComponent))]
    [FriendOf(typeof(Unit))]
    public class BuffObj: Entity, IAwake, IDestroy
    {
        ///<summary>
        ///这是个什么buff
        ///</summary>
        public BuffModel model;

        ///<summary>
        ///剩余多久，单位：秒
        ///</summary>
        public float duration;

        ///<summary>
        ///是否是一个永久的buff，永久的duration不会减少，但是timeElapsed还会增加
        ///</summary>
        public bool permanent;

        ///<summary>
        ///当前层数
        ///</summary>
        public int stack;

        ///<summary>
        ///buff的施法者是谁，当然可以是空的
        ///</summary>
        public long casterUnitId;
        
        ///<summary>
        ///buff的携带者，实际上是作为参数传递给脚本用，具体是谁，可定是所在控件的this.gameObject了
        ///</summary>
        public long carrierUnitId;
        
        ///<summary>
        ///buff已经存在了多少时间了，单位：秒
        ///</summary>
        public float timeElapsed = 0.00f;

        ///<summary>
        ///buff执行了多少次onTick了，如果不会执行onTick，那将永远是0
        ///</summary>
        public int ticked = 0;

        ///<summary>
        ///buff的一些参数，这些参数是逻辑使用的，比如wow中牧师的盾还能吸收多少伤害，就可以记录在buffParam里面
        ///</summary>
        public Dictionary<string, object> buffParam = new Dictionary<string, object>();

        // public BuffObj(
        // BuffModel model, GameObject caster, GameObject carrier,  float duration, int stack, bool permanent = false,
        // Dictionary<string, object> buffParam = null
        // ){
        //     this.model = model;
        //     this.caster = caster;
        //     this.carrier = carrier;
        //     this.duration = duration;
        //     this.stack = stack;
        //     this.permanent = permanent;
        //     if (buffParam != null) {
        //         foreach(KeyValuePair<string, object> kv in buffParam){
        //             this.buffParam.Add(kv.Key, kv.Value);
        //         }
        //     }
        // }
    }
}