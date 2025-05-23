using System;
using System.Collections.Generic;
using System.IO;
using Bright.Serialization;
using UnityEngine;
using YooAsset;

namespace ET.Client
{
    [Invoke]
    public class GetAllConfigBytes: AInvokeHandler<ConfigComponent.GetAllConfigBytes, Dictionary<Type, ByteBuf>>
    {
        public override Dictionary<Type, ByteBuf> Handle(ConfigComponent.GetAllConfigBytes args)
        {
            Dictionary<Type, ByteBuf> output = new Dictionary<Type, ByteBuf>();
            HashSet<Type> configTypes = EventSystem.Instance.GetTypes(typeof(ConfigAttribute));

            bool isReadEditor = false;
            if (Define.IsEditor)
            {
                if (ResConfig.Instance.ResLoadMode == EPlayMode.EditorSimulateMode)
                {
                    isReadEditor = true;
                }
                else
                {
                    isReadEditor = false;
                }
            }
            else
            {
                isReadEditor = false;
            }

            List<string> startConfigs = new List<string>()
            {
                "StartMachineConfigCategory",
                "StartProcessConfigCategory",
                "StartSceneConfigCategory",
                "StartZoneConfigCategory",
            };

            string ct = "cs";
            CodeMode codeMode = GlobalConfig.Instance.CodeMode;
            switch (codeMode)
            {
                case CodeMode.Client:
                    ct = "c";
                    break;
                case CodeMode.Server:
                    ct = "s";
                    break;
                case CodeMode.ClientServer:
                    ct = "cs";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (isReadEditor)
            {
                foreach (Type configType in configTypes)
                {
                    string configFilePath;
                    if (startConfigs.Contains(configType.Name))
                    {
                        if (ct == "c")
                        {
                            continue;
                        }
                        configFilePath = $"../Config/Excel/{ct}/{Options.Instance.StartConfig}/{configType.Name.ToLower()}.bytes";
                    }
                    else if(configType.FullName.StartsWith("ET.AbilityConfig."))
                    {
                        configFilePath = $"../Config/Excel/{ct}/AbilityConfig/{configType.Name.ToLower()}.bytes";
                    }
                    else
                    {
                        configFilePath = $"../Config/Excel/{ct}/GameConfig/{configType.Name.ToLower()}.bytes";
                    }
                    Log.Debug($"GetAllConfigBytes {configType.Name.ToLower()} {configFilePath}");
                    output[configType] = new ByteBuf(File.ReadAllBytes(configFilePath));
                }
            }
            else
            {
                string startConfigPath = Path.GetFileName(Options.Instance.StartConfig);
                foreach (Type configType in configTypes)
                {
                    string configFilePath;
                    if (startConfigs.Contains(configType.Name))
                    {
                        if (ct == "c")
                        {
                            continue;
                        }
                        if (ct == "cs")
                        {
                            if (Application.isMobilePlatform == false)
                            {
                                configFilePath = $"../Config/Excel/{ct}/{Options.Instance.StartConfig}/{configType.Name.ToLower()}.bytes";
                                output[configType] = new ByteBuf(File.ReadAllBytes(configFilePath));
                                continue;
                            }
                        }
                        configFilePath = $"{startConfigPath}_{configType.Name.ToLower()}";
                    }
                    else
                    {
                        configFilePath = configType.Name.ToLower();
                    }
                    Log.Debug($"GetAllConfigBytes {configType.Name.ToLower()}=>{configFilePath}");
                    TextAsset v = ResComponent.Instance.LoadAsset<TextAsset>(configFilePath) as TextAsset;
                    output[configType] = new ByteBuf(v.bytes);
                }
            }

            return output;
        }
    }

    [Invoke]
    public class GetOneConfigBytes: AInvokeHandler<ConfigComponent.GetOneConfigBytes, ByteBuf>
    {
        public override ByteBuf Handle(ConfigComponent.GetOneConfigBytes args)
        {
            ByteBuf configBytes;
            string configName = args.ConfigName;
            string configFullName = args.ConfigFullName;
            bool isReadEditor = false;
            if (Define.IsEditor)
            {
                if (ResConfig.Instance.ResLoadMode == EPlayMode.EditorSimulateMode)
                {
                    isReadEditor = true;
                }
                else
                {
                    isReadEditor = false;
                }
            }
            else
            {
                isReadEditor = false;
            }

            List<string> startConfigs = new List<string>()
            {
                "StartMachineConfigCategory",
                "StartProcessConfigCategory",
                "StartSceneConfigCategory",
                "StartZoneConfigCategory",
            };

            string ct = "cs";
            CodeMode codeMode = GlobalConfig.Instance.CodeMode;
            switch (codeMode)
            {
                case CodeMode.Client:
                    ct = "c";
                    break;
                case CodeMode.Server:
                    ct = "s";
                    break;
                case CodeMode.ClientServer:
                    ct = "cs";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (isReadEditor)
            {
                string configFilePath;
                if (startConfigs.Contains(configName))
                {
                    configFilePath = $"../Config/Excel/{ct}/{Options.Instance.StartConfig}/{configName.ToLower()}.bytes";
                }
                else if(configFullName.StartsWith("ET.AbilityConfig."))
                {
                    configFilePath = $"../Config/Excel/{ct}/AbilityConfig/{configName.ToLower()}.bytes";
                }
                else
                {
                    configFilePath = $"../Config/Excel/{ct}/GameConfig/{configName.ToLower()}.bytes";
                }
                Log.Debug($"GetOneConfigBytes {configName} {configFilePath}");
                configBytes = new ByteBuf(File.ReadAllBytes(configFilePath));
            }
            else
            {
                string startConfigPath = Path.GetFileName(Options.Instance.StartConfig);
                string configFilePath;
                if (startConfigs.Contains(configName))
                {
                    if (ct == "cs")
                    {
                        if (Application.isMobilePlatform == false)
                        {
                            configFilePath = $"../Config/Excel/{ct}/{Options.Instance.StartConfig}/{configName.ToLower()}.bytes";
                            configBytes = new ByteBuf(File.ReadAllBytes(configFilePath));
                            return configBytes;
                        }
                    }
                    configFilePath = $"{startConfigPath}_{configName.ToLower()}";
                }
                else
                {
                    configFilePath = configName.ToLower();
                }
                Log.Debug($"GetOneConfigBytes {configName}=>{configFilePath}");
                TextAsset v = ResComponent.Instance.LoadAsset<TextAsset>(configFilePath) as TextAsset;
                configBytes = new ByteBuf(v.bytes);
            }

            return configBytes;
        }
    }

    [Invoke]
    public class GetCodeMode: AInvokeHandler<ConfigComponent.GetCodeMode, string>
    {
        public override string Handle(ConfigComponent.GetCodeMode args)
        {
            return GlobalConfig.Instance.CodeMode.ToString();
        }
    }

    [Invoke]
    public class GetLocalDBSavePath: AInvokeHandler<ConfigComponent.GetLocalDBSavePath, string>
    {
        public override string Handle(ConfigComponent.GetLocalDBSavePath args)
        {
            return Path.Combine(PathHelper.AppHotfixResPath, "LocalDB");
        }
    }

    [Invoke]
    public class GetLocalMeshSavePath: AInvokeHandler<ConfigComponent.GetLocalMeshSavePath, string>
    {
        public override string Handle(ConfigComponent.GetLocalMeshSavePath args)
        {
            return Path.Combine(PathHelper.AppHotfixResPath, "LocalMeshData");
        }
    }
}