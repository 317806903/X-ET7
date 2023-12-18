using System;
using System.Collections.Generic;
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

    public enum ServerEnum
    {
        Localhost,
        Release_148,
        Release_Zpb,
        Release_OutNet_CN,
        Release_OutNet_EN,
        Release_ExternalTest,
    }

    public class BuildEditor: EditorWindow
    {
        private Dictionary<ServerEnum, string> serverAddressList = new()
        {
            { ServerEnum.Localhost, "127.0.0.1"},
            { ServerEnum.Release_148, "192.168.10.148"},
            { ServerEnum.Release_Zpb, "192.168.10.58"},
            { ServerEnum.Release_OutNet_CN, "8.134.156.170"},
            { ServerEnum.Release_OutNet_EN, "34.225.211.137"},
            { ServerEnum.Release_ExternalTest, "artd-gateway.deepmirror.com"},
        };
        private Dictionary<ServerEnum, string> hotfixAddressList = new()
        {
            { ServerEnum.Localhost, "http://127.0.0.1"},
            { ServerEnum.Release_148, "http://192.168.10.148"},
            { ServerEnum.Release_Zpb, "http://192.168.10.58"},
            { ServerEnum.Release_OutNet_CN, "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame"},
            { ServerEnum.Release_OutNet_EN, "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame_EN"},
            { ServerEnum.Release_ExternalTest, "https://prod-us-sv-aws-artd-deepmirror-s3.oss-us-west-1.aliyuncs.com/resources"},
        };

        private ServerEnum serverEnumServerAddress;
        private ServerEnum serverEnumHotfixAddress;

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

        public void RefreshBase()
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

            if (EditorApplication.isPlayingOrWillChangePlaymode == false)
            {
                this.serverEnumServerAddress = ServerEnum.Localhost;
                this.serverEnumHotfixAddress = ServerEnum.Localhost;
            }
        }

        private void OnEnable()
        {
            this.RefreshBase();

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
                    if (this.globalConfig.NeedDB != false)
                    {
                        this.globalConfig.NeedDB = false;
                        EditorUtility.SetDirty(this.globalConfig);
                        AssetDatabase.SaveAssets();
                    }
                    if (this.resConfig.IsNeedSendEventLog != false)
                    {
                        this.resConfig.IsNeedSendEventLog = false;
                        EditorUtility.SetDirty(this.resConfig);
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
                this.serverEnumHotfixAddress = (ServerEnum) EditorGUILayout.EnumPopup("ServerEnum: ", this.serverEnumHotfixAddress);
                if (this.hotfixAddressList[this.serverEnumHotfixAddress] != this.resConfig.ResHostServerIP)
                {
                    this.resConfig.ResHostServerIP = this.hotfixAddressList[this.serverEnumHotfixAddress];
                    EditorUtility.SetDirty(this.resConfig);
                    AssetDatabase.SaveAssets();
                }
                // EditorGUILayout.LabelField("    资源热更新地址:", this.serverAdressList[this.serverEnum]);

                EditorGUI.BeginChangeCheck();
                string resHostIp = EditorGUILayout.TextField("    资源热更新地址:", this.resConfig.ResHostServerIP);
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
            var NeedDB = EditorGUILayout.Toggle("NeedDB: ", this.globalConfig.NeedDB);
            if (NeedDB != this.globalConfig.NeedDB)
            {
                this.globalConfig.NeedDB = NeedDB;
                EditorUtility.SetDirty(this.globalConfig);
                AssetDatabase.SaveAssets();
            }
            var IsNeedSendEventLog = EditorGUILayout.Toggle("IsNeedSendEventLog: ", this.resConfig.IsNeedSendEventLog);
            if (IsNeedSendEventLog != this.resConfig.IsNeedSendEventLog)
            {
                this.resConfig.IsNeedSendEventLog = IsNeedSendEventLog;
                EditorUtility.SetDirty(this.resConfig);
                AssetDatabase.SaveAssets();
            }
            if (GUILayout.Button("创建Mongodb (需要Docker安装了)"))
            {
                ToolsEditor.RunMongoDBFromDocker();
            }
            GUILayout.Space(5);
            var areaType = (AreaType) EditorGUILayout.EnumPopup("AreaType: ", this.resConfig.areaType);
            if (areaType != this.resConfig.areaType)
            {
                this.resConfig.areaType = areaType;
                EditorUtility.SetDirty(this.resConfig);
                AssetDatabase.SaveAssets();
            }
            GUILayout.Space(5);
            this.serverEnumServerAddress = (ServerEnum) EditorGUILayout.EnumPopup("ServerEnum: ", this.serverEnumServerAddress);
            if (this.serverAddressList[this.serverEnumServerAddress] != this.resConfig.RouterHttpHost)
            {
                this.resConfig.RouterHttpHost = this.serverAddressList[this.serverEnumServerAddress];
                EditorUtility.SetDirty(this.resConfig);
                AssetDatabase.SaveAssets();
            }
            EditorGUILayout.LabelField("    对应服务器地址:", this.serverAddressList[this.serverEnumServerAddress]);
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

                if (GUILayout.Button("StartConfigExporter"))
                {
                    ToolsEditor.ExcelExporter(globalConfig.CodeMode, this.startConfigs[selectStartConfigIndex], ToolsEditor.ConfigType.StartConfig, "False");

                    string unityClientConfigForAB = $"../Unity/Assets/Bundles/Config/StartConfig";
                    if (Directory.Exists(unityClientConfigForAB + $"/{this.startConfigs[selectStartConfigIndex]}"))
                    {
                        Directory.Delete(unityClientConfigForAB + $"/{this.startConfigs[selectStartConfigIndex]}", true);
                    }

                    FileHelper.CopyDirectory($"../Config/Excel/c/StartConfig/{this.startConfigs[selectStartConfigIndex]}", unityClientConfigForAB + $"/{this.startConfigs[selectStartConfigIndex]}");

                    AssetDatabase.Refresh();
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("服务器模式预览:", this.selectStartConfigServerIP, GUILayout.MinHeight(120));


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
            string ct = "s";
            // switch (codeMode)
            // {
            //     case CodeMode.Client:
            //         ct = "c";
            //         break;
            //     case CodeMode.Server:
            //         ct = "s";
            //         break;
            //     case CodeMode.ClientServer:
            //         ct = "cs";
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
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