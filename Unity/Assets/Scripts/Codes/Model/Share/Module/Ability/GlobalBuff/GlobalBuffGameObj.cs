using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof (GlobalBuffGameComponent))]
    public class GlobalBuffGameObj: Entity, IAwake, IDestroy
    {
        public string CfgId { get; set; }

        ///<summary>
        ///这是个什么buff
        ///</summary>
        [BsonIgnore]
        public GameGlobalBuffCfg model
        {
            get
            {
                return GameGlobalBuffCfgCategory.Instance.Get(this.CfgId);
            }
        }

        /// <summary>
        /// 记录配置有触发事件对应的GlobalBuffObj
        /// </summary>
        public HashSet<ET.AbilityConfig.GlobalBuffTriggerEvent> monitorTriggerList;

        /// <summary>
        /// 当同组tagGroup有优先级更高的，则会处理
        /// </summary>
        public bool isEnabled;

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

        public TeamFlagType teamFlagType;

        ///<summary>
        ///施加者
        ///</summary>
        public long casterPlayerId;

        ///<summary>
        ///buff已经存在了多少时间了，单位：秒
        ///</summary>
        public float timeElapsed = 0.00f;

        ///<summary>
        ///buff执行了多少次onTick了，如果不会执行onTick，那将永远是0
        ///</summary>
        public int ticked = 0;

        public List<EntityRef<AoeObj>> selfAoeList;
    }
}