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

public sealed partial class ActionCfg_TimelinePlay: Bright.Config.BeanBase
{
    public ActionCfg_TimelinePlay(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        NewTimelineCfgId = _buf.ReadString();
        PostInit();
    }

    public static ActionCfg_TimelinePlay DeserializeActionCfg_TimelinePlay(ByteBuf _buf)
    {
        return new ActionCfg_TimelinePlay(_buf);
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
    /// 新的timeline
    /// </summary>
    public string NewTimelineCfgId { get; private set; }
    public TimelineCfg NewTimelineCfgId_Ref { get; private set; }

    public const int __ID__ = -700838490;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        this.NewTimelineCfgId_Ref = (_tables["TimelineCfgCategory"] as TimelineCfgCategory).GetOrDefault(NewTimelineCfgId);
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
        + "NewTimelineCfgId:" + NewTimelineCfgId + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}