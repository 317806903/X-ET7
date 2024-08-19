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

		public SortedDictionary<int, ChallengeLevelCfg> GetChallenges(int seasonCfgId)
		{
			if (this.SeasonChallenges.TryGetValue(seasonCfgId, out var tmp))
			{
				return tmp;
			}
			return null;
		}

		public ChallengeLevelCfg GetChallengeByIndex(int seasonCfgId, int index)
		{
			var dic = this.GetChallenges(seasonCfgId);
			if (dic.TryGetValue(index, out var challenge))
			{
				return challenge;
			}
			return null;
		}

		public ChallengeLevelCfg GetChallenge(RoomTypeInfo roomTypeInfo)
		{
			return this.GetChallengeByIndex(roomTypeInfo.seasonCfgId, roomTypeInfo.pveIndex);
		}

		public string GetCurChallengeGamePlayBattleLevelCfgId(int seasonCfgId, int pveIndex, bool isAR)
		{
			ChallengeLevelCfg ChallengeLevelCfg = GetChallengeByIndex(seasonCfgId, pveIndex);
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
			RoomType roomType = roomTypeInfo.roomType;
			SubRoomType subRoomType = roomTypeInfo.subRoomType;

			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonCfgId, pveIndex + 1);

			return nextChallengeLevelCfg;
		}

		public ChallengeLevelCfg GetNextChallenge(int seasonCfgId, int pveIndex)
		{
			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonCfgId, pveIndex + 1);
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
			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonCfgId, pveIndex + 1);
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
			RoomType roomType = roomTypeInfo.roomType;
			SubRoomType subRoomType = roomTypeInfo.subRoomType;

			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(seasonCfgId, pveIndex + 1);
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

		public string GetNextChallengeGamePlayBattleLevelCfgId(int seasonCfgId, int pveIndex, bool isAR)
		{
			ChallengeLevelCfg nextChallengeLevelCfg = GetNextChallenge(seasonCfgId, pveIndex);
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
				if (this.SeasonChallenges.TryGetValue(seasonCfgId, out var seasonChallenge) == false)
				{
					seasonChallenge = new SortedDictionary<int, ChallengeLevelCfg>();
					this.SeasonChallenges.Add(seasonCfgId, seasonChallenge);
				}
				seasonChallenge.Add(challengeCfg.Index, challengeCfg);
			}
		}
	}
}
