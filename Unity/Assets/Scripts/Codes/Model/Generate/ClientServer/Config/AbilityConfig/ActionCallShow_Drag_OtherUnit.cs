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
/// 远处-单体
/// </summary>
public sealed partial class ActionCallShow_Drag_OtherUnit:  ActionCallShow_Drag 
{
    public ActionCallShow_Drag_OtherUnit(ByteBuf _buf)  : base(_buf) 
    {
        PostInit();
    }

    public static ActionCallShow_Drag_OtherUnit DeserializeActionCallShow_Drag_OtherUnit(ByteBuf _buf)
    {
        return new ActionCallShow_Drag_OtherUnit(_buf);
    }


    public const int __ID__ = -943689097;
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
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}