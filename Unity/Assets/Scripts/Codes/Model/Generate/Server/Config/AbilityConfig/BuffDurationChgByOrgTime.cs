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
/// 使用原时长来修改总时间(剩余时间为总时间-之前的剩余时间)(orgDuration*orgTimeScale + stackCountScale*orgDuration*(stackCount-1))
/// </summary>
public sealed partial class BuffDurationChgByOrgTime:  BuffDealType 
{
    public BuffDurationChgByOrgTime(ByteBuf _buf)  : base(_buf) 
    {
        OrgTimeScale = _buf.ReadFloat();
        StackCountScale = _buf.ReadFloat();
        PostInit();
    }

    public static BuffDurationChgByOrgTime DeserializeBuffDurationChgByOrgTime(ByteBuf _buf)
    {
        return new BuffDurationChgByOrgTime(_buf);
    }

    /// <summary>
    /// 原时长的缩放
    /// </summary>
    public float OrgTimeScale { get; private set; }
    /// <summary>
    /// 层数的缩放
    /// </summary>
    public float StackCountScale { get; private set; }

    public const int __ID__ = -747107905;
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
        + "OrgTimeScale:" + OrgTimeScale + ","
        + "StackCountScale:" + StackCountScale + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}