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

public sealed partial class SkillConsumeCfg: Bright.Config.BeanBase
{
    public SkillConsumeCfg(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        TotalEnergy = _buf.ReadInt();
        ConsumeEnergy = _buf.ReadInt();
        ConsumeCommonEnergy = _buf.ReadInt();
        RestoreEnergyByTime = _buf.ReadFloat();
        RestoreEnergyByWave = _buf.ReadFloat();
        ResetFullEnergyByCostDiamond = _buf.ReadInt();
        PostInit();
    }

    public static SkillConsumeCfg DeserializeSkillConsumeCfg(ByteBuf _buf)
    {
        return new SkillConsumeCfg(_buf);
    }

    /// <summary>
    /// 这是id
    /// </summary>
    public string Id { get; private set; }
    public SkillCfg Id_Ref { get; private set; }
    /// <summary>
    /// 技能本身总能量点
    /// </summary>
    public int TotalEnergy { get; private set; }
    /// <summary>
    /// 消耗能量点
    /// </summary>
    public int ConsumeEnergy { get; private set; }
    /// <summary>
    /// 消耗通用能量点
    /// </summary>
    public int ConsumeCommonEnergy { get; private set; }
    /// <summary>
    /// 恢复能量点(每秒)
    /// </summary>
    public float RestoreEnergyByTime { get; private set; }
    /// <summary>
    /// 恢复能量点(每回合)
    /// </summary>
    public float RestoreEnergyByWave { get; private set; }
    /// <summary>
    /// 填充满能量需要花费钻石
    /// </summary>
    public int ResetFullEnergyByCostDiamond { get; private set; }

    public const int __ID__ = -638965799;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        this.Id_Ref = (_tables["SkillCfgCategory"] as SkillCfgCategory).GetOrDefault(Id);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "TotalEnergy:" + TotalEnergy + ","
        + "ConsumeEnergy:" + ConsumeEnergy + ","
        + "ConsumeCommonEnergy:" + ConsumeCommonEnergy + ","
        + "RestoreEnergyByTime:" + RestoreEnergyByTime + ","
        + "RestoreEnergyByWave:" + RestoreEnergyByWave + ","
        + "ResetFullEnergyByCostDiamond:" + ResetFullEnergyByCostDiamond + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}