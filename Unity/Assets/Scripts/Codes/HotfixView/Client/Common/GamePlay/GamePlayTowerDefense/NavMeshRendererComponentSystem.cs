using ET.Ability;
using ET.AbilityConfig;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
namespace ET.Client
{
    public static class NavMeshRendererComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<NavMeshRendererComponent>
        {
            protected override void Awake(NavMeshRendererComponent self)
            {
                NavMeshRendererComponent.Instance = self;
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_NavMeshRenderer");
                GameObject navMeshRenderer = ResComponent.Instance.LoadAsset<GameObject>(resEffectCfg.ResName);
                navMeshRenderer = GameObject.Instantiate(navMeshRenderer);
                self.navMeshRendererRoot = navMeshRenderer.transform;
                self.navMeshRendererItem = navMeshRenderer.transform.Find("NavMeshRendererItem");
                self.wireframeRendererItem = navMeshRenderer.transform.Find("WireframeRendererItem");
                navMeshRenderer.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
                self.ResetPos();
                self.MeshFilter = self.navMeshRendererItem.GetComponent<MeshFilter>();
                self.wireframeMeshFilter = self.wireframeRendererItem.GetComponent<MeshFilter>();
                self.navMeshRendererRoot.gameObject.SetActive(false);
                self.MeshFilter.sharedMesh = null;
                self.navmeshDataDict = new ();
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<NavMeshRendererComponent>
        {
            protected override void Destroy(NavMeshRendererComponent self)
            {
                if (self.navMeshRendererRoot != null)
                {
                    GameObject.Destroy(self.navMeshRendererRoot.gameObject);
                }
                NavMeshRendererComponent.Instance = null;
            }
        }

        public static void ShowMesh(this NavMeshRendererComponent self, bool isShow)
        {
            if (self.navMeshRendererRoot != null)
            {
                if (isShow)
                {
                    // 获取基地的位置，赋值给波浪
                    long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
                    GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
                    TeamFlagType teamFlag = gamePlayTowerDefenseComponent.GetHomeTeamFlagTypeByPlayer(myPlayerId);
                    PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                    Unit homeUnit = putHomeComponent.GetHomeUnit(teamFlag);
                    if (homeUnit != null)
                    {
                        WorldPositionSetter ps = self.navMeshRendererItem.GetComponent<WorldPositionSetter>();
                        ps.originPosition = homeUnit.Position;
                    }
                }

                if (self.navMeshRendererRoot.gameObject.activeSelf != isShow)
                {
                    self.navMeshRendererRoot.gameObject.SetActive(isShow);
                }
            }
        }

        public static void UpdatePolygonsFromPoints(this NavMeshRendererComponent self, List<List<Vector3>> lines, Mesh mesh)
        {
            int totalCount = lines.Select(points => points.Count).Sum();

            // Vertices, prev, next, direction, triangles
            var verticies = new Vector3[totalCount * 2];
            var prevs = new Vector3[totalCount * 2];
            var nexts = new Vector3[totalCount * 2];
            var data = new Vector2[totalCount * 2];

            var triangles = new int[(totalCount - 1) * 9]; // This over allocates I think?

            int lineStart = 0;

            foreach (var points in lines)
            {
                int lineIndex = lineStart * 2;
                // Set first element
                verticies[lineIndex + 0] = points[0];
                verticies[lineIndex + 1] = points[0];
                prevs[lineIndex + 0] = points[0];
                prevs[lineIndex + 1] = points[0];
                nexts[lineIndex + 0] = points[1];
                nexts[lineIndex + 1] = points[1];
                data[lineIndex + 0] = new Vector2(1, 1);
                data[lineIndex + 1] = new Vector2(-1, 1);

                // Set last element
                int lp = points.Count - 1;
                int l = lineIndex + 2 * lp;
                verticies[l + 0] = points[lp];
                verticies[l + 1] = points[lp];
                prevs[l + 0] = points[lp - 1];
                prevs[l + 1] = points[lp - 1];
                nexts[l + 0] = points[lp];
                nexts[l + 1] = points[lp];
                data[l + 0] = new Vector2(1, 2);
                data[l + 1] = new Vector2(-1, 2);

                // Set all but first and last
                for (int i = 1; i < points.Count - 1; ++i)
                {
                    int b = lineIndex + i * 2;

                    verticies[b + 0] = points[i];
                    verticies[b + 1] = points[i];

                    prevs[b + 0] = points[i - 1];
                    prevs[b + 1] = points[i - 1];

                    nexts[b + 0] = points[i + 1];
                    nexts[b + 1] = points[i + 1];

                    data[b + 0] = new Vector2(1, 0);
                    data[b + 1] = new Vector2(-1, 0);
                }

                for (int i = 0; i < points.Count - 1; ++i)
                {
                    int b = (lineStart + i) * 9;
                    int t = lineIndex + i * 2;

                    triangles[b + 0] = t + 3;
                    triangles[b + 1] = t + 1;
                    triangles[b + 2] = t + 0;

                    triangles[b + 3] = t + 2;
                    triangles[b + 4] = t + 1;
                    triangles[b + 5] = t + 0;

                    triangles[b + 6] = t + 0;
                    triangles[b + 7] = t + 2;
                    triangles[b + 8] = t + 3;
                }
                lineStart += points.Count;
            }

            mesh.SetVertices(verticies);
            mesh.SetUVs(1, prevs);
            mesh.SetUVs(2, nexts);
            mesh.SetUVs(3, data);
            mesh.SetTriangles(triangles, 0);
        }

        public static async ETTask<bool> ShowNavMeshFromPos(this NavMeshRendererComponent self, float3 pos)
        {
            (var polyRef, var _) = await GamePlayTowerDefenseHelper.GetNearestNavmeshPos(self.ClientScene(), pos);
            if (self.IsDisposed)
            {
                self.SetNavMesh(null);
                return false;
            }
            if (polyRef == 0)
            {
                self.SetNavMesh(null);
                return false;
            }
            if (self.navmeshDataDict.TryGetValue(polyRef, out NavmeshManagerComponent.NavMeshData navMeshData))
            {
                return self.SetNavMesh(navMeshData);
            }
            var data = await GamePlayTowerDefenseHelper.RequestReachableAreaFromPosition(self.ClientScene(), pos);
            if (self.IsDisposed)
            {
                self.SetNavMesh(null);
                return false;
            }
            foreach (long polyRefL in data.PolygonRefs)
            {
                self.navmeshDataDict[polyRefL] = data;
            }
            return self.SetNavMesh(data);
        }

        public static void ResetPos(this NavMeshRendererComponent self)
        {
            self.navMeshRendererRoot.position = Vector3.zero;
            self.navMeshRendererRoot.eulerAngles = Vector3.zero;
        }

        public static void Clear(this NavMeshRendererComponent self)
        {
            self.navmeshDataDict.Clear();
        }

        public static bool SetNavMesh(this NavMeshRendererComponent self, NavmeshManagerComponent.NavMeshData navMeshData)
        {
            if (self.navMeshRendererRoot == null)
            {
                return false;
            }

            if (navMeshData == null || navMeshData.Vertices.Count == 0 || navMeshData.Indices.Count == 0)
            {
                self.MeshFilter.sharedMesh = null;
                return false;
            }

            Mesh mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            var vertices = new Vector3[navMeshData.Vertices.Count];
            for (int i = 0; i < navMeshData.Vertices.Count; i++)
            {
                float3 Vertices = navMeshData.Vertices[i];
                Vertices = ET.Ability.UnitHelper.TranServerPos2ClientPos(self.DomainScene(), Vertices);
                vertices[i] = Vertices;
            }
            mesh.vertices = vertices;

            List<List<Vector3>> polygons = ListComponent<List<Vector3>>.Create();
            List<int> triangles = ListComponent<int>.Create();
            for (int i = 0; i < navMeshData.Indices.Count; i++)
            {
                List<Vector3> polygon = ListComponent<Vector3>.Create();
                int count = navMeshData.Indices[i];

                polygon.Add(vertices[navMeshData.Indices[i + 1]]);
                for (int j = i + 2; j < i + count; j++)
                {
                    triangles.Add(navMeshData.Indices[i + 1]);
                    triangles.Add(navMeshData.Indices[j + 1]);
                    triangles.Add(navMeshData.Indices[j]);
                    polygon.Add(vertices[navMeshData.Indices[j]]);
                }
                polygon.Add(vertices[navMeshData.Indices[i + count]]);
                polygon.Add(vertices[navMeshData.Indices[i + 1]]);
                polygons.Add(polygon);
                i += count;
            }
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
            mesh.RecalculateUVDistributionMetric(0);
            mesh.RecalculateBounds();
            mesh.Optimize();
            mesh.UploadMeshData(false);
            self.MeshFilter.sharedMesh = mesh;

            Mesh polygonMesh = new Mesh();
            self.UpdatePolygonsFromPoints(polygons, polygonMesh);
            self.wireframeMeshFilter.sharedMesh = polygonMesh;
            return true;
        }
    }
}
