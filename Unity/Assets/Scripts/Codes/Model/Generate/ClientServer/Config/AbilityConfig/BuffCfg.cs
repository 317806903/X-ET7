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

public sealed partial class BuffCfg: Bright.Config.BeanBase
{
    public BuffCfg(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        Icon = _buf.ReadString();
        Desc = _buf.ReadString();
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);Tags = new System.Collections.Generic.List<BuffTagType>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { BuffTagType _e0;  _e0 = (BuffTagType)_buf.ReadInt(); Tags.Add(_e0);}}
        if(_buf.ReadBool()){ TagGroup = (BuffTagGroupType)_buf.ReadInt(); } else { TagGroup = null; }
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);RemoveTags = new System.Collections.Generic.List<BuffTagType>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { BuffTagType _e0;  _e0 = (BuffTagType)_buf.ReadInt(); RemoveTags.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);RemoveTagGroups = new System.Collections.Generic.List<BuffTagGroupType>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { BuffTagGroupType _e0;  _e0 = (BuffTagGroupType)_buf.ReadInt(); RemoveTagGroups.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);ImmuneTags = new System.Collections.Generic.List<BuffTagType>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { BuffTagType _e0;  _e0 = (BuffTagType)_buf.ReadInt(); ImmuneTags.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);ImmuneTagGroups = new System.Collections.Generic.List<BuffTagGroupType>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { BuffTagGroupType _e0;  _e0 = (BuffTagGroupType)_buf.ReadInt(); ImmuneTagGroups.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);SelfEffectList = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); SelfEffectList.Add(_e0);}}
        SelfPlayAnimator = _buf.ReadString();
        BuffType = (BuffType)_buf.ReadInt();
        Priority = _buf.ReadInt();
        MaxStack = _buf.ReadInt();
        IsIgnoreCasterActor = _buf.ReadBool();
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);TickTime = new System.Collections.Generic.List<float>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { float _e0;  _e0 = _buf.ReadFloat(); TickTime.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);MonitorTriggers = new System.Collections.Generic.List<BuffActionCall>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { BuffActionCall _e0;  _e0 = BuffActionCall.DeserializeBuffActionCall(_buf); MonitorTriggers.Add(_e0);}}
        PostInit();
    }

    public static BuffCfg DeserializeBuffCfg(ByteBuf _buf)
    {
        return new BuffCfg(_buf);
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
    /// icon图标
    /// </summary>
    public string Icon { get; private set; }
    public ResIconCfg Icon_Ref { get; private set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Desc { get; private set; }
    /// <summary>
    /// tag标志
    /// </summary>
    public System.Collections.Generic.List<BuffTagType> Tags { get; private set; }
    /// <summary>
    /// tagGroup标志(同组只会有一个生效)
    /// </summary>
    public BuffTagGroupType? TagGroup { get; private set; }
    /// <summary>
    /// 移除哪个类型的buff
    /// </summary>
    public System.Collections.Generic.List<BuffTagType> RemoveTags { get; private set; }
    /// <summary>
    /// 移除TagGroup的buff
    /// </summary>
    public System.Collections.Generic.List<BuffTagGroupType> RemoveTagGroups { get; private set; }
    /// <summary>
    /// 免疫哪个类型的buff(这个新增的buff会加不上)
    /// </summary>
    public System.Collections.Generic.List<BuffTagType> ImmuneTags { get; private set; }
    /// <summary>
    /// 免疫TagGroup的buff
    /// </summary>
    public System.Collections.Generic.List<BuffTagGroupType> ImmuneTagGroups { get; private set; }
    /// <summary>
    /// 伴随buff的特效
    /// </summary>
    public System.Collections.Generic.List<string> SelfEffectList { get; private set; }
    public System.Collections.Generic.List<ActionCfg_EffectCreate> SelfEffectList_Ref { get; private set; }
    /// <summary>
    /// 伴随buff的动作
    /// </summary>
    public string SelfPlayAnimator { get; private set; }
    public ActionCfg_PlayAnimator SelfPlayAnimator_Ref { get; private set; }
    /// <summary>
    /// 增益还是减益Buff
    /// </summary>
    public BuffType BuffType { get; private set; }
    /// <summary>
    /// buff优先级(越小越低)
    /// </summary>
    public int Priority { get; private set; }
    /// <summary>
    /// buff最高层数
    /// </summary>
    public int MaxStack { get; private set; }
    /// <summary>
    /// 是否忽略发起者而进行合并
    /// </summary>
    public bool IsIgnoreCasterActor { get; private set; }
    /// <summary>
    /// buff工作周期(秒)
    /// </summary>
    public System.Collections.Generic.List<float> TickTime { get; private set; }
    public System.Collections.Generic.List<BuffActionCall> MonitorTriggers { get; private set; }

    public const int __ID__ = 1892616945;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        this.Icon_Ref = (_tables["ResIconCfgCategory"] as ResIconCfgCategory).GetOrDefault(Icon);
        { ActionCfg_EffectCreateCategory __table = (ActionCfg_EffectCreateCategory)_tables["ActionCfg_EffectCreateCategory"]; this.SelfEffectList_Ref = new System.Collections.Generic.List<ActionCfg_EffectCreate>(); foreach(var __e in SelfEffectList) { this.SelfEffectList_Ref.Add(__table.GetOrDefault(__e)); } }
        this.SelfPlayAnimator_Ref = (_tables["ActionCfg_PlayAnimatorCategory"] as ActionCfg_PlayAnimatorCategory).GetOrDefault(SelfPlayAnimator);
        foreach(var _e in MonitorTriggers) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var _e in MonitorTriggers) { _e?.TranslateText(translator); }
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "Icon:" + Icon + ","
        + "Desc:" + Desc + ","
        + "Tags:" + Bright.Common.StringUtil.CollectionToString(Tags) + ","
        + "TagGroup:" + TagGroup + ","
        + "RemoveTags:" + Bright.Common.StringUtil.CollectionToString(RemoveTags) + ","
        + "RemoveTagGroups:" + Bright.Common.StringUtil.CollectionToString(RemoveTagGroups) + ","
        + "ImmuneTags:" + Bright.Common.StringUtil.CollectionToString(ImmuneTags) + ","
        + "ImmuneTagGroups:" + Bright.Common.StringUtil.CollectionToString(ImmuneTagGroups) + ","
        + "SelfEffectList:" + Bright.Common.StringUtil.CollectionToString(SelfEffectList) + ","
        + "SelfPlayAnimator:" + SelfPlayAnimator + ","
        + "BuffType:" + BuffType + ","
        + "Priority:" + Priority + ","
        + "MaxStack:" + MaxStack + ","
        + "IsIgnoreCasterActor:" + IsIgnoreCasterActor + ","
        + "TickTime:" + Bright.Common.StringUtil.CollectionToString(TickTime) + ","
        + "MonitorTriggers:" + Bright.Common.StringUtil.CollectionToString(MonitorTriggers) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}