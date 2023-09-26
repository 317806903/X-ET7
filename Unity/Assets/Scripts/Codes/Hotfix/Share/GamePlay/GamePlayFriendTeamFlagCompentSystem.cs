using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayFriendTeamFlagCompent))]
    public static class GamePlayFriendTeamFlagCompentSystem
	{
		[ObjectSystem]
		public class GamePlayFriendTeamFlagCompentAwakeSystem : AwakeSystem<GamePlayFriendTeamFlagCompent>
		{
			protected override void Awake(GamePlayFriendTeamFlagCompent self)
			{
				self.teamFriendDic = new();
				self.playerId2TeamFlag = new();
				self.unitId2TeamFlag = new();
				self.playerId2Color = new();
			}
		}

		[ObjectSystem]
		public class GamePlayFriendTeamFlagCompentDestroySystem : DestroySystem<GamePlayFriendTeamFlagCompent>
		{
			protected override void Destroy(GamePlayFriendTeamFlagCompent self)
			{
				self.teamFriendDic?.Clear();
				self.playerId2TeamFlag?.Clear();
				self.unitId2TeamFlag?.Clear();
				self.playerId2Color?.Clear();
			}
		}

		public static void InitWhenRoom(this GamePlayFriendTeamFlagCompent self, RoomComponent roomComponent, List<RoomMember> roomMemberList)
		{
			self.InitPlayerTeamFlag(roomComponent, roomMemberList);
		}

		public static void InitWhenGlobal(this GamePlayFriendTeamFlagCompent self)
		{
		}

		public static void AddPlayerWhenGlobal(this GamePlayFriendTeamFlagCompent self, long playerId, int playerTeamId)
		{
			self.InitPlayerTeamFlagOne(playerId, playerTeamId);

			TeamFlagType teamFlagType = self.playerId2TeamFlag[playerId];
			self.unitId2TeamFlag[playerId] = teamFlagType;
		}

		public static void _InitPlayerTeamFlag(this GamePlayFriendTeamFlagCompent self, long playerId, int roomTeamId)
		{
			string key = $"TeamPlayer{roomTeamId + 1}";
			TeamFlagType teamFlagType = EnumHelper.FromString<TeamFlagType>(key);
			self.playerId2TeamFlag[playerId] = teamFlagType;
			self.unitId2TeamFlag[playerId] = teamFlagType;
		}

		public static void _InitPlayerColor(this GamePlayFriendTeamFlagCompent self, long playerId, int index)
		{
			float3 color = self.playerColorList[index];
			self.playerId2Color[playerId] = color;
		}

		/// <summary>
		/// 指定玩家的阵营,例如塔
		/// </summary>
		/// <param name="self"></param>
		/// <param name="playerId"></param>
		/// <param name="unit"></param>
		public static void AddPlayerUnitTeamFlag(this GamePlayFriendTeamFlagCompent self, long playerId, Unit unit)
		{
			TeamFlagType teamFlagType = self.playerId2TeamFlag[playerId];
			self.unitId2TeamFlag.Add(unit.Id, teamFlagType);
		}

		/// <summary>
		/// 直接指定阵营信息,例如 大本营，怪物
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unit"></param>
		/// <param name="teamFlag"></param>
		public static void AddUnitTeamFlag(this GamePlayFriendTeamFlagCompent self, Unit unit, TeamFlagType teamFlag)
		{
			self.unitId2TeamFlag.Add(unit.Id, teamFlag);
		}

		/// <summary>
		/// 有召唤者的设置阵营信息，例如子弹
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unitParent"></param>
		/// <param name="unit"></param>
		public static void AddUnitTeamFlagByParent(this GamePlayFriendTeamFlagCompent self, Unit unitParent, Unit unit)
		{
			TeamFlagType teamFlagType = self.unitId2TeamFlag[unitParent.Id];
			self.unitId2TeamFlag.Add(unit.Id, teamFlagType);
		}

		/// <summary>
		/// 移除 unit 的设置阵营信息
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unit"></param>
		public static void RemoveUnitTeamFlag(this GamePlayFriendTeamFlagCompent self, Unit unit)
		{
			self.unitId2TeamFlag.Remove(unit.Id);
		}

		public static TeamFlagType GetTeamFlagByUnit(this GamePlayFriendTeamFlagCompent self, Unit unit)
		{
			return self.unitId2TeamFlag[unit.Id];
		}

		public static GamePlayComponent GetGamePlay(this GamePlayFriendTeamFlagCompent self)
		{
			GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
			return gamePlayComponent;
		}

		public static GamePlayBattleLevelCfg GetGamePlayBattleConfig(this GamePlayFriendTeamFlagCompent self)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			return gamePlayComponent.GetGamePlayBattleConfig();
		}

		public static void InitPlayerTeamFlag(this GamePlayFriendTeamFlagCompent self, RoomComponent roomComponent, List<RoomMember> roomMemberList)
		{
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
			if (gamePlayBattleLevelCfg.TeamMode is AllPlayersOneGroup allPlayersOneGroup)
			{
				for (int i = 0; i < roomMemberList.Count; i++)
				{
					RoomMember roomMember = roomMemberList[i];
					self._InitPlayerTeamFlag(roomMember.Id, 0);
					self._InitPlayerColor(roomMember.Id, i);
				}
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerAlone playerAlone)
			{
				for (int i = 0; i < roomMemberList.Count; i++)
				{
					RoomMember roomMember = roomMemberList[i];
					self._InitPlayerTeamFlag(roomMember.Id, 0);
					self._InitPlayerColor(roomMember.Id, i);
				}
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerTeam playerTeam)
			{
				for (int i = 0; i < roomMemberList.Count; i++)
				{
					RoomMember roomMember = roomMemberList[i];
					self._InitPlayerTeamFlag(roomMember.Id, (int)roomMember.roomTeamId);
					self._InitPlayerColor(roomMember.Id, i);
				}
			}
		}

		public static void InitPlayerTeamFlagOne(this GamePlayFriendTeamFlagCompent self, long playerId, int playerTeamId)
		{
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
			if (gamePlayBattleLevelCfg.TeamMode is AllPlayersOneGroup allPlayersOneGroup)
			{
				self._InitPlayerTeamFlag(playerId, 0);
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerAlone playerAlone)
			{
				self._InitPlayerTeamFlag(playerId, 0);
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerTeam playerTeam)
			{
				self._InitPlayerTeamFlag(playerId, playerTeamId);
			}
			self._InitPlayerColor(playerId, 0);
		}

		public static long GetPlayerIdByUnitId(this GamePlayFriendTeamFlagCompent self, long unitId)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			return gamePlayComponent.GetPlayerIdByUnitId(unitId);
		}

		public static bool ChkIsFriend(this GamePlayFriendTeamFlagCompent self, Unit curUnit, Unit targetUnit)
		{
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
			if (gamePlayBattleLevelCfg.TeamMode is AllPlayersOneGroup allPlayersOneGroup)
			{
				return self._ChkIsFriend(self.GetTeamFlagByUnit(curUnit), self.GetTeamFlagByUnit(targetUnit));
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerAlone playerAlone)
			{
				long curPlayerId = self.GetPlayerIdByUnitId(curUnit.Id);
				long targetPlayerId = self.GetPlayerIdByUnitId(targetUnit.Id);
				if (curPlayerId != -1 && targetPlayerId != -1)
				{
					if (curPlayerId == targetPlayerId)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				return self._ChkIsFriend(self.GetTeamFlagByUnit(curUnit), self.GetTeamFlagByUnit(targetUnit));
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerTeam playerTeam)
			{
				return self._ChkIsFriend(self.GetTeamFlagByUnit(curUnit), self.GetTeamFlagByUnit(targetUnit));
			}

			return false;
		}

		public static float3 GetPlayerColor(this GamePlayFriendTeamFlagCompent self, long playerId)
		{
			return self.playerId2Color[playerId];
		}

		public static void DealFriendTeamFlag(this GamePlayFriendTeamFlagCompent self, List<TeamFlagType> teamFlagTypes, bool isWithPlayers, bool reset)
		{
			int newFriendTypes = 0;
			for (int i = 0; i < teamFlagTypes.Count; i++)
			{
				newFriendTypes |= (int)teamFlagTypes[i];
			}
			if (isWithPlayers)
			{
				foreach (var playerId2TeamFlag in self.playerId2TeamFlag)
				{
					newFriendTypes |= (int)playerId2TeamFlag.Value;
					teamFlagTypes.Add(playerId2TeamFlag.Value);
				}
			}
			for (int i = 0; i < teamFlagTypes.Count; i++)
			{
				TeamFlagType teamFlagType = teamFlagTypes[i];
				if (reset == false && self.teamFriendDic.TryGetValue(teamFlagType, out int friendType))
				{
					self.teamFriendDic[teamFlagType] = friendType | newFriendTypes;
				}
				else
				{
					self.teamFriendDic[teamFlagType] = newFriendTypes;
				}
			}
		}

		public static bool _ChkIsFriend(this GamePlayFriendTeamFlagCompent self, TeamFlagType curTeamFlagType, TeamFlagType targetTeamFlagType)
		{
			if (curTeamFlagType == targetTeamFlagType)
			{
				return true;
			}
			if (self.teamFriendDic.ContainsKey(curTeamFlagType) == false)
			{
				return false;
			}
			if (self.teamFriendDic.ContainsKey(targetTeamFlagType) == false)
			{
				return false;
			}
			bool isFriend = (self.teamFriendDic[curTeamFlagType] & (int)targetTeamFlagType) > 0;
			return isFriend;
		}

		public static void StopAllAI(this GamePlayFriendTeamFlagCompent self)
		{
			foreach (long unitId in self.unitId2TeamFlag.Keys)
			{
				Unit unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
				unit?.RemoveComponent<AIComponent>();
			}
		}

	}
}