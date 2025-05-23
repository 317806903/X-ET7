﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotRecast.Core.Numerics;
using DotRecast.Detour;
using DotRecast.Detour.Crowd;
using DotRecast.Recast.Toolset.Builder;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (NavmeshComponent))]
    public static class NavmeshComponentSystem
    {
        public static readonly DtQueryDefaultFilter DEFAULT_FILTER = new DtQueryDefaultFilter(SampleAreaModifications.SAMPLE_POLYFLAGS_WALK,
            SampleAreaModifications.SAMPLE_POLYFLAGS_DISABLED, new float[] { });

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
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static float GetRadius(this NavmeshComponent self)
        {
            return self.radius;
        }

        public static async ETTask CreateCrowd(this NavmeshComponent self, float maxAgentRadius)
        {
            self.radius = maxAgentRadius;
            await self._InitDtCrowd(maxAgentRadius);
        }

        public static DtNavMesh GetNavMesh(this NavmeshComponent self)
        {
            return self.GetParent<NavmeshManagerComponent>().GetNavMesh();
        }

        public static Sample GetSample(this NavmeshComponent self)
        {
            return self.GetParent<NavmeshManagerComponent>().navSample;
        }

        public static async Task _InitDtCrowd(this NavmeshComponent self, float maxAgentRadius)
        {
            DtCrowdConfig config = new DtCrowdConfig(maxAgentRadius);
            // Use aggressive path finding options to avoid non-optimal steering paths.
            config.maxFindPathIterations = 4000;
            config.maxTargetFindPathIterations = 200;
            config.maxTopologyOptimizationIterations = 128;
            config.maxObstacleAvoidanceCircles = 20;
            config.maxObstacleAvoidanceSegments = 20;

            self.crowd = new DtCrowd(config, self.GetNavMesh(), __ => DEFAULT_FILTER);

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

        public static void OnNavMeshUpdateCrowd(this NavmeshComponent self)
        {
            if (self.crowd != null)
            {
                IList<DtCrowdAgent> agents = self.crowd.GetActiveAgents();
                self.crowd.SetNavMesh(self.GetNavMesh());
                foreach (DtCrowdAgent dtCrowdAgent in agents)
                {
                    self.GetNearNavmeshPos(self.GetAgentPos(dtCrowdAgent), out var refs, out var nearestPt);
                    dtCrowdAgent.corridor.Reset(refs, nearestPt);

                    self.GetNearNavmeshPos(self.GetAgentTargetPos(dtCrowdAgent), out var refsTarget, out var nearestPtTarget);
                    self.crowd.RequestMoveTarget(dtCrowdAgent, refsTarget, nearestPtTarget);
                    dtCrowdAgent.targetReplanTime = 1e9f;
                    dtCrowdAgent.topologyOptTime = 1e9f;
                }
            }
        }

        public static DtCrowdAgent AddAgent(this NavmeshComponent self, float separationWeight, float radius, float maxSpeed, float3 position)
        {
            RcVec3f p = new RcVec3f(-position.x, position.y, position.z);
            DtCrowdAgentParams ap = GetAgentParams(self, radius, separationWeight);
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
            return new float3(-ag.targetPos.X, ag.targetPos.Y, ag.targetPos.Z);
        }

        public static float3 GetAgentPos(this NavmeshComponent self, DtCrowdAgent ag)
        {
            return new float3(-ag.npos.X, ag.npos.Y, ag.npos.Z);
        }

        public static float3 GetAgentForward(this NavmeshComponent self, DtCrowdAgent ag)
        {
            return new float3(-ag.vel.X, ag.vel.Y, ag.vel.Z);
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
            self.GetNearNavmeshPos(position, out var refs, out var nearestPt);

            ag.corridor.Reset(refs, nearestPt);
            ag.boundary.Reset();
            ag.partial = false;

            // Re-plan path immediately when the agent is spawn.
            ag.topologyOptTime = 1e9f;
            ag.targetReplanTime = 1e9f;

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
            return ag.corners.Length > 0;
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
                    if (math.abs(pos.x - targetPos1.x) < 0.1f
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

            self.GetNearNavmeshPos(pos, out var targetRef, out var targetPos);
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

            self.GetNearNavmeshPos(start, out var startNearRef, out var startNearPos);
            self.GetNearNavmeshPos(target, out var targetNearRef, out var targetNearPos);

            if (startNearRef != 0 && startNearRef == targetNearRef)
            {
                return true;
            }

            using ListComponent<long> pathListComponent = ListComponent<long>.Create();
            List<long> pathList = pathListComponent;
            var status = navquery.FindPath(startNearRef, targetNearRef, startNearPos, targetNearPos, filter, ref pathList, DtFindPathOption.NoOption);
            if (status.Succeeded())
            {
                if (pathList.Count <= 0)
                {
                    return false;
                }
                else if (pathList.Count == 1)
                {
                    if (pathList[0] == startNearRef)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static List<float3> GetArrivePath(this NavmeshComponent self, float3 start, float3 target)
        {
            DtNavMeshQuery navquery = self.GetSample().GetNavMeshQuery();
            IDtQueryFilter filter = self.crowd.GetFilter(0);

            self.GetNearNavmeshPos(start, out var startNearRef, out var startNearPos);
            self.GetNearNavmeshPos(target, out var targetNearRef, out var targetNearPos);
            List<RcVec3f> pathList = new();
            List<long> m_polys = new();
            self.GetParent<NavmeshManagerComponent>().navMeshTool.FindFollowPath(GetNavMesh(self), navquery, startNearRef, targetNearRef,
                startNearPos, targetNearPos, filter, true, ref m_polys, 0, ref pathList);
            List<float3> arrivePath = ListComponent<float3>.Create();
            for (int i = 0; i < pathList.Count; i++)
            {
                arrivePath.Add(RecastHelper.ToFloat3(pathList[i]));
            }
            arrivePath = LineSimplifier.SimplifyLineRDP(arrivePath);
            return arrivePath;
        }

        private static readonly RcVec3f MPolyPickExt = new RcVec3f(2, 4, 2);

        public static float3 GetNearNavmeshPos(this NavmeshComponent self, float3 pos, out long nearestRefOut, out RcVec3f nearestPtOut)
        {
            DtNavMeshQuery navquery = self.GetSample().GetNavMeshQuery();
            IDtQueryFilter filter = self.crowd.GetFilter(0);

            RcVec3f centerPos = RecastHelper.ToRcVec3f(pos);
            RcVec3f nearestPt;
            navquery.FindNearestPoly(centerPos, MPolyPickExt, filter, out var nearestRef, out nearestPt, out var _);

            nearestRefOut = nearestRef;
            nearestPtOut = nearestPt;
            return RecastHelper.ToFloat3(nearestPt);
        }

        public static DtCrowdAgentParams GetAgentParams(NavmeshComponent self, float radius, float separationWeight)
        {
            DtCrowdAgentParams ap = new DtCrowdAgentParams();
            ap.radius = math.max(GetSample(self).GetSettings().agentRadius, radius);
            ap.height = GetSample(self).GetSettings().agentHeight;
            ap.maxAcceleration = GetSample(self).GetSettings().agentMaxAcceleration;
            ap.maxSpeed = GetSample(self).GetSettings().agentMaxSpeed;
            ap.collisionQueryRange = ap.radius * 12.0f;
            ap.pathOptimizationRange = ap.radius * 30.0f;
            ap.updateFlags = GetUpdateFlags();
            ap.separationWeight = separationWeight;
            return ap;
        }

        public static int GetUpdateFlags()
        {
            int updateFlags = 0;
            updateFlags |= DtCrowdAgentUpdateFlags.DT_CROWD_ANTICIPATE_TURNS;
            updateFlags |= DtCrowdAgentUpdateFlags.DT_CROWD_OPTIMIZE_VIS;
            updateFlags |= DtCrowdAgentUpdateFlags.DT_CROWD_OPTIMIZE_TOPO;
            updateFlags |= DtCrowdAgentUpdateFlags.DT_CROWD_SEPARATION;
            return updateFlags;
        }

        public static void FixedUpdate(this NavmeshComponent self, float fixedDeltaTime)
        {
            if (self.crowd == null)
            {
                return;
            }

            if (++self.curFrameSyncPos >= self.waitFrameSyncPos)
            {
                self.curFrameSyncPos = 0;

                self.crowd.Update(fixedDeltaTime, null);
            }
        }

    }
}
