using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (TagObj))]
    public static class TagObjSystem
    {
        [ObjectSystem]
        public class TagObjAwakeSystem: AwakeSystem<TagObj>
        {
            protected override void Awake(TagObj self)
            {
            }
        }

        [ObjectSystem]
        public class TagObjDestroySystem: DestroySystem<TagObj>
        {
            protected override void Destroy(TagObj self)
            {
            }
        }

        public static void Init(this TagObj self, int buffCfgId)
        {
        }

    }
}