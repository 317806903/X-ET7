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
public partial class GameGlobalBuffCfgCategory: ConfigSingleton<GameGlobalBuffCfgCategory>
{
    private readonly Dictionary<string, GameGlobalBuffCfg> _dataMap;
    private readonly List<GameGlobalBuffCfg> _dataList;
    
    public GameGlobalBuffCfgCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, GameGlobalBuffCfg>();
        _dataList = new List<GameGlobalBuffCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            GameGlobalBuffCfg _v;
            _v = GameGlobalBuffCfg.DeserializeGameGlobalBuffCfg(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, GameGlobalBuffCfg> GetAll()
    {
        return _dataMap;
    }
    
    public List<GameGlobalBuffCfg> DataList => _dataList;

    public GameGlobalBuffCfg GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public GameGlobalBuffCfg Get(string key) => _dataMap[key];
    public GameGlobalBuffCfg this[string key] => _dataMap[key];

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
        return typeof(GameGlobalBuffCfg).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}