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
/// 判断是否拥有BuffTagType
/// </summary>
public sealed partial class BuffTagTypeCondition:  UnitConditionBase 
{
    public BuffTagTypeCondition(ByteBuf _buf)  : base(_buf) 
    {
        BuffTagType = (BuffTagType)_buf.ReadInt();
        PostInit();
    }

    public static BuffTagTypeCondition DeserializeBuffTagTypeCondition(ByteBuf _buf)
    {
        return new BuffTagTypeCondition(_buf);
    }

    public BuffTagType BuffTagType { get; private set; }

    public const int __ID__ = -876913638;
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
        + "BuffTagType:" + BuffTagType + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}