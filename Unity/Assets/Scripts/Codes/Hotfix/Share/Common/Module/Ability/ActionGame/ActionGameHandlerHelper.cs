using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    public static class ActionGameHandlerHelper
    {
        public static void CreateAction(Scene scene, string actionId, float delayTime, ref ActionGameContext actionGameContext)
        {
            scene.GetComponent<ActionGameHandlerComponent>().Run(actionId, delayTime, actionGameContext).Coroutine();
        }

        public static void AddGameAction(Scene scene, long playerId, ActionCfg_GlobalBuffAddImmediately _ActionCfg_GlobalBuffAddImmediately, Unit targetUnit)
        {
            TeamFlagType teamFlagType = TeamFlagType.None;
            if (targetUnit != null)
            {
                teamFlagType = TeamFlagHelper.GetTeamFlag(targetUnit);
                if (playerId == (long)ET.PlayerId.PlayerNone)
                {
                    playerId = TeamFlagHelper.GetPlayerId(targetUnit);
                }
            }

            if (_ActionCfg_GlobalBuffAddImmediately.GameActionHomeTeamCfgId.Count > 0)
            {
                ActionGameContext actionGameContextNew = new();
                actionGameContextNew.playerId = (long)ET.PlayerId.PlayerNone;
                if (teamFlagType != TeamFlagType.None)
                {
                    actionGameContextNew.teamFlagType = GamePlayHelper.GetHomeTeamFlagType(teamFlagType);
                }

                foreach (var actionGameId in _ActionCfg_GlobalBuffAddImmediately.GameActionHomeTeamCfgId)
                {
                    ActionGameHandlerHelper.CreateAction(scene, actionGameId, 0, ref actionGameContextNew);
                }
            }

            if (_ActionCfg_GlobalBuffAddImmediately.GameActionPlayerSelfCfgId.Count > 0)
            {
                if (playerId != (long)ET.PlayerId.PlayerNone)
                {
                    ActionGameContext actionGameContextNew = new();
                    actionGameContextNew.playerId = playerId;
                    actionGameContextNew.teamFlagType = TeamFlagType.None;
                    foreach (var actionGameId in _ActionCfg_GlobalBuffAddImmediately.GameActionPlayerSelfCfgId)
                    {
                        ActionGameHandlerHelper.CreateAction(scene, actionGameId, 0, ref actionGameContextNew);
                    }
                }
            }

            if (_ActionCfg_GlobalBuffAddImmediately.GameActionPlayerAllCfgId.Count > 0)
            {
                GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
                List<long> playerList = gamePlayComponent.GetPlayerList();

                bool isNeedChkFriend = true;
                if (playerId == (long)ET.PlayerId.PlayerNone)
                {
                    isNeedChkFriend = false;
                }
                foreach (long playerIdTmp in playerList)
                {
                    if (isNeedChkFriend == false
                        || ET.GamePlayHelper.ChkIsFriend(scene, playerId, playerIdTmp))
                    {
                        ActionGameContext actionGameContextNew = new();
                        actionGameContextNew.playerId = playerIdTmp;
                        actionGameContextNew.teamFlagType = TeamFlagType.None;
                        foreach (var actionGameId in _ActionCfg_GlobalBuffAddImmediately.GameActionPlayerAllCfgId)
                        {
                            ActionGameHandlerHelper.CreateAction(scene, actionGameId, 0, ref actionGameContextNew);
                        }
                    }
                }
            }

            if (_ActionCfg_GlobalBuffAddImmediately.GameActionUnitCfgId.Count > 0)
            {
                if (targetUnit != null)
                {
                    ActionGameContext actionGameContextNew = new();
                    actionGameContextNew.playerId = (long)ET.PlayerId.PlayerNone;
                    actionGameContextNew.teamFlagType = TeamFlagType.None;
                    actionGameContextNew.unitId = targetUnit.Id;
                    foreach (var actionGameId in _ActionCfg_GlobalBuffAddImmediately.GameActionUnitCfgId)
                    {
                        ActionGameHandlerHelper.CreateAction(scene, actionGameId, 0, ref actionGameContextNew);
                    }
                }
            }
        }

    }
}