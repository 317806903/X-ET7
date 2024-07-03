using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(DataCacheWriteComponent))]
    public static class DataCacheWriteComponentSystem
    {
        [ObjectSystem]
        public class DataCacheWriteComponentAwakeSystem : AwakeSystem<DataCacheWriteComponent>
        {
            protected override void Awake(DataCacheWriteComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DataCacheWriteComponentDestroySystem : DestroySystem<DataCacheWriteComponent>
        {
            protected override void Destroy(DataCacheWriteComponent self)
            {
            }
        }

        public static void SetNeedSave(this DataCacheWriteComponent self, bool isForce)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
            long time = 2000;
            if (isForce)
            {
                time = 100;
            }

            self.waitingForWrite = true;
            self.Timer = TimerComponent.Instance.NewOnceTimer(time, TimerInvokeType.DataCacheWriteChkTimer, self);
        }
    }
}