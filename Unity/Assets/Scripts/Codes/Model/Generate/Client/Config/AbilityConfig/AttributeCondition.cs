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
/// 判断属性值
/// </summary>
public sealed partial class AttributeCondition:  UnitConditionBase 
{
    public AttributeCondition(ByteBuf _buf)  : base(_buf) 
    {
        NumericType = (NumericType)_buf.ReadInt();
        Value = _buf.ReadFloat();
        PostInit();
    }

    public static AttributeCondition DeserializeAttributeCondition(ByteBuf _buf)
    {
        return new AttributeCondition(_buf);
    }

    /// <summary>
    /// 属性
    /// </summary>
    public NumericType NumericType { get; private set; }
    /// <summary>
    /// 数值
    /// </summary>
    public float Value { get; private set; }

    public const int __ID__ = 1121439231;
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
        + "NumericType:" + NumericType + ","
        + "Value:" + Value + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}