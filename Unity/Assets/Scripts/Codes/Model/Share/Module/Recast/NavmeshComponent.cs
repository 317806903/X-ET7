using System;
using System.Collections.Generic;
using DotRecast.Detour;
using DotRecast.Detour.Crowd;
using DotRecast.Recast.Toolset;
using DotRecast.Recast.Toolset.Builder;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof(NavmeshManagerComponent))]
    public class NavmeshComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public DtCrowd crowd;
        public float radius;
        public List<float3> arrivePath = new();
        public int waitFrameSyncPos = 0;
        public int curFrameSyncPos = 0;
        public Dictionary<int, Dictionary<int, Dictionary<int, float3>>> recordNearestPosDic;
        public Dictionary<int, Dictionary<int, Dictionary<int, long>>> recordNearestRefDic;
    }
}