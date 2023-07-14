using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayComponent))]
    public static class GamePlayComponentSystem
	{
		[ObjectSystem]
		public class GamePlayComponentAwakeSystem : AwakeSystem<GamePlayComponent>
		{
			protected override void Awake(GamePlayComponent self)
			{
			}
		}
	
		[ObjectSystem]
		public class GamePlayComponentDestroySystem : DestroySystem<GamePlayComponent>
		{
			protected override void Destroy(GamePlayComponent self)
			{
			}
		}

		public static void InitWhenRoom(this GamePlayComponent self, long dynamicMapInstanceId, string gamePlayBattleLevelCfgId, RoomComponent roomComponent, 
		List<RoomMember> roomMemberList)
		{
			self.dynamicMapInstanceId = dynamicMapInstanceId;
			self.gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId;
			self.roomId = roomComponent.Id;
			self.ownerPlayerId = roomComponent.ownerRoomMemberId;


			GamePlayPlayerListComponent gamePlayPlayerListComponent = self.AddComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.InitWhenRoom(roomComponent, roomMemberList);
            
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.AddComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.InitWhenRoom(roomComponent, roomMemberList);

			self.CreateGamePlayMode();
			
			self.gamePlayStatus = GamePlayStatus.WaitForStart;
			
			self.NoticeToClientAll();
		}

		public static void InitWhenGlobal(this GamePlayComponent self, long dynamicMapInstanceId, string gamePlayBattleLevelCfgId)
		{
			self.dynamicMapInstanceId = dynamicMapInstanceId;
			self.gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId;
			self.roomId = 0;
			self.ownerPlayerId = -1;

			GamePlayPlayerListComponent gamePlayPlayerListComponent = self.AddComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.InitWhenGlobal();
            
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.AddComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.InitWhenGlobal();

			self.CreateGamePlayMode();
			
			self.gamePlayStatus = GamePlayStatus.WaitForStart;

			//self.NoticeToClientAll();
		}
		
		public static void AddPlayerWhenGlobal(this GamePlayComponent self, long playerId, int playerTeamId)
		{
			GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.AddPlayerWhenGlobal(playerId, playerTeamId);
            
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.AddPlayerWhenGlobal(playerId, playerTeamId);

			
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
			if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefense1 gamePlayTowerDefense1)
			{
				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetComponent<GamePlayTowerDefenseComponent>();
				gamePlayTowerDefenseComponent.NoticeToClient(playerId);
			}
			else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayPK1 gamePlayPK1)
			{
				GamePlayPKComponent gamePlayPKComponent = self.GetComponent<GamePlayPKComponent>();
				gamePlayPKComponent.NoticeToClient(playerId);
			}
			self.NoticeToClient(playerId);
		}

		public static void Start(this GamePlayComponent self)
		{
			self.gamePlayStatus = GamePlayStatus.Gaming;
			self.NoticeToClientAll();
		}

		public static void GameEnd(this GamePlayComponent self)
		{
			self.gamePlayStatus = GamePlayStatus.GameEnd;
			self.StopAllAI();
			
			self.NoticeToClientAll();
			self.NoticeGameEndToRoom();
		}

		public static void StopAllAI(this GamePlayComponent self)
		{
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.StopAllAI();
		}

		public static void CreateGamePlayMode(this GamePlayComponent self)
		{
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlayBattleConfig();
			if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefense1 gamePlayTowerDefense1)
			{
				self.gamePlayMode = GamePlayMode.TowerDefense;
				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.AddComponent<GamePlayTowerDefenseComponent>();
				gamePlayTowerDefenseComponent.Init(self.ownerPlayerId, gamePlayTowerDefense1.GamePlayModeCfgId);
			}
			else if (gamePlayBattleLevelCfg.GamePlayMode is GamePlayPK1 gamePlayPK1)
			{
				self.gamePlayMode = GamePlayMode.PK;
				GamePlayPKComponent gamePlayPKComponent = self.AddComponent<GamePlayPKComponent>();
				gamePlayPKComponent.Init(self.ownerPlayerId, gamePlayPK1.GamePlayModeCfgId);
			}
		}

		public static GamePlayBattleLevelCfg GetGamePlayBattleConfig(this GamePlayComponent self)
		{
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(self.gamePlayBattleLevelCfgId);
			return gamePlayBattleLevelCfg;
		}
		
		public static List<long> GetPlayerList(this GamePlayComponent self)
		{
			GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
			return gamePlayPlayerListComponent.GetPlayerList();
		}
		
		/// <summary>
		/// 通过这个判断 unitId 是否归属 player, 返回-1则不是玩家的
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unitId"></param>
		/// <returns></returns>
		public static long GetPlayerIdByUnitId(this GamePlayComponent self, long unitId)
		{
			GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
			return gamePlayPlayerListComponent.GetPlayerIdByUnitId(unitId);
		}
		
		public static void PlayerQuitBattle(this GamePlayComponent self, long playerId, bool isNeedRemoveAllPlayerUnits)
		{
			GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent?.PlayerQuitBattle(playerId, isNeedRemoveAllPlayerUnits);
		}

		public static void DealFriendTeamFlag(this GamePlayComponent self, ListComponent<TeamFlagType> teamFlagTypes, bool isWithPlayers, bool reset)
		{
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.DealFriendTeamFlag(teamFlagTypes, isWithPlayers, reset);
		}
		
		public static void NoticeToClientAll(this GamePlayComponent self)
		{
			List<long> playerList = self.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				self.NoticeToClient(playerList[i]);
			}
		}

		public static void ReNoticeToClient(this GamePlayComponent self, long playerId)
		{
			self.NoticeToClient(playerId);

			if (self.gamePlayMode == GamePlayMode.TowerDefense)
			{
				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetComponent<GamePlayTowerDefenseComponent>();
				gamePlayTowerDefenseComponent.NoticeToClient(playerId); 
			}
			else if (self.gamePlayMode == GamePlayMode.PK)
			{
				GamePlayPKComponent gamePlayPKComponent = self.GetComponent<GamePlayPKComponent>();
				gamePlayPKComponent.NoticeToClient(playerId); 
			}
		}

		public static void NoticeToClient(this GamePlayComponent self, long playerId)
		{
			EventType.NoticeGamePlayToClient _NoticeGamePlayChgToClient = new ()
			{
				playerId = playerId,
				gamePlayComponent = self,
			};
			EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayChgToClient);
		}

		public static void NoticeGameEndToRoom(this GamePlayComponent self)
		{
			EventType.NoticeGameEndToRoom _NoticeGameEndToRoom = new ()
			{
				roomId = self.roomId,
			};
			EventSystem.Instance.Publish(self.DomainScene(), _NoticeGameEndToRoom);
		}

		/// <summary>
		/// 指定玩家的阵营,例如塔
		/// </summary>
		/// <param name="self"></param>
		/// <param name="playerId"></param>
		/// <param name="unit"></param>
		public static void AddPlayerUnitTeamFlag(this GamePlayComponent self, long playerId, Unit unit)
		{
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.AddPlayerUnitTeamFlag(playerId, unit);
		}

		/// <summary>
		/// 直接指定阵营信息,例如 大本营，怪物
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unit"></param>
		/// <param name="teamFlag"></param>
		public static void AddUnitTeamFlag(this GamePlayComponent self, Unit unit, TeamFlagType teamFlag)
		{
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.AddUnitTeamFlag(unit, teamFlag);
		}

		/// <summary>
		/// 有召唤者的设置阵营信息，例如子弹
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unitParent"></param>
		/// <param name="unit"></param>
		public static void AddUnitTeamFlagByParent(this GamePlayComponent self, Unit unitParent, Unit unit)
		{
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = self.GetComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.AddUnitTeamFlagByParent(unitParent, unit);
		}
		
		public static string GetPathfindingMapName(this GamePlayComponent self)
		{
			return self.GetGamePlayBattleConfig().SceneMap;
		}
		
		public static float3 GetPlayerBirthPos(this GamePlayComponent self, long playerId)
		{
			GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
			return gamePlayPlayerListComponent.GetPlayerBirthPos(playerId);
		}
		
		public static bool ChkIsGameEnd(this GamePlayComponent self)
		{
			return self.gamePlayStatus == GamePlayStatus.GameEnd;
		}

		public static void DealUnitBeKill(this GamePlayComponent self, Unit attackerUnit, Unit beKillUnit)
		{
			if (self.gamePlayMode == GamePlayMode.TowerDefense)
			{
				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetComponent<GamePlayTowerDefenseComponent>();
				gamePlayTowerDefenseComponent.DealUnitBeKill(attackerUnit, beKillUnit); 
			}
			else if (self.gamePlayMode == GamePlayMode.PK)
			{
				GamePlayPKComponent gamePlayPKComponent = self.GetComponent<GamePlayPKComponent>();
				gamePlayPKComponent.DealUnitBeKill(attackerUnit, beKillUnit); 
			}
		}

		public static int GetPlayerCoin(this GamePlayComponent self, long playerId, CoinType coinType)
		{
			GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetComponent<GamePlayPlayerListComponent>();
			return gamePlayPlayerListComponent.GetPlayerCoin(playerId, coinType);
		}

	}
}