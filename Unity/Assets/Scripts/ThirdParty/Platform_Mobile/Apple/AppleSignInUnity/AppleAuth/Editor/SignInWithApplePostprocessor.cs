#if UNITY_EDITOR
#if UNITY_IOS || UNITY_TVOS
#define UNITY_XCODE_EXTENSIONS_AVAILABLE
#endif

using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_XCODE_EXTENSIONS_AVAILABLE
using UnityEditor.iOS.Xcode;
#endif

namespace AppleAuth.Editor
{
    public static class SignInWithApplePostprocessor
    {

        [PostProcessBuild((99999))]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target == BuildTarget.iOS || target == BuildTarget.tvOS)
            {
                #if UNITY_XCODE_EXTENSIONS_AVAILABLE
                    var projectPath = PBXProject.GetPBXProjectPath(path);
                    #if UNITY_2019_3_OR_NEWER
                        var project = new PBXProject();
                        project.ReadFromString(System.IO.File.ReadAllText(projectPath));
                        var manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", null, project.GetUnityMainTargetGuid());
                        manager.AddSignInWithAppleWithCompatibility(project.GetUnityFrameworkTargetGuid());
                        manager.WriteToFile();
                    #else
                        var manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", PBXProject.GetUnityTargetName());
                        manager.AddSignInWithAppleWithCompatibility();
                        manager.WriteToFile();
                    #endif
                #endif
            }
            else if (target == BuildTarget.StandaloneOSX)
            {
                AppleAuthMacosPostprocessorHelper.FixManagerBundleIdentifier(target, path);
            }
        }
    }
}
#endif