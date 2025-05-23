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
public partial class LocalizeConfig_UI_Category: ConfigSingleton<LocalizeConfig_UI_Category>
{
    private readonly Dictionary<string, LocalizeConfig> _dataMap;
    private readonly List<LocalizeConfig> _dataList;
    
    public LocalizeConfig_UI_Category(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, LocalizeConfig>();
        _dataList = new List<LocalizeConfig>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            LocalizeConfig _v;
            _v = LocalizeConfig.DeserializeLocalizeConfig(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Key, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, LocalizeConfig> GetAll()
    {
        return _dataMap;
    }
    
    public List<LocalizeConfig> DataList => _dataList;

    public LocalizeConfig GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public LocalizeConfig Get(string key) => _dataMap[key];
    public LocalizeConfig this[string key] => _dataMap[key];

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
        return typeof(LocalizeConfig).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}