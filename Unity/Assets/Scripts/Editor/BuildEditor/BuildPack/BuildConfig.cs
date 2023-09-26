using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public static class BuildConfig
    {
        private static string[] startConfigs;

        [MenuItem("Pack/BuildConfig_All", false, 100)]
        public static async ETTask BuildConfig_All()
        {
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            await BuildConfigInternal(ToolsEditor.ConfigType.All);
        }

        [MenuItem("Pack/BuildConfig_AbilityConfig", false, 101)]
        public static async ETTask BuildConfig_AbilityConfig()
        {
            if(Application.isPlaying)
            {
                EditorUtility.DisplayDialog("警告", "请先停止运行Unity", "确定");
                return;
            }
            await BuildConfigInternal(ToolsEditor.ConfigType.AbilityConfig);
        }

        public static async ETTask BuildConfigInternal(ToolsEditor.ConfigType configType)
        {
            EditorApplication.isPlaying = false;

            await InitInstance();
            await BuildConfig_Pre();
            try
            {
                AssetDatabase.Refresh();
                await BuildInternal(configType);
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            await BuildConfig_After();
        }

        private static async ETTask InitInstance()
        {
            GlobalConfig.Instance = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
            ResConfig.Instance = AssetDatabase.LoadAssetAtPath<ResConfig>("Assets/Resources/ResConfig.asset");

            DirectoryInfo directoryInfo = new DirectoryInfo("Assets/Config/Excel/StartConfig");
            startConfigs = directoryInfo.GetDirectories().Select(x => x.Name).ToArray();
        }

        private static async ETTask BuildConfig_Pre()
        {
        }

        private static async ETTask BuildConfig_After()
        {
        }

        private static string GetBuildPackageVersion()
        {
            int totalMinutes = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
            return DateTime.Now.ToString("yyyy-MM-dd") + "-" + totalMinutes;
        }

        private static async ETTask BuildInternal(ToolsEditor.ConfigType configType)
        {
            if ((configType & ToolsEditor.ConfigType.AbilityConfig) > 0)
            {
                await BuildInternal_AbilityConfig();
            }
            if ((configType & ToolsEditor.ConfigType.StartConfig) > 0)
            {
                await BuildInternal_StartConfig();
            }
        }

        private static async ETTask BuildInternal_AbilityConfig()
        {
            ToolsEditor.ExcelExporter(CodeMode.ClientServer, "", ToolsEditor.ConfigType.AbilityConfig);

            string unityClientConfigForAB = "../Unity/Assets/Bundles/Config/AbilityConfig";
            if (Directory.Exists(unityClientConfigForAB))
            {
                Directory.Delete(unityClientConfigForAB, true);
            }

            FileHelper.CopyDirectory("../Config/Excel/c/AbilityConfig", unityClientConfigForAB);

            AssetDatabase.Refresh();
        }

        private static async ETTask BuildInternal_StartConfig()
        {
            foreach (string startConfig in startConfigs)
            {
                ToolsEditor.ExcelExporter(CodeMode.ClientServer, startConfig, ToolsEditor.ConfigType.StartConfig);
            }

            string unityClientConfigForAB = $"../Unity/Assets/Bundles/Config/StartConfig";
            if (Directory.Exists(unityClientConfigForAB))
            {
                Directory.Delete(unityClientConfigForAB, true);
            }

            FileHelper.CopyDirectory($"../Config/Excel/c/StartConfig", unityClientConfigForAB);

            AssetDatabase.Refresh();
        }

    }
}