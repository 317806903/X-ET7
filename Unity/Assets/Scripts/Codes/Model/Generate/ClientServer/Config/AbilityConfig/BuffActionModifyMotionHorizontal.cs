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
/// 修改运动水平方向
/// </summary>
public abstract partial class BuffActionModifyMotionHorizontal:  BuffActionModifyMotion 
{
    public BuffActionModifyMotionHorizontal(ByteBuf _buf)  : base(_buf) 
    {
        MotionTargetType = (MotionTargetType)_buf.ReadInt();
        PostInit();
    }

    public static BuffActionModifyMotionHorizontal DeserializeBuffActionModifyMotionHorizontal(ByteBuf _buf)
    {
        switch (_buf.ReadInt())
        {
            case BuffActionModifyMotionHorizontal_Forward.__ID__: return new BuffActionModifyMotionHorizontal_Forward(_buf);
            case BuffActionModifyMotionHorizontal_Back.__ID__: return new BuffActionModifyMotionHorizontal_Back(_buf);
            default: throw new SerializationException();
        }
    }

    /// <summary>
    /// 运动目标状态
    /// </summary>
    public MotionTargetType MotionTargetType { get; private set; }


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