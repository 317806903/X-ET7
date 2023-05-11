using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof(MoveByPathComponent))]
    [FriendOf(typeof(NumericComponent))]
    public static class UnitHelper
    {
        public static UnitInfo CreateUnitInfo(Unit unit)
        {
            UnitInfo unitInfo = new UnitInfo();
            NumericComponent nc = unit.GetComponent<NumericComponent>();
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.ConfigId;
            unitInfo.Type = (int)unit.Type;
            unitInfo.Position = unit.Position;
            unitInfo.Forward = unit.Forward;

            MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
            if (moveByPathComponent != null)
            {
                if (!moveByPathComponent.IsArrived())
                {
                    unitInfo.MoveInfo = new MoveInfo() { Points = new List<float3>() };
                    unitInfo.MoveInfo.Points.Add(unit.Position);
                    for (int i = moveByPathComponent.N; i < moveByPathComponent.Targets.Count; ++i)
                    {
                        float3 pos = moveByPathComponent.Targets[i];
                        unitInfo.MoveInfo.Points.Add(pos);
                    }
                }
            }

            unitInfo.KV = new Dictionary<int, long>();

            foreach ((int key, long value) in nc.NumericDic)
            {
                unitInfo.KV.Add(key, value);
            }

            return unitInfo;
        }
        
        // 获取看见unit的玩家，主要用于广播
        public static Dictionary<long, AOIEntity> GetBeSeePlayers(this Unit self)
        {
            return self.GetComponent<AOIEntity>().GetBeSeePlayers();
        }
    }
}