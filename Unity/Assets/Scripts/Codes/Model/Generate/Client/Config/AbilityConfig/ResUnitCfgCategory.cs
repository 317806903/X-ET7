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
public partial class ResUnitCfgCategory: ConfigSingleton<ResUnitCfgCategory>
{
    private readonly Dictionary<string, ResUnitCfg> _dataMap;
    private readonly List<ResUnitCfg> _dataList;
    
    public ResUnitCfgCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, ResUnitCfg>();
        _dataList = new List<ResUnitCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ResUnitCfg _v;
            _v = ResUnitCfg.DeserializeResUnitCfg(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, ResUnitCfg> GetAll()
    {
        return _dataMap;
    }
    
    public List<ResUnitCfg> DataList => _dataList;

    public ResUnitCfg GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ResUnitCfg Get(string key) => _dataMap[key];
    public ResUnitCfg this[string key] => _dataMap[key];

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
        return typeof(ResUnitCfg).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}