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
public partial class UnitCfgCategory: ConfigSingleton<UnitCfgCategory>
{
    private readonly Dictionary<string, UnitCfg> _dataMap;
    private readonly List<UnitCfg> _dataList;
    
    public UnitCfgCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, UnitCfg>();
        _dataList = new List<UnitCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            UnitCfg _v;
            _v = UnitCfg.DeserializeUnitCfg(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, UnitCfg> GetAll()
    {
        return _dataMap;
    }
    
    public List<UnitCfg> DataList => _dataList;

    public UnitCfg GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public UnitCfg Get(string key) => _dataMap[key];
    public UnitCfg this[string key] => _dataMap[key];

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
        return typeof(UnitCfg).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}