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
/// 塔击杀怪
/// </summary>
public sealed partial class TriggerImmediatelyTowerKillMonster:  TriggerImmediatelyBase 
{
    public TriggerImmediatelyTowerKillMonster(ByteBuf _buf)  : base(_buf) 
    {
        TowerType = (TowerType)_buf.ReadInt();
        TowerCfgId = _buf.ReadString();
        MonsterType = (MonsterType)_buf.ReadInt();
        MonsterCfgId = _buf.ReadString();
        PostInit();
    }

    public static TriggerImmediatelyTowerKillMonster DeserializeTriggerImmediatelyTowerKillMonster(ByteBuf _buf)
    {
        return new TriggerImmediatelyTowerKillMonster(_buf);
    }

    public TowerType TowerType { get; private set; }
    public string TowerCfgId { get; private set; }
    public TowerDefense_TowerCfg TowerCfgId_Ref { get; private set; }
    public MonsterType MonsterType { get; private set; }
    public string MonsterCfgId { get; private set; }
    public TowerDefense_MonsterCfg MonsterCfgId_Ref { get; private set; }

    public const int __ID__ = 1983598649;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        base.Resolve(_tables);
        this.TowerCfgId_Ref = (_tables["TowerDefense_TowerCfgCategory"] as TowerDefense_TowerCfgCategory).GetOrDefault(TowerCfgId);
        this.MonsterCfgId_Ref = (_tables["TowerDefense_MonsterCfgCategory"] as TowerDefense_MonsterCfgCategory).GetOrDefault(MonsterCfgId);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "TriggerType:" + TriggerType + ","
        + "TowerType:" + TowerType + ","
        + "TowerCfgId:" + TowerCfgId + ","
        + "MonsterType:" + MonsterType + ","
        + "MonsterCfgId:" + MonsterCfgId + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}