using System;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

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

        public static void ExcelExporter(CodeMode codeMode, string configFolder, ConfigType configType, string param)
        {
            string genCode = string.Empty;

#if UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
            // if ((configType & ConfigType.StartConfig) > 0)
            // {
            //     genCode = $"sh gen_code_client__StartConfig.sh {configFolder} {param}";
            //     ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            //
            //     genCode = $"sh gen_code_server__StartConfig.sh {configFolder} {param}";
            //     ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            //
            //     genCode = $"sh gen_code_client_server__StartConfig.sh {configFolder} {param}";
            //     ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            // }

            if ((configType & ConfigType.AbilityConfig) > 0)
            {
                genCode = $"sh gen_code_client__AbilityConfig.sh {param}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"sh gen_code_server__AbilityConfig.sh {param}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"sh gen_code_client_server__AbilityConfig.sh {param}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            }
#else
            if ((configType & ConfigType.StartConfig) > 0)
            {
                genCode = $"gen_code_client__StartConfig.bat {configFolder} {param}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"gen_code_server__StartConfig.bat {configFolder} {param}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"gen_code_client_server__StartConfig.bat {configFolder} {param}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");
            }

            if ((configType & ConfigType.AbilityConfig) > 0)
            {
                genCode = $"gen_code_client__AbilityConfig.bat {param}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"gen_code_server__AbilityConfig.bat {param}";
                ShellHelper.Run($"{genCode}", "../Tools/Luban/");

                genCode = $"gen_code_client_server__AbilityConfig.bat {param}";
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

        public static void RunMongoDBFromDocker()
        {
            ShellHelper.Run($"docker run -itd --restart=always --name mongo-db -e MONGO_INITDB_ROOT_USERNAME=admin -e MONGO_INITDB_ROOT_PASSWORD=example -p 27017:27017 mongo",".");
        }

        public static void RunXCode2IPA(string xcodeWorkspace, string productName)
        {
            string shellPath = $"{Application.dataPath}/../../Tools/XCode2IPA";
            FileHelper.CopyDirectory(shellPath, xcodeWorkspace);

            File.WriteAllText($"{xcodeWorkspace}/productName.txt", productName);

            ShellHelper.Run($"python3 RunXCode2IPA.py", xcodeWorkspace);
        }
    }
}