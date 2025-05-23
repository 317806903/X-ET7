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

public sealed partial class GamePlayPKCfg: Bright.Config.BeanBase
{
    public GamePlayPKCfg(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);GlobalBuffAddList = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); GlobalBuffAddList.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);AddPlayerGlobalBuffAddList = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); AddPlayerGlobalBuffAddList.Add(_e0);}}
        PostInit();
    }

    public static GamePlayPKCfg DeserializeGamePlayPKCfg(ByteBuf _buf)
    {
        return new GamePlayPKCfg(_buf);
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
    /// 效果列表
    /// </summary>
    public System.Collections.Generic.List<string> GlobalBuffAddList { get; private set; }
    public System.Collections.Generic.List<ActionCfg_GlobalBuffAdd> GlobalBuffAddList_Ref { get; private set; }
    /// <summary>
    /// AddPlayer时效果列表
    /// </summary>
    public System.Collections.Generic.List<string> AddPlayerGlobalBuffAddList { get; private set; }
    public System.Collections.Generic.List<ActionCfg_GlobalBuffAddImmediately> AddPlayerGlobalBuffAddList_Ref { get; private set; }

    public const int __ID__ = -576511549;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        { ActionCfg_GlobalBuffAddCategory __table = (ActionCfg_GlobalBuffAddCategory)_tables["ActionCfg_GlobalBuffAddCategory"]; this.GlobalBuffAddList_Ref = new System.Collections.Generic.List<ActionCfg_GlobalBuffAdd>(); foreach(var __e in GlobalBuffAddList) { this.GlobalBuffAddList_Ref.Add(__table.GetOrDefault(__e)); } }
        { ActionCfg_GlobalBuffAddImmediatelyCategory __table = (ActionCfg_GlobalBuffAddImmediatelyCategory)_tables["ActionCfg_GlobalBuffAddImmediatelyCategory"]; this.AddPlayerGlobalBuffAddList_Ref = new System.Collections.Generic.List<ActionCfg_GlobalBuffAddImmediately>(); foreach(var __e in AddPlayerGlobalBuffAddList) { this.AddPlayerGlobalBuffAddList_Ref.Add(__table.GetOrDefault(__e)); } }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "GlobalBuffAddList:" + Bright.Common.StringUtil.CollectionToString(GlobalBuffAddList) + ","
        + "AddPlayerGlobalBuffAddList:" + Bright.Common.StringUtil.CollectionToString(AddPlayerGlobalBuffAddList) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}