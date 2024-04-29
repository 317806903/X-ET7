using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using YooAsset.Editor;

namespace ET
{
    public static class BuildAssetBundle
    {
        private static string Key_LastCodeMode = "Key_LastCodeMode";
        private static string Key_LastCodeOptimization = "Key_LastCodeOptimization";

        private static string Key_LastEnableView = "Key_LastEnableView";
        private static string Key_LastEnableCodes = "Key_LastEnableCodes";

        [MenuItem("Pack/BuildABOnlyRes_Android", false, 200)]
        public static async ETTask<BuildResult> Menu_BuildABOnlyRes_Android()
        {
            return await BuildABOnlyRes_Android();
        }

        public static async ETTask<BuildResult> BuildABOnlyRes_Android(bool needChk = true)
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildAssetBundle), "BuildABOnlyRes_Android", new(){["needChk"]=needChk}))
            {
                return null;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            if (needChk)
            {
                ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildABOnlyRes_Android", () =>
                {
                    BuildABInternal(buildTarget, PackName.OutNet_CN, true, true).Coroutine();
                });
                return null;
            }
            else
            {
                return await BuildABInternal(buildTarget, PackName.OutNet_CN, true, true);
            }
        }

        [MenuItem("Pack/BuildABOnlyRes_IOS", false, 201)]
        public static async ETTask<BuildResult> Menu_BuildABOnlyRes_IOS()
        {
            return await BuildABOnlyRes_IOS();
        }

        public static async ETTask<BuildResult> BuildABOnlyRes_IOS(bool needChk = true)
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildAssetBundle), "BuildABOnlyRes_IOS", new(){["needChk"]=needChk}))
            {
                return null;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            if (needChk)
            {
                ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildABOnlyRes_IOS", () =>
                {
                    BuildABInternal(buildTarget, PackName.OutNet_CN, true, true).Coroutine();
                });
                return null;
            }
            else
            {
                return await BuildABInternal(buildTarget, PackName.OutNet_CN, true, true);
            }
        }

        [MenuItem("Pack/BuildAB_Android", false, 210)]
        public static async ETTask<BuildResult> Menu_BuildAB_Android()
        {
            return await BuildAB_Android();
        }

        public static async ETTask<BuildResult> BuildAB_Android(bool needChk = true)
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildAssetBundle), "BuildAB_Android", new(){["needChk"]=needChk}))
            {
                return null;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            if (needChk)
            {
                ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildAB_Android", () =>
                {
                    BuildABInternal(buildTarget, PackName.OutNet_CN, false, true).Coroutine();
                });
                return null;
            }
            else
            {
                return await BuildABInternal(buildTarget, PackName.OutNet_CN, false, true);
            }
        }

        [MenuItem("Pack/BuildAB_Android_Local", false, 210)]
        public static async ETTask<BuildResult> Menu_BuildAB_Android_Local()
        {
            return await BuildAB_Android_Local();
        }

        public static async ETTask<BuildResult> BuildAB_Android_Local(bool needChk = true)
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildAssetBundle), "BuildAB_Android_Local", new(){["needChk"]=needChk}))
            {
                return null;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            if (needChk)
            {
                ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildAB_Android_Local", () =>
                {
                    BuildABInternal(buildTarget, PackName.Local, false, true).Coroutine();
                });
                return null;
            }
            else
            {
                return await BuildABInternal(buildTarget, PackName.Local, false, true);
            }
        }

        [MenuItem("Pack/BuildAB_Android_Min", false, 210)]
        public static async ETTask<BuildResult> Menu_BuildAB_Android_Min()
        {
            return await BuildAB_Android_Min();
        }

        public static async ETTask<BuildResult> BuildAB_Android_Min(bool needChk = true)
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildAssetBundle), "BuildAB_Android_Min", new(){["needChk"]=needChk}))
            {
                return null;
            }

            BuildTarget buildTarget = BuildTarget.Android;
            if (needChk)
            {
                ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildAB_Android_Min", () =>
                {
                    BuildABInternal(buildTarget, PackName.OutNet_CN, false, false).Coroutine();
                });
                return null;
            }
            else
            {
                return await BuildABInternal(buildTarget, PackName.OutNet_CN, false, false);
            }
        }

        [MenuItem("Pack/BuildAB_IOS", false, 211)]
        public static async ETTask<BuildResult> Menu_BuildAB_IOS()
        {
            return await BuildAB_IOS();
        }

        public static async ETTask<BuildResult> BuildAB_IOS(bool needChk = true)
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildAssetBundle), "BuildAB_IOS", new(){["needChk"]=needChk}))
            {
                return null;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            if (needChk)
            {
                ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildAB_IOS", () =>
                {
                    BuildABInternal(buildTarget, PackName.OutNet_CN, false, true).Coroutine();
                });
                return null;
            }
            else
            {
                return await BuildABInternal(buildTarget, PackName.OutNet_CN, false, true);
            }
        }

        [MenuItem("Pack/BuildAB_IOS_Min", false, 211)]
        public static async ETTask<BuildResult> Menu_BuildAB_IOS_Min()
        {
            return await BuildAB_IOS_Min();
        }

        public static async ETTask<BuildResult> BuildAB_IOS_Min(bool needChk = true)
        {
            if (ET.BuildAssetBundle.ChkIsEnableCodes(typeof(BuildAssetBundle), "BuildAB_IOS_Min", new(){["needChk"]=needChk}))
            {
                return null;
            }

            BuildTarget buildTarget = BuildTarget.iOS;
            if (needChk)
            {
                ET.BuildAssetBundle.ChkTarget(buildTarget, $"BuildAB_IOS_Min", () =>
                {
                    BuildABInternal(buildTarget, PackName.OutNet_CN, false, false).Coroutine();
                });
                return null;
            }
            else
            {
                return await BuildABInternal(buildTarget, PackName.OutNet_CN, false, false);
            }
        }

        public static void BuildAB_CommandLine()
        {
            BuildTarget buildTarget = BuildHelper.GetBuildTargetFromCommandLine("BuildAB");
            PackName packName = BuildHelper.GetBuildPackNameFromCommandLine("PackName");
            BuildABInternal(buildTarget, packName, false, true).Coroutine();
        }

        public static async ETTask<BuildResult> BuildABInternal(BuildTarget buildTarget, PackName packName, bool OnlyRes, bool copyAllToSteamingAsset)
        {
            await InitInstance();
            bool bRet = await BuildAB_Pre(packName, OnlyRes);
            if (bRet == false)
            {
                return null;
            }
            BuildResult buildResult = null;
            try
            {
                buildResult = await BuildInternal(buildTarget, copyAllToSteamingAsset);
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            await BuildAB_After(packName, OnlyRes);
            return buildResult;
        }

        private static async ETTask InitInstance()
        {
            GlobalConfig.Instance = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            ResConfig.Instance = AssetDatabase.LoadAssetAtPath<ResConfig>("Assets/Resources/ResConfig.asset");
        }

        private static async ETTask<bool> BuildAB_Pre(PackName packName, bool OnlyRes)
        {
            EditorPrefs.SetInt(Key_LastCodeMode, (int)GlobalConfig.Instance.CodeMode);
            EditorPrefs.SetInt(Key_LastCodeOptimization, (int)GlobalConfig.Instance.codeOptimization);

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

            if (OnlyRes)
            {
                return true;
            }
            EditorPrefs.SetBool(Key_LastEnableView, Define.EnableView);
            EditorPrefs.SetBool(Key_LastEnableCodes, Define.EnableCodes);

            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            Debug.Log($"---defines[{defines}] Define.EnableView[{Define.EnableView}] Define.EnableCodes[{Define.EnableCodes}]");
            await BuildHelper.BuildModelAndHotfix();
            return true;
        }

        private static async ETTask BuildAB_After(PackName packName, bool OnlyRes)
        {
            int lastCodeMode = EditorPrefs.GetInt(Key_LastCodeMode);
            int lastCodeOptimization = EditorPrefs.GetInt(Key_LastCodeOptimization);
            GlobalConfig.Instance.CodeMode = (CodeMode)lastCodeMode;
            GlobalConfig.Instance.codeOptimization = (CodeOptimization)lastCodeOptimization;

            // bool lastEnableView = EditorPrefs.GetBool(Key_LastEnableView);
            // bool lastEnableCodes = EditorPrefs.GetBool(Key_LastEnableCodes);
            // Debug.Log($"lastEnableView[{lastEnableView}] lastEnableCodes[{lastEnableCodes}]");
            // if (lastEnableView || lastEnableCodes)
            // {
            //     ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", true);
            //
            //     //ET.BuildHelper.ReGenerateProjectFiles();
            // }
        }

        private static string GetBuildPackageVersion()
        {
            // int totalMinutes = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
            // return DateTime.Now.ToString("yyyy-MM-dd") + "_" + totalMinutes;
            return DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
        }

        private static async ETTask<BuildResult> BuildInternal(BuildTarget buildTarget, bool copyAllToSteamingAsset)
        {
            Debug.Log($"开始构建 : {buildTarget}");

            // 构建参数
            string defaultOutputRoot = AssetBundleBuilderHelper.GetDefaultOutputRoot();
            BuildParameters buildParameters = new BuildParameters();
            buildParameters.OutputRoot = defaultOutputRoot;
            buildParameters.BuildTarget = buildTarget;
            buildParameters.BuildPipeline = EBuildPipeline.ScriptableBuildPipeline;
            buildParameters.BuildMode = EBuildMode.IncrementalBuild;
            buildParameters.PackageName = "DefaultPackage";
            buildParameters.PackageVersion = GetBuildPackageVersion();//"1.0";
            buildParameters.VerifyBuildingResult = true;
            buildParameters.ShareAssetPackRule = new DefaultShareAssetPackRule();
            buildParameters.CompressOption = ECompressOption.LZ4;
            buildParameters.OutputNameStyle = EOutputNameStyle.BundleName_HashName;
            if (copyAllToSteamingAsset)
            {
                buildParameters.CopyBuildinFileOption = ECopyBuildinFileOption.None;
            }
            else
            {
                buildParameters.CopyBuildinFileOption = ECopyBuildinFileOption.ClearAndCopyByTags;
                buildParameters.CopyBuildinFileTags = "aotdlls;buildIn";
            }

            if (buildParameters.BuildPipeline == EBuildPipeline.ScriptableBuildPipeline)
            {
                buildParameters.SBPParameters = new BuildParameters.SBPBuildParameters();
                buildParameters.SBPParameters.WriteLinkXML = true;
            }

            // 执行构建
            AssetBundleBuilder builder = new AssetBundleBuilder();
            BuildResult buildResult = builder.Run(buildParameters);
            if (buildResult.Success)
            {
                Debug.Log($"构建成功 : {buildResult.OutputPackageDirectory}");
                EditorUtility.RevealInFinder(buildResult.OutputPackageDirectory);
                if (copyAllToSteamingAsset)
                {
                    CopyABToStreamingAssets(buildTarget, buildResult.OutputPackageDirectory);
                }
                CopyABToCDN(buildTarget, buildResult.OutputPackageDirectory);
            }
            else
            {
                Debug.LogError($"构建失败 : {buildResult.ErrorInfo}");
            }

            return buildResult;
        }

        private static void CopyABToStreamingAssets(BuildTarget buildTarget, string ABPath)
        {
            string streamingAssets = YooAsset.Editor.AssetBundleBuilderHelper.GetStreamingAssetsFolderPath();
            if (Directory.Exists(streamingAssets))
            {
                Directory.Delete(streamingAssets, true);
            }

            FileHelper.CopyDirectory(ABPath, streamingAssets);
        }

        private static void CopyABToCDN(BuildTarget buildTarget, string ABPath)
        {
            if (Directory.Exists($"{Application.dataPath}/../Bundles/CDN") == false)
            {
                return;
            }
            string gameVersion = ResConfig.Instance.ResGameVersion;
            string cdnPath = "";
            if (buildTarget == BuildTarget.Android)
            {
                cdnPath =  $"{Application.dataPath}/../Bundles/CDN/Android/{gameVersion}";
            }
            else if (buildTarget == BuildTarget.iOS)
            {
                cdnPath =  $"{Application.dataPath}/../Bundles/CDN/IOS/{gameVersion}";
            }
            else if (buildTarget == BuildTarget.WebGL)
            {
                cdnPath =  $"{Application.dataPath}/../Bundles/CDN/WebGL/{gameVersion}";
            }
            else if (buildTarget == BuildTarget.StandaloneWindows)
            {
                cdnPath =  $"{Application.dataPath}/../Bundles/CDN/PC/{gameVersion}";
            }
            if (Directory.Exists(cdnPath))
            {
                Directory.Delete(cdnPath, true);
            }
            FileHelper.CopyDirectory(ABPath, cdnPath);
        }

        public static bool ChkIsEnableCodes(Type type, string methodName, Dictionary<string, object> methodParamDic = null)
        {
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return true;
            }
            if (System.IO.Directory.Exists($"{HybridCLR.Editor.SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr") == false)
            {
                EditorUtility.DisplayDialog("警告", "没有安装HybridCLR", "确定");
                return true;
            }

            if (Define.EnableView || Define.EnableCodes)
            {
                ET.BuildHelper.EnableDefineSymbols("ENABLE_VIEW;ENABLE_CODES", false);

                CompilingFinishedCallback.SetMethodName(type, methodName, methodParamDic);
                return true;
            }

            return false;
        }

        public static void ChkTarget(BuildTarget buildTarget, string message, Action action)
        {
#if UNITY_ANDROID
            if (buildTarget == BuildTarget.Android)
            {
                action.Invoke();
                return;
            }
#elif UNITY_IPHONE
            if (buildTarget == BuildTarget.iOS)
            {
                action.Invoke();
                return;
            }
#endif
            if (EditorUtility.DisplayDialog("确定一下", message, "确定", "不了"))
            {
                action.Invoke();
            }
            return;
        }

    }
}