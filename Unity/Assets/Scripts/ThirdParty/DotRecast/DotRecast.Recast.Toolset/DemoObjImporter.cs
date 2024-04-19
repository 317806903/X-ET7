using System;
using System.IO;
using DotRecast.Recast.Toolset.Geom;

namespace DotRecast.Recast.Toolset
{
    public static class DemoObjImporter
    {
        public static DemoInputGeomProvider Load(byte[] chunk)
        {
            var context = ObjImporter.LoadContext(chunk, 1);
            return new DemoInputGeomProvider(context.vertexPositions, context.meshFaces);
        }

        public static DemoInputGeomProvider Load(byte[] chunk, float scale)
        {
            var context = ObjImporter.LoadContext(chunk, scale);
            return new DemoInputGeomProvider(context.vertexPositions, context.meshFaces);
        }
    }
}