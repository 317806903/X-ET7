using System;
using UnityEditor;

namespace ET
{
    public static class ToolsEditor
    {
        public enum ConfigType
        {
            AbilityConfig = 1<<1,
            StartConfig = 1<<2,
            All = AbilityConfig | StartConfig,
        }

        public static void ExcelExporter(CodeMode codeMode, string configFolder, ConfigType configType)
        {
            string genCode = string.Empty;

#if UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
            // if ((configType & ConfigType.StartConfig) > 0)
            // {
            //     genCode = $"sh gen_code_client__StartConfig.sh {configFolder}";
            //     ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            //
            //     genCode = $"sh gen_code_server__StartConfig.sh {configFolder}";
            //     ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            //
            //     genCode = $"sh gen_code_client_server__StartConfig.sh {configFolder}";
            //     ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            // }

            if ((configType & ConfigType.AbilityConfig) > 0)
            {
                genCode = $"sh gen_code_client__AbilityConfig.sh";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"sh gen_code_server__AbilityConfig.sh";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"sh gen_code_client_server__AbilityConfig.sh";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            }
#else
            if ((configType & ConfigType.StartConfig) > 0)
            {
                genCode = $"gen_code_client__StartConfig.bat {configFolder}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"gen_code_server__StartConfig.bat {configFolder}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"gen_code_client_server__StartConfig.bat {configFolder}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            }

            if ((configType & ConfigType.AbilityConfig) > 0)
            {
                genCode = $"gen_code_client__AbilityConfig.bat";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"gen_code_server__AbilityConfig.bat";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"gen_code_client_server__AbilityConfig.bat";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            }
#endif
        }

        public static void ExcelExporterUI()
        {
#if UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
            const string tools = "./Tool";
#else
            const string tools = ".\\Tool.exe";
#endif
            ShellHelper.Run($"{tools} --AppType=ExcelExporterUI --Console=1", "../Bin/");
        }

        public static void Proto2CS()
        {
#if UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
            const string tools = "./Tool";
#else
            const string tools = ".\\Tool.exe";
#endif
            ShellHelper.Run($"{tools} --AppType=Proto2CS --Console=1", "../Bin/");
        }
    }
}