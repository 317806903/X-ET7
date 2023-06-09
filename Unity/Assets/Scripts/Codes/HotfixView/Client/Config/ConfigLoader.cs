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
            Root.Instance.Scene.AddComponent<ResComponent>();
            Dictionary<Type, ByteBuf> output = new Dictionary<Type, ByteBuf>();
            HashSet<Type> configTypes = EventSystem.Instance.GetTypes(typeof(ConfigAttribute));
            //GlobalConfig.Instance.PlayMode
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

            if (isReadEditor)
            {
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
                List<string> startConfigs = new List<string>()
                {
                    "StartMachineConfigCategory", 
                    "StartProcessConfigCategory", 
                    "StartSceneConfigCategory", 
                    "StartZoneConfigCategory",
                };
                foreach (Type configType in configTypes)
                {
                    string configFilePath;
                    if (startConfigs.Contains(configType.Name))
                    {
                        configFilePath = $"../Config/Excel/{ct}/{Options.Instance.StartConfig}/{configType.Name.ToLower()}.bytes";    
                    }
                    else if(configType.FullName.StartsWith("ET.AbilityConfig."))
                    {
                        configFilePath = $"../Config/Excel/{ct}/AbilityConfig/{configType.Name}.bytes";
                    }
                    else
                    {
                        configFilePath = $"../Config/Excel/{ct}/GameConfig/{configType.Name}.bytes";
                    }
                    Log.Debug($"GetAllConfigBytes {configType.Name} {configFilePath}");
                    output[configType] = new ByteBuf(File.ReadAllBytes(configFilePath));
                }
            }
            else
            {
                foreach (Type configType in configTypes)
                {
                    Log.Debug($"GetAllConfigBytes {configType.Name}");
                    TextAsset v = ResComponent.Instance.LoadAsset<TextAsset>(configType.Name.ToLower()) as TextAsset;
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
            throw new NotImplementedException("client cant use LoadOneConfig");
        }
    }
}