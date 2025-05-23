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
public sealed partial class ProbabilityRecordCondition:  UnitConditionBase 
{
    public ProbabilityRecordCondition(ByteBuf _buf)  : base(_buf) 
    {
        RecordKey = (RecordKeyInt)_buf.ReadInt();
        PostInit();
    }

    public static ProbabilityRecordCondition DeserializeProbabilityRecordCondition(ByteBuf _buf)
    {
        return new ProbabilityRecordCondition(_buf);
    }

    /// <summary>
    /// 概率
    /// </summary>
    public RecordKeyInt RecordKey { get; private set; }

    public const int __ID__ = -294643979;
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
        + "RecordKey:" + RecordKey + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}