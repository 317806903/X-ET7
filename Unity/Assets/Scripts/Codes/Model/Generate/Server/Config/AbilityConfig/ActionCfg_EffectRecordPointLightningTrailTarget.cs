//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;


namespace ET.AbilityConfig
{

public sealed partial class ActionCfg_EffectRecordPointLightningTrailTarget: Bright.Config.BeanBase
{
    public ActionCfg_EffectRecordPointLightningTrailTarget(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        PostInit();
    }

    public static ActionCfg_EffectRecordPointLightningTrailTarget DeserializeActionCfg_EffectRecordPointLightningTrailTarget(ByteBuf _buf)
    {
        return new ActionCfg_EffectRecordPointLightningTrailTarget(_buf);
    }

    /// <summary>
    /// 这是id
    /// </summary>
    public string Id { get; private set; }
    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; private set; }

    public const int __ID__ = 877659946;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}