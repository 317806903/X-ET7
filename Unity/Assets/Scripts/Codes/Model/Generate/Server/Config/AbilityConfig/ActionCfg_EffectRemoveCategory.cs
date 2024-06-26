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
/// 移除特效
/// </summary>
[Config]
public partial class ActionCfg_EffectRemoveCategory: ConfigSingleton<ActionCfg_EffectRemoveCategory>
{
    private readonly Dictionary<string, ActionCfg_EffectRemove> _dataMap;
    private readonly List<ActionCfg_EffectRemove> _dataList;
    
    public ActionCfg_EffectRemoveCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, ActionCfg_EffectRemove>();
        _dataList = new List<ActionCfg_EffectRemove>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ActionCfg_EffectRemove _v;
            _v = ActionCfg_EffectRemove.DeserializeActionCfg_EffectRemove(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, ActionCfg_EffectRemove> GetAll()
    {
        return _dataMap;
    }
    
    public List<ActionCfg_EffectRemove> DataList => _dataList;

    public ActionCfg_EffectRemove GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ActionCfg_EffectRemove Get(string key) => _dataMap[key];
    public ActionCfg_EffectRemove this[string key] => _dataMap[key];

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
        return typeof(ActionCfg_EffectRemove).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}