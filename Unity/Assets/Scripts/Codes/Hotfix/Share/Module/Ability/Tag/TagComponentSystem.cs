using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (TagComponent))]
    [FriendOf(typeof (TagObj))]
    public static class TagComponentSystem
    {
        [ObjectSystem]
        public class TagComponentAwakeSystem: AwakeSystem<TagComponent>
        {
            protected override void Awake(TagComponent self)
            {
                self.removeList = new();
            }
        }

        [ObjectSystem]
        public class TagComponentDestroySystem: DestroySystem<TagComponent>
        {
            protected override void Destroy(TagComponent self)
            {
                self.removeList.Clear();
            }
        }

        public static TagObj AddTag(this TagComponent self, int tagCfgId)
        {
            TagObj tagObj = self.AddChild<TagObj>();
            tagObj.Init(tagCfgId);

            return tagObj;
        }
    }
}