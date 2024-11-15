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
/// 环绕轨迹
/// </summary>
public sealed partial class AroundMoveTweenType:  SpeedMoveTweenType 
{
    public AroundMoveTweenType(ByteBuf _buf)  : base(_buf) 
    {
        InitAngle = _buf.ReadFloat();
        Radius = _buf.ReadFloat();
        RadiusAddSpeed = _buf.ReadFloat();
        PostInit();
    }

    public static AroundMoveTweenType DeserializeAroundMoveTweenType(ByteBuf _buf)
    {
        return new AroundMoveTweenType(_buf);
    }

    /// <summary>
    /// 初始角度
    /// </summary>
    public float InitAngle { get; private set; }
    /// <summary>
    /// 环绕半径
    /// </summary>
    public float Radius { get; private set; }
    /// <summary>
    /// 环绕半径增加速度
    /// </summary>
    public float RadiusAddSpeed { get; private set; }

    public const int __ID__ = 1087275655;
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
        + "InitAngle:" + InitAngle + ","
        + "Radius:" + Radius + ","
        + "RadiusAddSpeed:" + RadiusAddSpeed + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}