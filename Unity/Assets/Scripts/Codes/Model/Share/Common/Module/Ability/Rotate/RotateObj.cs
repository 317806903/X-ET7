using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof (RotateComponent))]
    [FriendOf(typeof(Unit))]
    public class RotateObj: Entity, IAwake, IDestroy
    {
        ///<summary>
        ///想要旋转的增量角度
        ///</summary>
        public float incrementRotate;
    }
}