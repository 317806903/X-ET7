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
        static Dictionary<Unit, UnitPosInfo> tmpPosInfoDic = new();
        static Dictionary<Unit, UnitNumericInfo> tmpNumericDic = new();

        [Event(SceneType.Map)]
        public class SyncPosUnitInfo2C: AEvent<Scene, EventType.SyncPosUnits>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncPosUnits args)
            {
                List<Unit> list = args.units;
                tmpPosInfoDic.Clear();
                Dictionary<Unit, UnitPosInfo> unitPosInfos = tmpPosInfoDic;
                foreach (Unit unit in list)
                {
                    unitPosInfos.Add(unit, ET.Ability.UnitHelper.SyncPosUnitInfo(unit));
                }

                MultiMapSimple<long, Unit> playerSeeUnits = MessageHelper.GetUnitBeSeePlayers(list);
                foreach (var playerSeeUnit in playerSeeUnits)
                {
                    long playerId = playerSeeUnit.Key;
                    M2C_SyncPosUnits syncPosUnits = new ()
                    {
                        Units = new(),
                    };
                    foreach (Unit unitChg in playerSeeUnit.Value)
                    {
                        syncPosUnits.Units.Add(unitPosInfos[unitChg]);
                    }
                    MessageHelper.SendToClient(playerId, syncPosUnits, true);
                }

                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class SyncNumericUnitInfo2C: AEvent<Scene, EventType.SyncNumericUnits>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncNumericUnits args)
            {
                List<Unit> list = args.units;
                tmpNumericDic.Clear();
                Dictionary<Unit, UnitNumericInfo> unitNumericInfos = tmpNumericDic;
                foreach (Unit unit in list)
                {
                    unitNumericInfos.Add(unit, ET.Ability.UnitHelper.SyncNumericUnitInfo(unit));
                }

                MultiMapSimple<long, Unit> playerSeeUnits = MessageHelper.GetUnitBeSeePlayers(list);
                foreach (var playerSeeUnit in playerSeeUnits)
                {
                    long playerId = playerSeeUnit.Key;
                    M2C_SyncNumericUnits syncNumericUnits = new ()
                    {
                        Units = new(),
                    };
                    foreach (Unit unitChg in playerSeeUnit.Value)
                    {
                        syncNumericUnits.Units.Add(unitNumericInfos[unitChg]);
                    }
                    MessageHelper.SendToClient(playerId, syncNumericUnits, true);
                }

                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class SyncNumericUnitKeyInfo2C: AEvent<Scene, EventType.SyncNumericUnitsKey>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncNumericUnitsKey args)
            {
                List<Unit> list = args.units;
                tmpNumericDic.Clear();
                Dictionary<Unit, UnitNumericInfo> unitNumericInfos = tmpNumericDic;
                for (int i = 0; i < list.Count; i++)
                {
                    Unit unit = list[i];
                    List<int> keys = args.keys[i];
                    unitNumericInfos.Add(unit, ET.Ability.UnitHelper.SyncNumericUnitInfoKey(unit, keys));
                }
                foreach (Unit unit in list)
                {
                }

                MultiMapSimple<long, Unit> playerSeeUnits = MessageHelper.GetUnitBeSeePlayers(list);
                foreach (var playerSeeUnit in playerSeeUnits)
                {
                    long playerId = playerSeeUnit.Key;
                    M2C_SyncNumericUnits syncNumericUnitsKey = new ()
                    {
                        Units = new(),
                    };
                    foreach (Unit unitChg in playerSeeUnit.Value)
                    {
                        syncNumericUnitsKey.Units.Add(unitNumericInfos[unitChg]);
                    }
                    MessageHelper.SendToClient(playerId, syncNumericUnitsKey, true);
                }

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
                        MessageHelper.SendToClient(playerId, SyncUnitEffects);
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
        public class SyncPlayAudio2C: AEvent<Scene, EventType.SyncPlayAudio>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncPlayAudio args)
            {
                Unit unit = args.unit;
                string playAudioActionId = args.playAudioActionId;
                bool isOnlySelfShow = args.isOnlySelfShow;

                if (isOnlySelfShow)
                {
                    long playerId = ET.GamePlayHelper.GetPlayerIdByUnitId(unit);
                    if (playerId != -1)
                    {
                        M2C_SyncPlayAudio _M2C_SyncPlayAudio = new ();
                        _M2C_SyncPlayAudio.UnitId = unit.Id;
                        _M2C_SyncPlayAudio.PlayAudioActionId = playAudioActionId;

                        MessageHelper.SendToClient(playerId, _M2C_SyncPlayAudio);
                    }
                }
                else
                {
                    M2C_SyncPlayAudio _M2C_SyncPlayAudio = new ();
                    _M2C_SyncPlayAudio.UnitId = unit.Id;
                    _M2C_SyncPlayAudio.PlayAudioActionId = playAudioActionId;

                    MessageHelper.Broadcast(unit, _M2C_SyncPlayAudio);
                }

                await ETTask.CompletedTask;
            }
        }

        [Event(SceneType.Map)]
        public class SyncPlayAnimator2C: AEvent<Scene, EventType.SyncPlayAnimator>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncPlayAnimator args)
            {
                Unit unit = args.unit;
                bool isOnlySelfShow = args.isOnlySelfShow;

                if (isOnlySelfShow)
                {
                    long playerId = ET.GamePlayHelper.GetPlayerIdByUnitId(unit);
                    if (playerId != -1)
                    {
                        M2C_SyncPlayAnimator _M2C_SyncPlayAnimator = new ();
                        _M2C_SyncPlayAnimator.UnitId = unit.Id;
                        _M2C_SyncPlayAnimator.PlayAnimatorComponent = unit.GetComponent<AnimatorComponent>().ToBson();

                        MessageHelper.SendToClient(playerId, _M2C_SyncPlayAnimator);
                    }
                }
                else
                {
                    M2C_SyncPlayAnimator _M2C_SyncPlayAnimator = new ();
                    _M2C_SyncPlayAnimator.UnitId = unit.Id;
                    _M2C_SyncPlayAnimator.PlayAnimatorComponent = unit.GetComponent<AnimatorComponent>().ToBson();

                    MessageHelper.Broadcast(unit, _M2C_SyncPlayAnimator);
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

                R2M_NoticeRoomBattleEnd _R2M_NoticeRoomBattleEnd = (R2M_NoticeRoomBattleEnd) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new M2R_NoticeRoomBattleEnd()
                {
                    RoomId = args.roomId,
                });

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
                    MessageHelper.SendToClient(playerId, _M2C_GamePlayChgNotice, true, needWait);
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
                    MessageHelper.SendToClient(playerId, _M2C_GamePlayCoinChgNotice, true, needWait);
                }
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
                    MessageHelper.SendToClient(playerId, _M2C_GamePlayModeChgNotice, true, needWait);
                }
                await ETTask.CompletedTask;
            }
        }

        // 获取看见unit的玩家，主要用于广播
        public static Dictionary<long, EntityRef<AOIEntity>> GetBeSeePlayers(this Unit self)
        {
            AOIEntity aoiEntity = self.GetComponent<AOIEntity>();
            if (aoiEntity == null)
            {
                return null;
            }
            return aoiEntity.GetBeSeePlayers();
        }
    }
}