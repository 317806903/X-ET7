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
        public List<float3> arrivePath = new();
    }
}