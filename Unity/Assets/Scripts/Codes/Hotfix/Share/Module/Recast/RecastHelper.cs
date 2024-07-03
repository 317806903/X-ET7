using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    public static class RecastHelper
    {
        public static float3 GetNearNavmeshPos(Unit unit, float3 pos)
        {
            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            if (pathfindingComponent == null)
            {
                return pos;
            }
            return pathfindingComponent.GetNearNavmeshPos(pos);
        }

        public static float3 GetHitNavmeshPos(Scene scene, float3 pos, float height = 3)
        {
            (bool bRet, float3 hitPoint) = OnRaycast(scene, pos + new float3(0, height, 0), pos + new float3(0, -height, 0));

            return hitPoint;
        }

        public static List<float3> GetArrivePath(Unit unit, float3 startPos, float3 endPos)
        {
            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            if (pathfindingComponent == null)
            {
                return null;
            }
            return pathfindingComponent.GetArrivePath(startPos, endPos);
        }

        public static (bool, List<float3>) ChkArrive(Unit unit, float3 startPos, float3 endPos)
        {
            List<float3> points = ET.RecastHelper.GetArrivePath(unit, startPos, endPos);
            if (points == null)
            {
                return (false, points);
            }
            if (points.Count < 2)
            {
                return (false, points);
            }
            float3 lastPoint = points[points.Count - 1];
            if (math.abs(endPos.x - lastPoint.x) < 0.3f
                && math.abs(endPos.y - lastPoint.y) < 0.3f
                && math.abs(endPos.z - lastPoint.z) < 0.3f)
            {
                return (true, points);
            }
            else
            {
                return (false, points);
            }
        }

        public static (bool, float3) OnRaycast(Scene scene, float3 rayStartIn, float3 rayEndIn)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            return navmeshManagerComponent.OnRaycast(rayStartIn, rayEndIn);
        }

        public static (bool, float3) ChkHitMesh(Scene scene, float3 rayStartIn, float3 rayEndIn)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            return navmeshManagerComponent.ChkHitMesh(rayStartIn, rayEndIn);
        }

        public static bool ChkHitMeshOnPoint(Scene scene, float3 rayPos)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            return navmeshManagerComponent.ChkHitMeshOnPoint(rayPos);
        }

        public static List<float3> GetSegmentPoints(Scene scene, float3 rayStartIn, float3 rayEndIn)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            return navmeshManagerComponent.GetSegmentPoints(rayStartIn, rayEndIn);
        }
    }
}