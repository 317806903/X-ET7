using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotRecast.Recast.Toolset;
using DotRecast.Recast.Toolset.Builder;
using DotRecast.Recast.Toolset.Geom;
using Unity.Mathematics;
using DotRecast.Core;
using DotRecast.Detour;
using DotRecast.Recast;

namespace ET
{
    [FriendOf(typeof(NavmeshManagerComponent))]
    public static class NavmeshManagerComponentSystem
    {
        public class AwakeSystem: AwakeSystem<NavmeshManagerComponent>
        {
            protected override void Awake(NavmeshManagerComponent self)
            {
                self.NavmeshByRadius = new ();
                self.segPoints = new();
                self.recordMeshHitDic = new();
            }
        }

        public static void Destroy(this NavmeshManagerComponent self, string name)
        {
            self.NavmeshByRadius.Clear();
            self.segPoints.Clear();
            self.objBytes = null;
            self.m_nav = null;
            self._sample = null;
            self.soloNavMeshBuilder = null;
            self.tileNavMeshBuilder = null;
            self.segPoints = null;
            self.recordMeshHitDic.Clear();
        }

        public static void InitByFile(this NavmeshManagerComponent self, string filePath, float scale)
        {
            byte[] bytes = EventSystem.Instance.Invoke<NavmeshManagerComponent.RecastFileLoader, byte[]>(new NavmeshManagerComponent.RecastFileLoader() {Name = filePath});
            if (bytes.Length == 0)
            {
                Log.Error($"no nav data: {filePath}");
                return;
            }

            self.objBytes = bytes;
            try
            {
                DemoInputGeomProvider geom = DemoObjImporter.Load(bytes, scale);

                self._sample = new Sample(geom, null, null);
                self.ResetSampleSettings(self._sample, 1);

                self._InitNavMeshBuilder();
            }
            catch (Exception e)
            {
                Log.Error($"InitByFile {e.Message}");
                return;
            }
        }

        public static void InitByFileBytes(this NavmeshManagerComponent self, byte[] bytes, float scale)
        {
            self.objBytes = bytes;

            try
            {
                DemoInputGeomProvider geom = DemoObjImporter.Load(bytes, scale);

                self._sample = new Sample(geom, null, null);
                self.ResetSampleSettings(self._sample, 1);

                self._InitNavMeshBuilder();
            }
            catch (Exception e)
            {
                Log.Error($"InitByFileBytes {e.Message}");
                return;
            }
        }

        public static void InitByMeshData(this NavmeshManagerComponent self, MeshHelper.MeshData meshData, float scale)
        {
            self.meshData = meshData;
            float[] allVertices = new float[3 * self.meshData.verticesOrg.Length];
            for (int i = 0; i < self.meshData.verticesOrg.Length; i++)
            {
                allVertices[i * 3] = self.meshData.verticesOrg[i].x * scale;
                allVertices[i * 3 + 1] = self.meshData.verticesOrg[i].y * scale;
                allVertices[i * 3 + 2] = self.meshData.verticesOrg[i].z * scale;
            }

            DemoInputGeomProvider geom = new DemoInputGeomProvider(allVertices, self.meshData.trianglesOrg);
            self._sample = new Sample(geom, null, null);
            self.ResetSampleSettings(self._sample, scale);

            self._InitNavMeshBuilder();
        }

        public static NavmeshComponent GetNavmeshComponent(this NavmeshManagerComponent self, float agentRadius)
        {
            if (self.NavmeshByRadius.TryGetValue(agentRadius, out NavmeshComponent navmeshComponent))
            {
                return navmeshComponent;
            }
            return null;
        }

        public static async ETTask<NavmeshComponent> CreateCrowd(this NavmeshManagerComponent self, float agentRadius)
        {
            while (self.m_nav == null)
            {
                if (self.IsDisposed)
                {
                    return null;
                }
                await TimerComponent.Instance.WaitAsync(100);
            }
            agentRadius = self._sample.GetSettings().agentRadius;
            NavmeshComponent navmeshComponent = self.GetNavmeshComponent(agentRadius);
            if (navmeshComponent != null)
            {
                return navmeshComponent;
            }

            navmeshComponent = self.AddChild<NavmeshComponent>();
            await navmeshComponent.CreateCrowd(agentRadius);

            self.NavmeshByRadius[agentRadius] = navmeshComponent;

            return navmeshComponent;
        }

        public static void ResetSampleSettings(this NavmeshManagerComponent self, Sample sample, float scale)
        {
            RcNavMeshBuildSettings rcNavMeshBuildSettings = sample.GetSettings();

            rcNavMeshBuildSettings.agentHeight = 2.0f;
            rcNavMeshBuildSettings.agentRadius = 0.5f;
            rcNavMeshBuildSettings.agentMaxClimb = 1.5f;
            rcNavMeshBuildSettings.agentMaxSlope = 70f;
        }

        public static void _InitNavMeshBuilder(this NavmeshManagerComponent self)
        {
            NavMeshBuildResult buildResult;

            var settings = self._sample.GetSettings();
            settings.tiled = true;
            if (settings.tiled)
            {
                self.tileNavMeshBuilder = new();
                buildResult = self.tileNavMeshBuilder.Build(self._sample.GetInputGeom(), settings);
            }
            else
            {
                self.soloNavMeshBuilder = new();
                buildResult = self.soloNavMeshBuilder.Build(self._sample.GetInputGeom(), settings);
            }

            if (!buildResult.Success)
            {
                Log.Error("failed to build");
                return;
            }

            self.m_nav = buildResult.NavMesh;
            self._sample.Update(self._sample.GetInputGeom(), buildResult.RecastBuilderResults, buildResult.NavMesh);
            self._sample.SetChanged(false);
        }


        public static DtNavMesh GetNavMesh(this NavmeshManagerComponent self)
        {
            return self.m_nav;
        }

        public static Sample GetSample(this NavmeshManagerComponent self)
        {
            return self._sample;
        }

        public static (bool, RcVec3f) _OnRaycast(this NavmeshManagerComponent self, RcVec3f rayStart, RcVec3f rayEnd)
        {
            var _sample = self.GetSample();

            // Hit test mesh.
            DemoInputGeomProvider inputGeom = _sample.GetInputGeom();
            if (_sample == null)
                return (false, RcVec3f.Zero);

            float hitTime = 0.0f;
            bool hit = false;
            if (inputGeom != null)
            {
                hit = inputGeom.RaycastMesh(rayStart, rayEnd, out hitTime);
            }

            if (!hit && _sample.GetNavMesh() != null)
            {
                hit = DtNavMeshRaycast.Raycast(_sample.GetNavMesh(), rayStart, rayEnd, out hitTime);
            }

            if (!hit && _sample.GetRecastResults() != null)
            {
                hit = RcPolyMeshRaycast.Raycast(_sample.GetRecastResults(), rayStart, rayEnd, out hitTime);
            }

            if (hit)
            {
                RcVec3f pos = new RcVec3f();
                pos.x = rayStart.x + (rayEnd.x - rayStart.x) * hitTime;
                pos.y = rayStart.y + (rayEnd.y - rayStart.y) * hitTime;
                pos.z = rayStart.z + (rayEnd.z - rayStart.z) * hitTime;
                //Log.Debug($"hitPos={pos}");
                return (true, pos);
            }
            else
            {
                return (false, RcVec3f.Zero);
            }
        }

        public static (bool, float3) OnRaycast(this NavmeshManagerComponent self, float3 rayStartIn, float3 rayEndIn)
        {
            RcVec3f rayStart = new RcVec3f(-rayStartIn.x, rayStartIn.y, rayStartIn.z);
            RcVec3f rayEnd = new RcVec3f(-rayEndIn.x, rayEndIn.y, rayEndIn.z);

            var (bRet, rcVec3f) = self._OnRaycast(rayStart, rayEnd);
            if (bRet)
            {
                return (true, new float3(-rcVec3f.x, rcVec3f.y, rcVec3f.z));
            }
            else
            {
                return (false, float3.zero);
            }
        }

        public static (bool, float3) ChkHitMesh(this NavmeshManagerComponent self, float3 rayStartIn, float3 rayEndIn)
        {
            var segmentCount = math.ceil(math.distance(rayStartIn, rayEndIn) / self.GetSample().GetSettings().cellSize);

            var (bRet, hitPos) = self._ChkHitMesh(rayStartIn, rayEndIn, segmentCount);
            if (bRet)
            {
                return (true, hitPos);
            }
            else
            {
                return (false, float3.zero);
            }
        }

        public static (bool, float3) _ChkHitMesh(this NavmeshManagerComponent self, float3 rayStartIn, float3 rayEndIn, float segmentCount)
        {
            for (int i = 0; i <= segmentCount; i++)
            {
                var pos = math.lerp(rayStartIn, rayEndIn, i/segmentCount);
                bool bHit = self.ChkHitMeshOnPoint(pos);
                if (bHit)
                {
                    return (true, pos);
                }
            }
            return (false, float3.zero);
        }

        public static List<float3> GetSegmentPoints(this NavmeshManagerComponent self, float3 rayStartIn, float3 rayEndIn)
        {
            self.segPoints.Clear();
            if (rayStartIn.Equals(rayEndIn))
            {
                self.segPoints.Add(rayStartIn);
                return self.segPoints;
            }
            var segmentCount = math.ceil(math.distance(rayStartIn, rayEndIn) / self.GetSample().GetSettings().cellSize);
            for (int i = 0; i <= segmentCount; i++)
            {
                var pos = math.lerp(rayStartIn, rayEndIn, i/segmentCount);
                self.segPoints.Add(pos);
            }
            return self.segPoints;
        }

        public static (bool, bool) ChkHitMeshOnPointRecord(this NavmeshManagerComponent self, int x, int y, int z)
        {
            if (self.recordMeshHitDic.TryGetValue(x, out var dic2))
            {
                if (dic2.TryGetValue(y, out var dic3))
                {
                    if (dic3.TryGetValue(z, out var isHitMesh))
                    {
                        return (true, isHitMesh);
                    }
                }
            }

            return (false, false);
        }

        public static void RecordHitMeshOnPoint(this NavmeshManagerComponent self, int x, int y, int z, bool isHitMesh)
        {
            if (self.recordMeshHitDic.TryGetValue(x, out var dic2) == false)
            {
                dic2 = new();
                self.recordMeshHitDic[x] = dic2;
            }

            if (dic2.TryGetValue(y, out var dic3) == false)
            {
                dic3 = new();
                dic2[y] = dic3;
            }

            dic3[z] = isHitMesh;
        }

        public static bool ChkHitMeshOnPoint(this NavmeshManagerComponent self, float3 rayPosIn)
        {
            int x = (int)(rayPosIn.x * 100);
            int y = (int)(rayPosIn.y * 100);
            int z = (int)(rayPosIn.z * 100);

            (bool isRecord, bool isHitMesh) = self.ChkHitMeshOnPointRecord(x, y, z);
            if (isRecord)
            {
                return isHitMesh;
            }

            RcVec3f rayPos = new RcVec3f(-rayPosIn.x, rayPosIn.y, rayPosIn.z);

            foreach (RecastBuilderResult recastBuilderResult in self.GetSample().GetRecastResults())
            {
                RcHeightfield rcHeightfield = recastBuilderResult.GetSolidHeightfield();
                try
                {
                    bool bHit = self._ChkHitPointOneHeightfield(rayPos, rcHeightfield);
                    if (bHit)
                    {
                        self.RecordHitMeshOnPoint(x, y, z, true);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

            self.RecordHitMeshOnPoint(x, y, z, false);
            return false;
        }

        public static bool _ChkHitPointOneHeightfield(this NavmeshManagerComponent self, RcVec3f rayPosIn, RcHeightfield hf)
        {
            RcVec3f orig = hf.bmin;
            float cs = hf.cs;
            float ch = hf.ch;

            int w = hf.width;
            int h = hf.height;

            if (rayPosIn.x >= orig.x && rayPosIn.x < orig.x + w * cs
                && rayPosIn.z >= orig.z && rayPosIn.z < orig.z + h * cs)
            {
            }
            else
            {
                return false;
            }

            // if (!self.FrustumTest(hf.bmin, hf.bmax))
            // {
            //     return false;
            // }

            float disX = (rayPosIn.x - orig.x) / cs;
            int indexX = (int)Math.Floor(disX);

            float disZ = (rayPosIn.z - orig.z) / cs;
            int indexZ = (int)Math.Floor(disZ);

            RcSpan s = null;
            try
            {
                if (indexX + indexZ * w >= hf.spans.Length)
                {
                    return false;
                }
                s = hf.spans[indexX + indexZ * w];
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            while (s != null)
            {
                if (rayPosIn.y >= orig.y + s.smin * ch && rayPosIn.y <= orig.y + s.smax * ch)
                {
                    return true;
                }
                s = s.next;
            }

            // for (int y = 0; y < h; ++y)
            // {
            //     for (int x = 0; x < w; ++x)
            //     {
            //         float fx = orig.x + x * cs;
            //         float fz = orig.z + y * cs;
            //         RcSpan s = hf.spans[x + y * w];
            //         while (s != null)
            //         {
            //             if (rayPosIn.x >= fx && rayPosIn.x <= fx + cs
            //                 && rayPosIn.z >= fz && rayPosIn.z <= fz + cs
            //                 && rayPosIn.y >= orig.y + s.smin * ch && rayPosIn.y <= orig.y + s.smax * ch)
            //             {
            //                 return true;
            //             }
            //             s = s.next;
            //         }
            //     }
            // }

            return false;
        }

        public static bool FrustumTest(this NavmeshManagerComponent self, float bmin_x, float bmin_y, float bmin_z, float bmax_x, float bmax_y, float bmax_z)
        {
            foreach (float[] plane in self.frustumPlanes)
            {
                float p_x;
                float p_y;
                float p_z;
                float n_x;
                float n_y;
                float n_z;
                if (plane[0] >= 0)
                {
                    p_x = bmax_x;
                    n_x = bmin_x;
                }
                else
                {
                    p_x = bmin_x;
                    n_x = bmax_x;
                }

                if (plane[1] >= 0)
                {
                    p_y = bmax_y;
                    n_y = bmin_y;
                }
                else
                {
                    p_y = bmin_y;
                    n_y = bmax_y;
                }

                if (plane[2] >= 0)
                {
                    p_z = bmax_z;
                    n_z = bmin_z;
                }
                else
                {
                    p_z = bmin_z;
                    n_z = bmax_z;
                }

                if (plane[0] * p_x + plane[1] * p_y + plane[2] * p_z + plane[3] < 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool FrustumTest(this NavmeshManagerComponent self, RcVec3f bmin, RcVec3f bmax)
        {
            return self.FrustumTest(bmin.x, bmin.y, bmin.z, bmax.x, bmax.y, bmax.z);
        }
    }
}