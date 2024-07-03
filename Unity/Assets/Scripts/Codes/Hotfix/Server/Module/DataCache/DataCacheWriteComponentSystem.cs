using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [Invoke(TimerInvokeType.DataCacheWriteChkTimer)]
    public class DataCacheWriteComponentTimer: ATimer<DataCacheWriteComponent>
    {
        protected override void Run(DataCacheWriteComponent self)
        {
            try
            {
                TimerComponent.Instance?.Remove(ref self.Timer);
                self.Update().Coroutine();
            }
            catch (Exception e)
            {
                Log.Error($"DataCacheWriteComponentTimer error: {self.Id}\n{e}");
            }
        }
    }

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
                TimerComponent.Instance?.Remove(ref self.Timer);
            }
        }

        public static async ETTask Update(this DataCacheWriteComponent self)
        {
            if (self.waitingForWrite == false)
            {
                return;
            }
            await ET.Server.DBHelper.SaveDB(self.Parent);
            self.waitingForWrite = false;
        }
    }
}