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

public sealed partial class DropRuleOne:  DropItemBase 
{
    public DropRuleOne(ByteBuf _buf)  : base(_buf) 
    {
        DropRuleId = _buf.ReadString();
        PostInit();
    }

    public static DropRuleOne DeserializeDropRuleOne(ByteBuf _buf)
    {
        return new DropRuleOne(_buf);
    }

    /// <summary>
    /// 掉落规则嵌套
    /// </summary>
    public string DropRuleId { get; private set; }
    public DropRuleCfg DropRuleId_Ref { get; private set; }

    public const int __ID__ = -785734725;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        base.Resolve(_tables);
        this.DropRuleId_Ref = (_tables["DropRuleCfgCategory"] as DropRuleCfgCategory).GetOrDefault(DropRuleId);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "DropNum:" + DropNum + ","
        + "DropRatio:" + DropRatio + ","
        + "DropRuleId:" + DropRuleId + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}