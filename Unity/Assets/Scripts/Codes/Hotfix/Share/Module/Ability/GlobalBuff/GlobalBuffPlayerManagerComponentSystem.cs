using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalBuffPlayerManagerComponent))]
    public static class GlobalBuffPlayerManagerComponentSystem
    {
        [ObjectSystem]
        public class GlobalBuffPlayerManagerComponentAwakeSystem: AwakeSystem<GlobalBuffPlayerManagerComponent>
        {
            protected override void Awake(GlobalBuffPlayerManagerComponent self)
            {
                self.removeList = new();
            }
        }

        [ObjectSystem]
        public class GlobalBuffPlayerManagerComponentDestroySystem: DestroySystem<GlobalBuffPlayerManagerComponent>
        {
            protected override void Destroy(GlobalBuffPlayerManagerComponent self)
            {
                self.removeList.Clear();
            }
        }

        [ObjectSystem]
        public class GlobalBuffPlayerManagerComponentFixedUpdateSystem: FixedUpdateSystem<GlobalBuffPlayerManagerComponent>
        {
            protected override void FixedUpdate(GlobalBuffPlayerManagerComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask AddGlobalBuff(this GlobalBuffPlayerManagerComponent self, long playerId, long casterPlayerId, string playerGlobalBuffCfgId)
        {
            GlobalBuffPlayerComponent globalBuffPlayerComponent = self.GetChild<GlobalBuffPlayerComponent>(playerId);
            if (globalBuffPlayerComponent == null)
            {
                globalBuffPlayerComponent = self.AddChildWithId<GlobalBuffPlayerComponent>(playerId);
            }

            await globalBuffPlayerComponent.AddGlobalBuff(playerId, casterPlayerId, playerGlobalBuffCfgId);
        }

        public static void EventHandler(this GlobalBuffPlayerManagerComponent self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            foreach (var child in self.Children)
            {
                long playerId = child.Key;
                if (actionGameContext.playerId != 0 && actionGameContext.playerId != playerId)
                {
                    continue;
                }
                GlobalBuffPlayerComponent globalBuffPlayerComponent = (GlobalBuffPlayerComponent)child.Value;
                if (globalBuffPlayerComponent != null)
                {
                    globalBuffPlayerComponent.EventHandler(abilityGameMonitorTriggerEvent, ref actionGameContext);
                }
            }
        }

        public static void FixedUpdate(this GlobalBuffPlayerManagerComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            self.removeList.Clear();

            foreach (var child in self.Children)
            {
                long playerId = child.Key;
                bool isQuit = GamePlayHelper.GetGamePlay(self.DomainScene()).ChkPlayerIsQuit(playerId);
                if (isQuit)
                {
                    self.removeList.Add(playerId);
                    continue;
                }
                GlobalBuffPlayerComponent globalBuffPlayerComponent = child.Value as GlobalBuffPlayerComponent;
                globalBuffPlayerComponent.FixedUpdate(fixedDeltaTime);
            }

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                self.RemoveChild(self.removeList[i]);
            }
            self.removeList.Clear();

        }

    }
}