using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    public static class GlobalBuffManagerComponentSystem
    {
        [ObjectSystem]
        public class GlobalBuffComponentAwakeSystem: AwakeSystem<GlobalBuffManagerComponent>
        {
            protected override void Awake(GlobalBuffManagerComponent self)
            {
                self.AddComponent<GlobalBuffGameComponent>();
                self.AddComponent<GlobalBuffPlayerManagerComponent>();
                self.AddComponent<GlobalBuffUnitComponent>();
            }
        }

        [ObjectSystem]
        public class GlobalBuffComponentDestroySystem: DestroySystem<GlobalBuffManagerComponent>
        {
            protected override void Destroy(GlobalBuffManagerComponent self)
            {
            }
        }

        [ObjectSystem]
        public class GlobalBuffComponentFixedUpdateSystem: FixedUpdateSystem<GlobalBuffManagerComponent>
        {
            protected override void FixedUpdate(GlobalBuffManagerComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this GlobalBuffManagerComponent self, float fixedDeltaTime)
        {
            self.GetComponent<GlobalBuffGameComponent>().FixedUpdate(fixedDeltaTime);
            self.GetComponent<GlobalBuffPlayerManagerComponent>().FixedUpdate(fixedDeltaTime);
            self.GetComponent<GlobalBuffUnitComponent>().FixedUpdate(fixedDeltaTime);
        }

        //=================================================
        public static async ETTask AddGlobalBuff_Unit(this GlobalBuffManagerComponent self, long casterPlayerId, string unitGlobalBuffCfgId, long unitId)
        {
            GlobalBuffUnitComponent globalBuffUnitComponent = self.GetComponent<GlobalBuffUnitComponent>();
            await globalBuffUnitComponent.AddGlobalBuff(casterPlayerId, unitGlobalBuffCfgId, unitId);
        }

        //=================================================

        public static async ETTask AddGlobalBuff_Player(this GlobalBuffManagerComponent self, long playerId, long casterPlayerId, string playerGlobalBuffCfgId)
        {
            GlobalBuffPlayerManagerComponent globalBuffPlayerManagerComponent = self.GetComponent<GlobalBuffPlayerManagerComponent>();
            await globalBuffPlayerManagerComponent.AddGlobalBuff(playerId, casterPlayerId, playerGlobalBuffCfgId);
        }

        //=================================================

        public static async ETTask AddGlobalBuff_Game(this GlobalBuffManagerComponent self, TeamFlagType teamFlagType, long casterPlayerId, string gameGlobalBuffCfgId)
        {
            GlobalBuffGameComponent globalBuffGameComponent = self.GetComponent<GlobalBuffGameComponent>();
            await globalBuffGameComponent.AddGlobalBuff(teamFlagType, casterPlayerId, gameGlobalBuffCfgId);
        }

        //=================================================

        public static void EventHandler(this GlobalBuffManagerComponent self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            self.GetComponent<GlobalBuffGameComponent>()?.EventHandler(abilityGameMonitorTriggerEvent, ref actionGameContext);
            self.GetComponent<GlobalBuffPlayerManagerComponent>()?.EventHandler(abilityGameMonitorTriggerEvent, ref actionGameContext);
            self.GetComponent<GlobalBuffUnitComponent>()?.EventHandler(abilityGameMonitorTriggerEvent, ref actionGameContext);
        }

    }
}