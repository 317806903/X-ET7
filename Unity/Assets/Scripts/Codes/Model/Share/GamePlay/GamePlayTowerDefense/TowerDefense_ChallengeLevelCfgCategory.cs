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
		public SortedDictionary<int, ChallengeLevelCfg> NormalChallenges = new();

		public SortedDictionary<int, ChallengeLevelCfg> GetChallenges()
		{
			return this.NormalChallenges;
		}

		public ChallengeLevelCfg GetChallengeByIndex(int index)
		{
			var dic = this.GetChallenges();
			if (dic.TryGetValue(index, out var challenge))
			{
				return challenge;
			}
			return null;
		}

		public ChallengeLevelCfg GetChallenge(RoomTypeInfo roomTypeInfo)
		{
			return this.GetChallengeByIndex(roomTypeInfo.pveIndex);
		}

		public string GetCurChallengeGamePlayBattleLevelCfgId(int pveIndex, bool isAR)
		{
			ChallengeLevelCfg ChallengeLevelCfg = GetChallengeByIndex(pveIndex);
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

			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(pveIndex + 1);
			if (nextChallengeLevelCfg == null)
			{
				return null;
			}
			return nextChallengeLevelCfg;
		}

		public ChallengeLevelCfg GetNextChallenge(int pveIndex)
		{
			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(pveIndex + 1);
			if (nextChallengeLevelCfg == null)
			{
				return null;
			}
			return nextChallengeLevelCfg;
		}

		public ChallengeLevelCfg GetNextChallenge(ChallengeLevelCfg challengeLevelCfg)
		{
			int pveIndex = challengeLevelCfg.Index;
			ChallengeLevelCfg nextChallengeLevelCfg = this.GetChallengeByIndex(pveIndex + 1);
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
			if (seasonId > 0)
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

			ChallengeLevelCfg nextChallengeLevelCfg = this.GetNextChallenge( roomTypeInfo);
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

		public string GetNextChallengeGamePlayBattleLevelCfgId(int pveIndex, bool isAR)
		{
			ChallengeLevelCfg nextChallengeLevelCfg = GetNextChallenge(pveIndex);
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
				this.NormalChallenges.Add(challengeCfg.Index, challengeCfg);
			}
		}
	}
}
