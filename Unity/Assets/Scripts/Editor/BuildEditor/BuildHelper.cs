using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace ET
{
    public static class BuildHelper
    {
        private const string relativeDirPrefix = "../Release";

        public static string BuildFolder = "../Release/{0}/StreamingAssets/";


        [InitializeOnLoadMethod]
        public static void ReGenerateProjectFiles()
        {
            if (Unity.CodeEditor.CodeEditor.CurrentEditor.GetType().Name == "RiderScriptEditor")
            {
                FieldInfo generator = Unity.CodeEditor.CodeEditor.CurrentEditor.GetType().GetField("m_ProjectGeneration", BindingFlags.Static | BindingFlags.NonPublic);
                var syncMethod = generator.FieldType.GetMethod("Sync");
                syncMethod.Invoke(generator.GetValue(Unity.CodeEditor.CodeEditor.CurrentEditor), null);
                Debug.Log("ReGenerateProjectFiles rider finished.");
            }
            else
            {
                Unity.CodeEditor.CodeEditor.CurrentEditor.SyncAll();
                Debug.Log("ReGenerateProjectFiles vs finished.");
            }

        }


#if ENABLE_CODES
        [MenuItem("ET/ChangeDefine/Remove ENABLE_CODES")]
        public static void RemoveEnableCodes()
        {
            EnableDefineSymbols("ENABLE_CODES", false);
        }
#else
        [MenuItem("ET/ChangeDefine/Add ENABLE_CODES")]
        public static void AddEnableCodes()
        {
            EnableDefineSymbols("ENABLE_CODES", true);
        }
#endif

#if ENABLE_VIEW
        [MenuItem("ET/ChangeDefine/Remove ENABLE_VIEW")]
        public static void RemoveEnableView()
        {
            EnableDefineSymbols("ENABLE_VIEW", false);
        }
#else
        [MenuItem("ET/ChangeDefine/Add ENABLE_VIEW")]
        public static void AddEnableView()
        {
            EnableDefineSymbols("ENABLE_VIEW", true);
        }
#endif
        public static void EnableDefineSymbols(string symbolsIn, bool enable)
        {
            Log.Debug($"EnableDefineSymbols {symbolsIn} {enable}");
            var symbolsList = symbolsIn.Split(';').ToList();
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            var ss = defines.Split(';').ToList();
            bool isChg = false;
            for (int i = 0; i < symbolsList.Count; i++)
            {
                string symbols = symbolsList[i];

                if (enable)
                {
                    if (ss.Contains(symbols))
                    {
                        continue;
                    }
                    ss.Add(symbols);
                    isChg = true;
                }
                else
                {
                    if (!ss.Contains(symbols))
                    {
                        continue;
                    }
                    ss.Remove(symbols);
                    isChg = true;
                }
                BuildHelper.ShowNotification($"EnableDefineSymbols {symbols} {enable}");
            }

            if (isChg)
            {
                defines = string.Join(";", ss);
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, defines);
                //AssetDatabase.SaveAssets();
                UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
                AssetDatabase.Refresh();
            }
        }

        public static void ShowNotification(string tips)
        {
            EditorWindow game = EditorWindow.GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView"));
            game?.ShowNotification(new GUIContent($"{tips}"));
        }

        public static void Build(BuildTarget buildTarget, BuildOptions buildOptions, string packName)
        {
            string programName;
            if (string.IsNullOrEmpty(packName))
            {
                programName = "ARGame";
            }
            else
            {
                programName = packName;
            }

            switch (buildTarget)
            {
                case BuildTarget.StandaloneWindows:
                    programName += ".exe";
                    break;
                case BuildTarget.StandaloneWindows64:
                    programName += ".exe";
                    break;
                case BuildTarget.Android:
                    programName += ".apk";
                    break;
                case BuildTarget.iOS:
                    break;
                case BuildTarget.StandaloneOSX:
                    break;
                case BuildTarget.StandaloneLinux64:
                    break;
            }

            AssetDatabase.Refresh();
            string[] levels = {
                "Assets/ResAB/Scene/Init.unity",
            };
            Debug.Log("start buildpack");
            BuildReport buildReport = BuildPipeline.BuildPlayer(levels, $"{relativeDirPrefix}/{programName}", buildTarget, buildOptions);
            BuildSummary summary = buildReport.summary;
            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("----Build succeeded: " + summary.totalSize + " bytes");

                FileInfo fileInfo = new ($"{relativeDirPrefix}/{programName}");
                EditorUtility.RevealInFinder(fileInfo.FullName);
            }
            else if (summary.result == BuildResult.Failed)
            {
                Debug.LogError("----Build failed");
            }
        }


        public static async ETTask BuildModel()
        {
            if (Define.EnableCodes)
            {
                Log.Error("now in ENABLE_CODES mode, do not need Build!");
                return;
            }

            GlobalConfig globalConfig = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            var codeOptimization = globalConfig.codeOptimization;
            await BuildAssembliesHelper.BuildModel(codeOptimization, globalConfig);
        }

        public static async ETTask BuildHotfix()
        {
            if (Define.EnableCodes)
            {
                Log.Error("now in ENABLE_CODES mode, do not need Build!");
                return;
            }

            GlobalConfig globalConfig = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            var codeOptimization = globalConfig.codeOptimization;
            await BuildAssembliesHelper.BuildHotfix(codeOptimization, globalConfig);
        }

        public static async ETTask BuildModelAndHotfix()
        {
            if (Define.EnableCodes)
            {
                string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                Log.Error($"now in ENABLE_CODES mode, do not need Build! [{defines}]");
                return;
            }

            GlobalConfig globalConfig = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            var codeOptimization = globalConfig.codeOptimization;
            await BuildAssembliesHelper.BuildModel(codeOptimization, globalConfig);
            await BuildAssembliesHelper.BuildHotfix(codeOptimization, globalConfig);
        }

        // 从构建命令里获取参数示例
        public static BuildTarget GetBuildTargetFromCommandLine(string buildComman)
        {
            string buildTargetParam = "";
            foreach (string arg in System.Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith(buildComman))
                    buildTargetParam = arg.Split("="[0])[1].Trim();
                break;
            }

            BuildTarget buildTarget = BuildTarget.NoTarget;
            if (string.IsNullOrEmpty(buildTargetParam))
            {
                buildTarget = BuildTarget.NoTarget;
            }
            else if (buildTargetParam == "Android" || buildTargetParam == "android")
            {
                buildTarget = BuildTarget.Android;
            }
            else if (buildTargetParam == "IOS" || buildTargetParam == "iOS")
            {
                buildTarget = BuildTarget.iOS;
            }
            else if (buildTargetParam == "WebGL" || buildTargetParam == "webGL")
            {
                buildTarget = BuildTarget.WebGL;
            }
            else if (buildTargetParam == "StandaloneLinux64" || buildTargetParam == "standaloneLinux64" || buildTargetParam == "StandaloneLinux" || buildTargetParam == "standaloneLinux")
            {
                buildTarget = BuildTarget.StandaloneLinux64;
            }
            else if (buildTargetParam == "PC" || buildTargetParam == "StandaloneWindows64" || buildTargetParam == "standaloneWindows64" || buildTargetParam == "StandaloneWindows" || buildTargetParam == "standaloneWindows")
            {
                buildTarget = BuildTarget.StandaloneWindows64;
            }
            return buildTarget;
        }

        // 从构建命令里获取参数示例
        public static PackName GetBuildPackNameFromCommandLine(string buildComman)
        {
            string buildPackNameParam = "";
            foreach (string arg in System.Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith(buildComman))
                    buildPackNameParam = arg.Split("="[0])[1].Trim();
                break;
            }

            PackName packName = PackName.InNet148;
            if (string.IsNullOrEmpty(buildPackNameParam))
            {
                packName = PackName.InNet148;
            }
            else if (buildPackNameParam == "Local")
            {
                packName = PackName.Local;
            }
            else if (buildPackNameParam == "InNet148")
            {
                packName = PackName.InNet148;
            }
            else if (buildPackNameParam == "OutNet_CN")
            {
                packName = PackName.OutNet_CN;
            }
            else if (buildPackNameParam == "OutNet_EN")
            {
                packName = PackName.OutNet_EN;
            }
            return packName;
        }
    }
}
