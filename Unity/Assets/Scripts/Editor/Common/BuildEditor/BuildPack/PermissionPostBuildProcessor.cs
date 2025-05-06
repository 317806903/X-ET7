using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;
using UnityEditor.Android;
using UnityEngine;

namespace ET
{
    public class PermissionPostBuildProcessor : IPostGenerateGradleAndroidProject
    {
        public int callbackOrder { get { return 0; } }
        public void OnPostGenerateGradleAndroidProject(string path)
        {
            string manifestPath = GetManifestFilePath(path);
            if (File.Exists(manifestPath))
            {
                string manifestContent = File.ReadAllText(manifestPath);
                bool isChg = false;
                if (!manifestContent.Contains("android.permission.VIBRATE"))
                {
                    // 添加权限
                    manifestContent = manifestContent.Replace("<application", "<uses-permission android:name=\"android.permission.VIBRATE\" />\n<application");
                    isChg = true;
                }
#if !Platform_Quest
                if (!manifestContent.Contains("android.permission.CAMERA"))
                {
                    // 添加权限
                    manifestContent = manifestContent.Replace("<application", "<uses-permission android:name=\"android.permission.CAMERA\" />\n<application");
                    isChg = true;
                }
#else
                if (!manifestContent.Contains("horizonos.permission.HEADSET_CAMERA"))
                {
                    // 添加权限
                    manifestContent = manifestContent.Replace("<application", "<uses-permission android:name=\"horizonos.permission.HEADSET_CAMERA\" />\n<application");
                    isChg = true;
                }
#endif
                if (isChg)
                {
                    File.WriteAllText(manifestPath, manifestContent);
                }
            }
        }

        private string CombinePaths(string[] paths) {
            var path = "";
            foreach (var item in paths) {
                path = Path.Combine(path, item);
            }
            return path;
        }

        private string GetManifestFilePath(string root) {
            string[] comps = {root, "src", "main", "AndroidManifest.xml"};
            return CombinePaths(comps);
        }
    }
}