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

        public static async ETTask AddGlobalBuff(Scene scene, long casterPlayerId, string actionCfgGlobalBuffAddCfgId, Unit targetUnit,  TeamFlagType teamFlagType)
        {
            // if (casterPlayerId == (long)ET.PlayerId.PlayerNone && teamFlagType == TeamFlagType.None)
            // {
            //     Log.Error($"AddGlobalBuff casterPlayerId == {(long)ET.PlayerId.PlayerNone} && teamFlagType == TeamFlagType.None");
            //     return;
            // }
            if (ActionCfg_GlobalBuffAddCategory.Instance.Contain(actionCfgGlobalBuffAddCfgId) == false)
            {
                return;
            }
            ActionCfg_GlobalBuffAdd actionCfgGlobalBuffAdd = ActionCfg_GlobalBuffAddCategory.Instance.Get(actionCfgGlobalBuffAddCfgId);

            if (actionCfgGlobalBuffAdd.GlobalBuffHomeTeamCfgId.Count > 0)
            {
                if (teamFlagType == TeamFlagType.None)
                {
                    if (casterPlayerId != (long)ET.PlayerId.PlayerNone)
                    {
                        teamFlagType = GamePlayHelper.GetHomeTeamFlagTypeByPlayer(scene, casterPlayerId);
                    }
                }

                if (teamFlagType != TeamFlagType.None)
                {
                    foreach (var globalBuffHomeTeamCfgId in actionCfgGlobalBuffAdd.GlobalBuffHomeTeamCfgId)
                    {
                        await AddGlobalBuff_Game(scene, teamFlagType, casterPlayerId, globalBuffHomeTeamCfgId);
                    }
                }
            }

            if (actionCfgGlobalBuffAdd.GlobalBuffPlayerSelfCfgId.Count > 0)
            {
                if (casterPlayerId != (long)ET.PlayerId.PlayerNone)
                {
                    foreach (var globalBuffPlayerSelfCfgId in actionCfgGlobalBuffAdd.GlobalBuffPlayerSelfCfgId)
                    {
                        await AddGlobalBuff_Player(scene, casterPlayerId, casterPlayerId, globalBuffPlayerSelfCfgId);
                    }
                }
            }

            if (actionCfgGlobalBuffAdd.GlobalBuffPlayerAllCfgId.Count > 0)
            {
                GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
                List<long> playerList = gamePlayComponent.GetPlayerList();

                bool isNeedChkFriend = true;
                if (casterPlayerId == (long)ET.PlayerId.PlayerNone)
                {
                    isNeedChkFriend = false;
                }
                foreach (var globalBuffPlayerCfgId in actionCfgGlobalBuffAdd.GlobalBuffPlayerAllCfgId)
                {
                    foreach (long playerIdTmp in playerList)
                    {
                        if (isNeedChkFriend == false
                            || ET.GamePlayHelper.ChkIsFriend(scene, casterPlayerId, playerIdTmp))
                        {
                            await AddGlobalBuff_Player(scene, playerIdTmp, casterPlayerId, globalBuffPlayerCfgId);
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
            await globalBuffManagerComponent.AddGlobalBuff_Player(playerId, casterPlayerId, playerGlobalBuffCfgId);
        }

        public static async ETTask AddGlobalBuff_Game(Scene scene, TeamFlagType teamFlagType, long casterPlayerId, string homeTeamGlobalBuffCfgId)
        {
            GlobalBuffManagerComponent globalBuffManagerComponent = _GetGlobalBuffManagerComponent(scene);
            if (globalBuffManagerComponent == null)
            {
                return;
            }
            await globalBuffManagerComponent.AddGlobalBuff_Game(teamFlagType, casterPlayerId, homeTeamGlobalBuffCfgId);
        }

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