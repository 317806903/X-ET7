using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotRecast.Core.Numerics;
using DotRecast.Detour;
using DotRecast.Detour.Crowd;
using DotRecast.Recast.Toolset.Builder;
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
                self.recordNearestPosDic = new();
                self.recordNearestRefDic = new();
            }
        }

        [ObjectSystem]
        public class NavmeshComponentDestroySystem: DestroySystem<NavmeshComponent>
        {
            protected override void Destroy(NavmeshComponent self)
            {
                self.crowd = null;
                self.arrivePath.Clear();
                self.recordNearestPosDic.Clear();
                self.recordNearestRefDic.Clear();
            }
        }

        [ObjectSystem]
        public class NavmeshComponentFixedUpdateSystem: FixedUpdateSystem<NavmeshComponent>
        {
            protected override void FixedUpdate(NavmeshComponent self)
            {
                // if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                // {
                //     return;
                // }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static float GetRadius(this NavmeshComponent self)
        {
            return self.radius;
        }

        public static async ETTask CreateCrowd(this NavmeshComponent self, float agentRadius)
        {
            self.radius = agentRadius;
            await self._InitDtCrowd(agentRadius);
        }

        public static DtNavMesh GetNavMesh(this NavmeshComponent self)
        {
            return self.GetParent<NavmeshManagerComponent>().GetNavMesh();
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
                    for (int i = 0; i < pathList.Count; i++)
                    {

                    }
                    return true;
                }
            }
            return false;
        }

        public static List<float3> GetArrivePath(this NavmeshComponent self, float3 start, float3 target)
        {
            // start = new float3(11.938713073730469f, -20.010421752929688f, -27.78343391418457f);
            // target = new float3(-0.83235251903533936f, -15.153124809265137f, -44.560817718505859f);
            // start = new float3(55.34029f, -15.42707f, -26.54941f);
            // target = new float3(12.41908f, -18.55572f, -38.11686f);

            DtNavMeshQuery navquery = self.GetSample().GetNavMeshQuery();
            IDtQueryFilter filter = self.crowd.GetFilter(0);
            RcVec3f halfExtents = self.crowd.GetQueryExtents();

            self.GetNearNavmeshPos(start, out var startNearRef, out var startNearPos);
            self.GetNearNavmeshPos(target, out var targetNearRef, out var targetNearPos);

            using ListComponent<long> pathListTmp = ListComponent<long>.Create();
            List<long> pathList = pathListTmp;
            var status = navquery.FindPath(startNearRef, targetNearRef, startNearPos, targetNearPos, filter, ref pathList, DtFindPathOption.NoOption);
            if (status.Succeeded())
            {
                if (pathList.Count <= 0)
                {
                    return null;
                }
                else
                {
                    if (pathList.Count == 1 && pathList[0] == startNearRef)
                    {
                        return null;
                    }

                    // if (pathList[npolys - 1] != endRef)
                    // {
                    //     navquery.ClosestPointOnPoly(pathList[npolys - 1], endNearestPt, epos1, 0);
                    // }
                    // using ListComponent<DtStraightPath> straightPathTmp = ListComponent<DtStraightPath>.Create();
                    // List<DtStraightPath> straightPath = straightPathTmp;
                    int maxPath = 256;
                    Span<DtStraightPath> straightPath = stackalloc DtStraightPath[maxPath];
                    var result = navquery.FindStraightPath(startNearPos, targetNearPos, pathList, pathList.Count, straightPath, out var straightPathCount, maxPath, 
                        DtStraightPathOptions.DT_STRAIGHTPATH_ALL_CROSSINGS);
                    if (result.Failed())
                    {
                        return null;
                    }

                    List<float3> arrivePath = self.arrivePath;
                    arrivePath.Clear();

                    for (int i = 0; i < straightPathCount; i++)
                    {
                        RcVec3f pos = straightPath[i].pos;
                        float3 pos2 = new float3(-pos.X, pos.Y, pos.Z);
                        arrivePath.Add(pos2);
                    }

                    if (self.ChkAllPointOK(arrivePath) == false)
                    {
                        return null;
                    }
                    return arrivePath;
                }
            }
            return null;
        }

        public static bool ChkAllPointOK(this NavmeshComponent self, List<float3> arrivePath)
        {
            for (int i = 0; i < arrivePath.Count; i++)
            {
                float3 pos = arrivePath[i];
                (bool isHitMesh, float height) = RecastHelper.GetMeshHeightOnPoint(self.DomainScene(), pos);
                if (isHitMesh == false)
                {
                    continue;
                    //return false;
                }
                if (height < pos.y - 3f || height > pos.y + 3f)
                {
                    return false;
                }
            }
            for (int i = 0; i < arrivePath.Count-1; i++)
            {
                float absDisY = math.abs(arrivePath[i].y - arrivePath[i + 1].y);
                if (absDisY < 3)
                {
                    continue;
                }
                //float lowHeight = math.min(arrivePath[i].y, arrivePath[i+1].y);
                float3 pos = (arrivePath[i] + arrivePath[i+1])/2;
                float disY = math.max(1, absDisY * 0.5f);
                (bool isHitMesh, float height) = RecastHelper.GetMeshHeightOnPoint(self.DomainScene(), pos, disY);
                if (isHitMesh == false)
                {
                    return false;
                    // if (math.abs(arrivePath[i].y - arrivePath[i+1].y) > 4)
                    // {
                    //     return false;
                    // }
                    // continue;
                }

                if (height > arrivePath[i].y && height > arrivePath[i+1].y)
                {
                    continue;
                }
                if (height < pos.y - 3f || height > pos.y + 3f)
                {
                    return false;
                }
            }

            return true;
        }

        public static float3 GetNearNavmeshPos(this NavmeshComponent self, float3 pos, out long nearestRefOut, out RcVec3f nearestPtOut)
        {
            int x = (int)(pos.x * 100);
            int y = (int)(pos.y * 100);
            int z = (int)(pos.z * 100);

            (bool isRecord, float3 nearestPos, long nearestRef) = self.ChkNearestPosRecord(x, y, z);
            if (isRecord)
            {
                nearestRefOut = nearestRef;
                nearestPtOut = new RcVec3f(-nearestPos.x, nearestPos.y, nearestPos.z);
                return nearestPos;
            }

            DtNavMeshQuery navquery = self.GetSample().GetNavMeshQuery();
            IDtQueryFilter filter = self.crowd.GetFilter(0);
            RcVec3f halfExtents = self.crowd.GetQueryExtents();

            RcVec3f centerPos = new RcVec3f(-pos.x, pos.y, pos.z);
            RcVec3f nearestPt;
            navquery.FindNearestPoly(centerPos, halfExtents, filter, out nearestRef, out nearestPt, out var _);

            nearestRefOut = nearestRef;
            nearestPtOut = nearestPt;

            nearestPos = new float3(-nearestPt.X, nearestPt.Y, nearestPt.Z);
            self.RecordNearestPos(x, y, z, nearestPos, nearestRef);

            return nearestPos;
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
                updateFlags |= DtCrowdAgentUpdateFlags.DT_CROWD_ANTICIPATE_TURNS;
            }

            //if (toolParams.m_optimizeVis)
            {
                updateFlags |= DtCrowdAgentUpdateFlags.DT_CROWD_OPTIMIZE_VIS;
            }

            //if (toolParams.m_optimizeTopo)
            {
                updateFlags |= DtCrowdAgentUpdateFlags.DT_CROWD_OPTIMIZE_TOPO;
            }

            //if (toolParams.m_obstacleAvoidance)
            {
                updateFlags |= DtCrowdAgentUpdateFlags.DT_CROWD_OBSTACLE_AVOIDANCE;
            }

            //if (toolParams.m_separation)
            {
                updateFlags |= DtCrowdAgentUpdateFlags.DT_CROWD_SEPARATION;
            }

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

        public static (bool, float3, long) ChkNearestPosRecord(this NavmeshComponent self, int x, int y, int z)
        {
            if (self.recordNearestPosDic.TryGetValue(x, out var dic2))
            {
                if (dic2.TryGetValue(y, out var dic3))
                {
                    if (dic3.TryGetValue(z, out var nearestPos))
                    {
                        long nearestRef = self.recordNearestRefDic[x][y][z];
                        return (true, nearestPos, nearestRef);
                    }
                }
            }

            return (false, float3.zero, 0);
        }

        public static void RecordNearestPos(this NavmeshComponent self, int x, int y, int z, float3 nearestPos, long nearestRef)
        {
            if (self.recordNearestPosDic.TryGetValue(x, out var dic2) == false)
            {
                dic2 = new();
                self.recordNearestPosDic[x] = dic2;
            }

            if (dic2.TryGetValue(y, out var dic3) == false)
            {
                dic3 = new();
                dic2[y] = dic3;
            }

            dic3[z] = nearestPos;


            if (self.recordNearestRefDic.TryGetValue(x, out var dicRef2) == false)
            {
                dicRef2 = new();
                self.recordNearestRefDic[x] = dicRef2;
            }

            if (dicRef2.TryGetValue(y, out var dicRef3) == false)
            {
                dicRef3 = new();
                dicRef2[y] = dicRef3;
            }

            dicRef3[z] = nearestRef;
        }
    }
}