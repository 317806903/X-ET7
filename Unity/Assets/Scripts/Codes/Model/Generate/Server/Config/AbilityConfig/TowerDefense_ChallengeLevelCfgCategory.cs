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
/// 玩法_塔防_闯关模式配置
/// </summary>
[Config]
public partial class TowerDefense_ChallengeLevelCfgCategory: ConfigSingleton<TowerDefense_ChallengeLevelCfgCategory>
{
    private readonly Dictionary<int, ChallengeLevelCfg> _dataMap;
    private readonly List<ChallengeLevelCfg> _dataList;
    
    public TowerDefense_ChallengeLevelCfgCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<int, ChallengeLevelCfg>();
        _dataList = new List<ChallengeLevelCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ChallengeLevelCfg _v;
            _v = ChallengeLevelCfg.DeserializeChallengeLevelCfg(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Index, _v);
        }
        PostInit();
    }
    
    public bool Contain(int id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<int, ChallengeLevelCfg> GetAll()
    {
        return _dataMap;
    }
    
    public List<ChallengeLevelCfg> DataList => _dataList;

    public ChallengeLevelCfg GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ChallengeLevelCfg Get(int key) => _dataMap[key];
    public ChallengeLevelCfg this[int key] => _dataMap[key];

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
        return typeof(ChallengeLevelCfg).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}