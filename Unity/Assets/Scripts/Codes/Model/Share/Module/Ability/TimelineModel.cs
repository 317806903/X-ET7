using System.Collections.Generic;

namespace ET.Ability
{
    ///<summary>
    ///策划预先填表制作的，就是这个东西，同样她也是被clone到obj当中去的
    ///</summary>
    public struct TimelineModel
    {
        public string id;

        ///<summary>
        ///Timeline运行多久之后发生，单位：秒
        ///</summary>
        public List<TimelineNode> nodes;

        ///<summary>
        ///Timeline一共多长时间（到时间了就丢掉了），单位秒
        ///</summary>
        public float duration;

        ///<summary>
        ///如果有caster，并且caster处于蓄力状态，则可能会经历跳转点
        ///</summary>
        public TimelineGoTo chargeGoBack;

        // public TimelineModel(string id, TimelineNode[] nodes, float duration, TimelineGoTo chargeGoBack)
        // {
        //     this.id = id;
        //     this.nodes = nodes;
        //     this.duration = duration;
        //     this.chargeGoBack = chargeGoBack;
        // }
    }

    ///<summary>
    ///Timeline每一个节点上要发生的事情
    ///</summary>
    public struct TimelineNode
    {
        ///<summary>
        ///Timeline运行多久之后发生，单位：秒
        ///</summary>
        public float timeElapsed;

        ///<summary>
        ///要执行的脚本函数
        ///</summary>
        public string actionId;


        // public TimelineNode(float time, int actionId)
        // {
        //     this.timeElapsed = time;
        //     this.actionId = actionId;
        // }
    }

    ///<summary>
    ///Timeline的一个跳转点信息
    ///</summary>
    public struct TimelineGoTo
    {
        ///<summary>
        ///自身处于时间点
        ///</summary>
        public float atDuration;

        ///<summary>
        ///跳转到时间点
        ///</summary>
        public float gotoDuration;

        public TimelineGoTo(float atDuration, float gotoDuration)
        {
            this.atDuration = atDuration;
            this.gotoDuration = gotoDuration;
        }

        [StaticField]
        public static TimelineGoTo Null = new TimelineGoTo(float.MaxValue, float.MaxValue);
    }
}