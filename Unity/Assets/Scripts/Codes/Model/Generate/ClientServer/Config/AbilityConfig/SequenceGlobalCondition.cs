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
/// 多个触发条件与操作
/// </summary>
public sealed partial class SequenceGlobalCondition: Bright.Config.BeanBase
{
    public SequenceGlobalCondition(ByteBuf _buf) 
    {
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);Conditions = new System.Collections.Generic.List<ChkTriggerBase>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { ChkTriggerBase _e0;  _e0 = ChkTriggerBase.DeserializeChkTriggerBase(_buf); Conditions.Add(_e0);}}
        PostInit();
    }

    public static SequenceGlobalCondition DeserializeSequenceGlobalCondition(ByteBuf _buf)
    {
        return new SequenceGlobalCondition(_buf);
    }

    public System.Collections.Generic.List<ChkTriggerBase> Conditions { get; private set; }

    public const int __ID__ = -1468830057;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        foreach(var _e in Conditions) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var _e in Conditions) { _e?.TranslateText(translator); }
    }

    public override string ToString()
    {
        return "{ "
        + "Conditions:" + Bright.Common.StringUtil.CollectionToString(Conditions) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}