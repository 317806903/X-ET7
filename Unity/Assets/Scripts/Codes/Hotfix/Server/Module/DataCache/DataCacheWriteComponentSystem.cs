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
                self.Update();
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
                self.ChkTimeInterval = 2;

                self.StartTimer();
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

        public static void StartTimer(this DataCacheWriteComponent self)
        {
            self._RefreshTime();
            TimerComponent.Instance?.Remove(ref self.Timer);
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(1000, TimerInvokeType.DataCacheWriteChkTimer, self);
        }

        public static void ResetChkTimeInterval(this DataCacheWriteComponent self, float chkTimeInterval)
        {
            self.ChkTimeInterval = chkTimeInterval;
            self.StartTimer();
        }

        public static void _RefreshTime(this DataCacheWriteComponent self)
        {
            self.lastChkTime = TimeHelper.ServerNow() + (long)(self.ChkTimeInterval * 1000);
        }

        public static void Update(this DataCacheWriteComponent self)
        {
            if (self.lastChkTime <= TimeHelper.ServerNow())
            {
                self._RefreshTime();
                if (self.IsNeedWrite)
                {
                    ET.Server.DBHelper.SaveDB(self.Parent).Coroutine();
                    self.IsNeedWrite = false;
                }
            }
        }
    }
}