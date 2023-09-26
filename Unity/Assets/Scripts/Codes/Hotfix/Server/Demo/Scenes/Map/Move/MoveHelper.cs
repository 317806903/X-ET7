using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    public static class MoveHelper
    {
        // error: 0表示协程走完正常停止
        public static void NoticeStopToClient(this Unit unit, int error)
        {
            MessageHelper.Broadcast(unit, new M2C_Stop()
            {
                Error = error,
                Id = unit.Id, 
                Position = unit.Position,
                Rotation = unit.Rotation,
            });
        }
        
        public static void NoticePointListToClient(this Unit unit, List<float3> pointList)
        {
            // 广播寻路路径
            M2C_PathfindingResult m2CPathfindingResult = new () { Points = pointList };
            m2CPathfindingResult.Position = unit.Position;
            m2CPathfindingResult.Id = unit.Id;
            MessageHelper.Broadcast(unit, m2CPathfindingResult);
        }
        
    }
}