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
/// 偏移信息
/// </summary>
public sealed partial class OffSetInfo: Bright.Config.BeanBase
{
    public OffSetInfo(ByteBuf _buf) 
    {
        NodeName = (EffectNodeName)_buf.ReadInt();
        OffSetPosition = _buf.ReadVector3();
        RelateRotation = _buf.ReadVector3();
        KeepHorizontal = _buf.ReadBool();
        PostInit();
    }

    public static OffSetInfo DeserializeOffSetInfo(ByteBuf _buf)
    {
        return new OffSetInfo(_buf);
    }

    /// <summary>
    /// 特效表现节点类型
    /// </summary>
    public EffectNodeName NodeName { get; private set; }
    /// <summary>
    /// 相对偏移
    /// </summary>
    public System.Numerics.Vector3 OffSetPosition { get; private set; }
    /// <summary>
    /// 相对旋转
    /// </summary>
    public System.Numerics.Vector3 RelateRotation { get; private set; }
    /// <summary>
    /// 是否保持水平
    /// </summary>
    public bool KeepHorizontal { get; private set; }

    public const int __ID__ = -1289080863;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "NodeName:" + NodeName + ","
        + "OffSetPosition:" + OffSetPosition + ","
        + "RelateRotation:" + RelateRotation + ","
        + "KeepHorizontal:" + KeepHorizontal + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}