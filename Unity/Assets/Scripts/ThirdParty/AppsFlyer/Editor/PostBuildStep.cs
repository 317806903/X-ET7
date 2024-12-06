#if UNITY_EDITOR
#if UNITY_IOS || UNITY_TVOS
#define UNITY_XCODE_EXTENSIONS_AVAILABLE
#endif
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_XCODE_EXTENSIONS_AVAILABLE
using UnityEditor.iOS.Xcode;
#endif
using System.IO;
 
public static class PostBuildStep{
    // Set the IDFA request description:
    const string k_TrackingDescription = "Your data will be used to provide you a better and personalized ad experience.";
 
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode) {
        if (buildTarget == BuildTarget.iOS) {
            AddFramework(pathToXcode);
            AddPListValues(pathToXcode);
        }
    }
 
    // Implement a function to read and write values to the plist file:
    static void AddPListValues(string pathToXcode) {
        #if UNITY_XCODE_EXTENSIONS_AVAILABLE
            // Retrieve the plist file from the Xcode project directory:
            string plistPath = pathToXcode + "/Info.plist";
            PlistDocument plistObj = new PlistDocument();
    
    
            // Read the values from the plist file:
            plistObj.ReadFromString(File.ReadAllText(plistPath));
    
            // Set values from the root object:
            PlistElementDict plistRoot = plistObj.root;
    
            // Set the description key-value in the plist:
            plistRoot.SetString("NSUserTrackingUsageDescription", k_TrackingDescription);
            plistRoot.SetString("NSAdvertisingAttributionReportEndpoint", "https://appsflyer-skadnetwork.com/");

            plistRoot.SetBoolean("AppsFlyerShouldSwizzle", true);
    
            // Save changes to the plist:
            File.WriteAllText(plistPath, plistObj.WriteToString());
        #endif
    }

    static void AddFramework(string pathToXcode) {
        #if UNITY_XCODE_EXTENSIONS_AVAILABLE
            var projectPath = PBXProject.GetPBXProjectPath(pathToXcode);
            var project = new PBXProject();
            project.ReadFromString(System.IO.File.ReadAllText(projectPath));
            project.AddFrameworkToProject(project.GetUnityFrameworkTargetGuid(), "AppTrackingTransparency.framework", false);
            project.WriteToFile(projectPath);
        #endif
    }
}
#endif