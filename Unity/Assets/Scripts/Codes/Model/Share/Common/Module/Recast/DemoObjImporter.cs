using DotRecast.Core;
using DotRecast.Recast.Toolset.Geom;

namespace ET
{
    public static class DemoObjImporter
    {
        public static DemoInputGeomProvider Load(byte[] chunk)
        {
            var context = RcObjImporter.LoadContext(chunk);
            return new DemoInputGeomProvider(context.vertexPositions, context.meshFaces);
        }

        public static DemoInputGeomProvider Load(byte[] chunk, float scale)
        {
            var context = RcObjImporter.LoadContext(chunk);

            // Scale the vertices
            for (int i = 0; i < context.vertexPositions.Count; i += 3)
            {
                context.vertexPositions[i] *= scale;
                context.vertexPositions[i + 1] *= scale;
                context.vertexPositions[i + 2] *= scale;
            }

            return new DemoInputGeomProvider(context.vertexPositions, context.meshFaces);
        }
    }
}