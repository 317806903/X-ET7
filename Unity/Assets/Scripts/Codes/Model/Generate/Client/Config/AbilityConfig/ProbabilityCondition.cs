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
/// 判断概率(0-100)
/// </summary>
public sealed partial class ProbabilityCondition:  UnitConditionBase 
{
    public ProbabilityCondition(ByteBuf _buf)  : base(_buf) 
    {
        Value = _buf.ReadInt();
        PostInit();
    }

    public static ProbabilityCondition DeserializeProbabilityCondition(ByteBuf _buf)
    {
        return new ProbabilityCondition(_buf);
    }

    /// <summary>
    /// 概率
    /// </summary>
    public int Value { get; private set; }

    public const int __ID__ = -1869835130;
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
        + "ConditionCompare:" + ConditionCompare + ","
        + "Value:" + Value + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}