using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof (BuffComponent))]
    [FriendOf(typeof(Unit))]
    public class BuffObj: Entity, IAwake, IDestroy
    {
        public string CfgId { get; set; }

        ///<summary>
        ///这是个什么buff
        ///</summary>
        [BsonIgnore]
        public BuffCfg model
        {
            get
            {
                return BuffCfgCategory.Instance.Get(this.CfgId);
            }
        }

        /// <summary>
        /// 当同组tagGroup有优先级更高的，则会处理
        /// </summary>
        public bool isEnabled;

        public List<BuffAction> buffActions;

        ///<summary>
        ///总时长，单位：秒
        ///</summary>
        public float orgDuration;

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
        ///buff已经存在了多少时间了 (在刷新时间时会被重置)，单位：秒
        ///</summary>
        public float timeElapsed = 0.00f;

        ///<summary>
        ///buff已经存在了多少时间了 (不会被重置)，单位：秒
        ///</summary>
        public float timeElapsedReal = 0.00f;

        ///<summary>
        ///buff执行了多少次onTick了，如果不会执行onTick，那将永远是0
        ///</summary>
        public int ticked = 0;

        public MultiMapSimple<AbilityConfig.BuffTriggerEvent, BuffActionCall> monitorTriggerList;

        [BsonIgnore]
        public ActionContext actionContext;

        public List<EntityRef<EffectObj>> selfEffectList;
    }
}