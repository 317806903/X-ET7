using System;
using UnityEditor;
using UnityEngine;
using YooAsset;
using Debug = UnityEngine.Debug;

namespace ET
{
    public enum PackName
    {
        Local,
        InNet148,
        InNetZpb,
        OutNet_Benchmark,
        OutNet_CN,
        OutNet_EN,
        OutNet_EN_AAB,

        ExternalTest,  // External test for US users.
        ExternalTest_AAB
    }

    public static class BuildPack
    {
        private static CodeMode lastCodeMode;
        private static CodeOptimization lastCodeOptimization;

        private static bool lastEnableView;
        private static bool lastEnableCodes;

        private static YooAsset.EPlayMode lastResLoadMode;
        private static string lastResHostServerIP;
        private static string lastResGameVersion;
        private static string lastRouterHttpHost;
        private static int lastRouterHttpPort;

        private static string lastProductName;
        private static string lastPackageName;

        [MenuItem("Pack/BuildPack_Android_Local", false, 300)]
        public static async ETTask BuildPack_Android_Local()
        {
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if(System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_Local", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.Local);
        }

        [MenuItem("Pack/BuildPack_Android_InNet148", false, 301)]
        public static async ETTask BuildPack_Android_InNet148()
        {
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if(System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_InNet148", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.InNet148);
        }

        [MenuItem("Pack/BuildPack_Android_InNetZpb", false, 302)]
        public static async ETTask BuildPack_Android_InNetZpb()
        {
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if (System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_InNetZpb", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.InNetZpb);
        }

        [MenuItem("Pack/BuildPack_Android_OutNet_Benchmark", false, 303)]
        public static async ETTask BuildPack_Android_OutNet_Benchmark()
        {
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if(System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_OutNet_Benchmark", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.OutNet_Benchmark);
        }


        [MenuItem("Pack/BuildPack_Android_OutNet_CN", false, 304)]
        public static async ETTask BuildPack_Android_OutNet_CN()
        {
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if (System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_OutNet_CN", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.OutNet_CN);
        }

        [MenuItem("Pack/BuildPack_Android_OutNet_EN", false, 305)]
        public static async ETTask BuildPack_Android_OutNet_EN()
        {
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if(System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_OutNet_EN", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.OutNet_EN);
        }

        [MenuItem("Pack/BuildPack_Android_OutNet_EN_AAB", false, 306)]
        public static async ETTask BuildPack_Android_OutNet_EN_AAB()
        {
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if (System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_OutNet_EN_AAB", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.OutNet_EN_AAB);
        }

        [MenuItem("Pack/BuildPack_Android_ExternalTest", false, 307)]
        public static async ETTask BuildPack_Android_ExternalTest()
        {
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if (System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_ExternalTest", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.ExternalTest);
        }

        [MenuItem("Pack/BuildPack_Android_ExternalTest_AAB", false, 308)]
        public static async ETTask BuildPack_Android_ExternalTest_AAB()
        {
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if (System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_ExternalTest_AAB", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.ExternalTest_AAB);
        }

        [MenuItem("Pack/BuildPack_IOS_InNet148", false, 321)]
        public static async ETTask BuildPack_IOS_InNet148()
        {
#if !UNITY_EDITOR_OSX
            EditorUtility.DisplayDialog("警告", "只能mac下运行", "确定");
            return;
#endif
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if(System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_InNet148", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            await BuildPackInternal(buildTarget, PackName.InNet148);
        }

        [MenuItem("Pack/BuildPack_IOS_InNetZpb", false, 322)]
        public static async ETTask BuildPack_IOS_InNetZpb()
        {
#if !UNITY_EDITOR_OSX
            EditorUtility.DisplayDialog("警告", "只能mac下运行", "确定");
            return;
#endif
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if (System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_InNetZpb", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            await BuildPackInternal(buildTarget, PackName.InNetZpb);
        }

        [MenuItem("Pack/BuildPack_IOS_OutNet_Benchmark", false, 323)]
        public static async ETTask BuildPack_IOS_OutNet_Benchmark()
        {
#if !UNITY_EDITOR_OSX
            EditorUtility.DisplayDialog("警告", "只能mac下运行", "确定");
            return;
#endif
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if(System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_OutNet_Benchmark", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            await BuildPackInternal(buildTarget, PackName.OutNet_Benchmark);
        }

        [MenuItem("Pack/BuildPack_IOS_OutNet_CN", false, 324)]
        public static async ETTask BuildPack_IOS_OutNet_CN()
        {
#if !UNITY_EDITOR_OSX
            EditorUtility.DisplayDialog("警告", "只能mac下运行", "确定");
            return;
#endif
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if (System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_OutNet_CN", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            await BuildPackInternal(buildTarget, PackName.OutNet_CN);
        }

        [MenuItem("Pack/BuildPack_IOS_OutNet_EN", false, 325)]
        public static async ETTask BuildPack_IOS_OutNet_EN()
        {
#if !UNITY_EDITOR_OSX
            EditorUtility.DisplayDialog("警告", "只能mac下运行", "确定");
            return;
#endif
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if(System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_OutNet_EN", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            await BuildPackInternal(buildTarget, PackName.OutNet_EN);
        }

        [MenuItem("Pack/BuildPack_IOS_ExternalTest", false, 326)]
        public static async ETTask BuildPack_IOS_ExternalTest()
        {
#if !UNITY_EDITOR_OSX
            EditorUtility.DisplayDialog("警告", "只能mac下运行", "确定");
            return;
#endif
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            if (System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return;
            }

            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_ExternalTest", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            await BuildPackInternal(buildTarget, PackName.ExternalTest);
        }


        public static void BuildPack_CommandLine()
        {
            BuildTarget buildTarget = BuildHelper.GetBuildTargetFromCommandLine("BuildPack");
            PackName packName = BuildHelper.GetBuildPackNameFromCommandLine("PackName");
            BuildPackInternal(buildTarget, packName).Coroutine();
        }

        public static async ETTask BuildPackInternal(BuildTarget buildTarget, PackName packName)
        {
            EditorApplication.isPlaying = false;

            await InitInstance();
            await BuildPack_Pre(packName);
            try
            {
                AssetDatabase.Refresh();
                await BuildInternal(buildTarget);
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            await BuildPack_After(packName);
        }

        private static async ETTask InitInstance()
        {
            GlobalConfig.Instance = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            ResConfig.Instance = AssetDatabase.LoadAssetAtPath<ResConfig>("Assets/Resources/ResConfig.asset");
        }

        private static async ETTask BuildPack_Pre(PackName packName)
        {
            EditorUserBuildSettings.buildAppBundle = false;
            lastCodeMode = GlobalConfig.Instance.CodeMode;
            lastCodeOptimization = GlobalConfig.Instance.codeOptimization;
            if (packName == PackName.Local)
            {
                GlobalConfig.Instance.CodeMode = CodeMode.ClientServer;
            }
            else
            {
                GlobalConfig.Instance.CodeMode = CodeMode.Client;
            }
            if (false)
            {
                GlobalConfig.Instance.codeOptimization = CodeOptimization.Debug;
            }
            else
            {
                GlobalConfig.Instance.codeOptimization = CodeOptimization.Release;
            }

            lastEnableView = Define.EnableView;
            lastEnableCodes = Define.EnableCodes;

            ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

            lastResLoadMode = ResConfig.Instance.ResLoadMode;
            lastResHostServerIP = ResConfig.Instance.ResHostServerIP;
            lastResGameVersion = ResConfig.Instance.ResGameVersion;
            lastRouterHttpHost = ResConfig.Instance.RouterHttpHost;
            lastRouterHttpPort = ResConfig.Instance.RouterHttpPort;

            //UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.DevelopmentOnly;
            UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
            ResConfig.Instance.IsNeedSendEventLog = false;
            string productName = $"RealityGuard";
            string packageName = $"com.dm.ARGame";

            string MirrorARSessionAuthAppKey = "63f72bdc21c28002a4f4b218#mirrorsdk_dev";
            string MirrorARSessionAuthAppSecret = "79395951c7cf78bd7ec1fe5b5b1c97ce";
            // ============== Development environments (Office/Benchmark)=================================
            ResConfig.Instance.MirrorARSessionAuthAppKey = MirrorARSessionAuthAppKey;
            ResConfig.Instance.MirrorARSessionAuthAppSecret = MirrorARSessionAuthAppSecret;
            if (packName == PackName.Local)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;
                ResConfig.Instance.areaType = AreaType.CN;
                ResConfig.Instance.IsShowDebugMode = true;
                ResConfig.Instance.IsShowEditorLoginMode = true;
                productName = $"Local_RealityGuard";
                packageName = $"com.dm.ARGameLocal";
            }
            else if(packName == PackName.InNet148)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "http://192.168.10.148";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "192.168.10.148";
                ResConfig.Instance.RouterHttpPort = 30300;
                ResConfig.Instance.areaType = AreaType.CN;
                productName = $"148_RealityGuard";
                packageName = $"com.dm.ARGameInNet148";
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
            }
            else if(packName == PackName.InNetZpb)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "http://192.168.10.58";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "192.168.10.58";
                ResConfig.Instance.RouterHttpPort = 30300;
                ResConfig.Instance.areaType = AreaType.CN;
                ResConfig.Instance.IsShowDebugMode = true;
                ResConfig.Instance.IsShowEditorLoginMode = true;
                productName = $"Zpb_RealityGuard";
                packageName = $"com.dm.ARGameInNetZpb";
                UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
            }
            else if (packName == PackName.OutNet_Benchmark)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;
                ResConfig.Instance.RouterHttpHost = "18.166.14.188";   // Linux machine in AWS HK.
                ResConfig.Instance.RouterHttpPort = 30300;
                ResConfig.Instance.areaType = AreaType.CN;
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                productName = $"RealityGuard_Benchmark";
                packageName = $"com.dm.ARGameBenchmark";
            }
            // ============== Staging environments (Aliyun/AWS) =================================
            else if (packName == PackName.OutNet_CN)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "8.134.156.170";
                ResConfig.Instance.RouterHttpPort = 30300;
                ResConfig.Instance.areaType = AreaType.CN;
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                productName = $"RealityGuard_CN";
                packageName = $"com.dm.ARGameCN";
            }
            else if(packName == PackName.OutNet_EN || packName == PackName.OutNet_EN_AAB)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame_EN";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "34.225.211.137";
                ResConfig.Instance.RouterHttpPort = 30300;
                ResConfig.Instance.areaType = AreaType.EN;
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                productName = $"RealityGuard_EN";
                packageName = $"com.dm.ARGameEN";

                if (packName == PackName.OutNet_EN_AAB)
                {
                    // Build AAB
                    EditorUserBuildSettings.buildAppBundle = true;
                    productName = $"RealityGuard";
                    packageName = $"com.dm.realityguard";
                }
            }

            // ============== Production environments ===========================================
            else if (packName == PackName.ExternalTest || packName == PackName.ExternalTest_AAB)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "https://prod-us-sv-aws-artd-deepmirror-s3.oss-us-west-1.aliyuncs.com/resources";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "artd-gateway.deepmirror.com";
                ResConfig.Instance.RouterHttpPort = 30300;
                ResConfig.Instance.areaType = AreaType.EN;
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                ResConfig.Instance.IsNeedSendEventLog = true;  // Log events from real users.
                ResConfig.Instance.MirrorARSessionAuthAppKey = "63f72bdc21c28002a4f4b218#artd-test";
                ResConfig.Instance.MirrorARSessionAuthAppSecret = "8c1b80ac5da324652e08b3c75d3494ee";
                productName = $"RealityGuard";
                packageName = $"com.dm.realityguard";

                if (packName == PackName.ExternalTest_AAB)
                {
                    // Build AAB
                    EditorUserBuildSettings.buildAppBundle = true;
                }
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguard.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguard";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
            }

            lastProductName = PlayerSettings.productName;
            lastPackageName = PlayerSettings.applicationIdentifier;
            PlayerSettings.companyName = "DeepMirror";
            PlayerSettings.productName = productName;
            PlayerSettings.applicationIdentifier = packageName;

            PlayerSettings.bundleVersion = "1.0";
            int buildVersionCode = GetBuildVersionCode();
            PlayerSettings.Android.bundleVersionCode = buildVersionCode;
            PlayerSettings.iOS.buildNumber = buildVersionCode.ToString();

            // This requires Unity Pro account to build the package to have effect.
            PlayerSettings.SplashScreen.showUnityLogo = false;

            EditorUtility.SetDirty(ResConfig.Instance);
            AssetDatabase.SaveAssets();
        }

        private static int GetBuildVersionCode()
        {
            var dt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int buildVersionCode = (int)((DateTime.UtcNow.Ticks - dt.Ticks) / 10000 / 1000);
            return buildVersionCode;
        }

        private static async ETTask BuildPack_After(PackName packName)
        {
            GlobalConfig.Instance.CodeMode = lastCodeMode;
            GlobalConfig.Instance.codeOptimization = lastCodeOptimization;

            ResConfig.Instance.ResLoadMode = lastResLoadMode;
            ResConfig.Instance.ResHostServerIP = lastResHostServerIP;
            ResConfig.Instance.ResGameVersion = lastResGameVersion;
            ResConfig.Instance.RouterHttpHost = lastRouterHttpHost;
            ResConfig.Instance.RouterHttpPort = lastRouterHttpPort;

            PlayerSettings.productName = lastProductName;
            PlayerSettings.applicationIdentifier = lastPackageName;

            EditorUserBuildSettings.buildAppBundle = false;
        }

        private static string GetBuildPackageVersion()
        {
            int totalMinutes = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
            return DateTime.Now.ToString("yyyy-MM-dd") + "_" + totalMinutes;
        }

        private static async ETTask BuildInternal(BuildTarget buildTarget)
        {
            Debug.Log($"开始构建 : {buildTarget}");

            BuildOptions buildOptions = BuildOptions.None;

            switch (GlobalConfig.Instance.codeOptimization)
            {
                case CodeOptimization.None:
                case CodeOptimization.Debug:
                    buildOptions = BuildOptions.Development | BuildOptions.ConnectWithProfiler;
                    break;
                case CodeOptimization.Release:
                    buildOptions = BuildOptions.None;
                    break;
            }

            // Use the last part of package name as file name.
            string appName = PlayerSettings.applicationIdentifier["com.dm.".Length..];

            string packFileName = $"{appName}_{GetBuildPackageVersion()}";
            BuildHelper.Build(buildTarget, buildOptions, packFileName);
            //Debug.Log($"构建成功");
        }
    }
}