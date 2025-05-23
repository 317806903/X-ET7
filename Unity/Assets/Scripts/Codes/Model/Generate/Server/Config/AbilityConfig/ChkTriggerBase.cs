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
/// 触发条件
/// </summary>
public abstract partial class ChkTriggerBase: Bright.Config.BeanBase
{
    public ChkTriggerBase(ByteBuf _buf) 
    {
        PostInit();
    }

    public static ChkTriggerBase DeserializeChkTriggerBase(ByteBuf _buf)
    {
        switch (_buf.ReadInt())
        {
            case TriggerImmediatelyNoParam.__ID__: return new TriggerImmediatelyNoParam(_buf);
            case TriggerImmediatelyPutTower.__ID__: return new TriggerImmediatelyPutTower(_buf);
            case TriggerImmediatelyScaleTower.__ID__: return new TriggerImmediatelyScaleTower(_buf);
            case TriggerImmediatelyReclaimTower.__ID__: return new TriggerImmediatelyReclaimTower(_buf);
            case TriggerImmediatelyUpgradeTower.__ID__: return new TriggerImmediatelyUpgradeTower(_buf);
            case TriggerImmediatelyTowerKillMonster.__ID__: return new TriggerImmediatelyTowerKillMonster(_buf);
            case TriggerChkConditionImmediately.__ID__: return new TriggerChkConditionImmediately(_buf);
            case TriggerChkConditionStatus.__ID__: return new TriggerChkConditionStatus(_buf);
            default: throw new SerializationException();
        }
    }



    public virtual void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        PostResolve();
    }

    public virtual void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}