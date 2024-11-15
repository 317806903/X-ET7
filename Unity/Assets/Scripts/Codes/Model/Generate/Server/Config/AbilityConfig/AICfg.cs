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

public sealed partial class AICfg: Bright.Config.BeanBase
{
    public AICfg(ByteBuf _buf) 
    {
        AIConfigId = _buf.ReadString();
        Order = _buf.ReadInt();
        Name = _buf.ReadString();
        Desc = _buf.ReadString();
        WaitFrameNum = _buf.ReadInt();
        ResetTargetTime = _buf.ReadFloat();
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);SelectObject = new System.Collections.Generic.List<string>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { string _e0;  _e0 = _buf.ReadString(); SelectObject.Add(_e0);}}
        {int n0 = System.Math.Min(_buf.ReadSize(), _buf.Size);NodeParams = new System.Collections.Generic.List<int>(n0);for(var i0 = 0 ; i0 < n0 ; i0++) { int _e0;  _e0 = _buf.ReadInt(); NodeParams.Add(_e0);}}
        PostInit();
    }

    public static AICfg DeserializeAICfg(ByteBuf _buf)
    {
        return new AICfg(_buf);
    }

    /// <summary>
    /// 所属ai
    /// </summary>
    public string AIConfigId { get; private set; }
    /// <summary>
    /// 此ai中的顺序
    /// </summary>
    public int Order { get; private set; }
    /// <summary>
    /// 节点名字
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Desc { get; private set; }
    /// <summary>
    /// 间隔多少次执行(每次0.5秒)
    /// </summary>
    public int WaitFrameNum { get; private set; }
    /// <summary>
    /// 多久后(秒)重置目标,-1表示不重置
    /// </summary>
    public float ResetTargetTime { get; private set; }
    /// <summary>
    /// 目标
    /// </summary>
    public System.Collections.Generic.List<string> SelectObject { get; private set; }
    public System.Collections.Generic.List<SelectObjectCfg> SelectObject_Ref { get; private set; }
    /// <summary>
    /// 节点参数
    /// </summary>
    public System.Collections.Generic.List<int> NodeParams { get; private set; }

    public const int __ID__ = 62271260;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        { SelectObjectCfgCategory __table = (SelectObjectCfgCategory)_tables["SelectObjectCfgCategory"]; this.SelectObject_Ref = new System.Collections.Generic.List<SelectObjectCfg>(); foreach(var __e in SelectObject) { this.SelectObject_Ref.Add(__table.GetOrDefault(__e)); } }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "AIConfigId:" + AIConfigId + ","
        + "Order:" + Order + ","
        + "Name:" + Name + ","
        + "Desc:" + Desc + ","
        + "WaitFrameNum:" + WaitFrameNum + ","
        + "ResetTargetTime:" + ResetTargetTime + ","
        + "SelectObject:" + Bright.Common.StringUtil.CollectionToString(SelectObject) + ","
        + "NodeParams:" + Bright.Common.StringUtil.CollectionToString(NodeParams) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}