using System.IO;
using DotRecast.Core;

namespace ET.Server
{
    [Invoke]
    public class RecastFileReader: AInvokeHandler<NavmeshManagerComponent.RecastFileLoader, byte[]>
    {
        public override byte[] Handle(NavmeshManagerComponent.RecastFileLoader args)
        {
            //return File.ReadAllBytes(Path.Combine("../Config/Recast", args.Name));
            return Loader.ToBytes(Path.Combine("../Config/Recast", args.Name));
        }
    }
}