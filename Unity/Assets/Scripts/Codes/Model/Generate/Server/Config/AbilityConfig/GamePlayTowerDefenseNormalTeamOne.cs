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
/// 塔防(TeamOne)
/// </summary>
public sealed partial class GamePlayTowerDefenseNormalTeamOne:  GamePlayTowerDefenseBase 
{
    public GamePlayTowerDefenseNormalTeamOne(ByteBuf _buf)  : base(_buf) 
    {
        StageNum = _buf.ReadInt();
        MonsterWaveNumScalePercentCoefficientWhenStageNum = _buf.ReadFloat();
        MonsterWaveLevelScalePercentCoefficientWhenStageNum = _buf.ReadFloat();
        WaveRewardGoldScalePercentCoefficientWhenStageNum = _buf.ReadFloat();
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);CreateActionIds = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); CreateActionIds.Add(_e0);}}
        PostInit();
    }

    public static GamePlayTowerDefenseNormalTeamOne DeserializeGamePlayTowerDefenseNormalTeamOne(ByteBuf _buf)
    {
        return new GamePlayTowerDefenseNormalTeamOne(_buf);
    }

    /// <summary>
    /// N关一个阶段
    /// </summary>
    public int StageNum { get; private set; }
    /// <summary>
    /// 每阶段时刷怪数量增加x%
    /// </summary>
    public float MonsterWaveNumScalePercentCoefficientWhenStageNum { get; private set; }
    /// <summary>
    /// 每阶段时刷怪等级增加y%
    /// </summary>
    public float MonsterWaveLevelScalePercentCoefficientWhenStageNum { get; private set; }
    /// <summary>
    /// 每阶段时关卡奖励增加y%
    /// </summary>
    public float WaveRewardGoldScalePercentCoefficientWhenStageNum { get; private set; }
    /// <summary>
    /// 生成时Action事件id
    /// </summary>
    public System.Collections.Generic.List<string> CreateActionIds { get; private set; }

    public const int __ID__ = 770224741;
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
        + "GamePlayModeCfgId:" + GamePlayModeCfgId + ","
        + "StageNum:" + StageNum + ","
        + "MonsterWaveNumScalePercentCoefficientWhenStageNum:" + MonsterWaveNumScalePercentCoefficientWhenStageNum + ","
        + "MonsterWaveLevelScalePercentCoefficientWhenStageNum:" + MonsterWaveLevelScalePercentCoefficientWhenStageNum + ","
        + "WaveRewardGoldScalePercentCoefficientWhenStageNum:" + WaveRewardGoldScalePercentCoefficientWhenStageNum + ","
        + "CreateActionIds:" + Bright.Common.StringUtil.CollectionToString(CreateActionIds) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}