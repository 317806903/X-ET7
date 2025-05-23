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
/// 播放动作
/// </summary>
[Config]
public partial class ActionCfg_PlayAnimatorCategory: ConfigSingleton<ActionCfg_PlayAnimatorCategory>
{
    private readonly Dictionary<string, ActionCfg_PlayAnimator> _dataMap;
    private readonly List<ActionCfg_PlayAnimator> _dataList;
    
    public ActionCfg_PlayAnimatorCategory(ByteBuf _buf)
    {
        _dataMap = new Dictionary<string, ActionCfg_PlayAnimator>();
        _dataList = new List<ActionCfg_PlayAnimator>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ActionCfg_PlayAnimator _v;
            _v = ActionCfg_PlayAnimator.DeserializeActionCfg_PlayAnimator(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }
    
    public bool Contain(string id)
    {
        return _dataMap.ContainsKey(id);
    }

    public Dictionary<string, ActionCfg_PlayAnimator> GetAll()
    {
        return _dataMap;
    }
    
    public List<ActionCfg_PlayAnimator> DataList => _dataList;

    public ActionCfg_PlayAnimator GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ActionCfg_PlayAnimator Get(string key) => _dataMap[key];
    public ActionCfg_PlayAnimator this[string key] => _dataMap[key];

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
        return typeof(ActionCfg_PlayAnimator).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}