using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using SkillSlotType = ET.AbilityConfig.SkillSlotType;

namespace ET
{
    public static class GamePlayHelper
	{
		public static GamePlayComponent GetGamePlay(Scene scene)
		{
			if (scene == null)
			{
				return null;
			}
			GamePlayComponent gamePlayComponent = scene.GetComponent<GamePlayComponent>();
			return gamePlayComponent;
		}

		public static GamePlayComponent GetGamePlay(Unit unit)
		{
			Scene scene = unit.DomainScene();
			return GetGamePlay(scene);
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(Scene scene)
		{
			return GetGamePlay(scene)?.GetComponent<GamePlayTowerDefenseComponent>();
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(Unit unit)
		{
			return GetGamePlay(unit)?.GetComponent<GamePlayTowerDefenseComponent>();
		}

		//-----------------------------------------------------------

		public static Unit CreateObserverUnit(GamePlayMode gamePlayMode, Scene scene, long playerId)
		{
			float3 position = float3.zero;
			float3 forward = new float3(0, 0, 1);

			Unit unit = UnitHelper_Create.CreateWhenServer_ObserverUnit(scene, playerId, position, forward);

			GamePlayHelper.AddUnitPathfinding(unit);
			GamePlayHelper.AddUnitInfo(playerId, unit);
			return unit;
		}

		public static Unit CreateCameraPlayerUnit(GamePlayMode gamePlayMode, Scene scene, long playerId, int playerLevel, float3 position, float3 forward)
		{
			Unit unit = CreateCameraPlayerUnit(scene, playerId, playerLevel, position, forward);
			return unit;
		}

		public static Unit CreatePlayerUnit(GamePlayMode gamePlayMode, Scene scene, long playerId, int playerLevel, float3 position, float3 forward)
		{
			if (gamePlayMode == GamePlayMode.TowerDefense)
			{
				if (GamePlayHelper.ChkIsAR(scene))
				{
					return null;
				}
				Unit unit = CreatePlayerUnit(scene, playerId, playerLevel, position, forward);
				return unit;
			}
			else if (gamePlayMode == GamePlayMode.PK)
			{
				Unit unit = CreatePlayerUnit(scene, playerId, playerLevel, position, forward);
				return unit;
			}

			return null;
		}

		public static Unit CreatePlayerUnit(Scene scene, long playerId, int playerLevel, float3 position, float3 forward)
		{
			Unit playerUnit = ET.Ability.UnitHelper_Create.CreateWhenServer_PlayerUnit(scene, playerId, playerLevel, position, forward);

			GamePlayHelper.AddUnitPathfinding(playerUnit);
			GamePlayHelper.AddUnitInfo(playerId, playerUnit);
			GamePlayHelper.AddPlayerUnitTeamFlag(playerId, playerUnit);

			UnitCfg unitCfg = playerUnit.model;
			foreach (var item in unitCfg.SkillList)
			{
				string skillCfgId = item.Key;
				ET.AbilityConfig.SkillSlotType skillSlotType = item.Value;
				SkillHelper.LearnSkill(playerUnit, skillCfgId, 1, skillSlotType);
			}

			return playerUnit;
		}

		public static Unit CreateCameraPlayerUnit(Scene scene, long playerId, int playerLevel, float3 position, float3 forward)
		{
			Unit cameraPlayerUnit = ET.Ability.UnitHelper_Create.CreateWhenServer_CameraPlayerUnit(scene, playerId, playerLevel, position, forward);

			GamePlayHelper.AddUnitPathfinding(cameraPlayerUnit);
			GamePlayHelper.AddUnitInfo(playerId, cameraPlayerUnit);
			GamePlayHelper.AddPlayerUnitTeamFlag(playerId, cameraPlayerUnit);

			UnitCfg unitCfg = cameraPlayerUnit.model;
			foreach (var item in unitCfg.SkillList)
			{
				string skillCfgId = item.Key;
				ET.AbilityConfig.SkillSlotType skillSlotType = item.Value;
				SkillHelper.LearnSkill(cameraPlayerUnit, skillCfgId, 1, skillSlotType);
			}

			return cameraPlayerUnit;
		}

		public static Unit CreateBulletByUnit(Scene scene, Unit unitCaster, ActionCfg_FireBullet actionCfgFireBullet, SelectHandle selectHandle,
		ActionContext actionContext)
		{
			Unit bulletUnit = UnitHelper_Create.CreateWhenServer_Bullet(scene, unitCaster, actionCfgFireBullet, selectHandle, actionContext);

			GamePlayHelper.AddUnitTeamFlagByParent(unitCaster, bulletUnit);
			long playerId = GamePlayHelper.GetPlayerIdByUnitId(unitCaster);
			if (playerId != -1)
			{
				GamePlayHelper.AddUnitInfo(playerId, bulletUnit);
			}

			return bulletUnit;
		}

		public static Unit CreateActorByUnit(Scene scene, Unit unitCaster, ActionCfg_CallActor actionCfgCallActor, SelectHandle selectHandle, ref ActionContext actionContext)
		{
			Unit actorUnit = UnitHelper_Create.CreateWhenServer_CallActorUnit(scene, unitCaster, actionCfgCallActor, selectHandle, ref actionContext);

			if (actionCfgCallActor.Duration > 0)
			{
				actorUnit.AddComponent<UnitWaitDestroyComponent, float>(actionCfgCallActor.Duration);
			}

			GamePlayHelper.AddUnitTeamFlagByParent(unitCaster, actorUnit);
			long playerId = GamePlayHelper.GetPlayerIdByUnitId(unitCaster);
			if (playerId != -1)
			{
				GamePlayHelper.AddUnitInfo(playerId, actorUnit);
			}

			return actorUnit;
		}

		public static Unit CreateAoeByUnit(Scene scene, Unit unitCaster, ActionCfg_CallAoe actionCfg_CallAoe, SelectHandle selectHandle, ref ActionContext actionContext)
		{
			Unit aoeUnit = UnitHelper_Create.CreateWhenServer_Aoe(scene, unitCaster, actionCfg_CallAoe, selectHandle, actionContext);

			if (actionCfg_CallAoe.Duration > 0)
			{
				aoeUnit.AddComponent<UnitWaitDestroyComponent, float>(actionCfg_CallAoe.Duration);
			}

			GamePlayHelper.AddUnitTeamFlagByParent(unitCaster, aoeUnit);
			long playerId = GamePlayHelper.GetPlayerIdByUnitId(unitCaster);
			if (playerId != -1)
			{
				GamePlayHelper.AddUnitInfo(playerId, aoeUnit);
			}

			return aoeUnit;
		}

		public static Unit CreateSceneEffect(Scene scene, float3 position, float3 forward)
		{
			Unit unit = UnitHelper_Create.CreateWhenServer_SceneEffect(scene, position, forward);

			GamePlayHelper.AddUnitTeamFlag(unit, TeamFlagType.SceneEffect);

			return unit;
		}

		public static Unit CreateNPC(Scene scene, string unitCfgId, float3 position, float3 forward)
		{
			Unit unit = UnitHelper_Create.CreateWhenServer_NPC(scene, unitCfgId, position, forward);

			GamePlayHelper.AddUnitTeamFlag(unit, TeamFlagType.NPC);

			return unit;
		}

		/// <summary>
		/// 当unit创建时， 存储unitId 和 playerId 的关系
		/// </summary>
		/// <param name="unit"></param>
		public static void AddUnitInfo(long playerId, Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.AddUnitInfo(playerId, unit.Id, unit.Type);
		}

		/// <summary>
		/// 当unit销毁时， 去掉一些已存储的的关系
		/// </summary>
		/// <param name="unit"></param>
		public static void RemoveUnitInfo(Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			if (gamePlayComponent == null)
			{
				return;
			}
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();

			gamePlayPlayerListComponent.RemoveUnitInfo(unit.Id, unit.Type);

			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = gamePlayComponent.GetComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.RemoveUnitTeamFlag(unit.Id);
		}

		public static Unit GetCurPlayerUnit(Unit observerUnit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(observerUnit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			long playerId = observerUnit.Id;
			return gamePlayComponent.GetCurPlayerUnit(playerId);
		}

		public static Unit GetCurPlayerUnit(Unit unit, long playerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			return gamePlayComponent.GetCurPlayerUnit(playerId);
		}

		public static List<Unit> GetPlayerUnitList(Unit observerUnit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(observerUnit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			long playerId = observerUnit.Id;
			return gamePlayComponent.GetPlayerUnitList(playerId);
		}

		public static List<Unit> GetPlayerUnitList(Unit unit, long playerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			return gamePlayComponent.GetPlayerUnitList(playerId);
		}

		public static Unit GetCameraPlayerUnit(Unit observerUnit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(observerUnit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			long playerId = observerUnit.Id;
			return gamePlayComponent.GetCameraPlayerUnit(playerId);
		}

		public static Unit GetCameraPlayerUnit(Unit unit, long playerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			return gamePlayComponent.GetCameraPlayerUnit(playerId);
		}

		public static long GetPlayerIdByUnitId(Unit unit)
		{
			if (unit == null)
			{
				return -1;
			}
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			long playerId = gamePlayComponent.GetPlayerIdByUnitId(unit.Id);
			return playerId;
		}

		public static bool ChkIsFriend(Unit curUnit, Unit targetUnit, bool needSamePlayer = false)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(curUnit);
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = gamePlayComponent.GetComponent<GamePlayFriendTeamFlagCompent>();
			return gamePlayFriendTeamFlagCompent.ChkIsFriend(curUnit.Id, targetUnit.Id, needSamePlayer);
		}

		public static bool ChkIsFriend(Scene scene, long curPlayerId, long targetPlayerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = gamePlayComponent.GetComponent<GamePlayFriendTeamFlagCompent>();
			TeamFlagType curTeamFlagType = gamePlayFriendTeamFlagCompent.GetTeamFlagByPlayerId(curPlayerId);
			TeamFlagType targetTeamFlagType = gamePlayFriendTeamFlagCompent.GetTeamFlagByPlayerId(targetPlayerId);
			return ChkIsFriend(scene, curTeamFlagType, targetTeamFlagType);
		}

		public static bool ChkIsFriend(Scene scene, TeamFlagType curTeamFlagType, TeamFlagType targetTeamFlagType)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			return gamePlayComponent.ChkIsFriend(curTeamFlagType, targetTeamFlagType);
		}

		/// <summary>
		/// 指定玩家的阵营,例如塔
		/// </summary>
		/// <param name="self"></param>
		/// <param name="playerId"></param>
		/// <param name="unit"></param>
		public static void AddPlayerUnitTeamFlag(long playerId, Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			gamePlayComponent.AddPlayerUnitTeamFlag(playerId, unit);
		}

		/// <summary>
		/// 直接指定阵营信息,例如 大本营，怪物
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unit"></param>
		/// <param name="teamFlag"></param>
		public static void AddUnitTeamFlag(Unit unit, TeamFlagType teamFlag)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			gamePlayComponent.AddUnitTeamFlag(unit, teamFlag);
		}

		/// <summary>
		/// 有召唤者的设置阵营信息，例如子弹
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unitParent"></param>
		/// <param name="unit"></param>
		public static void AddUnitTeamFlagByParent(Unit unitParent, Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			gamePlayComponent.AddUnitTeamFlagByParent(unitParent, unit);
		}

		public static string GetPathfindingMapName(Scene scene)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			return gamePlayComponent.GetPathfindingMapName();
		}

		public static int GetAOIDis(Scene scene)
		{
			if (ChkIsAR(scene))
			{
				return 100;
			}
			return 60;
		}

		public static bool ChkIsAR(Scene scene)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			return gamePlayComponent.IsAR();
		}

		public static void AddUnitPathfinding(Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			gamePlayComponent.AddUnitPathfinding(unit);
		}

		public static void SetPlayerCoin(Scene scene, long playerId, CoinTypeInGame coinType, int setValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.SetPlayerCoin(playerId, coinType, setValue, GetCoinType.Normal);
		}

		public static bool ChkCanDoCoinAdd(Scene scene)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			if (gamePlayComponent.gamePlayMode == GamePlayMode.TowerDefense)
			{
				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = gamePlayComponent.GetComponent<GamePlayTowerDefenseComponent>();
				if (gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (gamePlayComponent.gamePlayMode == GamePlayMode.PK)
			{
				return false;
			}

			return false;
		}

		/// <summary>
		/// 修改player的代币
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="playerId"></param>
		/// <param name="coinType"></param>
		/// <param name="chgValue"></param>
		public static void ChgPlayerCoin(Scene scene, long playerId, CoinTypeInGame coinType, int chgValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.ChgPlayerCoin(playerId, coinType, chgValue, GetCoinType.Normal);
		}

		public static void ChgPlayerCoinShare(Scene scene, long playerId, CoinTypeInGame coinType, int chgValue, Unit showGetCoinUnit)
		{
			if (chgValue == 0)
			{
				return;
			}
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			TeamFlagType curTeamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerId);
			using ListComponent<long> teamPlayerList = ListComponent<long>.Create();
			foreach (long playerIdTmp in gamePlayComponent.GetPlayerList())
			{
				TeamFlagType teamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerIdTmp);
				if (teamFlagType == curTeamFlagType)
				{
					teamPlayerList.Add(playerIdTmp);
				}
			}

			if (teamPlayerList.Count == 0)
			{
				return;
			}
			int goldOne = (int)(1f*chgValue / teamPlayerList.Count);
			if (goldOne == 0)
			{
				goldOne = 1;
			}
			foreach (long playerIdTmp in teamPlayerList)
			{
				ET.GamePlayHelper.ChgPlayerCoin(scene, playerIdTmp, coinType, goldOne);

				ET.Ability.UnitHelper.AddSyncData_UnitGetCoinShow(playerIdTmp, showGetCoinUnit, coinType, chgValue);
			}
		}

		public static float GetPlayerCoin(Scene scene, long playerId, CoinTypeInGame coinType)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			return gamePlayPlayerListComponent.GetPlayerCoin(playerId, coinType);
		}

		/// <summary>
		/// 修改team的代币
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="playerId"></param>
		/// <param name="coinType"></param>
		/// <param name="chgValue"></param>
		public static void ChgTeamCoin(Scene scene, long playerId, CoinTypeInGame coinType, int chgValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			TeamFlagType teamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerId);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.ChgTeamCoin(teamFlagType, coinType, chgValue, GetCoinType.Normal);
		}

		public static float GetARScale(Scene scene)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			float fARScale = gamePlayComponent.GetARScale();
			return fARScale;
		}

		public static float GetAudioMaxDis(Scene scene)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			if (gamePlayComponent == null)
			{
				return 300;
			}
			if (ChkIsAR(scene))
			{
				return GetARScale(scene) * 10;
			}
			else
			{
				return 300;
			}
		}

		public static string GetBattleCfgId(RoomTypeInfo roomTypeInfo)
		{
			RoomType RoomTypeIn = roomTypeInfo.roomType;
			SubRoomType SubRoomTypeIn = roomTypeInfo.subRoomType;
			int seasonCfgId = roomTypeInfo.seasonCfgId;
			int pveIndex = roomTypeInfo.pveIndex;

			string battleCfgId = "";
			if (RoomTypeIn == RoomType.Normal)
			{
				if (SubRoomTypeIn == SubRoomType.NormalRoom)
				{
					battleCfgId = "GamePlayBattleLevel_Room11";
				}
				else if (SubRoomTypeIn == SubRoomType.NormalARCreate)
				{
					battleCfgId = "GamePlayBattleLevel_ARRoom";
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVE)
				{
					if (seasonCfgId > 0)
					{
						battleCfgId = SeasonChallengeLevelCfgCategory.Instance.GetCurChallengeGamePlayBattleLevelCfgId(seasonCfgId, pveIndex, false);
					}
					else
					{
						battleCfgId = TowerDefense_ChallengeLevelCfgCategory.Instance.GetCurChallengeGamePlayBattleLevelCfgId(pveIndex, false);
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVP)
				{
					if (ET.SceneHelper.ChkIsGameModeArcade())
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeNoARPVPCfgId;
					}
					else
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.NoARPVPCfgId;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalEndlessChallenge)
				{
					if (ET.SceneHelper.ChkIsGameModeArcade())
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeNoAREndlessChallengeCfgId;
					}
					else
					{
						if (seasonCfgId > 0)
						{
							SeasonInfoCfg seasonInfoCfg = SeasonInfoCfgCategory.Instance.Get(seasonCfgId);
							if (seasonInfoCfg != null)
							{
								battleCfgId = seasonInfoCfg.NoAREndlessChallengeCfgId;
							}
						}
						else
						{
							battleCfgId = GlobalSettingCfgCategory.Instance.NoAREndlessChallengeCfgId;
						}
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalScanMesh)
				{
					battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeNoARScanMeshCfgId;
				}
				else if (SubRoomTypeIn == SubRoomType.NormalARScanCode)
				{
					return "";
				}
			}
			else if (RoomTypeIn == RoomType.AR)
			{
				if (SubRoomTypeIn == SubRoomType.ARPVE)
				{
					if (seasonCfgId > 0)
					{
						battleCfgId = SeasonChallengeLevelCfgCategory.Instance.GetCurChallengeGamePlayBattleLevelCfgId(seasonCfgId, pveIndex, true);
					}
					else
					{
						battleCfgId = TowerDefense_ChallengeLevelCfgCategory.Instance.GetCurChallengeGamePlayBattleLevelCfgId(pveIndex, true);
					}
				}
				else if (SubRoomTypeIn == SubRoomType.ARPVP)
				{
					if (ET.SceneHelper.ChkIsGameModeArcade())
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeARPVPCfgId;
					}
					else
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.ARPVPCfgId;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.AREndlessChallenge)
				{
					if (ET.SceneHelper.ChkIsGameModeArcade())
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeAREndlessChallengeCfgId;
					}
					else
					{
						if (seasonCfgId > 0)
						{
							SeasonInfoCfg seasonInfoCfg = SeasonInfoCfgCategory.Instance.Get(seasonCfgId);
							if (seasonInfoCfg != null)
							{
								battleCfgId = seasonInfoCfg.AREndlessChallengeCfgId;
							}
						}
						else
						{
							battleCfgId = GlobalSettingCfgCategory.Instance.AREndlessChallengeCfgId;
						}
					}
				}
				else if (SubRoomTypeIn == SubRoomType.ARTutorialFirst)
				{
					battleCfgId = GlobalSettingCfgCategory.Instance.ARTutorialFirstCfgId;
				}
				else if (SubRoomTypeIn == SubRoomType.ArcadeScanMesh)
				{
					battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeARScanMeshCfgId;
				}
				else if (SubRoomTypeIn == SubRoomType.ARScanCode)
				{
					return "";
				}
			}

			if (string.IsNullOrEmpty(battleCfgId))
			{
				Log.Error($"string.IsNullOrEmpty({battleCfgId}) when roomTypeInfo=[{roomTypeInfo}]");
				return "";
			}
			if (GamePlayBattleLevelCfgCategory.Instance.Contain(battleCfgId) == false)
			{
				Log.Error($"GamePlayBattleLevelCfgCategory.Instance.Contain({battleCfgId}) == false");
				return "";
			}

			return battleCfgId;
		}

		public static RoomTypeInfo GetRoomTypeInfo(RoomType RoomTypeIn, SubRoomType SubRoomTypeIn, int seasonCfgId = -1, int pveIndex = -1, string gamePlayBattleLevelCfgId = "")
		{
			RoomTypeInfo roomTypeInfo = new();
			roomTypeInfo.roomType = RoomTypeIn;
			roomTypeInfo.subRoomType = SubRoomTypeIn;
			//roomTypeInfo.seasonIndex = seasonCfgId;
			roomTypeInfo.seasonCfgId = seasonCfgId;
			roomTypeInfo.pveIndex = pveIndex;
			if (RoomTypeIn == RoomType.AR && SubRoomTypeIn == SubRoomType.ARScanCode)
			{
				return roomTypeInfo;
			}
			else if (RoomTypeIn == RoomType.Normal && SubRoomTypeIn == SubRoomType.NormalARScanCode)
			{
				return roomTypeInfo;
			}

			if (string.IsNullOrEmpty(gamePlayBattleLevelCfgId))
			{
				roomTypeInfo.gamePlayBattleLevelCfgId = ET.GamePlayHelper.GetBattleCfgId(roomTypeInfo);
			}
			else
			{
				roomTypeInfo.gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId;
			}

			if (RoomTypeIn == RoomType.Normal)
			{
				switch (SubRoomTypeIn)
				{
					case SubRoomType.None:
						Log.Error($"RoomTypeIn == RoomType.Normal SubRoomTypeIn==SubRoomType.None");
						return roomTypeInfo;
				}
			}
			else if(RoomTypeIn == RoomType.AR)
			{

			}

			return roomTypeInfo;
		}

		public static int GetPhysicalCostPVE()
		{
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			if (ET.SceneHelper.ChkIsDemoShow())
			{
				return 1;
			}
			return GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength;
		}

		public static int GetPhysicalCostPVP()
		{
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			if (ET.SceneHelper.ChkIsDemoShow())
			{
				return 1;
			}
			return GlobalSettingCfgCategory.Instance.ARPVPCfgTakePhsicalStrength;
		}

		public static int GetPhysicalCostEndlessChallenge()
		{
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			if (ET.SceneHelper.ChkIsDemoShow())
			{
				return 1;
			}
			return GlobalSettingCfgCategory.Instance.AREndlessChallengeTakePhsicalStrength;
		}

		public static int GetPhysicalCost(RoomTypeInfo roomTypeInfo)
		{
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			RoomType RoomTypeIn = roomTypeInfo.roomType;
			SubRoomType SubRoomTypeIn = roomTypeInfo.subRoomType;
			int physicalCost = 0;
			if (RoomTypeIn == RoomType.Normal)
			{
				if (SubRoomTypeIn == SubRoomType.NormalRoom)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.NormalARCreate)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVE)
				{
					physicalCost = GetPhysicalCostPVE();
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVP)
				{
					physicalCost = GetPhysicalCostPVP();
				}
				else if (SubRoomTypeIn == SubRoomType.NormalEndlessChallenge)
				{
					physicalCost = GetPhysicalCostEndlessChallenge();
				}
				else if (SubRoomTypeIn == SubRoomType.NormalScanMesh)
				{
					physicalCost = 0;
				}
			}
			else if (RoomTypeIn == RoomType.AR)
			{
				if (SubRoomTypeIn == SubRoomType.ARPVE)
				{
					physicalCost = GetPhysicalCostPVE();
				}
				else if (SubRoomTypeIn == SubRoomType.ARPVP)
				{
					physicalCost = GetPhysicalCostPVP();
				}
				else if (SubRoomTypeIn == SubRoomType.AREndlessChallenge)
				{
					physicalCost = GetPhysicalCostEndlessChallenge();
				}
				else if (SubRoomTypeIn == SubRoomType.ARTutorialFirst)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.ArcadeScanMesh)
				{
					physicalCost = 0;
				}
			}

			return physicalCost;
		}


		public static int GetArcadeCoinCost(RoomTypeInfo roomTypeInfo, bool isRecover)
		{
			if (ET.SceneHelper.ChkIsGameModeArcade() == false)
			{
				return 0;
			}
			RoomType RoomTypeIn = roomTypeInfo.roomType;
			SubRoomType SubRoomTypeIn = roomTypeInfo.subRoomType;
			int arcadeCoinCost = 0;
			if (RoomTypeIn == RoomType.Normal)
			{
				if (SubRoomTypeIn == SubRoomType.NormalRoom)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.NormalARCreate)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVE)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVP)
				{
					if (isRecover)
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenRevive;
					}
					else
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenStart;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalEndlessChallenge)
				{
					if (isRecover)
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenRevive;
					}
					else
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenStart;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalScanMesh)
				{
					arcadeCoinCost = 0;
				}
			}
			else if (RoomTypeIn == RoomType.AR)
			{
				if (SubRoomTypeIn == SubRoomType.ARPVE)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.ARPVP)
				{
					if (isRecover)
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenRevive;
					}
					else
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenStart;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.AREndlessChallenge)
				{
					if (isRecover)
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenRevive;
					}
					else
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenStart;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.ARTutorialFirst)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.ArcadeScanMesh)
				{
					arcadeCoinCost = 0;
				}
			}

			return arcadeCoinCost;
		}

		public static int GetArcadeCoinCost(GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent, bool isRecover)
		{
			if (ET.SceneHelper.ChkIsGameModeArcade() == false)
			{
				return 0;
			}
			int arcadeCoinCost = 0;
			if (gamePlayTowerDefenseComponent.IsPVPMode())
			{
				if (isRecover)
				{
					arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenRevive;
				}
				else
				{
					arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenStart;
				}
			}
			else if (gamePlayTowerDefenseComponent.IsEndlessChallengeMode())
			{
				if (isRecover)
				{
					arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenRevive;
				}
				else
				{
					arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenStart;
				}
			}
			return arcadeCoinCost;
		}

		public static async ETTask DoCreateActions(Unit unit, List<string> createActionIds)
		{
			if (createActionIds == null || createActionIds.Count == 0)
			{
				return;
			}

			bool isReady = await ET.AOIHelper.ChkAOIReady(null, unit);
			if (isReady == false)
			{
				return;
			}

			SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(unit);
			ActionContext actionContext = new ActionContext()
			{
				unitId = unit.Id,
			};
			foreach (var actionId in createActionIds)
			{
				ActionHandlerHelper.CreateAction(unit, null, actionId, 0.1f, selectHandleSelf, ref actionContext);
			}
		}

		public static float GetGamePlayNumericValueByPlayerId(Scene scene, long playerId, GameNumericType gameNumericType)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayNumericComponent gamePlayNumericComponent = gamePlayComponent.GetComponent<GamePlayNumericComponent>();
			return gamePlayNumericComponent.GetPlayerNumeric(playerId, gameNumericType);
		}

		public static float GetGamePlayNumericValueByHomeTeamFlagType(Scene scene, TeamFlagType homeTeamFlagType, GameNumericType gameNumericType)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayNumericComponent gamePlayNumericComponent = gamePlayComponent.GetComponent<GamePlayNumericComponent>();
			return gamePlayNumericComponent.GetHomeTeamFlagTypeNumeric(homeTeamFlagType, gameNumericType);
		}

		public static void ChgGamePlayNumericValueByPlayerId(Scene scene, long playerId, GameNumericType gameNumericType, float chgValue, bool isReset)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayNumericComponent gamePlayNumericComponent = gamePlayComponent.GetComponent<GamePlayNumericComponent>();
			gamePlayNumericComponent.ChgPlayerNumeric(playerId, gameNumericType, chgValue, isReset);
		}

		public static void ChgGamePlayNumericValueByHomeTeamFlagType(Scene scene, TeamFlagType homeTeamFlagType, GameNumericType gameNumericType, float chgValue, bool isReset)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayNumericComponent gamePlayNumericComponent = gamePlayComponent.GetComponent<GamePlayNumericComponent>();
			gamePlayNumericComponent.ChgHomeTeamFlagTypeNumeric(homeTeamFlagType, gameNumericType, chgValue, isReset);
		}

        public static TeamFlagType GetHomeTeamFlagType(TeamFlagType teamFlagType)
        {
            // if (teamFlagType == TeamFlagType.Monster1
            //     || teamFlagType == TeamFlagType.TeamPlayer1
            //     || teamFlagType == TeamFlagType.TeamGlobal1)
            // {
            //     return TeamFlagType.TeamGlobal1;
            // }
            // else if (teamFlagType == TeamFlagType.Monster2
            //          || teamFlagType == TeamFlagType.TeamPlayer2
            //          || teamFlagType == TeamFlagType.TeamGlobal2)
            // {
            //     return TeamFlagType.TeamGlobal2;
            // }
            // else if (teamFlagType == TeamFlagType.Monster3
            //          || teamFlagType == TeamFlagType.TeamPlayer3
            //          || teamFlagType == TeamFlagType.TeamGlobal3)
            // {
            //     return TeamFlagType.TeamGlobal3;
            // }
            // else if (teamFlagType == TeamFlagType.Monster4
            //          || teamFlagType == TeamFlagType.TeamPlayer4
            //          || teamFlagType == TeamFlagType.TeamGlobal4)
            // {
            //     return TeamFlagType.TeamGlobal4;
            // }
            // else if (teamFlagType == TeamFlagType.Monster5
            //          || teamFlagType == TeamFlagType.TeamPlayer5
            //          || teamFlagType == TeamFlagType.TeamGlobal5)
            // {
            //     return TeamFlagType.TeamGlobal5;
            // }
            // else
            // {
            //     return TeamFlagType.TeamGlobal1;
            // }

            if (teamFlagType == TeamFlagType.TeamPlayer1
                || teamFlagType == TeamFlagType.TeamGlobal1)
            {
	            return TeamFlagType.TeamGlobal1;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer2
                     || teamFlagType == TeamFlagType.TeamGlobal2)
            {
	            return TeamFlagType.TeamGlobal2;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer3
                     || teamFlagType == TeamFlagType.TeamGlobal3)
            {
	            return TeamFlagType.TeamGlobal3;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer4
                     || teamFlagType == TeamFlagType.TeamGlobal4)
            {
	            return TeamFlagType.TeamGlobal4;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer5
                     || teamFlagType == TeamFlagType.TeamGlobal5)
            {
	            return TeamFlagType.TeamGlobal5;
            }
            else
            {
	            return TeamFlagType.None;
            }
        }

        public static TeamFlagType GetMonsterTeamFlagType(TeamFlagType teamFlagType)
        {
            if (teamFlagType == TeamFlagType.Monster1
                || teamFlagType == TeamFlagType.TeamPlayer1
                || teamFlagType == TeamFlagType.TeamGlobal1)
            {
                return TeamFlagType.Monster1;
            }
            else if (teamFlagType == TeamFlagType.Monster2
                     || teamFlagType == TeamFlagType.TeamPlayer2
                     || teamFlagType == TeamFlagType.TeamGlobal2)
            {
                return TeamFlagType.Monster2;
            }
            else if (teamFlagType == TeamFlagType.Monster3
                     || teamFlagType == TeamFlagType.TeamPlayer3
                     || teamFlagType == TeamFlagType.TeamGlobal3)
            {
                return TeamFlagType.Monster3;
            }
            else if (teamFlagType == TeamFlagType.Monster4
                     || teamFlagType == TeamFlagType.TeamPlayer4
                     || teamFlagType == TeamFlagType.TeamGlobal4)
            {
                return TeamFlagType.Monster4;
            }
            else if (teamFlagType == TeamFlagType.Monster5
                     || teamFlagType == TeamFlagType.TeamPlayer5
                     || teamFlagType == TeamFlagType.TeamGlobal5)
            {
                return TeamFlagType.Monster5;
            }
            else
            {
                return TeamFlagType.Monster1;
            }
        }

        public static TeamFlagType GetHomeTeamFlagTypeByPlayer(Scene scene, long playerId)
        {
	        TeamFlagType teamFlagType = GetGamePlay(scene).GetTeamFlagByPlayerId(playerId);

	        return GetHomeTeamFlagType(teamFlagType);
        }

        public static TeamFlagType GetMonsterTeamFlagTypeByPlayer(Scene scene, long playerId)
        {
	        TeamFlagType teamFlagType = GetGamePlay(scene).GetTeamFlagByPlayerId(playerId);

	        return GetMonsterTeamFlagType(teamFlagType);
        }

	}
}