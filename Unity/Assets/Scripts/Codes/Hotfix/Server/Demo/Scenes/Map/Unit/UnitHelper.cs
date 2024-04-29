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
        public class SyncUnitEffects2C: AEvent<Scene, EventType.SyncUnitEffects>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncUnitEffects args)
            {
                Unit unit = args.unit;
                AOIEntity aoiEntity = unit.GetComponent<AOIEntity>();
                while (aoiEntity == null)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (unit == null || unit.IsDisposed)
                    {
                        return;
                    }
                    aoiEntity = unit.GetComponent<AOIEntity>();
                }
                if (aoiEntity.IsDisposed)
                {
                    return;
                }
                while (aoiEntity.bInit == false)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (unit.IsDisposed || aoiEntity.IsDisposed)
                    {
                        return;
                    }
                }

                bool isAddEffect = args.isAddEffect;
                long effectObjId = args.effectObjId;
                ET.Ability.EffectObj effectObj = args.effectObj;
                bool isOnlySelfShow = args.isOnlySelfShow;
                if (isAddEffect == false)
                {
                    isOnlySelfShow = false;
                }
                if (isOnlySelfShow)
                {
                    long playerId = ET.GamePlayHelper.GetPlayerIdByUnitId(unit);
                    if (playerId != -1)
                    {
                        M2C_SyncUnitEffects SyncUnitEffects = new ();
                        SyncUnitEffects.UnitId = unit.Id;
                        SyncUnitEffects.AddOrRemove = isAddEffect?0:1;
                        if (isAddEffect)
                        {
                            SyncUnitEffects.EffectComponent = effectObj.ToBson();
                        }
                        else
                        {
                            SyncUnitEffects.EffectObjId = effectObjId;
                        }

                        if (ET.Ability.UnitHelper.ChkIsSceneEffect(unit) && isAddEffect)
                        {
                            await TimerComponent.Instance.WaitFrameAsync();
                        }
                        MessageHelper.SendToClient(playerId, SyncUnitEffects, scene.InstanceId);
                    }
                }
                else
                {
                    M2C_SyncUnitEffects SyncUnitEffects = new ();
                    SyncUnitEffects.UnitId = unit.Id;
                    SyncUnitEffects.AddOrRemove = isAddEffect?0:1;
                    if (isAddEffect)
                    {
                        SyncUnitEffects.EffectComponent = effectObj.ToBson();
                    }
                    else
                    {
                        SyncUnitEffects.EffectObjId = effectObjId;
                    }

                    if (ET.Ability.UnitHelper.ChkIsSceneEffect(unit) && isAddEffect)
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                    }
                    MessageHelper.Broadcast(unit, SyncUnitEffects);
                }
                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class SyncGetCoinShow2C: AEvent<Scene, EventType.SyncGetCoinShow>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncGetCoinShow args)
            {
                long playerId = args.playerId;
                Unit unit = args.unit;
                CoinType coinType = args.coinType;
                int chgValue = args.chgValue;
                if (playerId != -1)
                {
                    M2C_SyncGetCoinShow _M2C_SyncGetCoinShow = new ();
                    _M2C_SyncGetCoinShow.UnitId = unit.Id;
                    _M2C_SyncGetCoinShow.CoinType = (int)coinType;
                    _M2C_SyncGetCoinShow.ChgValue = chgValue;

                    MessageHelper.SendToClient(playerId, _M2C_SyncGetCoinShow, scene.InstanceId);
                }

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

                GamePlayModeComponent gamePlayModeComponent = args.gamePlayModeComponent;
                M2C_GamePlayModeChgNotice _M2C_GamePlayModeChgNotice = new ()
                {
                    GamePlayModeInfo = gamePlayModeComponent.ToBson(),
                    Components = new(),
                };
                foreach (Entity entity in gamePlayModeComponent.Components.Values)
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