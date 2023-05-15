using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof (TimelineComponent))]
    [FriendOf(typeof(Unit))]
    public class TimelineObj: Entity, IAwake<TimelineCfg, Unit>, IDestroy
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
        ///Timeline的创建参数，如果是一个技能，这就是一个skillObj
        ///</summary>
        public object param;

        ///<summary>
        ///Timeline已经运行了多少秒了
        ///</summary>
        public float timeElapsed = 0;

        ///<summary>
        ///一些重要的逻辑参数，是根据游戏机制在程序层提供的，这里目前需要的是
        ///[faceDegree] 发生时如果有caster，则caster企图面向的角度（主动）。
        ///[moveDegree] 发生时如果有caster，则caster企图移动向的角度（主动）。
        ///</summary>
        public Dictionary<string, object> values;

        /////<summary>
        /////尝试从values获得某个值
        /////<param name="key">这个值的key{faceDegree, moveDegree}</param>
        /////<return>取出对应的值，如果不存在就是null</return>
        /////</summary>
        //public object GetValue(string key)
        //{
        //    if (values.ContainsKey(key) == false) return null;
        //    return values[key];
        //}
    }
}