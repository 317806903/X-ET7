using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DotRecast.Core;
using DotRecast.Detour;
using DotRecast.Detour.Crowd;
using DotRecast.Detour.Io;
using DotRecast.Recast;
using DotRecast.Recast.Toolset;
using DotRecast.Recast.Toolset.Builder;
using DotRecast.Recast.Toolset.Geom;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(NavmeshComponent))]
    public static class NavmeshComponentSystem
    {
        public class AwakeSystem: AwakeSystem<NavmeshComponent>
        {
            protected override void Awake(NavmeshComponent self)
            {
            }
        }

        [ObjectSystem]
        public class NavmeshComponentDestroySystem: DestroySystem<NavmeshComponent>
        {
            protected override void Destroy(NavmeshComponent self)
            {
                self.crowd = null;
            }
        }

        [ObjectSystem]
        public class NavmeshComponentFixedUpdateSystem: FixedUpdateSystem<NavmeshComponent>
        {
            protected override void FixedUpdate(NavmeshComponent self)
            {
                // if (self.DomainScene().SceneType != SceneType.Map)
                // {
                //     return;
                // }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask CreateCrowd(this NavmeshComponent self, float agentRadius)
        {
            await self._InitDtCrowd(agentRadius);
        }

        public static DtNavMesh GetNavMesh(this NavmeshComponent self)
        {
            return self.GetParent<NavmeshManagerComponent>().m_nav;
        }

        public static Sample GetSample(this NavmeshComponent self)
        {
            return self.GetParent<NavmeshManagerComponent>()._sample;
        }

        public static async Task _InitDtCrowd(this NavmeshComponent self, float agentRadius)
        {
            DtCrowdConfig config = new DtCrowdConfig(agentRadius);

            self.crowd = new DtCrowd(config, self.GetNavMesh(), __ => new DtQueryDefaultFilter(SampleAreaModifications.SAMPLE_POLYFLAGS_ALL,
                SampleAreaModifications.SAMPLE_POLYFLAGS_DISABLED, new float[] { 1f, 10f, 1f, 1f, 2f, 1.5f }));

            // Setup local avoidance option to different qualities.
            // Use mostly default settings, copy from dtCrowd.
            DtObstacleAvoidanceParams option = new DtObstacleAvoidanceParams(self.crowd.GetObstacleAvoidanceParams(0));

            // Low (11)
            option.velBias = 0.5f;
            option.adaptiveDivs = 5;
            option.adaptiveRings = 2;
            option.adaptiveDepth = 1;
            self.crowd.SetObstacleAvoidanceParams(0, option);

            // Medium (22)
            option.velBias = 0.5f;
            option.adaptiveDivs = 5;
            option.adaptiveRings = 2;
            option.adaptiveDepth = 2;
            self.crowd.SetObstacleAvoidanceParams(1, option);

            // Good (45)
            option.velBias = 0.5f;
            option.adaptiveDivs = 7;
            option.adaptiveRings = 2;
            option.adaptiveDepth = 3;
            self.crowd.SetObstacleAvoidanceParams(2, option);

            // High (66)
            option.velBias = 0.5f;
            option.adaptiveDivs = 7;
            option.adaptiveRings = 3;
            option.adaptiveDepth = 3;

            self.crowd.SetObstacleAvoidanceParams(3, option);
            await ETTask.CompletedTask;
        }

        public static DtCrowdAgent AddAgent(this NavmeshComponent self, float separationWeight, float radius, float maxSpeed, float3 position)
        {
            RcVec3f p = new RcVec3f(-position.x, position.y, position.z);
            DtCrowdAgentParams ap = GetAgentParams(radius, separationWeight);
            ap.maxSpeed = maxSpeed;
            DtCrowdAgent ag = self.crowd.AddAgent(p, ap);
            return ag;
        }

        public static void ResetAgentSpeed(this NavmeshComponent self, DtCrowdAgent ag, float newSpeed)
        {
            ag.option.maxSpeed = newSpeed;
            ag.desiredSpeed = newSpeed;
        }

        public static float3 GetAgentTargetPos(this NavmeshComponent self, DtCrowdAgent ag)
        {
            return new float3(-ag.targetPos.x, ag.targetPos.y, ag.targetPos.z);
        }

        public static float3 GetAgentPos(this NavmeshComponent self, DtCrowdAgent ag)
        {
            return new float3(-ag.npos.x, ag.npos.y, ag.npos.z);
        }

        public static float3 GetAgentForward(this NavmeshComponent self, DtCrowdAgent ag)
        {
            return new float3(-ag.vel.x, ag.vel.y, ag.vel.z);
        }

        public static void DisableAgent(this NavmeshComponent self, DtCrowdAgent ag)
        {
            ag.state = DtCrowdAgentState.DT_CROWDAGENT_STATE_INVALID;
        }

        public static void EnableAgent(this NavmeshComponent self, DtCrowdAgent ag)
        {
            ag.state = DtCrowdAgentState.DT_CROWDAGENT_STATE_WALKING;
        }

        public static bool IsAgentEnable(this NavmeshComponent self, DtCrowdAgent ag)
        {
            return ag.state == DtCrowdAgentState.DT_CROWDAGENT_STATE_WALKING;
        }

        public static void RemoveAgent(this NavmeshComponent self, DtCrowdAgent ag)
        {
            self.crowd.RemoveAgent(ag);
        }

        public static void ResetPos(this NavmeshComponent self, DtCrowdAgent ag, float3 position)
        {
            DtNavMeshQuery navquery = self.GetSample().GetNavMeshQuery();
            IDtQueryFilter filter = self.crowd.GetFilter(0);
            RcVec3f halfExtents = self.crowd.GetQueryExtents();

            RcVec3f centerPos = new RcVec3f(-position.x, position.y, position.z);
            var status = navquery.FindNearestPoly(centerPos, halfExtents, filter, out var refs, out var nearestPt, out var _);
            if (status.Failed())
            {
                nearestPt = centerPos;
                refs = 0;
            }

            ag.corridor.Reset(refs, nearestPt);
            ag.boundary.Reset();
            ag.partial = false;

            ag.topologyOptTime = 0;
            ag.targetReplanTime = 0;

            ag.dvel = RcVec3f.Zero;
            ag.nvel = RcVec3f.Zero;
            ag.vel = RcVec3f.Zero;
            ag.npos = nearestPt;

            ag.desiredSpeed = 0;

        }

        public static void StopMoveTarget(this NavmeshComponent self, DtCrowdAgent ag)
        {
            self.crowd.ResetMoveTarget(ag);
        }

        public static void StopMoveVelocity(this NavmeshComponent self, DtCrowdAgent ag)
        {
            if (ag.targetState == DtMoveRequestState.DT_CROWDAGENT_TARGET_VELOCITY)
            {
                self.crowd.ResetMoveTarget(ag);
            }
        }

        public static bool ChkIsMoveing(this NavmeshComponent self, DtCrowdAgent ag)
        {
            if (ag.targetState == DtMoveRequestState.DT_CROWDAGENT_TARGET_VELOCITY)
            {
                return true;
            }
            if (ag.targetState == DtMoveRequestState.DT_CROWDAGENT_TARGET_REQUESTING)
            {
                return true;
            }
            if (ag.targetState == DtMoveRequestState.DT_CROWDAGENT_TARGET_NONE)
            {
                return false;
            }
            return ag.corners.Count > 0;
        }

        public static bool ChkIsNeedChgFace(this NavmeshComponent self, DtCrowdAgent ag)
        {
            if (ag.targetState == DtMoveRequestState.DT_CROWDAGENT_TARGET_VELOCITY)
            {
                return false;
            }
            return true;
        }

        public static void SetMoveTarget(this NavmeshComponent self, DtCrowdAgent ag, float3 pos)
        {
            if (self.IsAgentEnable(ag))
            {
                if (ag.targetState == DtMoveRequestState.DT_CROWDAGENT_TARGET_NONE)
                {
                }
                else
                {
                    float3 targetPos1 = self.GetAgentTargetPos(ag);
                    if(math.abs(pos.x - targetPos1.x) < 0.1f
                       && math.abs(pos.z - targetPos1.z) < 0.1f)
                    {
                        if (ag.targetState == DtMoveRequestState.DT_CROWDAGENT_TARGET_VALID)
                        {
                            //ag.targetState = DtMoveRequestState.DT_CROWDAGENT_TARGET_REQUESTING;
                        }
                        return;
                    }
                }
            }

            self.EnableAgent(ag);

            DtNavMeshQuery navquery = self.GetSample().GetNavMeshQuery();
            IDtQueryFilter filter = self.crowd.GetFilter(0);
            RcVec3f halfExtents = self.crowd.GetQueryExtents();

            RcVec3f centerPos = new RcVec3f(-pos.x, pos.y, pos.z);
            RcVec3f targetPos;
            long targetRef = 0;
            navquery.FindNearestPoly(centerPos, halfExtents, filter, out targetRef, out targetPos, out var _);
            if (targetRef != 0)
            {
                self.crowd.RequestMoveTarget(ag, targetRef, targetPos);
            }
        }

        public static void SetMoveVelocity(this NavmeshComponent self, DtCrowdAgent ag, float3 velocity)
        {
            self.EnableAgent(ag);
            RcVec3f targetVelocity = new RcVec3f(-velocity.x, velocity.y, velocity.z);
            self.crowd.RequestMoveVelocity(ag, targetVelocity);
        }

        public static bool ChkCanArrive(this NavmeshComponent self, float3 start, float3 target)
        {
            DtNavMeshQuery navquery = self.GetSample().GetNavMeshQuery();
            IDtQueryFilter filter = self.crowd.GetFilter(0);
            RcVec3f halfExtents = self.crowd.GetQueryExtents();

            RcVec3f startPos = new RcVec3f(-start.x, start.y, start.z);
            RcVec3f startNearPos;
            RcVec3f targetPos = new RcVec3f(-target.x, target.y, target.z);
            RcVec3f targetNearPos;
            long startNearRef = 0;
            long targetNearRef = 0;
            navquery.FindNearestPoly(startPos, halfExtents, filter, out startNearRef, out startNearPos, out var _);
            navquery.FindNearestPoly(targetPos, halfExtents, filter, out targetNearRef, out targetNearPos, out var _);
            if (startNearRef != 0 && startNearRef == targetNearRef)
            {
                return true;
            }

            List<long> pathList = ListComponent<long>.Create();
            var status = navquery.FindPath(startNearRef, targetNearRef, startNearPos, targetNearPos, filter, ref pathList, DtFindPathOption.NoOption);
            if (status.Succeeded())
            {
                if (pathList.Count <= 0)
                {
                    return false;
                }
                else
                {
                    if (pathList.Count == 1 && pathList[0] == startNearRef)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public static List<float3> GetArrivePath(this NavmeshComponent self, float3 start, float3 target)
        {
            DtNavMeshQuery navquery = self.GetSample().GetNavMeshQuery();
            IDtQueryFilter filter = self.crowd.GetFilter(0);
            RcVec3f halfExtents = self.crowd.GetQueryExtents();

            RcVec3f startPos = new RcVec3f(-start.x, start.y, start.z);
            RcVec3f startNearPos;
            RcVec3f targetPos = new RcVec3f(-target.x, target.y, target.z);
            RcVec3f targetNearPos;
            long startNearRef = 0;
            long targetNearRef = 0;
            navquery.FindNearestPoly(startPos, halfExtents, filter, out startNearRef, out startNearPos, out var _);
            navquery.FindNearestPoly(targetPos, halfExtents, filter, out targetNearRef, out targetNearPos, out var _);

            List<float3> arrivePath = self.arrivePath;
            arrivePath.Clear();

            using ListComponent<long> pathListTmp = ListComponent<long>.Create();
            List<long> pathList = pathListTmp;
            var status = navquery.FindPath(startNearRef, targetNearRef, startNearPos, targetNearPos, filter, ref pathList, DtFindPathOption.NoOption);
            if (status.Succeeded())
            {
                if (pathList.Count <= 0)
                {
                    return arrivePath;
                }
                else
                {
                    if (pathList.Count == 1 && pathList[0] == startNearRef)
                    {
                        return arrivePath;
                    }

                    // if (pathList[npolys - 1] != endRef)
                    // {
                    //     navquery.ClosestPointOnPoly(pathList[npolys - 1], endNearestPt, epos1, 0);
                    // }
                    using ListComponent<StraightPathItem> straightPathTmp = ListComponent<StraightPathItem>.Create();
                    List<StraightPathItem> straightPath = straightPathTmp;
                    var result = navquery.FindStraightPath(startNearPos, targetNearPos, pathList, ref straightPath, 100, 0);
                    if (result.Failed())
                    {
                        return arrivePath;
                    }
                    for (int i = 0; i < straightPath.Count; i++)
                    {
                        RcVec3f pos = straightPath[i].pos;
                        arrivePath.Add(new float3(-pos.x, pos.y, pos.z));
                    }
                    return arrivePath;
                }
            }
            return arrivePath;
        }

        public static float3 GetNearNavmeshPos(this NavmeshComponent self, float3 pos)
        {
            DtNavMeshQuery navquery = self.GetSample().GetNavMeshQuery();
            IDtQueryFilter filter = self.crowd.GetFilter(0);
            RcVec3f halfExtents = self.crowd.GetQueryExtents();

            RcVec3f centerPos = new RcVec3f(-pos.x, pos.y, pos.z);
            RcVec3f targetPos;
            long targetRef = 0;
            navquery.FindNearestPoly(centerPos, halfExtents, filter, out targetRef, out targetPos, out var _);
            return new float3(-targetPos.x, targetPos.y, targetPos.z);
        }

        public static DtCrowdAgentParams GetAgentParams(float radius, float separationWeight)
        {
            // DtCrowdAgentParams ap = new DtCrowdAgentParams();
            // ap.radius = _impl.GetSample().GetSettings().agentRadius;
            // ap.height = _impl.GetSample().GetSettings().agentHeight;
            // ap.maxAcceleration = _impl.GetSample().GetSettings().agentMaxAcceleration;
            // ap.maxSpeed = _impl.GetSample().GetSettings().agentMaxSpeed;
            // ap.collisionQueryRange = ap.radius * 12.0f;
            // ap.pathOptimizationRange = ap.radius * 30.0f;
            // ap.updateFlags = GetUpdateFlags();
            // ap.obstacleAvoidanceType = toolParams.m_obstacleAvoidanceType;
            // ap.separationWeight = toolParams.m_separationWeight;
            // return ap;
            DtCrowdAgentParams ap = new DtCrowdAgentParams();
            ap.radius = radius;
            ap.height = 2f;
            ap.maxAcceleration = 200f;
            ap.maxSpeed = 3.5f;
            ap.collisionQueryRange = ap.radius * 12.0f;
            ap.pathOptimizationRange = ap.radius * 30.0f;
            ap.updateFlags = GetUpdateFlags();
            ap.obstacleAvoidanceType = 3;
            ap.separationWeight = separationWeight;
            return ap;
        }

        public static int GetUpdateFlags()
        {
            int updateFlags = 0;
            //if (toolParams.m_anticipateTurns)
            {
                updateFlags |= DtCrowdAgentParams.DT_CROWD_ANTICIPATE_TURNS;
            }

            //if (toolParams.m_optimizeVis)
            {
                updateFlags |= DtCrowdAgentParams.DT_CROWD_OPTIMIZE_VIS;
            }

            //if (toolParams.m_optimizeTopo)
            {
                updateFlags |= DtCrowdAgentParams.DT_CROWD_OPTIMIZE_TOPO;
            }

            //if (toolParams.m_obstacleAvoidance)
            {
                updateFlags |= DtCrowdAgentParams.DT_CROWD_OBSTACLE_AVOIDANCE;
            }

            //if (toolParams.m_separation)
            {
                updateFlags |= DtCrowdAgentParams.DT_CROWD_SEPARATION;
            }

            return updateFlags;
        }

        public static void FixedUpdate(this NavmeshComponent self, float fixedDeltaTime)
        {
            if (self.crowd == null)
            {
                return;
            }
            self.crowd.Update(fixedDeltaTime, null);
        }

    }
}