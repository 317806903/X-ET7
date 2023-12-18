using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson;

namespace ET.Server
{
    [Invoke(TimerInvokeType.ReamGetGatePlayerCountChecker)]
    public class RealmGetGatePlayerCountComponentChecker: ATimer<RealmGetGatePlayerCountComponent>
    {
        protected override void Run(RealmGetGatePlayerCountComponent self)
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

    [FriendOf(typeof(RealmGetGatePlayerCountComponent))]
    public static class RealmGetGatePlayerCountComponentSystem
    {
        public class RealmGetGatePlayerCountComponentAwakeSystem: AwakeSystem<RealmGetGatePlayerCountComponent>
        {
            protected override void Awake(RealmGetGatePlayerCountComponent self)
            {
                self.GateId2Count = new();

                self.RepeatedTimer = TimerComponent.Instance.NewRepeatedTimer(self.CheckInteral, TimerInvokeType.ReamGetGatePlayerCountChecker, self);
                self.GetGatePlayerCount(true).Coroutine();
            }
        }

        public class RealmGetGatePlayerCountComponentDestroySystem: DestroySystem<RealmGetGatePlayerCountComponent>
        {
            protected override void Destroy(RealmGetGatePlayerCountComponent self)
            {
                self.GateId2Count.Clear();
                TimerComponent.Instance?.Remove(ref self.RepeatedTimer);
            }
        }

        public static StartSceneConfig GetMinCountGate(this RealmGetGatePlayerCountComponent self)
        {
            int minCount = 0;
            StartSceneConfig minCountGate = null;
            foreach (var item in self.GateId2Count)
            {
                if (item.Value <= minCount)
                {
                    minCountGate = item.Key;
                    minCount = item.Value;
                }
            }

            return minCountGate;
        }

        public static void AddGatePlayerCount(this RealmGetGatePlayerCountComponent self, StartSceneConfig gateConfig)
        {
            if (self.GateId2Count.TryGetValue(gateConfig, out int count))
            {
                self.GateId2Count[gateConfig] = count + 1;
            }
            else
            {
                self.GateId2Count[gateConfig] = 1;
            }
        }

        public static async ETTask GetGatePlayerCount(this RealmGetGatePlayerCountComponent self, bool waitFrame)
        {
            if (waitFrame)
            {
                await TimerComponent.Instance.WaitFrameAsync();
            }

            List<StartSceneConfig> gateConfigList = StartSceneConfigCategory.Instance.GetGateAll(self.DomainZone());

            self.GateId2Count.Clear();
            foreach (StartSceneConfig startSceneConfig in gateConfigList)
            {
                G2R_GetGatePlayerCount _G2R_GetGatePlayerCount = (G2R_GetGatePlayerCount) await ActorMessageSenderComponent.Instance.Call(startSceneConfig.InstanceId, new R2G_GetGatePlayerCount());
                self.GateId2Count[startSceneConfig] = _G2R_GetGatePlayerCount.PlayerCount;
            }

            var lines = self.GateId2Count.Select(kvp => $"Id[{kvp.Key.Id}] Process[{kvp.Key.Process}] Name[{kvp.Key.Name}] : count[{kvp.Value.ToString()}]");
            string text = string.Join(Environment.NewLine, lines);
            Log.Info($"---zpb GetGatePlayerCount self.GateId2Count[{text}]");
        }

        public static void Check(this RealmGetGatePlayerCountComponent self)
        {
            self.GetGatePlayerCount(false).Coroutine();
        }
    }
}