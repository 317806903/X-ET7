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

public sealed partial class MailCfg: Bright.Config.BeanBase
{
    public MailCfg(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        MailType = _buf.ReadString();
        MailTitle_l10n_key = _buf.ReadString(); MailTitle = _buf.ReadString();
        MailContent_l10n_key = _buf.ReadString(); MailContent = _buf.ReadString();
        DropRuleId = _buf.ReadString();
        EffectiveTime = _buf.ReadInt();
        PostInit();
    }

    public static MailCfg DeserializeMailCfg(ByteBuf _buf)
    {
        return new MailCfg(_buf);
    }

    /// <summary>
    /// 邮件模版id
    /// </summary>
    public string Id { get; private set; }
    /// <summary>
    /// 邮件类型
    /// </summary>
    public string MailType { get; private set; }
    public MailTypeCfg MailType_Ref { get; private set; }
    /// <summary>
    /// 邮件标题
    /// </summary>
    public string MailTitle { get; private set; }
    public string MailTitle_l10n_key { get; }
    /// <summary>
    /// 邮件内容
    /// </summary>
    public string MailContent { get; private set; }
    public string MailContent_l10n_key { get; }
    /// <summary>
    /// 邮件附件
    /// </summary>
    public string DropRuleId { get; private set; }
    public DropRuleCfg DropRuleId_Ref { get; private set; }
    /// <summary>
    /// 邮件有效时长(天)
    /// </summary>
    public int EffectiveTime { get; private set; }

    public const int __ID__ = -1799378163;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        this.MailType_Ref = (_tables["MailTypeCfgCategory"] as MailTypeCfgCategory).GetOrDefault(MailType);
        this.DropRuleId_Ref = (_tables["DropRuleCfgCategory"] as DropRuleCfgCategory).GetOrDefault(DropRuleId);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        MailTitle = translator(MailTitle_l10n_key, MailTitle);
        MailContent = translator(MailContent_l10n_key, MailContent);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "MailType:" + MailType + ","
        + "MailTitle:" + MailTitle + ","
        + "MailContent:" + MailContent + ","
        + "DropRuleId:" + DropRuleId + ","
        + "EffectiveTime:" + EffectiveTime + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}