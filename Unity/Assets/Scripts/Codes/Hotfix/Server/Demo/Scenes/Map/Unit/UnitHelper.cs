using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof(MoveByPathComponent))]
    [FriendOf(typeof(NumericComponent))]
    [FriendOf(typeof(Unit))]
    public static class UnitHelper
    {
        [Event(SceneType.Map)]
        public class SyncNoticeUnitAdds2C: AEvent<Scene, EventType.SyncNoticeUnitAdds>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncNoticeUnitAdds args)
            {
                Unit beNoticeUnit = args.beNoticeUnit;
                List<Unit> list = args.units;


                M2C_CreateUnits createUnits = new() { Units = new() };
                for (int i = 0; i < list.Count; i++)
                {
                    Unit unit = list[i];
                    if (unit != null)
                    {
                        createUnits.Units.Add(ET.Ability.UnitHelper.CreateUnitInfo(unit));
                    }
                }

                if (createUnits.Units.Count > 0)
                {
                    MessageHelper.SendToClient(beNoticeUnit, createUnits);
                }

                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class SyncNoticeUnitRemoves2C: AEvent<Scene, EventType.SyncNoticeUnitRemoves>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncNoticeUnitRemoves args)
            {
                Unit beNoticeUnit = args.beNoticeUnit;
                List<long> list = args.unitIds;

                M2C_RemoveUnits removeUnits = new() {Units = new()};
                removeUnits.Units.AddRange(list);
                if (removeUnits.Units.Count > 0)
                {
                    MessageHelper.SendToClient(beNoticeUnit, removeUnits);
                }

                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class SyncDataList2C: AEvent<Scene, EventType.SyncDataList>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncDataList args)
            {
                long playerId = args.playerId;
                M2C_SyncDataList _M2C_SyncDataList = new ();
                _M2C_SyncDataList.SyncDataList = args.syncDataList;
                MessageHelper.SendToClient(playerId, _M2C_SyncDataList, scene.InstanceId, true);
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class NoticeGameEndToRoom2R: AEvent<Scene, EventType.NoticeGameEndToRoom>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGameEndToRoom args)
            {
                StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(scene.DomainZone());

                var playerWinResult = args.playerWinResult;
                M2R_NoticeRoomBattleEnd _M2R_NoticeRoomBattleEnd = new M2R_NoticeRoomBattleEnd()
                {
                    RoomId = args.roomId,
                    PlayerId = args.playerId,
                    IsReady = args.isReady?1:0,
                };
                if (playerWinResult != null)
                {
                    _M2R_NoticeRoomBattleEnd.WinPlayers = new();
                    foreach (var item in playerWinResult)
                    {
                        if (item.Value)
                        {
                            _M2R_NoticeRoomBattleEnd.WinPlayers.Add(item.Key);
                        }
                    }
                }

                R2M_NoticeRoomBattleEnd _R2M_NoticeRoomBattleEnd = (R2M_NoticeRoomBattleEnd) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, _M2R_NoticeRoomBattleEnd);

                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class NoticeGameBattleRemovePlayer: AEvent<Scene, EventType.NoticeGameBattleRemovePlayer>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGameBattleRemovePlayer args)
            {
                long playerId = args.playerId;
                await LocationProxyComponent.Instance.RemoveLocation(playerId, LocationType.Unit);
                // Unit unit = ET.Ability.UnitHelper.GetUnit(scene, playerId);
                // if (unit != null)
                // {
                //     await unit.RemoveLocation(LocationType.Unit);
                // }

                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class WaitNoticeGamePlayChg2C: AEvent<Scene, EventType.WaitNoticeGamePlayToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.WaitNoticeGamePlayToClient args)
            {
                long playerId = args.playerId;
                GamePlayComponent gamePlayComponent = args.gamePlayComponent;
                gamePlayComponent.AddWaitNoticeGamePlayToClientList(playerId);
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class WaitNoticeGamePlayPlayerListChg2C: AEvent<Scene, EventType.WaitNoticeGamePlayPlayerListToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.WaitNoticeGamePlayPlayerListToClient args)
            {
                long playerId = args.playerId;
                GamePlayPlayerListComponent gamePlayPlayerListComponent = args.gamePlayPlayerListComponent;
                gamePlayPlayerListComponent.GetGamePlay().AddWaitNoticeGamePlayPlayerListToClientList(playerId);
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class WaitNoticeGamePlayStatisticalChg2C: AEvent<Scene, EventType.WaitNoticeGamePlayStatisticalToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.WaitNoticeGamePlayStatisticalToClient args)
            {
                long playerId = args.playerId;
                GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent = args.gamePlayStatisticalDataComponent;
                gamePlayStatisticalDataComponent.GetGamePlay().AddWaitNoticeGamePlayStatisticalToClientList(playerId);
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class WaitNoticeGamePlayModeChg2C: AEvent<Scene, EventType.WaitNoticeGamePlayModeToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.WaitNoticeGamePlayModeToClient args)
            {
                long playerId = args.playerId;

                GamePlayComponent gamePlayComponent = args.gamePlayComponent;
                gamePlayComponent.AddWaitNoticeGamePlayModeToClientList(playerId);
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class NoticeGameWaitForStart2Server: AEvent<Scene, EventType.NoticeGameWaitForStart2Server>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGameWaitForStart2Server args)
            {
                GamePlayComponent gamePlayComponent = args.gamePlayComponent;
                await gamePlayComponent.GameWaitForStartWhenServer();
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class NoticeGameBegin2Server: AEvent<Scene, EventType.NoticeGameBegin2Server>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGameBegin2Server args)
            {
                GamePlayComponent gamePlayComponent = args.gamePlayComponent;
                await gamePlayComponent.GameBeginWhenServer();
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class NoticeGameEnd2Server: AEvent<Scene, EventType.NoticeGameEnd2Server>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGameEnd2Server args)
            {
                GamePlayComponent gamePlayComponent = args.gamePlayComponent;
                await gamePlayComponent.GameEndWhenServer();
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class NoticeGamePlayChg2C: AEvent<Scene, EventType.NoticeGamePlayToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGamePlayToClient args)
            {
                HashSet<long> playerIds = args.playerIds;
                GamePlayComponent gamePlayComponent = args.gamePlayComponent;
                M2C_GamePlayChgNotice _M2C_GamePlayChgNotice = new ()
                {
                    GamePlayInfo = gamePlayComponent.ToBson(),
                    Components = new(),
                };
                foreach (Entity entity in gamePlayComponent.Components.Values)
                {
                    if (entity is ITransferClient)
                    {
                        _M2C_GamePlayChgNotice.Components.Add(entity.ToBson());
                    }
                }

                bool needWait = args.needSendSuccess;
                foreach (long playerId in playerIds)
                {
                    MessageHelper.SendToClient(playerId, _M2C_GamePlayChgNotice, scene.InstanceId, true, needWait);
                }
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class NoticeGamePlayPlayerListChg2C: AEvent<Scene, EventType.NoticeGamePlayPlayerListToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGamePlayPlayerListToClient args)
            {
                HashSet<long> playerIds = args.playerIds;
                GetCoinType getCoinType = args.getCoinType;
                GamePlayPlayerListComponent gamePlayPlayerListComponent = args.gamePlayPlayerListComponent;
                M2C_GamePlayCoinChgNotice _M2C_GamePlayCoinChgNotice = new ()
                {
                    GetCoinType = (int)getCoinType,
                    GamePlayPlayerListComponent = gamePlayPlayerListComponent.ToBson(),
                };

                bool needWait = args.needSendSuccess;
                foreach (long playerId in playerIds)
                {
                    MessageHelper.SendToClient(playerId, _M2C_GamePlayCoinChgNotice, scene.InstanceId, true, needWait);
                }
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class NoticeGamePlayStatisticalChg2C: AEvent<Scene, EventType.NoticeGamePlayStatisticalToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGamePlayStatisticalToClient args)
            {
                long playerId = args.playerId;
                GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent = args.gamePlayStatisticalDataComponent;
                M2C_GamePlayStatisticalDataChgNotice _M2C_GamePlayStatisticalDataChgNotice = new ()
                {
                    GamePlayStatisticalDataComponent = gamePlayStatisticalDataComponent.ToBson(),
                };

                MessageHelper.SendToClient(playerId, _M2C_GamePlayStatisticalDataChgNotice, scene.InstanceId, true);
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class NoticeGamePlayModeChg2C: AEvent<Scene, EventType.NoticeGamePlayModeToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGamePlayModeToClient args)
            {
                HashSet<long> playerIds = args.playerIds;

                GamePlayModeComponentBase gamePlayModeComponentBase = args.GamePlayModeComponentBase;
                M2C_GamePlayModeChgNotice _M2C_GamePlayModeChgNotice = new ()
                {
                    GamePlayModeInfo = gamePlayModeComponentBase.ToBson(),
                    Components = new(),
                };
                foreach (Entity entity in gamePlayModeComponentBase.Components.Values)
                {
                    if (entity is ITransferClient)
                    {
                        _M2C_GamePlayModeChgNotice.Components.Add(entity.ToBson());
                    }
                }

                bool needWait = args.needSendSuccess;
                foreach (long playerId in playerIds)
                {
                    MessageHelper.SendToClient(playerId, _M2C_GamePlayModeChgNotice, scene.InstanceId, true, needWait);
                }
                await ETTask.CompletedTask;
            }
        }

    }
}