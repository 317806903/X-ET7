using System;
using System.Collections.Generic;
using DotRecast.Core;
using Unity.Mathematics;
using ET.Ability;

namespace ET
{
    [FriendOf(typeof(PathfindingComponent))]
    public static class PathfindingComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<PathfindingComponent>
        {
            protected override void Awake(PathfindingComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<PathfindingComponent>
        {
            protected override void Destroy(PathfindingComponent self)
            {
                self.Name = string.Empty;

                if (self.navMeshAgent != null)
                {
                    if (self.NavMesh != null)
                    {
                        self.NavMesh.RemoveAgent(self.navMeshAgent);
                    }
                    self.navMeshAgent = null;
                }
            }
        }

        [ObjectSystem]
        public class PathfindingComponentFixedUpdateSystem: FixedUpdateSystem<PathfindingComponent>
        {
            protected override void FixedUpdate(PathfindingComponent self)
            {
                if (self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask Init(this PathfindingComponent self, NavmeshManagerComponent navmeshManagerComponent)
        {
            Unit unit = self.GetUnit();
            float speed = ET.Ability.UnitHelper.GetMoveSpeed(unit);
            float radius = ET.Ability.UnitHelper.GetBodyRadius(unit);
            self.NavMesh = await navmeshManagerComponent.CreateCrowd(radius);

            if (self.NavMesh == null)
            {
                throw new Exception($"nav load fail");
            }

            float separationWeight = 2;
            if (UnitHelper.ChkIsObserver(unit))
            {
                separationWeight = 2;
            }
            else
            {
                TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
                if (towerComponent != null)
                {
                    separationWeight = 2;
                }
            }
            self.navMeshAgent = self.NavMesh.AddAgent(separationWeight, radius, speed, unit.Position);
        }

        public static void FixedUpdate(this PathfindingComponent self, float fixedDeltaTime)
        {
            self.ResetPosAndFace();
        }

        public static Unit GetUnit(this PathfindingComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void ResetPosAndFace(this PathfindingComponent self)
        {
            if (self.NavMesh == null)
            {
                return;
            }
            bool chkIsMoveing = self.ChkIsMoveing();
            if (chkIsMoveing == false)
            {
                return;
            }

            Unit unit = self.GetUnit();
            float3 position = self.NavMesh.GetAgentPos(self.navMeshAgent);
            if (math.distancesq(unit.Position, position) > 0.01f)
            {
                unit.Position = position;
                if (self.NavMesh.ChkIsNeedChgFace(self.navMeshAgent))
                {
                    float3 forward = self.NavMesh.GetAgentForward(self.navMeshAgent);
                    if (forward.Equals(float3.zero) == false)
                    {
                        unit.Forward = forward;
                    }
                }
            }
        }

        public static void ResetAgentSpeed(this PathfindingComponent self)
        {
            Unit unit = self.GetUnit();
            float newSpeed = ET.Ability.UnitHelper.GetMoveSpeed(unit);
            self.NavMesh.ResetAgentSpeed(self.navMeshAgent, newSpeed);
        }

        public static void ResetPos(this PathfindingComponent self, float3 position)
        {
            self.NavMesh.StopMoveTarget(self.navMeshAgent);
            self.NavMesh.ResetPos(self.navMeshAgent, position);
        }

        public static void StopMoveTarget(this PathfindingComponent self)
        {
            self.NavMesh.DisableAgent(self.navMeshAgent);
            self.NavMesh.StopMoveTarget(self.navMeshAgent);
        }

        public static void StopMoveVelocity(this PathfindingComponent self)
        {
            self.NavMesh.StopMoveVelocity(self.navMeshAgent);
        }

        public static bool ChkIsArrived(this PathfindingComponent self)
        {
            Unit unit = self.GetUnit();
            MoveOrIdleComponent moveOrIdleComponent = unit.GetComponent<MoveOrIdleComponent>();
            if (moveOrIdleComponent.moveInputType == MoveInputType.Stop)
            {
                return true;
            }

            if (self.navMeshAgent == null)
            {
                return false;
            }
            bool chkIsMoveing = self.NavMesh.ChkIsMoveing(self.navMeshAgent);
            if (chkIsMoveing == false)
            {
                return true;
            }

            return false;
        }

        public static bool ChkIsMoveing(this PathfindingComponent self)
        {
            if (self.navMeshAgent.targetState == DotRecast.Detour.Crowd.DtMoveRequestState.DT_CROWDAGENT_TARGET_VELOCITY)
            {
                return true;
            }
            Unit unit = self.GetUnit();
            MoveOrIdleComponent moveOrIdleComponent = unit.GetComponent<MoveOrIdleComponent>();
            if (moveOrIdleComponent.moveInputType == MoveInputType.Stop)
            {
                return false;
            }
            return self.NavMesh.ChkIsMoveing(self.navMeshAgent);
        }

        public static bool ChkCanArrive(this PathfindingComponent self, float3 startPos, float3 targetPos)
        {
            if (self.NavMesh == null)
            {
                Log.Debug("寻路| Find 失败 pathfinding ptr is zero");
                throw new Exception($"pathfinding ptr is zero: {self.DomainScene().Name}");
            }

            return self.NavMesh.ChkCanArrive(startPos, targetPos);
        }

        public static List<float3> GetArrivePath(this PathfindingComponent self, float3 startPos, float3 targetPos)
        {
            if (self.NavMesh == null)
            {
                Log.Debug("寻路| Find 失败 pathfinding ptr is zero");
                throw new Exception($"pathfinding ptr is zero: {self.DomainScene().Name}");
            }

            return self.NavMesh.GetArrivePath(startPos, targetPos);
        }

        // public static void Find(this PathfindingComponent self, float3 start, float3 target, List<float3> result)
        // {
        //     if (self.NavMesh == null)
        //     {
        //         Log.Debug("寻路| Find 失败 pathfinding ptr is zero");
        //         throw new Exception($"pathfinding ptr is zero: {self.DomainScene().Name}");
        //     }
        //
        //     self.StartPos[0] = -start.x;
        //     self.StartPos[1] = start.y;
        //     self.StartPos[2] = start.z;
        //
        //     self.EndPos[0] = -target.x;
        //     self.EndPos[1] = target.y;
        //     self.EndPos[2] = target.z;
        //     //Log.Debug($"start find path: {self.GetParent<Unit>().Id}");
        //
        //     //float3 tmp1 = self.RecastFindNearestPoint(target);
        //     int n = 0;//Recast.RecastFind(self.NavMesh, PathfindingComponent.extents, self.StartPos, self.EndPos, self.Result);
        //     for (int i = 0; i < n; ++i)
        //     {
        //         int index = i * 3;
        //         result.Add(new float3(-self.Result[index], self.Result[index + 1], self.Result[index + 2]));
        //     }
        //     //Log.Debug($"finish find path: {self.GetParent<Unit>().Id} {result.ListToString()}");
        // }
        //
        // public static void FindWithAdjust(this PathfindingComponent self, float3 start, float3 target, List<float3> result,float adjustRaduis)
        // {
        //     self.Find(start, target, result);
        //     for (int i = 0; i < result.Count; i++)
        //     {
        //         float3 adjust = self.FindRandomPointWithRaduis(result[i], adjustRaduis);
        //         result[i] = adjust;
        //     }
        // }
        //
        // public static float3 FindRandomPointWithRaduis(this PathfindingComponent self, float3 pos, float raduis)
        // {
        //     if ( self.NavMesh == null)
        //     {
        //         throw new Exception($"pathfinding ptr is zero: {self.DomainScene().Name}");
        //     }
        //
        //     if (raduis > PathfindingComponent.FindRandomNavPosMaxRadius * 0.001f)
        //     {
        //         throw new Exception($"pathfinding raduis is too large，cur: {raduis}, max: {PathfindingComponent.FindRandomNavPosMaxRadius}");
        //     }
        //
        //     int degrees = RandomGenerator.RandomNumber(0, 360);
        //     float r = RandomGenerator.RandomNumber(0, (int) (raduis * 1000)) / 1000f;
        //
        //     float x = r * math.cos(math.radians(degrees));
        //     float z = r * math.sin(math.radians(degrees));
        //
        //     float3 findpos = new float3(pos.x + x, pos.y, pos.z + z);
        //
        //     return self.RecastFindNearestPoint(findpos);
        // }
        //
        // /// <summary>
        // /// 以pos为中心各自在宽和高的左右 前后两个方向延伸
        // /// </summary>
        // /// <param name="self"></param>
        // /// <param name="pos"></param>
        // /// <param name="width"></param>
        // /// <param name="height"></param>
        // /// <returns></returns>
        // /// <exception cref="Exception"></exception>
        // public static float3 FindRandomPointWithRectangle(this PathfindingComponent self, float3 pos, int width, int height)
        // {
        //     if ( self.NavMesh == null)
        //     {
        //         throw new Exception($"pathfinding ptr is zero: {self.DomainScene().Name}");
        //     }
        //
        //     if (width > PathfindingComponent.FindRandomNavPosMaxRadius * 0.001f || height > PathfindingComponent.FindRandomNavPosMaxRadius * 0.001f)
        //     {
        //         throw new Exception($"pathfinding rectangle is too large，width: {width} height: {height}, max: {PathfindingComponent.FindRandomNavPosMaxRadius}");
        //     }
        //
        //     float x = RandomGenerator.RandomNumber(-width, width);
        //     float z = RandomGenerator.RandomNumber(-height, height);
        //
        //     float3 findpos = new float3(pos.x + x, pos.y, pos.z + z);
        //
        //     return self.RecastFindNearestPoint(findpos);
        // }
        //
        // public static float3 FindRandomPointWithRaduis(this PathfindingComponent self, float3 pos, float minRadius, float maxRadius)
        // {
        //     if ( self.NavMesh == null)
        //     {
        //         throw new Exception($"pathfinding ptr is zero: {self.DomainScene().Name}");
        //     }
        //
        //     if (maxRadius > PathfindingComponent.FindRandomNavPosMaxRadius * 0.001f)
        //     {
        //         throw new Exception($"pathfinding raduis is too large，cur: {maxRadius}, max: {PathfindingComponent.FindRandomNavPosMaxRadius}");
        //     }
        //
        //     int degrees = RandomGenerator.RandomNumber(0, 360);
        //     float r = RandomGenerator.RandomNumber((int) (minRadius * 1000), (int) (maxRadius * 1000)) / 1000f;
        //
        //     float x = r * math.cos(math.radians(degrees));
        //     float z = r * math.sin(math.radians(degrees));
        //
        //     float3 findpos = new float3(pos.x + x, pos.y, pos.z + z);
        //
        //     return self.RecastFindNearestPoint(findpos);
        // }
        //
        // public static float3 RecastFindNearestPoint(this PathfindingComponent self, float3 pos)
        // {
        //     if ( self.NavMesh == null)
        //     {
        //         throw new Exception($"pathfinding ptr is zero: {self.DomainScene().Name}");
        //     }
        //
        //     self.StartPos[0] = -pos.x;
        //     self.StartPos[1] = pos.y;
        //     self.StartPos[2] = pos.z;
        //
        //     int ret = 1;//Recast.RecastFindNearestPoint(self.NavMesh, PathfindingComponent.extents, self.StartPos, self.EndPos);
        //     if (ret == 0)
        //     {
        //         throw new Exception($"RecastFindNearestPoint fail, 可能是位置配置有问题: sceneName:{self.DomainScene().Name} {pos} {self.Name} {self.GetParent<Unit>().Id} {self.EndPos.ArrayToString()}");
        //     }
        //
        //     return new float3(-self.EndPos[0], self.EndPos[1], self.EndPos[2]);
        // }

        public static void SetMoveTarget(this PathfindingComponent self, float3 pos)
        {
            if ( self.NavMesh == null)
            {
                throw new Exception($"pathfinding ptr is zero: {self.DomainScene().Name}");
            }

            self.NavMesh.SetMoveTarget(self.navMeshAgent, pos);
        }

        public static void SetMoveVelocity(this PathfindingComponent self, float3 velocity)
        {
            if ( self.NavMesh == null)
            {
                throw new Exception($"pathfinding ptr is zero: {self.DomainScene().Name}");
            }

            self.NavMesh.SetMoveVelocity(self.navMeshAgent, velocity);
        }

        public static float3 GetNearNavmeshPos(this PathfindingComponent self, float3 pos)
        {
            if ( self.NavMesh == null)
            {
                throw new Exception($"pathfinding ptr is zero: {self.DomainScene().Name}");
            }

            return self.NavMesh.GetNearNavmeshPos(pos);
        }
    }
}