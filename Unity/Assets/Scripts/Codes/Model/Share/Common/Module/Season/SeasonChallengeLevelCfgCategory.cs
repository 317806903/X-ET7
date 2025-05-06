using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET.AbilityConfig
{
	public partial class SeasonChallengeLevelCfgCategory
	{
		[ProtoIgnore]
		[BsonIgnore]
		public Dictionary<int, Dictionary<PVELevelDifficulty, SortedDictionary<int, ChallengeLevelCfg>>> SeasonChallenges = new();

		public SortedDictionary<int, ChallengeLevelCfg> GetChallenges(int seasonCfgId, PVELevelDifficulty pveLevelDifficulty)
		{
			if (this.SeasonChallenges.TryGetValue(seasonCfgId, out var tmpSeason))
			{
				if (tmpSeason.TryGetValue(pveLevelDifficulty, out var tmp))
				{
					return tmp;
				}
			}
			return null;
		}

		public int GetChallengesCount(int seasonCfgId)
		{
			if (this.SeasonChallenges.TryGetValue(seasonCfgId, out var tmpSeason))
			{
				foreach (var item in tmpSeason)
				{
					return item.Value.Count;
				}
			}
			return 0;
		}

		public List<ChallengeLevelCfg> _GetChallenges_All()
		{
			ListComponent<ChallengeLevelCfg> allList = ListComponent<ChallengeLevelCfg>.Create();
			foreach (var tmp in this.SeasonChallenges)
			{
				foreach (var item in tmp.Value)
				{
					foreach (var item2 in item.Value)
					{
						allList.Add(item2.Value);
					}
				}
			}
			return allList;
		}

		public ChallengeLevelCfg GetChallengeByIndex(int seasonCfgId, int index, PVELevelDifficulty pveLevelDifficulty)
		{
			var dic = this.GetChallenges(seasonCfgId, pveLevelDifficulty);
			if (dic.TryGetValue(index, out var challenge))
			{
				return challenge;
			}
			return null;
		}

		public ChallengeLevelCfg GetChallenge(RoomTypeInfo roomTypeInfo)
		{
			return this.GetChallengeByIndex(roomTypeInfo.seasonCfgId, roomTypeInfo.pveIndex, roomTypeInfo.pveLevelDifficulty);
		}

		public ChallengeLevelCfg GetChallengeByDropItemCfgId(string dropItemCfgId)
		{
			var dic = this._GetChallenges_All();
			foreach (ChallengeLevelCfg challengeLevelCfg in dic)
			{
				if (challengeLevelCfg.FirstRewardItemListShow.ContainsKey(dropItemCfgId))
				{
					return challengeLevelCfg;
				}
			}
			return null;
		}

		public string GetCurChallengeGamePlayBattleLevelCfgId(int seasonCfgId, int pveIndex, PVELevelDifficulty pveLevelDifficulty, bool isAR)
		{
			ChallengeLevelCfg ChallengeLevelCfg = GetChallengeByIndex(seasonCfgId, pveIndex, pveLevelDifficulty);
			if (ChallengeLevelCfg == null)
			{
				return "";
			}
			if (isAR)
			{
				return ChallengeLevelCfg.BattleCfgIdAR;
			}
			else
			{
				return ChallengeLevelCfg.BattleCfgIdNoAR;
			}
		}

		public ChallengeLevelCfg GetNextChallenge(RoomTypeInfo roomTypeInfo)
		{
			(bool isChallenge, bool isAR) = ChkIsChallenge(roomTypeInfo);
			if (isChallenge == false)
			{
				return null;
			}
			int seasonCfgId = roomTypeInfo.seasonCfgId;
			int pveIndex = roomTypeInfo.pveIndex;
			PVELevelDifficulty pveLevelDifficulty = roomTypeInfo.pveLevelDifficulty;
			RoomType roomType = roomTypeInfo.roomType;
			SubRoomType subRoomType = roomTypeInfo.subRoomType;

			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonCfgId, pveIndex + 1, pveLevelDifficulty);

			return nextChallengeLevelCfg;
		}

		public ChallengeLevelCfg GetNextChallenge(int seasonCfgId, int pveIndex, PVELevelDifficulty pveLevelDifficulty)
		{
			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonCfgId, pveIndex + 1, pveLevelDifficulty);
			if (nextChallengeLevelCfg == null)
			{
				return null;
			}
			return nextChallengeLevelCfg;
		}

		public ChallengeLevelCfg GetNextChallenge(ChallengeLevelCfg challengeLevelCfg)
		{
			int seasonCfgId = challengeLevelCfg.SeasonId;
			int pveIndex = challengeLevelCfg.Index;
			PVELevelDifficulty pveLevelDifficulty = challengeLevelCfg.PveLevelDifficulty;
			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonCfgId, pveIndex + 1, pveLevelDifficulty);
			if (nextChallengeLevelCfg == null)
			{
				return null;
			}
			return nextChallengeLevelCfg;
		}

		public (bool, bool) ChkIsChallenge(RoomTypeInfo roomTypeInfo)
		{
			int seasonCfgId = roomTypeInfo.seasonCfgId;
			int pveIndex = roomTypeInfo.pveIndex;
			RoomType roomType = roomTypeInfo.roomType;
			SubRoomType subRoomType = roomTypeInfo.subRoomType;

			bool isChallenge = false;
			bool isAR = false;
			if (seasonCfgId <= 0)
			{
				Log.Error($"ET.AbilityConfig.ChallengeLevelCfgCategory.ChkIsChallenge seasonCfgId <= 0");
				return (false, false);
			}
			if (pveIndex <= 0)
			{
				Log.Error($"ET.AbilityConfig.ChallengeLevelCfgCategory.ChkIsChallenge pveIndex <= 0");
				return (false, false);
			}

			if (roomType == RoomType.Normal)
			{
				if (subRoomType == SubRoomType.NormalPVE)
				{
					isChallenge = true;
				}
			}
			else if (roomType == RoomType.AR)
			{
				if (subRoomType == SubRoomType.ARPVE)
				{
					isChallenge = true;
					isAR = true;
				}
			}

			return (isChallenge, isAR);
		}

		public string GetNextChallengeGamePlayBattleLevelCfgId(RoomTypeInfo roomTypeInfo)
		{
			(bool isChallenge, bool isAR) = ChkIsChallenge(roomTypeInfo);
			if (isChallenge == false)
			{
				return null;
			}
			int seasonCfgId = roomTypeInfo.seasonCfgId;
			int pveIndex = roomTypeInfo.pveIndex;
			PVELevelDifficulty pveLevelDifficulty = roomTypeInfo.pveLevelDifficulty;
			RoomType roomType = roomTypeInfo.roomType;
			SubRoomType subRoomType = roomTypeInfo.subRoomType;

			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonCfgId, pveIndex + 1, pveLevelDifficulty);
			if (nextChallengeLevelCfg == null)
			{
				return "";
			}
			if (isAR)
			{
				return nextChallengeLevelCfg.BattleCfgIdAR;
			}
			else
			{
				return nextChallengeLevelCfg.BattleCfgIdNoAR;
			}
		}

		public string GetNextChallengeGamePlayBattleLevelCfgId(int seasonCfgId, int pveIndex, PVELevelDifficulty pveLevelDifficulty, bool isAR)
		{
			ChallengeLevelCfg nextChallengeLevelCfg = GetNextChallenge(seasonCfgId, pveIndex, pveLevelDifficulty);
			if (nextChallengeLevelCfg == null)
			{
				return "";
			}
			if (isAR)
			{
				return nextChallengeLevelCfg.BattleCfgIdAR;
			}
			else
			{
				return nextChallengeLevelCfg.BattleCfgIdNoAR;
			}
		}

		partial void PostResolve()
		{
			foreach (var challengeCfg in this.DataList)
			{
				int seasonCfgId = challengeCfg.SeasonId;
				if (this.SeasonChallenges.TryGetValue(seasonCfgId, out var seasonChallengeDifficulty) == false)
				{
					seasonChallengeDifficulty = new Dictionary<PVELevelDifficulty, SortedDictionary<int, ChallengeLevelCfg>>();
					this.SeasonChallenges.Add(seasonCfgId, seasonChallengeDifficulty);
				}
				PVELevelDifficulty pveLevelDifficulty = challengeCfg.PveLevelDifficulty;
				if (seasonChallengeDifficulty.TryGetValue(pveLevelDifficulty, out var seasonChallenge) == false)
				{
					seasonChallenge = new SortedDictionary<int, ChallengeLevelCfg>();
					seasonChallengeDifficulty.Add(pveLevelDifficulty, seasonChallenge);
				}
				seasonChallenge.Add(challengeCfg.Index, challengeCfg);
			}
		}

		public bool HasPVEDifficulty(int seasonId, PVELevelDifficulty pveDifficulty)
		{
			if (SeasonChallenges.TryGetValue(seasonId, out var seasonChallenge))
			{
				return seasonChallenge.ContainsKey(pveDifficulty);
			}
			return false;
		}
	}
}
