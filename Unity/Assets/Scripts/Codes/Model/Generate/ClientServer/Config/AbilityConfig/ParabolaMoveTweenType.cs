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
/// 抛物线轨迹
/// </summary>
public sealed partial class ParabolaMoveTweenType:  SpeedMoveTweenType 
{
    public ParabolaMoveTweenType(ByteBuf _buf)  : base(_buf) 
    {
        ParabolaHeight = _buf.ReadFloat();
        PostInit();
    }

    public static ParabolaMoveTweenType DeserializeParabolaMoveTweenType(ByteBuf _buf)
    {
        return new ParabolaMoveTweenType(_buf);
    }

    /// <summary>
    /// 抛物线高度
    /// </summary>
    public float ParabolaHeight { get; private set; }

    public const int __ID__ = -1988199406;
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
        + "HoldTime:" + HoldTime + ","
        + "Speed:" + Speed + ","
        + "AcceleratedSpeed:" + AcceleratedSpeed + ","
        + "ParabolaHeight:" + ParabolaHeight + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}