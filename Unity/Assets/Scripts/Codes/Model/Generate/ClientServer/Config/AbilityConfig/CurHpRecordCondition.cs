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
/// 判断剩余血量
/// </summary>
public sealed partial class CurHpRecordCondition:  UnitConditionBase 
{
    public CurHpRecordCondition(ByteBuf _buf)  : base(_buf) 
    {
        IsPercent = _buf.ReadBool();
        RecordKey = (RecordKeyInt)_buf.ReadInt();
        PostInit();
    }

    public static CurHpRecordCondition DeserializeCurHpRecordCondition(ByteBuf _buf)
    {
        return new CurHpRecordCondition(_buf);
    }

    /// <summary>
    /// 是否百分比
    /// </summary>
    public bool IsPercent { get; private set; }
    /// <summary>
    /// 数值
    /// </summary>
    public RecordKeyInt RecordKey { get; private set; }

    public const int __ID__ = -52027550;
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
        + "IsPercent:" + IsPercent + ","
        + "RecordKey:" + RecordKey + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}