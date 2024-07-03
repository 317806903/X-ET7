using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class GlobalBuffHelper
    {
        public static GlobalBuffManagerComponent _GetGlobalBuffManagerComponent(Scene scene)
        {
            GlobalBuffManagerComponent globalBuffManagerComponent = scene.GetComponent<GlobalBuffManagerComponent>();
            if (globalBuffManagerComponent == null)
            {
                return null;
            }
            return globalBuffManagerComponent;
        }

        public static async ETTask AddGlobalBuff(Scene scene, long casterPlayerId, ActionCfg_GlobalBuffAdd actionCfgGlobalBuffAdd, Unit targetUnit,  TeamFlagType teamFlagType)
        {
            if (casterPlayerId == 0 && teamFlagType == TeamFlagType.None)
            {
                Log.Error($"AddGlobalBuff casterPlayerId == 0 && teamFlagType == TeamFlagType.None");
                return;
            }

            if (actionCfgGlobalBuffAdd.GlobalBuffGameCfgId.Count > 0)
            {
                if (teamFlagType == TeamFlagType.None)
                {
                    if (casterPlayerId != 0 && casterPlayerId != -1)
                    {
                        teamFlagType = GamePlayHelper.GetHomeTeamFlagTypeByPlayer(scene, casterPlayerId);
                    }
                }
                foreach (var globalBuffGameCfgId in actionCfgGlobalBuffAdd.GlobalBuffGameCfgId)
                {
                    await AddGlobalBuff_Game(scene, teamFlagType, casterPlayerId, globalBuffGameCfgId);
                }
            }

            if (actionCfgGlobalBuffAdd.GlobalBuffPlayerSelfCfgId.Count > 0)
            {
                foreach (var globalBuffPlayerSelfCfgId in actionCfgGlobalBuffAdd.GlobalBuffPlayerSelfCfgId)
                {
                    await AddGlobalBuff_Player(scene, casterPlayerId, casterPlayerId, globalBuffPlayerSelfCfgId);
                }
            }

            if (actionCfgGlobalBuffAdd.GlobalBuffPlayerAllCfgId.Count > 0)
            {
                GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
                List<long> playerList = gamePlayComponent.GetPlayerList();
                foreach (var globalBuffPlayerSelfCfgId in actionCfgGlobalBuffAdd.GlobalBuffPlayerAllCfgId)
                {
                    foreach (long playerId in playerList)
                    {
                        if (ET.GamePlayHelper.ChkIsFriend(scene, casterPlayerId, playerId))
                        {
                            await AddGlobalBuff_Player(scene, playerId, casterPlayerId, globalBuffPlayerSelfCfgId);
                        }
                    }
                }
            }

            if (actionCfgGlobalBuffAdd.GlobalBuffUnitCfgId.Count > 0)
            {
                if (targetUnit != null)
                {
                    foreach (var globalBuffUnitCfgId in actionCfgGlobalBuffAdd.GlobalBuffUnitCfgId)
                    {
                        await AddGlobalBuff_Unit(targetUnit, casterPlayerId, globalBuffUnitCfgId);
                    }
                }
            }
        }

        public static async ETTask AddGlobalBuff_Unit(Unit targetUnit, long casterPlayerId, string unitGlobalBuffCfgId)
        {
            GlobalBuffManagerComponent globalBuffManagerComponent = _GetGlobalBuffManagerComponent(targetUnit.DomainScene());
            if (globalBuffManagerComponent == null)
            {
                return;
            }
            await globalBuffManagerComponent.AddGlobalBuff_Unit(casterPlayerId, unitGlobalBuffCfgId, targetUnit.Id);
        }

        public static async ETTask AddGlobalBuff_Player(Scene scene, long playerId, long casterPlayerId, string playerGlobalBuffCfgId)
        {
            GlobalBuffManagerComponent globalBuffManagerComponent = _GetGlobalBuffManagerComponent(scene);
            if (globalBuffManagerComponent == null)
            {
                return;
            }
            await globalBuffManagerComponent.AddGlobalBuff_Player(casterPlayerId, playerId, playerGlobalBuffCfgId);
        }

        public static async ETTask AddGlobalBuff_Game(Scene scene, TeamFlagType teamFlagType, long casterPlayerId, string gameGlobalBuffCfgId)
        {
            GlobalBuffManagerComponent globalBuffManagerComponent = _GetGlobalBuffManagerComponent(scene);
            if (globalBuffManagerComponent == null)
            {
                return;
            }
            await globalBuffManagerComponent.AddGlobalBuff_Game(teamFlagType, casterPlayerId, gameGlobalBuffCfgId);
        }

        // public static List<GlobalBuffUnitObj> GetGlobalBuffList_Unit(Scene scene)
        // {
        //     GlobalBuffComponent globalBuffComponent = _GetGlobalBuffManagerComponent(scene);
        //     if (globalBuffComponent == null)
        //     {
        //         return null;
        //     }
        //     return globalBuffComponent.GetAllBuffList();
        // }

        public static void EventHandler(Scene scene, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            GlobalBuffManagerComponent globalBuffManagerComponent = _GetGlobalBuffManagerComponent(scene);
            if (globalBuffManagerComponent == null)
            {
                return;
            }
            globalBuffManagerComponent?.EventHandler(abilityGameMonitorTriggerEvent, ref actionGameContext);
        }

    }
}