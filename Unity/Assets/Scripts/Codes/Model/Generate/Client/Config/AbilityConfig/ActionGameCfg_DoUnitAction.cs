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

public sealed partial class ActionGameCfg_DoUnitAction: Bright.Config.BeanBase
{
    public ActionGameCfg_DoUnitAction(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        Desc = _buf.ReadString();
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);MonitorTriggers = new System.Collections.Generic.List<UnitActionCall>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { UnitActionCall _e0;  _e0 = UnitActionCall.DeserializeUnitActionCall(_buf); MonitorTriggers.Add(_e0);}}
        PostInit();
    }

    public static ActionGameCfg_DoUnitAction DeserializeActionGameCfg_DoUnitAction(ByteBuf _buf)
    {
        return new ActionGameCfg_DoUnitAction(_buf);
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
    public System.Collections.Generic.List<UnitActionCall> MonitorTriggers { get; private set; }

    public const int __ID__ = 1376553832;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
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
        + "Desc:" + Desc + ","
        + "MonitorTriggers:" + Bright.Common.StringUtil.CollectionToString(MonitorTriggers) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}