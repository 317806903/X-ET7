using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayPlayerListComponent))]
    public static class GamePlayPlayerListComponentSystem
	{
		[ObjectSystem]
		public class GamePlayPlayerListComponentAwakeSystem : AwakeSystem<GamePlayPlayerListComponent>
		{
			protected override void Awake(GamePlayPlayerListComponent self)
			{
				self.playerId2UnitIds = new();
				self.unitId2PlayerId = new();
				self.playerId2IsQuit = new();
				self.playerId2BirthPos = new();
				self.playerId2CoinList = new();
			}
		}
	
		[ObjectSystem]
		public class GamePlayPlayerListComponentDestroySystem : DestroySystem<GamePlayPlayerListComponent>
		{
			protected override void Destroy(GamePlayPlayerListComponent self)
			{
				self.playerId2UnitIds?.Clear();
				self.unitId2PlayerId?.Clear();
				self.playerId2IsQuit?.Clear();
				self.playerId2BirthPos?.Clear();
				self.playerId2CoinList?.Clear();
			}
		}

		public static void InitWhenRoom(this GamePlayPlayerListComponent self, RoomComponent roomComponent, List<RoomMember> roomMemberList)
		{
			for (int i = 0; i < roomMemberList.Count; i++)
			{
				RoomMember roomMember = roomMemberList[i];
				self.playerId2IsQuit[roomMember.Id] = false;
			}

			self.InitPlayerBirthPosWhenRoom(roomComponent, roomMemberList);
		}

		public static void InitWhenGlobal(this GamePlayPlayerListComponent self)
		{
		}
		
		public static void AddPlayerWhenGlobal(this GamePlayPlayerListComponent self, long playerId, int playerTeamId)
		{
			self.playerId2IsQuit[playerId] = false;
			self.InitPlayerBirthPosWhenGlobal(playerId, playerTeamId);
		}

		public static void PlayerQuitBattle(this GamePlayPlayerListComponent self, long playerId, bool isNeedRemoveAllPlayerUnits)
		{
			if (self.playerId2IsQuit[playerId])
			{
				return;
			}
			self.playerId2IsQuit[playerId] = true;

			Unit playerUnit = UnitHelper.GetUnit(self.DomainScene(), playerId);
			self._InitPlayerBirthPos(playerId, playerUnit.Position, true);

			if (isNeedRemoveAllPlayerUnits)
			{
				foreach (var unitId in self.playerId2UnitIds[playerId])
				{
					Unit unit = UnitHelper.GetUnit(self.DomainScene(), unitId);
					unit?.Dispose();
				}
			}
		}
		
		/// <summary>
		/// 当unit创建时， 存储unitId 和 playerId 的关系
		/// </summary>
		/// <param name="self"></param>
		/// <param name="playerId"></param>
		/// <param name="unitId"></param>
		public static void AddUnitInfo(this GamePlayPlayerListComponent self, long playerId, long unitId)
		{
			if (self.unitId2PlayerId.ContainsKey(unitId) == false)
			{
				self.playerId2UnitIds.Add(playerId, unitId);
				self.unitId2PlayerId.Add(unitId, playerId);
			}
		}
		
		/// <summary>
		/// 当unit销毁时， 去掉已存储的unitId 和 playerId 的关系
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unitId"></param>
		public static void RemoveUnitInfo(this GamePlayPlayerListComponent self, long unitId)
		{
			if (self.unitId2PlayerId.ContainsKey(unitId))
			{
				long playerId = self.unitId2PlayerId[unitId];
				self.playerId2UnitIds.Remove(playerId, unitId);
			}
		}

		/// <summary>
		/// 通过这个判断 unitId 是否归属 player, 返回-1则不是玩家的
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unitId"></param>
		/// <returns></returns>
		public static long GetPlayerIdByUnitId(this GamePlayPlayerListComponent self, long unitId)
		{
			if (self.unitId2PlayerId.ContainsKey(unitId) == false)
			{
				return -1;
			}
			long playerId = self.unitId2PlayerId[unitId];
			return playerId;
		}

		/// <summary>
		/// 获取未退出战斗的 玩家列表
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static List<long> GetPlayerList(this GamePlayPlayerListComponent self)
		{
			ListComponent<long> playerList = ListComponent<long>.Create();
			foreach (var playerId2IsQuit in self.playerId2IsQuit)
			{
				long playerId = playerId2IsQuit.Key;
				bool isQuit = playerId2IsQuit.Value;
				if (isQuit == false)
				{
					playerList.Add(playerId);
				}
			}
			return playerList;
		}

		public static GamePlayBattleLevelCfg GetGamePlayBattleConfig(this GamePlayPlayerListComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
			return gamePlayComponent.GetGamePlayBattleConfig();
		}

		public static void InitPlayerBirthPosWhenRoom(this GamePlayPlayerListComponent self, RoomComponent roomComponent, List<RoomMember> roomMemberList)
		{
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
			List<List<System.Numerics.Vector3>> allPlayerBirthPosList = gamePlayBattleLevelCfg.PlayerBirthPosList;
			if (gamePlayBattleLevelCfg.TeamMode is AllPlayersOneGroup allPlayersOneGroup)
			{
				List<System.Numerics.Vector3> playerBirthList = RandomGenerator.RandomArray(allPlayerBirthPosList);
				for (int i = 0; i < roomMemberList.Count; i++)
				{
					RoomMember roomMember = roomMemberList[i];
					self._InitPlayerBirthPos(roomMember.Id, RandomGenerator.RandomArray(playerBirthList));
				}
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerAlone playerAlone)
			{
				if (gamePlayBattleLevelCfg.IsGlobalMode)
				{
					for (int i = 0; i < roomMemberList.Count; i++)
					{
						List<System.Numerics.Vector3> playerBirthList = RandomGenerator.RandomArray(allPlayerBirthPosList);
						RoomMember roomMember = roomMemberList[i];
						self._InitPlayerBirthPos(roomMember.Id, RandomGenerator.RandomArray(playerBirthList));
					}
				}
				else
				{
					if (allPlayerBirthPosList.Count <= roomMemberList.Count - 1)
					{
						Log.Error($"ET.GamePlayPlayerListComponentSystem.InitPlayerBirthPos allPlayerBirthPosList.Count[{allPlayerBirthPosList.Count}] <= roomMemberList.Count[{roomMemberList.Count}] - 1");
					}
					for (int i = 0; i < roomMemberList.Count; i++)
					{
						RoomMember roomMember = roomMemberList[i];
						List<System.Numerics.Vector3> playerBirthList = allPlayerBirthPosList[i];
						self._InitPlayerBirthPos(roomMember.Id, RandomGenerator.RandomArray(playerBirthList));
					}
				}
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerTeam playerTeam)
			{
				Dictionary<int, int> teamId2BirthIndex = new();
				for (int i = 0; i < roomMemberList.Count; i++)
				{
					RoomMember roomMember = roomMemberList[i];
					if (teamId2BirthIndex.ContainsKey((int)roomMember.roomTeamId) == false)
					{
						teamId2BirthIndex[(int)roomMember.roomTeamId] = RandomGenerator.RandomNumber(0, allPlayerBirthPosList.Count);
					}
				}
				for (int i = 0; i < roomMemberList.Count; i++)
				{
					RoomMember roomMember = roomMemberList[i];
					List<System.Numerics.Vector3> playerBirthList = allPlayerBirthPosList[teamId2BirthIndex[(int)roomMember.roomTeamId]];
					self._InitPlayerBirthPos(roomMember.Id, RandomGenerator.RandomArray(playerBirthList));
				}
			}
		}

		public static void _InitPlayerBirthPos(this GamePlayPlayerListComponent self, long playerId, System.Numerics.Vector3 pos, bool forceSet = 
		false)
		{
			self._InitPlayerBirthPos(playerId, new float3(pos.X, pos.Y, pos.Z), forceSet);
		}
		
		public static void _InitPlayerBirthPos(this GamePlayPlayerListComponent self, long playerId, float3 pos, bool forceSet = 
		false)
		{
			if (forceSet == false && self.playerId2BirthPos.ContainsKey(playerId))
			{
				return;
			}
			self.playerId2BirthPos[playerId] = pos;
		}
		
		public static float3 GetPlayerBirthPos(this GamePlayPlayerListComponent self, long playerId)
		{
			return self.playerId2BirthPos[playerId];
		}

		public static void InitPlayerBirthPosWhenGlobal(this GamePlayPlayerListComponent self, long playerId, int playerTeamId)
		{
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
			List<List<System.Numerics.Vector3>> allPlayerBirthPosList = gamePlayBattleLevelCfg.PlayerBirthPosList;
			if (gamePlayBattleLevelCfg.TeamMode is AllPlayersOneGroup allPlayersOneGroup)
			{
				List<System.Numerics.Vector3> playerBirthList = RandomGenerator.RandomArray(allPlayerBirthPosList);
				self._InitPlayerBirthPos(playerId, RandomGenerator.RandomArray(playerBirthList));
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerAlone playerAlone)
			{
				List<System.Numerics.Vector3> playerBirthList = RandomGenerator.RandomArray(allPlayerBirthPosList);
				self._InitPlayerBirthPos(playerId, RandomGenerator.RandomArray(playerBirthList));
			}
			else if (gamePlayBattleLevelCfg.TeamMode is PlayerTeam playerTeam)
			{
				List<System.Numerics.Vector3> playerBirthList = allPlayerBirthPosList[playerTeamId];
				self._InitPlayerBirthPos(playerId, RandomGenerator.RandomArray(playerBirthList));
			}
		}

		public static void SetPlayerCoin(this GamePlayPlayerListComponent self, long playerId, CoinType coinType, int setValue)
		{
			setValue = math.max(0, setValue);
			self.playerId2CoinList.Add(playerId, coinType.ToString(), setValue);
			
			self.NoticeCoinToClient(playerId);
		}
		
		public static void ChgPlayerCoin(this GamePlayPlayerListComponent self, long playerId, CoinType coinType, int chgValue)
		{
			self.playerId2CoinList.TryGetValue(playerId, coinType.ToString(), out int curValue);
			curValue = math.max(0, curValue + chgValue);
			self.playerId2CoinList.Add(playerId, coinType.ToString(), curValue);

			self.NoticeCoinToClient(playerId);
		}
		
		public static int GetPlayerCoin(this GamePlayPlayerListComponent self, long playerId, CoinType coinType)
		{
			self.playerId2CoinList.TryGetValue(playerId, coinType.ToString(), out int curValue);
			return curValue;
		}
		
		public static void NoticeCoinToClient(this GamePlayPlayerListComponent self, long playerId)
		{
			EventType.NoticeGamePlayPlayerListToClient _NoticeGamePlayPlayerListToClient = new ()
			{
				playerId = playerId,
				gamePlayPlayerListComponent = self,
			};
			EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayPlayerListToClient);
		}
	}
}