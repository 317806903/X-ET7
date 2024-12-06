using DotRecast.Core.Numerics;
using DotRecast.Detour;
using DotRecast.Detour.Dynamic;
using DotRecast.Detour.Dynamic.Colliders;
using DotRecast.Detour.Dynamic.Io;
using DotRecast.Recast;
using DotRecast.Recast.Toolset;
using DotRecast.Recast.Toolset.Builder;
using DotRecast.Recast.Toolset.Geom;
using DotRecast.Recast.Toolset.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Mathematics;
namespace ET
{
    [FriendOf(typeof (NavmeshManagerComponent))]
    public static class NavmeshManagerComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<NavmeshManagerComponent>
        {
            protected override void Awake(NavmeshManagerComponent self)
            {
                self.segPoints = new();
                self.recordMeshHitDic = new();
                self.recordMeshHeightDic = new();
                self.navMeshDataDictionary = new();
                self.navMeshTool = new RcTestNavMeshTool();
            }
        }

        [ObjectSystem]
        public class NavmeshManagerComponentFixedUpdateSystem: FixedUpdateSystem<NavmeshManagerComponent>
        {
            protected override void FixedUpdate(NavmeshManagerComponent self)
            {
                if (self.IsDisposed)
                {
                    return;
                }
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<NavmeshManagerComponent>
        {
            protected override void Destroy(NavmeshManagerComponent self)
            {
                self.segPoints.Clear();
                self.navSample = null;
                self.segPoints = null;
                self.recordMeshHitDic.Clear();
                self.recordMeshHeightDic.Clear();
            }
        }

        public static void InitByFileBytes(this NavmeshManagerComponent self, byte[] bytes, float scale)
        {
            try
            {
                DemoInputGeomProvider geom = DemoObjImporter.Load(bytes, scale);

                self.navSample = new Sample(geom, null, null);
                self.ResetSampleSettings(self.navSample, 1);

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
            float[] allVertices = new float[3 * meshData.verticesOrg.Length];
            for (int i = 0; i < meshData.verticesOrg.Length; i++)
            {
                allVertices[i * 3] = meshData.verticesOrg[i].x * scale;
                allVertices[i * 3 + 1] = meshData.verticesOrg[i].y * scale;
                allVertices[i * 3 + 2] = meshData.verticesOrg[i].z * scale;
            }

            DemoInputGeomProvider geom = new DemoInputGeomProvider(allVertices, meshData.trianglesOrg);
            self.navSample = new Sample(geom, null, null);
            self.ResetSampleSettings(self.navSample, scale);

            self._InitNavMeshBuilder();
        }

        public static async ETTask<NavmeshComponent> GetPlayerNavMesh(this NavmeshManagerComponent self)
        {
            while (!self.isLoadMeshFinished)
            {
                if (self.IsDisposed)
                {
                    return null;
                }
                await TimerComponent.Instance.WaitAsync(100);
            }
            if (self.isLoadMeshError)
            {
                return null;
            }
            NavmeshComponent navmeshComponent = self.playerNavmesh;
            if (navmeshComponent != null)
            {
                return navmeshComponent;
            }

            navmeshComponent = self.AddChild<NavmeshComponent>();
            await navmeshComponent.CreateCrowd(self.navSample.GetSettings().agentRadius);
            self.playerNavmesh = navmeshComponent;
            return navmeshComponent;
        }

        public static async ETTask<NavmeshComponent> GetUnitNavmesh(this NavmeshManagerComponent self)
        {
            while (!self.isLoadMeshFinished)
            {
                if (self.IsDisposed)
                {
                    return null;
                }
                await TimerComponent.Instance.WaitAsync(100);
            }
            if (self.isLoadMeshError)
            {
                return null;
            }
            NavmeshComponent navmeshComponent = self.unitNavmesh;
            if (navmeshComponent != null)
            {
                return navmeshComponent;
            }

            navmeshComponent = self.AddChild<NavmeshComponent>();
            await navmeshComponent.CreateCrowd(self.navSample.GetSettings().agentRadius);
            self.unitNavmesh = navmeshComponent;
            return navmeshComponent;
        }

        public static void ResetSampleSettings(this NavmeshManagerComponent self, Sample sample, float scale)
        {
            RcNavMeshBuildSettings rcNavMeshBuildSettings = sample.GetSettings();

            rcNavMeshBuildSettings.agentHeight = 2.0f;
            rcNavMeshBuildSettings.agentRadius = 0.5f;
            rcNavMeshBuildSettings.agentMaxClimb = 1.0f;
            rcNavMeshBuildSettings.agentMaxSlope = 50f;
            rcNavMeshBuildSettings.agentMaxAcceleration = 50f;

            rcNavMeshBuildSettings.tiled = true;
            rcNavMeshBuildSettings.tileSize = 64;
        }

        public static void _InitNavMeshBuilder(this NavmeshManagerComponent self)
        {
            var settings = self.navSample.GetSettings();
            var inputGeom = self.navSample.GetInputGeom();
            NavMeshBuildResult buildResult;

            if (settings.tiled)
            {
                TileNavMeshBuilder builder = new();
                buildResult = builder.Build(inputGeom, settings);
            }
            else
            {
                SoloNavMeshBuilder builder = new();
                buildResult = builder.Build(inputGeom, settings);
            }

            if (!buildResult.Success)
            {
                Log.Error("failed to build");
                self.isLoadMeshFinished = true;
                self.isLoadMeshError = true;
                return;
            }
            RcConfig cfg = new RcConfig(settings.tiled, settings.tileSize, settings.tileSize,
                RcConfig.CalcBorder(settings.agentRadius, settings.cellSize),
                RcPartitionType.OfValue(settings.partitioning),
                settings.cellSize, settings.cellHeight,
                settings.agentMaxSlope, settings.agentHeight, settings.agentRadius, settings.agentMaxClimb,
                settings.minRegionSize, settings.mergedRegionSize,
                settings.edgeMaxLen, settings.edgeMaxError,
                settings.vertsPerPoly,
                settings.detailSampleDist, settings.detailSampleMaxError,
                settings.filterLowHangingObstacles, settings.filterLedgeSpans, settings.filterWalkableLowHeightSpans,
                SampleAreaModifications.SAMPLE_AREAMOD_WALKABLE, true);

            var voxelFile = DtVoxelFile.From(cfg, buildResult.RecastBuilderResults);
            var dynaMesh = new DtDynamicNavMesh(voxelFile);
            dynaMesh.config.keepIntermediateResults = true;
            dynaMesh.config.detailSampleDistance = 6;
            dynaMesh.config.detailSampleMaxError = 1;
            if (!dynaMesh.Build(Task.Factory))
            {
                Log.Error("Failed to build dynamic nav mesh");
                self.isLoadMeshFinished = true;
                self.isLoadMeshError = true;
                return;
            }

            self.dynamicMesh = dynaMesh;
            self.navSample.Update(self.navSample.GetInputGeom(), dynaMesh.RecastResults(), dynaMesh.NavMesh());
            self.navSample.SetChanged(false);
            self.isLoadMeshFinished = true;
            self.isLoadMeshError = false;
        }

        public static DtNavMesh GetNavMesh(this NavmeshManagerComponent self)
        {
            return self.navSample.GetNavMesh();
        }

        public static Sample GetSample(this NavmeshManagerComponent self)
        {
            return self.navSample;
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
                pos.X = rayStart.X + (rayEnd.X - rayStart.X) * hitTime;
                pos.Y = rayStart.Y + (rayEnd.Y - rayStart.Y) * hitTime;
                pos.Z = rayStart.Z + (rayEnd.Z - rayStart.Z) * hitTime;
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
                return (true, new float3(-rcVec3f.X, rcVec3f.Y, rcVec3f.Z));
            }
            else
            {
                return (false, float3.zero);
            }
        }

        public static (bool, float3) ChkHitMesh(this NavmeshManagerComponent self, float3 rayStartIn, float3 rayEndIn)
        {
            float cellSize = self.GetSample().GetSettings().cellSize;
            float cellHeight = self.GetSample().GetSettings().cellHeight;
            var segmentCount = math.ceil(math.distance(rayStartIn, rayEndIn) / math.min(cellSize, cellHeight));

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
                var pos = math.lerp(rayStartIn, rayEndIn, i / segmentCount);
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
                var pos = math.lerp(rayStartIn, rayEndIn, i / segmentCount);
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

            foreach (RcBuilderResult recastBuilderResult in self.GetSample().GetRecastResults())
            {
                RcHeightfield rcHeightfield = recastBuilderResult.SolidHeightfiled;
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

            if (rayPosIn.X >= orig.X && rayPosIn.X < orig.X + w * cs
                && rayPosIn.Z >= orig.Z && rayPosIn.Z < orig.Z + h * cs)
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

            float disX = (rayPosIn.X - orig.X) / cs;
            int indexX = (int)Math.Floor(disX);

            float disZ = (rayPosIn.Z - orig.Z) / cs;
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
                if (rayPosIn.Y >= orig.Y + s.smin * ch && rayPosIn.Y <= orig.Y + s.smax * ch)
                {
                    //Log.Error($"--zpb True rayPosIn[{rayPosIn.Y}] [{orig.Y + s.smin * ch}] [{orig.Y + s.smax * ch}]================");
                    return true;
                }
                else
                {
                    //Log.Error($"--zpb False rayPosIn[{rayPosIn.Y}] [{orig.Y + s.smin * ch}] [{orig.Y + s.smax * ch}]");
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

        public static (bool, (bool isHitMesh, float height)) GetMeshHeightOnPointRecord(this NavmeshManagerComponent self, int x, int y, int z)
        {
            if (self.recordMeshHeightDic.TryGetValue(x, out var dic2))
            {
                if (dic2.TryGetValue(y, out var dic3))
                {
                    if (dic3.TryGetValue(z, out var isHitMesh))
                    {
                        return (true, isHitMesh);
                    }
                }
            }

            return (false, (false, 0));
        }

        public static void RecordMeshHeightOnPoint(this NavmeshManagerComponent self, int x, int y, int z, (bool isHitMesh, float height) info)
        {
            if (self.recordMeshHeightDic.TryGetValue(x, out var dic2) == false)
            {
                dic2 = new();
                self.recordMeshHeightDic[x] = dic2;
            }

            if (dic2.TryGetValue(y, out var dic3) == false)
            {
                dic3 = new();
                dic2[y] = dic3;
            }

            dic3[z] = info;
        }

        private static readonly RcVec3f MPolyPickExt = new RcVec3f(2, 4, 2);
        private static readonly DtQueryDefaultFilter MFilter = new DtQueryDefaultFilter();

        public static NavmeshManagerComponent.NavMeshData GetNavMeshData(this NavmeshManagerComponent self, float3 startPosition)
        {
            if (!self.isLoadMeshFinished || self.isLoadMeshError)
            {
                Log.Warning("NavMesh is not ready.");
                return new();
            }

            long polyRef;
            var query = self.GetSample().GetNavMeshQuery();
            RcVec3f pos = new RcVec3f(-startPosition.x, startPosition.y, startPosition.z);
            if (!query.FindNearestPoly(pos, MPolyPickExt, MFilter, out polyRef, out var _, out var _).Succeeded())
            {
                Log.Warning("Failed to find nearest poly.");
                return new();
            }

            NavmeshManagerComponent.NavMeshData result;
            if (self.navMeshDataDictionary.TryGetValue(polyRef, out result))
            {
                Log.Debug("Found previous nav mesh data.");
                return result;
            }
            result = new();

            var polyCenter = self.navSample.GetNavMesh().GetPolyCenter(polyRef);
            var diameter = (self.GetSample().GetInputGeom().GetMeshBoundsMax() - self.GetSample().GetInputGeom().GetMeshBoundsMin()).Length();
            List<long> polys = new List<long>();
            List<long> resultParent = new List<long>();
            List<float> resultCost = new List<float>();
            List<(DtMeshTile, DtPoly)> tilesAndPolygons = new List<(DtMeshTile, DtPoly)>();
            if (!query.FindPolysAroundCircle(polyRef, polyCenter, diameter, MFilter, ref polys, ref resultParent, ref resultCost).Succeeded())
            {
                Log.Warning("Failed to find polys around circle.");
                return result;
            }
            int totalIndices = 0;
            var comparer = Comparer<float3>.Create((x, y) =>
            {
                if (x.Equals(y)) return 0;
                if (!x.x.Equals(y.x)) return x.x.CompareTo(y.x);
                if (!x.y.Equals(y.y)) return x.y.CompareTo(y.y);
                return x.z.CompareTo(y.z);
            });
            SortedSet<float3> vertices = new SortedSet<float3>(comparer);
            List<long> polyRefs = new List<long>();

            for (int i = 0; i < polys.Count; i++)
            {
                var status = self.navSample.GetNavMesh().GetTileAndPolyByRef(polys[i], out var tile, out var poly);
                if (!status.Succeeded())
                {
                    continue;
                }
                for (int j = 0; j < poly.vertCount; j++)
                {
                    int v = poly.verts[j] * 3;
                    vertices.Add(new float3(-tile.data.verts[v], tile.data.verts[v + 1], tile.data.verts[v + 2]));
                }
                totalIndices += poly.vertCount + 1;
                tilesAndPolygons.Add((tile, poly));
                polyRefs.Add(polys[i]);
            }
            result.PolygonRefs = polyRefs;
            result.Indices = new List<int>(totalIndices);
            result.Vertices = vertices.ToList();
            foreach ((var tile, var poly) in tilesAndPolygons)
            {
                result.Indices.Add(poly.vertCount);
                for (int j = 0; j < poly.vertCount; j++)
                {
                    int v = poly.verts[j] * 3;
                    var vertice = new float3(-tile.data.verts[v], tile.data.verts[v + 1], tile.data.verts[v + 2]);
                    result.Indices.Add(result.Vertices.BinarySearch(0, result.Vertices.Count, vertice, comparer));
                }
            }

            foreach (var polyRefL in polys)
            {
                self.navMeshDataDictionary[polyRefL] = result;
            }
            Log.Debug($"Found NavMesh with {result.Vertices.Count} vertices and {result.Indices.Count} indices for position: {pos}.");
            return result;
        }

        public static (bool isHitMesh, float height) GetMeshHeightOnPoint(this NavmeshManagerComponent self, float3 rayPosIn)
        {
            int x = (int)(rayPosIn.x * 100);
            int y = (int)(rayPosIn.y * 100);
            int z = (int)(rayPosIn.z * 100);

            (bool isRecord, (bool isHitMesh, float height) info) = self.GetMeshHeightOnPointRecord(x, y, z);
            if (isRecord)
            {
                return info;
            }

            RcVec3f rayPos = new RcVec3f(-rayPosIn.x, rayPosIn.y, rayPosIn.z);

            foreach (RcBuilderResult recastBuilderResult in self.GetSample().GetRecastResults())
            {
                RcHeightfield rcHeightfield = recastBuilderResult.SolidHeightfiled;
                try
                {
                    (bool bHit, float height) = self._GetMeshHeightfield(rayPos, rcHeightfield);
                    if (bHit)
                    {
                        self.RecordMeshHeightOnPoint(x, y, z, (true, height));
                        return (true, height);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

            self.RecordMeshHeightOnPoint(x, y, z, (false, 0));
            return (false, 0);
        }

        public static (bool, float) _GetMeshHeightfield(this NavmeshManagerComponent self, RcVec3f rayPosIn, RcHeightfield hf)
        {
            RcVec3f orig = hf.bmin;
            float cs = hf.cs;
            float ch = hf.ch;

            int w = hf.width;
            int h = hf.height;

            if (rayPosIn.X >= orig.X && rayPosIn.X < orig.X + w * cs
                && rayPosIn.Z >= orig.Z && rayPosIn.Z < orig.Z + h * cs)
            {
            }
            else
            {
                return (false, 0);
            }

            float disX = (rayPosIn.X - orig.X) / cs;
            int indexX = (int)Math.Floor(disX);

            float disZ = (rayPosIn.Z - orig.Z) / cs;
            int indexZ = (int)Math.Floor(disZ);

            RcSpan s = null;
            try
            {
                if (indexX + indexZ * w >= hf.spans.Length)
                {
                    return (false, 0);
                }
                s = hf.spans[indexX + indexZ * w];
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            float curHeight = -99999;
            bool bRet = false;
            while (s != null)
            {
                if (rayPosIn.Y >= orig.Y + s.smax * ch)
                {
                    if (bRet == false)
                    {
                        bRet = true;
                        curHeight = orig.Y + s.smax * ch;
                    }
                    else if (curHeight < orig.Y + s.smax * ch)
                    {
                        curHeight = orig.Y + s.smax * ch;
                    }
                }
                s = s.next;
            }

            if (bRet)
            {
                return (true, curHeight);
            }

            return (false, 0);
        }

        public static bool FrustumTest(this NavmeshManagerComponent self, float bmin_x, float bmin_y, float bmin_z, float bmax_x, float bmax_y,
            float bmax_z)
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
            return self.FrustumTest(bmin.X, bmin.Y, bmin.Z, bmax.X, bmax.Y, bmax.Z);
        }

        public static bool UpdateDynamicMesh(this NavmeshManagerComponent self)
        {
            if (self.dynamicMesh != null)
            {
                if (self.dynamicMesh.Update(Task.Factory))
                {
                    self.navSample.Update(self.navSample.GetInputGeom(), self.dynamicMesh.RecastResults(), self.dynamicMesh.NavMesh());
                    NavmeshComponent player = self.playerNavmesh;
                    if (player != null)
                    {
                        NavmeshComponentSystem.OnNavMeshUpdated(player);
                    }
                    NavmeshComponent unit = self.unitNavmesh;
                    if (unit != null) {
                        NavmeshComponentSystem.OnNavMeshUpdated(unit);
                    }
                    
                    // Clear all cached nav mesh data since navmesh is updated.
                    self.navMeshDataDictionary.Clear();
                    return true;
                }
            }
            return false;
        }

        public static long AddObstacle(this NavmeshManagerComponent self, float3 pos, float radius)
        {
            if (self.dynamicMesh == null)
            {
                return -1;
            }
            radius -= self.navSample.GetSettings().agentRadius;
            radius *= 2f;
            if (radius <= 0)
            {
                radius = 0.01f;
            }
            var newPos = RecastHelper.ToRcVec3f(pos);
            RcVec3f extent = new RcVec3f(radius, 2f, radius);
            RcVec3f[] halfEdges = DtBoxCollider.GetHalfEdges(new RcVec3f(0, 1f, 0), new RcVec3f(1f, 0, 0f), extent);

            long id = self.dynamicMesh.AddCollider(new DtBoxCollider(newPos, halfEdges, RcRecast.RC_NULL_AREA,
                self.navSample.GetSettings().agentMaxClimb));
            return id;
        }

        public static void RemoveObstacle(this NavmeshManagerComponent self, long id)
        {
            if (self.dynamicMesh == null)
            {
                return;
            }
            self.dynamicMesh.RemoveCollider(id);
        }

        public static void FixedUpdate(this NavmeshManagerComponent self, float fixedDeltaTime)
        {
            self.UpdateDynamicMesh();
        }
    }
}
