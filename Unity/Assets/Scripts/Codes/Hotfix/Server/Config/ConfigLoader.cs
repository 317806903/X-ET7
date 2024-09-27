using System;
using System.Collections.Generic;
using System.IO;
using Bright.Serialization;

namespace ET.Server
{
    [Invoke]
    public class GetAllConfigBytes: AInvokeHandler<ConfigComponent.GetAllConfigBytes, Dictionary<Type, ByteBuf>>
    {
        public override Dictionary<Type, ByteBuf> Handle(ConfigComponent.GetAllConfigBytes args)
        {
            Dictionary<Type, ByteBuf> output = new Dictionary<Type, ByteBuf>();
            List<string> startConfigs = new List<string>()
            {
                "StartMachineConfigCategory",
                "StartProcessConfigCategory",
                "StartSceneConfigCategory",
                "StartZoneConfigCategory",
            };
            HashSet<Type> configTypes = EventSystem.Instance.GetTypes(typeof (ConfigAttribute));
#if UNITY_EDITOR
            string configPath = "cs";
#else
            string configPath = "s";
#endif
            foreach (Type configType in configTypes)
            {
                string configFilePath;
                if (startConfigs.Contains(configType.Name))
                {
                    configFilePath = $"../Config/Excel/{configPath}/{Options.Instance.StartConfig}/{configType.Name.ToLower()}.bytes";
                }
                else if (configType.FullName.StartsWith("ET.AbilityConfig."))
                {
                    configFilePath = $"../Config/Excel/{configPath}/AbilityConfig/{configType.Name.ToLower()}.bytes";
                }
                else
                {
                    configFilePath = $"../Config/Excel/{configPath}/GameConfig/{configType.Name.ToLower()}.bytes";
                }
                Log.Debug($"GetAllConfigBytes {configFilePath}");

                bool isExists = File.Exists(configFilePath);
                if (isExists == false)
                {
                    Log.Error($"==GetAllConfigBytes {configFilePath} isExists == false");
                    continue;
                }

                output[configType] = new ByteBuf(File.ReadAllBytes(configFilePath));
            }

            return output;
        }
    }

    [Invoke]
    public class GetOneConfigBytes: AInvokeHandler<ConfigComponent.GetOneConfigBytes, ByteBuf>
    {
        public override ByteBuf Handle(ConfigComponent.GetOneConfigBytes args)
        {
            ByteBuf configBytes = new ByteBuf(File.ReadAllBytes($"../Config/{args.ConfigName}.bytes"));
            return configBytes;
        }
    }

    [Invoke]
    public class GetCodeMode: AInvokeHandler<ConfigComponent.GetCodeMode, string>
    {
        public override string Handle(ConfigComponent.GetCodeMode args)
        {
#if DOTNET
            return "Server";
#else
            return GlobalConfig.Instance.CodeMode.ToString();
#endif
        }
    }

    [Invoke]
    public class GetLocalDBSavePath: AInvokeHandler<ConfigComponent.GetLocalDBSavePath, string>
    {
        public override string Handle(ConfigComponent.GetLocalDBSavePath args)
        {
#if DOTNET
            return "../LocalDB";
#else
            return Path.Combine(PathHelper.AppHotfixResPath, "LocalDB");
#endif
        }
    }

    [Invoke]
    public class GetLocalMeshSavePath: AInvokeHandler<ConfigComponent.GetLocalMeshSavePath, string>
    {
        public override string Handle(ConfigComponent.GetLocalMeshSavePath args)
        {
#if DOTNET
            return "../LocalMeshData";
#else
            return Path.Combine(PathHelper.AppHotfixResPath, "LocalMeshData");
#endif
        }
    }
}