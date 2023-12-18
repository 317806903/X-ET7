using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class GlobalBuffHelper
    {
        public static GlobalBuffComponent _GetGlobalBuffComponent(Scene scene)
        {
            GlobalBuffComponent globalBuffComponent = scene.GetComponent<GlobalBuffComponent>();
            if (globalBuffComponent == null)
            {
                return null;
            }
            return globalBuffComponent;
        }

        public static async ETTask AddGlobalBuff_Unit(Unit targetUnit, UnitGlobalBuffCfg unitGlobalBuffCfg, ActionContext actionContext)
        {
            GlobalBuffComponent globalBuffComponent = _GetGlobalBuffComponent(targetUnit.DomainScene());
            if (globalBuffComponent == null)
            {
                return;
            }
            await globalBuffComponent.AddGlobalBuff_Unit(targetUnit, unitGlobalBuffCfg, actionContext);
        }

        public static async ETTask AddGlobalBuff_Player(Scene scene, long playerId, PlayerGlobalBuffCfg playerGlobalBuffCfg, ActionPlayerContext actionPlayerContext)
        {
            GlobalBuffComponent globalBuffComponent = _GetGlobalBuffComponent(scene);
            if (globalBuffComponent == null)
            {
                return;
            }
            await globalBuffComponent.AddGlobalBuff_Player(playerId, playerGlobalBuffCfg, actionPlayerContext);
        }

        public static async ETTask AddGlobalBuff_Game(Scene scene, GameGlobalBuffCfg gameGlobalBuffCfg, ActionGameContext actionGameContext)
        {
            GlobalBuffComponent globalBuffComponent = _GetGlobalBuffComponent(scene);
            if (globalBuffComponent == null)
            {
                return;
            }
            await globalBuffComponent.AddGlobalBuff_Game(gameGlobalBuffCfg, actionGameContext);
        }

        // public static List<GlobalBuffUnitObj> GetGlobalBuffList_Unit(Scene scene)
        // {
        //     GlobalBuffComponent globalBuffComponent = _GetGlobalBuffComponent(scene);
        //     if (globalBuffComponent == null)
        //     {
        //         return null;
        //     }
        //     return globalBuffComponent.GetAllBuffList();
        // }

        public static bool ChkIsTrigGlobalBuff(Scene scene, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent)
        {
            GlobalBuffComponent globalBuffComponent = _GetGlobalBuffComponent(scene);
            if (globalBuffComponent == null)
            {
                return false;
            }
            return globalBuffComponent.ChkIsTrigGlobalBuff(abilityGameMonitorTriggerEvent);
        }

        public static void EventHandler(Scene scene, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            GlobalBuffComponent globalBuffComponent = _GetGlobalBuffComponent(scene);
            if (globalBuffComponent == null)
            {
                return;
            }
            globalBuffComponent?.EventHandler(abilityGameMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }

    }
}