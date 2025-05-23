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
/// 掉落表
/// </summary>
[Config]
public partial class DropRuleCfgCategory: ConfigSingleton<DropRuleCfgCategory>
{
    private readonly Dictionary<string, DropRuleCfg> _dataMap;
    private readonly List<DropRuleCfg> _dataList;
    
    public DropRuleCfgCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, DropRuleCfg>();
        _dataList = new List<DropRuleCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            DropRuleCfg _v;
            _v = DropRuleCfg.DeserializeDropRuleCfg(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.DropRuleId, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, DropRuleCfg> GetAll()
    {
        return _dataMap;
    }
    
    public List<DropRuleCfg> DataList => _dataList;

    public DropRuleCfg GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public DropRuleCfg Get(string key) => _dataMap[key];
    public DropRuleCfg this[string key] => _dataMap[key];

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
        return typeof(DropRuleCfg).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}