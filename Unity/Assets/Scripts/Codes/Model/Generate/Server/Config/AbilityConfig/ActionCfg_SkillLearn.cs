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

public sealed partial class ActionCfg_SkillLearn: Bright.Config.BeanBase
{
    public ActionCfg_SkillLearn(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        SkillId = _buf.ReadString();
        SkillLevel = _buf.ReadInt();
        SkillSlotType = (SkillSlotType)_buf.ReadInt();
        PostInit();
    }

    public static ActionCfg_SkillLearn DeserializeActionCfg_SkillLearn(ByteBuf _buf)
    {
        return new ActionCfg_SkillLearn(_buf);
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
    /// 技能id
    /// </summary>
    public string SkillId { get; private set; }
    public SkillCfg SkillId_Ref { get; private set; }
    /// <summary>
    /// 技能等级
    /// </summary>
    public int SkillLevel { get; private set; }
    /// <summary>
    /// 技能类型
    /// </summary>
    public SkillSlotType SkillSlotType { get; private set; }

    public const int __ID__ = 1064374596;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        this.SkillId_Ref = (_tables["SkillCfgCategory"] as SkillCfgCategory).GetOrDefault(SkillId);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "SkillId:" + SkillId + ","
        + "SkillLevel:" + SkillLevel + ","
        + "SkillSlotType:" + SkillSlotType + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}