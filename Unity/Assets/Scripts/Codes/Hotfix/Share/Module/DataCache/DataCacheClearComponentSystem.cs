using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [Invoke(TimerInvokeType.DataCacheClearChkTimer)]
    public class DataCacheClearComponentTimer: ATimer<DataCacheClearComponent>
    {
        protected override void Run(DataCacheClearComponent self)
        {
            try
            {
                self.Update();
            }
            catch (Exception e)
            {
                Log.Error($"DataCacheClearComponentTimer error: {self.Id}\n{e}");
            }
        }
    }

    [FriendOf(typeof(DataCacheClearComponent))]
    public static class DataCacheClearComponentSystem
    {
        [ObjectSystem]
        public class DataCacheClearComponentAwakeSystem : AwakeSystem<DataCacheClearComponent>
        {
            protected override void Awake(DataCacheClearComponent self)
            {
                self.ChkTimeInterval = 30;

                self.StartTimer();
            }
        }

        [ObjectSystem]
        public class DataCacheClearComponentDestroySystem : DestroySystem<DataCacheClearComponent>
        {
            protected override void Destroy(DataCacheClearComponent self)
            {
                TimerComponent.Instance?.Remove(ref self.Timer);
            }
        }

        public static void StartTimer(this DataCacheClearComponent self)
        {
            self.RefreshTime();
            TimerComponent.Instance?.Remove(ref self.Timer);
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(1000, TimerInvokeType.DataCacheClearChkTimer, self);
        }

        public static void ResetChkTimeInterval(this DataCacheClearComponent self, float chkTimeInterval)
        {
            self.ChkTimeInterval = chkTimeInterval;
            self.StartTimer();
        }

        public static void RefreshTime(this DataCacheClearComponent self)
        {
            self.lastChkTime = TimeHelper.ServerNow() + (long)(self.ChkTimeInterval * 1000);
        }

        public static void Update(this DataCacheClearComponent self)
        {
            if (self.lastChkTime <= TimeHelper.ServerNow())
            {
                self.Parent.Dispose();
            }
        }

    }
}