using System.Collections.Generic;
using System.IO;
using HybridCLR.Editor;
using HybridCLR.Editor.Settings;
using UnityEditor;

namespace ET
{
    public static class HybridCLREditor
    {
        [MenuItem("HybridCLR/CopyAotDlls")]
        public static void CopyAotDll()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            string fromDir = Path.Combine(HybridCLRSettings.Instance.strippedAOTDllOutputRootDir, target.ToString());
            string toDir = "Assets/Bundles/AotDlls";
            if (Directory.Exists(toDir))
            {
                Directory.Delete(toDir, true);
            }
            Directory.CreateDirectory(toDir);
            AssetDatabase.Refresh();

            foreach (string aotDll in HybridCLRSettings.Instance.patchAOTAssemblies)
            {
                //Google Admob的dll分Android和iOS
                if(File.Exists(Path.Combine(fromDir, aotDll)))
                {
                    File.Copy(Path.Combine(fromDir, aotDll), Path.Combine(toDir, $"{aotDll}.bytes"), true);
                }
            }

            // 设置ab包
            // AssetImporter assetImporter = AssetImporter.GetAtPath(toDir);
            // assetImporter.assetBundleName = "AotDlls.unity3d";
            // AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}