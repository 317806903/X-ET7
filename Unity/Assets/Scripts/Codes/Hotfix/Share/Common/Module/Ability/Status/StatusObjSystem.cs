using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (StatusObj))]
    public static class StatusObjSystem
    {
        [ObjectSystem]
        public class StatusObjAwakeSystem: AwakeSystem<StatusObj>
        {
            protected override void Awake(StatusObj self)
            {
            }
        }

        [ObjectSystem]
        public class StatusObjDestroySystem: DestroySystem<StatusObj>
        {
            protected override void Destroy(StatusObj self)
            {
            }
        }

        public static void Init(this StatusObj self, int buffCfgId)
        {
        }

    }
}