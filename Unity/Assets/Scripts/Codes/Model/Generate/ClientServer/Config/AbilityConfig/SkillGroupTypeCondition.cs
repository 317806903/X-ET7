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
/// 判断当前是否技能组类型的技能
/// </summary>
public sealed partial class SkillGroupTypeCondition:  UnitConditionBase 
{
    public SkillGroupTypeCondition(ByteBuf _buf)  : base(_buf) 
    {
        SkillGroupType = (SkillGroupType)_buf.ReadInt();
        PostInit();
    }

    public static SkillGroupTypeCondition DeserializeSkillGroupTypeCondition(ByteBuf _buf)
    {
        return new SkillGroupTypeCondition(_buf);
    }

    /// <summary>
    /// 技能组类型
    /// </summary>
    public SkillGroupType SkillGroupType { get; private set; }

    public const int __ID__ = -1096791341;
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
        + "ConditionCompare:" + ConditionCompare + ","
        + "SkillGroupType:" + SkillGroupType + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}