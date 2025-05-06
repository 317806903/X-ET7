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
		public Dictionary<PVELevelDifficulty, SortedDictionary<int, ChallengeLevelCfg>> NormalChallenges = new();

		public SortedDictionary<int, ChallengeLevelCfg> GetChallenges(PVELevelDifficulty pveLevelDifficulty)
		{
			if (this.NormalChallenges.TryGetValue(pveLevelDifficulty, out var tmp))
			{
				return tmp;
			}
			return null;
		}

		public int GetChallengesCount()
		{
			foreach (var item in this.NormalChallenges)
			{
				return item.Value.Count;
			}
			return 0;
		}

		public List<ChallengeLevelCfg> _GetChallenges_All()
		{
			ListComponent<ChallengeLevelCfg> allList = ListComponent<ChallengeLevelCfg>.Create();
			foreach (var item in this.NormalChallenges)
			{
				foreach (var item2 in item.Value)
				{
					allList.Add(item2.Value);
				}
			}
			return allList;
		}

		public ChallengeLevelCfg GetChallengeByIndex(int index, PVELevelDifficulty pveLevelDifficulty)
		{
			var dic = this.GetChallenges(pveLevelDifficulty);
			if (dic.TryGetValue(index, out var challenge))
			{
				return challenge;
			}
			return null;
		}

		public ChallengeLevelCfg GetChallenge(RoomTypeInfo roomTypeInfo)
		{
			return this.GetChallengeByIndex(roomTypeInfo.pveIndex, roomTypeInfo.pveLevelDifficulty);
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

		public string GetCurChallengeGamePlayBattleLevelCfgId(int pveIndex, PVELevelDifficulty pveLevelDifficulty, bool isAR)
		{
			ChallengeLevelCfg ChallengeLevelCfg = GetChallengeByIndex(pveIndex, pveLevelDifficulty);
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
			int pveIndex = roomTypeInfo.pveIndex;
			PVELevelDifficulty pveLevelDifficulty = roomTypeInfo.pveLevelDifficulty;

			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(pveIndex + 1, pveLevelDifficulty);
			if (nextChallengeLevelCfg == null)
			{
				return null;
			}
			return nextChallengeLevelCfg;
		}

		public ChallengeLevelCfg GetNextChallenge(int pveIndex, PVELevelDifficulty pveLevelDifficulty)
		{
			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(pveIndex + 1, pveLevelDifficulty);
			if (nextChallengeLevelCfg == null)
			{
				return null;
			}
			return nextChallengeLevelCfg;
		}

		public ChallengeLevelCfg GetNextChallenge(ChallengeLevelCfg challengeLevelCfg)
		{
			int pveIndex = challengeLevelCfg.Index;
			PVELevelDifficulty pveLevelDifficulty = challengeLevelCfg.PveLevelDifficulty;
			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(pveIndex + 1, pveLevelDifficulty);
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
			if (seasonCfgId > 0)
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

			ChallengeLevelCfg nextChallengeLevelCfg = this.GetNextChallenge(roomTypeInfo);
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

		public string GetNextChallengeGamePlayBattleLevelCfgId(int pveIndex, PVELevelDifficulty pveLevelDifficulty, bool isAR)
		{
			ChallengeLevelCfg nextChallengeLevelCfg = GetNextChallenge(pveIndex, pveLevelDifficulty);
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
				PVELevelDifficulty pveLevelDifficulty = challengeCfg.PveLevelDifficulty;
				if (this.NormalChallenges.TryGetValue(pveLevelDifficulty, out var normalChallengeDifficulty) == false)
				{
					normalChallengeDifficulty = new SortedDictionary<int, ChallengeLevelCfg>();
					this.NormalChallenges.Add(pveLevelDifficulty, normalChallengeDifficulty);
				}
				normalChallengeDifficulty.Add(challengeCfg.Index, challengeCfg);
			}
		}

		public bool HasPVEDiffculty(PVELevelDifficulty pvelevelDifficulty)
		{
			return NormalChallenges.ContainsKey(pvelevelDifficulty);
		}
	}
}
