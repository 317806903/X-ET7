using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof (MoveComponent))]
    [FriendOf(typeof(Unit))]
    public class MoveObj: Entity, IAwake, IDestroy
    {
        ///<summary>
        ///想要移动的方向和距离
        ///</summary>
        public float3 velocity;

        ///<summary>
        ///多久完成，单位秒
        ///</summary>
        public float inTime;

        ///<summary>
        ///还有多久移动完成，单位：秒，如果小于1帧的时间但还大于0，就会当做1帧来执行
        ///</summary>
        public float duration;
        
        ///<summary>
        ///已经存在了多少时间了，单位：秒
        ///</summary>
        public float timeElapsed = 0.00f;
    }
}