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
		public Dictionary<int, SortedDictionary<int, ChallengeLevelCfg>> SeasonChallenges = new();

		public SortedDictionary<int, ChallengeLevelCfg> GetChallenges(int seasonId)
		{
			if (this.SeasonChallenges.TryGetValue(seasonId, out var tmp))
			{
				return tmp;
			}
			return null;
		}

		public ChallengeLevelCfg GetChallengeByIndex(int seasonId, int index)
		{
			var dic = this.GetChallenges(seasonId);
			if (dic.TryGetValue(index, out var challenge))
			{
				return challenge;
			}
			return null;
		}

		public ChallengeLevelCfg GetChallenge(RoomTypeInfo roomTypeInfo)
		{
			return this.GetChallengeByIndex(roomTypeInfo.seasonId, roomTypeInfo.pveIndex);
		}

		public string GetCurChallengeGamePlayBattleLevelCfgId(int seasonId, int pveIndex, bool isAR)
		{
			ChallengeLevelCfg ChallengeLevelCfg = GetChallengeByIndex(seasonId, pveIndex);
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
			int seasonId = roomTypeInfo.seasonId;
			int pveIndex = roomTypeInfo.pveIndex;
			RoomType roomType = roomTypeInfo.roomType;
			SubRoomType subRoomType = roomTypeInfo.subRoomType;

			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonId, pveIndex + 1);

			return nextChallengeLevelCfg;
		}

		public ChallengeLevelCfg GetNextChallenge(int seasonId, int pveIndex)
		{
			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonId, pveIndex + 1);
			if (nextChallengeLevelCfg == null)
			{
				return null;
			}
			return nextChallengeLevelCfg;
		}

		public ChallengeLevelCfg GetNextChallenge(ChallengeLevelCfg challengeLevelCfg)
		{
			int seasonId = challengeLevelCfg.SeasonId;
			int pveIndex = challengeLevelCfg.Index;
			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonId, pveIndex + 1);
			if (nextChallengeLevelCfg == null)
			{
				return null;
			}
			return nextChallengeLevelCfg;
		}

		public (bool, bool) ChkIsChallenge(RoomTypeInfo roomTypeInfo)
		{
			int seasonId = roomTypeInfo.seasonId;
			int pveIndex = roomTypeInfo.pveIndex;
			RoomType roomType = roomTypeInfo.roomType;
			SubRoomType subRoomType = roomTypeInfo.subRoomType;

			bool isChallenge = false;
			bool isAR = false;
			if (seasonId <= 0)
			{
				Log.Error($"ET.AbilityConfig.ChallengeLevelCfgCategory.ChkIsChallenge seasonId <= 0");
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
			int seasonId = roomTypeInfo.seasonId;
			int pveIndex = roomTypeInfo.pveIndex;
			RoomType roomType = roomTypeInfo.roomType;
			SubRoomType subRoomType = roomTypeInfo.subRoomType;

			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonId, pveIndex + 1);
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

		public string GetNextChallengeGamePlayBattleLevelCfgId(int seasonId, int pveIndex, bool isAR)
		{
			ChallengeLevelCfg nextChallengeLevelCfg = GetNextChallenge(seasonId, pveIndex);
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
				int seasonId = challengeCfg.SeasonId;
				if (this.SeasonChallenges.TryGetValue(seasonId, out var seasonChallenge) == false)
				{
					seasonChallenge = new SortedDictionary<int, ChallengeLevelCfg>();
					this.SeasonChallenges.Add(seasonId, seasonChallenge);
				}
				seasonChallenge.Add(challengeCfg.Index, challengeCfg);
			}
		}
	}
}
