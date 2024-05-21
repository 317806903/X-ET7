using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using YooAsset;
using Debug = UnityEngine.Debug;

namespace ET
{
    public enum PackName
    {
        Local,
        InNetGDC,
        MetaQuest3,
        AppleVisionPro,
        InNet148Master,
        InNet148Release,
        InNetZpb,
        OutNet_Benchmark,
        OutNet_Arcade,
        OutNet_CN,
        OutNet_CN_Demo,
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
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_Local", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_Local", () =>
            {
                BuildPackInternal(buildTarget, PackName.Local).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_InNetGDC", false, 301)]
        public static async ETTask BuildPack_Android_InNetGDC()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_InNetGDC", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_InNetGDC", () =>
            {
                BuildPackInternal(buildTarget, PackName.InNetGDC).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_InNet148Master", false, 301)]
        public static async ETTask BuildPack_Android_InNet148Master()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_InNet148Master", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_InNet148Master", () =>
            {
                BuildPackInternal(buildTarget, PackName.InNet148Master).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_InNet148Release", false, 301)]
        public static async ETTask BuildPack_Android_InNet148Release()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_InNet148Release", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_InNet148Release", () =>
            {
                BuildPackInternal(buildTarget, PackName.InNet148Release).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_InNetZpb", false, 302)]
        public static async ETTask BuildPack_Android_InNetZpb()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_InNetZpb", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_InNetZpb", () =>
            {
                BuildPackInternal(buildTarget, PackName.InNetZpb).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_OutNet_Benchmark", false, 303)]
        public static async ETTask BuildPack_Android_OutNet_Benchmark()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_OutNet_Benchmark", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_OutNet_Benchmark", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_Benchmark).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_OutNet_Arcade", false, 303)]
        public static async ETTask BuildPack_Android_OutNet_Arcade()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_OutNet_Arcade", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_OutNet_Arcade", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_Arcade).Coroutine();
            });
        }


        [MenuItem("Pack/BuildPack_Android_OutNet_CN", false, 304)]
        public static async ETTask BuildPack_Android_OutNet_CN()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_OutNet_CN", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_OutNet_CN", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_CN).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_OutNet_CN_Demo", false, 304)]
        public static async ETTask BuildPack_Android_OutNet_CN_Demo()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_OutNet_CN_Demo", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_OutNet_CN_Demo", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_CN_Demo).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_OutNet_EN", false, 305)]
        public static async ETTask BuildPack_Android_OutNet_EN()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_OutNet_EN", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_OutNet_EN", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_EN).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_OutNet_EN_AAB", false, 306)]
        public static async ETTask BuildPack_Android_OutNet_EN_AAB()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_OutNet_EN_AAB", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_OutNet_EN_AAB", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_EN_AAB).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_ExternalTest", false, 307)]
        public static async ETTask BuildPack_Android_ExternalTest()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_ExternalTest", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_ExternalTest", () =>
            {
                BuildPackInternal(buildTarget, PackName.ExternalTest).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_ExternalTest_AAB", false, 308)]
        public static async ETTask BuildPack_Android_ExternalTest_AAB()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_ExternalTest_AAB", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_ExternalTest_AAB", () =>
            {
                BuildPackInternal(buildTarget, PackName.ExternalTest_AAB).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_Android_MetaQuest3", false, 309)]
        public static async ETTask BuildPack_Android_MetaQuest3()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_MetaQuest3", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_MetaQuest3", () =>
            {
                BuildPackInternal(buildTarget, PackName.MetaQuest3).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_InNetGDC", false, 321)]
        public static async ETTask BuildPack_IOS_InNetGDC()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_InNetGDC", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_InNetGDC", () =>
            {
                BuildPackInternal(buildTarget, PackName.InNetGDC).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_InNet148Master", false, 321)]
        public static async ETTask BuildPack_IOS_InNet148Master()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_InNet148Master", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_InNet148Master", () =>
            {
                BuildPackInternal(buildTarget, PackName.InNet148Master).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_InNet148Release", false, 321)]
        public static async ETTask BuildPack_IOS_InNet148Release()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_InNet148Release", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_InNet148Release", () =>
            {
                BuildPackInternal(buildTarget, PackName.InNet148Release).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_InNetZpb", false, 322)]
        public static async ETTask BuildPack_IOS_InNetZpb()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_InNetZpb", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_InNetZpb", () =>
            {
                BuildPackInternal(buildTarget, PackName.InNetZpb).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_OutNet_Benchmark", false, 323)]
        public static async ETTask BuildPack_IOS_OutNet_Benchmark()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_OutNet_Benchmark", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_OutNet_Benchmark", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_Benchmark).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_OutNet_Arcade", false, 323)]
        public static async ETTask BuildPack_IOS_OutNet_Arcade()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_OutNet_Arcade", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_OutNet_Arcade", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_Arcade).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_OutNet_CN", false, 324)]
        public static async ETTask BuildPack_IOS_OutNet_CN()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_OutNet_CN", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_OutNet_CN", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_CN).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_OutNet_CN_Demo", false, 324)]
        public static async ETTask BuildPack_IOS_OutNet_CN_Demo()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_OutNet_CN_Demo", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_OutNet_CN_Demo", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_CN_Demo).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_OutNet_EN", false, 325)]
        public static async ETTask BuildPack_IOS_OutNet_EN()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_OutNet_EN", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_OutNet_EN", () =>
            {
                BuildPackInternal(buildTarget, PackName.OutNet_EN).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_ExternalTest", false, 326)]
        public static async ETTask BuildPack_IOS_ExternalTest()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_ExternalTest", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_ExternalTest", () =>
            {
                BuildPackInternal(buildTarget, PackName.ExternalTest).Coroutine();
            });
        }

        [MenuItem("Pack/BuildPack_IOS_AppleVisionPro", false, 327)]
        public static async ETTask BuildPack_IOS_AppleVisionPro()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_AppleVisionPro", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_AppleVisionPro", () =>
            {
                BuildPackInternal(buildTarget, PackName.AppleVisionPro).Coroutine();
            });
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

            SetBuildVersionCode();
            SetBuildPackageVersion();

            await InitInstance();
            await BuildPack_Pre(packName, buildTarget);
            try
            {
                AssetDatabase.Refresh();
                await BuildInternal(packName, buildTarget);
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            await BuildPack_After(packName);
        }

        private static async ETTask InitInstance()
        {
            GlobalConfig.Instance = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            ResConfig.Instance = AssetDatabase.LoadAssetAtPath<ResConfig>("Assets/Resources/ResConfig.asset");
        }

        private static async ETTask BuildPack_Pre(PackName packName, BuildTarget buildTarget)
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

            PlayerSettings.SplashScreen.showUnityLogo = false;

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
            ResConfig.Instance.Channel = "10000";
            ResConfig.Instance.IsGameModeArcade = false;
            ResConfig.Instance.IsDemoShow = false;
            PlayerSettings.Android.useCustomKeystore = false;
            PlayerSettings.iOS.appleDeveloperTeamID = "9882G66R3A";

            EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Disabled;

            if (packName == PackName.Local)
            {
                //ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "http://192.168.10.58";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "127.0.0.1";
                ResConfig.Instance.RouterHttpPort = 3478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.CN;
                ResConfig.Instance.IsShowDebugMode = true;
                ResConfig.Instance.IsShowEditorLoginMode = true;
                productName = $"Local_RealityGuard";
                packageName = $"com.dm.ARGameLocal";
            }
            else if(packName == PackName.InNetGDC)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;
                ResConfig.Instance.RouterHttpHost = "192.168.31.238";
                ResConfig.Instance.RouterHttpPort = 3478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.GDC;
                productName = $"GDC_RealityGuard";
                packageName = $"com.dm.ARGameInNetGDC";
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
                UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
            }
            else if(packName == PackName.AppleVisionPro)
            {
                ResConfig.Instance.Channel = "20001";

                ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;
                ResConfig.Instance.RouterHttpHost = "192.168.10.148";
                ResConfig.Instance.RouterHttpPort = 5478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.OnDevice;
                productName = $"AVP_RealityGuard";
                packageName = $"com.dm.ARGameAVP";
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
                UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
            }
            else if (packName == PackName.MetaQuest3)
            {
                ResConfig.Instance.Channel = "20001";

                ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;
                ResConfig.Instance.RouterHttpHost = "192.168.10.148";
                ResConfig.Instance.RouterHttpPort = 5478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.OnDevice;
                productName = $"Quest3_RealityGuard";
                packageName = $"com.dm.ARGameQuest3";
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
                UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
            }
            else if(packName == PackName.InNet148Master)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                //ResConfig.Instance.ResHostServerIP = "http://192.168.10.148";
                ResConfig.Instance.ResHostServerIP = "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame_148Master";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "192.168.10.148";
                ResConfig.Instance.RouterHttpPort = 5478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.CN;
                productName = $"148Master_RealityGuard";
                packageName = $"com.dm.ARGameInNet148Master";
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
                UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
            }
            else if(packName == PackName.InNet148Release)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                //ResConfig.Instance.ResHostServerIP = "http://192.168.10.148";
                ResConfig.Instance.ResHostServerIP = "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame_148Release";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "192.168.10.148";
                ResConfig.Instance.RouterHttpPort = 3478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.CN;
                productName = $"148Release_RealityGuard";
                packageName = $"com.dm.ARGameInNet148Release";
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
                UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
            }
            else if(packName == PackName.InNetZpb)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "http://192.168.10.58";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "192.168.10.58";
                ResConfig.Instance.RouterHttpPort = 3478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.CN;
                ResConfig.Instance.IsShowDebugMode = true;
                ResConfig.Instance.IsShowEditorLoginMode = true;
                productName = $"Zpb_RealityGuard";
                packageName = $"com.dm.ARGameInNetZpb";
                UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
                //EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Debugging;

            }
            else if (packName == PackName.OutNet_Benchmark)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;
                ResConfig.Instance.RouterHttpHost = "18.166.14.188";   // Linux machine in AWS HK.
                ResConfig.Instance.RouterHttpPort = 3478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.CN;
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                productName = $"RealityGuard_Benchmark";
                packageName = $"com.dm.ARGameBenchmark";
            }
            // ============== Staging environments (Aliyun/AWS) =================================
            else if (packName == PackName.OutNet_Arcade)
            {
                ResConfig.Instance.IsGameModeArcade = true;
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame_Arcade";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "8.134.89.37";
                ResConfig.Instance.RouterHttpPort = 5478;
                ResConfig.Instance.languageType = LanguageTypeEditor.CN.ToString();
                ResConfig.Instance.areaType = AreaType.CN;
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                ResConfig.Instance.IsNeedSendEventLog = true;  // Log events from real users.
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
                productName = $"Arcade_RealityGuard";
                packageName = $"com.dm.RealityGuardArcade";
            }
            else if (packName == PackName.OutNet_CN)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "8.134.156.170";
                ResConfig.Instance.RouterHttpPort = 3478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.CN;
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
                productName = $"CN_RealityGuard";
                packageName = $"com.dm.ARGameCN";
            }
            else if (packName == PackName.OutNet_CN_Demo)
            {
                ResConfig.Instance.IsDemoShow = true;
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame_CNDemo";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "8.134.156.170";
                ResConfig.Instance.RouterHttpPort = 5478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.CN;
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                ResConfig.Instance.IsNeedSendEventLog = true;  // Log events from real users.
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
                productName = $"CNDemo_RealityGuard";
                packageName = $"com.dm.ARGameCNDemo";
            }
            else if(packName == PackName.OutNet_EN || packName == PackName.OutNet_EN_AAB)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame_EN";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "34.225.211.137";
                ResConfig.Instance.RouterHttpPort = 3478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
                ResConfig.Instance.areaType = AreaType.EN;
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
                productName = $"EN_RealityGuard";
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
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "10001";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "10002";
                }

                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "https://download-realityguard.deepmirror.com/resources";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "artd-gateway.deepmirror.com";
                ResConfig.Instance.RouterHttpPort = 3478;
                ResConfig.Instance.languageType = LanguageTypeEditor.EN.ToString();
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

                // Apple Team ID "DeepMirror Inc"
                PlayerSettings.iOS.appleDeveloperTeamID = "T5X59PQT4X";
            }

            lastProductName = PlayerSettings.productName;
            lastPackageName = PlayerSettings.applicationIdentifier;
            PlayerSettings.companyName = "DeepMirror";
            PlayerSettings.productName = productName;
            PlayerSettings.applicationIdentifier = packageName;

            int buildVersionCode = GetBuildVersionCode();
            PlayerSettings.bundleVersion = "1." + buildVersionCode;
            ResConfig.Instance.Version = PlayerSettings.bundleVersion;
            PlayerSettings.Android.bundleVersionCode = buildVersionCode;
            PlayerSettings.iOS.buildNumber = buildVersionCode.ToString();

            // This requires Unity Pro account to build the package to have effect.
            PlayerSettings.SplashScreen.showUnityLogo = false;

            EditorUtility.SetDirty(ResConfig.Instance);
            AssetDatabase.SaveAssets();
        }

        private static async ETTask BuildPack_After(PackName packName)
        {
            // GlobalConfig.Instance.CodeMode = lastCodeMode;
            // GlobalConfig.Instance.codeOptimization = lastCodeOptimization;
            //
            // ResConfig.Instance.ResLoadMode = lastResLoadMode;
            // ResConfig.Instance.ResHostServerIP = lastResHostServerIP;
            // ResConfig.Instance.ResGameVersion = lastResGameVersion;
            // ResConfig.Instance.RouterHttpHost = lastRouterHttpHost;
            // ResConfig.Instance.RouterHttpPort = lastRouterHttpPort;
            //
            // PlayerSettings.productName = lastProductName;
            // PlayerSettings.applicationIdentifier = lastPackageName;
            //
            // EditorUserBuildSettings.buildAppBundle = false;
        }

        private static int buildVersionCode;
        private static void SetBuildVersionCode()
        {
            var dt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            buildVersionCode = (int)((DateTime.UtcNow.Ticks - dt.Ticks) / 10000 / 1000);
        }

        private static string buildPackageVersion;
        private static void SetBuildPackageVersion()
        {
            buildPackageVersion = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
        }

        private static int GetBuildVersionCode()
        {
            return buildVersionCode;
        }

        private static string GetBuildPackageVersion()
        {
            return buildPackageVersion;
        }

        private static async ETTask BuildInternal(PackName packName, BuildTarget buildTarget)
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

            string packFileName = $"{appName}_{GetBuildPackageVersion()}_{GetBuildVersionCode()}";
            (bool bRet, string packFullPath) = BuildHelper.Build(buildTarget, buildOptions, packFileName);
            if (bRet)
            {
                DealWhenAfterBuild(buildTarget, packFullPath, PlayerSettings.productName);
                WriteVersionToCDN(buildTarget, packName);
                WritePackFullPathToCDN(buildTarget, packName, packFullPath);
            }
            //Debug.Log($"构建成功");
        }

        private static void DealWhenAfterBuild(BuildTarget buildTarget, string packFullPath, string productName)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                ET.ToolsEditor.RunXCode2IPA(packFullPath, productName);
            }
        }

        private static void WriteVersionToCDN(BuildTarget buildTarget, PackName packName)
        {
            if (Directory.Exists($"{Application.dataPath}/../Bundles/CDN") == false)
            {
                return;
            }

            string versionInfo = ResConfig.Instance.Version;
            string versionFile = $"Version_{packName.ToString()}.txt";
            string cdnVersionFilePath = "";
            if (buildTarget == BuildTarget.Android)
            {
                cdnVersionFilePath =  $"{Application.dataPath}/../Bundles/CDN/Android/{versionFile}";
            }
            else if (buildTarget == BuildTarget.iOS)
            {
                cdnVersionFilePath =  $"{Application.dataPath}/../Bundles/CDN/IOS/{versionFile}";
            }
            else if (buildTarget == BuildTarget.WebGL)
            {
                cdnVersionFilePath =  $"{Application.dataPath}/../Bundles/CDN/WebGL/{versionFile}";
            }
            else if (buildTarget == BuildTarget.StandaloneWindows)
            {
                cdnVersionFilePath =  $"{Application.dataPath}/../Bundles/CDN/PC/{versionFile}";
            }
            FileHelper.CreateDirectory(cdnVersionFilePath);
            File.WriteAllText(cdnVersionFilePath, versionInfo);
            if (packName == PackName.InNetZpb)
            {
                File.WriteAllText(cdnVersionFilePath.Replace(versionFile, "Version.txt"), versionInfo);
            }
        }

        private static void WritePackFullPathToCDN(BuildTarget buildTarget, PackName packName, string packFullPath)
        {
            if (Directory.Exists($"{Application.dataPath}/../Bundles/CDN") == false)
            {
                return;
            }

            string versionFile = $"PackFullPath_{packName.ToString()}.txt";
            string cdnVersionFilePath = "";

            string downLoadFile = $"DownLoadFile_{packName.ToString()}.txt";
            string cdnDownLoadFilePath = "";
            if (buildTarget == BuildTarget.Android)
            {
                cdnVersionFilePath =  $"{Application.dataPath}/../Bundles/CDN/Android/{versionFile}";
                cdnDownLoadFilePath =  $"{Application.dataPath}/../Bundles/CDN/Android/{downLoadFile}";
            }
            else if (buildTarget == BuildTarget.iOS)
            {
                cdnVersionFilePath =  $"{Application.dataPath}/../Bundles/CDN/IOS/{versionFile}";
                cdnDownLoadFilePath =  $"{Application.dataPath}/../Bundles/CDN/IOS/{downLoadFile}";
            }
            else if (buildTarget == BuildTarget.WebGL)
            {
                cdnVersionFilePath =  $"{Application.dataPath}/../Bundles/CDN/WebGL/{versionFile}";
                cdnDownLoadFilePath =  $"{Application.dataPath}/../Bundles/CDN/WebGL/{downLoadFile}";
            }
            else if (buildTarget == BuildTarget.StandaloneWindows)
            {
                cdnVersionFilePath =  $"{Application.dataPath}/../Bundles/CDN/PC/{versionFile}";
                cdnDownLoadFilePath =  $"{Application.dataPath}/../Bundles/CDN/PC/{downLoadFile}";
            }

            FileHelper.CreateDirectory(cdnVersionFilePath);
            if (buildTarget == BuildTarget.iOS)
            {
                string context = $"{packFullPath}.ipa\n{packFullPath}.plist";
                File.WriteAllText(cdnVersionFilePath, context);
            }
            else
            {
                File.WriteAllText(cdnVersionFilePath, packFullPath);
            }

            FileHelper.CreateDirectory(cdnDownLoadFilePath);
            if (buildTarget == BuildTarget.iOS)
            {
                string fileName = Path.GetFileName(packFullPath);
                string context = $"{fileName}.plist";
                File.WriteAllText(cdnDownLoadFilePath, context);
            }
            else
            {
                string fileName = Path.GetFileName(packFullPath);
                File.WriteAllText(cdnDownLoadFilePath, fileName);
            }
        }
    }
}