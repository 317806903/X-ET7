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
   
[Config]
public partial class ResEffectCfgCategory: ConfigSingleton<ResEffectCfgCategory>
{
    private readonly Dictionary<string, ResEffectCfg> _dataMap;
    private readonly List<ResEffectCfg> _dataList;
    
    public ResEffectCfgCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, ResEffectCfg>();
        _dataList = new List<ResEffectCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ResEffectCfg _v;
            _v = ResEffectCfg.DeserializeResEffectCfg(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, ResEffectCfg> GetAll()
    {
        return _dataMap;
    }
    
    public List<ResEffectCfg> DataList => _dataList;

    public ResEffectCfg GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ResEffectCfg Get(string key) => _dataMap[key];
    public ResEffectCfg this[string key] => _dataMap[key];

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
        return typeof(ResEffectCfg).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}