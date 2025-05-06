using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    // [Invoke(TimerInvokeType.GamePlayChkTimer)]
    // public class GamePlayComponentTimer: ATimer<GamePlayComponent>
    // {
    // 	protected override void Run(GamePlayComponent self)
    // 	{
    // 		try
    // 		{
    // 			self.Update();
    // 		}
    // 		catch (Exception e)
    // 		{
    // 			Log.Error($"move timer error: {self.Id}\n{e}");
    // 		}
    // 	}
    // }

    [FriendOf(typeof (GamePlayComponent))]
    public static class GamePlayComponentSystem
    {
        [ObjectSystem]
        public class GamePlayComponentAwakeSystem: AwakeSystem<GamePlayComponent>
        {
            protected override void Awake(GamePlayComponent self)
            {
                //self.Timer = TimerComponent.Instance.NewRepeatedTimer(5000, TimerInvokeType.GamePlayChkTimer, self);
                self.waitNoticeGamePlayToClientList = new();
                self.waitNoticeGamePlayModeToClientList = new();
                self.waitNoticeGamePlayModeToClientListForceSend = new();
                self.waitNoticeGamePlayPlayerListToClientList = new();
                self.waitNoticeGamePlayStatisticalToClientList = new();
            }
        }

        [ObjectSystem]
        public class GamePlayComponentDestroySystem: DestroySystem<GamePlayComponent>
        {
            protected override void Destroy(GamePlayComponent self)
            {
                //TimerComponent.Instance?.Remove(ref self.Timer);
                self.waitNoticeGamePlayToClientList?.Clear();
                self.waitNoticeGamePlayModeToClientList?.Clear();
                self.waitNoticeGamePlayModeToClientListForceSend?.Clear();
                self.waitNoticeGamePlayPlayerListToClientList?.Clear();
                self.waitNoticeGamePlayStatisticalToClientList?.Clear();
            }
        }

        [ObjectSystem]
        public class GamePlayComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayComponent>
        {
            protected override void FixedUpdate(GamePlayComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this GamePlayComponent self, float fixedDeltaTime)
        {
            self.ChkNeedNoticeClient().Coroutine();

            if (++self.curFrameChk >= self.waitFrameChk)
            {
                self.curFrameChk = 0;

                bool willDestroy = self.ChkGamePlayWaitDestroy();
                if (willDestroy == false)
                {
                    self.ChkPlayerWaitDestroy().Coroutine();
                }
            }

            if (++self.curFrameSyncPos >= self.waitFrameSyncPos)
            {
                self.curFrameSyncPos = 0;

                self.ResetObserverUnitPos();
            }
        }

        public static void AddWaitNoticeGamePlayToClientList(this GamePlayComponent self, long playerId)
        {
            if (self.waitNoticeGamePlayToClientList.Contains(playerId))
            {
                return;
            }

            self.waitNoticeGamePlayToClientList.Add(playerId);
        }

        public static void AddWaitNoticeGamePlayModeToClientList(this GamePlayComponent self, long playerId, bool bForceSend)
        {
            if (bForceSend)
            {
                if (self.waitNoticeGamePlayModeToClientListForceSend.Contains(playerId))
                {
                    return;
                }

                self.waitNoticeGamePlayModeToClientListForceSend.Add(playerId);
            }
            else
            {
                if (self.waitNoticeGamePlayModeToClientList.Contains(playerId))
                {
                    return;
                }

                self.waitNoticeGamePlayModeToClientList.Add(playerId);
            }
        }

        public static void AddWaitNoticeGamePlayPlayerListToClientList(this GamePlayComponent self, long playerId)
        {
            if (self.waitNoticeGamePlayPlayerListToClientList.Contains(playerId))
            {
                return;
            }

            self.waitNoticeGamePlayPlayerListToClientList.Add(playerId);
        }

        public static void AddWaitNoticeGamePlayStatisticalToClientList(this GamePlayComponent self, long playerId)
        {
            if (self.waitNoticeGamePlayStatisticalToClientList.Contains(playerId))
            {
                return;
            }

            self.waitNoticeGamePlayStatisticalToClientList.Add(playerId);
        }

        public static async ETTask<bool> ChkPlayerConnected(this GamePlayComponent self)
        {
            if (self.waitNoticeGamePlayToClientList.Count > 0)
            {
                if (self.isChkPlayerConnect)
                {
                    return false;
                }

                if (self.isFirstSendGamePlayToClient == false)
                {
                    self.isChkPlayerConnect = true;
                    foreach (long playerId in self.waitNoticeGamePlayToClientList)
                    {
                        bool bRet = await ET.Server.MessageHelper.ChkPlayerConnected(playerId);
                        if (bRet == false)
                        {
                            return false;
                        }
                    }

                    self.isChkPlayerConnect = false;
                }
            }
            if (self.waitNoticeGamePlayPlayerListToClientList.Count > 0)
            {
                if (self.isChkPlayerConnect)
                {
                    return false;
                }

                if (self.isFirstSendGamePlayPlayerListToClient == false)
                {
                    self.isChkPlayerConnect = true;
                    foreach (long playerId in self.waitNoticeGamePlayPlayerListToClientList)
                    {
                        bool bRet = await ET.Server.MessageHelper.ChkPlayerConnected(playerId);
                        if (bRet == false)
                        {
                            return false;
                        }
                    }

                    self.isChkPlayerConnect = false;
                }
            }
            if (self.waitNoticeGamePlayModeToClientList.Count > 0)
            {
                if (self.isChkPlayerConnect)
                {
                    return false;
                }

                if (self.isFirstSendGamePlayModeToClient == false)
                {
                    self.isChkPlayerConnect = true;
                    foreach (long playerId in self.waitNoticeGamePlayModeToClientList)
                    {
                        bool bRet = await ET.Server.MessageHelper.ChkPlayerConnected(playerId);
                        if (bRet == false)
                        {
                            return false;
                        }
                    }

                    self.isChkPlayerConnect = false;
                }
            }
            return true;
        }

        public static async ETTask ChkNeedNoticeClient(this GamePlayComponent self)
        {
            bool bRet = await self.ChkPlayerConnected();
            if (bRet == false)
            {
                return;
            }

            while (self.waitNoticeGamePlayToClientList.Count > 0)
            {
                try
                {
                    bool needSendSuccess = false;
                    if (self.isFirstSendGamePlayToClient == false)
                    {
                        self.isFirstSendGamePlayToClient = true;
                        needSendSuccess = true;
                    }

                    EventType.NoticeGamePlayToClient _NoticeGamePlayToClient = new()
                    {
                        playerIds = self.waitNoticeGamePlayToClientList, gamePlayComponent = self, needSendSuccess = needSendSuccess,
                    };
                    EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayToClient);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                self.waitNoticeGamePlayToClientList.Clear();
            }

            while (self.waitNoticeGamePlayPlayerListToClientList.Count > 0)
            {
                try
                {
                    bool needSendSuccess = false;
                    if (self.isFirstSendGamePlayPlayerListToClient == false)
                    {
                        self.isFirstSendGamePlayPlayerListToClient = true;
                        needSendSuccess = true;
                    }

                    EventType.NoticeGamePlayPlayerListToClient _NoticeGamePlayPlayerListToClient = new()
                    {
                        playerIds = self.waitNoticeGamePlayPlayerListToClientList,
                        getCoinType = GetCoinType.Normal,
                        gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>(),
                        needSendSuccess = needSendSuccess,
                    };
                    EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayPlayerListToClient);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                self.waitNoticeGamePlayPlayerListToClientList.Clear();
            }

            while (self.waitNoticeGamePlayStatisticalToClientList.Count > 0)
            {
                try
                {
                    foreach (long playerId in self.waitNoticeGamePlayStatisticalToClientList)
                    {
                        EventType.NoticeGamePlayStatisticalToClient _NoticeGamePlayStatisticalToClient = new()
                        {
                            playerId = playerId,
                            gamePlayStatisticalDataComponent = self.GetComponent<GamePlayStatisticalDataManagerComponent>(). GetGamePlayStatisticalData(playerId),
                        };
                        EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayStatisticalToClient);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                self.waitNoticeGamePlayStatisticalToClientList.Clear();
            }

            while (self.waitNoticeGamePlayModeToClientList.Count > 0)
            {
                try
                {
                    bool needSendSuccess = false;
                    if (self.isFirstSendGamePlayModeToClient == false)
                    {
                        self.isFirstSendGamePlayModeToClient = true;
                        needSendSuccess = true;
                    }

                    EventType.NoticeGamePlayModeToClient _NoticeGamePlayModeToClient = new()
                    {
                        playerIds = self.waitNoticeGamePlayModeToClientList,
                        GamePlayModeComponentBase = self.GetGamePlayMode(),
                        needSendSuccess = needSendSuccess,
                    };
                    EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayModeToClient);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                self.waitNoticeGamePlayModeToClientList.Clear();
            }

            while (self.waitNoticeGamePlayModeToClientListForceSend.Count > 0)
            {
                try
                {
                    EventType.NoticeGamePlayModeToClient _NoticeGamePlayModeToClient = new()
                    {
                        playerIds = self.waitNoticeGamePlayModeToClientListForceSend,
                        GamePlayModeComponentBase = self.GetGamePlayMode(),
                        needSendSuccess = true,
                    };
                    EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayModeToClient);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                self.waitNoticeGamePlayModeToClientListForceSend.Clear();
            }
        }

        public static bool ChkGamePlayWaitDestroy(this GamePlayComponent self)
        {
            if (self.gamePlayWaitDestroyTime == 0)
            {
                if (self.ChkIsNeedDestroy())
                {
                    self.gamePlayWaitDestroyTime = TimeHelper.ServerNow() + 5000;
                    return true;
                }

                return false;
            }
            else if (self.gamePlayWaitDestroyTime < TimeHelper.ServerNow())
            {
                self.gamePlayWaitDestroyTime = TimeHelper.ServerNow() + 5000;
                self.TrigDestroyGamePlay().Coroutine();
            }

            return true;
        }

        public static async ETTask TrigDestroyGamePlay(this GamePlayComponent self)
        {
            long dynamicMapInstanceId = self.dynamicMapInstanceId;
            DynamicMapManagerComponent dynamicMapManagerComponent = self.GetParent<Scene>().GetParent<DynamicMapManagerComponent>();
            if (dynamicMapManagerComponent != null)
            {
                await dynamicMapManagerComponent.DestroyDynamicMap(dynamicMapInstanceId);
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask TrigDestroyPlayer(this GamePlayComponent self, long playerId)
        {
            StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(self.DomainZone());

            try
            {
                ActorMessageSenderComponent.Instance.Send(roomSceneConfig.InstanceId,
                        new M2R_MemberQuitRoom() { PlayerId = playerId, RoomId = self.roomId, });
            }
            catch (Exception e)
            {
                Log.Error($"ET.Server.GamePlayComponentSystem.TrigDestroyPlayer {e}");
            }

            self.PlayerQuitBattle(playerId, true);
            await ETTask.CompletedTask;
        }

        public static bool ChkIsNeedDestroy(this GamePlayComponent self)
        {
            // if (self.ChkIsGameEnd())
            // {
            // 	return true;
            // }
            if (self.GetPlayerList().Count == 0)
            {
                return true;
            }

            return false;
        }

        public static async ETTask ChkPlayerWaitDestroy(this GamePlayComponent self)
        {
            List<long> playerList = self.GetPlayerList();
            if (playerList == null)
            {
                return;
            }
            PlayerLocationChkComponent playerLocationChkComponent = self.GetComponent<PlayerLocationChkComponent>();
            if (playerLocationChkComponent == null)
            {
                return;
            }
            playerLocationChkComponent.SetChkPlayerOfflineList(playerList);

            HashSet<long> notExistPlayerList = playerLocationChkComponent.GetPlayerOfflineList();
            foreach (long playerId in notExistPlayerList)
            {
                if (self.IsDisposed)
                {
                    return;
                }

                if (self.ChkPlayerIsQuit(playerId) == false)
                {
                    await self.TrigDestroyPlayer(playerId);
                }
            }
        }

        public static void ResetObserverUnitPos(this GamePlayComponent self)
        {
            if (self.gamePlayStatus == GamePlayStatus.GameEnd)
            {
                return;
            }

            List<long> playerList = self.GetPlayerList();
            if (playerList == null)
            {
                return;
            }

            foreach (long playerId in playerList)
            {
                List<Unit> playerUnitList = self.GetPlayerUnitList(playerId);
                if (playerUnitList != null)
                {
                    foreach (Unit playerUnit in playerUnitList)
                    {
                        if (playerUnit != null)
                        {
                            Unit observerUnit = Ability.UnitHelper.GetUnit(self.DomainScene(), playerId);
                            observerUnit.Position = playerUnit.Position;
                        }
                    }
                }
            }
        }

        public static async ETTask GameWaitForStartWhenServer(this GamePlayComponent self)
        {
            if (self.gamePlayStatus != GamePlayStatus.WaitForStart)
            {
                return;
            }

            bool bRet = await self.ChkPlayerExist();
            if (bRet == false)
            {
                return;
            }
            await self.AddPlayerSeasonBringUp();
            await self.AddGamePlayGlobalBuffAddList();
            self.AddGamePlayTowerDefenseGlobalBuffAddList().Coroutine();
            self.AddGamePlayPKGlobalBuffAddList().Coroutine();
        }

        public static async ETTask AddPlayerSeasonBringUp(this GamePlayComponent self)
        {
            List<long> playerList = self.GetPlayerList();
            if (playerList == null)
            {
                return;
            }

            foreach (long playerId in playerList)
            {
                PlayerSeasonInfoComponent playerSeasonInfoComponent = await PlayerCacheHelper.GetPlayerSeasonInfoByPlayerId(self.DomainScene(), playerId, true);
                if (self.IsDisposed)
                {
                    return;
                }
                foreach (var item in playerSeasonInfoComponent.GetSeasonBringUpDic())
                {
                    string bringUpCfgId = item.Key;
                    int level = item.Value;
                    SeasonBringUpCfg seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetSeasonBringUpCfg(bringUpCfgId, level);
                    foreach (var actionCfgGlobalBuffAddCfgId in seasonBringUpCfg.BuffList)
                    {
                        await GlobalBuffHelper.AddGlobalBuff(self.DomainScene(), playerId, actionCfgGlobalBuffAddCfgId, null, TeamFlagType.None);
                        if (self.IsDisposed)
                        {
                            return;
                        }
                    }
                }
            }
        }

        public static async ETTask AddGamePlayGlobalBuffAddList(this GamePlayComponent self)
        {
            long playerId = (long)ET.PlayerId.PlayerNone;
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
            foreach (var actionCfgGlobalBuffAddCfgId in gamePlayBattleLevelCfg.GlobalBuffAddList)
            {
                await GlobalBuffHelper.AddGlobalBuff(self.DomainScene(), playerId, actionCfgGlobalBuffAddCfgId, null, TeamFlagType.TeamGlobal);
                if (self.IsDisposed)
                {
                    return;
                }
            }
            //
            // string actionCfgGlobalBuffAddCfgId = "GlobalBuffAdd_TestProps;GlobalBuffAdd_TestWeather_TeamGlobal;GlobalBuffAdd_TestSynergy";
            // await GlobalBuffHelper.AddGlobalBuff(self.DomainScene(), playerId, actionCfgGlobalBuffAddCfgId, null, TeamFlagType.TeamGlobal);
            // if (self.IsDisposed)
            // {
            //     return;
            // }
        }

        public static async ETTask AddGamePlayTowerDefenseGlobalBuffAddList(this GamePlayComponent self)
        {
            while (self.GetGamePlayMode() == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
                    return;
                }
            }
            long playerId = (long)ET.PlayerId.PlayerNone;

            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayMode() as GamePlayTowerDefenseComponent;

                foreach (var actionCfgGlobalBuffAddCfgId in gamePlayTowerDefenseComponent.model.GlobalBuffAddList)
                {
                    await GlobalBuffHelper.AddGlobalBuff(self.DomainScene(), playerId, actionCfgGlobalBuffAddCfgId, null, TeamFlagType.TeamGlobal);
                    if (self.IsDisposed)
                    {
                        return;
                    }
                }
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
            }
        }

        public static async ETTask AddGamePlayPKGlobalBuffAddList(this GamePlayComponent self)
        {
            while (self.GetGamePlayMode() == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
                    return;
                }
            }
            long playerId = (long)ET.PlayerId.PlayerNone;

            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                GamePlayPkComponent gamePlayPkComponent = self.GetGamePlayMode() as GamePlayPkComponent;

                foreach (var actionCfgGlobalBuffAddCfgId in gamePlayPkComponent.model.GlobalBuffAddList)
                {
                    await GlobalBuffHelper.AddGlobalBuff(self.DomainScene(), playerId, actionCfgGlobalBuffAddCfgId, null, TeamFlagType.TeamGlobal);
                    if (self.IsDisposed)
                    {
                        return;
                    }
                }
            }
        }

        public static async ETTask GameBeginWhenServer(this GamePlayComponent self)
        {
            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayMode() as GamePlayTowerDefenseComponent;

                await gamePlayTowerDefenseComponent.GameBeginWhenServer();
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                GamePlayPkComponent gamePlayPkComponent = self.GetGamePlayMode() as GamePlayPkComponent;

                await gamePlayPkComponent.GameBeginWhenServer();
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer(this GamePlayComponent self)
        {
            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayMode() as GamePlayTowerDefenseComponent;

                await gamePlayTowerDefenseComponent.GameEndWhenServer();
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                GamePlayPkComponent gamePlayPkComponent = self.GetGamePlayMode() as GamePlayPkComponent;

                await gamePlayPkComponent.GameEndWhenServer();
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask GameRecoverWhenServer(this GamePlayComponent self, long playerId)
        {
            if (self.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayMode() as GamePlayTowerDefenseComponent;

                await gamePlayTowerDefenseComponent.GameRecoverWhenServer(playerId);
            }
            else if (self.gamePlayMode == GamePlayMode.PK)
            {
                GamePlayPkComponent gamePlayPkComponent = self.GetGamePlayMode() as GamePlayPkComponent;

                await gamePlayPkComponent.GameRecoverWhenServer(playerId);
            }

            await ETTask.CompletedTask;
        }
    }
}