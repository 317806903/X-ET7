using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using YooAsset;
using Debug = UnityEngine.Debug;
using HybridCLR.Editor.Commands;

namespace ET
{
    public enum PackName
    {
        Local,
        InNetGDC,
        MetaQuest3,
        MetaQuest3_EN,
        AppleVisionPro,
        InNet148Master,
        InNet148Release,
        InNetZpb,
        InNetUSDebug,
        InNet108,
        OutNet_Benchmark,
        OutNet_Arcade,
        OutNet_CN,
        OutNet_CN_Demo,
        OutNet_EN,
        OutNet_EN_AAB,

        ExternalTest,  // External test for US users.
        ExternalTest_AAB,

        // External events
        Event_Huanyu,
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/Dev/BuildPack_Android_Local", false, 300)]
        #endif
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

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_InNetGDC", false, 301)]
        #endif
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_InNet148Master", false, 301)]
        #endif
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_InNet148Release", false, 301)]
        #endif
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

        #if UNITY_ANDROID &&( Platform_Mobile || Platform_Quest )
        [MenuItem("Pack/Dev/BuildPack_Android_InNetZpb", false, 302)]
        #endif
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

        #if UNITY_ANDROID &&( Platform_Mobile || Platform_Quest )
        [MenuItem("Pack/Dev/BuildPack_Android_InNetUSDebug", false, 302)]
        #endif
        public static async ETTask BuildPack_Android_InNetUSDebug()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_InNetUSDebug", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_InNetUSDebug", () =>
            {
                BuildPackInternal(buildTarget, PackName.InNetUSDebug).Coroutine();
            });
        }

        #if UNITY_ANDROID && Platform_Quest
        [MenuItem("Pack/Dev/BuildPack_Android_InNet108", false, 302)]
        #endif
        public static async ETTask BuildPack_Android_InNet108()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_InNet108", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_InNet108", () =>
            {
                BuildPackInternal(buildTarget, PackName.InNet108).Coroutine();
            });
        }

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_OutNet_Benchmark", false, 303)]
        #endif
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_OutNet_Arcade", false, 303)]
        #endif
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_OutNet_CN", false, 304)]
        #endif
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_OutNet_CN_Demo", false, 304)]
        #endif
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_OutNet_EN", false, 305)]
        #endif
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_OutNet_EN_AAB", false, 306)]
        #endif
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_ExternalTest", false, 307)]
        #endif
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_ExternalTest_AAB", false, 308)]
        #endif
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

        #if UNITY_ANDROID && Platform_Quest
        [MenuItem("Pack/BuildPack_Android_MetaQuest3", false, 309)]
        #endif
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

        #if UNITY_ANDROID && Platform_Quest
        [MenuItem("Pack/BuildPack_Android_MetaQuest3_EN", false, 309)]
        #endif
        public static async ETTask BuildPack_Android_MetaQuest3_EN()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_MetaQuest3_EN", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_MetaQuest3_EN", () =>
            {
                BuildPackInternal(buildTarget, PackName.MetaQuest3_EN).Coroutine();
            });
        }

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_InNetGDC", false, 321)]
        #endif
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

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_InNet148Master", false, 321)]
        #endif
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

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_InNet148Release", false, 321)]
        #endif
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

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_InNetZpb", false, 322)]
        #endif
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

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_OutNet_Benchmark", false, 323)]
        #endif
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

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_OutNet_Arcade", false, 323)]
        #endif
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

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_OutNet_CN", false, 324)]
        #endif
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

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_OutNet_CN_Demo", false, 324)]
        #endif
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

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_OutNet_EN", false, 325)]
        #endif
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

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_ExternalTest", false, 326)]
        #endif
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

        #if UNITY_IOS && Platform_AVP
        [MenuItem("Pack/BuildPack_IOS_AppleVisionPro", false, 327)]
        #endif
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

        #if UNITY_ANDROID && Platform_Mobile
        [MenuItem("Pack/BuildPack_Android_EventHuanyu", false, 341)]
        #endif
        public static async ETTask BuildPack_Android_EventHuanyu()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_Android_EventHuanyu", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_Android_EventHuanyu", () =>
            {
                BuildPackInternal(buildTarget, PackName.Event_Huanyu).Coroutine();
            });
        }

        #if UNITY_IOS && Platform_Mobile
        [MenuItem("Pack/BuildPack_IOS_EventHuanyu", false, 342)]
        #endif
        public static async ETTask BuildPack_IOS_EventHuanyu()
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildPack), "BuildPack_IOS_EventHuanyu", null))
            {
                return;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildPack_IOS_EventHuanyu", () =>
            {
                BuildPackInternal(buildTarget, PackName.Event_Huanyu).Coroutine();
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
            string productName = $"RealityGuard";
            string packageName = $"com.dm.ARGame";

            string MirrorARSessionAuthAppKey = "63f72bdc21c28002a4f4b218#mirrorsdk_dev";
            string MirrorARSessionAuthAppSecret = "79395951c7cf78bd7ec1fe5b5b1c97ce";

            // ============== Development environments (Office/Benchmark)=================================
            ResConfig.Instance.MirrorARSessionAuthAppKey = MirrorARSessionAuthAppKey;
            ResConfig.Instance.MirrorARSessionAuthAppSecret = MirrorARSessionAuthAppSecret;
            ResConfig.Instance.Channel = "10000";
            ResConfig.Instance.IsNeedSendEventLog = false;
            ResConfig.Instance.IsGameModeArcade = false;
            ResConfig.Instance.IsDemoShow = false;
            ResConfig.Instance.IsShowDebugMode = false;
            ResConfig.Instance.IsShowEditorLoginMode = false;
            PlayerSettings.Android.useCustomKeystore = false;
            PlayerSettings.iOS.appleDeveloperTeamID = "9882G66R3A";

            EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Disabled;

            if (packName == PackName.Local)
            {
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "9999";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "9999";
                }
                //ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
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

                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
            }
            else if(packName == PackName.AppleVisionPro)
            {
                if (buildTarget == BuildTarget.Android)
                {
                    return;
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "4001";
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;

                productName = $"AVP_RealityGuard";
                packageName = $"com.dm.ARGameAVP";
            }
            else if (packName == PackName.MetaQuest3)
            {
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "4002";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    return;
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;

                // Disable debug mode checks for now so to support auto login if possible.
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;

                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";

                productName = $"Quest3_RealityGuard";
                packageName = $"com.dm.ARGameQuest3";

            }
            else if (packName == PackName.MetaQuest3_EN)
            {
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "4003";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    return;
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;

                // Disable debug mode checks for now so to support auto login if possible.
                ResConfig.Instance.IsShowDebugMode = false;
                ResConfig.Instance.IsShowEditorLoginMode = false;

                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";

                productName = $"Quest3_RealityGuard_EN";
                packageName = $"com.dm.ARGameQuest3EN";
            }
            else if(packName == PackName.InNet148Master)
            {
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "9021";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "9022";
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;

                productName = $"148Master_RealityGuard";
                packageName = $"com.dm.ARGameInNet148Master";

                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
            }
            else if(packName == PackName.InNet148Release)
            {
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "9031";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "9032";
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;

                productName = $"148Release_RealityGuard";
                packageName = $"com.dm.ARGameInNet148Release";

                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguarddebug.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguarddebug";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
            }
            else if(packName == PackName.InNetZpb)
            {
#if Platform_Mobile
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "9999";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "9999";
                }
                productName = $"Zpb_RealityGuard";
                packageName = $"com.dm.ARGameInNetZpb";
#elif Platform_Quest
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "99991";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "99991";
                }
                productName = $"Zpb_RealityGuardQuest";
                packageName = $"com.dm.ARGameInNetZpbQuest";
#elif Platform_AVP
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "99992";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "99992";
                }
                productName = $"Zpb_RealityGuardAVP";
                packageName = $"com.dm.ARGameInNetZpbAVP";
#else
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "9999";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "9999";
                }
                productName = $"Zpb_RealityGuard";
                packageName = $"com.dm.ARGameInNetZpb";
#endif
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.IsShowDebugMode = true;
                ResConfig.Instance.IsShowEditorLoginMode = true;
                //EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Debugging;
            }
            else if(packName == PackName.InNet108)
            {
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "9997";
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.IsShowDebugMode = true;
                ResConfig.Instance.IsShowEditorLoginMode = true;
                productName = $"108_RealityGuard";
                packageName = $"com.dm.ARGameInNet108";
                //EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Debugging;
            }
            else if(packName == PackName.InNetUSDebug)
            {
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "9998";
                    #if Platform_Quest
                    ResConfig.Instance.Channel = "99981";
                    #endif
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "9998";
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;
                ResConfig.Instance.IsShowDebugMode = true;
                ResConfig.Instance.IsShowEditorLoginMode = true;
                productName = $"UsDebug_RealityGuard";
                // Must use the same package name as the production version to test AppsFlyer.
                packageName = $"com.dm.realityguard";
            }
            else if (packName == PackName.OutNet_Benchmark)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;

                productName = $"RealityGuard_Benchmark";
                packageName = $"com.dm.ARGameBenchmark";
            }
            // ============== Staging environments (Aliyun/AWS) =================================
            else if (packName == PackName.OutNet_Arcade)
            {
                // NOTE: Arcade is now for offline tryouts. Built with DeepMirror INC Apple Developer, and signed with prod key.
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "2001";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "2002";
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;

                ResConfig.Instance.MirrorARSessionAuthAppKey = "63f72bdc21c28002a4f4b218#chinajoy";
                ResConfig.Instance.MirrorARSessionAuthAppSecret = "31d8ee5843b392d271232afcd0568968";
                // 设置签名
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = "realityguard.keystore"; // Unity root path
                PlayerSettings.Android.keystorePass = "DMDM0731!";
                PlayerSettings.Android.keyaliasName = "realityguard";
                PlayerSettings.Android.keyaliasPass = "DMDM0731!";
                productName = $"镜界守卫AR";
                packageName = $"com.dm.realityguard.demo";
                // Apple Team ID "DeepMirror Inc"
                PlayerSettings.iOS.appleDeveloperTeamID = "T5X59PQT4X";
            }
            else if (packName == PackName.OutNet_CN)
            {
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "9001";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "9002";
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;

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
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "3001";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "3002";
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;

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
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "9011";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "9012";
                }
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;

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
                    ResConfig.Instance.Channel = "1001";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "1002";
                }

                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;

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

            // ============= Events environments ===================================================
            else if (packName == PackName.Event_Huanyu)
            {
                if (buildTarget == BuildTarget.Android)
                {
                    ResConfig.Instance.Channel = "1101";
                }
                else if (buildTarget == BuildTarget.iOS)
                {
                    ResConfig.Instance.Channel = "1102";
                }

                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.MirrorARSessionAuthAppKey = "63f72bdc21c28002a4f4b218#chinajoy";
                ResConfig.Instance.MirrorARSessionAuthAppSecret = "31d8ee5843b392d271232afcd0568968";
                productName = $"镜界守卫AR";
                packageName = $"com.dm.realityguard.demo";
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

        #if UNITY_ANDROID && Platform_Quest
        [MenuItem("HybridCLR/Generate/All(Meta)", priority = 200)]
        #endif
        public static async ETTask HybridCLR_Generate_MetaQuest3()
        {
            PrebuildCommand.GenerateAll();
            EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ASTC;
            EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
            AssetDatabase.SaveAssets();
        }
    }
}