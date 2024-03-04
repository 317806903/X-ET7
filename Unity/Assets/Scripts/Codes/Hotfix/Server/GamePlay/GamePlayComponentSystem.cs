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
            self.ChkNeedNoticeClient();

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

        public static void AddWaitNoticeGamePlayModeToClientList(this GamePlayComponent self, long playerId)
        {
            if (self.waitNoticeGamePlayModeToClientList.Contains(playerId))
            {
                return;
            }

            self.waitNoticeGamePlayModeToClientList.Add(playerId);
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

        public static void ChkNeedNoticeClient(this GamePlayComponent self)
        {
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
                        gamePlayModeComponent = self.GetGamePlayMode(),
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
                R2M_MemberQuitRoom _R2M_MemberQuitRoom =
                    (R2M_MemberQuitRoom)await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId,
                        new M2R_MemberQuitRoom() { PlayerId = playerId, RoomId = self.roomId, });
            }
            catch (Exception e)
            {
                Log.Error($"ET.Server.GamePlayComponentSystem.TrigDestroyPlayer {e}");
            }

            Unit unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), playerId);
            if (unit != null)
            {
                await unit.RemoveLocation(LocationType.Unit);
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
            await ETTask.CompletedTask;
            List<long> playerList = self.GetPlayerList();
            if (playerList == null)
            {
                return;
            }
            if (self.playerWaitQuitTime == null)
            {
                self.playerWaitQuitTime = new();
            }

            if (self.playerListTmp == null)
            {
                self.playerListTmp = new();
            }
            else
            {
                if (self.playerListTmp.Count > 0)
                {
                    return;
                }
            }

            for (int i = 0; i < playerList.Count; i++)
            {
                self.playerListTmp.Add(playerList[i]);
            }

            //ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
            foreach (long playerId in self.playerListTmp)
            {
                long locationActorId = await LocationProxyComponent.Instance.Get(LocationType.Player, playerId, self.DomainScene().InstanceId);
                if (self.IsDisposed)
                {
                    Log.Error($" ChkPlayerWaitDestroy self.IsDisposed==true ");
                    return;
                }
                if (locationActorId == 0)
                {
                    if (self.playerWaitQuitTime.ContainsKey(playerId) == false)
                    {
                        self.playerWaitQuitTime[playerId] = TimeHelper.ServerNow() + 5000;
                    }
                }
                else
                {
                    if (self.playerWaitQuitTime.ContainsKey(playerId))
                    {
                        self.playerWaitQuitTime.Remove(playerId);
                    }
                }
                // if (oneTypeLocationType.GetChild<Entity>(playerId) == null)
                // {
                //     if (self.playerWaitQuitTime.ContainsKey(playerId) == false)
                //     {
                //         self.playerWaitQuitTime[playerId] = TimeHelper.ServerNow() + 5000;
                //     }
                // }
                // else
                // {
                //     if (self.playerWaitQuitTime.ContainsKey(playerId))
                //     {
                //         self.playerWaitQuitTime.Remove(playerId);
                //     }
                // }
            }

            self.playerListTmp.Clear();

            while (true)
            {
                bool bWhile = false;
                foreach (var playerWaitQuitTime in self.playerWaitQuitTime)
                {
                    long playerId = playerWaitQuitTime.Key;
                    long time = playerWaitQuitTime.Value;
                    if (time != -1 && time < TimeHelper.ServerNow())
                    {
                        self.playerWaitQuitTime[playerId] = -1;
                        bWhile = true;

                        await self.TrigDestroyPlayer(playerId);
                        break;
                    }
                }

                if (bWhile == false)
                {
                    break;
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
                Unit playerUnit = self.GetPlayerUnit(playerId);
                if (playerUnit != null)
                {
                    Unit observerUnit = Ability.UnitHelper.GetUnit(self.DomainScene(), playerId);
                    observerUnit.Position = playerUnit.Position;
                }
            }
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
                GamePlayPKComponent gamePlayPKComponent = self.GetGamePlayMode() as GamePlayPKComponent;

                await gamePlayPKComponent.GameEndWhenServer();
            }

            await ETTask.CompletedTask;
        }
    }
}