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

public sealed partial class DropItemOne:  DropItemBase 
{
    public DropItemOne(ByteBuf _buf)  : base(_buf) 
    {
        ItemId = _buf.ReadString();
        PostInit();
    }

    public static DropItemOne DeserializeDropItemOne(ByteBuf _buf)
    {
        return new DropItemOne(_buf);
    }

    /// <summary>
    /// 掉落道具ID
    /// </summary>
    public string ItemId { get; private set; }
    public ItemCfg ItemId_Ref { get; private set; }

    public const int __ID__ = -218188732;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        base.Resolve(_tables);
        this.ItemId_Ref = (_tables["ItemCfgCategory"] as ItemCfgCategory).GetOrDefault(ItemId);
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
        + "ItemId:" + ItemId + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}