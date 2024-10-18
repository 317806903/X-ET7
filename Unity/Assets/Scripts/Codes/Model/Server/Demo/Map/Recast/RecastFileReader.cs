using System.IO;

namespace ET.Server
{
    [Invoke]
    public class RecastFileReader: AInvokeHandler<NavmeshManagerComponent.RecastFileLoader, byte[]>
    {
        public override byte[] Handle(NavmeshManagerComponent.RecastFileLoader args)
        {
            //return File.ReadAllBytes(Path.Combine("../Config/Recast", args.Name));
            return ToBytes(Path.Combine("../Config/Recast", args.Name));
        } 
        
        private static byte[] ToBytes(string filename)
        {
            var filepath = FindParentPath(filename);
            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        private static string FindParentPath(string filename)
        {
            string filePath = Path.Combine("resources", filename);
            for (int i = 0; i < 10; ++i)
            {
                if (File.Exists(filePath))
                {
                    return Path.GetFullPath(filePath);
                }
                filePath = Path.Combine("..", filePath);
            }
            return Path.GetFullPath(filename);
        }
    }
}