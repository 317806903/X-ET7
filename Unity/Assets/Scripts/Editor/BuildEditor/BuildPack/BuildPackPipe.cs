using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using YooAsset;
using YooAsset.Editor;

namespace ET
{
    public static class BuildPackPipe
    {
        [MenuItem("Pack/BuildConfig_AbilityConfig | BuildABOnlyRes_Android", false, 1000)]
        public static async ETTask BuildConfig_AbilityConfigAndBuildABOnlyRes_Android()
        {
            await ET.BuildConfig.BuildConfig_AbilityConfig();
            await ET.BuildAssetBundle.BuildABOnlyRes_Android();
        }

        [MenuItem("Pack/BuildConfig_AbilityConfig | BuildAB_Android", false, 1000)]
        public static async ETTask BuildConfig_AbilityConfigAndBuildAB_Android()
        {
            await ET.BuildConfig.BuildConfig_AbilityConfig();
            await ET.BuildAssetBundle.BuildAB_Android();
        }

        [MenuItem("Pack/BuildConfig_AbilityConfig | BuildServer_win", false, 1100)]
        public static async ETTask BuildConfig_AbilityConfigAndBuildServer_win()
        {
            await ET.BuildConfig.BuildConfig_AbilityConfig();
            await ET.BuildServer.BuildServer_win();
        }

        [MenuItem("Pack/BuildConfig_AbilityConfig | BuildAB_Android | BuildPack_Android_Local", false, 1200)]
        public static async ETTask BuildConfig_AbilityConfigAndBuildAB_AndroidAndBuildPack_Android_Local()
        {
            await ET.BuildConfig.BuildConfig_AbilityConfig();
            BuildResult buildResult = await ET.BuildAssetBundle.BuildAB_Android(false);
            if (buildResult.Success)
            {
                await ET.BuildPack.BuildPack_Android_Local();
            }
        }

        [MenuItem("Pack/BuildConfig_AbilityConfig | BuildAB_Android | BuildPack_Android_InNet148", false, 1201)]
        public static async ETTask BuildConfig_AbilityConfigAndBuildAB_AndroidAndBuildPack_Android_InNet148()
        {
            await ET.BuildConfig.BuildConfig_AbilityConfig();
            BuildResult buildResult = await ET.BuildAssetBundle.BuildAB_Android(false);
            if (buildResult.Success)
            {
                await ET.BuildPack.BuildPack_Android_InNet148Master();
            }
        }

        [MenuItem("Pack/BuildConfig_AbilityConfig | BuildAB_Android | BuildPack_Android_OutNet_CN", false, 1202)]
        public static async ETTask BuildConfig_AbilityConfigAndBuildAB_AndroidAndBuildPack_Android_OutNet_CN()
        {
            await ET.BuildConfig.BuildConfig_AbilityConfig();
            BuildResult buildResult = await ET.BuildAssetBundle.BuildAB_Android(false);
            if (buildResult.Success)
            {
                await ET.BuildPack.BuildPack_Android_OutNet_CN();
            }
        }

    }
}