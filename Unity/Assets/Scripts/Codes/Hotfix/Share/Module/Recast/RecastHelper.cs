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

        public static List<float3> GetArrivePath(Unit unit, float3 startPos, float3 endPos)
        {
            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            if (pathfindingComponent == null)
            {
                return null;
            }
            return pathfindingComponent.GetArrivePath(startPos, endPos);
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