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

public sealed partial class ActionCfg_EffectCreate: Bright.Config.BeanBase
{
    public ActionCfg_EffectCreate(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        Desc = _buf.ReadString();
        IsOnlySelfShow = _buf.ReadBool();
        ResEffectId = _buf.ReadString();
        PlayAudioActionId = _buf.ReadString();
        FloatingTextId = _buf.ReadString();
        Key = _buf.ReadString();
        MaxKeyNum = _buf.ReadInt();
        Duration = _buf.ReadFloat();
        EffectShowType = (EffectShowType)_buf.ReadInt();
        IsSceneEffect = _buf.ReadBool();
        IsScaleByUnit = _buf.ReadBool();
        OffSetInfo = OffSetInfo.DeserializeOffSetInfo(_buf);
        PostInit();
    }

    public static ActionCfg_EffectCreate DeserializeActionCfg_EffectCreate(ByteBuf _buf)
    {
        return new ActionCfg_EffectCreate(_buf);
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
    /// 是否仅自己可见
    /// </summary>
    public bool IsOnlySelfShow { get; private set; }
    /// <summary>
    /// 特效资源id
    /// </summary>
    public string ResEffectId { get; private set; }
    public ResEffectCfg ResEffectId_Ref { get; private set; }
    /// <summary>
    /// 音效资源id
    /// </summary>
    public string PlayAudioActionId { get; private set; }
    public ActionCfg_PlayAudio PlayAudioActionId_Ref { get; private set; }
    /// <summary>
    /// 飘字id
    /// </summary>
    public string FloatingTextId { get; private set; }
    public ActionCfg_FloatingText FloatingTextId_Ref { get; private set; }
    /// <summary>
    /// 唯一key(用来便于准确删除)
    /// </summary>
    public string Key { get; private set; }
    /// <summary>
    /// 指定key后限制最大数量(-1表示不限制)
    /// </summary>
    public int MaxKeyNum { get; private set; }
    /// <summary>
    /// 持续时间(s),-1表示永久
    /// </summary>
    public float Duration { get; private set; }
    /// <summary>
    /// 特效类型
    /// </summary>
    public EffectShowType EffectShowType { get; private set; }
    /// <summary>
    /// 是否场景特效
    /// </summary>
    public bool IsSceneEffect { get; private set; }
    /// <summary>
    /// 是否跟随unit缩放
    /// </summary>
    public bool IsScaleByUnit { get; private set; }
    public OffSetInfo OffSetInfo { get; private set; }

    public const int __ID__ = 1283151998;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        this.ResEffectId_Ref = (_tables["ResEffectCfgCategory"] as ResEffectCfgCategory).GetOrDefault(ResEffectId);
        this.PlayAudioActionId_Ref = (_tables["ActionCfg_PlayAudioCategory"] as ActionCfg_PlayAudioCategory).GetOrDefault(PlayAudioActionId);
        this.FloatingTextId_Ref = (_tables["ActionCfg_FloatingTextCategory"] as ActionCfg_FloatingTextCategory).GetOrDefault(FloatingTextId);
        OffSetInfo?.Resolve(_tables);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        OffSetInfo?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "Desc:" + Desc + ","
        + "IsOnlySelfShow:" + IsOnlySelfShow + ","
        + "ResEffectId:" + ResEffectId + ","
        + "PlayAudioActionId:" + PlayAudioActionId + ","
        + "FloatingTextId:" + FloatingTextId + ","
        + "Key:" + Key + ","
        + "MaxKeyNum:" + MaxKeyNum + ","
        + "Duration:" + Duration + ","
        + "EffectShowType:" + EffectShowType + ","
        + "IsSceneEffect:" + IsSceneEffect + ","
        + "IsScaleByUnit:" + IsScaleByUnit + ","
        + "OffSetInfo:" + OffSetInfo + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}