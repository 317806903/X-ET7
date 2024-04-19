using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof (EffectComponent))]
    [FriendOf(typeof(Unit))]
    public class EffectObj: Entity, IAwake, IDestroy
    {
        public string CfgId { get; set; }

        public string PlayAudioActionId { get; set; }

        ///<summary>
        ///这是个什么Effect
        ///</summary>
        [BsonIgnore]
        public ResEffectCfg model
        {
            get
            {
                return ResEffectCfgCategory.Instance.Get(this.CfgId);
            }
        }

        ///<summary>
        ///挂载点,用于查找出这个transform后添加为子节点
        ///</summary>
        public EffectNodeName hangPointName;

        ///<summary>
        ///相对挂载点的偏移位置
        ///</summary>
        public float3 offSet;

        ///<summary>
        ///相对挂载点的旋转
        ///</summary>
        public float3 rotation;

        ///<summary>
        ///标志key，用来记录后便于准确删除
        ///</summary>
        public string key;

        ///<summary>
        ///剩余多久，单位：秒
        ///</summary>
        public float duration;

        ///<summary>
        ///是否是一个永久的effect，永久的duration不会减少，但是timeElapsed还会增加
        ///</summary>
        public bool permanent;

        ///<summary>
        ///effect已经存在了多少时间了，单位：秒
        ///</summary>
        public float timeElapsed = 0.00f;

        public long createTime;
        public float3 createPos;

        public bool isScaleByUnit;

        public EffectShowType effectShowType;

        ///<summary>
        ///施加者是谁
        ///</summary>
        public long casterUnitId;

        /// <summary>
        /// 记录闪电拖尾特效的必经点(unitId)
        /// </summary>
        public List<long> pointLightningTrailList;
    }
}