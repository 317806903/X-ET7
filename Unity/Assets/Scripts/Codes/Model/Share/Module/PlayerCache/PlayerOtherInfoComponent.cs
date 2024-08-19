using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerOtherInfoComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<string, bool> battleGuideStatus;
        public Dictionary<string, int> battleGuideStepIndex;

        public HashSet<UIRedDotType> uiRedDotTypes;
    }
}