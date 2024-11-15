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

public sealed partial class PlayerSkillCfg: Bright.Config.BeanBase
{
    public PlayerSkillCfg(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Level = _buf.ReadInt();
        TutorialCfgId = _buf.ReadString();
        IsShowInBattleDeckUI = _buf.ReadBool();
        UnLockCondition = UnLockConditionBase.DeserializeUnLockConditionBase(_buf);
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);Labels = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); Labels.Add(_e0);}}
        PropertyType = _buf.ReadString();
        LearnOrUpdateCost = _buf.ReadInt();
        NextPlayerSkillCfgId = _buf.ReadString();
        LearnOrUpdateCondition = _buf.ReadString();
        PostInit();
    }

    public static PlayerSkillCfg DeserializePlayerSkillCfg(ByteBuf _buf)
    {
        return new PlayerSkillCfg(_buf);
    }

    /// <summary>
    /// 这是id
    /// </summary>
    public string Id { get; private set; }
    public SkillCfg Id_Ref { get; private set; }
    /// <summary>
    /// 等级
    /// </summary>
    public int Level { get; private set; }
    /// <summary>
    /// 指引视频
    /// </summary>
    public string TutorialCfgId { get; private set; }
    public TutorialCfg TutorialCfgId_Ref { get; private set; }
    /// <summary>
    /// 是否在UI面板中展示
    /// </summary>
    public bool IsShowInBattleDeckUI { get; private set; }
    /// <summary>
    /// 解锁条件
    /// </summary>
    public UnLockConditionBase UnLockCondition { get; private set; }
    /// <summary>
    /// 标签
    /// </summary>
    public System.Collections.Generic.List<string> Labels { get; private set; }
    /// <summary>
    /// 属性类型
    /// </summary>
    public string PropertyType { get; private set; }
    /// <summary>
    /// 学习或升级消耗
    /// </summary>
    public int LearnOrUpdateCost { get; private set; }
    /// <summary>
    /// 下一级id
    /// </summary>
    public string NextPlayerSkillCfgId { get; private set; }
    public PlayerSkillCfg NextPlayerSkillCfgId_Ref { get; private set; }
    /// <summary>
    /// 学习或升级条件
    /// </summary>
    public string LearnOrUpdateCondition { get; private set; }

    public const int __ID__ = 1778264692;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        this.Id_Ref = (_tables["SkillCfgCategory"] as SkillCfgCategory).GetOrDefault(Id);
        this.TutorialCfgId_Ref = (_tables["TutorialCfgCategory"] as TutorialCfgCategory).GetOrDefault(TutorialCfgId);
        UnLockCondition?.Resolve(_tables);
        this.NextPlayerSkillCfgId_Ref = (_tables["PlayerSkillCfgCategory"] as PlayerSkillCfgCategory).GetOrDefault(NextPlayerSkillCfgId);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        UnLockCondition?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Level:" + Level + ","
        + "TutorialCfgId:" + TutorialCfgId + ","
        + "IsShowInBattleDeckUI:" + IsShowInBattleDeckUI + ","
        + "UnLockCondition:" + UnLockCondition + ","
        + "Labels:" + Bright.Common.StringUtil.CollectionToString(Labels) + ","
        + "PropertyType:" + PropertyType + ","
        + "LearnOrUpdateCost:" + LearnOrUpdateCost + ","
        + "NextPlayerSkillCfgId:" + NextPlayerSkillCfgId + ","
        + "LearnOrUpdateCondition:" + LearnOrUpdateCondition + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}