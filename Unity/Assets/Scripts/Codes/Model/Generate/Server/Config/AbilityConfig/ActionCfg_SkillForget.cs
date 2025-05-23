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

public sealed partial class ActionCfg_SkillForget: Bright.Config.BeanBase
{
    public ActionCfg_SkillForget(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        SkillId = _buf.ReadString();
        SkillSlotType = (SkillSlotType)_buf.ReadInt();
        SkillSlotIndex = _buf.ReadInt();
        SkillGroupType = (SkillGroupType)_buf.ReadInt();
        PostInit();
    }

    public static ActionCfg_SkillForget DeserializeActionCfg_SkillForget(ByteBuf _buf)
    {
        return new ActionCfg_SkillForget(_buf);
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
    /// 技能类型
    /// </summary>
    public SkillSlotType SkillSlotType { get; private set; }
    /// <summary>
    /// 技能类型序号(-1表示所有,0表示第一个)
    /// </summary>
    public int SkillSlotIndex { get; private set; }
    /// <summary>
    /// 技能组类型
    /// </summary>
    public SkillGroupType SkillGroupType { get; private set; }

    public const int __ID__ = -1526169875;
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
        + "SkillSlotType:" + SkillSlotType + ","
        + "SkillSlotIndex:" + SkillSlotIndex + ","
        + "SkillGroupType:" + SkillGroupType + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}