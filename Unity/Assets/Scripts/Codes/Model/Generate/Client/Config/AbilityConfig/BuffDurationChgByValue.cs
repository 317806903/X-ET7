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
/// 直接修改总时间(剩余时间为总时间-之前的剩余时间)
/// </summary>
public sealed partial class BuffDurationChgByValue:  BuffDealType 
{
    public BuffDurationChgByValue(ByteBuf _buf)  : base(_buf) 
    {
        AddTime = _buf.ReadFloat();
        PostInit();
    }

    public static BuffDurationChgByValue DeserializeBuffDurationChgByValue(ByteBuf _buf)
    {
        return new BuffDurationChgByValue(_buf);
    }

    /// <summary>
    /// 增加的时长(s)
    /// </summary>
    public float AddTime { get; private set; }

    public const int __ID__ = 706861791;
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
        + "AddTime:" + AddTime + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}