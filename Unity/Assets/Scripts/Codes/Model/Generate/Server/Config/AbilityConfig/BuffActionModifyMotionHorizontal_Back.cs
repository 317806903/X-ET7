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
/// 修改运动水平方向_背向
/// </summary>
public sealed partial class BuffActionModifyMotionHorizontal_Back:  BuffActionModifyMotionHorizontal 
{
    public BuffActionModifyMotionHorizontal_Back(ByteBuf _buf)  : base(_buf) 
    {
        PostInit();
    }

    public static BuffActionModifyMotionHorizontal_Back DeserializeBuffActionModifyMotionHorizontal_Back(ByteBuf _buf)
    {
        return new BuffActionModifyMotionHorizontal_Back(_buf);
    }


    public const int __ID__ = 1832939209;
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
        + "Speed:" + Speed + ","
        + "AcceleratedSpeed:" + AcceleratedSpeed + ","
        + "MotionTargetType:" + MotionTargetType + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}