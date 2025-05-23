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
/// 通过波次修改属性(waveIndex表示实际波数)(float chgValue = buffActionModifyAttribute.BaseValueWaveIndex*waveIndex + buffActionModifyAttribute.StackValueWaveIndex * stackCount*waveIndex)<br/>+<br/>通过循环波次修改属性(circleWaveIndex表示实际波数-从第N波开始循环)(float chgValue = buffActionModifyAttribute.BaseValueCircleWaveIndex*circleWaveIndex + buffActionModifyAttribute.StackValueCircleWaveIndex * stackCount*circleWaveIndex)<br/>+<br/>通过循环次数修改属性(circleNum表示第N次循环,circleIndex表示第N次循环中的第几个)(float chgValue = buffActionModifyAttribute.BaseValueCircleNumIndex*circleIndex + buffActionModifyAttribute.StackValueCircleNumIndex * stackCount*circleIndex) + buffActionModifyAttribute.BaseValueCircleNum*circleNum + buffActionModifyAttribute.StackValueCircleNum * stackCount
/// </summary>
public sealed partial class BuffActionModifyAttributeByWaveIndex:  BuffAction 
{
    public BuffActionModifyAttributeByWaveIndex(ByteBuf _buf)  : base(_buf) 
    {
        NumericType = (NumericType)_buf.ReadInt();
        BaseValueWaveIndex = _buf.ReadFloat();
        StackValueWaveIndex = _buf.ReadFloat();
        BaseValueStageNum = _buf.ReadFloat();
        StackValueStageNum = _buf.ReadFloat();
        BaseValueCircleWaveIndex = _buf.ReadFloat();
        StackValueCircleWaveIndex = _buf.ReadFloat();
        BaseValueCircleNumIndex = _buf.ReadFloat();
        StackValueCircleNumIndex = _buf.ReadFloat();
        BaseValueCircleNum = _buf.ReadFloat();
        StackValueCircleNum = _buf.ReadFloat();
        MaxChgValue = _buf.ReadFloat();
        PostInit();
    }

    public static BuffActionModifyAttributeByWaveIndex DeserializeBuffActionModifyAttributeByWaveIndex(ByteBuf _buf)
    {
        return new BuffActionModifyAttributeByWaveIndex(_buf);
    }

    /// <summary>
    /// 属性
    /// </summary>
    public NumericType NumericType { get; private set; }
    /// <summary>
    /// 初始附加(通过波次修改属性)
    /// </summary>
    public float BaseValueWaveIndex { get; private set; }
    /// <summary>
    /// 按照层数附加(通过波次修改属性)
    /// </summary>
    public float StackValueWaveIndex { get; private set; }
    /// <summary>
    /// 初始附加(通过阶段数修改属性)
    /// </summary>
    public float BaseValueStageNum { get; private set; }
    /// <summary>
    /// 按照层数附加(通过阶段数修改属性)
    /// </summary>
    public float StackValueStageNum { get; private set; }
    /// <summary>
    /// 初始附加(通过循环波次修改属性)
    /// </summary>
    public float BaseValueCircleWaveIndex { get; private set; }
    /// <summary>
    /// 按照层数附加(通过循环波次修改属性)
    /// </summary>
    public float StackValueCircleWaveIndex { get; private set; }
    /// <summary>
    /// 初始附加(通过循环次数中的序号修改属性)
    /// </summary>
    public float BaseValueCircleNumIndex { get; private set; }
    /// <summary>
    /// 按照层数附加(通过循环次数中的序号修改属性)
    /// </summary>
    public float StackValueCircleNumIndex { get; private set; }
    /// <summary>
    /// 初始附加(通过循环次数修改属性)
    /// </summary>
    public float BaseValueCircleNum { get; private set; }
    /// <summary>
    /// 按照层数附加(通过循环次数修改属性)
    /// </summary>
    public float StackValueCircleNum { get; private set; }
    /// <summary>
    /// 最大允许修改值(-1表示不限制)
    /// </summary>
    public float MaxChgValue { get; private set; }

    public const int __ID__ = -1506701879;
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
        + "NumericType:" + NumericType + ","
        + "BaseValueWaveIndex:" + BaseValueWaveIndex + ","
        + "StackValueWaveIndex:" + StackValueWaveIndex + ","
        + "BaseValueStageNum:" + BaseValueStageNum + ","
        + "StackValueStageNum:" + StackValueStageNum + ","
        + "BaseValueCircleWaveIndex:" + BaseValueCircleWaveIndex + ","
        + "StackValueCircleWaveIndex:" + StackValueCircleWaveIndex + ","
        + "BaseValueCircleNumIndex:" + BaseValueCircleNumIndex + ","
        + "StackValueCircleNumIndex:" + StackValueCircleNumIndex + ","
        + "BaseValueCircleNum:" + BaseValueCircleNum + ","
        + "StackValueCircleNum:" + StackValueCircleNum + ","
        + "MaxChgValue:" + MaxChgValue + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}