using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET.AbilityConfig
{
	public partial class TowerDefense_ChallengeLevelCfgCategory
	{
		[ProtoIgnore]
		[BsonIgnore]
		public SortedDictionary<int, TowerDefense_ChallengeLevelCfg> Challenges = new();

		[ProtoIgnore]
		[BsonIgnore]
		public SortedDictionary<int, TowerDefense_ChallengeLevelCfg> AR_Challenges = new();

		public SortedDictionary<int, TowerDefense_ChallengeLevelCfg> GetChallenges(bool isAR)
		{
			if (isAR)
			{
				return AR_Challenges;
			}
			return Challenges;
		}

		public TowerDefense_ChallengeLevelCfg GetChallengeByIndex(bool isAR, int index)
		{
			var dic = this.GetChallenges(isAR);
			if (dic.TryGetValue(index, out var challenge))
			{
				return challenge;
			}
			return null;
		}

		public bool ChkIsAR(string battleLevelCfgId)
		{
			if (this.Contain(battleLevelCfgId) == false)
			{
				return false;
			}
			TowerDefense_ChallengeLevelCfg challengeLevelCfg = this.Get(battleLevelCfgId);
			return challengeLevelCfg.IsAR;
		}

		public int GetChallengeIndex(string battleLevelCfgId)
		{
			if (this.Contain(battleLevelCfgId) == false)
			{
				return -1;
			}
			TowerDefense_ChallengeLevelCfg challengeLevelCfg = this.Get(battleLevelCfgId);
			int level = challengeLevelCfg.Index;

			return level;
		}

		public TowerDefense_ChallengeLevelCfg GetNextChallenge(string battleLevelCfgId)
		{
			if (this.Contain(battleLevelCfgId) == false)
			{
				Log.Error($"GetNextChallenge Contain({battleLevelCfgId}) == false");
				return null;
			}
			TowerDefense_ChallengeLevelCfg challengeLevelCfg = this.Get(battleLevelCfgId);
			int level = challengeLevelCfg.Index;
			TowerDefense_ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(challengeLevelCfg.IsAR, level + 1);

			return nextChallengeLevelCfg;
		}

		partial void PostResolve()
		{
			foreach (var challengeCfg in this.DataList)
			{
				if (challengeCfg.IsAR)
				{
					this.AR_Challenges.Add(challengeCfg.Index, challengeCfg);
				}
				else
				{
					this.Challenges.Add(challengeCfg.Index, challengeCfg);
				}
			}
		}
	}
}
