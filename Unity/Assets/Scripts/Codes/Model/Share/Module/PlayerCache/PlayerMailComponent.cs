using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerMailComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<string, FunctionMenuStatus> functionMenuDic;
    }
}