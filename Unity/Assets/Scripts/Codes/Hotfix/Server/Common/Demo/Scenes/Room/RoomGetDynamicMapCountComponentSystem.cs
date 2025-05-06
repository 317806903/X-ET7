using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Server
{
    [Invoke(TimerInvokeType.RoomGetDynamicMapCountChecker)]
    public class RoomGetDynamicMapCountComponentChecker: ATimer<RoomGetDynamicMapCountComponent>
    {
        protected override void Run(RoomGetDynamicMapCountComponent self)
        {
            try
            {
                self.Check();
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {self.Id}\n{e}");
            }
        }
    }

    [FriendOf(typeof(RoomGetDynamicMapCountComponent))]
    public static class RoomGetDynamicMapCountComponentSystem
    {
        public class RoomGetDynamicMapCountComponentAwakeSystem: AwakeSystem<RoomGetDynamicMapCountComponent>
        {
            protected override void Awake(RoomGetDynamicMapCountComponent self)
            {
                self.DynamicMapId2Count = new();

                self.RepeatedTimer = TimerComponent.Instance.NewRepeatedTimer(self.CheckInteral, TimerInvokeType.RoomGetDynamicMapCountChecker, self);

                self.GetDynamicMapCount(true).Coroutine();
            }
        }

        public class RoomGetDynamicMapCountComponentDestroySystem: DestroySystem<RoomGetDynamicMapCountComponent>
        {
            protected override void Destroy(RoomGetDynamicMapCountComponent self)
            {
                self.DynamicMapId2Count.Clear();
                TimerComponent.Instance?.Remove(ref self.RepeatedTimer);
            }
        }

        public static StartSceneConfig GetMinCountDynamicMap(this RoomGetDynamicMapCountComponent self)
        {
            int minCount = 0;
            StartSceneConfig minCountDynamic = null;
            foreach (var item in self.DynamicMapId2Count)
            {
                if (item.Value <= minCount)
                {
                    minCountDynamic = item.Key;
                    minCount = item.Value;
                }
            }

            return minCountDynamic;
        }

        public static void AddDynamicMapCount(this RoomGetDynamicMapCountComponent self, StartSceneConfig dynamicMapConfig)
        {
            if (self.DynamicMapId2Count.TryGetValue(dynamicMapConfig, out int count))
            {
                self.DynamicMapId2Count[dynamicMapConfig] = count + 1;
            }
            else
            {
                self.DynamicMapId2Count[dynamicMapConfig] = 1;
            }
        }

        public static async ETTask GetDynamicMapCount(this RoomGetDynamicMapCountComponent self, bool waitFrame)
        {
            if (waitFrame)
            {
                await TimerComponent.Instance.WaitFrameAsync();
            }
            List<StartSceneConfig> dynamicMapConfigList = StartSceneConfigCategory.Instance.GetDynamicMapAll(self.DomainZone());

            self.DynamicMapId2Count.Clear();
            foreach (StartSceneConfig startSceneConfig in dynamicMapConfigList)
            {
                M2R_GetDynamicMapCount _M2R_GetDynamicMapCount = (M2R_GetDynamicMapCount) await ActorMessageSenderComponent.Instance.Call(startSceneConfig.InstanceId, new R2M_GetDynamicMapCount());
                self.DynamicMapId2Count[startSceneConfig] = _M2R_GetDynamicMapCount.DynamicMapCount;
            }
            var lines = self.DynamicMapId2Count.Select(kvp => $"Id[{kvp.Key.Id}] Process[{kvp.Key.Process}] Name[{kvp.Key.Name}] : count[{kvp.Value.ToString()}]");
            string text = string.Join(Environment.NewLine, lines);
            Log.Info($"---zpb GetDynamicMapCount self.DynamicMapId2Count[{text}]");
        }

        public static void Check(this RoomGetDynamicMapCountComponent self)
        {
            self.GetDynamicMapCount(false).Coroutine();
        }
    }
}