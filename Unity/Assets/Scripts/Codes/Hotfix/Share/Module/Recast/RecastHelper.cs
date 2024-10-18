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

        public static (bool isHitMesh, float height) GetMeshHeightOnPoint(Scene scene, float3 pos, float height = 3)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            return navmeshManagerComponent.GetMeshHeightOnPoint(pos + new float3(0, height, 0));
        }

        public static NavmeshManagerComponent.NavMeshData GetNavMesh(Scene scene, float3 startPos)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            return navmeshManagerComponent.GetNavMeshData(startPos);
        }

        public static List<float3> GetSegmentPoints(Scene scene, float3 rayStartIn, float3 rayEndIn)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            return navmeshManagerComponent.GetSegmentPoints(rayStartIn, rayEndIn);
        }

        public static float3 GetMidPosWhen2Pos(Scene scene, float3 pos1, float3 pos2)
        {
            Unit observerUnit = ET.Ability.UnitHelper.GetOneObserverUnit(scene);

            List<float3> points = ET.RecastHelper.GetArrivePath(observerUnit, pos1, pos2);
            if (points == null)
            {
                return float3.zero;
            }

            if (points.Count <= 1)
            {
                return float3.zero;
            }

            float totalLength = 0;
            float3 midPos = float3.zero;
            for (int i = 1; i < points.Count; i++)
            {
                totalLength += math.length(points[i] - points[i - 1]);
            }

            float curLength = 0;
            for (int i = 1; i < points.Count; i++)
            {
                float lastLength = curLength;
                curLength += math.length(points[i] - points[i - 1]);
                if (lastLength <= 0.5f * totalLength && curLength > 0.5f * totalLength)
                {
                    float needLength = 0.5f * totalLength - lastLength;
                    midPos = points[i - 1] + math.normalize(points[i] - points[i - 1]) * needLength;
                    break;
                }
            }

            return midPos;
        }

        public static float3 GetMidPosWhen2Pos(List<float3> points)
        {
            if (points == null)
            {
                return float3.zero;
            }

            if (points.Count <= 1)
            {
                return float3.zero;
            }

            float totalLength = 0;
            float3 midPos = float3.zero;
            for (int i = 1; i < points.Count; i++)
            {
                totalLength += math.length(points[i] - points[i - 1]);
            }

            float curLength = 0;
            for (int i = 1; i < points.Count; i++)
            {
                float lastLength = curLength;
                curLength += math.length(points[i] - points[i - 1]);
                if (lastLength <= 0.5f * totalLength && curLength > 0.5f * totalLength)
                {
                    float needLength = 0.5f * totalLength - lastLength;
                    midPos = points[i - 1] + math.normalize(points[i] - points[i - 1]) * needLength;
                    break;
                }
            }

            return midPos;
        }

        public static float3 GetMidPosWhen3Pos(Scene scene, List<float3> posList)
        {
            float2 GetCircleCenter(float2 p1, float2 p2, float2 p3)
            {
                float a = p1.x - p2.x;
                float b = p1.y - p2.y;
                float c = p1.x - p3.x;
                float d = p1.y - p3.y;
                float e = (math.pow(p1.x, 2) - math.pow(p2.x, 2) + math.pow(p1.y, 2) - math.pow(p2.y, 2)) / 2.0f;
                float f = (math.pow(p1.x, 2) - math.pow(p3.x, 2) + math.pow(p1.y, 2) - math.pow(p3.y, 2)) / 2.0f;
                float det = b * c - a * d;
                if (math.abs(det) > 0)
                {
                    //x0,y0为计算得到的原点
                    float x0 = -(d * e - b * f) / det;
                    float y0 = -(a * f - c * e) / det;
                    return new float2(x0, y0);
                }
                else
                {
                    return new float2(0, 0);
                }
            }

            float3 GetCenterPoint(List<float3> posList)
            {
                if (posList == null || posList.Count == 0)
                {
                    return float3.zero;
                }
                float3 centerPos = float3.zero;
                foreach (float3 pos in posList)
                {
                    centerPos += pos;
                }

                return centerPos / posList.Count;
            }

            bool ChkArrive(Unit observerUnit, List<float3> posList, float3 chkPos)
            {
                foreach (float3 pos in posList)
                {
                    (bool canArrive, List<float3> pointList) = ET.RecastHelper.ChkArrive(observerUnit, chkPos, pos);
                    if (canArrive == false)
                    {
                        return false;
                    }
                }
                return true;
            }

            float2 circleCenter2D = GetCircleCenter(new float2(posList[0].x, posList[0].z),
                new float2(posList[1].x, posList[1].z),
                new float2(posList[2].x, posList[2].z));
            float3 circleCenterOrg1 = new float3(circleCenter2D.x, posList[0].y, circleCenter2D.y);

            float3 circleCenterOrg2 = GetCenterPoint(posList);

            (bool, float3) ChkCanReach(Unit observerUnit, List<float3> posList, float3 chkPos)
            {
                float3 centerPos = ET.RecastHelper.GetNearNavmeshPos(observerUnit, chkPos);

                if (ChkArrive(observerUnit, posList, centerPos))
                {
                    return (true, centerPos);
                }
                return (false, float3.zero);
            }

            Unit observerUnit = ET.Ability.UnitHelper.GetOneObserverUnit(scene);
            foreach (float3 circleCenterOrg in new List<float3>(){circleCenterOrg1, circleCenterOrg2})
            {
                float dis = 0.1f;
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        float3 circleCenter = circleCenterOrg + new float3(dis * i, 0, dis * j);
                        (bool canReach, float3 point) = ChkCanReach(observerUnit, posList, circleCenter);
                        if (canReach)
                        {
                            return point;
                        }

                        circleCenter = circleCenterOrg + new float3(-dis * i, 0, dis * j);
                        (canReach, point) = ChkCanReach(observerUnit, posList, circleCenter);
                        if (canReach)
                        {
                            return point;
                        }

                        circleCenter = circleCenterOrg + new float3(-dis * i, 0, -dis * j);
                        (canReach, point) = ChkCanReach(observerUnit, posList, circleCenter);
                        if (canReach)
                        {
                            return point;
                        }

                        circleCenter = circleCenterOrg + new float3(dis * i, 0, -dis * j);
                        (canReach, point) = ChkCanReach(observerUnit, posList, circleCenter);
                        if (canReach)
                        {
                            return point;
                        }
                    }
                }
            }
            return float3.zero;
        }

        public static float3 GetMidPosWhenNPos(Scene scene, List<float3> posList)
        {
            float3 GetCenterPoint(List<float3> posList)
            {
                if (posList == null || posList.Count == 0)
                {
                    return float3.zero;
                }
                float3 centerPos = float3.zero;
                foreach (float3 pos in posList)
                {
                    centerPos += pos;
                }

                return centerPos / posList.Count;
            }

            bool ChkArrive(Unit observerUnit, List<float3> posList, float3 chkPos)
            {
                foreach (float3 pos in posList)
                {
                    (bool canArrive, List<float3> pointList) = ET.RecastHelper.ChkArrive(observerUnit, chkPos, pos);
                    if (canArrive == false)
                    {
                        return false;
                    }
                }
                return true;
            }

            float3 circleCenterOrg = GetCenterPoint(posList);

            (bool, float3) ChkCanReach(Unit observerUnit, List<float3> posList, float3 chkPos)
            {
                float3 centerPos = ET.RecastHelper.GetNearNavmeshPos(observerUnit, chkPos);

                if (ChkArrive(observerUnit, posList, centerPos))
                {
                    return (true, centerPos);
                }
                return (false, float3.zero);
            }

            Unit observerUnit = ET.Ability.UnitHelper.GetOneObserverUnit(scene);
            float dis = 0.1f;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    float3 circleCenter = circleCenterOrg + new float3(dis * i, 0, dis * j);
                    (bool canReach, float3 point) = ChkCanReach(observerUnit, posList, circleCenter);
                    if (canReach)
                    {
                        return point;
                    }

                    circleCenter = circleCenterOrg + new float3(-dis * i, 0, dis * j);
                    (canReach, point) = ChkCanReach(observerUnit, posList, circleCenter);
                    if (canReach)
                    {
                        return point;
                    }

                    circleCenter = circleCenterOrg + new float3(-dis * i, 0, -dis * j);
                    (canReach, point) = ChkCanReach(observerUnit, posList, circleCenter);
                    if (canReach)
                    {
                        return point;
                    }

                    circleCenter = circleCenterOrg + new float3(dis * i, 0, -dis * j);
                    (canReach, point) = ChkCanReach(observerUnit, posList, circleCenter);
                    if (canReach)
                    {
                        return point;
                    }
                }
            }

            return float3.zero;
        }

    }
}