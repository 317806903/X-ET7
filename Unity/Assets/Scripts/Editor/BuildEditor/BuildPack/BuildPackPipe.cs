using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using YooAsset;
using YooAsset.Editor;

namespace ET
{
    public static class BuildPackPipe
    {
        public static void BuildConfig_AbilityConfigAndBuildAB_Android()
        {
            Action build = async () =>
            {
                await ET.BuildConfig.BuildConfig_AbilityConfig();
                await ET.BuildAssetBundle.BuildAB_Android_Min(false);
            };
            build();
        }

        public static void BuildConfig_AbilityConfigAndBuildAB_IOS()
        {
            Action build = async () =>
            {
                await ET.BuildConfig.BuildConfig_AbilityConfig();
                await ET.BuildAssetBundle.BuildAB_IOS_Min(false);
            };
            build();
        }

        public static void BuildConfig_AbilityConfigAndBuildABAndPack_Android()
        {
            string[] args = Environment.GetCommandLineArgs();
            string param = args[args.Length - 1];

            string[] paramTmp = param.Split(new char[] { ',', ';', '|' });
            HashSet<string> paramList = new();
            foreach (string tmp in paramTmp)
            {
                paramList.Add(tmp);
            }

            Action build = async () =>
            {
                await ET.BuildConfig.BuildConfig_AbilityConfig();
                await ET.BuildAssetBundle.BuildAB_Android_Min(false);
                if (paramList.Contains("CN"))
                {
                    await ET.BuildPack.BuildPack_Android_OutNet_CN();
                }
                if (paramList.Contains("EN"))
                {
                    await ET.BuildPack.BuildPack_Android_OutNet_EN();
                }
                if (paramList.Contains("CNDemo"))
                {
                    await ET.BuildPack.BuildPack_Android_OutNet_CN_Demo();
                }
            };
            build();
        }

        public static void BuildConfig_AbilityConfigAndBuildABAndPack_IOS()
        {
            string[] args = Environment.GetCommandLineArgs();
            string param = args[args.Length - 1];

            string[] paramTmp = param.Split(new char[] { ',', ';', '|' });
            HashSet<string> paramList = new();
            foreach (string tmp in paramTmp)
            {
                paramList.Add(tmp);
            }

            Action build = async () =>
            {
                await ET.BuildConfig.BuildConfig_AbilityConfig();
                await ET.BuildAssetBundle.BuildAB_IOS_Min(false);
                if (paramList.Contains("CN"))
                {
                    await ET.BuildPack.BuildPack_IOS_OutNet_CN();
                }
                if (paramList.Contains("EN"))
                {
                    await ET.BuildPack.BuildPack_IOS_OutNet_EN();
                }
                if (paramList.Contains("CNDemo"))
                {
                    await ET.BuildPack.BuildPack_IOS_OutNet_CN_Demo();
                }
            };
            build();
        }

        public static async ETTask BuildConfig_AbilityConfigAndBuildServer_win()
        {
            Action build = async () =>
            {
                await ET.BuildConfig.BuildConfig_AbilityConfig();
                await ET.BuildServer.BuildServer_win();
            };
            build();
        }
    }
}