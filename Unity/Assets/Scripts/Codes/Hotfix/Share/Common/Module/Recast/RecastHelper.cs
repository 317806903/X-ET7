using DotRecast.Core.Numerics;
using ET.Ability;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
namespace ET
{
    public static class RecastHelper
    {
        public static float3 GetNearNavmeshPos(Unit unit, float3 pos, float nearDis = 0)
        {
            if (nearDis > 0)
            {
                float randomValue1 = RandomGenerator.RandFloat01() * nearDis;
                randomValue1 = RandomGenerator.RandomBool()? randomValue1 : -randomValue1;
                float randomValue2 = RandomGenerator.RandFloat01() * nearDis;
                randomValue2 = RandomGenerator.RandomBool()? randomValue2 : -randomValue2;
                pos += new float3(randomValue1, 0.05f, randomValue2);
            }
            return GetNearNavmeshPos(unit, pos, out _);
        }

        public static float3 GetNearNavmeshPos(Unit unit, float3 pos, out long polyRef)
        {
            polyRef = 0;
            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            if (pathfindingComponent == null)
            {
                return pos;
            }
            return pathfindingComponent.GetNearNavmeshPos(pos, out polyRef);
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
            List<float3> points = GetArrivePath(unit, startPos, endPos);
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

        public static List<float3> GetRandomPointFromMesh(Scene scene, float pointDis)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            return navmeshManagerComponent.GetRandomPointFromMesh(pointDis);
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
            Unit observerUnit = UnitHelper.GetOneObserverUnit(scene);

            List<float3> points = GetArrivePath(observerUnit, pos1, pos2);
            if (points == null)
            {
                return float3.zero;
            }

            if (points.Count <= 1)
            {
                return float3.zero;
            }

            return GetLengthMidPosByList(points);
        }

        public static float3 GetLengthMidPosByList(List<float3> points)
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
            // 计算外心的函数
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
                    (bool canArrive, List<float3> pointList) = RecastHelper.ChkArrive(observerUnit, chkPos, pos);
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
                float3 centerPos = GetNearNavmeshPos(observerUnit, chkPos);

                if (ChkArrive(observerUnit, posList, centerPos))
                {
                    return (true, centerPos);
                }
                return (false, float3.zero);
            }

            Unit observerUnit = UnitHelper.GetOneObserverUnit(scene);
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
                    (bool canArrive, List<float3> pointList) = RecastHelper.ChkArrive(observerUnit, chkPos, pos);
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
                float3 centerPos = GetNearNavmeshPos(observerUnit, chkPos);

                if (ChkArrive(observerUnit, posList, centerPos))
                {
                    return (true, centerPos);
                }
                return (false, float3.zero);
            }

            Unit observerUnit = UnitHelper.GetOneObserverUnit(scene);
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

        public static float3? RemoveObstacleFromUnit(Scene scene, long unitId)
        {
            if (unitId == 0)
            {
                return null;
            }
            Unit unitToMove = UnitHelper.GetUnit(scene, unitId);
            if (unitToMove == null)
            {
                return null;
            }
            PathfindingComponent pathfindingComponent = unitToMove.GetComponent<PathfindingComponent>();
            if (pathfindingComponent == null)
            {
                return null;
            }
            var result = pathfindingComponent.RemoveObstacle();
            return result;
        }

        public static bool AddObstacleFromUnit(Scene scene, long unitId, float3 pos)
        {
            if (unitId == 0)
            {
                return false;
            }
            Unit unitToMove = UnitHelper.GetUnit(scene, unitId);
            if (unitToMove == null)
            {
                return false;
            }
            PathfindingComponent pathfindingComponent = unitToMove.GetComponent<PathfindingComponent>();
            if (pathfindingComponent == null)
            {
                return false;
            }
            return pathfindingComponent.AddOrUpdateObstacle(pos);
        }

        public static long AddObstacle(Scene scene, float3 pos, float radius, float height)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            return navmeshManagerComponent.AddObstacle(pos, radius, height);
        }

        public static void RemoveObstacle(Scene scene, long obstacleRef)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            navmeshManagerComponent.RemoveObstacle(obstacleRef);
        }

        public static bool UpdateDynamicMesh(Scene scene, bool isNeedUpdateCrowd)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            NavmeshManagerComponent navmeshManagerComponent = gamePlayComponent.GetComponent<NavmeshManagerComponent>();
            return navmeshManagerComponent.UpdateDynamicMesh(isNeedUpdateCrowd);
        }

        public static List<NavPath> GetAllPathsFromMonsterCallsToHeadQuarter(Unit unit)
        {
            Scene scene = unit.DomainScene();
            var gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlay(scene)?.GetComponent<GamePlayTowerDefenseComponent>();
            if (gamePlayTowerDefenseComponent == null)
            {
                return null;
            }
            PutMonsterCallComponent putMonsterCallComponent = gamePlayTowerDefenseComponent.GetComponent<PutMonsterCallComponent>();
            PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
            if (putMonsterCallComponent != null && putMonsterCallComponent.MonsterCallUnitId != null && putHomeComponent != null)
            {
                List<NavPath> paths = new List<NavPath>();
                foreach (var monsterCallUnitIds in putMonsterCallComponent.MonsterCallUnitId)
                {
                    long playerId = monsterCallUnitIds.Key;
                    long monsterCallUnitId = monsterCallUnitIds.Value;
                    Unit monsterCallUnit = UnitHelper.GetUnit(scene, monsterCallUnitId);
                    if (monsterCallUnit == null)
                    {
                        continue;
                    }
                    float3 pos = monsterCallUnit.Position;
                    TeamFlagType homeTeamFlagType = gamePlayTowerDefenseComponent.GetHomeTeamFlagTypeByPlayer(playerId);
                    Unit homeUnit = putHomeComponent.GetHomeUnit(homeTeamFlagType);
                    if (homeUnit == null)
                    {
                        continue;
                    }
                    float3 homePos = homeUnit.Position;
                    var path = GetArrivePath(unit, pos, homePos);
                    if (path != null && path.Count > 0)
                    {
                        paths.Add(new NavPath
                        {
                            TargetPosition = homePos,
                            PlayerId = playerId,
                            MonsterCallUnitId = monsterCallUnitId,
                            Points = path
                        });
                    }
                }
                return paths;
            }
            return null;
        }

        public static List<float3> GetRandomPointFromMonsterCallsToHeadQuarter(Scene scene, float pointDis)
        {
            var gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
            if (gamePlayTowerDefenseComponent == null)
            {
                return null;
            }
            PutMonsterCallComponent putMonsterCallComponent = gamePlayTowerDefenseComponent.GetComponent<PutMonsterCallComponent>();
            PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
            if (putMonsterCallComponent != null && putMonsterCallComponent.MonsterCallUnitId != null && putHomeComponent != null)
            {
                HashSetComponent<float3> randomPaths = HashSetComponent<float3>.Create();
                foreach (var monsterCallUnitIds in putMonsterCallComponent.MonsterCallUnitId)
                {
                    long playerId = monsterCallUnitIds.Key;
                    long monsterCallUnitId = monsterCallUnitIds.Value;
                    Unit monsterCallUnit = UnitHelper.GetUnit(scene, monsterCallUnitId);
                    if (monsterCallUnit == null)
                    {
                        continue;
                    }
                    float3 monsterCallUnitPos = monsterCallUnit.Position;
                    TeamFlagType homeTeamFlagType = gamePlayTowerDefenseComponent.GetHomeTeamFlagTypeByPlayer(playerId);
                    Unit homeUnit = putHomeComponent.GetHomeUnit(homeTeamFlagType);
                    if (homeUnit == null)
                    {
                        continue;
                    }
                    float3 homePos = homeUnit.Position;
                    Unit observerUnit = UnitHelper.GetOneObserverUnit(scene);
                    var points = GetArrivePath(observerUnit, monsterCallUnitPos, homePos);
                    if (points != null && points.Count > 0)
                    {
                        for (int i = 1; i < points.Count; i++)
                        {
                            var segmentCount = math.ceil(math.distance(points[i], points[i - 1]) / pointDis);
                            for (int j = 0; j <= segmentCount; j++)
                            {
                                var pos = math.lerp(points[i], points[i - 1], j / segmentCount);
                                randomPaths.Add(pos);
                            }
                        }
                    }
                }
                return randomPaths.ToList();
            }
            return null;
        }

        public static float3 GetCenterPointFromMonsterCallsToHeadQuarter(Scene scene)
        {
            var gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
            if (gamePlayTowerDefenseComponent == null)
            {
                return float3.zero;
            }
            PutMonsterCallComponent putMonsterCallComponent = gamePlayTowerDefenseComponent.GetComponent<PutMonsterCallComponent>();
            PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
            if (putMonsterCallComponent != null && putMonsterCallComponent.MonsterCallUnitId != null && putHomeComponent != null)
            {
                float3 centerPoint = float3.zero;
                int count = 0;
                foreach (var monsterCallUnitIds in putMonsterCallComponent.MonsterCallUnitId)
                {
                    long monsterCallUnitId = monsterCallUnitIds.Value;
                    Unit monsterCallUnit = UnitHelper.GetUnit(scene, monsterCallUnitId);
                    if (monsterCallUnit == null)
                    {
                        continue;
                    }
                    float3 monsterCallUnitPos = monsterCallUnit.Position;
                    centerPoint += monsterCallUnitPos;
                    count++;
                }
                Dictionary<TeamFlagType, long> homeUnitList = putHomeComponent.GetHomeUnitList();
                foreach (var homeUnitId in homeUnitList.Values)
                {
                    Unit homeUnit = UnitHelper.GetUnit(scene, homeUnitId);
                    if (homeUnit == null)
                    {
                        continue;
                    }
                    float3 monsterCallUnitPos = homeUnit.Position;
                    centerPoint += monsterCallUnitPos;
                    count++;
                }

                return centerPoint / count;
            }
            return float3.zero;
        }

        public static float3 ToFloat3(RcVec3f rcVec3f)
        {
            return new float3(-rcVec3f.X, rcVec3f.Y, rcVec3f.Z);
        }

        public static RcVec3f ToRcVec3f(float3 float3)
        {
            return new RcVec3f(-float3.x, float3.y, float3.z);
        }
    }
}