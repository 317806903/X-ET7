using System;
using System.IO;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using YooAsset;

namespace ET
{
    public enum PlatformType
    {
        None,
        Android,
        IOS,
        Windows,
        MacOS,
        Linux
    }

    public enum ConfigFolder
    {
        Localhost,
        Release,
        RouterTest,
        Benchmark
    }

    public enum BuildType
    {
        Development,
        Release,
    }

    public class BuildEditor: EditorWindow
    {
        private PlatformType activePlatform;
        private PlatformType platformType;
        private ConfigFolder configFolder;
        private bool clearFolder;
        private bool isBuildExe;
        private bool isContainAB;
        private string fairyGUIXMLPath;
        private BuildOptions buildOptions;
        private BuildAssetBundleOptions buildAssetBundleOptions = BuildAssetBundleOptions.None;

        private GlobalConfig globalConfig;
        private ResConfig resConfig;

        [MenuItem("ET/Build Tool")]
        public static void ShowWindow()
        {
            GetWindow<BuildEditor>(DockDefine.Types);
        }

        private void OnEnable()
        {
            globalConfig = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            resConfig = AssetDatabase.LoadAssetAtPath<ResConfig>("Assets/Resources/ResConfig.asset");

#if UNITY_ANDROID
            activePlatform = PlatformType.Android;
#elif UNITY_IOS
			activePlatform = PlatformType.IOS;
#elif UNITY_STANDALONE_WIN
			activePlatform = PlatformType.Windows;
#elif UNITY_STANDALONE_OSX
			activePlatform = PlatformType.MacOS;
#elif UNITY_STANDALONE_LINUX
			activePlatform = PlatformType.Linux;
#else
			activePlatform = PlatformType.None;
#endif
            platformType = activePlatform;
        }

        private void OnGUI()
        {
            this.platformType = (PlatformType) EditorGUILayout.EnumPopup(platformType);
            this.clearFolder = EditorGUILayout.Toggle("clean folder? ", clearFolder);
            this.isBuildExe = EditorGUILayout.Toggle("build exe?", this.isBuildExe);
            this.isBuildExe = true;

            this.isContainAB = EditorGUILayout.Toggle("contain assetsbundle?", this.isContainAB);
            var codeOptimization = (CodeOptimization) EditorGUILayout.EnumPopup("CodeOptimization ", this.globalConfig.codeOptimization);
            if (codeOptimization != this.globalConfig.codeOptimization)
            {
                this.globalConfig.codeOptimization = codeOptimization;
                EditorUtility.SetDirty(this.globalConfig);
                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.LabelField("BuildAssetBundleOptions ");
            this.buildAssetBundleOptions = (BuildAssetBundleOptions) EditorGUILayout.EnumFlagsField(this.buildAssetBundleOptions);

            switch (codeOptimization)
            {
                case CodeOptimization.None:
                case CodeOptimization.Debug:
                    this.buildOptions = BuildOptions.Development | BuildOptions.ConnectWithProfiler;
                    break;
                case CodeOptimization.Release:
                    this.buildOptions = BuildOptions.None;
                    break;
            }

            GUILayout.Space(5);

            if (GUILayout.Button("BuildPackage"))
            {
                EditorApplication.isPlaying = false;
                if (this.platformType == PlatformType.None)
                {
                    ShowNotification(new GUIContent("please select platform!"));
                    return;
                }

                if (platformType != activePlatform)
                {
                    switch (EditorUtility.DisplayDialogComplex("Warning!",
                        $"current platform is {activePlatform}, if change to {platformType}, may be take a long time", "change", "cancel",
                        "no change"))
                    {
                        case 0:
                            activePlatform = platformType;
                            break;
                        case 1:
                            return;
                        case 2:
                            platformType = activePlatform;
                            break;
                    }
                }

                BuildHelper.Build(this.platformType, this.buildAssetBundleOptions, this.buildOptions, this.isBuildExe, this.isContainAB,
                    this.clearFolder);
            }

            GUILayout.Label("");
            
            var resLoadMode = (EPlayMode) EditorGUILayout.EnumPopup("ResLoadMode: ", this.resConfig.ResLoadMode);
            if (resLoadMode != this.resConfig.ResLoadMode)
            {
                this.resConfig.ResLoadMode = resLoadMode;
                EditorUtility.SetDirty(this.resConfig);
                AssetDatabase.SaveAssets();
            }
            GUILayout.Space(5);
            
            GUILayout.Label("Code Compile：");

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("EnableCodes Status", GUILayout.Width(200f));
#if ENABLE_CODES
                if (GUILayout.Button("Remove EnableCodes"))
                {
                    EditorApplication.isPlaying = false;
                    BuildHelper.EnableDefineSymbols("ENABLE_CODES", false);
                }
#else
                if (GUILayout.Button("Add EnableCodes"))
                {
                    EditorApplication.isPlaying = false;
                    BuildHelper.EnableDefineSymbols("ENABLE_CODES", true);
                    if (CodeMode.ClientServer != this.globalConfig.CodeMode)
                    {
                        this.globalConfig.CodeMode = CodeMode.ClientServer;
                        EditorUtility.SetDirty(this.globalConfig);
                        AssetDatabase.SaveAssets();
                    }
                }
#endif
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            var codeMode = (CodeMode) EditorGUILayout.EnumPopup("CodeMode: ", this.globalConfig.CodeMode);
            if (codeMode != this.globalConfig.CodeMode)
            {
                this.globalConfig.CodeMode = codeMode;
                EditorUtility.SetDirty(this.globalConfig);
                AssetDatabase.SaveAssets();
            }
            GUILayout.Space(5);

            if (GUILayout.Button("BuildModelAndHotfix"))
            {
                EditorApplication.isPlaying = false;
                BuildModelAndHotfix();
            }

            if (GUILayout.Button("BuildModel"))
            {
                EditorApplication.isPlaying = false;
                BuildModel();
            }

            if (GUILayout.Button("BuildHotfix"))
            {
                EditorApplication.isPlaying = false;
                BuildHotfix();
            }

            GUILayout.Space(5);
            if (GUILayout.Button("Proto2CS"))
            {
                ToolsEditor.Proto2CS();
            }

            GUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            {
                this.configFolder = (ConfigFolder) EditorGUILayout.EnumPopup(this.configFolder, GUILayout.Width(200f));
                if (this.configFolder.ToString() != this.globalConfig.StartConfig)
                {
                    this.globalConfig.StartConfig = this.configFolder.ToString();
                    EditorUtility.SetDirty(this.globalConfig);
                    AssetDatabase.SaveAssets();
                }

                if (GUILayout.Button("ExcelExporter"))
                {
                    ToolsEditor.ExcelExporter(globalConfig.CodeMode, this.configFolder);

                    string unityClientConfigForAB = "../Unity/Assets/Bundles/Config/GameConfig";
                    if (Directory.Exists(unityClientConfigForAB))
                    {
                        Directory.Delete(unityClientConfigForAB, true);
                    }

                    FileHelper.CopyDirectory("../Config/Excel/c/GameConfig", unityClientConfigForAB);

                    unityClientConfigForAB = "../Unity/Assets/Bundles/Config/AbilityConfig";
                    if (Directory.Exists(unityClientConfigForAB))
                    {
                        Directory.Delete(unityClientConfigForAB, true);
                    }

                    FileHelper.CopyDirectory("../Config/Excel/c/AbilityConfig", unityClientConfigForAB);

                    unityClientConfigForAB = $"../Unity/Assets/Bundles/Config/StartConfig";
                    if (Directory.Exists(unityClientConfigForAB))
                    {
                        Directory.Delete(unityClientConfigForAB, true);
                    }

                    FileHelper.CopyDirectory($"../Config/Excel/c/StartConfig/{this.configFolder.ToString()}",
                        unityClientConfigForAB + $"/{this.configFolder.ToString()}");

                    AssetDatabase.Refresh();
                }
            }
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(5);
            GUILayout.Label("");
            // GUILayout.Label("FairyGUI");
            // GUIContent guiContent = new GUIContent("FairyGUI语言文件XML路径：", "在 FairyGUI 里生成");
            // EditorGUI.BeginChangeCheck();
            // string xmlPath = EditorGUILayout.TextField(guiContent, fairyGUIXMLPath);
            // if (EditorGUI.EndChangeCheck())
            // {
            // 	fairyGUIXMLPath = xmlPath;
            // }
            //
            // if (GUILayout.Button("导出 FairyGUI 多语言"))
            // {
            // 	if (FUICodeSpawner.Localize(fairyGUIXMLPath))
            // 	{
            // 		ShowNotification("FairyGUI 多语言导出成功！");
            // 	}
            // 	else
            // 	{
            // 		ShowNotification("FairyGUI 多语言导出失败！");
            // 	}
            // }
            //
            // GUILayout.Space(5);
            // if (GUILayout.Button("FUI代码生成"))
            // {
            // 	FUICodeSpawner.FUICodeSpawn();
            // 	ShowNotification("FUI代码生成成功！");
            // }
        }

        private static void AfterCompiling()
        {
            Directory.CreateDirectory(BuildAssembliesHelper.CodeDir);

            // 设置ab包
            AssetImporter assetImporter = AssetImporter.GetAtPath("Assets/Bundles/Code");
            assetImporter.assetBundleName = "Code.unity3d";
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("build success!");
        }

        public static void ShowNotification(string tips)
        {
            EditorWindow game = EditorWindow.GetWindow(typeof (ET.BuildEditor).Assembly.GetType("ET.BuildEditor"));
            game?.ShowNotification(new GUIContent($"{tips}"));
        }

        public static void BuildModel()
        {
            if (Define.EnableCodes)
            {
                throw new Exception("now in ENABLE_CODES mode, do not need Build!");
            }

            GlobalConfig globalConfig = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            var codeOptimization = globalConfig.codeOptimization;
            BuildAssembliesHelper.BuildModel(codeOptimization, globalConfig);

            AfterCompiling();

            ShowNotification("Build Model Success!");
        }

        public static void BuildHotfix()
        {
            if (Define.EnableCodes)
            {
                throw new Exception("now in ENABLE_CODES mode, do not need Build!");
            }

            GlobalConfig globalConfig = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            var codeOptimization = globalConfig.codeOptimization;
            BuildAssembliesHelper.BuildHotfix(codeOptimization, globalConfig);

            AfterCompiling();
            ShowNotification("Build Hotfix Success!");
        }

        public static void BuildModelAndHotfix()
        {
            if (Define.EnableCodes)
            {
                throw new Exception("now in ENABLE_CODES mode, do not need Build!");
            }

            GlobalConfig globalConfig = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            var codeOptimization = globalConfig.codeOptimization;
            BuildAssembliesHelper.BuildModel(codeOptimization, globalConfig);
            BuildAssembliesHelper.BuildHotfix(codeOptimization, globalConfig);

            AfterCompiling();

            ShowNotification("Build Model And Hotfix Success!");
        }
    }
}