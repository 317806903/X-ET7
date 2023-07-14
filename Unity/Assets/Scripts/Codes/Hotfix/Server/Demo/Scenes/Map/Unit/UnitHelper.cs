using System.Collections.Generic;
using ET.Client;
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

                MultiMap<long, Unit> playerSeeUnits = MessageHelper.GetUnitBeSeePlayers(list);
                foreach (var playerSeeUnit in playerSeeUnits)
                {
                    long playerId = playerSeeUnit.Key;
                    M2C_SyncPosUnits syncPosUnits = new ()
                    {
                        Units = ListComponent<UnitPosInfo>.Create()
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

                MultiMap<long, Unit> playerSeeUnits = MessageHelper.GetUnitBeSeePlayers(list);
                foreach (var playerSeeUnit in playerSeeUnits)
                {
                    long playerId = playerSeeUnit.Key;
                    M2C_SyncNumericUnits syncNumericUnits = new ()
                    {
                        Units = ListComponent<UnitNumericInfo>.Create()
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
        public class SyncUnitEffects2C: AEvent<Scene, EventType.SyncUnitEffects>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncUnitEffects args)
            {
                Unit unit = args.unit;
                bool isAddEffect = args.isAddEffect;
                long effectObjId = args.effectObjId;
                ET.Ability.EffectObj effectObj = args.effectObj;

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
                MessageHelper.Broadcast(unit, SyncUnitEffects);
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
        public class NoticeGamePlayChg2C: AEvent<Scene, EventType.NoticeGamePlayToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGamePlayToClient args)
            {
                long playerId = args.playerId;
                GamePlayComponent gamePlayComponent = args.gamePlayComponent;
                M2C_GamePlayChgNotice _M2C_GamePlayChgNotice = new ()
                {
                    GamePlayInfo = gamePlayComponent.ToBson(),
                    Components = ListComponent<byte[]>.Create(),
                };
                foreach (Entity entity in gamePlayComponent.Components.Values)
                {
                    if (entity is ITransferClient)
                    {
                        _M2C_GamePlayChgNotice.Components.Add(entity.ToBson());
                    }
                }
                
                MessageHelper.SendToClient(playerId, _M2C_GamePlayChgNotice);
                await ETTask.CompletedTask;
            }
        }
        
        [Event(SceneType.Map)]
        public class NoticeGamePlayPlayerListChg2C: AEvent<Scene, EventType.NoticeGamePlayPlayerListToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGamePlayPlayerListToClient args)
            {
                long playerId = args.playerId;
                GamePlayPlayerListComponent gamePlayPlayerListComponent = args.gamePlayPlayerListComponent;
                M2C_GamePlayCoinChgNotice _M2C_GamePlayCoinChgNotice = new ()
                {
                    GamePlayPlayerListComponent = gamePlayPlayerListComponent.ToBson(),
                };
                
                MessageHelper.SendToClient(playerId, _M2C_GamePlayCoinChgNotice);
                await ETTask.CompletedTask;
            }
        }
        
        [Event(SceneType.Map)]
        public class NoticeGamePlayModeChg2C: AEvent<Scene, EventType.NoticeGamePlayModeToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGamePlayModeToClient args)
            {
                long playerId = args.playerId;
                
                GamePlayModeComponent gamePlayModeComponent = args.gamePlayModeComponent;
                M2C_GamePlayModeChgNotice _M2C_GamePlayModeChgNotice = new ()
                {
                    GamePlayModeInfo = gamePlayModeComponent.ToBson(),
                    Components = ListComponent<byte[]>.Create(),
                };
                foreach (Entity entity in gamePlayModeComponent.Components.Values)
                {
                    if (entity is ITransferClient)
                    {
                        _M2C_GamePlayModeChgNotice.Components.Add(entity.ToBson());
                    }
                }
                
                MessageHelper.SendToClient(playerId, _M2C_GamePlayModeChgNotice);
                await ETTask.CompletedTask;
            }
        }
        
        // 获取看见unit的玩家，主要用于广播
        public static Dictionary<long, AOIEntity> GetBeSeePlayers(this Unit self)
        {
            return self.GetComponent<AOIEntity>().GetBeSeePlayers();
        }
    }
}