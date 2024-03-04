using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ChildOf(typeof(RankComponent))]
    public class RankItemComponent : Entity, IAwake, IDestroy, IComparable
    {
        public long score;
        public long playerId;
        public long recordTime;
        public virtual int CompareTo(object obj)
        {
            RankItemComponent rankItemComponent = (RankItemComponent)obj;
            if (this.score != rankItemComponent.score)
            {
                return this.score.CompareTo(rankItemComponent.score);
            }
            if (this.recordTime != rankItemComponent.recordTime)
            {
                return this.recordTime.CompareTo(rankItemComponent.recordTime);
            }
            return 0;
        }
    }
}