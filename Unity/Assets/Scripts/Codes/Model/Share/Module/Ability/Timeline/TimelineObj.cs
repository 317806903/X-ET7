using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof (TimelineComponent))]
    [FriendOf(typeof(Unit))]
    public class TimelineObj: Entity, IAwake, IDestroy
    {
        ///<summary>
        ///Timeline的基础信息
        ///</summary>
        public TimelineCfg model;

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

        ///<summary>
        /// 存储指定的参数信息
        ///</summary>
        public SelectHandle selectHandle;
    }
}