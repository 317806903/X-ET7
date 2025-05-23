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
/// 修改Buff剩余时长，层数，或者移除
/// </summary>
[Config]
public partial class ActionCfg_BuffDealCategory: ConfigSingleton<ActionCfg_BuffDealCategory>
{
    private readonly Dictionary<string, ActionCfg_BuffDeal> _dataMap;
    private readonly List<ActionCfg_BuffDeal> _dataList;
    
    public ActionCfg_BuffDealCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, ActionCfg_BuffDeal>();
        _dataList = new List<ActionCfg_BuffDeal>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ActionCfg_BuffDeal _v;
            _v = ActionCfg_BuffDeal.DeserializeActionCfg_BuffDeal(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, ActionCfg_BuffDeal> GetAll()
    {
        return _dataMap;
    }
    
    public List<ActionCfg_BuffDeal> DataList => _dataList;

    public ActionCfg_BuffDeal GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ActionCfg_BuffDeal Get(string key) => _dataMap[key];
    public ActionCfg_BuffDeal this[string key] => _dataMap[key];

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
        return typeof(ActionCfg_BuffDeal).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}