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

		[ObjectSystem]
		public class GamePlayFriendTeamFlagCompentFixedUpdateSystem: FixedUpdateSystem<GamePlayFriendTeamFlagCompent>
		{
			protected override void FixedUpdate(GamePlayFriendTeamFlagCompent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this GamePlayFriendTeamFlagCompent self, float fixedDeltaTime)
		{
			if (++self.curFrameChk >= self.waitFrameChk)
			{
				self.curFrameChk = 0;

				self.DealCurUnit2TargetUnitIsFriend();
			}
		}

		public static void DealCurUnit2TargetUnitIsFriend(this GamePlayFriendTeamFlagCompent self)
		{
			if (self.curUnit2TargetUnitIsFriend.Count == 0)
			{
				return;
			}

			self.RemoveList.Clear();
			foreach (var item in self.curUnit2TargetUnitIsFriend)
			{
				long unitId = item.Key;
				if (UnitHelper.ChkUnitAlive(self.DomainScene(), unitId) == false)
				{
					self.RemoveList.Add(item.Key);
				}
			}

			foreach (long unitId in self.RemoveList)
			{
				self.curUnit2TargetUnitIsFriend.Remove(unitId);
			}
			self.RemoveList.Clear();
		}

		public static void InitWhenRoom(this GamePlayFriendTeamFlagCompent self, RoomComponent roomComponent, List<RoomMember> roomMemberList)
		{
			self.InitPlayerTeamFlagWhenRoom(roomComponent, roomMemberList);
		}

		public static void InitWhenGlobal(this GamePlayFriendTeamFlagCompent self)
		{
		}

		public static void AddPlayerWhenGlobal(this GamePlayFriendTeamFlagCompent self, long playerId, int playerTeamId)
		{
			self.InitPlayerTeamFlagOneWhenGlobal(playerId, playerTeamId);
		}

		/// <summary>
		/// 设置player的阵营标志
		/// </summary>
		/// <param name="self"></param>
		/// <param name="playerId"></param>
		/// <param name="roomTeamId"></param>
		public static void _InitPlayerTeamFlag(this GamePlayFriendTeamFlagCompent self, long playerId, int roomTeamId)
		{
			string key = $"TeamPlayer{roomTeamId + 1}";
			TeamFlagType teamFlagType = EnumHelper.FromString<TeamFlagType>(key);
			self.playerId2TeamFlag[playerId] = teamFlagType;
		}

		/// <summary>
		/// 玩家对应颜色
		/// </summary>
		/// <param name="self"></param>
		/// <param name="playerId"></param>
		/// <param name="index"></param>
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
			TeamFlagHelper.AddTeamFlag(playerId, unit, teamFlagType);
		}

		/// <summary>
		/// 直接指定阵营信息,例如 大本营，怪物
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unit"></param>
		/// <param name="teamFlagType"></param>
		public static void AddUnitTeamFlag(this GamePlayFriendTeamFlagCompent self, long playerId, Unit unit, TeamFlagType teamFlagType)
		{
			self.unitId2TeamFlag.Add(unit.Id, teamFlagType);
			TeamFlagHelper.AddTeamFlag(playerId, unit, teamFlagType);
		}

		/// <summary>
		/// 有召唤者的设置阵营信息，例如子弹
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unitParent"></param>
		/// <param name="unit"></param>
		public static void AddUnitTeamFlagByParent(this GamePlayFriendTeamFlagCompent self, long playerId, Unit unitParent, Unit unit)
		{
			TeamFlagType teamFlagType = TeamFlagHelper.GetTeamFlag(unitParent);
			self.unitId2TeamFlag.Add(unit.Id, teamFlagType);

			TeamFlagHelper.AddTeamFlag(playerId, unit, teamFlagType);
		}

		/// <summary>
		/// 移除 unit 的设置阵营信息
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unit"></param>
		public static void RemoveUnitTeamFlag(this GamePlayFriendTeamFlagCompent self, long unitId)
		{
			if (self.unitId2TeamFlag.TryGetValue(unitId, out var teamFlagType))
			{
				self.unitId2TeamFlag.Remove(unitId);
			}
		}

		public static TeamFlagType GetTeamFlagByPlayerId(this GamePlayFriendTeamFlagCompent self, long playerId)
		{
			return self.playerId2TeamFlag[playerId];
		}

		public static Dictionary<long, TeamFlagType> GetAllPlayerTeamFlag(this GamePlayFriendTeamFlagCompent self)
		{
			return self.playerId2TeamFlag;
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

		public static void InitPlayerTeamFlagWhenRoom(this GamePlayFriendTeamFlagCompent self, RoomComponent roomComponent, List<RoomMember> roomMemberList)
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

		public static void InitPlayerTeamFlagOneWhenGlobal(this GamePlayFriendTeamFlagCompent self, long playerId, int playerTeamId)
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

			int colorIndex = RandomGenerator.RandomNumber(0, self.playerColorList.Count);
			self._InitPlayerColor(playerId, colorIndex);
		}

		/// <summary>
		/// 判断阵营
		/// </summary>
		/// <param name="self"></param>
		/// <param name="curUnitId"></param>
		/// <param name="targetUnitId"></param>
		/// <returns></returns>
		public static bool ChkIsFriend(this GamePlayFriendTeamFlagCompent self, long curUnitId, long targetUnitId, bool needSamePlayer)
		{
			Unit curUnit = UnitHelper.GetUnit(self.DomainScene(), curUnitId);
			Unit targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
			if (needSamePlayer)
			{
				long curPlayerId = TeamFlagHelper.GetPlayerId(curUnit);
				long targetPlayerId = TeamFlagHelper.GetPlayerId(targetUnit);
				if (curPlayerId == (long)ET.PlayerId.PlayerNone || targetPlayerId == (long)ET.PlayerId.PlayerNone)
				{
					return false;
				}
				if (curPlayerId == targetPlayerId)
				{
					return true;
				}
				return false;
			}

			if (self.curUnit2TargetUnitIsFriend.TryGetValue(curUnitId, targetUnitId, out bool isFriend))
			{
				return isFriend;
			}

			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
			if (gamePlayBattleLevelCfg.TeamMode is AllPlayersOneGroup allPlayersOneGroup)
			{
				isFriend = self._ChkIsFriend(TeamFlagHelper.GetTeamFlag(curUnit), TeamFlagHelper.GetTeamFlag(targetUnit));
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerAlone playerAlone)
			{
				long curPlayerId = TeamFlagHelper.GetPlayerId(curUnit);
				long targetPlayerId = TeamFlagHelper.GetPlayerId(targetUnit);
				if (curPlayerId != (long)ET.PlayerId.PlayerNone && targetPlayerId != (long)ET.PlayerId.PlayerNone)
				{
					if (curPlayerId == targetPlayerId)
					{
						isFriend = true;
					}
					else
					{
						isFriend = false;
					}
					self.curUnit2TargetUnitIsFriend.Add(curUnitId, targetUnitId, isFriend);
					return isFriend;
				}
				isFriend = self._ChkIsFriend(TeamFlagHelper.GetTeamFlag(curUnit), TeamFlagHelper.GetTeamFlag(targetUnit));
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerTeam playerTeam)
			{
				isFriend = self._ChkIsFriend(TeamFlagHelper.GetTeamFlag(curUnit), TeamFlagHelper.GetTeamFlag(targetUnit));
			}
			else
			{
				isFriend = false;
			}

			self.curUnit2TargetUnitIsFriend.Add(curUnitId, targetUnitId, isFriend);
			return isFriend;
		}

		public static float3 GetPlayerColor(this GamePlayFriendTeamFlagCompent self, long playerId)
		{
			if (self == null)
			{
				return new float3(0.2f, 0.46f, 1f);
			}
			if (self.playerId2Color.TryGetValue(playerId, out float3 color))
			{
				return color;
			}

			return self.playerColorList[0];
		}

		/// <summary>
		/// 处理阵营关系
		/// </summary>
		/// <param name="self"></param>
		/// <param name="teamFlagTypes"></param>
		/// <param name="isWithPlayers"></param>
		/// <param name="reset"></param>
		public static void DealFriendTeamFlag(this GamePlayFriendTeamFlagCompent self, List<TeamFlagType> teamFlagTypes, bool isWithPlayers, bool reset)
		{
			if (reset)
			{
				self.curUnit2TargetUnitIsFriend.Clear();
			}

			if (teamFlagTypes == null)
			{
				teamFlagTypes = new();
			}

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
				if (unit != null)
				{
					unit.RemoveComponent<AIComponent>();
					ET.Ability.MoveOrIdleHelper.DoIdle(unit).Coroutine();
				}
			}
		}

		public static void PauseAllAI(this GamePlayFriendTeamFlagCompent self)
		{
			foreach (long unitId in self.unitId2TeamFlag.Keys)
			{
				Unit unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
				if (unit != null)
				{
					AIComponent aiComponent = unit.GetComponent<AIComponent>();
					if (aiComponent != null)
					{
						aiComponent.PauseAI();
						ET.Ability.MoveOrIdleHelper.DoIdle(unit).Coroutine();
					}
				}
			}
		}

		public static void RecoveryAllAI(this GamePlayFriendTeamFlagCompent self)
		{
			foreach (long unitId in self.unitId2TeamFlag.Keys)
			{
				Unit unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
				if (unit != null)
				{
					AIComponent aiComponent = unit.GetComponent<AIComponent>();
					if (aiComponent != null)
					{
						aiComponent.RecoveryAI();
					}
				}
			}
		}

	}
}