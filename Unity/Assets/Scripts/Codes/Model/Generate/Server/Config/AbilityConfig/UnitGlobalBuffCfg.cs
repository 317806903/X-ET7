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

public sealed partial class UnitGlobalBuffCfg: Bright.Config.BeanBase
{
    public UnitGlobalBuffCfg(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        if(_buf.ReadBool()){ TagGroup = (BuffTagGroupType)_buf.ReadInt(); } else { TagGroup = null; }
        Priority = _buf.ReadInt();
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);TickTime = new System.Collections.Generic.List<float>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { float _e0;  _e0 = _buf.ReadFloat(); TickTime.Add(_e0);}}
        MonitorTriggers = GlobalBuffActionCall.DeserializeGlobalBuffActionCall(_buf);
        PostInit();
    }

    public static UnitGlobalBuffCfg DeserializeUnitGlobalBuffCfg(ByteBuf _buf)
    {
        return new UnitGlobalBuffCfg(_buf);
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
    /// tagGroup标志(同组只会有一个生效)
    /// </summary>
    public BuffTagGroupType? TagGroup { get; private set; }
    /// <summary>
    /// buff优先级(越小越低)
    /// </summary>
    public int Priority { get; private set; }
    /// <summary>
    /// 工作周期(秒)
    /// </summary>
    public System.Collections.Generic.List<float> TickTime { get; private set; }
    public GlobalBuffActionCall MonitorTriggers { get; private set; }

    public const int __ID__ = -757242198;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        MonitorTriggers?.Resolve(_tables);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        MonitorTriggers?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "TagGroup:" + TagGroup + ","
        + "Priority:" + Priority + ","
        + "TickTime:" + Bright.Common.StringUtil.CollectionToString(TickTime) + ","
        + "MonitorTriggers:" + MonitorTriggers + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}