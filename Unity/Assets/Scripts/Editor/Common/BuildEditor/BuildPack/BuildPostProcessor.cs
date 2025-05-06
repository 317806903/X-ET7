using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace ET
{
    public class BuildPostProcessor
    {
        [PostProcessBuild(Int32.MaxValue)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.Android)
            {
                //
            }
            else if (target == BuildTarget.iOS)
            {
#if UNITY_IOS
                // Getting access to the xcode project file
                string projectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
                PBXProject pbxProject = new PBXProject();
                pbxProject.ReadFromFile(projectPath);
                // Getting the UnityFramework Target and changing build settings
                string frameworkTargetGuid = pbxProject.GetUnityFrameworkTargetGuid();
                pbxProject.SetBuildProperty(frameworkTargetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
                // After we're done editing the build settings we save it
                pbxProject.WriteToFile(projectPath);
#endif
            }
        }
    }
}