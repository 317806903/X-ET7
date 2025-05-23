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

public sealed partial class ActionCfg_DamageUnit: Bright.Config.BeanBase
{
    public ActionCfg_DamageUnit(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        Desc = _buf.ReadString();
        FloatingTextId = _buf.ReadString();
        DamageInfo = DamageInfo.DeserializeDamageInfo(_buf);
        PostInit();
    }

    public static ActionCfg_DamageUnit DeserializeActionCfg_DamageUnit(ByteBuf _buf)
    {
        return new ActionCfg_DamageUnit(_buf);
    }

    /// <summary>
    /// 这是id
    /// </summary>
    public string Id { get; private set; }
    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Desc { get; private set; }
    /// <summary>
    /// 飘字id
    /// </summary>
    public string FloatingTextId { get; private set; }
    public ActionCfg_FloatingText FloatingTextId_Ref { get; private set; }
    public DamageInfo DamageInfo { get; private set; }

    public const int __ID__ = 1994931300;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        this.FloatingTextId_Ref = (_tables["ActionCfg_FloatingTextCategory"] as ActionCfg_FloatingTextCategory).GetOrDefault(FloatingTextId);
        DamageInfo?.Resolve(_tables);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        DamageInfo?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "Desc:" + Desc + ","
        + "FloatingTextId:" + FloatingTextId + ","
        + "DamageInfo:" + DamageInfo + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}