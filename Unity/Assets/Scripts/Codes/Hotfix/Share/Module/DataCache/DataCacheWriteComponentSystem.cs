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

        public static void SetNeedSave(this DataCacheWriteComponent self)
        {
            self.IsNeedWrite = true;
        }
    }
}