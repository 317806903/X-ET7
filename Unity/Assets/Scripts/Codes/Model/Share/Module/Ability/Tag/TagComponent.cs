using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(TagObj))]
    [ComponentOf(typeof(Unit))]
    public class TagComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<TagObj> removeList;
    }
}