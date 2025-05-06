using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (StatusComponent))]
    [FriendOf(typeof (StatusObj))]
    public static class StatusComponentSystem
    {
        [ObjectSystem]
        public class StatusComponentAwakeSystem: AwakeSystem<StatusComponent>
        {
            protected override void Awake(StatusComponent self)
            {
                self.removeList = new();
            }
        }

        [ObjectSystem]
        public class StatusComponentDestroySystem: DestroySystem<StatusComponent>
        {
            protected override void Destroy(StatusComponent self)
            {
                self.removeList.Clear();
            }
        }

        public static StatusObj AddStatus(this StatusComponent self, int statusCfgId)
        {
            StatusObj statusObj = self.AddChild<StatusObj>();
            statusObj.Init(statusCfgId);

            return statusObj;
        }
    }
}