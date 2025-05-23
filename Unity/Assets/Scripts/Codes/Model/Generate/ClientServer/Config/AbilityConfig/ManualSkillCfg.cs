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

public sealed partial class ManualSkillCfg: Bright.Config.BeanBase
{
    public ManualSkillCfg(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        SkillSelectAction = _buf.ReadString();
        PostInit();
    }

    public static ManualSkillCfg DeserializeManualSkillCfg(ByteBuf _buf)
    {
        return new ManualSkillCfg(_buf);
    }

    /// <summary>
    /// 这是id
    /// </summary>
    public string Id { get; private set; }
    public SkillCfg Id_Ref { get; private set; }
    /// <summary>
    /// 主动施法范围预览
    /// </summary>
    public string SkillSelectAction { get; private set; }
    public SelectObjectCfg SkillSelectAction_Ref { get; private set; }

    public const int __ID__ = 1249231449;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        this.Id_Ref = (_tables["SkillCfgCategory"] as SkillCfgCategory).GetOrDefault(Id);
        this.SkillSelectAction_Ref = (_tables["SelectObjectCfgCategory"] as SelectObjectCfgCategory).GetOrDefault(SkillSelectAction);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "SkillSelectAction:" + SkillSelectAction + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}