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

/// <summary>
/// 立即触发(无参数)
/// </summary>
public sealed partial class TriggerImmediatelyNoParam:  TriggerImmediatelyBase 
{
    public TriggerImmediatelyNoParam(ByteBuf _buf)  : base(_buf) 
    {
        PostInit();
    }

    public static TriggerImmediatelyNoParam DeserializeTriggerImmediatelyNoParam(ByteBuf _buf)
    {
        return new TriggerImmediatelyNoParam(_buf);
    }


    public const int __ID__ = 1833631782;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        base.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "TriggerType:" + TriggerType + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}