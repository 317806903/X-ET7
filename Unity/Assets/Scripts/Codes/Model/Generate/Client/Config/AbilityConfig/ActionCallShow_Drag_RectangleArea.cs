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
/// 指定方向-矩形
/// </summary>
public sealed partial class ActionCallShow_Drag_RectangleArea:  ActionCallShow_Drag 
{
    public ActionCallShow_Drag_RectangleArea(ByteBuf _buf)  : base(_buf) 
    {
        RectangleArea = RectangleArea.DeserializeRectangleArea(_buf);
        PostInit();
    }

    public static ActionCallShow_Drag_RectangleArea DeserializeActionCallShow_Drag_RectangleArea(ByteBuf _buf)
    {
        return new ActionCallShow_Drag_RectangleArea(_buf);
    }

    /// <summary>
    /// 矩形信息
    /// </summary>
    public RectangleArea RectangleArea { get; private set; }

    public const int __ID__ = -750012705;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        base.Resolve(_tables);
        RectangleArea?.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
        RectangleArea?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "RectangleArea:" + RectangleArea + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}