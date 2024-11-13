using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerBattleSkillComponent : Entity, IAwake, IDestroy
    {
        public List<string> skillCfgIdList;
    }
}