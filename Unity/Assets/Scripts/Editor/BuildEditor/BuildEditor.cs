using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.SceneManagement;
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

    public enum BuildType
    {
        Development,
        Release,
    }

    public class BuildEditor: EditorWindow
    {
        private PlatformType activePlatform;
        private PlatformType platformType;

        private GlobalConfig globalConfig;
        private ResConfig resConfig;

        private int localHostStartConfigIndex = 1;
        private int selectStartConfigIndex = 1;
        private string[] startConfigs;
        private string startConfig;
        private string selectStartConfigServerIP;

        [MenuItem("ET/Build Tool")]
        public static void ShowWindow()
        {
            GetWindow<BuildEditor>(DockDefine.Types);
        }

        public void Refresh()
        {
            globalConfig = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            resConfig = AssetDatabase.LoadAssetAtPath<ResConfig>("Assets/Resources/ResConfig.asset");
            DirectoryInfo directoryInfo = new DirectoryInfo("Assets/Config/Excel/StartConfig");
            this.startConfigs = directoryInfo.GetDirectories().Select(x => x.Name).ToArray();

            for (int i = 0; i < this.startConfigs.Length; i++)
            {
                if (this.startConfigs[i] == this.globalConfig.StartConfig)
                {
                    this.selectStartConfigIndex = i;
                }
                if (this.startConfigs[i] == "Localhost")
                {
                    this.localHostStartConfigIndex = i;
                }
            }
            this.selectStartConfigServerIP = GetServerIP(this.globalConfig.CodeMode, this.globalConfig.StartConfig);
        }

        private void OnEnable()
        {
            this.Refresh();

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

            GUILayout.Label("====================================");
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Code Compile：", GUILayout.Width(200f));
                if (GUILayout.Button("ResetLocalEditor"))
                {
                    if (this.globalConfig.CodeMode != CodeMode.ClientServer)
                    {
                        this.globalConfig.CodeMode = CodeMode.ClientServer;
                        EditorUtility.SetDirty(this.globalConfig);
                        AssetDatabase.SaveAssets();
                    }
                    if (this.globalConfig.StartConfig != "Localhost")
                    {
                        this.globalConfig.StartConfig = "Localhost";
                        EditorUtility.SetDirty(this.globalConfig);
                        AssetDatabase.SaveAssets();
                    }
                    if (this.resConfig.ResLoadMode != EPlayMode.EditorSimulateMode)
                    {
                        this.resConfig.ResLoadMode = EPlayMode.EditorSimulateMode;
                        EditorUtility.SetDirty(this.resConfig);
                        AssetDatabase.SaveAssets();
                    }

                    EditorSceneManager.OpenScene("Assets/ResAB/Scene/Init.unity");
                    BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", true);

                    var tmp = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("ProjectSettings/ProjectSettings.asset");
                    EditorUtility.SetDirty(tmp);
                    AssetDatabase.SaveAssets();

                    this.Refresh();
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Label("====================================");
            GUILayout.Space(5);

            var codeOptimization = (CodeOptimization) EditorGUILayout.EnumPopup("CodeOptimization ", this.globalConfig.codeOptimization);
            if (codeOptimization != this.globalConfig.codeOptimization)
            {
                this.globalConfig.codeOptimization = codeOptimization;
                EditorUtility.SetDirty(this.globalConfig);
                AssetDatabase.SaveAssets();
            }

            GUILayout.Label("");

            var resLoadMode = (EPlayMode) EditorGUILayout.EnumPopup("ResLoadMode: ", this.resConfig.ResLoadMode);
            if (resLoadMode != this.resConfig.ResLoadMode)
            {
                this.resConfig.ResLoadMode = resLoadMode;
                EditorUtility.SetDirty(this.resConfig);
                AssetDatabase.SaveAssets();
            }
            if (resLoadMode == EPlayMode.HostPlayMode)
            {
                EditorGUI.BeginChangeCheck();
                string resHostIp = EditorGUILayout.TextField("资源热更新地址:", this.resConfig.ResHostServerIP);
                if (EditorGUI.EndChangeCheck())
                {
                    if (resHostIp != this.resConfig.ResHostServerIP)
                    {
                        this.resConfig.ResHostServerIP = resHostIp;
                        EditorUtility.SetDirty(this.resConfig);
                        AssetDatabase.SaveAssets();
                    }
                }
            }
            GUILayout.Space(5);


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
                    if (EPlayMode.EditorSimulateMode != this.resConfig.ResLoadMode)
                    {
                        this.resConfig.ResLoadMode = EPlayMode.EditorSimulateMode;
                        EditorUtility.SetDirty(this.resConfig);
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
                BuildModelAndHotfix().Coroutine();
            }

            if (GUILayout.Button("BuildModel"))
            {
                EditorApplication.isPlaying = false;
                BuildModel().Coroutine();
            }

            if (GUILayout.Button("BuildHotfix"))
            {
                EditorApplication.isPlaying = false;
                BuildHotfix().Coroutine();
            }

            GUILayout.Space(5);
            if (GUILayout.Button("Proto2CS"))
            {
                ToolsEditor.Proto2CS();
            }

            GUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            {
                selectStartConfigIndex = EditorGUILayout.Popup("服务器模式:", selectStartConfigIndex, this.startConfigs, GUILayout.MinWidth(150));
                if (this.startConfigs[selectStartConfigIndex] != this.globalConfig.StartConfig)
                {
                    this.globalConfig.StartConfig = this.startConfigs[selectStartConfigIndex];
                    EditorUtility.SetDirty(this.globalConfig);
                    AssetDatabase.SaveAssets();
                    this.selectStartConfigServerIP = GetServerIP(codeMode, this.globalConfig.StartConfig);
                }
                if (string.IsNullOrEmpty(this.selectStartConfigServerIP))
                {
                    this.selectStartConfigServerIP = GetServerIP(codeMode, this.globalConfig.StartConfig);
                }

                if (GUILayout.Button("ExcelExporter"))
                {
                    ToolsEditor.ExcelExporter(globalConfig.CodeMode, this.startConfigs[selectStartConfigIndex], ToolsEditor.ConfigType.All);

                    string unityClientConfigForAB = "../Unity/Assets/Bundles/Config/AbilityConfig";
                    if (Directory.Exists(unityClientConfigForAB))
                    {
                        Directory.Delete(unityClientConfigForAB, true);
                    }

                    FileHelper.CopyDirectory("../Config/Excel/c/AbilityConfig", unityClientConfigForAB);

                    unityClientConfigForAB = $"../Unity/Assets/Bundles/Config/StartConfig";
                    if (Directory.Exists(unityClientConfigForAB + $"/{this.startConfigs[selectStartConfigIndex]}"))
                    {
                        Directory.Delete(unityClientConfigForAB + $"/{this.startConfigs[selectStartConfigIndex]}", true);
                    }

                    FileHelper.CopyDirectory($"../Config/Excel/c/StartConfig/{this.startConfigs[selectStartConfigIndex]}", unityClientConfigForAB + $"/{this.startConfigs[selectStartConfigIndex]}");

                    AssetDatabase.Refresh();
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("对应服务器地址:", this.selectStartConfigServerIP, GUILayout.MinHeight(120));


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

        private static string GetServerIP(CodeMode codeMode, string StartConfigPath)
        {
            string ct = "cs";
            switch (codeMode)
            {
                case CodeMode.Client:
                    ct = "c";
                    break;
                case CodeMode.Server:
                    ct = "s";
                    break;
                case CodeMode.ClientServer:
                    ct = "cs";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            string configFile = "StartMachineConfigCategory";
            string configFilePath = $"../Config/Json/{ct}/StartConfig/{StartConfigPath}/{configFile.ToLower()}.json";
            if (File.Exists(configFilePath) == false)
            {
                string err = $"--------configFilePath[{configFilePath}] not exists";
                Debug.LogError(err);
                return err;
            }
            return File.ReadAllText(configFilePath);
        }

        private static void AfterCompiling()
        {
            // Directory.CreateDirectory(BuildAssembliesHelper.CodeDir);
            //
            // // 设置ab包
            // AssetImporter assetImporter = AssetImporter.GetAtPath("Assets/Bundles/Code");
            // assetImporter.assetBundleName = "Code.unity3d";
            // AssetDatabase.SaveAssets();
            // AssetDatabase.Refresh();

            Debug.Log("build success!");
        }

        public static void ShowNotification(string tips)
        {
            EditorWindow game = EditorWindow.GetWindow(typeof (ET.BuildEditor).Assembly.GetType("ET.BuildEditor"));
            game?.ShowNotification(new GUIContent($"{tips}"));
        }

        public static async ETTask BuildModel()
        {
            if (Define.EnableCodes)
            {
                Debug.LogError("now in ENABLE_CODES mode, do not need Build!");
                return;
            }

            await ET.BuildHelper.BuildModel();

            AfterCompiling();

            ShowNotification("Build Model Success!");
        }

        public static async ETTask BuildHotfix()
        {
            if (Define.EnableCodes)
            {
                Debug.LogError("now in ENABLE_CODES mode, do not need Build!");
                return;
            }

            await ET.BuildHelper.BuildHotfix();

            AfterCompiling();
            ShowNotification("Build Hotfix Success!");
        }

        public static async ETTask BuildModelAndHotfix()
        {
            if (Define.EnableCodes)
            {
                Debug.LogError("now in ENABLE_CODES mode, do not need Build!");
                return;
            }

            await ET.BuildHelper.BuildModelAndHotfix();

            AfterCompiling();

            ShowNotification("Build Model And Hotfix Success!");
        }
    }
}