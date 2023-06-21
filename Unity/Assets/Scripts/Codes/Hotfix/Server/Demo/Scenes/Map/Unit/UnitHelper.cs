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
        [Invoke]
        public class SyncPosUnitInfo2C: AInvokeHandler<SyncPosUnits>
        {
            public override void Handle(SyncPosUnits args)
            {
                List<Unit> list = args.units;
                foreach (Unit unit in list)
                {
                    M2C_SyncPosUnits syncPosUnits = new () { Units = ListComponent<UnitPosInfo>.Create()};
                    syncPosUnits.Units.Add(ET.Ability.UnitHelper.SyncPosUnitInfo(unit));
                    MessageHelper.Broadcast(unit, syncPosUnits);
                }
                
            }
        }

        [Invoke]
        public class SyncNumericUnitInfo2C: AInvokeHandler<SyncNumericUnits>
        {
            public override void Handle(SyncNumericUnits args)
            {
                List<Unit> list = args.units;
                foreach (Unit unit in list)
                {
                    M2C_SyncNumericUnits syncNumericUnits = new () { Units = ListComponent<UnitNumericInfo>.Create() };
                    syncNumericUnits.Units.Add(ET.Ability.UnitHelper.SyncNumericUnitInfo(unit));
                    MessageHelper.Broadcast(unit, syncNumericUnits);
                }
                
            }
        }

        [Invoke]
        public class SyncUnitEffects2C: AInvokeHandler<SyncUnitEffects>
        {
            public override void Handle(SyncUnitEffects args)
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
            }
        }

        // 获取看见unit的玩家，主要用于广播
        public static Dictionary<long, AOIEntity> GetBeSeePlayers(this Unit self)
        {
            return self.GetComponent<AOIEntity>().GetBeSeePlayers();
        }
    }
}