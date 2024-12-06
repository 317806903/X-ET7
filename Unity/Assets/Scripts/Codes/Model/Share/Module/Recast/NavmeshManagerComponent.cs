using DotRecast.Detour.Dynamic;
using DotRecast.Recast.Toolset.Tools;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using Unity.Mathematics;
namespace ET
{
    [ComponentOf(typeof(GamePlayComponent))]
    public class NavmeshManagerComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public struct RecastFileLoader
        {
            public string Name { get; set; }
        }
        public EntityRef<NavmeshComponent> unitNavmesh;
        public EntityRef<NavmeshComponent> playerNavmesh;
        [BsonIgnore]
        public RcTestNavMeshTool navMeshTool;

        public bool isLoadMeshFinished;
        public bool isLoadMeshError;

        [BsonIgnore]
        public DtDynamicNavMesh dynamicMesh;

        [BsonIgnore]
        public Sample navSample;

        public List<float3> segPoints;

        public readonly float[][] frustumPlanes =
        {
            new[] { 0f, 0f, 0f, 0f },
            new[] { 0f, 0f, 0f, 0f },
            new[] { 0f, 0f, 0f, 0f },
            new[] { 0f, 0f, 0f, 0f },
            new[] { 0f, 0f, 0f, 0f },
            new[] { 0f, 0f, 0f, 0f },
        };

        public Dictionary<int, Dictionary<int, Dictionary<int, bool>>> recordMeshHitDic;
        public Dictionary<int, Dictionary<int, Dictionary<int, (bool, float)>>> recordMeshHeightDic;
        
        public class NavMeshData
        {
            public List<float3> Vertices { get; set; }
            // Indices of vertices of the nav mesh polygons, each polygon contains n + 1 numbers, where the first number is the number of vertices of
            // the polygon, and the following n numbers are the indices of the vertices. 
            public List<int> Indices { get; set; }
            public List<long> PolygonRefs { get; set; }
        }

        // Dictionary that stores (Polygon Ref -> NavMeshData) pairs.
        public Dictionary<long, NavMeshData> navMeshDataDictionary;
    }
}