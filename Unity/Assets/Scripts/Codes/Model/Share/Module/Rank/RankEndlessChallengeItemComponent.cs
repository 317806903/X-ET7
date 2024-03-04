using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ChildOf(typeof(RankComponent))]
    public class RankEndlessChallengeItemComponent : RankItemComponent
    {
        public long killNum;
        public override int CompareTo(object obj)
        {
            try
            {
                RankEndlessChallengeItemComponent rankEndlessChallengeItemComponent2 = (RankEndlessChallengeItemComponent)obj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            RankEndlessChallengeItemComponent rankEndlessChallengeItemComponent = (RankEndlessChallengeItemComponent)obj;
            if (this.score != rankEndlessChallengeItemComponent.score)
            {
                return this.score.CompareTo(rankEndlessChallengeItemComponent.score);
            }
            if (this.killNum != rankEndlessChallengeItemComponent.killNum)
            {
                return this.killNum.CompareTo(rankEndlessChallengeItemComponent.killNum);
            }
            if (this.recordTime != rankEndlessChallengeItemComponent.recordTime)
            {
                return this.recordTime.CompareTo(rankEndlessChallengeItemComponent.recordTime);
            }
            return 0;
        }
    }
}