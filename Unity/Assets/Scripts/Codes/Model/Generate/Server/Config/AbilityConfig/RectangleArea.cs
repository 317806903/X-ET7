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
/// 矩形
/// </summary>
public sealed partial class RectangleArea:  AreaType 
{
    public RectangleArea(ByteBuf _buf)  : base(_buf) 
    {
        Width = _buf.ReadFloat();
        Length = _buf.ReadFloat();
        PostInit();
    }

    public static RectangleArea DeserializeRectangleArea(ByteBuf _buf)
    {
        return new RectangleArea(_buf);
    }

    /// <summary>
    /// 宽度(最左到最右)
    /// </summary>
    public float Width { get; private set; }
    /// <summary>
    /// 长度
    /// </summary>
    public float Length { get; private set; }

    public const int __ID__ = -1944989060;
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
        + "IgnoringHeight:" + IgnoringHeight + ","
        + "KeepHorizontal:" + KeepHorizontal + ","
        + "Width:" + Width + ","
        + "Length:" + Length + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}