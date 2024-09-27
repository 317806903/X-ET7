using System;
using System.Collections.Generic;
using DotRecast.Detour;
using DotRecast.Detour.Crowd;
using DotRecast.Recast.Toolset;
using DotRecast.Recast.Toolset.Builder;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof(GamePlayComponent))]
    public class NavmeshManagerComponent: Entity, IAwake, IDestroy
    {
        public struct RecastFileLoader
        {
            public string Name { get; set; }
        }

        public byte[] objBytes;
        public MeshHelper.MeshData meshData;
        public Dictionary<float, NavmeshComponent> NavmeshByRadius;
        public NavmeshComponent playerNavmesh;

        public bool isLoadMeshFinished;
        public bool isLoadMeshError;
        public DtNavMesh m_nav;
        public Sample _sample;

        public SoloNavMeshBuilder soloNavMeshBuilder;
        public TileNavMeshBuilder tileNavMeshBuilder;

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
    }
}