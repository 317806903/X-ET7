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

        
        // 获取看见unit的玩家，主要用于广播
        public static Dictionary<long, AOIEntity> GetBeSeePlayers(this Unit self)
        {
            return self.GetComponent<AOIEntity>().GetBeSeePlayers();
        }
    }
}