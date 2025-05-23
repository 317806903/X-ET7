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
/// 判断是否tower
/// </summary>
public sealed partial class ChkIsTowerCondition:  UnitConditionBase 
{
    public ChkIsTowerCondition(ByteBuf _buf)  : base(_buf) 
    {
        TowerType = (TowerType)_buf.ReadInt();
        PostInit();
    }

    public static ChkIsTowerCondition DeserializeChkIsTowerCondition(ByteBuf _buf)
    {
        return new ChkIsTowerCondition(_buf);
    }

    /// <summary>
    /// towerType
    /// </summary>
    public TowerType TowerType { get; private set; }

    public const int __ID__ = -63403902;
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
        + "TowerType:" + TowerType + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}