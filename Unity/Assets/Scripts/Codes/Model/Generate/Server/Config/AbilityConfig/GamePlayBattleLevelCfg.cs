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

public sealed partial class GamePlayBattleLevelCfg: Bright.Config.BeanBase
{
    public GamePlayBattleLevelCfg(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        IsGlobalMode = _buf.ReadBool();
        BattleGuideConfigFileName = _buf.ReadString();
        MaxPlayerCount = _buf.ReadInt();
        GamePlayMode = GamePlayModeBase.DeserializeGamePlayModeBase(_buf);
        TeamMode = TeamModeBase.DeserializeTeamModeBase(_buf);
        SceneMap = _buf.ReadString();
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);PlayerBirthPosList = new System.Collections.Generic.List<System.Collections.Generic.List<System.Numerics.Vector3>>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { System.Collections.Generic.List<System.Numerics.Vector3> _e0;  {int n1 = System.Math.Min(_buf.ReadSize(), _buf.Size);_e0 = new System.Collections.Generic.List<System.Numerics.Vector3>(n1);for(var i1 = 0 ; i1 < n1 ; i1++) { System.Numerics.Vector3 _e1;  _e1 = _buf.ReadVector3(); _e0.Add(_e1);}} PlayerBirthPosList.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);MonsterCallPosList = new System.Collections.Generic.List<System.Collections.Generic.List<System.Numerics.Vector3>>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { System.Collections.Generic.List<System.Numerics.Vector3> _e0;  {int n1 = System.Math.Min(_buf.ReadSize(), _buf.Size);_e0 = new System.Collections.Generic.List<System.Numerics.Vector3>(n1);for(var i1 = 0 ; i1 < n1 ; i1++) { System.Numerics.Vector3 _e1;  _e1 = _buf.ReadVector3(); _e0.Add(_e1);}} MonsterCallPosList.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);MusicList = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); MusicList.Add(_e0);}}
        PostInit();
    }

    public static GamePlayBattleLevelCfg DeserializeGamePlayBattleLevelCfg(ByteBuf _buf)
    {
        return new GamePlayBattleLevelCfg(_buf);
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
    /// 是全局模式还是房间模式
    /// </summary>
    public bool IsGlobalMode { get; private set; }
    /// <summary>
    /// 战斗指引文件名称
    /// </summary>
    public string BattleGuideConfigFileName { get; private set; }
    /// <summary>
    /// 玩家上限
    /// </summary>
    public int MaxPlayerCount { get; private set; }
    public GamePlayModeBase GamePlayMode { get; private set; }
    public TeamModeBase TeamMode { get; private set; }
    /// <summary>
    /// 地图场景名称
    /// </summary>
    public string SceneMap { get; private set; }
    /// <summary>
    /// 玩家出生点
    /// </summary>
    public System.Collections.Generic.List<System.Collections.Generic.List<System.Numerics.Vector3>> PlayerBirthPosList { get; private set; }
    /// <summary>
    /// 怪物初始点
    /// </summary>
    public System.Collections.Generic.List<System.Collections.Generic.List<System.Numerics.Vector3>> MonsterCallPosList { get; private set; }
    /// <summary>
    /// 背景音乐
    /// </summary>
    public System.Collections.Generic.List<string> MusicList { get; private set; }
    public System.Collections.Generic.List<ResAudioCfg> MusicList_Ref { get; private set; }

    public const int __ID__ = -517860578;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        GamePlayMode?.Resolve(_tables);
        TeamMode?.Resolve(_tables);
        { ResAudioCfgCategory __table = (ResAudioCfgCategory)_tables["ResAudioCfgCategory"]; this.MusicList_Ref = new System.Collections.Generic.List<ResAudioCfg>(); foreach(var __e in MusicList) { this.MusicList_Ref.Add(__table.GetOrDefault(__e)); } }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        GamePlayMode?.TranslateText(translator);
        TeamMode?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "IsGlobalMode:" + IsGlobalMode + ","
        + "BattleGuideConfigFileName:" + BattleGuideConfigFileName + ","
        + "MaxPlayerCount:" + MaxPlayerCount + ","
        + "GamePlayMode:" + GamePlayMode + ","
        + "TeamMode:" + TeamMode + ","
        + "SceneMap:" + SceneMap + ","
        + "PlayerBirthPosList:" + Bright.Common.StringUtil.CollectionToString(PlayerBirthPosList) + ","
        + "MonsterCallPosList:" + Bright.Common.StringUtil.CollectionToString(MonsterCallPosList) + ","
        + "MusicList:" + Bright.Common.StringUtil.CollectionToString(MusicList) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}