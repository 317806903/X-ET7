using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerSkillComponent : Entity, IAwake, IDestroy
    {
        public HashSet<string> learnSkillCfgList;
        public List<string> usingSkillCfgList;
        public HashSet<string> newSkillList = new();
    }
}