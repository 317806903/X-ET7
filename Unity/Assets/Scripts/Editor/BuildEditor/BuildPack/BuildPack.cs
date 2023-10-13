using System;
using System.Diagnostics;
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
        OutNet_CN,
        InNetZpb,
        OutNet_EN,
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
            if (Define.EnableCodes)
            {
                if (true)
                {
                    ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

                    CompilingFinishedCallback.Set(typeof(BuildPack), "BuildPack_Android_Local", null);
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "请 Remove EnableCodes ", "确定");
                }
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
            if (Define.EnableCodes)
            {
                if (true)
                {
                    ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

                    CompilingFinishedCallback.Set(typeof(BuildPack), "BuildPack_Android_InNet148", null);
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "请 Remove EnableCodes ", "确定");
                }
                return;
            }
            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.InNet148);
        }

        [MenuItem("Pack/BuildPack_Android_OutNet_CN", false, 302)]
        public static async ETTask BuildPack_Android_OutNet_CN()
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
            if (Define.EnableCodes)
            {
                if (true)
                {
                    ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

                    CompilingFinishedCallback.Set(typeof(BuildPack), "BuildPack_Android_OutNet_CN", null);
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "请 Remove EnableCodes ", "确定");
                }
                return;
            }
            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.OutNet_CN);
        }

        [MenuItem("Pack/BuildPack_Android_InNetZpb", false, 303)]
        public static async ETTask BuildPack_Android_InNetZpb()
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
            if (Define.EnableCodes)
            {
                if (true)
                {
                    ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

                    CompilingFinishedCallback.Set(typeof(BuildPack), "BuildPack_Android_InNetZpb", null);
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "请 Remove EnableCodes ", "确定");
                }
                return;
            }
            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.InNetZpb);
        }

        [MenuItem("Pack/BuildPack_Android_OutNet_EN", false, 304)]
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
            if (Define.EnableCodes)
            {
                if (true)
                {
                    ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

                    CompilingFinishedCallback.Set(typeof(BuildPack), "BuildPack_Android_OutNet_EN", null);
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "请 Remove EnableCodes ", "确定");
                }
                return;
            }
            BuildTarget buildTarget = BuildTarget.Android;
            await BuildPackInternal(buildTarget, PackName.OutNet_EN);
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
            if (Define.EnableCodes)
            {
                if (true)
                {
                    ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

                    CompilingFinishedCallback.Set(typeof(BuildPack), "BuildPack_IOS_InNet148", null);
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "请 Remove EnableCodes ", "确定");
                }
                return;
            }
            BuildTarget buildTarget = BuildTarget.iOS;
            await BuildPackInternal(buildTarget, PackName.InNet148);
        }

        [MenuItem("Pack/BuildPack_IOS_OutNet_CN", false, 322)]
        public static async ETTask BuildPack_IOS_OutNet_CN()
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
            if (Define.EnableCodes)
            {
                if (true)
                {
                    ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

                    CompilingFinishedCallback.Set(typeof(BuildPack), "BuildPack_IOS_OutNet_CN", null);
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "请 Remove EnableCodes ", "确定");
                }
                return;
            }
            BuildTarget buildTarget = BuildTarget.iOS;
            await BuildPackInternal(buildTarget, PackName.OutNet_CN);
        }

        [MenuItem("Pack/BuildPack_IOS_InNetZpb", false, 323)]
        public static async ETTask BuildPack_IOS_InNetZpb()
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
            if (Define.EnableCodes)
            {
                if (true)
                {
                    ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

                    CompilingFinishedCallback.Set(typeof(BuildPack), "BuildPack_IOS_InNetZpb", null);
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "请 Remove EnableCodes ", "确定");
                }
                return;
            }
            BuildTarget buildTarget = BuildTarget.iOS;
            await BuildPackInternal(buildTarget, PackName.InNetZpb);
        }

        [MenuItem("Pack/BuildPack_IOS_OutNet_EN", false, 324)]
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
            if (Define.EnableCodes)
            {
                if (true)
                {
                    ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

                    CompilingFinishedCallback.Set(typeof(BuildPack), "BuildPack_IOS_OutNet_EN", null);
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "请 Remove EnableCodes ", "确定");
                }
                return;
            }
            BuildTarget buildTarget = BuildTarget.iOS;
            await BuildPackInternal(buildTarget, PackName.OutNet_EN);
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

            UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.DevelopmentOnly;
            string productName = $"ARGame";
            string packageName = $"com.dm.ARGame";
            if (packName == PackName.Local)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.OfflinePlayMode;
                ResConfig.Instance.isShowDebugRoot = true;
                productName = $"ARGame_Local";
                packageName = $"com.dm.ARGameLocal";
            }
            else if(packName == PackName.InNet148)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "http://192.168.10.148";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "192.168.10.148";
                ResConfig.Instance.RouterHttpPort = 30300;
                ResConfig.Instance.isShowDebugRoot = true;
                productName = $"ARGame_InNet148";
                packageName = $"com.dm.ARGameInNet148";
                UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
            }
            else if(packName == PackName.OutNet_CN)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "8.134.156.170";
                ResConfig.Instance.RouterHttpPort = 30300;
                ResConfig.Instance.isShowDebugRoot = false;
                productName = $"ARGame_OutNet_CN";
                packageName = $"com.dm.ARGameCN";
            }
            else if(packName == PackName.InNetZpb)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "http://192.168.10.58";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "192.168.10.58";
                ResConfig.Instance.RouterHttpPort = 30300;
                ResConfig.Instance.isShowDebugRoot = false;
                productName = $"ARGame_InNetZpb";
                packageName = $"com.dm.ARGameInNetZpb";
                UnityEditor.PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
            }
            else if(packName == PackName.OutNet_EN)
            {
                ResConfig.Instance.ResLoadMode = EPlayMode.HostPlayMode;
                ResConfig.Instance.ResHostServerIP = "https://omelette.oss-cn-beijing.aliyuncs.com/dev/DeepMirrorARGame_EN";
                ResConfig.Instance.ResGameVersion = "v1.0";
                ResConfig.Instance.RouterHttpHost = "34.225.211.137";
                ResConfig.Instance.RouterHttpPort = 30300;
                ResConfig.Instance.isShowDebugRoot = false;
                productName = $"ARGame_OutNet_EN";
                packageName = $"com.dm.ARGameEN";
            }

            // // 设置签名
            // PlayerSettings.Android.keystoreName = "完整路径（包含文件后缀）";
            // PlayerSettings.Android.keystorePass = "密码";
            // PlayerSettings.Android.keyaliasPass = "密码";

            lastProductName = PlayerSettings.productName;
            lastPackageName = PlayerSettings.applicationIdentifier;
            PlayerSettings.companyName = "DeepMirror";
            PlayerSettings.productName = productName;
            PlayerSettings.bundleVersion = "1.0";
            PlayerSettings.applicationIdentifier = packageName;
        }

        private static async ETTask BuildPack_After(PackName packName)
        {
            GlobalConfig.Instance.CodeMode = lastCodeMode;
            GlobalConfig.Instance.codeOptimization = lastCodeOptimization;

            //ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", true);

            ResConfig.Instance.ResLoadMode = lastResLoadMode;
            ResConfig.Instance.ResHostServerIP = lastResHostServerIP;
            ResConfig.Instance.ResGameVersion = lastResGameVersion;
            ResConfig.Instance.RouterHttpHost = lastRouterHttpHost;
            ResConfig.Instance.RouterHttpPort = lastRouterHttpPort;

            PlayerSettings.productName = lastProductName;
            PlayerSettings.applicationIdentifier = lastPackageName;
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

            string packFileName = $"{PlayerSettings.productName}_{GetBuildPackageVersion()}";
            BuildHelper.Build(buildTarget, buildOptions, packFileName);
            //Debug.Log($"构建成功");
        }
    }
}