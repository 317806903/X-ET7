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
/// 条件判断触发(立即触发)
/// </summary>
public sealed partial class TriggerChkConditionImmediately:  TriggerChkConditionBase 
{
    public TriggerChkConditionImmediately(ByteBuf _buf)  : base(_buf) 
    {
        TriggerImmediatelyType = TriggerImmediatelyBase.DeserializeTriggerImmediatelyBase(_buf);
        PostInit();
    }

    public static TriggerChkConditionImmediately DeserializeTriggerChkConditionImmediately(ByteBuf _buf)
    {
        return new TriggerChkConditionImmediately(_buf);
    }

    public TriggerImmediatelyBase TriggerImmediatelyType { get; private set; }

    public const int __ID__ = 1036332529;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        base.Resolve(_tables);
        TriggerImmediatelyType?.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
        TriggerImmediatelyType?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "NeedCount:" + NeedCount + ","
        + "TriggerImmediatelyType:" + TriggerImmediatelyType + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}