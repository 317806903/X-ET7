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
                if (self.RemoveObstacle() != null)
                {
                    RecastHelper.FullyUpdateTileCache(self.DomainScene());
                    NotifyToUpdateAllNavigationPathsForClients(self.DomainScene());
                }
                self.Name = string.Empty;

                if (self.navMeshAgent != null)
                {
                    if (self.NavMesh != null)
                    {
                        self.NavMesh.RemoveAgent(self.navMeshAgent);
                        self.NavMesh = null;
                    }
                    self.navMeshAgent = null;
                }
                self.NavMesh = null;
            }
        }

        [ObjectSystem]
        public class PathfindingComponentFixedUpdateSystem: FixedUpdateSystem<PathfindingComponent>
        {
            protected override void FixedUpdate(PathfindingComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask Init(this PathfindingComponent self, NavmeshManagerComponent navmeshManagerComponent, bool isPlayer,
        float navObstacleRadius)
        {
            Unit unit = self.GetUnit();
            float speed = ET.Ability.UnitHelper.GetMoveSpeed(unit);
            float radius = ET.Ability.UnitHelper.GetBodyRadius(unit);
            if (isPlayer)
            {
                self.NavMesh = await navmeshManagerComponent.CreateCrowdWhenPlayer(radius);
            }
            else
            {
                self.NavMesh = await navmeshManagerComponent.CreateCrowd(radius);
            }
            if (self.NavMesh == null)
            {
                return;
            }
            if (radius > self.NavMesh.GetRadius())
            {
                radius = self.NavMesh.GetRadius();
            }

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
            self.NavObstacleRadius = navObstacleRadius;
            self._NavMeshManager = navmeshManagerComponent;
        }

        private static void NotifyToUpdateAllNavigationPathsForClients(Scene scene)
        {
            Unit observerUnit = ET.Ability.UnitHelper.GetOneObserverUnit(scene);
            if (observerUnit == null)
            {
                return;
            }
            M2C_DrawAllMonsterCall2HeadQuarterPath drawAllMonsterCall2HeadQuarterPath = new()
            {
                Path = RecastHelper.GetAllPathsFromMonsterCallsToHeadQuarter(observerUnit)
            };
            EventType.SendDrawPathsToClients selfSendDrawPathsToClients = new()
            {
                pathToDraw = drawAllMonsterCall2HeadQuarterPath
            };
            EventSystem.Instance.Publish(scene.DomainScene(), selfSendDrawPathsToClients);
        }

        public static void FixedUpdate(this PathfindingComponent self, float fixedDeltaTime)
        {
            if (++self._CurFrameSyncPos >= self._WaitFrameSyncPos)
            {
                self._CurFrameSyncPos = 0;

                self.ResetPosAndFace();
                self.ChkIsUpOrDown();

                if (self.NavObstacleRadius > 0)
                {
                    Unit unit = self.GetUnit();
                    if (!self._ObstaclePos.Equals(unit.Position))
                    {
                        if (AddOrUpdateObstacle(self, unit.Position))
                        {
                            RecastHelper.FullyUpdateTileCache(self.DomainScene());
                            NotifyToUpdateAllNavigationPathsForClients(self.DomainScene());
                        }
                    }
                }
            }
        }
        
        public static float3? RemoveObstacle(this PathfindingComponent self)
        {
            if (self._ObstacleRef != 0)
            {
                NavmeshManagerComponent navmeshManagerComponent = self._NavMeshManager;
                if (navmeshManagerComponent == null)
                {
                    return null;
                }
                var tileCache = navmeshManagerComponent.obstacleTool.GetTileCache();
                tileCache.RemoveObstacle(self._ObstacleRef);
                self._ObstacleRef = 0;
                return self._ObstaclePos;
            }
            return null;
        }
        
        public static bool AddOrUpdateObstacle(this PathfindingComponent self, float3 pos)
        {
            if (self.NavObstacleRadius <= 0)
            {
                return false;
            }
            RemoveObstacle(self);
            self._ObstaclePos = pos;
            NavmeshManagerComponent navmeshManagerComponent = self._NavMeshManager;
            if (navmeshManagerComponent == null)
            {
                return false;
            }
            var tileCache = navmeshManagerComponent.obstacleTool.GetTileCache();
            self._ObstacleRef = tileCache.AddObstacle(new DotRecast.Core.Numerics.RcVec3f(-pos.x, pos.y, pos.z), self.NavObstacleRadius,
                navmeshManagerComponent.navSample.GetSettings().agentHeight);
            return self._ObstacleRef != 0;
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
                        unit.Forward = math.normalize(forward);
                    }
                }
            }
        }

        public static void ChkIsUpOrDown(this PathfindingComponent self)
        {
            if (self.NavMesh == null)
            {
                return;
            }

            float3 forward = self.NavMesh.GetAgentForward(self.navMeshAgent);
            if (forward.Equals(float3.zero))
            {
                return;
            }
            float3 position = self.NavMesh.GetAgentPos(self.navMeshAgent);
            (bool isHitMesh, float height) = ET.RecastHelper.GetMeshHeightOnPoint(self.DomainScene(), position);
            if (isHitMesh == false)
            {
                return;
            }

            float3 nextPosition = position + forward * 0.3f;
            (bool isHitMeshNext, float heightNext) = ET.RecastHelper.GetMeshHeightOnPoint(self.DomainScene(), nextPosition);
            if (isHitMeshNext == false)
            {
                return;
            }

            float startChgHeight = 0.15f;
            float upBigChgHeight = 0.2f;
            float downBigChgHeight = 0.2f;
            //Log.Error($"--zpb {math.abs(nextHitPos.y - hitPosition.y)} {nextHitPos.y > hitPosition.y} ");
            if(math.abs(heightNext - height) < startChgHeight)
            {
                self.ResetAgentSpeed(1f);
            }
            else if (heightNext > height)
            {
                if (heightNext > height + upBigChgHeight)
                {
                    self.ResetAgentSpeed(0.3f);
                }
                else
                {
                    self.ResetAgentSpeed(0.7f);
                }
            }
            else
            {
                if (heightNext < height - downBigChgHeight)
                {
                    self.ResetAgentSpeed(1.25f);
                }
                else
                {
                    self.ResetAgentSpeed(1.1f);
                }
            }
        }

        public static void ResetAgentSpeed(this PathfindingComponent self, float speedScale = 1)
        {
            Unit unit = self.GetUnit();
            float newSpeed = ET.Ability.UnitHelper.GetMoveSpeed(unit);
            self.NavMesh.ResetAgentSpeed(self.navMeshAgent, newSpeed * speedScale);
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
            if (UnitHelper.ChkIsObserver(unit))
            {
                return false;
            }
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
                Log.Error($"ChkCanArrive pathfinding ptr is zero: {self.DomainScene().Name}");
                return false;
            }

            return self.NavMesh.ChkCanArrive(startPos, targetPos);
        }

        public static List<float3> GetArrivePath(this PathfindingComponent self, float3 startPos, float3 targetPos)
        {
            if (self.NavMesh == null)
            {
                Log.Error($"GetArrivePath pathfinding ptr is zero: {self.DomainScene().Name}");
                return null;
            }

            return self.NavMesh.GetArrivePath(startPos, targetPos);
        }
        
        public static void SetMoveTarget(this PathfindingComponent self, float3 pos)
        {
            if ( self.NavMesh == null)
            {
                Log.Error($"SetMoveTarget pathfinding ptr is zero: {self.DomainScene().Name}");
                return;
            }

            self.NavMesh.SetMoveTarget(self.navMeshAgent, pos);
        }

        public static void SetMoveVelocity(this PathfindingComponent self, float3 velocity)
        {
            if ( self.NavMesh == null)
            {
                Log.Error($"SetMoveVelocity pathfinding ptr is zero: {self.DomainScene().Name}");
                return;
            }

            self.NavMesh.SetMoveVelocity(self.navMeshAgent, velocity);
        }

        public static float3 GetNearNavmeshPos(this PathfindingComponent self, float3 pos)
        {
            if (self.NavMesh == null)
            {
                Log.Error($"GetNearNavmeshPos pathfinding ptr is zero: {self.DomainScene().Name}");
                return pos;
            }

            return self.NavMesh.GetNearNavmeshPos(pos, out var _, out var _);
        }
    }
}