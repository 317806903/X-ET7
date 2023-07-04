using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof (TimelineComponent))]
    [FriendOf(typeof(Unit))]
    public class TimelineObj: Entity, IAwake, IDestroy
    {
        public string CfgId { get; set; }
        
        ///<summary>
        ///Timeline的基础信息
        ///</summary>
        [BsonIgnore]
        public TimelineCfg model
        {
            get
            {
                return TimelineCfgCategory.Instance.Get(this.CfgId);
            }
        }

        ///<summary>
        ///Timeline的焦点对象也就是创建timeline的负责人，比如技能产生的timeline，就是技能的施法者
        ///</summary>
        public long casterUnitId;

        ///<summary>
        ///倍速，1=100%，0.1=10%是最小值
        ///</summary>
        public float timeScale
        {
            get
            {
                return _timeScale;
            }
            set
            {
                _timeScale = math.max(0.100f, value);
            }
        }
        private float _timeScale = 1.00f;

        ///<summary>
        ///Timeline已经运行了多少秒了
        ///</summary>
        public float timeElapsed = 0;

        [BsonIgnore]
        public ActionContext actionContext;
    }
}