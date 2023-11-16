using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ChildOf(typeof(RankManagerComponent))]
    public class RankEndlessChallengeComponent : RankComponent
    {
    }
}