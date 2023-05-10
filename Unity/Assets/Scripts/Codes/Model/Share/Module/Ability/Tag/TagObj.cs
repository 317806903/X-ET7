using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof (TagComponent))]
    [FriendOf(typeof(Unit))]
    public class TagObj: Entity, IAwake, IDestroy
    {
        ///<summary>
        ///
        ///</summary>
        public string tag;
    }
}