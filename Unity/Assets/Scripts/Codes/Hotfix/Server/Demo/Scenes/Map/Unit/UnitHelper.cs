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
        
        [Event(SceneType.Map)]
        public class SyncPosUnitInfo2C: AEvent<Scene, EventType.SyncPosUnits>
        {
            protected override async ETTask Run(Scene scene, EventType.SyncPosUnits args)
            {
                List<Unit> list = args.units;
                foreach (Unit unit in list)
                {
                    M2C_SyncPosUnits syncPosUnits = new ()
                    {
                        Units = ListComponent<UnitPosInfo>.Create()
                    };
                    syncPosUnits.Units.Add(ET.Ability.UnitHelper.SyncPosUnitInfo(unit));
                    MessageHelper.Broadcast(unit, syncPosUnits);
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
                foreach (Unit unit in list)
                {
                    M2C_SyncNumericUnits syncNumericUnits = new ()
                    {
                        Units = ListComponent<UnitNumericInfo>.Create()
                    };
                    syncNumericUnits.Units.Add(ET.Ability.UnitHelper.SyncNumericUnitInfo(unit));
                    MessageHelper.Broadcast(unit, syncNumericUnits);
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
        public class NoticeGamePlayChg2C: AEvent<Scene, EventType.NoticeGamePlayToClient>
        {
            protected override async ETTask Run(Scene scene, EventType.NoticeGamePlayToClient args)
            {
                long playerId = args.playerId;
                GamePlayComponent gamePlayComponent = scene.GetComponent<GamePlayComponent>();
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
        
        // 获取看见unit的玩家，主要用于广播
        public static Dictionary<long, AOIEntity> GetBeSeePlayers(this Unit self)
        {
            return self.GetComponent<AOIEntity>().GetBeSeePlayers();
        }
    }
}