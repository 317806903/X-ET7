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
/// 召唤Actor
/// </summary>
[Config]
public partial class ActionCfg_CallActorCategory: ConfigSingleton<ActionCfg_CallActorCategory>
{
    private readonly Dictionary<string, ActionCfg_CallActor> _dataMap;
    private readonly List<ActionCfg_CallActor> _dataList;
    
    public ActionCfg_CallActorCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, ActionCfg_CallActor>();
        _dataList = new List<ActionCfg_CallActor>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ActionCfg_CallActor _v;
            _v = ActionCfg_CallActor.DeserializeActionCfg_CallActor(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, ActionCfg_CallActor> GetAll()
    {
        return _dataMap;
    }
    
    public List<ActionCfg_CallActor> DataList => _dataList;

    public ActionCfg_CallActor GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ActionCfg_CallActor Get(string key) => _dataMap[key];
    public ActionCfg_CallActor this[string key] => _dataMap[key];

    public override void Resolve(Dictionary<string, IConfigSingleton> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    public override void TrimExcess()
    {
        _dataMap.TrimExcess();
        _dataList.TrimExcess();
    }
    
    
    public override string ConfigName()
    {
        return typeof(ActionCfg_CallActor).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}