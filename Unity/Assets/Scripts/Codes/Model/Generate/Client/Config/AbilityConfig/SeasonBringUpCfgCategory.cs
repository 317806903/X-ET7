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
/// 赛季加成表
/// </summary>
[Config]
public partial class SeasonBringUpCfgCategory: ConfigSingleton<SeasonBringUpCfgCategory>
{
    private readonly List<SeasonBringUpCfg> _dataList;

    private Dictionary<(string, int), SeasonBringUpCfg> _dataMapUnion;

    public SeasonBringUpCfgCategory(ByteBuf _buf)
    {
        _dataList = new List<SeasonBringUpCfg>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            SeasonBringUpCfg _v;
            _v = SeasonBringUpCfg.DeserializeSeasonBringUpCfg(_buf);
            _dataList.Add(_v);
        }
        _dataMapUnion = new Dictionary<(string, int), SeasonBringUpCfg>();
        foreach(var _v in _dataList)
        {
            _dataMapUnion.Add((_v.Id, _v.Level), _v);
        }
        PostInit();
    }

    public List<SeasonBringUpCfg> DataList => _dataList;

    public SeasonBringUpCfg Get(string id, int level) => _dataMapUnion.TryGetValue((id, level), out SeasonBringUpCfg __v) ? __v : null;

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
        _dataList.TrimExcess();
    }
        
    
    public override string ConfigName()
    {
        return typeof(SeasonBringUpCfg).Name;
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}