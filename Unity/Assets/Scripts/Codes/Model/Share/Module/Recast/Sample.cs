using System.Collections.Generic;
using DotRecast.Detour;
using DotRecast.Recast;
using DotRecast.Recast.Toolset;
using DotRecast.Recast.Toolset.Geom;

namespace ET
{

    public class Sample
    {
        private DemoInputGeomProvider _inputGeom;
        private DtNavMesh _navMesh;
        private DtNavMeshQuery _navMeshQuery;
        private readonly RcNavMeshBuildSettings _settings;
        private IList<RcBuilderResult> _recastResults;
        private bool _changed;

        public Sample(DemoInputGeomProvider inputGeom, IList<RcBuilderResult> recastResults, DtNavMesh navMesh)
        {
            _inputGeom = inputGeom;
            _recastResults = recastResults;
            _navMesh = navMesh;
            _settings = new RcNavMeshBuildSettings();

            SetQuery(navMesh);
            _changed = true;
        }

        private void SetQuery(DtNavMesh navMesh)
        {
            _navMeshQuery = navMesh != null? new DtNavMeshQuery(navMesh) : null;
        }

        public DemoInputGeomProvider GetInputGeom()
        {
            return _inputGeom;
        }

        public IList<RcBuilderResult> GetRecastResults()
        {
            return _recastResults;
        }

        public DtNavMesh GetNavMesh()
        {
            return _navMesh;
        }

        public RcNavMeshBuildSettings GetSettings()
        {
            return _settings;
        }

        public DtNavMeshQuery GetNavMeshQuery()
        {
            return _navMeshQuery;
        }

        public bool IsChanged()
        {
            return _changed;
        }

        public void SetChanged(bool changed)
        {
            _changed = changed;
        }

        public void Update(DemoInputGeomProvider geom, IList<RcBuilderResult> recastResults, DtNavMesh navMesh)
        {
            _inputGeom = geom;
            _recastResults = recastResults;
            _navMesh = navMesh;
            SetQuery(navMesh);

            _changed = true;

            // // by update
            // _inputGeom.ClearConvexVolumes();
            // _inputGeom.RemoveOffMeshConnections(x => true);
            //
            // if (null != _navMesh && 0 < _navMesh.GetTileCount())
            // {
            //     for (int ti = 0; ti < _navMesh.GetTileCount(); ++ti)
            //     {
            //         var tile = _navMesh.GetTile(ti);
            //         for (int pi = 0; pi < tile.data.polys.Length; ++pi)
            //         {
            //             var polyType = tile.data.polys[pi].GetPolyType();
            //             var polyArea= tile.data.polys[pi].GetArea();
            //
            //             if (0 != polyType)
            //             {
            //                 int a = 3;
            //             }
            //
            //             if (0 != polyArea)
            //             {
            //                 int b = 3;
            //             }
            //             
            //             Console.WriteLine($"tileIdx({ti}) polyIdx({pi}) polyType({polyType} polyArea({polyArea})");
            //         }
            //         
            //     }
            // }
        }
    }
}