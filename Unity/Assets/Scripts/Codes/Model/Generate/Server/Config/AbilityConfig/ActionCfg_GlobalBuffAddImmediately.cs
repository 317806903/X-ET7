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

public sealed partial class ActionCfg_GlobalBuffAddImmediately: Bright.Config.BeanBase
{
    public ActionCfg_GlobalBuffAddImmediately(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        Name = _buf.ReadString();
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);GameActionHomeTeamCfgId = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); GameActionHomeTeamCfgId.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);GameActionPlayerSelfCfgId = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); GameActionPlayerSelfCfgId.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);GameActionPlayerAllCfgId = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); GameActionPlayerAllCfgId.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);GameActionUnitCfgId = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); GameActionUnitCfgId.Add(_e0);}}
        PostInit();
    }

    public static ActionCfg_GlobalBuffAddImmediately DeserializeActionCfg_GlobalBuffAddImmediately(ByteBuf _buf)
    {
        return new ActionCfg_GlobalBuffAddImmediately(_buf);
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
    /// 对阵营执行gameAction
    /// </summary>
    public System.Collections.Generic.List<string> GameActionHomeTeamCfgId { get; private set; }
    /// <summary>
    /// 对自身执行gameAction
    /// </summary>
    public System.Collections.Generic.List<string> GameActionPlayerSelfCfgId { get; private set; }
    /// <summary>
    /// 给同个阵营所有player加gameAction
    /// </summary>
    public System.Collections.Generic.List<string> GameActionPlayerAllCfgId { get; private set; }
    /// <summary>
    /// 给自身unit加gameAction
    /// </summary>
    public System.Collections.Generic.List<string> GameActionUnitCfgId { get; private set; }

    public const int __ID__ = -1001682204;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
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
        + "GameActionHomeTeamCfgId:" + Bright.Common.StringUtil.CollectionToString(GameActionHomeTeamCfgId) + ","
        + "GameActionPlayerSelfCfgId:" + Bright.Common.StringUtil.CollectionToString(GameActionPlayerSelfCfgId) + ","
        + "GameActionPlayerAllCfgId:" + Bright.Common.StringUtil.CollectionToString(GameActionPlayerAllCfgId) + ","
        + "GameActionUnitCfgId:" + Bright.Common.StringUtil.CollectionToString(GameActionUnitCfgId) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}