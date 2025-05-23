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
/// 判断和发起者的距离
/// </summary>
public sealed partial class DisCondition:  UnitConditionBase 
{
    public DisCondition(ByteBuf _buf)  : base(_buf) 
    {
        Dis = _buf.ReadFloat();
        PostInit();
    }

    public static DisCondition DeserializeDisCondition(ByteBuf _buf)
    {
        return new DisCondition(_buf);
    }

    /// <summary>
    /// 距离
    /// </summary>
    public float Dis { get; private set; }

    public const int __ID__ = 504798285;
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
        + "Dis:" + Dis + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}