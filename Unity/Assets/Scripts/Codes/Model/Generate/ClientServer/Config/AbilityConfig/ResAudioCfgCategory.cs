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
public partial class ResAudioCfgCategory: ConfigSingleton<ResAudioCfgCategory>
{
    private readonly Dictionary<string, ResAudioCfg> _dataMap;
    private readonly List<ResAudioCfg> _dataList;
    
    public ResAudioCfgCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, ResAudioCfg>();
        _dataList = new List<ResAudioCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ResAudioCfg _v;
            _v = ResAudioCfg.DeserializeResAudioCfg(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, ResAudioCfg> GetAll()
    {
        return _dataMap;
    }
    
    public List<ResAudioCfg> DataList => _dataList;

    public ResAudioCfg GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ResAudioCfg Get(string key) => _dataMap[key];
    public ResAudioCfg this[string key] => _dataMap[key];

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
        return typeof(ResAudioCfg).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}