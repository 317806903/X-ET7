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
/// 额外获得GamePlayTowerDefense卡
/// </summary>
[Config]
public partial class ActionGameCfg_RewardGamePlayTowerDefenseCardsCategory: ConfigSingleton<ActionGameCfg_RewardGamePlayTowerDefenseCardsCategory>
{
    private readonly Dictionary<string, ActionGameCfg_RewardGamePlayTowerDefenseCards> _dataMap;
    private readonly List<ActionGameCfg_RewardGamePlayTowerDefenseCards> _dataList;
    
    public ActionGameCfg_RewardGamePlayTowerDefenseCardsCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, ActionGameCfg_RewardGamePlayTowerDefenseCards>();
        _dataList = new List<ActionGameCfg_RewardGamePlayTowerDefenseCards>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ActionGameCfg_RewardGamePlayTowerDefenseCards _v;
            _v = ActionGameCfg_RewardGamePlayTowerDefenseCards.DeserializeActionGameCfg_RewardGamePlayTowerDefenseCards(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, ActionGameCfg_RewardGamePlayTowerDefenseCards> GetAll()
    {
        return _dataMap;
    }
    
    public List<ActionGameCfg_RewardGamePlayTowerDefenseCards> DataList => _dataList;

    public ActionGameCfg_RewardGamePlayTowerDefenseCards GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ActionGameCfg_RewardGamePlayTowerDefenseCards Get(string key) => _dataMap[key];
    public ActionGameCfg_RewardGamePlayTowerDefenseCards this[string key] => _dataMap[key];

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
        return typeof(ActionGameCfg_RewardGamePlayTowerDefenseCards).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}