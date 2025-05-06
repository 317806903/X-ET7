using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerBattleCardComponent : Entity, IAwake, IDestroy
    {
        public List<string> itemCfgIdList;
        public List<string> itemCfgIdList_MonsterCall;
    }
}