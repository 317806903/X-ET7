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
        public class SyncUnitInfo2C: AInvokeHandler<SyncUnits>
        {
            public override void Handle(SyncUnits args)
            {
                List<Unit> list = args.units;
                foreach (Unit unit in list)
                {
                    M2C_SyncUnits syncUnits = new () { Units = new List<UnitInfo>() };
                    syncUnits.Units.Add(ET.Ability.UnitHelper.SyncUnitSimpleInfo(unit));
                    MessageHelper.Broadcast(unit, syncUnits);
                }
                
            }
        }

        [Invoke]
        public class SyncUnitEffects2C: AInvokeHandler<SyncUnitEffects>
        {
            public override void Handle(SyncUnitEffects args)
            {
                Unit unit = args.unit;
                bool isSceneEffect = args.isSceneEffect;
                bool isAddEffect = args.isAddEffect;
                long effectObjId = args.effectObjId;
                ET.Ability.EffectObj effectObj = args.effectObj;

                M2C_SyncUnitEffects SyncUnitEffects = new ();
                SyncUnitEffects.UnitId = isSceneEffect?0:unit.Id;
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