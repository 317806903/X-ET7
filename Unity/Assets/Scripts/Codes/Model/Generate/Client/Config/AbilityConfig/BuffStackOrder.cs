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

public sealed partial class BuffStackOrder:  SelectObjectOrder 
{
    public BuffStackOrder(ByteBuf _buf)  : base(_buf) 
    {
        BuffDealSelectCondition = BuffDealSelectCondition.DeserializeBuffDealSelectCondition(_buf);
        PostInit();
    }

    public static BuffStackOrder DeserializeBuffStackOrder(ByteBuf _buf)
    {
        return new BuffStackOrder(_buf);
    }

    /// <summary>
    /// buff相关条件
    /// </summary>
    public BuffDealSelectCondition BuffDealSelectCondition { get; private set; }

    public const int __ID__ = -1874258311;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        base.Resolve(_tables);
        BuffDealSelectCondition?.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
        BuffDealSelectCondition?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "IsDescending:" + IsDescending + ","
        + "BuffDealSelectCondition:" + BuffDealSelectCondition + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}